using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.Queries
{
    class Query6
    {
        public Query6()
        {
            StartQuery();
        }

        private void StartQuery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string sqlExpression = @"select OBJ.NAME as District, OBJ2.NAME as SuperMarket, 
                                    (select count(*) from objects where PARENT_ID = OBJ2.OBJECT_ID and OBJECT_TYPE_ID = 6) as Num_POS,
                                    (select count(*) from objects where PARENT_ID = OBJ2.OBJECT_ID and OBJECT_TYPE_ID = 7) as Num_Pay_Box,
                                    (select count(*) from objects where PARENT_ID = OBJ2.OBJECT_ID and OBJECT_TYPE_ID = 8) as Num_ATM
                                    from OBJECTS as OBJ
                                    join OBJECTS as OBJ2 on OBJ2.PARENT_ID = OBJ.OBJECT_ID
                                    where OBJ.OBJECT_TYPE_ID = 4";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0, -10} \t{1, -10} \t{2, -10} \t{3, -10} \t{4}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3), reader.GetName(4));
                    //Console.WriteLine();

                    while (reader.Read())
                    {
                        object district = reader.GetValue(0);
                        object superMarket = reader.GetValue(1);
                        object numPos = reader.GetValue(2);
                        object numPayBox = reader.GetValue(3);
                        object numAtm = reader.GetValue(4);

                        Console.WriteLine("{0, -10} \t{1, -10} \t{2, -10} \t{3, -10} \t{4}", district, superMarket, numPos, numPayBox, numAtm);
                    }
                }

                reader.Close();
            }
            Console.ReadLine();
        }
    }
}