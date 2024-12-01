using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.Models
{
    public class CallingDTO
    {
        public int ID { get; set; }
        public int? ID_number_host { get; set; }
        public string number_host { get; set; }
        public int? ID_number_slave { get; set; }
        public string number_slave { get; set; }
        public DateTime? Date { get; set; }
        public int? Count { get; set; }
        public int? Connection_type { get; set; }
        public string type { get; set; }
        public CallingDTO() { }
        public CallingDTO(Calling p)
        {
            number_host = p.Number.Number1.Trim();
            number_slave = p.Number1.Number1.Trim();
            ID = p.ID;
            ID_number_host = p.ID_number_host;
            ID_number_slave = p.ID_number_slave;
            Date = p.C_Date;
            Count = p.C_Count;
            Connection_type = p.Connection_type;
            switch (Connection_type)
            {
                case 0:
                    type = "По городу";
                    break;
                case 1:
                    type = "Между городами";
                    break;
                case 2:
                    type = "Международный";
                    break;
            }
        }

    }
    public class SMSDTO
    {
        public int ID { get; set; }
        public int? ID_number_host { get; set; }
        public string number_host { get; set; }
        public int? ID_number_slave { get; set; }
        public string number_slave { get; set; }
        public DateTime? Date { get; set; }
        public string Data { get; set; }
        public int? Connection_type { get; set; }
        public string type { get; set; }

        public SMSDTO() { }
        public SMSDTO(SMS p)
        {
            ID = p.ID;
            ID_number_host = p.ID_number_host;
            ID_number_slave = p.ID_number_slave;
            Date = p.C_Date;
            Data = p.C_Data.Trim();
            Connection_type = p.Connection_type;

            number_host = p.Number.Number1.Trim();
            number_slave = p.Number1.Number1.Trim();
            switch (Connection_type)
            {
                case 0:
                    type = "По городу";
                    break;
                case 1:
                    type = "Между городами";
                    break;
                case 2:
                    type = "Международный";
                    break;
            }
        }
    }
    public class InternetDTO
    {
        public int ID { get; set; }
        public int? ID_number { get; set; }
        public DateTime? C_Date { get; set; }
        public decimal? C_Data { get; set; }
        public InternetDTO(Internet p)
        {
            ID = p.ID;
            ID_number = p.ID_number;
            C_Date = p.C_Date;
            C_Data = p.C_Data;
        }
    }
    public class ClientDTO
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public byte? C_type { get; set; }

        public ClientDTO() { }
        public ClientDTO(Client p)
        {
            //EF db = new EF();
            ID = p.ID;
            C_type = p.C_type;
            if (p.C_type == 0) Type = "Физическое лицо"; else Type = "Юридическое лицо";
            Login = p.C_Login.Trim();
            Password = p.C_Password.Trim();
        }
    }
    public class AdministratorDTO
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public byte? C_type { get; set; }

        public AdministratorDTO() { }
        public AdministratorDTO(Administrator p)
        {
            ID = p.ID;
            Login = p.C_Login.Trim();
            Password = p.C_Password.Trim();
        }
    }

    public class Client_LegalEntityDTO
    {
        public int ID { get; set; }
        public string OrganizationName { get; set; }
        public string MSRN { get; set; }
        public string IdividualTaxpayerNumber { get; set; }
        public string Adress { get; set; }
        public int? ClientID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public byte? C_type { get; set; }

        public Client_LegalEntityDTO() { }
        public Client_LegalEntityDTO(LegalEntity p)
        {
            ID = p.ID;
            OrganizationName = p.OrganizationName.Trim();
            MSRN = p.MSRN.Trim();
            IdividualTaxpayerNumber = p.IdividualTaxpayerNumber.Trim();
            Adress = p.Adress.Trim();
            ClientID = p.ClientID;

            C_type = p.Client.C_type;
            if (p.Client.C_type == 0) Type = "Физическое лицо"; else Type = "Юридическое лицо";
            Login = p.Client.C_Login.Trim();
            Password = p.Client.C_Password.Trim();
        }
    }
    public class Client_IndividualDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Passport { get; set; }
        public int? ClientID { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public byte? C_type { get; set; }
        public Client_IndividualDTO() { }
        public Client_IndividualDTO(Individual p)
        {
            ID = p.ID;
            FirstName = p.FirstName.Trim();
            LastName = p.LastName.Trim();
            Patronymic = p.Patronymic.Trim();
            Passport = p.Passport.Trim();
            ClientID = p.ClientID;

            C_type = p.Client.C_type;
            if (p.Client.C_type == 0) Type = "Физическое лицо"; else Type = "Юридическое лицо";
            Login = p.Client.C_Login.Trim();
            Password = p.Client.C_Password.Trim();
        }
    }
    public class TarifDTO
    {
        public int ID { get; set; }
        public decimal Price { get; set; }
        public int SMS_amount { get; set; }
        public int MINUTES_amount { get; set; }
        public int Internet_amount { get; set; }
        public decimal call_price_inCity { get; set; }
        public decimal call_price_outCity { get; set; }
        public decimal call_price_outContry { get; set; }
        public decimal sms_price { get; set; }
        public decimal internet_price { get; set; }
        public string name { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }

        public TarifDTO() { }
        public TarifDTO(Tarif p)
        {
            ID = p.ID;
            Price = p.Price;
            SMS_amount = p.SMS_amount;
            MINUTES_amount = p.MINUTES_amount;
            Internet_amount = p.Internet_amount;
            call_price_inCity = p.call_price_inCity;
            call_price_outCity = p.call_price_outCity;
            call_price_outContry = p.call_price_outContry;
            sms_price = p.sms_price;
            internet_price = p.internet_price;
            name = p.name.Trim();
            if (p.C_type == 0) Type = "Физическое лицо"; else Type = "Юридическое лицо";
            if (p.C_status == 0) Status = false; else Status = true;
        }
    }
    public class NumberDTO
    {
        public int ID { get; set; }
        public int ID_Tarif { get; set; }
        public string Tarif { get; set; }
        public int? ID_Client { get; set; }
        public string Number { get; set; }
        public decimal Bill { get; set; }
        public DateTime TarifDate { get; set; }
        public bool Status { get; set; }


        public NumberDTO() { }
        public NumberDTO(Number p)
        {
            Tarif = p.Tarif.name.Trim();
            ID = p.ID;
            ID_Tarif = p.ID_Tarif;
            ID_Client = p.ID_Client;
            Number = p.Number1.Trim();
            Bill = p.Bill;
            TarifDate = p.TarifDate;
            if (p.C_status == 0) Status = false; else Status = true;
        }
    }
    public class LegalEntityDTO
    {
        public int ID { get; set; }
        public string OrganizationName { get; set; }
        public string MSRN { get; set; }
        public string IdividualTaxpayerNumber { get; set; }
        public string Adress { get; set; }
        public int? ClientID { get; set; }
        public LegalEntityDTO() { }
        public LegalEntityDTO(LegalEntity p)
        {
            ID = p.ID;
            OrganizationName = p.OrganizationName.Trim();
            MSRN = p.MSRN.Trim();
            IdividualTaxpayerNumber = p.IdividualTaxpayerNumber.Trim();
            Adress = p.Adress.Trim();
            ClientID = p.ClientID;
        }
    }
    public class IndividualDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Passport { get; set; }
        public int? ClientID { get; set; }
        public IndividualDTO() { }
        public IndividualDTO(Individual p)
        {
            ID = p.ID;
            FirstName = p.FirstName.Trim();
            LastName = p.LastName.Trim();
            Patronymic = p.Patronymic.Trim();
            Passport = p.Passport.Trim();
            ClientID = p.ClientID;
        }
    }
    public class Tarif_HistoryDTO
    {
        public int ID { get; set; }
        public int ID_Tarif { get; set; }
        public int ID_Number { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Number { get; set; }
        public string Tarif { get; set; }
        public Tarif_HistoryDTO(Tarif_History p)
        {
            ID = p.ID;
            ID_Tarif = p.ID_Tarif;
            ID_Number = p.ID_Number;
            StartDate = p.StartDate;
            EndDate = p.EndDate;
            Number = p.Number.Number1.Trim();
            Tarif = p.Tarif.name.Trim();
        }
    }
    public class ServiceDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public ServiceDTO(C_Service p)
        {
            ID = p.ID;
            Name = p.Name.Trim();
            Price = p.Price;
        }
    }
    public class Service_ConnectionDTO
    {
        public int ID { get; set; }
        public int? ID_number { get; set; }
        public int? ID_Service { get; set; }
        public DateTime? C_Date { get; set; }
        public byte? C_status { get; set; }
        public string Number { get; set; }
        public string Service { get; set; }
        public Service_ConnectionDTO(C_Service_Connection p)
        {
            ID = p.ID;
            ID_number = p.ID_number;
            ID_Service = p.ID_Service;
            C_Date = p.C_Date;
            C_status = p.C_status;
            Number = p.Number.Number1.Trim();
            Service = p.C_Service.Name.Trim();
        }

    }
    public class ExpensesDTO
    {
        public ExpensesDTO(Expenses p)
        {
            ID = p.ID;
            ID_number = p.ID_number;
            Date = p.C_Date;
            Expense = p.Expense;
            type = p.C_type;
            Calling = p.Calling;
            Number = p.Number;
            Internet = p.Internet;
            SMS = p.SMS;
        }

        public int ID { get; set; }

        public int? ID_number { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Expense { get; set; }

        public byte? type { get; set; }

        public virtual ICollection<Calling> Calling { get; set; }

        public virtual Number Number { get; set; }

        public virtual ICollection<Internet> Internet { get; set; }
        public virtual ICollection<SMS> SMS { get; set; }
    }
    public partial class Monthly_remains_tarifDTO
    {
        public Monthly_remains_tarifDTO(Monthly_remains_tarif p)
        {
            ID = p.ID;
            SMS_amount = p.SMS_amount;
            MINUTES_amount = p.MINUTES_amount;
        }
        public int ID { get; set; }

        public int SMS_amount { get; set; }

        public int MINUTES_amount { get; set; }

        public int Internet_amount { get; set; }
    }
}
