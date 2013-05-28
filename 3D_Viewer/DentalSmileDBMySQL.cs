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
   public class DentalSmileDBMySQL : DentalSmileDB
    {
        
        //Constructor
        public DentalSmileDBMySQL()
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
                connectionString = "SERVER=" + server + ";" + "Port=" + port + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connectionString += "Convert Zero Datetime=True;";
            return connectionString;
        }
        protected override void Initialize2(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
            patientConnection = new MySqlConnection(connectionString);
            dentistConnection = new MySqlConnection(connectionString);
            phaseConnection = new MySqlConnection(connectionString);
            fileConnection = new MySqlConnection(connectionString);
        }

        protected override IDbCommand getSqlCommand(string query, IDbConnection conn)
        {
            return new MySqlCommand(query, (MySqlConnection)conn);
        }

       protected override int getLastInsertId(IDbCommand cmd, string query, IDbConnection conn)
        {
            return (int)((MySqlCommand)cmd).LastInsertedId;
        }

    }
}
