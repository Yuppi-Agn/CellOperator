﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.Models
{
    public class Methods
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
        }
    }
}