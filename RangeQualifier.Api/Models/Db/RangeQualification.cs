using System;
using System.Collections.Generic;

namespace RangeQualifier.Api.Models.Db
{
    public partial class RangeQualification
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int BowTypeId { get; set; }
        public int RangeId { get; set; }
        public int? Score { get; set; }
        public Guid? SignedById { get; set; }
        public DateTime? SignedDate { get; set; }

        public BowType BowType { get; set; }
        public Range Range { get; set; }
        public User SignedBy { get; set; }
        public User User { get; set; }
    }
}
