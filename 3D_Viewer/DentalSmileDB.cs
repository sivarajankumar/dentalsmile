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
using System.Data.Common;

namespace smileUp
{
   public abstract class DentalSmileDB
    {
       protected IDbConnection connection;
       protected IDbConnection patientConnection;
       protected IDbConnection dentistConnection;
       protected IDbConnection phaseConnection;
       protected IDbConnection fileConnection;

        protected string server;
        protected string database;
        protected string uid;
        protected string password;
        protected string port;
        
        public string User { 
            get { return (App.user == null? "USER" : App.user.UserId); }  
        }

        //Constructor
        public DentalSmileDB()
        {
            Initialize();
        }

        //Initialize values
        protected void Initialize()
        {
            //server = "localhost";
            //database = "dentalsmile";
            //uid = "root";
            //password = "";
            server = Smile.DbHost;
            port = Smile.DbPort;
            database = Smile.DbDatabase;
            uid = Smile.DbUserId;
            password = Smile.DbPassword;

            string connectionString = ConnectinString();
            Initialize2(connectionString);
        }

        protected abstract string ConnectinString();

        protected abstract void Initialize2(string connectionString);

        public bool TestConnectionString()
        {
            try
            {
                if (this.OpenConnection() == true)
                {                   
                    this.CloseConnection();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (DbException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.ErrorCode)
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
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool OpenPatientConnection()
        {
            try
            {
                patientConnection.Open();
                return true;
            }
            catch (DbException ex)
            {
                switch (ex.ErrorCode)
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
        private bool ClosePatientConnection()
        {
            try
            {
                patientConnection.Close();
                return true;
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool OpenDentistConnection()
        {
            try
            {
                dentistConnection.Open();
                return true;
            }
            catch (DbException ex)
            {
                switch (ex.ErrorCode)
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
        private bool CloseDentistConnection()
        {
            try
            {
                dentistConnection.Close();
                return true;
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool OpenPhaseConnection()
        {
            try
            {
                phaseConnection.Open();
                return true;
            }
            catch (DbException ex)
            {
                switch (ex.ErrorCode)
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
        private bool ClosePhaseConnection()
        {
            try
            {
                phaseConnection.Close();
                return true;
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool OpenFileConnection()
        {
            try
            {
                fileConnection.Open();
                return true;
            }
            catch (DbException ex)
            {
                switch (ex.ErrorCode)
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
        private bool CloseFileConnection()
        {
            try
            {
                fileConnection.Close();
                return true;
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        protected abstract IDbCommand getSqlCommand(string query, IDbConnection patientConnection);
        
       public void InsertPatient(Patient p)
        {
            string tableName = "patient";
            string columns = "(id, fname, lname, birthdate, birthplace, gender, address1,address2,city,phone,created, createdby)";
            string values = "('" + p.Id + "','" + p.FirstName + "','" + p.LastName + "','" + p.BirthDate.ToString(Smile.DATE_FORMAT) + "','" + p.BirthPlace + "','" + p.Gender + "','" + p.Address1 + "','" + p.Address2 + "','" + p.City + "','" + p.Phone + "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','"+User+"')";
            string query = "INSERT INTO "+tableName + " "+ columns +" values "+ values +" ;";

            if (this.OpenPatientConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, patientConnection);
                cmd.ExecuteNonQuery();
                this.ClosePatientConnection();
            }
        }

     
        public void UpdatePatient(Patient p)
        {
            string tableName = "patient";
            string setColumns = "fname = '" + p.FirstName + "', lname= '" + p.LastName + "', birthdate = '" + p.BirthDate.ToString(Smile.DATE_FORMAT) + "', birthplace= '" + p.BirthPlace + "', gender= '" + p.Gender + "',address1= '" + p.Address1 + "',address2= '" + p.Address2 + "',city= '" + p.City + "',phone= '" + p.Phone + "', modified = '" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy= '"+User+"' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = '"+p.Id+"'";

            if (this.OpenPatientConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, patientConnection);
                cmd.ExecuteNonQuery();
                this.ClosePatientConnection();
            }
        }

        public bool InsertDentist (Dentist p)
        {
            string tableName = "dentist";
            string columns = "(userid, fname, lname, birthdate, birthplace, gender,address1,address2,city,phone, created,createdBy)";
            string values = "('" + p.UserId + "','" + p.FirstName + "','" + p.LastName + "','" + p.BirthDate.ToString(Smile.DATE_FORMAT) + "','" + p.BirthPlace + "','" + p.Gender + "','" + p.Address1 + "','" + p.Address2 + "','" + p.City + "','" + p.Phone + "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','"+User+"')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenDentistConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, dentistConnection);
                cmd.ExecuteNonQuery();
                this.CloseDentistConnection();
                return true;
            }
            
            return false;
        }

        public void UpdateDentist (Dentist p)
        {
            string tableName = "dentist";
            string setColumns = "fname = '" + p.FirstName + "', lname= '" + p.LastName + "', birthdate = '" + p.BirthDate.ToString(Smile.DATE_FORMAT) + "', birthplace= '" + p.BirthPlace + "', gender= '" + p.Gender + "',address1= '" + p.Address1 + "',address2= '" + p.Address2 + "',city= '" + p.City + "',phone= '" + p.Phone + "', modified = '" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy= '"+User+"' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE userid = '" + p.UserId+"'";
            
            if (this.OpenDentistConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, dentistConnection);
                cmd.ExecuteNonQuery();
                this.CloseDentistConnection();
            }
        }

        public bool InsertUser(SmileUser p)
        {
            string tableName = "smileuser";
            string columns = "(userid, password, created,createdBy)";
            string values = "('" + p.UserId + "','" + p.Password+ "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','"+User+"')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }

            return false;
        }

        public void SetPassword(string md5generated, string userid)
        {
            string tableName = "smileuser";
            string setColumns = "password = '" + md5generated + "', modified = '" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy= '"+User+"' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE userid = '" + userid+"'";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        
        public void SetAdmin(bool p, string userid)
        {
            string tableName = "smileuser";
            string setColumns = "admin= '" + p + "', modified = '" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy= '"+User+"' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE userid = '" + userid + "'";


            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }


        public bool InsertTreatment(Treatment t)
        {
            if (t.Id == null)
                t.Id = getTreatmentNewId(t.Patient.Id);
            string tableName = "treatment";
            string columns = "(id, phase, dentist, patient, tdate, ttime, room, refid, created,createdBy)";
            string values = "('" + t.Id + "'," + t.Phase.Id + ",'" + t.Dentist.UserId + "','" + t.Patient.Id + "','" + t.TreatmentDate.ToString(Smile.DATE_FORMAT) + "','" + t.TreatmentTime + "','" + t.Room + "','"  + t.RefId+ "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','" + User + "')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }
            return false;
        }

        public void UpdateTreatment(Treatment t)
        {
            string tableName = "treatment";
            string setColumns = "phase =" + t.Phase.Id + ", dentist='" + t.Dentist.UserId + "', patient='" + t.Patient.Id + "', tdate='" + t.TreatmentDate.ToString(Smile.DATE_FORMAT) + "', ttime='" + t.TreatmentTime + "',room='" + t.Room + "',refid='" + t.RefId+ "', modified='" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy='" + User + "' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = '" + t.Id + "'";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public bool InsertFileInfo(SmileFile t)
        {
            string tableName = "pfile";
            string columns = "(id, filename, description, patient, screenshot, type, refid, created,createdBy)";
            string values = "('" + t.Id + "','" + t.FileName + "','" + t.Description + "','" + t.Patient.Id + "','" + t.Screenshot + "'," + t.Type + ",'" + t.RefId+ "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','" + User + "')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                
                return true;
            }
            return false;
        }

        public void UpdateFileInfo(SmileFile t)
        {
            string tableName = "pfile";
            string setColumns = "filename='" + t.FileName + "', description='" + t.Description + "', patient='" + t.Patient.Id + "', screenshot='" + t.Screenshot + "', type=" + t.Type + ", refid='" + t.RefId+ "', modified='" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy='" + User + "' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = '" + t.Id + "'";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public List<Dentist> findDentistsByOr(string userid, string firstname, string lastname)
        {
            string query = "SELECT d.*, (SELECT COUNT(s.userid) isuser FROM smileuser s WHERE s.userid = d.userid ) isuser FROM dentist d";
            bool any = false;
            string where = "";
            if (userid != null && !userid.Equals(string.Empty))
            {
                where += " userid like '%" + userid + "%' ";
                any = true;
            }
            if (firstname != null && !firstname.Equals(string.Empty))
            {
                if (any) where += " or ";
                where += " fname like '%" + firstname + "%' ";
                any = true;
            }
            if (lastname != null && !lastname.Equals(string.Empty))
            {
                if (any) where += " or ";
                where += " lname like '%" + lastname + "%' ";
                any = true;
            }
            if (any) query += " WHERE " + where;

            List<Dentist> list = null;
            if (this.OpenDentistConnection() == true)
            {
                list = new List<Dentist>();
                IDbCommand cmd = getSqlCommand(query, dentistConnection);
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Dentist p = toDentist(dataReader);
                    list.Add(p);
                }
                dataReader.Close();

                this.CloseDentistConnection();

            }
            return list;
        }

        public List<Dentist> findDentistsByAnd(string userid, string firstname, string lastname)
        {
            string query = "SELECT d.*, (SELECT COUNT(s.userid) isuser FROM smileuser s WHERE s.userid = d.userid ) isuser isuser FROM dentist d";
            bool any = false;
            string where = "";
            if (userid != null && !userid.Equals(string.Empty))
            {
                where += " userid like '%" + userid + "%' ";
                any = true;
            }
            if (firstname != null && !firstname.Equals(string.Empty))
            {
                if (any) where += " and ";
                where += " fname like '%" + firstname + "%' ";
                any = true;
            }
            if (lastname != null && !lastname.Equals(string.Empty))
            {
                if (any) where += " and ";
                where += " lname like '%" + lastname + "%' ";
                any = true;
            }
            if (any) query += " WHERE " + where;

            List<Dentist> list = null;
            if (this.OpenDentistConnection() == true)
            {
                list = new List<Dentist>();
                IDbCommand cmd = getSqlCommand(query, dentistConnection);
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Dentist p = toDentist(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseDentistConnection();

            }
            return list;
        }

        public List<Dentist> SelectAllDentists()
        {
            string query = "SELECT d.*, (SELECT COUNT(s.userid) isuser FROM smileuser s WHERE s.userid = d.userid ) isuser FROM dentist d";
            List<Dentist> list = null;
            if (this.OpenDentistConnection() == true)
            {
                list = new List<Dentist>();
                IDbCommand cmd = getSqlCommand(query, dentistConnection);
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Dentist p = toDentist(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseDentistConnection();
            }
            return list;
        }

        private Dentist toDentist(IDataReader dataReader)
        {
            Dentist p = new Dentist();

            p.UserId = GetStringSafe(dataReader, "userid");
            p.FirstName = GetStringSafe(dataReader, "fname");
            p.LastName = GetStringSafe(dataReader, "lname");
            if (!dataReader.IsDBNull(dataReader.GetOrdinal("birthdate")))
            {
                p.BirthDate = dataReader.GetDateTime(dataReader.GetOrdinal("birthdate"));
            }
            p.BirthPlace = GetStringSafe(dataReader, "birthplace");
            p.Gender = GetStringSafe(dataReader, "gender");
            p.Address1 = GetStringSafe(dataReader, "address1");
            p.Address2 = GetStringSafe(dataReader, "address2");
            p.City = GetStringSafe(dataReader, "city");
            p.Phone = GetStringSafe(dataReader, "phone");
            p.IsUser = (dataReader.IsDBNull(dataReader.GetOrdinal("isuser")) ? false: dataReader.GetBoolean(dataReader.GetOrdinal("isuser")));
            return p;
        }

        private Patient toPatient(IDataReader dataReader)
        {
            Patient p = new Patient();

            p.Id = GetStringSafe(dataReader, "id");
            p.FirstName = GetStringSafe(dataReader, "fname");
            p.LastName = GetStringSafe(dataReader, "lname");
            //p.BirthDate = dataReader.GetDateTime(dataReader.GetOrdinal("birthdate"));
            if (!dataReader.IsDBNull(dataReader.GetOrdinal("birthdate")))
            {
                p.BirthDate = dataReader.GetDateTime(dataReader.GetOrdinal("birthdate"));
            }
            p.BirthPlace = GetStringSafe(dataReader, "birthplace");
            p.Gender = GetStringSafe(dataReader, "gender");
            p.Address1 = GetStringSafe(dataReader, "address1");
            p.Address2 = GetStringSafe(dataReader, "address2");
            p.City = GetStringSafe(dataReader, "city");
            p.Phone = GetStringSafe(dataReader, "phone");
            
            return p;
        }

        private SmileFile toSmileFile(IDataReader dataReader, bool nested)
        {
            SmileFile p = new SmileFile();
            
            p.Id = GetStringSafe(dataReader, "id");
            p.FileName = GetStringSafe(dataReader, "filename");
            p.Description = GetStringSafe(dataReader, "description");
            p.Screenshot = GetStringSafe(dataReader, "screenshot");
            p.Type = dataReader.GetInt32(dataReader.GetOrdinal("type"));
            p.RefId = GetStringSafe(dataReader, "refid");
            p.Phase = findPhaseById(p.Type);

            if(nested) p.Patient = findPatientById(GetStringSafe(dataReader, "patient"));

            return p;
        }

        private Phase toPhase(IDataReader dataReader)
        {
            Phase p = new Phase();
            
            p.Id = dataReader.GetInt32(dataReader.GetOrdinal("id"));
            p.Name = GetStringSafe(dataReader, "name");

            return p;
        }


        private Appointment toAppointment(IDataReader dataReader)
        {
            Appointment p = new Appointment();
            
            p.Id = dataReader.GetInt32(dataReader.GetOrdinal("id"));
            p.Subject = GetStringSafe(dataReader, "subject");
            p.Room = GetStringSafe(dataReader, "room");
            p.Notes = GetStringSafe(dataReader, "notes");
            p.Aptime = GetStringSafe(dataReader, "appointment_time");
            p.ApDate = dataReader.GetDateTime(dataReader.GetOrdinal("appointment_date")); ;
            p.Patient = findPatientById(GetStringSafe(dataReader,"patient"));
            p.Dentist = findDentistByUserId(GetStringSafe(dataReader, "dentist"));

            return p;
        }

        private Treatment toTreatment(IDataReader dataReader)
        {
            Treatment p = new Treatment();

            p.Id = GetStringSafe(dataReader, "id");
            p.Room = GetStringSafe(dataReader, "room");
            p.TreatmentDate = dataReader.GetDateTime(dataReader.GetOrdinal("tdate"));
            p.TreatmentTime = GetStringSafe(dataReader, "ttime");
            p.RefId = GetStringSafe(dataReader, "refid");

            p.Patient = findPatientById(GetStringSafe(dataReader, "patient"));
            p.Phase = findPhaseById(dataReader.GetInt32(dataReader.GetOrdinal("phase")));
            //p.Phase = Smile.GetPhase(dataReader.GetInt32("phase"));
            p.Dentist = findDentistByUserId(GetStringSafe(dataReader, "dentist"));

            p.Files = findSmileFilesByTreatmentId(p.Id);
            return p;
        }


        private SmileUser toSmileUser(IDataReader dataReader)
        {
            SmileUser p = new SmileUser();

            p.UserId = dataReader.GetString(dataReader.GetOrdinal("userid"));
            p.Dentist = findDentistByUserId(dataReader.GetString(dataReader.GetOrdinal("userid")));

            return p;
        }

        private Measurement toMeasurement(IDataReader dataReader)
        {
            Measurement p = new Measurement();

            p.Id = Convert.ToInt32(GetStringSafe(dataReader, "id"));
            p.Patient = GetStringSafe(dataReader, "patient");
            p.Pfile = GetStringSafe(dataReader, "pfile");
            p.Treatment = GetStringSafe(dataReader, "treatment");
            p.Type = GetStringSafe(dataReader, "type");

            return p;
        }

        private MeasurementTeeth toTeethMeasurement(IDataReader dataReader)
        {
            MeasurementTeeth p = new MeasurementTeeth();

            p.Identity = GetStringSafe(dataReader, "teethid");
            p.Length = Convert.ToDouble(GetStringSafe(dataReader, "length"));
            p.SPoint = GetStringSafe(dataReader, "Spoint");
            p.EPoint = GetStringSafe(dataReader, "EPoint");
            p.Type = GetStringSafe(dataReader, "type");
            if (GetStringSafe(dataReader, "modified").ToString() == "")
            { p.ModifiedDate = GetStringSafe(dataReader, "created".ToString()).ToString(); }
            else
            { p.ModifiedDate = GetStringSafe(dataReader, "modified").ToString(); }
            p.Loaded = true;


            return p;
        }



        private Phase findPhaseById(int id)
        {
            string query = "SELECT * FROM phase WHERE id= @id";
            Phase p = null;
            if (this.OpenPhaseConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, phaseConnection);
                cmd.Parameters.Add(new MySqlParameter("id", id));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toPhase(dataReader);
                }
                dataReader.Close();
                this.ClosePhaseConnection();

            }
            return p;
        }

        private Dentist findDentistByUserId(string id)
        {
            string query = "SELECT d.*, (SELECT COUNT(s.userid) isuser FROM smileuser s WHERE s.userid = d.userid ) isuser FROM dentist d WHERE d.userid = '"+id+"'";
            Dentist p = null;
            if (this.OpenDentistConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, dentistConnection);
                //cmd.Parameters.Add(new MySqlParameter("userid", id));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toDentist(dataReader);
                }
                dataReader.Close();
                this.CloseDentistConnection();

            }
            return p;
        }

        public bool login(string id, string password, ref SmileUser p)
        {
            string query = "SELECT * FROM smileuser WHERE LCASE(userid) = LCASE('"+id+"')";
            string dbpassword  = null;
            bool isAdmin = false;
            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("userid", id));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    dbpassword = GetStringSafe(dataReader, "password");
                    isAdmin = dataReader.GetBoolean(dataReader.GetOrdinal("admin"));
                }
                dataReader.Close();
                this.CloseConnection();
            }

            if (dbpassword != null)
            {
                //crosscheck the password using MD5
                if (CalculateMD5Hash(password).Equals(dbpassword))
                {
                    p = new SmileUser();
                    p.UserId = id;
                    p.Dentist = findDentistByUserId(p.UserId);
                    p.Admin = isAdmin;
                    return true;
                }
            }

            return false ;
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        private SmileUser findUserByUserId(string id)
        {
            string query = "SELECT * FROM smileuser WHERE userid = '"+id+"'";
            SmileUser p = null;
            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("userid", id));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toSmileUser(dataReader);
                }
                dataReader.Close();
                this.CloseConnection();

            }
            return p;
        }
        
        private List<SmileFile> findSmileFilesByTreatmentId(string treatmentId)
        {
            string query = "SELECT f.* FROM treatment_pfile t, pfile f WHERE t.treatment = '"+treatmentId+"' AND t.pfile = f.Id";
            List<SmileFile> list = null;
            if (this.OpenFileConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, fileConnection);
                //cmd.Parameters.Add(new MySqlParameter("id", treatmentId));
                list = new List<SmileFile>();
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    SmileFile p = toSmileFile(dataReader, false);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseFileConnection();

            }
            return list;
        }

        private Patient findPatientById(string id)
        {
            string query = "SELECT * FROM patient WHERE id = '"+id+"'";
            Patient p = null;
            if (this.OpenPatientConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, patientConnection);
                //cmd.Parameters.Add(new MySqlParameter("id", id));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toPatient(dataReader);
                }
                dataReader.Close();
                this.ClosePatientConnection();

            }
            return p;
        }

        public List<Patient> SelectAllPatient()
        {
            string query = "SELECT * FROM patient";
            List<Patient> list = null;
            if (this.OpenPatientConnection() == true)
            {
                list = new List<Patient>();

                IDbCommand cmd = getSqlCommand(query, patientConnection);
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Patient p = toPatient(dataReader);
                    list.Add(p);
                }
                dataReader.Close();

                this.ClosePatientConnection();

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
                IDbCommand cmd = getSqlCommand(query, connection);

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
        private void InsertTeethFiles(int patient_id, string file_name, string file_desc)
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
                IDbCommand cmd = getSqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        private void InsertPatientTreatment(int patient_id, int doctor_id, int phase)
        {
           
            string query = "INSERT INTO patient_treatment (patient_id, doctor_id, phase) VALUES('" + patient_id + "', '" + doctor_id + "', '" + phase + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                IDbCommand cmd = getSqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }


        internal List<Phase> SelectAllPhases()
        {
            string query = "SELECT * FROM phase";
            List<Phase> list = null;
            if (this.OpenPhaseConnection() == true)
            {
                list = new List<Phase>();
                IDbCommand cmd = getSqlCommand(query, phaseConnection);
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Phase p = toPhase(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.ClosePhaseConnection();
            }
            return list;
        }

        public bool insertTreatmentFiles(Treatment treatment, SmileFile file)
        {
            string tableName = "treatment_pfile";
            string columns = "(treatment, pfile)";
            string values = "('" + treatment.Id + "','" + file.Id+ "')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();

                return true;
            }
            return false;
        }

        public string getSmileFileNewId(string patientid)
        {
            string query = "SELECT MAX(id) id FROM ( SELECT MAX(SUBSTR(id, 14)) id FROM pfile WHERE patient = '"+patientid+"' UNION SELECT 0 id FROM DUAL ) a";
            int p = 0;
            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("patient", patientid));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = dataReader.GetInt32(dataReader.GetOrdinal("id")); ;
                }
                dataReader.Close();
                this.CloseConnection();
            }
            
            p = p + 1; //newid
            string r = patientid +""+p.ToString("000");
            return r;
        }

        public string getTreatmentNewId(string patientid)
        {
            string query = "SELECT MAX(id) id FROM ( SELECT MAX(SUBSTR(id, 14))  id FROM treatment WHERE patient = '"+patientid+"' UNION SELECT 0 id FROM DUAL ) a ";
            int p = 0;
            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("patient", patientid));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = dataReader.GetInt32(dataReader.GetOrdinal("id")); ;
                }
                dataReader.Close();
                this.CloseConnection();
            }

            p = p + 1; //newid
            string r = patientid + "" + p.ToString("000");
            return r;
        }



        public List<Treatment> findTreatmentsByPatientId(string patientid)
        {
            string query = "SELECT * FROM treatment WHERE patient = '" + patientid + "' ORDER BY tdate DESC, ttime DESC";
            List<Treatment> list = null;
            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("id", patientid));

                IDataReader dataReader = cmd.ExecuteReader();
                list = new List<Treatment>(); 
                while (dataReader.Read())
                {
                    Treatment p = toTreatment(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return list;
        }

        public List<Treatment> findTreatments()
        {
            string query = "SELECT * FROM treatment ORDER BY tdate DESC, ttime DESC";
            List<Treatment> list = null;
            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);

                IDataReader dataReader = cmd.ExecuteReader();
                list = new List<Treatment>();
                while (dataReader.Read())
                {
                    Treatment p = toTreatment(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return list;
        }

        internal bool insertMeasurementTeeth(Measurement measurement, List<MeasurementTeeth> results)
        {
            if (this.OpenConnection() == true)
            {
                string tableName = "measurementteeth";
                string columns = "(measurementid, teethId, length, spoint, epoint,type)";
                string values = "";
                foreach (MeasurementTeeth m in results)
                {
                    values = "(" + measurement.Id + ",'" + m.Identity + "'," + m.Length + ",'" + m.SPoint + "','" + m.EPoint + "','" + m.Type+ "')";
                    string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

                    IDbCommand cmd = getSqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    m.Id = getLastInsertId(cmd, query, connection);
                    //m.Id = (int)cmd.LastInsertedId;
                }
                
                this.CloseConnection();
                return true;
            }
            return false;
        }

        protected abstract int getLastInsertId(IDbCommand cmd, string query, IDbConnection connection);

        internal bool insertMeasurement(Measurement measurement)
        {
            if (this.OpenConnection() == true)
            {
                string tableName = "measurement";
                string columns = "(patient, treatment, pfile, type, created, createdBy)";
                string values = "";
                values = "('" + measurement.Patient + "','" + measurement.Treatment + "','" + measurement.Pfile + "'," + measurement.Type + ",'" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','"+User+"')";
                string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                //measurement.Id = (int) cmd.LastInsertedId;
                measurement.Id = getLastInsertId(cmd, query, connection);
                
                this.CloseConnection();

                return true;
            }
            return false;
        }


        internal List<Treatment> findTreatmentsByOr(string id, string tdate, string patient, string dentist)
        {
            string query = "SELECT * FROM treatment ";
            bool any = false;
            string where = "";
            if (id != null)
            {
                where += " id like '%" + id + "%' ";
                any = true;
            }
            if (tdate != null)
            {
                if (any) where += " or";
                where += " tdate = '" + tdate+ "' ";
                any = true;
            }
            if (patient!= null)
            {
                if (any) where += " or ";
                where += " patient like '%" + patient+ "%' ";
                any = true;
            }
            
            if (dentist!= null)
            {
                if (any) where += " or ";
                where += " dentist like '%" + dentist + "%' ";
                any = true;
            }

            if (any) query += " WHERE " + where;

            List<Treatment> list = null;
            if (this.OpenConnection() == true)
            {
                list = new List<Treatment>();
                IDbCommand cmd = getSqlCommand(query, connection);
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Treatment p = toTreatment(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();

            }
            return list;
        }

        internal bool insertTreatmentNotes(Treatment treatment, string resume, SmileFile file, string description)
        {
            if (this.OpenConnection() == true)
            {
                string tableName = "treatment_notes";
                string columns = "(treatment, notes, pfile, description, created, createdBy)";
                string values = "";
                values = "('" + treatment.Id + "','" + resume + "','" + (file == null ? null : file.Id) + "','" + description + "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','" + User + "')";
                string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                this.CloseConnection();

                return true;
            }
            return false;
        }

        public static string GetStringSafe(IDataReader reader, int colIndex)
        {
            return GetStringSafe(reader, colIndex, string.Empty);
        }

        public static string GetStringSafe(IDataReader reader, int colIndex, string defaultValue)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return defaultValue;
        }

        public static string GetStringSafe(IDataReader reader, string indexName)
        {
            return GetStringSafe(reader, reader.GetOrdinal(indexName));
        }

        public static string GetStringSafe(IDataReader reader, string indexName, string defaultValue)
        {
            return GetStringSafe(reader, reader.GetOrdinal(indexName), defaultValue);
        }

        internal void updateAdmin(string p)
        {
            string tableName = "smileuser";
            string setColumns = "password='" + CalculateMD5Hash(p)+ "', modified='" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy='" + User + "' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE userid = 'root'";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        internal SmileUser selectDefaultAdmin()
        {
            string query = "SELECT * FROM smileuser WHERE userid= 'root'";
            SmileUser p = null;
            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("userid", "root"));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toSmileUser(dataReader);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return p;
        }

        internal bool InsertDefaultAdmin(string p)
        {
            string tableName = "smileuser";
            string columns = "(userid, password, admin, created,createdBy)";
            string values = "('root','" + CalculateMD5Hash(p)+ "',1,'" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','DENTALSMILE')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                Dentist d = new Dentist();
                d.UserId = "root";
                d.FirstName = "Root";
                d.BirthDate = DateTime.Now;

                InsertDentist(d);
                return true;
            }

            return false;
        }

        internal bool findPatientByNameAndBirthDate(string fname, string lname, string bdate)
        {
            string query = "SELECT * FROM patient ";
            bool any = false;
            string where = "";
            if (fname!= null)
            {
                where += " LCASE(fname) = '" + fname.ToLower() + "' ";
                any = true;
            }
            if (lname != null)
            {
                if (any) where += " and ";
                where += " LCASE(lname) = '" + lname.ToLower() + "' ";
                any = true;
            }
            if (bdate != null)
            {
                if (any) where += " and ";
                where += " birthdate = '" + bdate + "' ";
                any = true;
            }
            if (any) query += " WHERE " + where;

            int i = 0;
            if (this.OpenDentistConnection() == true)
            {                
                IDbCommand cmd = getSqlCommand(query, dentistConnection);
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    i++;
                }
                dataReader.Close();
                this.CloseDentistConnection();
            }
            return i > 0;
        }
        private int countPatientByCreated()
        {
            string query = "SELECT Count(*) FROM patient WHERE created = '"+ DateTime.Now.ToString(Smile.DATE_FORMAT) + "'";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                IDbCommand cmd = getSqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }
        internal string getPatientNewId()
        {
            string id = DateTime.Now.DayOfWeek.ToString().Substring(0,3);
            id += DateTime.Now.ToString(Smile.DATE_ID_FORMAT);
            int idInt = countPatientByCreated();
            idInt++;
            id += idInt.ToString("0000");
            return id;
        }

        internal bool DeleteUserOnly(string p)
        {
            string query = "delete smileuser WHERE userid = '"+p+"'";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("userid", p));
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }

