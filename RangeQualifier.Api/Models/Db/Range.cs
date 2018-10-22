using System;
using System.Collections.Generic;

namespace RangeQualifier.Api.Models.Db
{
    public partial class Range
    {
        public Range()
        {
            RangeQualification = new HashSet<RangeQualification>();
        }

        public int Id { get; set; }
        public int Distance { get; set; }

        public ICollection<RangeQualification> RangeQualification { get; set; }
    }
}
