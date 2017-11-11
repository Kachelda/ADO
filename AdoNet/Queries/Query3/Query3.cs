using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.Queries
{
    class Query3
    {
        public Query3()
        {
            StartQuery();
        }

        private void StartQuery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sqlExpression = @"select NAME
                                    from OBJECTS
                                    where OBJECT_TYPE_ID = 5 and OBJECT_ID in
                                    (select OBJ.PARENT_ID
                                    from OBJECTS as OBJ
                                    where OBJ.OBJECT_TYPE_ID = 9
                                    group by OBJ.PARENT_ID
                                    having Count(OBJ.PARENT_ID) > 2)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0}", reader.GetName(0));
                    //Console.WriteLine();

                    while (reader.Read())
                    {
                        object name = reader.GetValue(0);
                       
                        Console.WriteLine("{0}", name);
                    }
                }

                reader.Close();
            }
            Console.ReadLine();
        }
    }
}