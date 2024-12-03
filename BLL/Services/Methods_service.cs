﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.Models;
using DAL;
using static BLL.Models.Methods;

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
            var ThisNumber = db.Number.Find(ID);

            reports = (from calling in db.Calling
                       where calling.ID_number_host == ThisNumber.ID
                       /*from Number1 in db.Number
                              join calling in db.Calling on Number1.ID equals calling.ID_number_host
                              where calling.ID_number_host == ID*/
                       select new Methods.Report_Calling
                       {
                           Type = "Исходящие",
                           Minutes = (int)calling.C_Count,
                           Date = (DateTime)calling.C_Date,
                           OtherNumber = calling.Number_slave,
                           NumConnectionType = (byte)calling.Connection_type,
                       }).ToList<Methods.Report_Calling>();
            reports.AddRange((
                from calling in db.Calling
                where calling.Number_slave.Trim() == ThisNumber.Number1.Trim()
                /*from Number1 in db.Number
                              join calling in db.Calling on Number1.Number1 equals calling.Number_slave
                              where calling.Number_slave == ThisNumber.Number1*/
                select new Methods.Report_Calling
                              {
                    Type = "Входящие",
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
            var ThisNumber = db.Number.Find(ID);

            reports = (from SMS in db.SMS
                       where SMS.ID_number_host == ThisNumber.ID
                       /*from Number1 in db.Number
                              join SMS in db.SMS on Number1.ID equals SMS.ID_number_host
                              where SMS.ID_number_host == ID*/
                       select new Methods.Report_SMS
                       {
                           Type = "Исходящие",
                           Date = (DateTime)SMS.C_Date,
                           OtherNumber = SMS.Number_slave,
                           NumConnectionType = (byte)SMS.Connection_type,
                       }).ToList<Methods.Report_SMS>();
            reports.AddRange((
                from SMS in db.SMS
                where SMS.Number_slave.Trim() == ThisNumber.Number1.Trim()
                /*from Number1 in db.Number
                              join SMS in db.Calling on Number1.Number1 equals SMS.Number_slave
                              where SMS.Number_slave == ThisNumber.Number1*/
                select new Methods.Report_SMS
                {
                    Type = "Входящие",
                    Date = (DateTime)SMS.C_Date,
                    OtherNumber = SMS.Number_slave,
                    NumConnectionType = (byte)SMS.Connection_type,
                }).ToList<Methods.Report_SMS>());
            return reports;
        }
        public List<ExpensesDTO> Report_Expenses(int ID)
        {
            var list = db.Expenses.Where(p => p.ID_number == ID).AsEnumerable();
            if (list.Count() > 0) return list.Select(i => new ExpensesDTO(i)).ToList<ExpensesDTO>();
            else return new List<ExpensesDTO>();
        }
        public List<ServiceOutput> GetServices(int NumberID)
        {
            List<ServiceOutput> Output = new List<ServiceOutput>();
            foreach (var item in db.C_Service.ToList())
            {
                bool Connected;
                if (db.C_Service_Connection.Where(P => P.ID_number == NumberID && P.ID_Service == item.ID).FirstOrDefault() != null) Connected = true; else Connected = false;
                Output.Add(new ServiceOutput(item, Connected));
            }
            return Output;
        }
    }
}
