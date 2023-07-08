using System.Data;

namespace Cafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set => instance = value; 
        }
        private AccountDAO() { }
        public sbyte Login(string username, string password)
        {
            string query = "exec USP_Login '" + username + "','" + password + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                DTO.Account item = new DTO.Account(data.Rows[0]);
                MemberID = item.MemberID;
                return item.Type;
            }
            return -1;
        }
        private static int MemberID = 1;
        public int GetMemberID()
        {
            return MemberID;
        }
    }
}
