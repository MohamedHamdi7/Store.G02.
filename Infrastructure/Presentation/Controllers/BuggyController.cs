using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BuggyController:ControllerBase
    {
        //

       [HttpGet("NotFound")]  //api/Buggy/NotFound
       public IActionResult GetNotFoundRequest()
       {
           return NotFound(); //404
       }

        [HttpGet("servererror")] //api/Buggy/ServerError
        public IActionResult GetAServerErrorRequest()
        {
            throw new Exception(); //500

            return Ok();
        }

        [HttpGet("badrequest")]  //api/Buggy/BadRequest
        public IActionResult GetBadRequest()  
        {
            return BadRequest(); //400
        }

        [HttpGet("badrequest/{id}")]  //api/Buggy/BadRequest/ahmed
        public IActionResult GetBadRequest(int id)  //validation Error
        {
            return BadRequest(); //400
        }

        [HttpGet("unauthorized")] //api/Buggy/unauthorized
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized(); //401
        }


    }
}
