namespace Democracy.Web.Controllers.API
{
    using Data;
    using Democracy.Web.Data.Entities;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
            return Ok(this.votingEventRepository.GetAllWithUsersAndCandidates());
        }

        [HttpGet("{id}")]
        public IActionResult GetEventWithId([FromRoute] int id)
        {
            return this.Ok(this.votingEventRepository.GetVotingEventWithId(id));
        }

        [HttpGet("Results")]
        public IActionResult GeResults()
        {
            return this.Ok(this.votingEventRepository.GetVotingEventsWithCandidatesEnd());
        }

        [HttpPost]
        public async Task<IActionResult> PostVotingEvent([FromBody] Common.Models.VotingEvent votingEvent)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var entityVotingEvent = new VotingEvent
            {
                
                EventName = votingEvent.EventName,
                Description = votingEvent.Description,
                StartDate = votingEvent.StartDate,
                EndDate = votingEvent.EndDate
            };
            var newVotingEvent = await this.votingEventRepository.CreateAsync(entityVotingEvent);
            return Ok(newVotingEvent);
        }
    }
}