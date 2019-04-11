namespace Democracy.Web.Controllers
{
    using System.Threading.Tasks;
    using Democracy.Web.Data;
    using Democracy.Web.Data.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class VotingEventsController : Controller
    {
        private readonly IRepository repository;

        public VotingEventsController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: VotingEvents
        public IActionResult Index()
        {
            return View(this.repository.GetVotingEvents());
        }

        // GET: VotingEvents/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = this.repository.GetVotingEvent(id.Value);
            if (votingEvent == null)
            {
                return NotFound();
            }

            return View(votingEvent);
        }

        // GET: VotingEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VotingEvents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VotingEvent votingEvent)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddVotingEvent(votingEvent);
                await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(votingEvent);
        }

        // GET: VotingEvents/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = this.repository.GetVotingEvent(id.Value);
            if (votingEvent == null)
            {
                return NotFound();
            }
            return View(votingEvent);
        }

        // POST: VotingEvents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VotingEvent votingEvent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.repository.UpdateVotingEvent(votingEvent);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.repository.VotingEventExists(votingEvent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(votingEvent);
        }

        // GET: VotingEvents/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var votingEvent = this.repository.GetVotingEvent(id.Value);
            if (votingEvent == null)
            {
                return NotFound();
            }

            return View(votingEvent);
        }

        // POST: VotingEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var votingEvent = this.repository.GetVotingEvent(id);
            this.repository.RemoveVotingEvent(votingEvent);
            await this.repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
