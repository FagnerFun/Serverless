using System;
using System.Collections.Generic;
using System.Text;

namespace SendMail.MailTemplate
{
    public sealed class OnboardingTemplate
    {
        public static string Mail
        {
            get
            {
                return @"
                    <!DOCTYPE html>

                    <html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
                    <head>
                        <meta charset='utf-8' />
                        <title></title>
                        <link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css' integrity='sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh' crossorigin='anonymous'>
                    </head>
                    <body>

                        <div class='container'>
                            <div class='row' style='background-color:black'>
                                <div class='col-md-12'>
                                    <img class='mt-4 mb-4' src='https://www.mvpconf.com.br/images/mvpconf/logo-branco.png' height=50px />
                                </div>
                            </div>
                            <div class='row'>
                                <div class='mt-4 col-md-12'>
                                    Bem vindo, {0} !
                                    <br />
                                    <br />
                                    Ao se registrar no MVP Conf 2021 você ajudou uma instituição!.
                                </div>
                            </div>
                            <div class='row'>
                                <div class='col-md-12'>
                                    <a href='https://mvpconf2021api.azure-api.net/api/ValidateUser?id={1}'> Confirme sua conta aqui </a>
                                </div>
                            </div>
                        </div>

                    </body>
                    </html>";
            }
        }
    }
}
