using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Options;
using URLStatus.Application.Logic.Account;
using URLStatus.Application.Logic.User;
using URLStatus.Infrastructure.Auth;
using URLStatus.WebAPI.Application.Auth;
using URLStatus.WebAPI.Application.Response;


namespace URLStatus.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class AccountController : BaseController
    {


        public AccountController(ILogger<AccountController> logger,
            IMediator mediator) : base(logger, mediator) //needed generic fixed
        {

        }


        [HttpGet]
        public async Task<ActionResult> GetCurrentAccount()
        {
            var data = await _mediator.Send(new CurrentAccountQuery.Request() { });
            return Ok(data);
        }

       


        
    }
}