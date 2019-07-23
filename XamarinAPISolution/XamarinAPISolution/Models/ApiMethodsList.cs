namespace XamarinAPISolution.Models
{
    public class ApiMethodsList
    {
        private string MainDomain = "http://10.0.2.2:53081/"; // This localhost addres works only on AndroidEmulator

        // PostMethods
        public string API_LoginMethod_POST()
        {
            return MainDomain + "api/Account/Login";
        }
        public string API_RegistrationMethod_POST()
        {
            return MainDomain + "api/Account/Registration";
        }

        // GetMethods
        public string API_UserWalletInfo_GET()
        {
            return MainDomain + "/api/apiWallet/UserData?email=";
        }
        public string API_TransactionsHistory_GET()
        {
            return MainDomain + "/api/apiWallet/TransactionsHistory?email=";
        }
        // This Get Method excists to make Autocompleat Users search field work
        // It also caches users in usersList.json (in the real project better to use SQL Lite or Redis do to size of users List)
        public string API_UsersList_GET()
        {
            return MainDomain + "/api/apiWallet/users";
        }

        // test request
        public string GetPrivateAccsess()
        {
            return MainDomain + "api/AccessTest/PrivateResponce";
        }
    }
}
