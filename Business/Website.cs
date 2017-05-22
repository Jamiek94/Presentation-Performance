using System.ComponentModel.DataAnnotations;

namespace Business
{
    public class Website
    {
        public Website(string name, string url)
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
