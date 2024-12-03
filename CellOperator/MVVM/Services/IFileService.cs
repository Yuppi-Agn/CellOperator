using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

using BLL.Services;
using BLL.Models;
using System.Collections.ObjectModel;

using System.IO;

namespace CellOperator.MVVM.Services
{
    public interface IFileService
    {
        void Save(List<Methods.Report_Calling> Callings);
        void Save(List<Methods.Report_SMS> SMS);
        void Save(List<ExpensesDTO> Expenses);
        //List<Phone> Open(string filename);
        //void Save(string filename, List<Phone> phonesList);
    }
    public interface IDialogService
    {
        //void ShowMessage();   // показ сообщения
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialog();  // открытие файла
        bool SaveFileDialog();  // сохранение файла
    }
    public class WindowsDilalogService : IDialogService
    {
        Microsoft.Win32.SaveFileDialog SaveDialog = new Microsoft.Win32.SaveFileDialog();
        Microsoft.Win32.OpenFileDialog Openialog = new Microsoft.Win32.OpenFileDialog();

        //dlg.FileName = "Document"; // Default file name
        //dlg.DefaultExt = ".txt"; // Default file extension
        //dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

        public string FilePath { get; set; }

        /// <summary>
        /// FileName = "Document" -> Default file name.
        /// Extension = ".txt" -> Default file extension.
        /// FilterName = "Text documents" -> Filter files by extension.
        /// </summary>
        public WindowsDilalogService(string FileName, string Extension, string FilterName)
        {
            SaveDialog.FileName = FileName;
            Openialog.FileName = FileName;

            SaveDialog.DefaultExt = Extension;
            Openialog.DefaultExt = Extension;

            string Filter = FilterName.Trim() + "|" + '*'+Extension;
            SaveDialog.Filter = Filter;
            Openialog.Filter = Filter;
        }
        public WindowsDilalogService()
        {

        }
        /*public void ShowMessage()
        {
            //SaveDialog.ShowDialog();
        }*/
        public bool OpenFileDialog()
        {
            var result = Openialog.ShowDialog();

            if (result == true)
            {
                FilePath = Openialog.FileName;
                return true;
            }
            else return false;
        }
        public bool SaveFileDialog()
        {
            var result = SaveDialog.ShowDialog();

            if(result == true)
            {
                FilePath = SaveDialog.FileName;
                return true;
            }
            else return false;
        }
    }
    public class FileService_csv : IFileService
    {
        IDialogService dialogService;
        string Path="";
        public FileService_csv()
        {
            dialogService = new WindowsDilalogService("Введите название", "csv", "Таблица csv");
        }
        public void Save(List<Methods.Report_Calling> Callings) {
            if (!FilePath()) return;

            string String = "";
            const string Separator = ",",
                StrSym = "\"";
            var stream = new StreamWriter(Path, false, Encoding.UTF8);

            String = StrSym+"Тип"+ StrSym;
            String += Separator + StrSym + "Иной номер" + StrSym;
            String += Separator + StrSym + "Дата" + StrSym;
            String += Separator + StrSym + "Время" + StrSym;
            String += Separator + StrSym + "Тип звонка" + StrSym;

            stream.WriteLine(String);

            foreach (var item in Callings)
            {
                //stream.WriteLine("\"{0:yyyy-MM-dd HH:mm:ss}\",\"{1}\"", DateTime.Now, this.textBox1.Text);
                String = StrSym + item.Type + StrSym;
                String += Separator + StrSym + item.OtherNumber.Trim() + StrSym;
                String += Separator + StrSym + item.Date.ToString() + StrSym;
                String += Separator + StrSym + item.Minutes + StrSym;
                String += Separator + StrSym + item.ConnectionType + StrSym;

                stream.WriteLine(String);
            }
            stream.Dispose();
        }
        public void Save(List<Methods.Report_SMS> SMS) {
            if (!FilePath()) return;

            string String = "";
            const string Separator = ",",
                StrSym = "\"";
            var stream = new StreamWriter(Path, false, Encoding.UTF8);

            String = StrSym + "Тип" + StrSym;
            String += Separator + StrSym + "Иной номер" + StrSym;
            String += Separator + StrSym + "Дата" + StrSym;
            String += Separator + StrSym + "Тип звонка" + StrSym;

            stream.WriteLine(String);

            foreach (var item in SMS)
            {
                String = StrSym + item.Type + StrSym;
                String += Separator + StrSym + item.OtherNumber.Trim() + StrSym;
                String += Separator + StrSym + item.Date.ToString() + StrSym;
                String += Separator + StrSym + item.ConnectionType + StrSym;

                stream.WriteLine(String);
            }
            stream.Dispose();
        }
        public void Save(List<ExpensesDTO> Expenses) {
            if (!FilePath()) return;

            string String = "";
            const string Separator = ",",
                StrSym = "\"";
            var stream = new StreamWriter(Path, false, Encoding.UTF8);
            
            String = StrSym + "Дата" + StrSym;
            String += Separator + StrSym + "Тип" + StrSym;
            String += Separator + StrSym + "Сумма" + StrSym;

            stream.WriteLine(String);

            foreach (var item in Expenses)
            {
                String = StrSym + item.Date.ToString() + StrSym;
                String += Separator + StrSym + item.Type + StrSym;
                String += Separator + StrSym + item.Expense + StrSym;

                stream.WriteLine(String);
            }
            stream.Dispose();
        }
        private bool FilePath()
        {
            if (dialogService.SaveFileDialog()) { 
                Path = dialogService.FilePath;
                return true;
            }
            else return false;
        }
    }

}
