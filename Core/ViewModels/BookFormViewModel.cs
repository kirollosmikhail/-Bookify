﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookify.Core.ViewModels
{
    public class BookFormViewModel
    {
        public int Id { get; set; }

        [MaxLength(500,ErrorMessage =Errors.MaxLength)]
        public string Title { get; set; } = null!;
        [Display(Name ="Author")]
        public int AuthorId { get; set; }

        public IEnumerable<SelectListItem>? Authors { get; set; }

        [MaxLength(200, ErrorMessage = Errors.MaxLength)]
        public string Publisher { get; set; } = null!;

        [Display(Name ="Publishing Date")]
        public DateTime PublishingDate { get; set; }

        public IFormFile? Image { get; set; }

        [MaxLength(50, ErrorMessage = Errors.MaxLength)]
        public string Hall { get; set; } = null!;
        [Display(Name = "Is available for rental?")]
        public bool IsAvailableForRental { get; set; }

        public string Description { get; set; } = null!;

        public IList<int> SelectedCategories { get; set; } = new List<int>();

        public IEnumerable<SelectListItem>? Categories { get; set; }


    }
}
