using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Text;

namespace AvaloniaDBDemo
{
    public class DBAccess
    {
        public static IList<Member> GetAllMembers()
        {
            ConnectionFactory cf = new ConnectionFactory();
            IList<Member> members = new List<Member>();
            string statement =
                "SELECT * " +
                "FROM Members";

            using (cf.Connection)
            {
                SQLiteCommand selectCommand = new SQLiteCommand(statement, cf.Connection);
                cf.Connection.Open();
                SQLiteDataReader sdr = selectCommand.ExecuteReader();

                int a = sdr.GetOrdinal("Id");
                int b = sdr.GetOrdinal("Last_Name");
                int c = sdr.GetOrdinal("First_Name");
                int d = sdr.GetOrdinal("Mail_Address");
                int e = sdr.GetOrdinal("Membership_Expiry_Date");

                while (sdr.Read())
                {
                    Member member = new Member();
                    member.Id = sdr.GetInt32(a);
                    member.LastName = sdr.GetString(b);
                    member.FirstName = sdr.GetString(c);
                    member.MailAddress = sdr.GetString(d);
                    member.MembershipExpiryDate = sdr.GetString(e);
                    members.Add(member);
                }
            }

            return members;
        }

        public static long GetNextId()
        {
            ConnectionFactory cf = new ConnectionFactory();
            string statement =
                "SELECT MAX(Id) " +
                "FROM MEMBERS";
            
            using (cf.Connection)
            {
                SQLiteCommand getMaxIdCommand = new SQLiteCommand(statement, cf.Connection);
                cf.Connection.Open();
                return Convert.ToInt64(getMaxIdCommand.ExecuteScalar()) + 1;
            }
        }

        public static void SaveMember(Member member, bool isUpdate)
        {
            ConnectionFactory cf = new ConnectionFactory();
            string statement;

            if (!isUpdate)
            {
                statement =
                    "INSERT INTO MEMBERS " +
                    "VALUES(@Id, @LastName, @FirstName, @MailAddress, @MembershipExpiryDate)";
            }
            else
            {
                statement =
                    "UPDATE MEMBERS " +
                    "SET Last_Name = @LastName, First_Name = @FirstName, Mail_Address = @MailAddress, Membership_Expiry_Date = @MemberShipExpiryDate " +
                    "WHERE Id = @Id";
            }
            using (cf.Connection)
            {
                SQLiteCommand insertCommand = new SQLiteCommand(statement, cf.Connection);
                cf.Connection.Open();
                insertCommand.Parameters.AddWithValue("@Id", member.Id);
                insertCommand.Parameters.AddWithValue("@LastName", member.LastName);
                insertCommand.Parameters.AddWithValue("@FirstName", member.FirstName);
                insertCommand.Parameters.AddWithValue("@MailAddress", member.MailAddress);
                insertCommand.Parameters.AddWithValue("@MembershipExpiryDate", member.MembershipExpiryDate);
                insertCommand.ExecuteNonQuery();
            }
        }

        public static void RemoveMember(Member member)
        {
            ConnectionFactory cf = new ConnectionFactory();
            string statement =
                "DELETE FROM MEMBERS " +
                "WHERE Id = @Id";

            using (cf.Connection)
            {
                SQLiteCommand deleteCommand = new SQLiteCommand(statement, cf.Connection);
                deleteCommand.Parameters.AddWithValue("@Id", member.Id);
                cf.Connection.Open();
                deleteCommand.ExecuteNonQuery();
            }
        }
    }
}
