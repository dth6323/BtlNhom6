namespace BtlNhom6.Models
{
    public class Employee
    {
        public Employee()
        {
            Menu = new HashSet<Menu>();
        }

        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
    }
}
