import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppState } from 'app/app.service';
import { XTestService } from 'app/xtest.service';
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'page1-app',
  templateUrl: './page1.html',
})
export class Page1Component {
  constructor(public userService: AppState,private router: Router,private xservice: XTestService ) { }
  Topic = 'AES Algorithum Demo Presentation.';
  EncryptPlainText: any;
  EncryptKey: any;
  CipherText: string;
  Formsubmit() { 
    let body = JSON.stringify({ PlainText: this.EncryptPlainText, Key: this.EncryptKey })
    this.userService.Service("AESAlgorithumEncrypt", body).subscribe(data => {
      this.CipherText = data.Encrypted;
    });
  }


  SendKey:any;
  SendPlainText:any;
  FormSend()
  {
    this.xservice.SetFunction(this.SendKey);
    let body = JSON.stringify({ PlainText: this.SendPlainText, Key: this.SendKey });
    this.userService.Service("AESAlgorithumEncrypt", body).subscribe(data =>{   
      var cipher = data.Encrypted;
      this.router.navigate(['/Page2', cipher]);
    });
    
  }
  reveal1()
  {
    $('#reveal1').click(function () {
      var $pwd = $("#pwd1");
      if ($pwd.attr('type') === 'password') {
          $pwd.attr('type', 'text');
      } else {
          $pwd.attr('type', 'password');
      }
  });
  }
  reveal2()
  {  
    $('#reveal2').click(function () {
      var $pwd = $("#pwd2");
      if ($pwd.attr('type') === 'password') {
          $pwd.attr('type', 'text');
      } else {
          $pwd.attr('type', 'password');
      }
  });    
  }
}
