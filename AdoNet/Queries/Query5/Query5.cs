using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.Queries
{
    class Query5
    {
        public Query5()
        {
            StartQuery();
        }

        private void StartQuery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sqlExpression = @"select concat('Банкомат ', OBJ.NAME, ' находится по адресу: ', PRM.VALUE, ' ', PRM1.VALUE) as Address
                                    from OBJECTS as OBJ
                                    join PARAMS as PRM on PRM.OBJECT_ID = OBJ.PARENT_ID
                                    join PARAMS as PRM1 on PRM1.OBJECT_ID = OBJ.PARENT_ID
                                    where PRM.ATTR_ID = 10 and PRM1.ATTR_ID = 11 and OBJ.OBJECT_TYPE_ID = 8";

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
                        object address = reader.GetValue(0);
                        
                        Console.WriteLine("{0}", address);
                    }
                }

                reader.Close();
            }
            Console.ReadLine();
        }
    }
}