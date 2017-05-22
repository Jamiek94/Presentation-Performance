using System.ComponentModel.DataAnnotations;

namespace Business
{
    public class WebsiteViewModel
    {
        public WebsiteViewModel() { }

        public WebsiteViewModel(string name, string url)
        {
            Name = name;
            Url = url;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
