using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BLL.Services;
using BLL.Models;

namespace CellOperator.MVVM.Services
{
    class AuthService
    {
        DataBase_service Database;

        public AuthService()
        {
            Database = new DataBase_service();
        }
        public int AuthCheck(string Username, string Password)
        {

            if (AdministratorAuth_check(Username, Password))
            {
                return 1;
            }
            else if (UserAuth_check(Username, Password))
            {
                return 2;
            }
            return 0;
        }
        private bool AdministratorAuth_check(string Login, string Password)
        {
            AdministratorDTO Admin = Database.FindAdmin(Login, Password);
            if (Admin != null)
                return true;
            else return false;
        }
        private bool UserAuth_check(string Login, string Password)
        {
            ClientDTO Client = Database.FindClient(Login, Password);
            if (Client != null)
                return true;
            else return false;
        }
    }
}
