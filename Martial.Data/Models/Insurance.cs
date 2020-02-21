using System;
using System.Collections.Generic;
using System.Text;

namespace Martial.Data.Models
{
    public class Insurance
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Awarded { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
