namespace ApplicationCore.Entities
{
    public class TypePoint
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Coefficient { get; set; }
        public ICollection<Point> Points { get; set; }
    }
}
