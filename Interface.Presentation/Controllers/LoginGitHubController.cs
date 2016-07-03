using BugTracker.Domain.Interface.Service;
using Interface.Presentation.Services;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Interface.Presentation.Controllers
{
    public class LoginGitHubController : Controller
    {
        const string clientId = "d6fe6834f30081cf609d";
        private const string clientSecret = "d829abc7d3a9435cdae06599d698106f9066420d";

        IUserService userService = UserServiceInjection.Create();

        readonly GitHubClient client = new GitHubClient(
            new ProductHeaderValue("BugTracker-GitHub-Oauth"), 
            new Uri("https://github.com/"));

        [HttpGet]
        public async Task<ActionResult> GithubAuthentication(string code, string state)
        {
            if (!String.IsNullOrEmpty(code))
            {
                var expectedState = Session["CSRF:State"] as string;
                //TODO: Tratar erro de segurança
                if (state != expectedState) throw new InvalidOperationException("SECURITY FAIL!");
                Session["CSRF:State"] = null;

                var token = await client.Oauth.CreateAccessToken(
                    new OauthTokenRequest(clientId, clientSecret, code)
                    {
                        RedirectUri = new Uri("http://localhost:58173/logingithub/githubauthentication")
                    });
                Session["OAuthToken"] = token.AccessToken;
            }

            return RedirectToAction("SignInGitHub");
        }

        [HttpGet]
        public async Task<ActionResult> SignInGitHub()
        {
            var accessToken = Session["OAuthToken"] as string;

            if (accessToken != null)
            {
                client.Credentials = new Credentials(accessToken);
            }

            try
            {
                var email = await client.User.Email.GetAll();

                string primaryEmail = email.FirstOrDefault(e => e.Primary == true).Email;

                var gitHubUser = await client.User.Current();

                string name = gitHubUser.Name != null ? gitHubUser.Name : gitHubUser.Login;

                string profileImage = DownloadImageService.DownloadUserImage(gitHubUser.AvatarUrl,gitHubUser.Login);

                BugTracker.Domain.Entity.User userFound = userService.FindByEmail(primaryEmail);

                if (userFound == null)
                {
                    userFound = userService.Add(new BugTracker.Domain.Entity.User(
                            name,
                            primaryEmail,
                            null,
                            profileImage,
                            Guid.NewGuid().ToString(),
                            null,
                            true,
                            true

                        ));
                }

                UserSessionService.CreateSession(new Models.User.LoggedUserViewModel(userFound));

                return RedirectToAction("Index","User");

            }

            catch (AuthorizationException)
            {
                return Redirect(GetOauthLoginUrl());
            }
        }

        private string GetOauthLoginUrl()
        {
            string csrf = Membership.GeneratePassword(24, 1);
            Session["CSRF:State"] = csrf;

            var request = new OauthLoginRequest(clientId)
            {
                Scopes = { "user", "email" },
                State = csrf
            };
            var oauthLoginUrl = client.Oauth.GetGitHubLoginUrl(request);
            return oauthLoginUrl.ToString();
        }
    }
}