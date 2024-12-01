using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.Models;
using DAL;

namespace BLL.Services
{
    public class Methods_service
    {
        private EF db;
        public Methods_service()
        {
            db = new EF();
        }
        public List<Methods.CallLog> InDB(int Y)
        {
            var results = db.Database.SqlQuery<Methods.CallLog>("EXEC CallLog_С {0}", Y);
            return results.ToList();
        }
        public List<Methods.Method2> Other()
        {
            return (from client in db.Client
                    join number in db.Number on client.ID equals number.ID_Client
                    join individual in db.Individual on client.ID equals individual.ClientID
                    group number by individual.Passport into g
                    select new Methods.Method2
                    {
                        Name = g.Key,
                        Bill = g.Sum(n => n.Bill)
                    }).ToList<Methods.Method2>();
        }
        public List<Methods.Report_Calling> Report_Calling(int ID)
        {
            List<Methods.Report_Calling> reports = new List<Methods.Report_Calling>();
            reports = (from Number1 in db.Number
                       join calling in db.Calling on Number1.ID equals calling.ID_number_host
                       where calling.ID_number_host == ID
                       select new Methods.Report_Calling
                       {
                           Type = "Исходящие",
                           Minutes = (int)calling.C_Count,
                           Date = (DateTime)calling.C_Date,
                           OtherNumber = calling.Number1.Number1,
                           NumConnectionType = (byte)calling.Connection_type,
                       }).ToList<Methods.Report_Calling>();
            reports.AddRange((from Number1 in db.Number
                              join calling in db.Calling on Number1.ID equals calling.ID_number_slave
                              where calling.ID_number_slave == ID
                              select new Methods.Report_Calling
                              {
                                  Minutes = (int)calling.C_Count,
                                  Date = (DateTime)calling.C_Date,
                                  OtherNumber = calling.Number.Number1,
                                  NumConnectionType = (byte)calling.Connection_type,
                              }).ToList<Methods.Report_Calling>());
            return reports;
        }

        public List<Methods.Report_SMS> Report_SMS(int ID)
        {
            List<Methods.Report_SMS> reports = new List<Methods.Report_SMS>();
            reports = (from Number1 in db.Number
                       join SMS in db.SMS on Number1.ID equals SMS.ID_number_host
                       where SMS.ID_number_host == ID
                       select new Methods.Report_SMS
                       {
                           Type = "Исходящие",
                           Date = (DateTime)SMS.C_Date,
                           OtherNumber = SMS.Number1.Number1,
                           NumConnectionType = (byte)SMS.Connection_type,
                       }).ToList<Methods.Report_SMS>();
            reports.AddRange((from Number1 in db.Number
                              join SMS in db.Calling on Number1.ID equals SMS.ID_number_slave
                              where SMS.ID_number_slave == ID
                              select new Methods.Report_SMS
                              {
                                  Type = "Исходящие",
                                  Date = (DateTime)SMS.C_Date,
                                  OtherNumber = SMS.Number1.Number1,
                                  NumConnectionType = (byte)SMS.Connection_type,
                              }).ToList<Methods.Report_SMS>());
            return reports;
        }
    }
}
