using System.Collections.Generic;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Primitives;
using AuthorizationServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;
using OpenIddict.Models;

namespace AuthorizationServer.Controllers
{
    [Route("api")]
    public class ResourceController : Controller
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _applicationManager;

        public ResourceController(OpenIddictApplicationManager<OpenIddictApplication> applicationManager)
        {
            _applicationManager = applicationManager;
        }

        [Authorize(ActiveAuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        [HttpGet("message")]
        public async Task<IActionResult> GetMessage()
        {
            var subject = User.FindFirst(OpenIdConnectConstants.Claims.Subject)?.Value;
            if (string.IsNullOrEmpty(subject))
            {
                return BadRequest();
            }

            var application = await _applicationManager.FindByClientIdAsync(subject, HttpContext.RequestAborted);
            if (application == null)
            {
                return BadRequest();
            }

            return Content($"{application.DisplayName} has been successfully authenticated.");
        }

        [Authorize(ActiveAuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        [HttpGet("message2")]
        public async Task<IActionResult> GetMessage2()
        {
            var subject = User.FindFirst(OpenIdConnectConstants.Claims.Subject)?.Value;
            if (string.IsNullOrEmpty(subject))
            {
                return BadRequest();
            }

            var application = await _applicationManager.FindByClientIdAsync(subject, HttpContext.RequestAborted);
            if (application == null)
            {
                return BadRequest();
            }

            return Content($"{application.DisplayName}2 has been successfully authenticated.");
        }

        [Authorize(ActiveAuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        [HttpGet("{id}", Name = "People")]
        public IActionResult GetPeople()
        {
            IList<Person> people = new List<Person>()
            {
                new Person()
                {
                    FirstName = "Antonio",
                    LastName = "Larosa"
                },
                 new Person()
                {
                    FirstName = "Luca",
                    LastName = "Dal molin"
                },
                  new Person()
                {
                    FirstName = "Davide",
                    LastName = "Piatti"
                },
                   new Person()
                {
                    FirstName = "Daniele",
                    LastName = "Sicoli"
                },
            };

            return Ok(people);
        }


    }
}