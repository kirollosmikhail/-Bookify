namespace Bookify.Core.ViewModels
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(100,ErrorMessage ="Max length cannot be more than 5 char.")]
        public string Name { get; set; } = null!;
    }
}
