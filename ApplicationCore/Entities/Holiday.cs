 namespace ApplicationCore.Entities
{
    public class Holiday : BaseEnity
    {
        public string Name { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set;}
        public DateTime LastDate { get; set;}
    }
}
