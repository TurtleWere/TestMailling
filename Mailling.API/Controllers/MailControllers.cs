﻿using Microsoft.AspNetCore.Mvc;
using Mailling.Application;
using Mailling.Core;

namespace Mailling.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly MailService _mailService;

        public MailController(MailService mailService)
        {
            _mailService = new MailService();
        }

        [HttpPost("mail")]
        public IActionResult ProcessEmail([FromBody] Mail request)
        {
            var response = _mailService.CreateResponse(request);
            return Ok(response);
        }
    }
}
