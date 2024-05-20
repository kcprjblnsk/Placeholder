using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Options;
using URLStatus.Application.Logic.Account;
using URLStatus.Application.Logic.MonitoredUrl;
using URLStatus.Application.Logic.User;
using URLStatus.Infrastructure.Auth;
using URLStatus.WebAPI.Application.Auth;
using URLStatus.WebAPI.Application.Response;


namespace URLStatus.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class MonitoredUrlController : BaseController
    {


        public MonitoredUrlController(ILogger<MonitoredUrlController> logger,
            IMediator mediator) : base(logger, mediator) //needed generic fixed
        {

        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdate([FromBody] CreateOrUpdateCommand.Request model)
        {
            var data = await _mediator.Send(model);
            return Ok(data);
        }
        [HttpPost]
        public async Task<ActionResult> Delete([FromBody] DeleteCommand.Request model)
        {
            var data = await _mediator.Send(model);
            return Ok(data);
        }
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetQuery.Request model)
        {
            var data = await _mediator.Send(model);
            return Ok(data);
        }
    }
}