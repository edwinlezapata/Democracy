namespace Democracy.Web.Models
{
    using Democracy.Web.Data.Entities;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CandidateViewModel : Candidate
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
