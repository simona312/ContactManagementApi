namespace ContactManagementApi.Models
{
    public class Country
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; } = string.Empty;
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
