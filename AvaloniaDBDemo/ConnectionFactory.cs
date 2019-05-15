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
                scsb.DataSource = @"..\..\..\AvaloniaDBDemo.db";
            }
            else
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    scsb.DataSource = "AvaloniaDBDemo.db";
                }
                else
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        scsb.DataSource = "AvaloniaDBDemo.db";
                    }
                }
            }
            Connection = new SQLiteConnection(scsb.ConnectionString);
        }

    }
}
