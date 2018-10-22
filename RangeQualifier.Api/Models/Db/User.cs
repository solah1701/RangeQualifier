using System;
using System.Collections.Generic;

namespace RangeQualifier.Api.Models.Db
{
    public partial class User
    {
        public User()
        {
            RangeQualificationSignedBy = new HashSet<RangeQualification>();
            RangeQualificationUser = new HashSet<RangeQualification>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ArcheryAustraliaId { get; set; }

        public ICollection<RangeQualification> RangeQualificationSignedBy { get; set; }
        public ICollection<RangeQualification> RangeQualificationUser { get; set; }
    }
}
