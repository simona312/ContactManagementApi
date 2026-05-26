namespace ContactManagementApi.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
