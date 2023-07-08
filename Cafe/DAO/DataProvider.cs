using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DAO
{
    public class DataProvider
    {
        private static DataProvider instance; //Ctrl R E, con tro at instance
        
        private readonly string connectionSTR = "Data Source=DELL\\THUHA;Initial Catalog=Cafe;Integrated Security=True";

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set => instance = value;
        }
        private DataProvider() { }
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] ListPara = query.Split(' ');
                    int i = 0;
                    foreach (string para in ListPara)
                    {
                        if (para.Contains('@'))
                        {
                            command.Parameters.AddWithValue(para, parameter[i]);
                            ++i;
                        }
                    }
                }
                data.Load(command.ExecuteReader());
                connection.Close();
            }
            return data;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] ListPara = query.Split(' ');
                    int i = 0;
                    foreach (string para in ListPara)
                    {
                        if (para.Contains('@'))
                        {
                            command.Parameters.AddWithValue(para, parameter[i]);
                            ++i;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }
        public object ExecuteScalar(string query, object[] parameter = null) //trả về giá trị của ô đầu tiên
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] ListPara = query.Split(' ');
                    int i = 0;
                    foreach (string para in ListPara)
                    {
                        if (para.Contains('@'))
                        {
                            command.Parameters.AddWithValue(para, parameter[i]);
                            ++i;
                        }
                    }
                }
                data = command.ExecuteScalar();
                connection.Close();
            }
            return data;
        }
    }

}
