using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AvaloniaDBDemo
{
    public class Member
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MailAddress { get; set; }
        CultureInfo dateFormat = new CultureInfo("fr-FR");

        DateTime membershipExpiryDate;
        public string MembershipExpiryDate
        {
            get
            {
                return $"{membershipExpiryDate.Day}/{membershipExpiryDate.Month}/{membershipExpiryDate.Year}";
            }
            set
            {
                membershipExpiryDate = DateTime.Parse(value, dateFormat);
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
