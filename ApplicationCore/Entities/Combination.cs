namespace ApplicationCore.Entities
{
    public class Combination
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
