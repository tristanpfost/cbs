using System;

namespace Read
{
    public class VolunteerUser{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string NationalSociety { get; set; }
        public string PreferredLanguage { get; set; }
        public string GpsLocation { get; set; }
        public string MobilePhoneNumber{ get; set; }
        public string Email { get; set; }
    }
}
