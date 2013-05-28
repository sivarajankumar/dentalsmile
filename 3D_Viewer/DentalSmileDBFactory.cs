using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp
{
    public class DentalSmileDBFactory
    {
        static DentalSmileDB db;

        public static DentalSmileDB GetInstance()
        {
            if (db == null)
            {
                db = createIntance();
            }
            return db;
        }

        private static DentalSmileDB createIntance()
        {
            string type = Smile.DbType;
            DentalSmileDB d;
            if (type.ToLower().Equals("sqllite"))
            {
                //d = new DentalSmileDBSqlLite();
                d = new DentalSmileDBMySQL();
            }
            else
                if (type.ToLower().Equals("oracle"))
                {
                    //d = new DentalSmileDBOracle();
                    d = new DentalSmileDBMySQL();
                }
                else
                {
                    d = new DentalSmileDBMySQL();
                }
            return d;
        }


        internal static void ReInstantiate()
        {
            db = createIntance();
        }
    }

}
