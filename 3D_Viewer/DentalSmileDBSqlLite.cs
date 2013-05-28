using smileUp.Calendar;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

//Add MySql Library
using MySql.Data.MySqlClient;
using smileUp.DataModel;
using System.Data;
using System.Security.Cryptography;
using System.Reflection;
using System;

namespace smileUp
{
   public class DentalSmileDBSqlLite : DentalSmileDB
    {
        //Constructor
        public DentalSmileDBSqlLite()
        {
            Initialize();
        }

        protected override string ConnectinString()
        {
            server = Smile.DbHost;
            port = Smile.DbPort;
            database = Smile.DbDatabase;
            uid = Smile.DbUserId;
            password = Smile.DbPassword;

            string connectionString;
            //connectionString = "SERVER=" + server + ";" + "Port=" + port + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            //connectionString += "Convert Zero Datetime=True;";
            connectionString = "Data Source=DentalSmile.db;Version=3;New=True;Compress=True;";
            return connectionString;
        }
        protected override void Initialize2(string connectionString)
        {
            connection = new SQLiteConnection(connectionString);
            patientConnection = new SQLiteConnection(connectionString);
            dentistConnection = new SQLiteConnection(connectionString);
            phaseConnection = new SQLiteConnection(connectionString);
            fileConnection = new SQLiteConnection(connectionString);
        }

       protected override IDbCommand getSqlCommand(string query, IDbConnection conn)
        {
            return new SQLiteCommand(query, (SQLiteConnection) conn);
        }

        protected override int getLastInsertId(IDbCommand cmd, string query, IDbConnection conn)
        {
            cmd = new SQLiteCommand("select last_insert_rowid() ", (SQLiteConnection) conn);
            SQLiteDataReader r = ((SQLiteCommand)cmd).ExecuteReader();
            return r.GetInt32(0);
        }
    }
}
