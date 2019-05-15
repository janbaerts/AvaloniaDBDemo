using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaDBDemo
{
    public class Member
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MailAddress { get; set; }

        DateTime membershipExpiryDate;
        public string MembershipExpiryDate
        {
            get
            {
                return membershipExpiryDate.ToShortDateString();
            }
            set
            {
                membershipExpiryDate = DateTime.Parse(value);
            }
        }

        public Member()
        {
            Id = DBAccess.GetNextId();
            membershipExpiryDate = DateTime.Now;
        }

        public void ExtendMembership()
        {
            membershipExpiryDate = membershipExpiryDate.AddYears(1);
        }
    }
}
