namespace ApplicationCore.Entities
{
    public class TypePoint : BaseEnity
    {
        public string Name { get; set; }
        public int? Coefficient { get; set; }
        public ICollection<Point> Points { get; set; }
    }
}
