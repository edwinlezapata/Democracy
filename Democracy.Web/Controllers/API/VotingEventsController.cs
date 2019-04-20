namespace Democracy.Web.Controllers.API
{
    using Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VotingEventsController : Controller
    {
        private readonly IVotingEventRepository votingEventRepository;

        public VotingEventsController(IVotingEventRepository votingEventRepository)
        {
            this.votingEventRepository = votingEventRepository;
        }

        [HttpGet]
       public IActionResult GetVotingEvents()
       {
         return Ok(this.votingEventRepository.GetAllWithUsers());
       }
    }
}