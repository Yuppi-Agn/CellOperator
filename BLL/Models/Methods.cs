using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.Models
{
    public class CallLog
    {
        public string host { get; set; }
        public string slave { get; set; }
        public int Time { get; set; }
    }
    public class Method2
    {
        public string Name { get; set; }
        public decimal Bill { get; set; }

        public Method2() { }
        public Method2(Method2 p)
        {
            Name = p.Name;
            Bill = p.Bill;
        }
    }
    public class Report_Calling
    {
        public string Type { get; set; } //Income|Outcome
        public int Minutes { get; set; }
        public string OtherNumber { get; set; }
        public DateTime Date { get; set; }
        public string ConnectionType { get; set; }
        public byte NumConnectionType { get; set; }
        public Report_Calling()
        {
            switch (NumConnectionType)
            {
                default:
                case 0:
                    this.ConnectionType = "По городу";
                    break;
                case 1:
                    this.ConnectionType = "Между городами";
                    break;
                case 2:
                    this.ConnectionType = "Международный";
                    break;
            }
        }
        public void SetTabs(){
            switch (NumConnectionType)
            {
                default:
                case 0:
                    this.ConnectionType = "По городу";
                    break;
                case 1:
                    this.ConnectionType = "Между городами";
                    break;
                case 2:
                    this.ConnectionType = "Международный";
                    break;
            }
        }
    }
    public class Report_SMS
    {
        public string Type { get; set; } //Income|Outcome
        public string OtherNumber { get; set; }
        public DateTime Date { get; set; }
        public string ConnectionType { get; set; }
        public byte NumConnectionType { get; set; }
        public Report_SMS()
        {
            switch (NumConnectionType)
            {
                default:
                case 0:
                    this.ConnectionType = "По городу";
                    break;
                case 1:
                    this.ConnectionType = "Между городами";
                    break;
                case 2:
                    this.ConnectionType = "Международный";
                    break;
            }
        }
        public void SetTabs()
        {
            switch (NumConnectionType)
            {
                default:
                case 0:
                    this.ConnectionType = "По городу";
                    break;
                case 1:
                    this.ConnectionType = "Между городами";
                    break;
                case 2:
                    this.ConnectionType = "Международный";
                    break;
            }
        }
    }
    public class ServiceOutput
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }
        public bool Stat { get; set; }
        public ServiceOutput(C_Service p, bool Stat)
        {
            ID = p.ID;
            Name = p.Name.Trim();
            Price = p.Price;
            Discription = p.Discription;
            this.Stat = Stat;
            if (Stat) Status = "Подключено"; else Status = "Не подключено";
        }
        public ServiceOutput(ServiceDTO p)
        {
            ID = p.ID;
            Name = p.Name.Trim();
            Price = p.Price;
            Discription = p.Discription;
        }
    }
    public class AdministratorReportExpences
    {
        public AdministratorReportExpences(Expenses p)
        {
            if (p != null)
            {
                ID = p.ID;
                ID_number = p.ID_number;
                Date = p.C_Date;
                Expense = p.Expense;
                type = p.C_type;
                if (p.Number != null) Number = p.Number.Number1.Trim();
                switch (type)
                {
                    default:
                    case 1:
                        Type = "Звонки в тарифе";
                        break;
                    case 2:
                        Type = "СМС в тарифе";
                        break;
                    case 3:
                        Type = "Трафик в тарифе";
                        break;
                    case 4:
                        Type = "Звонки вне тарифа";
                        break;
                    case 5:
                        Type = "СМС вне тарифа";
                        break;
                    case 6:
                        Type = "Трафик вне тарифа";
                        break;
                    case 8:
                        Type = "Подключение/смена тарифа";
                        break;
                    case 9:
                        Type = "Подключение услуги";
                        break;
                    case 10:
                        Type = "Плата за услугу в месяц";
                        break;
                    case 11:
                        Type = "Плата за тариф в месяц";
                        break;
                }
            }
        }
        public int ID { get; set; }
        public int? ID_number { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Expense { get; set; }
        public byte? type { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
