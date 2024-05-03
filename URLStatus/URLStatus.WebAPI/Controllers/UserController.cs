using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;
using URLStatus.Application.Logic.User;
using URLStatus.Infrastructure.Auth;
using URLStatus.WebAPI.Application.Auth;
using URLStatus.WebAPI.Application.Response;


namespace URLStatus.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class UserController : BaseController
    {
        private readonly JwtManager _jwtManager;
        private readonly CookieSettings? _cookieSettings;
        private readonly IAntiforgery _antiforgery;

        public UserController(ILogger<UserController> logger,
            IOptions<CookieSettings> cookieSettings,
            JwtManager jwtManager,
            IAntiforgery antiforgery,
            IMediator mediator) : base(logger, mediator) //needed generic fixed
        {
            _antiforgery = antiforgery;
            _cookieSettings = cookieSettings != null ? cookieSettings.Value:null;
            _jwtManager = jwtManager;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult> CreateUserWithAccount([FromBody] CreateUserWithAccount.Request model)
        {
            var createAccountResult = await _mediator.Send(model);
            var token = _jwtManager.GenerateUserToken(createAccountResult.UserId);
            SetTokenCookie(token);
            return Ok(new JwtToken() { AccessToken = token });

        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginToAccount.Request model)
        {
            var loginResult = await _mediator.Send(model);
            var token = _jwtManager.GenerateUserToken(loginResult.UserId);
            SetTokenCookie(token);
            return Ok(new JwtToken() { AccessToken = token });
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            var logoutResult = await _mediator.Send(new LogoutFromAccount.Request());
            DeleteCookie();
            return Ok(logoutResult);
        }

        [HttpGet]
        public async Task<ActionResult> GetLoggedInUser()
        {
            var data = await _mediator.Send(new LoggedInUserQuery.Request() { });
            return Ok(data);
        }
        [HttpGet]
        public async Task<ActionResult> AntiforgeryToken()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            return Ok(tokens.RequestToken);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOption = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.Lax,
            };
            if (_cookieSettings != null)
            {
                cookieOption = new CookieOptions()
                {
                    HttpOnly = cookieOption.HttpOnly,
                    Expires = cookieOption.Expires,

                    Secure = _cookieSettings.Secure,
                    SameSite = _cookieSettings.SameSite,
                };
            }
            Response.Cookies.Append(CookieSettings.CookieName,token,cookieOption);

        }

        private void DeleteCookie()
        {
            Response.Cookies.Delete(CookieSettings.CookieName, new CookieOptions()
            {
                HttpOnly = true,
            });
        }
    }
}