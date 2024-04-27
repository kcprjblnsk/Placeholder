﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace URLStatus.WebAPI.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly ILogger _logger;
    protected readonly IMediator _mediator;

    public BaseController(ILogger logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
}