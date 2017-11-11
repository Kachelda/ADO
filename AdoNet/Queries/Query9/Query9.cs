using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.Queries
{
    class Query9
    {
        public Query9()
        {
            StartQuery();
        }

        private void StartQuery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sqlExpression = @"select OBJ.OBJECT_ID, OBJ.NAME, PARAMS.VALUE
                                    from OBJECTS as OBJ
                                    join PARAMS on PARAMS.OBJECT_ID = OBJ.OBJECT_ID and PARAMS.ATTR_ID = 18
                                    where PARAMS.VALUE like 'No'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0, -10} \t{1, -10} \t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));
                    //Console.WriteLine();

                    while (reader.Read())
                    {
                        object objectId = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object value = reader.GetValue(2);

                        Console.WriteLine("{0, -10} \t{1, -10} \t{2}", objectId, name, value);
                    }
                }

                reader.Close();
            }
            Console.ReadLine();
        }
    }
}