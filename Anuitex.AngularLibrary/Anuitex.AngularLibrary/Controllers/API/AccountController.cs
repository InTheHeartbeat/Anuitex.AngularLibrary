using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;
using Anuitex.AngularLibrary.Extensions;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class AccountController : BaseApiController
    {

        [Route("api/Account/GetCurrentUser")]
        [ResponseType(typeof(AuthModel))]
        [HttpGet]
        public AuthModel GetCurrentUser()
        {            
            if (CurrentUser == null && CurrentVisitor != null)
            {
                return new AuthModel() {IsVisitor = true,Token = CurrentVisitor.Token.ToString(), IsAdmin = false};
            }
            if (CurrentUser != null)
            {
                return new AuthModel() {IsAdmin = CurrentUser.IsAdmin, IsVisitor = false, Name = CurrentUser.Login, Token = CurrentUser.AccountAccessRecords.First().Token.ToString()};
            }

            if (CurrentVisitor == null && CurrentUser == null)
            {
                Guid token = Guid.NewGuid();

                Visitor visitor = new Visitor()
                {
                    Token = token,
                    LastAccess = DateTime.Now,
                };

                DataContext.Visitors.InsertOnSubmit(visitor);
                DataContext.SubmitChanges();

                return new AuthModel(){IsVisitor = true,Token = token.ToString()};               
            }
            return null;
        }

        [Route("api/Account/TrySignIn")]
        [ResponseType(typeof(AuthModel))]
        [HttpPost]
        public AuthModel TrySignIn(LoginData data)
        {                        
             Account account = DataContext.Accounts.FirstOrDefault(
                ac => ac.Login == data.login && ac.Hash == data.password.MD5());

            if (account == null)
            {
                return new AuthModel(){Message = "Login or password incorrect"};
            }

            Guid token = Guid.NewGuid();


            string adr = "";
            if (Request.Properties.ContainsKey("MS_HttpContext"))
            {
                adr = ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            AccountAccessRecord previousRecord = account.AccountAccessRecords.FirstOrDefault(r => r.Source == adr);
            if (previousRecord != null)
            {
                DataContext.AccountAccessRecords.DeleteOnSubmit(previousRecord);
                DataContext.SubmitChanges();
            }

            AccountAccessRecord record = new AccountAccessRecord() { ActiveDate = DateTime.Now, Account = account, Source = adr, Token = token };
            DataContext.AccountAccessRecords.InsertOnSubmit(record);
            DataContext.SubmitChanges();

            return new AuthModel() {IsAdmin = account.IsAdmin, Name = account.Login, Token = token.ToString(),IsVisitor = false};
        }

        [Route("api/Account/SignOut")]
        [ResponseType(typeof(void))]
        [HttpGet]
        public IHttpActionResult SignOut()
        {
            if (CurrentUser == null) { return Unauthorized(); }
            string adr = "";
            if (Request.Properties.ContainsKey("MS_HttpContext"))
            {
                adr = ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            AccountAccessRecord previousRecord = CurrentUser?.AccountAccessRecords.FirstOrDefault(r => r.Source == adr);
            if (previousRecord != null)
            {
                DataContext.AccountAccessRecords.DeleteOnSubmit(previousRecord);
                DataContext.SubmitChanges();                
            }

            return Ok();
        }

        [Route("api/Account/TrySignUp")]
        [ResponseType(typeof(AuthModel))]
        [HttpPost]
        public AuthModel TrySignUp(LoginData data)
        {
            AuthModel model = new AuthModel();
            if (DataContext.Accounts.Any(ac => ac.Login == data.login))
            {
                model.Message = "Login already exist";
                return model;
            }

            Account newAccount = new Account()
            {
                Login = data.login,
                Hash = data.password.MD5()
            };

            DataContext.Accounts.InsertOnSubmit(newAccount);
            DataContext.SubmitChanges();

            return TrySignIn(data);
        }
    }

    public class LoginData {
        public string login { get; set; } public string password { get; set; }
    }
}

