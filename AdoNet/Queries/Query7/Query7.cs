using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.Queries
{
    class Query7
    {
        public Query7()
        {
            StartQuery();
        }

        private void StartQuery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sqlExpression = @"select RFR.OBJECT_ID, OBJ.NAME
                                    from REFERENCESS as RFR
                                    join OBJECTS as OBJ on OBJ.OBJECT_ID = RFR.OBJECT_ID
                                    group by RFR.OBJECT_ID, OBJ.NAME
                                    having Count(RFR.OBJECT_ID) = 2";

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