namespace BtlNhom6.Models
{
    public class Employee
    {
        public Employee()
        {
            MenuDetail = new HashSet<MenuDetail>();
        }

        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<MenuDetail> MenuDetail { get; set; }
    }
}
