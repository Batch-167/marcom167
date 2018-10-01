﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCom.ViewModel
{
    public class MenuAccessViewModel
    {
        public int Id { get; set; }

        [DisplayName("Role Code")]
        public int M_Role_Id { get; set; }

        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        public int M_Menu_Id { get; set; }

        public bool Is_Delete { get; set; }

        [DisplayName("Created")]
        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [DisplayName("Create Date")]
        public DateTime Create_Date { get; set; }
    }
}