            return false;
        }


        internal List<Appointment> findAppointmentsByPatient(string patient, int month)
        {
            string query = "SELECT * FROM APPOINTMENTS WHERE patient = '"+patient+"' and MONTH(appointment_date) = "+month+"";
            List<Appointment> list = null;
            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("id", patient));
                //cmd.Parameters.Add(new MySqlParameter("month", month));

                IDataReader dataReader = cmd.ExecuteReader();
                list = new List<Appointment>(); 
                while (dataReader.Read())
                {
                    Appointment p = toAppointment(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return list;        
        }


        internal bool insertAppointment(Appointment t)
        {
            string tableName = "Appointments";
            string columns = "(subject, patient, dentist, appointment_date, appointment_time, room, notes, created,createdBy)";
            string values = "('" + t.Subject + "','" + t.Patient.Id + "','" + t.Dentist.UserId + "','" + t.ApDate.ToString(Smile.DATE_FORMAT) + "'," + t.Aptime + ",'" + t.Room + "','" + t.Notes+ "','" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "','" + User + "')";
            string query = "INSERT INTO " + tableName + " " + columns + " values " + values + " ;";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                //t.Id = (int)cmd.LastInsertedId;
                t.Id = getLastInsertId(cmd, query, connection);

                this.CloseConnection();

                return true;
            }
            return false;
        }

