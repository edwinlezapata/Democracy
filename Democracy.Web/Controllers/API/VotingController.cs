namespace Democracy.Web.Controllers.API
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Democracy.Common.Models;
    using Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VotingController : Controller
    {
        private readonly IVotingRepository votingRepository;
        private readonly IVotingEventRepository votingEventRepository;
        private readonly IUserHelper userHelper;

        public VotingController(
            IVotingRepository votingRepository,
            IVotingEventRepository votingEventRepository,
            IUserHelper userHelper)
        {
            this.votingRepository = votingRepository;
            this.votingEventRepository = votingEventRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetVotes()
        {
            return this.Ok(this.votingRepository.GetVoteWithAll());
        }

        [HttpGet("VotingEvent/{eventId}")]
        public IActionResult GetVotesOfEvent([FromRoute] int eventId)
        {
            return this.Ok(this.votingRepository.GetVoteOfVotingEvent(eventId));
        }

        [HttpGet("Candidate/{candidateId}")]
        public IActionResult GetVotesOfCandidate([FromRoute] int candidateId)
        {
            return this.Ok(this.votingRepository.GetVoteOfCandidate(candidateId));
        }

        #region USER
        [HttpPost("User")]
        public IActionResult GetVoteOfUser([FromBody] Common.Models.Voting searchVote)
        {
            return this.Ok(this.votingRepository.GetUserVotes(searchVote.Email));
        }

        [HttpGet("User/VotingEvent")]
        public IActionResult GetVoteOfUserInEvent([FromBody] Common.Models.Voting searchVote)
        {
            return this.Ok(this.votingRepository.GetUserVoteVotingEvent(searchVote.Email, searchVote.VotingEvent));
        }


        [HttpGet("ValidateVote")]
        public IActionResult GetVoteCount([FromBody] Common.Models.Voting searchVote)
        {
            return this.Ok(this.votingRepository.GetNumberVotes(searchVote.Email, searchVote.VotingEvent));
        }
        #endregion



        [HttpPost]
        public async Task<IActionResult> PostVote([FromBody] Common.Models.Voting voting)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(voting.Email);
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            var votingEvent = await this.votingEventRepository.GetByIdAsync(voting.VotingEvent);

            if (votingEvent == null)
            {
                return this.BadRequest("Invalid event");
            }

            if (votingEvent.StartDate > DateTime.Now)
            {
                return this.BadRequest("The voting event has not started yet");
            }

            if (votingEvent.EndDate < DateTime.Now)
            {
                return this.BadRequest("The voting event has closed");
            }

            int userVote = this.votingRepository.GetNumberVotes(user.Email, votingEvent.Id);
            if (userVote > 0)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "You have already registered a vote in this event, you must wait for the finalization, in order to see the results."
                });
            }

            var candidate = await this.votingEventRepository.GetCandidateAsync(voting.Candidate);
            if (candidate == null)
            {
                return this.BadRequest("Invalid candidate");
            }

            await this.votingEventRepository.UpdateCandidateAsync(candidate);

            var eventVote = new Data.Entities.Voting
            {
                User = user,
                VotingEvent = votingEvent,
                Candidate = candidate
            };

            await this.votingRepository.CreateAsync(eventVote);
            return Ok(new Response
            {
                IsSuccess = true,
                Message = $"Registered vote. The candidate \"{eventVote.Candidate.NameCandidate}\" "
            });

        }
    }
}
