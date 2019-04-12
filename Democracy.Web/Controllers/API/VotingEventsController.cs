namespace Democracy.Web.Controllers.API
{
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
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
            return this.Ok(this.votingEventRepository.GetAllWithUsers());
        }
    }
}