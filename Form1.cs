using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace Test_Licko
{
    public partial class Form1 : Form
    {
        private Database db = new Database();

        public Form1()
        {
            InitializeComponent();
            LoadData(); // Načtení dat při inicializaci
        }

        private void LoadData()
        {
            List<Person> osoby = db.loadDataPersons(); // Načtení osob
            List<Company> firmy = db.loadDataCompanies(); // Načtení firem

            gridControl1.DataSource = osoby; // Nastavení zdroje dat pro grid osob
            gridControl1.MainView.PopulateColumns();
            gridControl2.DataSource = firmy; // Nastavení zdroje dat pro grid firem
            gridControl2.MainView.PopulateColumns();

            var view = gridControl1.MainView as GridView;
            SetupPersonGrid(view); // Nastavení gridu pro osoby

            var view2 = gridControl2.MainView as GridView;
            SetupCompanyGrid(view2); // Nastavení gridu pro firmy
        }

        private void SetupPersonGrid(GridView view)
        {
            // Nastavení sloupců pro grid osob
            view.Columns["ID"].Visible = false;
            view.Columns["Name"].Caption = "Jméno";
            view.Columns["Name"].Width = 150;
            view.Columns["Surname"].Caption = "Příjmení";
            view.Columns["Surname"].Width = 150;
            view.Columns["Date"].Caption = "Datum Narození";
            view.Columns["Date"].Width = 150;
            view.Columns["Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            view.Columns["Phone"].Caption = "Telefon";
            view.Columns["Phone"].Width = 150;
            view.Columns["Email"].Caption = "Email";
            view.Columns["Email"].Width = 150;

            view.FocusedRowChanged += ViewPersons_FocusedRowChangedd; // Ošetření změny vybrané řádky
            view.OptionsBehavior.Editable = false; // Zakázání editace
            ViewPersons_FocusedRowChangedd(null, null); // Načtení prvního řádku
            view.ClearSelection(); // Zrušení výběru
        }

        private void SetupCompanyGrid(GridView view2)
        {
            // Nastavení sloupců pro grid firem
            view2.Columns["ID"].Visible = false;
            view2.Columns["Name"].Caption = "Company";
            view2.Columns["Name"].Width = 150;
            view2.Columns["Ico"].Caption = "IČO";
            view2.Columns["Ico"].Width = 150;
            view2.Columns["Phone"].Caption = "Telefon";
            view2.Columns["Phone"].Width = 150;
            view2.Columns["Email"].Width = 150;
            view2.Columns["Email"].Caption = "Email";

            view2.FocusedRowChanged += ViewCompanies_FocusedRowChanged; // Ošetření změny vybrané řádky
            view2.OptionsBehavior.Editable = false; // Zakázání editace
            ViewCompanies_FocusedRowChanged(null, null); // Načtení prvního řádku
            view2.ClearSelection(); // Zrušení výběru
        }

        private void addPerson(object sender, EventArgs e)
        {
            // Vytvoření nové osoby
            bool status = db.insertPerson(PNameEdit.Text, PSurnameEdit.Text, PDateEdit.DateTime, PPhoneEdit.Text, PEmailEdit.Text);
            if(status){
                this.ClearPersonInputs();
                LoadData(); // Načtení dat
            }
        }

        private void addCompany(object sender, EventArgs e)
        {
            // Vytvoření nové firmy
            bool status = db.insertCompany(CNameEdit.Text, CIcoEdit.Text, CPhoneEdit.Text, CEmailEdit.Text);
              if(status){
                this.ClearCompanyInputs();
                LoadData(); // Načtení dat
            }
        }

        private void editPerson(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            int selectedRowHandle = view.FocusedRowHandle;

            if (selectedRowHandle >= 0) // Ověření platného řádku
            {
                // Získání hodnot a editace osoby
                var id = (int)view.GetRowCellValue(selectedRowHandle, "ID");
                var name = PNameEdit_e.Text;
                var surname = PSurnameEdit_e.Text;
                var date = dateEdit_e.DateTime;
                var phone = PPhoneEdit_e.Text;
                var email = PEmailEdit_e.Text;

                db.editPerson(id, name, surname, date, phone, email);
            }

            LoadData(); // Načtení dat
        }

        private void editCompany(object sender, EventArgs e)
        {
            var view = gridControl2.MainView as GridView;
            int selectedRowHandle = view.FocusedRowHandle;

            if (selectedRowHandle >= 0) // Ověření platného řádku
            {
                // Získání hodnot a editace firmy
                var id = (int)view.GetRowCellValue(selectedRowHandle, "ID");
                var name = CNameEdit_e.Text;
                var ico = CIcoEdit_e.Text;
                var phone = CPhoneEdit_e.Text;
                var email = CEmailEdit_e.Text;

                db.editCompany(id, name, ico, phone, email);
            }

            LoadData(); // Načtení dat
        }

        private void ViewPersons_FocusedRowChangedd(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            int selectedRowHandle = view.FocusedRowHandle;

            if (selectedRowHandle >= 0) // Ověření platného řádku
            {
                // Načtení dat do editačních polí
                PNameEdit_e.Text = (string)view.GetRowCellValue(selectedRowHandle, "Name");
                PSurnameEdit_e.Text = (string)view.GetRowCellValue(selectedRowHandle, "Surname");
                dateEdit_e.DateTime = (DateTime)view.GetRowCellValue(selectedRowHandle, "Date");
                PPhoneEdit_e.Text = (string)view.GetRowCellValue(selectedRowHandle, "Phone");
                PEmailEdit_e.Text = (string)view.GetRowCellValue(selectedRowHandle, "Email");
            }
        }

        private void ViewCompanies_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = gridControl2.MainView as GridView;
            int selectedRowHandle = view.FocusedRowHandle;

            if (selectedRowHandle >= 0) // Ověření platného řádku
            {
                // Načtení dat do editačních polí pro firmy
                CNameEdit_e.Text = (string)view.GetRowCellValue(selectedRowHandle, "Name");
                CIcoEdit_e.Text = (string)view.GetRowCellValue(selectedRowHandle, "Ico");
                CPhoneEdit_e.Text = (string)view.GetRowCellValue(selectedRowHandle, "Phone");
                CEmailEdit_e.Text = (string)view.GetRowCellValue(selectedRowHandle, "Email");
            }
        }

        private void deletePerson(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            int selectedRowHandle = view.FocusedRowHandle;

            if (selectedRowHandle >= 0) // Ověření platného řádku
            {
                var id = (int)view.GetRowCellValue(selectedRowHandle, "ID");
                db.RemovePerson(id); // Mazání osoby
            }

            LoadData(); // Načtení dat
        }

        private void deleteCompany(object sender, EventArgs e)
        {
            var view = gridControl2.MainView as GridView;
            int selectedRowHandle = view.FocusedRowHandle;

            if (selectedRowHandle >= 0) // Ověření platného řádku
            {
                var id = (int)view.GetRowCellValue(selectedRowHandle, "ID");
                db.RemoveCompany(id); // Mazání firmy
            }

            LoadData(); // Načtení dat
        }

        private void ClearPersonInputs(){
            this.PNameEdit.Text = "";
            this.PSurnameEdit.Text = "";
            this.PEmailEdit.Text = "";
            this.PPhoneEdit.Text = "";
            this.PDateEdit.EditValue = null;
        }

        
        private void ClearCompanyInputs(){
            this.CNameEdit.Text = "";
            this.CIcoEdit.Text = "";
            this.CEmailEdit.Text = "";
            this.CPhoneEdit.Text = "";
        }

    }
}
