namespace Database.Models
{
    public partial class Websites
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsDone { get; set; }
    }
}
