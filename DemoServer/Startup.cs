using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
//using olympic.Identity;
using System.Configuration;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace olympic
{
    //public partial class Startup
    //{
    //    public void Configuration(IAppBuilder app)
    //    {
    //        HttpConfiguration httpConfig = new HttpConfiguration();
            
    //        ConfigureWebApi(httpConfig);

    //        app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

    //        app.UseWebApi(httpConfig);
    //    }

        

    //    private void ConfigureWebApi(HttpConfiguration config)
    //    {
    //        config.MapHttpAttributeRoutes();

    //        var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
    //        jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    //    }
    //}

    internal class TokenValidationHandler : DelegatingHandler
    {
        private string issuer = ConfigurationManager.AppSettings["msdi:Issuer"];
        private string audienceId = ConfigurationManager.AppSettings["msdi:AudienceId"];
        private string audienceSecret = ConfigurationManager.AppSettings["msdi:AudienceSecret"];

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;
            //determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                //allow requests with no token - whether a action method needs an authentication can be set with the claimsauthorization attribute
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var now = DateTime.UtcNow;
                var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(audienceSecret));

                SecurityToken securityToken;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = issuer,
                    ValidIssuer = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };
                //extract and assign the user of the jwt
                Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = handler.ValidateToken(token, validationParameters, out securityToken);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException e)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }


    }
}
