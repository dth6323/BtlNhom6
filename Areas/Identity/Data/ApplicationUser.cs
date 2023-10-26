using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BtlNhom6.Models;
using Microsoft.AspNetCore.Identity;
namespace BtlNhom6.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() {
            Menus = new HashSet<Menu>();
        }
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string FirstName{ get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
