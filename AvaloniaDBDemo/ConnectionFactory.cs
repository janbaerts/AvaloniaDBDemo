using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Text;

namespace AvaloniaDBDemo
{
    public class ConnectionFactory
    {
        public SQLiteConnection Connection { get; }

        public ConnectionFactory()
        {
            SQLiteConnectionStringBuilder scsb = new SQLiteConnectionStringBuilder();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                scsb.DataSource = @"D:\SQLite\AvaloniaDBDemo.db";
            }
            else
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    scsb.DataSource = "/home/student/AvaloniaDBDemo.db";
                }
                else
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        scsb.DataSource = "/Users/galahad/Desktop/AvaloniaDBDemo.db";
                    }
                }
            }
            Connection = new SQLiteConnection(scsb.ConnectionString);
        }

    }
}
