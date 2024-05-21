using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Options;
using URLStatus.Application.Logic.Account;
using URLStatus.Application.Logic.MonitoredUrl;
using URLStatus.Application.Logic.ResultRule;
using URLStatus.Application.Logic.User;
using URLStatus.Infrastructure.Auth;
using URLStatus.WebAPI.Application.Auth;
using URLStatus.WebAPI.Application.Response;


namespace URLStatus.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class ResultRuleController : BaseController
    {


        public ResultRuleController(ILogger<ResultRuleController> logger,
            IMediator mediator) : base(logger, mediator) //needed generic fixed
        {

        }

        [HttpGet]
        public async Task<ActionResult> Settings([FromQuery] SettingsQuery.Request model)
        {
            var data = await _mediator.Send(model);
            return Ok(data);
        }

    }
}