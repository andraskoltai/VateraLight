namespace VateraLight.Domain
{
    public class Reservation
    {
        public Guid Guid { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }

        public override string? ToString()
        {
            return String.Format("{{ Guid = {0}, UserName = {1} }}", Guid, User.UserName);
        }
    }
}
