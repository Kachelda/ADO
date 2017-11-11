using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.Queries
{
    class Query2
    {
        public Query2()
        {
            StartQuery();
        }

        private void StartQuery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sqlExpression = @"select OBJ.OBJECT_ID, OBJ.NAME
                                    from OBJECTS as OBJ
                                    left join REFERENCESS as RFR on RFR.OBJECT_ID = OBJ.OBJECT_ID
                                    where OBJ.OBJECT_TYPE_ID = 9 and OBJ.OBJECT_ID not in
                                    (select OBJECT_ID from REFERENCESS)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0, -10} \t{1}", reader.GetName(0), reader.GetName(1));
                    //Console.WriteLine();

                    while (reader.Read())
                    {
                        object objectId = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        
                        Console.WriteLine("{0, -10} \t{1}", objectId, name);
                    }
                }

                reader.Close();
            }
            Console.ReadLine();
        }
    }
}