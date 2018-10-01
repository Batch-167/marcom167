using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Is_Delete = false;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int M_Role_Id { get; set; }

        public string Role { get; set; }

        public int M_Employee_Id { get; set; }

        [Display(Name ="Employee")]
        public string Fullname { get; set; }

        public string Company { get; set; }

        public bool Is_Delete { get; set; }

        [Display(Name = "Create By")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [Display(Name = "Create Date")]
        public DateTime Create_Date { get; set; }
    }
}
