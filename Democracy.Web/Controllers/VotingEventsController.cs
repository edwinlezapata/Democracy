namespace Democracy.Web.Controllers
{
    using Data;
    using Data.Entities;
    using Democracy.Web.Helpers;
    using Democracy.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    
    public class VotingEventsController : Controller
    {
        private readonly IVotingEventRepository votingEventRepository;
        private readonly IUserHelper userHelper;

        public VotingEventsController(IVotingEventRepository votingEventRepository, IUserHelper userHelper)
        {
            this.votingEventRepository = votingEventRepository;
            this.userHelper = userHelper;
        }

        // GET: VotingEvents
        public IActionResult Index()
        {
            return View(this.votingEventRepository.GetAll().OrderBy(v => v.EventName));
        }

        // GET: VotingEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VotingEventNotFound");
            }

            var votingEvent = await this.votingEventRepository.GetByIdAsync(id.Value);
            if (votingEvent == null)
            {
                return new NotFoundViewResult("VotingEventNotFound");
            }

            return View(votingEvent);
        }

        // GET: VotingEvents/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: VotingEvents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VotingEventViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.png";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\VotingEvents",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/VotingEvents/{file}";
                }

                var votingEvent = this.ToVotingEvent(view, path);

                
                votingEvent.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await this.votingEventRepository.CreateAsync(votingEvent);
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private VotingEvent ToVotingEvent(VotingEventViewModel view, string path)
        {
            return new VotingEvent
            {
                Id = view.Id,
                ImageUrl = path,
                EventName = view.EventName,
                Description = view.Description,
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                User = view.User
            };
        }

        // GET: VotingEvents/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VotingEventNotFound");
            }

            var votingEvent = await this.votingEventRepository.GetByIdAsync(id.Value);
            if (votingEvent == null)
            {
                return new NotFoundViewResult("VotingEventNotFound");
            }

            var view = this.ToVotingEventViewModel(votingEvent);
            return View(view);
        }

        private VotingEventViewModel ToVotingEventViewModel(VotingEvent votingEvent)
        {
            return new VotingEventViewModel
            {
                Id = votingEvent.Id,
                ImageUrl = votingEvent.ImageUrl,
                EventName = votingEvent.EventName,
                Description = votingEvent.Description,
                StartDate = votingEvent.StartDate,
                EndDate = votingEvent.EndDate,
                User = votingEvent.User
            };
        }

        // POST: VotingEvents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VotingEventViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = view.ImageUrl;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.png";

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\VotingEvents",
                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/VotingEvents/{file}";
                    }

                    var votingEvent = this.ToVotingEvent(view, path);

                    
                    votingEvent.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await this.votingEventRepository.UpdateAsync(votingEvent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.votingEventRepository.ExistAsync(view.Id))
                    {
                        return new NotFoundViewResult("VotingEventNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: VotingEvents/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VotingEventNotFound");
            }

            var votingEvent = await this.votingEventRepository.GetByIdAsync(id.Value);
            if (votingEvent == null)
            {
                return new NotFoundViewResult("VotingEventNotFound");
            }

            return View(votingEvent);
        }

        // POST: VotingEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var votingEvent = await this.votingEventRepository.GetByIdAsync(id);
            await this.votingEventRepository.DeleteAsync(votingEvent);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult VotingEventNotFound()
        {
            return this.View();
        }

    }
}