        internal bool updateAppointment(Appointment t)
        {
            string tableName = "Appointments";
            string setColumns = "subject='" + t.Subject + "',patient='" + t.Patient.Id + "', dentist='" + t.Dentist.UserId + "', appointment_date='" + t.ApDate.ToString(Smile.DATE_FORMAT) + "', appointment_time='" + t.Aptime + "', room='" + t.Room + "', notes='" + t.Notes+ "', modified='" + DateTime.Now.ToString(Smile.LONG_DATE_FORMAT) + "', modifiedBy='" + User + "' ";
            string query = "UPDATE " + tableName + " SET " + setColumns + " WHERE id = "+t.Id+"";

            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }

        internal SmileFile findSmileFileById(string id)
        {
            string query = "SELECT * FROM pfile WHERE id = '"+id+"'";
            SmileFile p = null;
            if (this.OpenPatientConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, patientConnection);
                //cmd.Parameters.Add(new MySqlParameter("id", id));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toSmileFile(dataReader, false);
                }
                dataReader.Close();
                this.ClosePatientConnection();

            }
            return p;
        }

        public Measurement findMeasurementByFileId(string id)
        {
            //string query = "SELECT * FROM Measurement WHERE pfile = @id";
            string query = "SELECT * FROM    measurement WHERE pfile = '"+id+"' and  ID = (SELECT MAX(ID) FROM measurement)";
            Measurement p = null;
            if (this.OpenConnection() == true)
            {
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("id", id));

                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    p = toMeasurement(dataReader);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return p;
        }

        public List<MeasurementTeeth> SelectTeethById(string measurementid)
        {
            //string query = "SELECT * FROM measurementteeth where measurementid = @measurementid ";
            string query = "SELECT a.id, a.teethid, a.measurementid, a.length, a.spoint, a.epoint, a.type,  b.created, b.modified FROM measurementteeth a , measurement b WHERE a.measurementid = "+measurementid+" AND b.id = a.measurementid";
            List<MeasurementTeeth> list = null;
            if (this.OpenConnection() == true)
            {
                list = new List<MeasurementTeeth>();
                IDbCommand cmd = getSqlCommand(query, connection);
                //cmd.Parameters.Add(new MySqlParameter("measurementid", measurementid));
                IDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    MeasurementTeeth p = toTeethMeasurement(dataReader);
                    list.Add(p);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return list;
        }


    }
}
