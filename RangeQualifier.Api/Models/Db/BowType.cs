using System;
using System.Collections.Generic;

namespace RangeQualifier.Api.Models.Db
{
    public partial class BowType
    {
        public BowType()
        {
            RangeQualification = new HashSet<RangeQualification>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<RangeQualification> RangeQualification { get; set; }
    }
}
