namespace OdeToFood.Core
{
    public class Rate
    {
        public int Id { get; set; }
        public int RestoRating { get; set; }
        public OwnUser OwnUser { get; set; }
    }
}