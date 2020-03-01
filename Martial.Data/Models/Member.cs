using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Martial.Data.Models
{
    public enum Belt { White, Yellow, Green , Purple, Orange, Blue, Brown, Red, Black}
    public class Member
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Display(Name = "Belt Classification")]
        public Belt Belt { get; set; }

        [Display(Name = "Date Insurance Due")]
        [DataType(DataType.Date)]
        public DateTime InsuranceDue { get; set; }

        [Display(Name = "Insurance Paid?")]
        public bool InsurancePaid { get; set; }
        public IList<Badge> Badges { get; set; } = new List<Badge>();

    }
}
