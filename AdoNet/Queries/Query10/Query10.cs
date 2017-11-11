using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.Queries
{
    class Query10
    {
        public Query10()
        {
            StartQuery();
        }

        private void StartQuery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sqlExpression = @"select OBJC.OBJECT_ID, OBJC.NAME, PARAMS.VALUE
                                    from OBJECTS AS OBJC
                                    left join PARAMS on PARAMS.OBJECT_ID = OBJC.OBJECT_ID and PARAMS.ATTR_ID = 12
                                    where OBJC.OBJECT_TYPE_ID = 6 and PARAMS.VALUE is NULL";

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