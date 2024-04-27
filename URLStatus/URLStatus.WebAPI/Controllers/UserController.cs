using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using URLStatus.Application.Logic.User;


namespace URLStatus.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class UserController : BaseController
    {
        public UserController(ILogger logger, IMediator mediator) : base(logger, mediator)
        {

        }

        [HttpPost]
        public async Task<ActionResult> CreateUserWithAccount([FromBody] CreateUser.Request model)
        {
            var createAccountResult = await _mediator.Send(model);
            return Ok(createAccountResult);
        }
    }
}