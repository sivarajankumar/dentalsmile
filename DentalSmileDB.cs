using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
//Add MySql Library
using MySql.Data.MySqlClient;

namespace smileUp
{
    class DentalSmileDB
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DentalSmileDB()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "dentalsmile";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connectionString += "Convert Zero Datetime=True;";

            connection = new MySqlConnection(connectionString);
        }


        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void InsertPatient(Patient p)
        {
            string tableName = "PATIENT";
            string columns = "(id, fname, lname, birthdate, birthplace, gender,address1,address2,city,phone, created,createdBy)";
            string values = "(" + p.Id + ",'" + p.FirstName + "','" + p.LastName + "'," + p.BirthDate.ToString("yyyy-MM-dd") + ",'" + p.BirthPlace + "','" + p.Gender + "','" + p.Address1 + "','" + p.Address2 + "','" + p.City + "','" + p.Phone + "," + DateTime.Now + ",'USER')";
            string query = "INSERT INTO "+tableName + " "+ columns +" values "+ values +" ;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        public void UpdatePatient(Patient p)
        {
            string tableName = "PATIENT";
            string setColumns = "fname = '" + p.FirstName + "', lname= '" + p.LastName + "', birthdate = '" + p.BirthDate.ToString("yyyy-MM-dd") + "', birthplace= '" + p.BirthPlace + "', gender= '" + p.Gender + "',address1= '" + p.Address1 + "',address2= '" + p.Address2 + "',city= '" + p.City + "',phone= '" + p.Phone + "', modified = '" + DateTime.Now + "', modifiedBy= 'USER')";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = "+p.Id;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public bool InsertDentist (Dentist p)
        {
            string tableName = "DENTIST";
            string columns = "(userid, fname, lname, birthdate, birthplace, gender,address1,address2,city,phone, created,createdBy)";
            string values = "('" + p.UserId + "','" + p.FirstName + "','" + p.LastName + "','" + p.BirthDate.ToString("yyyy-MM-dd") + "','" + p.BirthPlace + "','" + p.Gender + "','" + p.Address1 + "','" + p.Address2 + "','" + p.City + "','" + p.Phone + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:i:s") + "','USER')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }
            
            return false;
        }

        public void UpdateDentist (Dentist p)
        {
            string tableName = "DENTIST";
            string setColumns = "fname = '" + p.FirstName + "', lname= '" + p.LastName + "', birthdate = '" + p.BirthDate.ToString("yyyy-MM-dd") + "', birthplace= '" + p.BirthPlace + "', gender= '" + p.Gender + "',address1= '" + p.Address1 + "',address2= '" + p.Address2 + "',city= '" + p.City + "',phone= '" + p.Phone + "', modified = '" + DateTime.Now.ToLongDateString() + "', modifiedBy= 'USER')";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE userid = " + p.UserId;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        
        public void SetPassword(string p, string userid)
        {
            string tableName = "DENTIST";
            string setColumns = "password = '" + p + "', modified = '" + DateTime.Now + "', modifiedBy= 'USER')";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE userid = " + userid;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }


        public void InsertTreatment(Treatment t)
        {
            string tableName = "TREATMENT";
            string columns = "(id, phase, dentist, patient, tdate, ttime,room, created,createdBy)";
            string values = "(" + t.Id + ",'" + t.Phase.Id + "','" + t.Dentist.UserId + "'," + t.Patient.Id + ",'" + t.TreatmentDate + "','" + t.TreatmentTime + "','" + t.Room + "'," + DateTime.Now + ",'USER')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        public void UpdateTreatment(Treatment t)
        {
            string tableName = "TREATMENT";
            string setColumns = "phase ='" + t.Phase.Id + "', dentist='" + t.Dentist.UserId + "', patient='" + t.Patient.Id + "', tdate='" + t.TreatmentDate + "', ttime='" + t.TreatmentTime + "',room='" + t.Room + "', modified='" + DateTime.Now + "', modifiedBy='USER')";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = " + t.Id;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void InsertFileInfo(SmileFile t)
        {
            string tableName = "PFILE";
            string columns = "(id, filename, description, patient, screenshot, type, created,createdBy)";
            string values = "(" + t.Id + ",'" + t.FileName + "','" + t.Description + "'," + t.Patient.Id + ",'" + t.Screenshot + "','" + t.Type + "'," + DateTime.Now + ",'USER')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void UpdateFileInfo(SmileFile t)
        {
            string tableName = "PFILE";
            string setColumns = "filename='" + t.FileName + "', description='" + t.Description + "', patient='" + t.Patient.Id + "', screenshot='" + t.Screenshot + "', type='" + t.Type + "', modified='" + DateTime.Now + "', modifiedBy='USER')";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = " + t.Id;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public List<Dentist> findDentistsByOr(string userid, string firstname, string lastname)
        {
            string query = "SELECT * FROM DENTIST ";
            bool any = false;
            string where = "";
            if (userid != null)
            {
                where += " userid like '%" + userid + "%' ";
                any = true;
            }
            if (firstname != null)
            {
                if (any) where += " or ";
                where += " fname like '%" + firstname + "%' ";
                any = true;
            }
            if (lastname != null)
            {
                if (any) where += " or ";
                where += " or lname like '%" + lastname + "%' ";
                any = true;
            }
            if (any) query += " WHERE " + where;

            List<Dentist> list = null;
            if (this.OpenConnection() == true)
            {
                list = new List<Dentist>();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Dentist p = toDentist(dataReader);
                    list.Add(p);
                }
                dataReader.Close();

                this.CloseConnection();

            }
            return list;
        }

        public List<Dentist> findDentistsByAnd(string userid, string firstname, string lastname)
        {
            string query = "SELECT * FROM DENTIST ";
            bool any = false;
            string where = "";
            if (userid != null)
            {
                where += " userid like '%" + userid + "%' ";
                any = true;
            }
            if (firstname != null)
            {
                if (any) where += " and ";
                where += " fname like '%" + firstname + "%' ";
                any = true;
            }
            if (lastname != null)
            {
                if (any) where += " and ";
                where += " or lname like '%" + lastname + "%' ";
                any = true;
            }
            if (any) query += " WHERE " + where;

            List<Dentist> list = null;
            if (this.OpenConnection() == true)
            {
                list = new List<Dentist>();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Dentist p = toDentist(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();

            }
            return list;
        }

        public List<Dentist> SelectAllDentists()
        {
            string query = "SELECT * FROM DENTIST";
            List<Dentist> list = null;
            if (this.OpenConnection() == true)
            {
                list = new List<Dentist>();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Dentist p = toDentist(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return list;
        }

        private Dentist toDentist(MySqlDataReader dataReader)
        {
            Dentist p = new Dentist();

            p.UserId = dataReader.GetString("userid");
            p.FirstName = dataReader.GetString("fname");
            p.LastName = dataReader.GetString("lname");
            p.BirthDate = dataReader.GetDateTime("birthdate");
            p.BirthPlace = dataReader.GetString("birthplace");
            p.Gender = dataReader.GetString("gender");
            p.Address1 = dataReader.GetString("address1");
            p.Address2 = dataReader.GetString("address2");
            p.City = dataReader.GetString("city");
            p.Phone = dataReader.GetString("phone");

            return p;
        }

        private Patient toPatient(MySqlDataReader dataReader)
        {
            Patient p = new Patient();
            
            p.Id = dataReader.GetString("id");
            p.FirstName = dataReader.GetString("fname");
            p.LastName = dataReader.GetString("lname");
            p.BirthDate = dataReader.GetDateTime("birtdate");
            p.BirthPlace = dataReader.GetString("birthplace");
            p.Gender = dataReader.GetString("gender");
            p.Address1 = dataReader.GetString("address1");
            p.Address2 = dataReader.GetString("address2");
            p.City = dataReader.GetString("city");
            p.Phone = dataReader.GetString("phone");
            
            return p;
        }

        private SmileFile toSmileFile(MySqlDataReader dataReader)
        {
            SmileFile p = new SmileFile();
            
            p.Id = dataReader.GetString("id");
            p.FileName = dataReader.GetString("filename");
            p.Description = dataReader.GetString("description");
            p.Screenshot = dataReader.GetString("screenshot");
            
            p.Patient = findPatientById(dataReader.GetInt32("patient"));

            return p;
        }

        private Patient findPatientById(int id)
        {
            string query = "SELECT * FROM PATIENT WHERE id = @id";
            Patient p = null;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add(new MySqlParameter("id", id));

                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toPatient(dataReader);
                }
                dataReader.Close();
                this.CloseConnection();

            }
            return p;
        }

        public List<Patient> SelectAllPatient()
        {
            string query = "SELECT * FROM PATIENT";
            List<Patient> list = null;
            if (this.OpenConnection() == true)
            {
                list = new List<Patient>();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Patient p = toPatient(dataReader);
                    list.Add(p);
                }
                dataReader.Close();

                this.CloseConnection();

            }
            return list;
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar()+"");
                
                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);

                
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                
                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error , unable to Restore!");
            }
        }

        //internal void InsertMeasurement(string measurement, string patient, string fileid, string measurementstatus, string measurementlastupdate, char measurementby)
        //{
        //    throw new NotImplementedException();
        //}

        //Insert  teethFile
        public void InsertTeethFiles(int patient_id, string file_name, string file_desc)
        {
            var A = DateTime.Today;
            var created_date = DateTime.Today;
            var modified_date = DateTime.Today;
            string created_by = "Asri";
            string modified_by = "Asri";

            string query = "INSERT INTO teeth_files (patient_id, file_name, file_desc, created_date, created_by, modified_date,modified_by) VALUES('" + patient_id + "', '" + file_name + "', '" + file_desc + "', '" + created_date + "', '" + created_by + "', '" + modified_date + "', '" + modified_by + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public void InsertPatientTreatment(int patient_id, int doctor_id, int phase)
        {
           

            string query = "INSERT INTO patient_treatment (patient_id, doctor_id, phase) VALUES('" + patient_id + "', '" + doctor_id + "', '" + phase + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

    }
}