import { AppState } from './../app.service';
import { Component, OnInit, ViewChild } from '@angular/core';
declare var jquery: any;
declare var $: any;
import { Router, ActivatedRoute } from '@angular/router';
import { XTestService } from 'app/xtest.service';

@Component({
    selector: 'page2-app',
    templateUrl: './page2.html'
})
export class page2Component implements OnInit {
    Decrypted: any;
    constructor(public userService: AppState, private route: ActivatedRoute, private xservice: XTestService, private router: Router) {
        this.route.params.subscribe(params => {
            if (!this.isEmpty(params)) {
                var k = this.GetKey();
                if (!this.isEmpty(k)) {
                    this.CipherFromPage1 = params["id"];
                    this.URLhasParam = true;
                }
                else
                {
                    this.router.navigate(['/Page1']);
                }
            }
            else {
                this.URLhasParam = false;
            }
        });
    }
    CipherFromPage1: string;
    KeyFromPage1: string;
    URLhasParam: any;
    decryptKey: any;
    decryptCipher: any
    BackPlainText: any;
    ngOnInit() {
        this.GetKey();
    }
    GetKey(): string {
        let key;
        this.xservice.currentUser.subscribe(value => {
            this.KeyFromPage1 = value;
            key = value;
        });
        return key;
    }

    Decrypt() {
        let body = JSON.stringify({ cipherText: this.decryptCipher, Key: this.decryptKey })
        this.userService.Service("AESAlgorithumDecrypt", body).subscribe(data => {
            this.BackPlainText = data.Decrypted
        });
    }
    BackToPage1() {
        this.router.navigate(['/Page1']);
    }

    isEmpty(obj: any) {
        for (var key in obj) {
            if (obj.hasOwnProperty(key))
                return false;
        }
        return true;
    }
    reveal1() {
        $('#reveal1').click(function () {
            var $pwd = $("#pwd1");
            if ($pwd.attr('type') === 'password') {
                $pwd.attr('type', 'text');
            } else {
                $pwd.attr('type', 'password');
            }
        });
    }
    reveal2() {
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