namespace ApplicationCore.Entities
{
    public class Combination : BaseEnity
    {
        public string Name { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
