namespace PlatformService.Models
{
    public interface IPlatform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Cost { get; set; }
    }
}