using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using BLL.Models;
using System.IO;

namespace BLL.Services
{
    class GetDAL
    {
        private static EF EF;
        public static ref EF GetEF()
        {
            if (EF != null) return ref EF;
            else
            {
                EF = new EF();
                {
                    GeneratorService generator = new GeneratorService();
                    if (EF.Client.ToList().Count < 1) generator.GenerateMembersNew(100, 6); //generator.GenerateMembers(700);
                }                
                return ref EF;
            }
        }
    }
    public class ClientService
    {
        private EF db;
        public ClientService()
        {
            db = GetDAL.GetEF();
        }
        public List<ClientDTO> GetAllClients()
        {
            return db.Client.ToList().Select(i => new ClientDTO(i)).ToList();
        }
        public List<Client_IndividualDTO> GetAllIndividualClients()
        {
            return db.Individual.ToList().Select(i => new Client_IndividualDTO(i)).ToList();
        }
        public List<Client_LegalEntityDTO> GetAllLegalEntityClients()
        {
            return db.LegalEntity.ToList().Select(i => new Client_LegalEntityDTO(i)).ToList();
        }
        public ClientDTO GetClient(int ID)
        {
            var item = db.Client.Find(ID);
            if (item == null) return null;
            else return new ClientDTO(item);
        }
        public bool AddClient(ClientDTO C)
        {
            Client It = db.Client.Find(C.ID);
            if (It == null)
            {
                db.Client.Add(new Client()
                {
                    ID = C.ID,
                    C_type = C.C_type,
                    C_Login = C.Login,
                    C_Password = C.Password
                });
                return Save();
            }
            return false;
        }
        public void UpdateClient(ClientDTO C)
        {
            Client It = db.Client.Find(C.ID);
            It.C_type = C.C_type;
            It.C_Login = C.Login;
            It.C_Password = C.Password;
            Save();
        }
        public void DeleteClient(int ID)
        {
            Client It = db.Client.Find(ID);
            if (It != null)
            {
                db.Client.Remove(It);
                Save();
            }
        }
        public bool AddIndividualClient(Client_IndividualDTO C)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Client client = db.Client.Add(new Client()
            {
                ID = db.Client.OrderBy(p => p.ID).ToList().Last().ID + 1,
                C_type = 0,
                C_Login = (58 + r.Next(0, 9999) + r.Next(0, 9999) * 69 * 356).ToString(),
                C_Password = (434 + r.Next(0, 9999) + r.Next(0, 9999) * 123 * 25).ToString()
            });
            db.Individual.Add(new Individual()
            {
                ID = db.Individual.OrderBy(p => p.ID).ToList().Last().ID + 1,
                ClientID = client.ID,
                FirstName = C.FirstName,
                LastName = C.LastName,
                Patronymic = C.Patronymic,
                Passport = C.Passport
            });
            return Save();
        }
        public bool AddLegalEntityClient(Client_LegalEntityDTO C)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            Client client = db.Client.Add(new Client()
            {
                ID = db.Client.OrderBy(p => p.ID).ToList().Last().ID + 1,
                C_type = 1,
                C_Login = (58 + r.Next(0, 9999) + r.Next(0, 9999) * 69 * 356).ToString(),
                C_Password = (434 + r.Next(0, 9999) + r.Next(0, 9999) * 123 * 25).ToString()
            });
            db.LegalEntity.Add(new LegalEntity()
            {
                ID = db.LegalEntity.OrderBy(p => p.ID).ToList().Last().ID + 1,
                ClientID = client.ID,
                Adress = C.Adress,
                IdividualTaxpayerNumber = C.IdividualTaxpayerNumber,
                OrganizationName = C.OrganizationName
            });
            return Save();
        }
        public bool AddIndividualClient(IndividualDTO C, string Password, string Login)
        {
            var list = db.Client.Where(p => p.C_Login == Login).AsEnumerable();
            if (list.Count() > 0) return false;

            Client client = db.Client.Add(new Client()
            {
                ID = db.Client.OrderBy(p => p.ID).ToList().Last().ID + 1,
                C_type = 0,
                C_Login = Login,
                C_Password = Password
            });
            db.Individual.Add(new Individual()
            {
                ID = db.Individual.OrderBy(p => p.ID).ToList().Last().ID + 1,
                ClientID = client.ID,
                FirstName = C.FirstName,
                LastName = C.LastName,
                Patronymic = C.Patronymic,
                Passport = C.Passport
            });
            return Save();
        }
        public bool AddLegalEntityClient(LegalEntityDTO C, string Password, string Login)
        {
            var list = db.Client.Where(p => p.C_Login == Login).AsEnumerable();
            if (list.Count() > 0) return false;

            Client client = db.Client.Add(new Client()
            {
                ID = db.Client.OrderBy(p => p.ID).ToList().Last().ID + 1,
                C_type = 1,
                C_Login = Login,
                C_Password = Password
            });
            db.LegalEntity.Add(new LegalEntity()
            {
                ID = db.LegalEntity.OrderBy(p => p.ID).ToList().Last().ID + 1,
                ClientID = client.ID,
                Adress = C.Adress,
                IdividualTaxpayerNumber = C.IdividualTaxpayerNumber,
                OrganizationName = C.OrganizationName
            });
            return Save();
        }
        public void UpdateIndividualClient(Client_IndividualDTO C)
        {
            Individual It = db.Individual.Find(C.ID);
            It.FirstName = C.FirstName;
            It.LastName = C.LastName;
            It.Patronymic = C.Patronymic;
            It.Passport = C.Passport;
            Save();
        }
        public void UpdateLegalEntityClient(Client_LegalEntityDTO C)
        {
            LegalEntity It = db.LegalEntity.Find(C.ID);
            It.Adress = C.Adress;
            It.OrganizationName = C.OrganizationName;
            It.IdividualTaxpayerNumber = C.IdividualTaxpayerNumber;
            Save();
        }
        public void DeleteIndividualClient(int ID)
        {
            Individual It = db.Individual.Find(ID);

            if (It != null)
            {
                Client It2 = It.Client;
                db.Individual.Remove(It);
                db.Client.Remove(It2);
                Save();
            }
        }
        public void DeleteLegalEntityClient(int ID)
        {
            LegalEntity It = db.LegalEntity.Find(ID);
            if (It != null)
            {
                Client It2 = It.Client;
                db.LegalEntity.Remove(It);
                db.Client.Remove(It2);
                Save();
            }
        }
        public void PasswordChange(int ClientID, string Password)
        {
            Client It = db.Client.Find(ClientID);
            if (It != null)
            {
                It.C_Password = Password;
                Save();
            }
        }
        public ClientDTO FindClient(string Login, string Password)
        {
            var clients = db.Client.Where(p => p.C_Login == Login).Where(p => p.C_Password == Password);
            if (clients.Count() != 0) return clients.AsEnumerable().Select(i => new ClientDTO(i)).First();
            else return null;
        }
        public AdministratorDTO FindAdmin(string Login, string Password)
        {
            var admins = db.Administrator.Where(p => p.C_Login == Login).Where(p => p.C_Password == Password);
            if (admins.Count() != 0) return admins.AsEnumerable().Select(i => new AdministratorDTO(i)).First();
            else return null;
        }
        public string FindName(ClientDTO client)
        {
            var LegalEntity = db.LegalEntity.Where(p => p.ClientID == client.ID).AsEnumerable();
            if (LegalEntity.Count() > 0) return LegalEntity.Select(i => new LegalEntityDTO(i)).First().OrganizationName;

            var Individual = db.Individual.Where(p => p.ClientID == client.ID).AsEnumerable();
            if (Individual.Count() > 0)
            {
                var IndividualDT = Individual.Select(i => new IndividualDTO(i)).First();
                return IndividualDT.FirstName + " " + IndividualDT.LastName + " " + IndividualDT.Patronymic;
            }

            return "Ups...";
        }
        private bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            else return false;
        }
    }
    public class ClientInteractionsService
    {
        private EF db;
        public ClientInteractionsService()
        {
            db = GetDAL.GetEF();
        }
        public List<CallingDTO> GetAllCallings()
        {
            return db.Calling.ToList().Select(i => new CallingDTO(i)).ToList();
        }
        public List<SMSDTO> GetAllSMS()
        {
            return db.SMS.ToList().Select(i => new SMSDTO(i)).ToList();
        }
        public void DeleteCalling(int ID)
        {
            Calling It = db.Calling.Find(ID);
            if (It != null)
            {
                db.Calling.Remove(It);
                Save();
            }
        }
        public void DeleteSMS(int ID)
        {
            SMS It = db.SMS.Find(ID);
            if (It != null)
            {
                db.SMS.Remove(It);
                Save();
            }
        }
        private Expenses AddExpenses(int NumID, byte type, decimal expense)
        {
            /*
             * 1-Звонки в тарифе | бесплатно
             * 2-СМС в тарифе | бесплатно
             * 3-Трафик в тарифе | бесплатно
             * 
             * 4-Звонки вне тарифа | платно
             * 5-СМС вне тарифа | платно
             * 6-Трафик вне тарифа | платно
             * 
             * 8-Подключение/смена тарифа | платно
             * 9-Подключение услуги | платно
             * 
             * 10-Плата за услугу в месяц | платно
             * 11-Плата за тариф в месяц | платно
             */

            int ID = 0;
            if (db.Expenses.Count() > 0) ID = db.Expenses.OrderBy(p => p.ID).ToList().Last().ID + 1;
            if (db.Expenses.Local.OrderBy(p => p.ID).ToList().Last().ID + 1 > ID) ID = db.Expenses.Local.OrderBy(p => p.ID).ToList().Last().ID + 1;
            Expenses expenses = new Expenses()
            {
                ID = ID,
                C_Date = DateTime.Now,
                ID_number = NumID,
                C_type = type,
                Expense = expense
            };
            db.Expenses.Local.Add(expenses);
            return expenses;
        }
        public void UserSendSMS(int IDnumber, string number_slave, string Message)
        {
            var BD_Number = db.Number.Find(IDnumber);
            Expenses expense;

            if (BD_Number.SMS_remains_amount >= 1)
            {
                BD_Number.SMS_remains_amount -= 1;
                expense = AddExpenses(BD_Number.ID, 2, 0);
            }
            else
            {
                var Cost = BD_Number.Tarif.sms_price;
                BD_Number.Bill -= Cost;
                expense = AddExpenses(BD_Number.ID, 5, Cost);
            }

            int ID = 0;
            if (db.SMS.Count() > 0) ID = db.SMS.OrderBy(p => p.ID).ToList().Last().ID + 1;

            SMS ThisSMS = new SMS()
            {
                ID = ID,
                ID_number_host = BD_Number.ID,
                Number = BD_Number,
                Number_slave = number_slave,
                Connection_type = 1,
                C_Data = Message,
                Expenses = expense,
                ID_Expenses = expense.ID,
                C_Date = DateTime.Now,
            };
            db.SMS.Local.Add(ThisSMS);
            Save();
        }
        public void UserMakeCall(int IDnumber, string number_slave, int Duration)
        {
            var BD_Number = db.Number.Find(IDnumber);
            Expenses expense;

            if (BD_Number.MINUTES_remains_amount >= Duration)
            {
                BD_Number.MINUTES_remains_amount -= Duration;
                expense = AddExpenses(BD_Number.ID, 1, 0);
            }
            else //if (BD_Number.Monthly_remains_tarif.MINUTES_amount > 0)
            {
                var Remains = BD_Number.MINUTES_remains_amount;
                BD_Number.MINUTES_remains_amount = 0;
                var Cost = BD_Number.Tarif.call_price_inCity * (Duration - Remains);

                BD_Number.Bill -= Cost;
                expense = AddExpenses(BD_Number.ID, 4, Cost);
            }

            int ID = 0;
            if (db.Calling.Count() > 0) ID = db.Calling.OrderBy(p => p.ID).ToList().Last().ID + 1;

            Calling ThisCalling = new Calling()
            {
                ID = ID,
                ID_number_host = BD_Number.ID,
                Number = BD_Number,
                Number_slave = number_slave,
                Connection_type = 1,
                C_Count = Duration,
                Expenses = expense,
                ID_Expenses = expense.ID,
                C_Date = DateTime.Now,
            };
            db.Calling.Local.Add(ThisCalling);
            Save();
        }
        public void UserSpentInternet(int IDnumber, int Amount)
        {
            var BD_Number = db.Number.Find(IDnumber);
            Expenses expense;

            if (BD_Number.Internet_remains_amount >= Amount)
            {
                BD_Number.Internet_remains_amount -= Amount;
                expense = AddExpenses(BD_Number.ID, 3, 0);
            }
            else
            {
                var Remains = BD_Number.Internet_remains_amount;
                BD_Number.Internet_remains_amount = 0;
                var Cost = BD_Number.Tarif.internet_price / 1000 * (Amount - Remains);

                BD_Number.Bill -= Cost;
                expense = AddExpenses(BD_Number.ID, 6, Cost);
            }

            int ID = 0;
            if (db.Internet.Count() > 0) ID = db.Internet.OrderBy(p => p.ID).ToList().Last().ID + 1;
            Internet ThisInternet = new Internet()
            {
                ID = ID,
                Number = BD_Number,
                C_Data = Amount,
                Expenses = expense,
                ID_Expenses = expense.ID,
                C_Date = DateTime.Now,
            };
            db.Internet.Local.Add(ThisInternet);
            Save();
        }        
        public void MonthSpent()
        {
            var numbers = db.Number.ToList();
            for (int i = 0; i < numbers.Count; i++)
            {
                //10 - Плата за услугу в месяц | платно
                //11 - Плата за тариф в месяц | платно
                var NumID = numbers[i].ID;
                var Tarif = db.Tarif.Find(numbers[i].ID_Tarif);
                if (Tarif != null)
                {
                    decimal TarifCost = Tarif.Price;
                    AddExpenses(NumID, 11, TarifCost);
                    numbers[i].Bill -= TarifCost;
                    numbers[i].Internet_remains_amount = Tarif.Internet_amount;
                    numbers[i].MINUTES_remains_amount = Tarif.MINUTES_amount;
                    numbers[i].SMS_remains_amount = Tarif.SMS_amount;
                }
                var Services = db.C_Service_Connection.Where(p => p.ID_number == NumID).ToList();
                for (int j = 0; j < Services.Count(); j++)
                {
                    var Service = db.C_Service.Find(Services[j].ID_Service);
                    decimal ServiceCost = (decimal)Service.Price;
                    AddExpenses(NumID, 10, ServiceCost);
                    numbers[i].Bill -= ServiceCost;
                }
            }
            Save();
        }
        private bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            else return false;
        }
    }
    public class TarifService
    {
        private EF db;
        public TarifService()
        {
            db = GetDAL.GetEF();
        }
        public List<TarifDTO> GetAllTarifs()
        {
            return db.Tarif.ToList().Select(i => new TarifDTO(i)).ToList();
        }
        public List<TarifDTO> GetAllTarifs(ClientDTO client)
        {
            return db.Tarif.ToList().Where(i => i.C_type == client.C_type).Select(i => new TarifDTO(i)).ToList();
        }
        public List<Tarif_HistoryDTO> GetAllTarif_history()
        {
            return db.Tarif_History.ToList().Select(i => new Tarif_HistoryDTO(i)).ToList();
        }
        private Expenses AddExpenses(int NumID, byte type, decimal expense)
        {
            /*
             * 1-Звонки в тарифе | бесплатно
             * 2-СМС в тарифе | бесплатно
             * 3-Трафик в тарифе | бесплатно
             * 
             * 4-Звонки вне тарифа | платно
             * 5-СМС вне тарифа | платно
             * 6-Трафик вне тарифа | платно
             * 
             * 8-Подключение/смена тарифа | платно
             * 9-Подключение услуги | платно
             * 
             * 10-Плата за услугу в месяц | платно
             * 11-Плата за тариф в месяц | платно
             */

            int ID = 0;
            if (db.Expenses.Count() > 0) ID = db.Expenses.OrderBy(p => p.ID).ToList().Last().ID + 1;
            if (db.Expenses.Local.OrderBy(p => p.ID).ToList().Last().ID + 1 > ID) ID = db.Expenses.Local.OrderBy(p => p.ID).ToList().Last().ID + 1;
            Expenses expenses = new Expenses()
            {
                ID = ID,
                C_Date = DateTime.Now,
                ID_number = NumID,
                C_type = type,
                Expense = expense
            };
            db.Expenses.Add(expenses);
            return expenses;
        }
        public void DeleteTarif(int ID)
        {
            Tarif It = db.Tarif.Find(ID);
            if (It != null)
            {
                db.Tarif.Remove(It);
                Save();
            }
        }
        public bool ChangeTarif_NumByClient(ClientDTO client, int TarifID, int NumID)
        {
            Tarif NewTarif = db.Tarif.Find(TarifID);
            Number ThisNum = db.Number.Find(NumID);
            if (NewTarif == null || ThisNum == null) return false;

            int ID = 0;
            if (db.Tarif_History.Count() > 0) ID = db.Tarif_History.OrderBy(p => p.ID).ToList().Last().ID + 1;

            Tarif_History tarif_History = new Tarif_History();
            tarif_History.ID = ID;
            tarif_History.ID_Number = NumID;
            tarif_History.ID_Tarif = TarifID;
            tarif_History.StartDate = ThisNum.TarifDate;
            tarif_History.EndDate = DateTime.Now;
            db.Tarif_History.Add(tarif_History);

            ThisNum.ID_Tarif = NewTarif.ID;
            ThisNum.TarifDate = DateTime.Now;

            ThisNum.SMS_remains_amount = NewTarif.SMS_amount;
            ThisNum.MINUTES_remains_amount = NewTarif.MINUTES_amount;
            ThisNum.Internet_remains_amount = NewTarif.Internet_amount;

            ThisNum.Bill -= NewTarif.Price;
            AddExpenses(NumID, 8, NewTarif.Price);

            return Save();
        }
        private bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            else return false;
        }
    }
    public class ServicesService
    {
        private EF db;
        public ServicesService()
        {
            db = GetDAL.GetEF();
        }
        private Expenses AddExpenses(int NumID, byte type, decimal expense)
        {
            /*
             * 1-Звонки в тарифе | бесплатно
             * 2-СМС в тарифе | бесплатно
             * 3-Трафик в тарифе | бесплатно
             * 
             * 4-Звонки вне тарифа | платно
             * 5-СМС вне тарифа | платно
             * 6-Трафик вне тарифа | платно
             * 
             * 8-Подключение/смена тарифа | платно
             * 9-Подключение услуги | платно
             * 
             * 10-Плата за услугу в месяц | платно
             * 11-Плата за тариф в месяц | платно
             */

            int ID = 0;
            if (db.Expenses.Count() > 0) ID = db.Expenses.OrderBy(p => p.ID).ToList().Last().ID + 1;
            if (db.Expenses.Local.OrderBy(p => p.ID).ToList().Last().ID + 1 > ID) ID = db.Expenses.Local.OrderBy(p => p.ID).ToList().Last().ID + 1;
            Expenses expenses = new Expenses()
            {
                ID = ID,
                C_Date = DateTime.Now,
                ID_number = NumID,
                C_type = type,
                Expense = expense
            };
            db.Expenses.Add(expenses);
            return expenses;
        }
        public List<ServiceDTO> GetAllServices()
        {
            return db.C_Service.ToList().Select(i => new ServiceDTO(i)).ToList();
        }
        public List<Service_ConnectionDTO> GetAllService_Connection()
        {
            return db.C_Service_Connection.ToList().Select(i => new Service_ConnectionDTO(i)).ToList();
        }
        public List<C_Service_Connection_HistoryDTO> GetAllC_Service_Connection_History()
        {
            return db.C_Service_Connection_History.ToList().Select(i => new C_Service_Connection_HistoryDTO(i)).ToList();
        }
        public void ServiceConnect(int NumID, int ServiceID)
        {
            var Number = db.Number.Find(NumID);
            var Service = db.C_Service.Find(ServiceID);

            if (Number == null || Service == null) return;

            int ID = 0;
            if (db.C_Service_Connection.OrderBy(p => p.ID).ToList().Count > 0) ID = db.C_Service_Connection.OrderBy(p => p.ID).ToList().Last().ID + 1;

            db.C_Service_Connection.Add(new C_Service_Connection()
            {
                ID = ID,
                C_Date = DateTime.Now,
                ID_number = Number.ID,
                ID_Service = Service.ID,
            });

            Number.Bill -= (decimal)Service.Price;
            AddExpenses(Number.ID, 9, (decimal)Service.Price);
            Save();
        }
        public void ServiceDisconnect(int NumID, int ServiceID)
        {
            var Number = db.Number.Find(NumID);
            var Service = db.C_Service.Find(ServiceID);

            if (Number == null || Service == null) return;

            var Service_Connection = db.C_Service_Connection.Where(P => P.ID_number == Number.ID && P.ID_Service == Service.ID).FirstOrDefault();
            if (Service_Connection == null) return;

            int ID = 0;
            if (db.C_Service_Connection_History.Count() > 0) ID = db.C_Service_Connection_History.OrderBy(p => p.ID).ToList().Last().ID + 1;

            db.C_Service_Connection_History.Add(new C_Service_Connection_History()
            {
                ID = ID,
                StartDate = Service_Connection.C_Date,
                EndDate = DateTime.Now,
                ID_number = Number.ID,
                ID_Service = Service.ID,
            });
            db.C_Service_Connection.Remove(Service_Connection);
            Save();
        }
        private bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            else return false;
        }
    }
    public class NumberService
    {
        private EF db;
        public NumberService()
        {
            db = GetDAL.GetEF();
        }
        public List<NumberDTO> GetAllNumbers()
        {
            return db.Number.ToList().Select(i => new NumberDTO(i)).ToList();
        }
        public bool AddNumber(NumberDTO C)
        {
            Number It = db.Number.Find(C.ID);
            if (It == null)
            {
                byte status;
                if (C.Status) status = 1; else status = 0;
                db.Number.Add(new Number()
                {
                    ID = C.ID,
                    ID_Client = C.ID_Client,
                    ID_Tarif = C.ID_Tarif,
                    Bill = C.Bill,
                    Number1 = C.Number,
                    TarifDate = C.TarifDate,
                    C_status = status
                });
                return Save();
            }
            return false;
        }
        public void UpdateNumber(NumberDTO C)
        {
            Number It = db.Number.Find(C.ID);
            It.ID = C.ID;
            It.ID_Client = C.ID_Client;
            It.ID_Tarif = C.ID_Tarif;
            It.Bill = C.Bill;
            It.Number1 = C.Number;
            It.TarifDate = C.TarifDate;
            if (C.Status) It.C_status = 1; else It.C_status = 0;
            Save();
        }
        public void DeleteNumber(int ID)
        {
            Number It = db.Number.Find(ID);
            if (It != null)
            {
                db.Number.Remove(It);
                Save();
            }
        }
        public void AddMoney(int ID, decimal Money)
        {
            Number It = db.Number.Find(ID);
            if (It != null)
            {
                It.Bill += Money;
                Save();
            }
        }
        public List<NumberDTO> FindNumbers(ClientDTO client)
        {
            if (client == null) return null;
            var list = db.Number.Where(p => p.ID_Client == client.ID).AsEnumerable();
            if (list.Count() > 0) return list.Select(i => new NumberDTO(i)).ToList<NumberDTO>();
            else return new List<NumberDTO>();
        }

        public bool BuyNumber(ClientDTO client, int TarifID)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            bool IsExists = true;
            Number NewNumber = new Number();
            while (IsExists)
            {

                int ID = 0;
                if (db.Number.Count() > 0) ID = db.Number.OrderBy(p => p.ID).ToList().Last().ID + 1;

                NewNumber = new Number()
                {
                    ID = ID,
                    Bill = 0,
                    ID_Client = client.ID,
                    TarifDate = DateTime.Now,
                    ID_Tarif = TarifID,
                    C_status = 1,
                    Number1 = "+7980" + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9),
                    SMS_remains_amount = db.Tarif.Find(TarifID).SMS_amount,
                    MINUTES_remains_amount = db.Tarif.Find(TarifID).MINUTES_amount,
                    Internet_remains_amount = db.Tarif.Find(TarifID).Internet_amount,
                };
                var List = db.Number.Where(p => p.Number1 == NewNumber.Number1).AsEnumerable();
                IsExists = List.Count() > 0;
            }
            db.Number.Add(NewNumber);
            //NewNumber.Bill -= db.Tarif.Find(TarifID).Price.Price;
            //AddExpenses(NewNumber.ID, 8, db.Tarif.Find(TarifID).Price);

            Save();
            return false;
        }
        private bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            else return false;
        }
    }
    class GeneratorService
    {
        private EF db;
        private Random rand = new Random((int) DateTime.Now.Ticks);
        public GeneratorService()
        {
            db = GetDAL.GetEF();
        }
        private bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            else return false;
        }
        private void CheckUpMonthly_remains_tarif()
        {
            var Numbers = db.Number.Local.ToList();
            foreach (var item in Numbers)
            {
                Tarif Tarif = db.Tarif.Find(item.ID_Tarif);
                if (Tarif != null)
                {
                    item.Internet_remains_amount = Tarif.Internet_amount;
                    item.MINUTES_remains_amount = Tarif.MINUTES_amount;
                    item.SMS_remains_amount = Tarif.SMS_amount;
                }
            }
        }
        private int  GenInt(int First, int Last)
        {
            return rand.Next(First, Last+1);
        }
        public void GenerateMembers(int count)
        {
            const int TarifNumber = 10;

            List<string> LastNames = new List<string>();
            List<string> FirstNames = new List<string>();
            List<string> Patronomics = new List<string>();
            List<string> Tarifs = new List<string>();
            List<string> Somethings = new List<string>();
            List<string> Adreeses = new List<string>();

            //if (true)
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                StreamReader sr;

                sr = new StreamReader(@"C:\Users\Yuppi\source\repos\PO Construction\ResourcesForGenerator\FLP.txt");
                string line = sr.ReadLine();
                while (line != null)
                {
                    var Items = line.Split(' ');
                    FirstNames.Add(Items[0]);
                    LastNames.Add(Items[1]);
                    Patronomics.Add(Items[2]);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader(@"C:\Users\Yuppi\source\repos\PO Construction\ResourcesForGenerator\Tarif.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Tarifs.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader(@"C:\Users\Yuppi\source\repos\PO Construction\ResourcesForGenerator\Something.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    switch (rand.Next(0, 3))
                    {
                        default:
                        case 0:
                            line = "ООО \"" + line + '\"';
                            break;
                        case 1:
                            line = "АО \"" + line + '\"';
                            break;
                        case 2:
                            line = "ПАО \"" + line + '\"';
                            break;
                    }
                    Somethings.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader(@"C:\Users\Yuppi\source\repos\PO Construction\ResourcesForGenerator\Adress.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Adreeses.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }

            Random r = new Random((int)DateTime.Now.Ticks);// new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < count; i++)
            {
                switch (i % 2)
                {
                    case 0:
                        db.Client.Add(new Client()
                        {
                            ID = i,
                            C_type = ((byte)(i % 2)),
                            C_Login = (58 + r.Next(0, 9999) + r.Next(0, 9999) * 69 * 356).ToString(),
                            C_Password = (434 + r.Next(0, 9999) + r.Next(0, 9999) * 123 * 25).ToString()
                        });
                        db.Individual.Add(new Individual()
                        {
                            ID = i / 2,
                            ClientID = i,
                            FirstName = FirstNames[r.Next(0, FirstNames.Count - 1)],
                            LastName = LastNames[r.Next(0, LastNames.Count - 1)],
                            Patronymic = Patronomics[r.Next(0, Patronomics.Count - 1)],
                            Passport = r.Next(1000, 9999) + "" + r.Next(100000, 999999)
                        });
                        break;
                    case 1:
                        db.Client.Add(new Client()
                        {
                            ID = i,
                            C_type = ((byte)(i % 2)),
                            C_Login = (58 + r.Next(0, 9999) + r.Next(0, 9999) * 67 * 356).ToString(),
                            C_Password = (434 + r.Next(0, 9999) + r.Next(0, 9999) * 123 * 25).ToString()
                        });
                        db.LegalEntity.Add(new LegalEntity()
                        {
                            ID = i / 2,
                            ClientID = i,
                            Adress = Adreeses[r.Next(0, Adreeses.Count - 1)],
                            IdividualTaxpayerNumber = r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + "",
                            OrganizationName = Somethings[r.Next(0, Somethings.Count - 1)]
                        });
                        break;
                }
            }

            for (int i = 0; i < TarifNumber; i++)
            {
                db.Tarif.Add(new Tarif()
                {
                    ID = i,
                    Price = (decimal)(r.Next(10, 1000) + r.Next(0, 99) / 100),
                    SMS_amount = r.Next(10, 500),
                    MINUTES_amount = r.Next(10, 500),
                    Internet_amount = r.Next(10, 500),
                    call_price_inCity = (decimal)(r.Next(1, 10) + r.Next(0, 99) / 100),
                    call_price_outCity = (decimal)(r.Next(1, 50) + r.Next(0, 99) / 100),
                    call_price_outContry = (decimal)(r.Next(100, 400) + r.Next(0, 99) / 100),
                    sms_price = (decimal)(r.Next(1, 10) + r.Next(0, 99) / 100),
                    internet_price = (decimal)(r.Next(1, 100) + r.Next(0, 99) / 100),
                    C_status = 1,
                    C_type = ((byte)(i % 2)),
                    name = Tarifs[r.Next(0, Tarifs.Count - 1)]
                });
            }

            for (int i = 0; i < count; i++)
            {
                db.Number.Add(new Number()
                {
                    ID = i,
                    Bill = r.Next(0, 10000) + r.Next(0, 99) / 100,
                    ID_Client = i,
                    TarifDate = DateTime.Now,
                    ID_Tarif = i % TarifNumber,
                    C_status = 1,
                    Number1 = "+7980" + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9)
                });
            }

            db.C_Service.Add(new C_Service()
            {
                ID = 0,
                Name = "Живой баланс",
                Discription = "При каждой совершенной вами трате отправляет вам на телефон уведомление об оставшейся сумме на счету и сумме этой траты",
                Price = r.Next(10, 300) + r.Next(0, 99) / 100
            });
            db.C_Service.Add(new C_Service()
            {
                ID = 1,
                Name = "Обещанный платеж",
                Discription = "Нужно совершить важный звонок, а денег на счету нет? Не беда! С этой услугой мы дадим вам в кредит 100 рублей до следующего пополенния счета и уведомим об этом!",
                Price = r.Next(7, 100) + r.Next(0, 99) / 100
            });
            db.C_Service.Add(new C_Service()
            {
                ID = 2,
                Name = "Секретарь",
                Discription = "Умный ИИ отвечает на совершенные Вам звонки сам и старается ответить на все вопросы звонившего, а вам лишь останется прочитать результаты этого диалога!",
                Price = r.Next(100, 1000) + r.Next(0, 99) / 100
            });

            for (int i = 0; i < count / 2; i++)
            {
                int i1 = r.Next(0, count / 2 - 1), i2 = r.Next(count / 2, count);
                db.Calling.Add(new Calling()
                {
                    ID = i,
                    ID_number_host = i1,
                    Number_slave = db.Number.Local.Where(P => P.ID == i2).First().Number1,
                    Connection_type = (byte)(r.Next(0, 3)),
                    C_Count = (r.Next(1, 60)),
                    C_Date = DateTime.Now
                });
            }
            for (int i = 0; i < count / 2; i++)
            {
                int i1 = r.Next(0, count / 2 - 1), i2 = r.Next(count / 2, count);
                db.SMS.Add(new SMS()
                {
                    ID = i,
                    ID_number_host = i1,
                    Number_slave = db.Number.Local.Where(P => P.ID == i2).First().Number1,
                    Connection_type = (byte)(r.Next(0, 3)),
                    C_Date = DateTime.Now,
                    C_Data = "" + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 999999)
                });
            }

            int SpecID = db.Client.Local.Count;

            db.Client.Add(new Client()
            {
                ID = SpecID,
                C_type = 0,
                C_Login = "123",
                C_Password = "123"
            });
            db.Individual.Add(new Individual()
            {
                ID = db.Individual.Local.Count,
                ClientID = SpecID,
                FirstName = "Юппи",
                LastName = "Агненко",
                Patronymic = "-",
                Passport = r.Next(1000, 9999) + "" + r.Next(100000, 999999)
            });

            db.Number.Add(new Number()
            {
                ID = db.Number.Local.Count,
                Bill = r.Next(0, 10000) + r.Next(0, 99) / 100,
                ID_Client = SpecID,
                TarifDate = DateTime.Now,
                ID_Tarif = 0,
                C_status = 1,
                Number1 = "+7980" + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9)
            });

            for (int i = 0; i < count / 2; i++)
            {
                int i1 = r.Next(0, count / 2 - 1);
                switch (i % 2)
                {
                    case 0:
                        db.Calling.Add(new Calling()
                        {
                            ID = db.Calling.Local.Count,
                            ID_number_host = SpecID,
                            Number_slave = db.Number.Local.Where(P => P.ID == i1).First().Number1,
                            Connection_type = (byte)(r.Next(0, 3)),
                            C_Count = (r.Next(1, 60)),
                            C_Date = DateTime.Now
                        });
                        break;
                    case 1:
                        db.Calling.Add(new Calling()
                        {
                            ID = db.Calling.Local.Count,
                            ID_number_host = i1,
                            Number_slave = db.Number.Local.Where(P => P.ID_Client == SpecID).First().Number1,
                            Connection_type = (byte)(r.Next(0, 3)),
                            C_Count = (r.Next(1, 60)),
                            C_Date = DateTime.Now
                        });
                        break;
                }

            }
            for (int i = 0; i < count / 2; i++)
            {
                int i1 = r.Next(0, count / 2 - 1);
                switch (i % 2)
                {
                    case 0:
                        db.SMS.Add(new SMS()
                        {
                            ID = db.SMS.Local.Count,
                            ID_number_host = SpecID,
                            Number_slave = db.Number.Local.Where(P => P.ID == i1).First().Number1,
                            Connection_type = (byte)(r.Next(0, 3)),
                            C_Date = DateTime.Now,
                            C_Data = "" + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 999999)
                        });
                        break;
                    case 1:
                        db.SMS.Add(new SMS()
                        {
                            ID = db.SMS.Local.Count,
                            ID_number_host = i1,
                            Number_slave = db.Number.Local.Where(P => P.ID_Client == SpecID).First().Number1,
                            Connection_type = (byte)(r.Next(0, 3)),
                            C_Date = DateTime.Now,
                            C_Data = "" + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 999999)
                        });
                        break;
                }
            }

            CheckUpMonthly_remains_tarif();
            Save();

            LastNames.Clear();
            FirstNames.Clear();
            Patronomics.Clear();
            Tarifs.Clear();
            Somethings.Clear();
            Adreeses.Clear();
        }
        public void GenerateMembersNew(int count, int Months)
        {
            const int TarifNumber = 10;

            List<string> LastNames = new List<string>();
            List<string> FirstNames = new List<string>();
            List<string> Patronomics = new List<string>();
            List<string> Tarifs = new List<string>();
            List<string> Somethings = new List<string>();
            List<string> Adreeses = new List<string>();

            //if (true)
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                StreamReader sr;

                sr = new StreamReader(@"C:\Users\Yuppi\source\repos\PO Construction\ResourcesForGenerator\FLP.txt");
                string line = sr.ReadLine();
                while (line != null)
                {
                    var Items = line.Split(' ');
                    FirstNames.Add(Items[0]);
                    LastNames.Add(Items[1]);
                    Patronomics.Add(Items[2]);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader(@"C:\Users\Yuppi\source\repos\PO Construction\ResourcesForGenerator\Tarif.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Tarifs.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader(@"C:\Users\Yuppi\source\repos\PO Construction\ResourcesForGenerator\Something.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    switch (rand.Next(0, 3))
                    {
                        default:
                        case 0:
                            line = "ООО \"" + line + '\"';
                            break;
                        case 1:
                            line = "АО \"" + line + '\"';
                            break;
                        case 2:
                            line = "ПАО \"" + line + '\"';
                            break;
                    }
                    Somethings.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader(@"C:\Users\Yuppi\source\repos\PO Construction\ResourcesForGenerator\Adress.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Adreeses.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }

            Random r = new Random((int)DateTime.Now.Ticks);// new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < count; i++)
            {
                switch (i % 2)
                {
                    case 0:
                        db.Client.Local.Add(new Client()
                        {
                            ID = i,
                            C_type = ((byte)(i % 2)),
                            C_Login = (58 + r.Next(0, 9999) + r.Next(0, 9999) * 69 * 356).ToString(),
                            C_Password = (434 + r.Next(0, 9999) + r.Next(0, 9999) * 123 * 25).ToString()
                        });
                        db.Individual.Local.Add(new Individual()
                        {
                            ID = i / 2,
                            ClientID = i,
                            FirstName = FirstNames[r.Next(0, FirstNames.Count - 1)],
                            LastName = LastNames[r.Next(0, LastNames.Count - 1)],
                            Patronymic = Patronomics[r.Next(0, Patronomics.Count - 1)],
                            Passport = r.Next(1000, 9999) + "" + r.Next(100000, 999999)
                        });
                        break;
                    case 1:
                        db.Client.Local.Add(new Client()
                        {
                            ID = i,
                            C_type = ((byte)(i % 2)),
                            C_Login = (58 + r.Next(0, 9999) + r.Next(0, 9999) * 67 * 356).ToString(),
                            C_Password = (434 + r.Next(0, 9999) + r.Next(0, 9999) * 123 * 25).ToString()
                        });
                        db.LegalEntity.Local.Add(new LegalEntity()
                        {
                            ID = i / 2,
                            ClientID = i,
                            Adress = Adreeses[r.Next(0, Adreeses.Count - 1)],
                            IdividualTaxpayerNumber = r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + "",
                            OrganizationName = Somethings[r.Next(0, Somethings.Count - 1)]
                        });
                        break;
                }
            }

            for (int i = 0; i < TarifNumber; i++)
            {
                db.Tarif.Local.Add(new Tarif()
                {
                    ID = i,
                    Price = (decimal)(r.Next(10, 1000) + r.Next(0, 99) / 100),
                    SMS_amount = r.Next(10, 500),
                    MINUTES_amount = r.Next(10, 500),
                    Internet_amount = r.Next(10, 500),
                    call_price_inCity = (decimal)(r.Next(1, 10) + r.Next(0, 99) / 100),
                    call_price_outCity = (decimal)(r.Next(1, 50) + r.Next(0, 99) / 100),
                    call_price_outContry = (decimal)(r.Next(10, 20) + r.Next(0, 99) / 100),
                    sms_price = (decimal)(r.Next(1, 10) + r.Next(0, 99) / 100),
                    internet_price = (decimal)(r.Next(1, 100) + r.Next(0, 99) / 100),
                    C_status = 1,
                    C_type = ((byte)(i % 2)),
                    name = Tarifs[r.Next(0, Tarifs.Count - 1)]
                });
            }

            for (int i = 0; i < count; i++)
            {
                db.Number.Local.Add(new Number()
                {
                    ID = i,
                    Bill = r.Next(0, 10000) + r.Next(0, 99) / 100,
                    ID_Client = i,
                    TarifDate = DateTime.Now,
                    ID_Tarif = i % TarifNumber,
                    C_status = 1,
                    Number1 = "+7980" + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9)
                });
            }

            db.C_Service.Local.Add(new C_Service()
            {
                ID = 0,
                Name = "Живой баланс",
                Discription = "При каждой совершенной вами трате отправляет вам на телефон уведомление об оставшейся сумме на счету и сумме этой траты",
                Price = r.Next(10, 300) + r.Next(0, 99) / 100
            });
            db.C_Service.Local.Add(new C_Service()
            {
                ID = 1,
                Name = "Обещанный платеж",
                Discription = "Нужно совершить важный звонок, а денег на счету нет? Не беда! С этой услугой мы дадим вам в кредит 100 рублей до следующего пополенния счета и уведомим об этом!",
                Price = r.Next(7, 100) + r.Next(0, 99) / 100
            });
            db.C_Service.Local.Add(new C_Service()
            {
                ID = 2,
                Name = "Секретарь",
                Discription = "Умный ИИ отвечает на совершенные Вам звонки сам и старается ответить на все вопросы звонившего, а вам лишь останется прочитать результаты этого диалога!",
                Price = r.Next(100, 1000) + r.Next(0, 99) / 100
            });
            {
                int GloblExpencesID = 0;
                int GlobalSMSID = db.SMS.Local.Count;
                int GlobalCallingID = db.Calling.Local.Count;
                int GlobalInternetID = db.Internet.Local.Count;

                List<Tarif> CTarifs = new List<Tarif>();
                foreach(var item in db.Tarif.Local)
                {
                    CTarifs.Add(item);
                }
                List<Number> CNumbers = new List<Number>();
                foreach (var item in db.Number.Local)
                {
                    CNumbers.Add(item);
                }

                var Added_SMS = new List<SMS>();
                var Added_Calling = new List<Calling>();
                var Added_Internet = new List<Internet>();
                var Added_Expenses = new List<Expenses>();

                var StartTime = DateTimeOffset.UtcNow;//DateTime.Now;
                int ThisMounth = DateTime.Now.Month;
                for (int StartMounth = ThisMounth - Months; StartMounth <= ThisMounth; StartMounth++)
                {
                    for (int ThisNumberID = 0; ThisNumberID < count; ThisNumberID++)
                    {
                        var ThisNumber = CNumbers[ThisNumberID]; //db.Number.Local.Where(p => p.ID == ThisNumberID).First();
                        if (ThisNumber == null)
                        {
                            Console.WriteLine("Error on generator with NumID " + ThisNumberID);
                            continue;
                        }
                        var ThisTarif = CTarifs[ThisNumber.ID_Tarif];//db.Tarif.Local.Where(p => p.ID == ThisNumber.ID_Tarif).First();
                        if (ThisTarif == null)
                        {
                            Console.WriteLine("Error on generator with TarifID " + ThisNumberID);
                            continue;
                        }
                        int ID = GloblExpencesID++;//db.Expenses.Local.Count;

                        DateTime ThisDate = DateTime.Now;
                        ThisDate=ThisDate.AddMonths(StartMounth - ThisDate.Month);
                        ThisDate = ThisDate.AddDays(28 - ThisDate.Day);

                        Added_Expenses.Add(new Expenses()
                        {
                            ID = ID,
                            Expense = ThisTarif.Price,
                            ID_number = ThisNumberID,
                            C_type = 11,
                            C_Date = ThisDate,
                        });

                        /*db.Expenses.Local.Add(new Expenses()
                        {
                            ID = ID,
                            Expense = ThisTarif.Price,
                            ID_number = ThisNumberID,
                            C_type = 11,
                        });*/
                    }


                    for (int StartDay = 0; StartDay <= 2; StartDay++) //28
                    {
                        for (int ThisNumberID = 0; ThisNumberID < count; ThisNumberID++)
                        {
                            var ThisNumber = CNumbers[ThisNumberID]; // db.Number.Local.Where(p => p.ID == ThisNumberID).First();
                            if (ThisNumber == null)
                            {
                                Console.WriteLine("Error on generator with NumID " + ThisNumberID);
                                continue;
                            }
                            var ThisTarif = CTarifs[ThisNumber.ID_Tarif];//db.Tarif.Local.Where(p => p.ID == ThisNumber.ID_Tarif).First();
                            if (ThisTarif == null)
                            {
                                Console.WriteLine("Error on generator with TarifID " + ThisNumberID);
                                continue;
                            }
                            const int MaxInteractions = 5;
                            int SMSCount = GenInt(0, MaxInteractions), CallingCount = GenInt(0, MaxInteractions), InternetCount = GenInt(0, MaxInteractions);

                            for (int i = 0; i < CallingCount; i++)
                            {
                                decimal TarifExpence = 0;
                                int connectiont = r.Next(0, 3);
                                int Ccount = r.Next(1, 7);
                                switch (connectiont)
                                {
                                    default:
                                    case 0:
                                        TarifExpence = ThisTarif.call_price_inCity;
                                        break;
                                    case 1:
                                        TarifExpence = ThisTarif.call_price_outCity;
                                        break;
                                    case 2:
                                        TarifExpence = ThisTarif.call_price_outContry;
                                        break;
                                }

                                int i2 = r.Next(count / 2, count);
                                int MainID = GlobalCallingID++,// db.Calling.Local.Count,
                                    SecID = GloblExpencesID++;// db.Expenses.Local.Count;
                                DateTime ThisDate = DateTime.Now;
                                ThisDate = ThisDate.AddMonths(StartMounth - ThisDate.Month);
                                ThisDate = ThisDate.AddDays(StartDay - ThisDate.Day);

                                ThisDate = ThisDate.AddHours(GenInt(0, 24));
                                ThisDate = ThisDate.AddMinutes(GenInt(0, 60));
                                ThisDate = ThisDate.AddSeconds(GenInt(0, 60));

                                Added_Expenses.Add(new Expenses()
                                {
                                    ID = SecID,
                                    C_Date = ThisDate,
                                    ID_number = ThisNumberID,
                                    C_type = 4,
                                    Expense = Ccount * TarifExpence,
                                });
                                Added_Calling.Add(new Calling()
                                {
                                    ID = MainID,
                                    ID_number_host = ThisNumberID,
                                    Number_slave = CNumbers[i2].Number1, //db.Number.Local.Where(P => P.ID == i2).First().Number1,
                                    Connection_type = (byte)(connectiont),
                                    C_Count = Ccount,
                                    C_Date = ThisDate,
                                    ID_Expenses = SecID,
                                });
                                /*db.Expenses.Local.Add(new Expenses()
                                {
                                    ID = SecID,
                                    C_Date = ThisDate,
                                    ID_number = ThisNumberID,
                                    C_type = 4,
                                    Expense = Ccount * TarifExpence,
                                });
                                db.Calling.Local.Add(new Calling()
                                {
                                    ID = MainID,
                                    ID_number_host = ThisNumberID,
                                    Number_slave = CNumbers[i2].Number1, //db.Number.Local.Where(P => P.ID == i2).First().Number1,
                                    Connection_type = (byte)(connectiont),
                                    C_Count = Ccount,
                                    C_Date = ThisDate,
                                    ID_Expenses = SecID,
                                });*/
                            }
                            for (int i = 0; i < SMSCount; i++)
                            {
                                decimal TarifExpence = TarifExpence = ThisTarif.sms_price;
                                int connectiont = r.Next(0, 3);

                                int i2 = r.Next(count / 2, count);
                                int MainID = GlobalSMSID++,//db.Calling.Local.Count,
                                    SecID = GloblExpencesID++;// db.Expenses.Local.Count;
                                DateTime ThisDate = DateTime.Now;
                                ThisDate = ThisDate.AddMonths(StartMounth - ThisDate.Month);
                                ThisDate = ThisDate.AddDays(StartDay - ThisDate.Day);

                                ThisDate = ThisDate.AddHours(GenInt(0, 24));
                                ThisDate = ThisDate.AddMinutes(GenInt(0, 60));
                                ThisDate = ThisDate.AddSeconds(GenInt(0, 60));

                                Added_Expenses.Add(new Expenses()
                                {
                                    ID = SecID,
                                    C_Date = ThisDate,
                                    ID_number = ThisNumberID,
                                    C_type = 5,
                                    Expense = TarifExpence,

                                });
                                Added_SMS.Add(new SMS()
                                {
                                    ID = MainID,
                                    ID_number_host = ThisNumberID,
                                    Number_slave = CNumbers[i2].Number1,//db.Number.Local.Where(P => P.ID == i2).First().Number1,
                                    Connection_type = (byte)(connectiont),
                                    C_Data = "" + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 999999),
                                    C_Date = ThisDate,
                                    ID_Expenses = SecID,
                                });
                                /*db.Expenses.Local.Add(new Expenses()
                                {
                                    ID = SecID,
                                    C_Date = ThisDate,
                                    ID_number = ThisNumberID,
                                    C_type = 5,
                                    Expense = TarifExpence,

                                });*/
                                /*db.SMS.Local.Add(new SMS()
                                {
                                    ID = MainID,
                                    ID_number_host = ThisNumberID,
                                    Number_slave = CNumbers[i2].Number1,//db.Number.Local.Where(P => P.ID == i2).First().Number1,
                                    Connection_type = (byte)(connectiont),
                                    C_Data = "" + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 999999),
                                    C_Date = ThisDate,
                                    ID_Expenses = SecID,
                                });*/
                            }
                            for (int i = 0; i < InternetCount; i++)
                            {
                                decimal TarifExpence = TarifExpence = ThisTarif.internet_price;
                                int connectiont = r.Next(0, 3);
                                int Ccount = r.Next(1, 20);
                                decimal DData = r.Next(1, 2000);
                                int i2 = r.Next(count / 2, count);
                                int MainID = GlobalInternetID++,//db.Calling.Local.Count,
                                    SecID = GloblExpencesID++;// db.Expenses.Local.Count;
                                DateTime ThisDate = DateTime.Now;
                                ThisDate = ThisDate.AddMonths(StartMounth - ThisDate.Month);
                                ThisDate = ThisDate.AddDays(StartDay - ThisDate.Day);

                                ThisDate = ThisDate.AddHours(GenInt(0, 24));
                                ThisDate = ThisDate.AddMinutes(GenInt(0, 60));
                                ThisDate = ThisDate.AddSeconds(GenInt(0, 60));

                                Added_Expenses.Add(new Expenses()
                                {
                                    ID = SecID,
                                    C_Date = ThisDate,
                                    ID_number = ThisNumberID,
                                    C_type = 6,
                                    Expense = DData / 1000 * TarifExpence,

                                });
                                Added_Internet.Add(new Internet()
                                {
                                    ID = MainID,
                                    C_Data = DData,
                                    C_Date = ThisDate,
                                    ID_Expenses = SecID
                                });

                                /*db.Expenses.Local.Add(new Expenses()
                                {
                                    ID = SecID,
                                    C_Date = ThisDate,
                                    ID_number = ThisNumberID,
                                    C_type = 6,
                                    Expense = DData / 1000 * TarifExpence,

                                });
                                db.Internet.Local.Add(new Internet()
                                {
                                    ID = MainID,
                                    C_Data = DData,
                                    C_Date = ThisDate,
                                    ID_Expenses = SecID
                                });*/
                            }
                        }
                    }
                }
                Console.WriteLine($"Время на генерацию {DateTimeOffset.UtcNow.ToUnixTimeSeconds() - StartTime.ToUnixTimeSeconds()} сек с кол-вом обьектов {Added_SMS.Count+ Added_Calling.Count+ Added_Expenses.Count+ Added_Internet.Count}");
                //var i = System.Data.Entity.Infrastructure.DbContextConfiguration;
                
                {
                    int SpecID = db.Client.Local.Count;
                    db.Client.Local.Add(new Client()
                    {
                        ID = SpecID,
                        C_type = 0,
                        C_Login = "123",
                        C_Password = "123"
                    });
                    db.Individual.Local.Add(new Individual()
                    {
                        ID = db.Individual.Local.Count,
                        ClientID = SpecID,
                        FirstName = "Юппи",
                        LastName = "Агненко",
                        Patronymic = "-",
                        Passport = r.Next(1000, 9999) + "" + r.Next(100000, 999999)
                    });

                    db.Number.Local.Add(new Number()
                    {
                        ID = db.Number.Local.Count,
                        Bill = r.Next(0, 10000) + r.Next(0, 99) / 100,
                        ID_Client = SpecID,
                        TarifDate = DateTime.Now,
                        ID_Tarif = 0,
                        C_status = 1,
                        Number1 = "+7980" + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9)
                    });

                    var ThisTarif = CTarifs[0];
                    var ThisNum = db.Number.Local.Where(P => P.ID_Client == SpecID).First().Number1;
                    for (int i = 0; i < count / 2; i++)
                    {
                        int i1 = r.Next(0, count / 2 - 1);
                        switch (i % 2)
                        {
                            case 0:
                                {
                                    decimal TarifExpence = 0;
                                    int connectiont = r.Next(0, 3);
                                    int Ccount = r.Next(1, 7);
                                    switch (connectiont)
                                    {
                                        default:
                                        case 0:
                                            TarifExpence = ThisTarif.call_price_inCity;
                                            break;
                                        case 1:
                                            TarifExpence = ThisTarif.call_price_outCity;
                                            break;
                                        case 2:
                                            TarifExpence = ThisTarif.call_price_outContry;
                                            break;
                                    }

                                    int i2 = r.Next(count / 2, count);
                                    int MainID = GlobalCallingID++,// db.Calling.Local.Count,
                                        SecID = GloblExpencesID++;// db.Expenses.Local.Count;
                                    DateTime ThisDate = DateTime.Now;
                                    ThisDate = ThisDate.AddMonths(GenInt(Months, DateTime.Now.Month) - ThisDate.Month);
                                    ThisDate = ThisDate.AddDays(GenInt(0, 28) - ThisDate.Day);

                                    ThisDate = ThisDate.AddHours(GenInt(0, 24));
                                    ThisDate = ThisDate.AddMinutes(GenInt(0, 60));
                                    ThisDate = ThisDate.AddSeconds(GenInt(0, 60));

                                    Added_Expenses.Add(new Expenses()
                                    {
                                        ID = SecID,
                                        C_Date = ThisDate,
                                        ID_number = SpecID,
                                        C_type = 4,
                                        Expense = Ccount * TarifExpence,
                                    });
                                    Added_Calling.Add(new Calling()
                                    {
                                        ID = MainID,
                                        ID_number_host = SpecID,
                                        Number_slave = CNumbers[i1].Number1,
                                        Connection_type = (byte)(connectiont),
                                        C_Count = Ccount,
                                        C_Date = ThisDate,
                                        ID_Expenses = SecID,
                                    });
                                }
                                break;
                            case 1:
                                {
                                    decimal TarifExpence = 0;
                                    int connectiont = r.Next(0, 3);
                                    int Ccount = r.Next(1, 7);
                                    switch (connectiont)
                                    {
                                        default:
                                        case 0:
                                            TarifExpence = ThisTarif.call_price_inCity;
                                            break;
                                        case 1:
                                            TarifExpence = ThisTarif.call_price_outCity;
                                            break;
                                        case 2:
                                            TarifExpence = ThisTarif.call_price_outContry;
                                            break;
                                    }

                                    int i2 = r.Next(count / 2, count);
                                    int MainID = GlobalCallingID++,// db.Calling.Local.Count,
                                        SecID = GloblExpencesID++;// db.Expenses.Local.Count;
                                    DateTime ThisDate = DateTime.Now;
                                    ThisDate = ThisDate.AddMonths(GenInt(Months, DateTime.Now.Month) - ThisDate.Month);
                                    ThisDate = ThisDate.AddDays(GenInt(0, 28) - ThisDate.Day);

                                    ThisDate = ThisDate.AddHours(GenInt(0, 24));
                                    ThisDate = ThisDate.AddMinutes(GenInt(0, 60));
                                    ThisDate = ThisDate.AddSeconds(GenInt(0, 60));

                                    Added_Expenses.Add(new Expenses()
                                    {
                                        ID = SecID,
                                        C_Date = ThisDate,
                                        ID_number = SpecID,
                                        C_type = 4,
                                        Expense = Ccount * TarifExpence,
                                    });
                                    Added_Calling.Add(new Calling()
                                    {
                                        ID = MainID,
                                        ID_number_host = i1,
                                        Number_slave = ThisNum,
                                        Connection_type = (byte)(connectiont),
                                        C_Count = Ccount,
                                        C_Date = ThisDate,
                                        ID_Expenses = SecID,
                                    });
                                }
                                break;
                        }

                    }
                    for (int i = 0; i < count / 2; i++)
                    {
                        int i1 = r.Next(0, count / 2 - 1);
                        switch (i % 2)
                        {
                            case 0:
                                {
                                    decimal TarifExpence = TarifExpence = ThisTarif.sms_price;
                                    int connectiont = r.Next(0, 3);

                                    int i2 = r.Next(count / 2, count);
                                    int MainID = GlobalSMSID++,//db.Calling.Local.Count,
                                        SecID = GloblExpencesID++;// db.Expenses.Local.Count;
                                    DateTime ThisDate = DateTime.Now;
                                    ThisDate = ThisDate.AddMonths(GenInt(Months, DateTime.Now.Month) - ThisDate.Month);
                                    ThisDate = ThisDate.AddDays(GenInt(0, 28) - ThisDate.Day);

                                    ThisDate = ThisDate.AddHours(GenInt(0, 24));
                                    ThisDate = ThisDate.AddMinutes(GenInt(0, 60));
                                    ThisDate = ThisDate.AddSeconds(GenInt(0, 60));

                                    Added_Expenses.Add(new Expenses()
                                    {
                                        ID = SecID,
                                        C_Date = ThisDate,
                                        ID_number = SpecID,
                                        C_type = 5,
                                        Expense = TarifExpence,

                                    });
                                    Added_SMS.Add(new SMS()
                                    {
                                        ID = MainID,
                                        ID_number_host = SpecID,
                                        Number_slave = CNumbers[i1].Number1,
                                        Connection_type = (byte)(connectiont),
                                        C_Date = ThisDate,
                                        C_Data = "" + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 999999)
                                    });
                                }
                                break;
                            case 1:
                                {
                                    decimal TarifExpence = TarifExpence = ThisTarif.sms_price;
                                    int connectiont = r.Next(0, 3);

                                    int i2 = r.Next(count / 2, count);
                                    int MainID = GlobalSMSID++,//db.Calling.Local.Count,
                                        SecID = GloblExpencesID++;// db.Expenses.Local.Count;
                                    DateTime ThisDate = DateTime.Now;
                                    ThisDate = ThisDate.AddMonths(GenInt(Months, DateTime.Now.Month) - ThisDate.Month);
                                    ThisDate = ThisDate.AddDays(GenInt(0, 28) - ThisDate.Day);

                                    ThisDate = ThisDate.AddHours(GenInt(0, 24));
                                    ThisDate = ThisDate.AddMinutes(GenInt(0, 60));
                                    ThisDate = ThisDate.AddSeconds(GenInt(0, 60));

                                    Added_Expenses.Add(new Expenses()
                                    {
                                        ID = SecID,
                                        C_Date = ThisDate,
                                        ID_number = SpecID,
                                        C_type = 5,
                                        Expense = TarifExpence,

                                    });
                                    Added_SMS.Add(new SMS()
                                    {
                                        ID = MainID,
                                        ID_number_host = i1,
                                        Number_slave = ThisNum,
                                        Connection_type = (byte)(connectiont),
                                        C_Date = ThisDate,
                                        C_Data = "" + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 999999)
                                    });

                                }
                                break;
                        }
                    }
                }

                db.Expenses.AddRange(Added_Expenses);
                //Added_Expenses.Clear();
                db.SMS.AddRange(Added_SMS);
                //Added_SMS.Clear();
                db.Calling.AddRange(Added_Calling);
                //Added_Calling.Clear();
                db.Internet.AddRange(Added_Internet);
                //Added_Internet.Clear();

               /* foreach (var item in Added_SMS)
                {
                    db.SMS.Local.Add(item);
                }
                foreach (var item in Added_Calling)
                {
                    db.Calling.Local.Add(item);
                }
                foreach (var item in Added_Expenses)
                {
                    db.Expenses.Local.Add(item);
                }
                foreach (var item in Added_Internet)
                {
                    db.Internet.Local.Add(item);
                }*/
            }
            /*
             * 1-Звонки в тарифе | бесплатно
             * 2-СМС в тарифе | бесплатно
             * 3-Трафик в тарифе | бесплатно
             * 
             * 4-Звонки вне тарифа | платно
             * 5-СМС вне тарифа | платно
             * 6-Трафик вне тарифа | платно
             * 
             * 8-Подключение/смена тарифа | платно
             * 9-Подключение услуги | платно
             * 
             * 10-Плата за услугу в месяц | платно
             * 11-Плата за тариф в месяц | платно
             */
            /*int ID = db.Calling.Local.Count;
            for (int i = 0; i < count / 2; i++)
            {
                int i1 = r.Next(0, count / 2 - 1), i2 = r.Next(count / 2, count);
                db.Calling.Local.Add(new Calling()
                {
                    ID = ID++,
                    ID_number_host = i1,
                    Number_slave = db.Number.Local.Where(P => P.ID == i2).First().Number1,
                    Connection_type = (byte)(r.Next(0, 3)),
                    C_Count = (r.Next(1, 60)),
                    C_Date = DateTime.Now
                });
            }
            ID = db.SMS.Local.Count;
            for (int i = 0; i < count / 2; i++)
            {
                int i1 = r.Next(0, count / 2 - 1), i2 = r.Next(count / 2, count);
                db.SMS.Local.Add(new SMS()
                {
                    ID = ID++,
                    ID_number_host = i1,
                    Number_slave = db.Number.Local.Where(P => P.ID == i2).First().Number1,
                    Connection_type = (byte)(r.Next(0, 3)),
                    C_Date = DateTime.Now,
                    C_Data = "" + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 999999)
                });
            }*/

            

            CheckUpMonthly_remains_tarif();
            Save();

            LastNames.Clear();
            FirstNames.Clear();
            Patronomics.Clear();
            Tarifs.Clear();
            Somethings.Clear();
            Adreeses.Clear();
        }

    }
}
