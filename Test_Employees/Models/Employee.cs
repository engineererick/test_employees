using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Employees.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "RFC is required")]
        [StringLength(13, MinimumLength = 12, ErrorMessage = "Incomplete RFC; persona física (13 caracteres), persona moral (12 caracteres)")]
        [Display(Name = "RFC")]
        public string RFC { get; set; }

        [Required(ErrorMessage = "Born date is required")]
        [Display(Name = "Born date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        [Required(ErrorMessage = "Employee status is required")]
        [Display(Name = "Status")]
        [EnumDataType(typeof(EmployeeStatus))]
        public EmployeeStatus Status { get; set; }
    }

    public enum EmployeeStatus
    {
        [Display(Name = "Not set")]
        NotSet = 0,

        [Display(Name = "Active")]
        Active = 1,

        [Display(Name = "Inactive")]
        Inactive = 2
    }
}
