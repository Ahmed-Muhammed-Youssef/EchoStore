using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext storeContext;

        public BuggyController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            throw new Exception("server-side error");
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("validationerror/{id}")]
        public ActionResult GetValidationError(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            return Ok();
        }

    }
}
