using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using BLL.Models;

using System.IO;

namespace BLL.Services
{
    public class DataBase_service
    {
        EF db;
        List<string> LastNames = new List<string>();
        List<string> FirstNames = new List<string>();
        List<string> Patronomics = new List<string>();
        List<string> Tarifs = new List<string>();
        List<string> Somethings = new List<string>();
        public DataBase_service()
        {
            db = new EF();

            if (true)
            {
                StreamReader sr = new StreamReader("C:\\Users\\Yuppi\\source\\repos\\PO_Lab3\\Resources\\Firstname.txt");
                string line = sr.ReadLine();
                while (line != null)
                {
                    FirstNames.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader("C:\\Users\\Yuppi\\source\\repos\\PO_Lab3\\Resources\\Lastname.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    LastNames.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader("C:\\Users\\Yuppi\\source\\repos\\PO_Lab3\\Resources\\Patronomic.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Patronomics.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader("C:\\Users\\Yuppi\\source\\repos\\PO_Lab3\\Resources\\Tarif.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Tarifs.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader("C:\\Users\\Yuppi\\source\\repos\\PO_Lab3\\Resources\\Something.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Somethings.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
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
        public List<CallingDTO> GetAllCallings()
        {
            return db.Calling.ToList().Select(i => new CallingDTO(i)).ToList();
        }
        public List<NumberDTO> GetAllNumbers()
        {
            return db.Number.ToList().Select(i => new NumberDTO(i)).ToList();
        }
        public List<SMSDTO> GetAllSMS()
        {
            return db.SMS.ToList().Select(i => new SMSDTO(i)).ToList();
        }
        public List<TarifDTO> GetAllTarifs()
        {
            return db.Tarif.ToList().Select(i => new TarifDTO(i)).ToList();
        }
        public List<TarifDTO> GetAllTarifs(ClientDTO client)
        {
            return db.Tarif.ToList().Where(i=> i.C_type == client.C_type).Select(i => new TarifDTO(i)).ToList();
        }
        public List<ServiceDTO> GetAllServices()
        {
            return db.C_Service.ToList().Select(i => new ServiceDTO(i)).ToList();
        }
        public List<Service_ConnectionDTO> GetAllService_Connection()
        {
            return db.C_Service_Connection.ToList().Select(i => new Service_ConnectionDTO(i)).ToList();
        }
        public List<Tarif_HistoryDTO> GetAllTarif_history()
        {
            return db.Tarif_History.ToList().Select(i => new Tarif_HistoryDTO(i)).ToList();
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
        public void DeleteTarif(int ID)
        {
            Tarif It = db.Tarif.Find(ID);
            if (It != null)
            {
                db.Tarif.Remove(It);
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
                MSRN = C.MSRN,
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
                MSRN = C.MSRN,
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
            It.MSRN = C.MSRN;
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
        public void AddMoney(int ID, decimal Money)
        {
            Number It = db.Number.Find(ID);
            if (It != null)
            {
                It.Bill += Money;
                Save();
            }
        }
        private bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            else return false;
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
            if (Individual.Count() > 0) {
                var IndividualDT = Individual.Select(i => new IndividualDTO(i)).First();
                return IndividualDT.FirstName + " " + IndividualDT.LastName + " " + IndividualDT.Patronymic; 
            }
            
            return "Ups...";
        }
        public List<NumberDTO> FindNumbers(ClientDTO client)
        {
            var list = db.Number.Where(p => p.ID_Client == client.ID).AsEnumerable();
            if (list.Count() > 0) return list.Select(i => new NumberDTO(i)).ToList<NumberDTO>();
            else return new List<NumberDTO>();
        }
        public bool ChangeTarif_NumByClient(ClientDTO client, int TarifID, int NumID)
        {
            Tarif NewTarif= db.Tarif.Find(TarifID);
            Number ThisNum = db.Number.Find(NumID);
            if (NewTarif == null || ThisNum == null) return false;

            Tarif_History tarif_History = new Tarif_History();
            tarif_History.ID = db.Tarif_History.OrderBy(p => p.ID).ToList().Last().ID + 1;
            tarif_History.ID_Number = NumID;
            tarif_History.ID_Tarif = TarifID;
            tarif_History.StartDate = ThisNum.TarifDate;
            tarif_History.EndDate = DateTime.Now;
            db.Tarif_History.Add(tarif_History);

            ThisNum.ID_Tarif = NewTarif.ID;
            ThisNum.TarifDate = DateTime.Now;

            return Save();
        }
        public bool BuyNumber(ClientDTO client, int TarifID)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            bool IsExists = true;
            Number NewNumber = new Number();
            while (IsExists) {
                NewNumber = new Number()
                {
                    ID = db.Number.OrderBy(p => p.ID).ToList().Last().ID + 1,
                    Bill = 0,
                    ID_Client = client.ID,
                    TarifDate = DateTime.Now,
                    ID_Tarif = TarifID,
                    C_status = 1,
                    Number1 = "7980" + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9)
                };
                var List = db.Number.Where(p => p.Number1 == NewNumber.Number1).AsEnumerable();
                IsExists = List.Count() > 0;
            }
            db.Number.Add(NewNumber);
            Save();
            return false;
        }
        public void GenerateMembers(int count)
        {
            Random r = new Random(DateTime.Now.Millisecond);
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
                            C_Login = (58 + r.Next(0, 9999) + r.Next(0, 9999) * 69 * 356).ToString(),
                            C_Password = (434 + r.Next(0, 9999) + r.Next(0, 9999) * 123 * 25).ToString()
                        });
                        db.LegalEntity.Add(new LegalEntity()
                        {
                            ID = i / 2,
                            ClientID = i,
                            Adress = "",
                            IdividualTaxpayerNumber = r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + "",
                            MSRN = r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + "",
                            OrganizationName = Somethings[r.Next(0, Somethings.Count - 1)]
                        });
                        break;
                }
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
                db.Number.Add(new Number()
                {
                    ID = i,
                    Bill = r.Next(0, 10000) + r.Next(0, 99) / 100,
                    ID_Client = i,
                    TarifDate = DateTime.Now,
                    ID_Tarif = i,
                    C_status = 1,
                    Number1 = "7980" + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9)
                });
                db.C_Service.Add(new C_Service()
                {
                    ID = i,
                    Name = FirstNames[r.Next(0, FirstNames.Count - 1)],
                    Price = r.Next(0, 10000) + r.Next(0, 99) / 100
                });
            }
            for (int i = 0; i < count / 2; i++)
            {
                int i1 = r.Next(0, count / 2 - 1), i2 = r.Next(count / 2, count);
                db.Calling.Add(new Calling()
                {
                    ID = i,
                    ID_number_host = i1,
                    ID_number_slave = i2,
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
                    ID_number_slave = i2,
                    Connection_type = (byte)(r.Next(0, 3)),
                    C_Date = DateTime.Now,
                    C_Data = "" + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 9999) + r.Next(0, 999999)
                });
            }
            Save();
        }
    }
}
