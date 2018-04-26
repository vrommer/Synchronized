using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synchronized.Model;

namespace Synchronized.WebApp.Controllers
{
    [Produces("application/json")]
    //[Route("api/Voter")]
    public class VoterController : Controller
    {
        [HttpPost]
        public IActionResult VoteUp([FromBody]Question question)
        {
            question.Points++;
            return new ObjectResult(question);
        }

        // POST: api/Voter/VoteDown
        [HttpPost]
        public IActionResult VoteDown([FromBody]Question question)
        {
            question.Points--;
            return new ObjectResult(question);
        }
    }
}
