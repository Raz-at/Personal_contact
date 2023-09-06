using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register
{
    public partial class ContactList : Form
    {
        contact model = new contact();
        private int userID;
        private string userName;
        public ContactList(int UserID , string Username)
        {
            InitializeComponent();
            this.userID = UserID;
            this.userName = Username;
            
        }

        private void ContactList_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            this.ActiveControl = txtFirstName;
            clear();
            loadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            model.FirstName = txtFirstName.Text;
            model.LastName = txtLastName.Text;
            model.Email = txtEmail.Text;
            model.Address = txtAddress.Text;
            model.ContactNo = txtContactNo.Text;
            model.UserId = userID;

            using (RegisterEntities1 db = new RegisterEntities1())
            {
                if (model.ContactId == 0)
                {
                    db.contacts.Add(model);
                    db.SaveChanges();
                    MessageBox.Show("Contact Saved");
                }
                else
                {

                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            clear();
            loadData();


        }

        void clear()
        {
            txtFirstName.Text = txtAddress.Text = txtContactNo.Text = txtEmail.Text = txtLastName.Text = "";
            btnSave.Text = "Save";
            model.ContactId = 0;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();

        }

        void loadData()
        {
            dvgContact.AutoGenerateColumns = false;

            using (RegisterEntities1 db = new RegisterEntities1())
            {
                var matchingContacts = db.contacts.Where(c => c.UserId == userID).ToList();

                dvgContact.DataSource = matchingContacts;
                lblshow.Text = "Hello " + userName + "..";
            }
        }

        private void dvgCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dvgCustomer_DoubleClick(object sender, EventArgs e)
        {

            if (dvgContact.CurrentRow.Index != -1)
            {
                model.ContactId = Convert.ToInt32(dvgContact.CurrentRow.Cells["dgContactId"].Value);
                using (RegisterEntities1 db = new RegisterEntities1())
                {
                    model = db.contacts.Where(x => x.ContactId == model.ContactId && x.UserId == userID).FirstOrDefault();
                    
                    txtFirstName.Text = model.FirstName;
                    txtLastName.Text = model.LastName;
                    txtContactNo.Text = model.ContactNo;
                    txtAddress.Text = model.Address;
                    txtEmail.Text = model.Email;
                }
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure want to delete this contact?", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (RegisterEntities1 db = new RegisterEntities1())
                {
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Detached)
                    {
                        db.contacts.Attach(model);
                        db.contacts.Remove(model);
                        db.SaveChanges();
                        loadData();
                        clear();
                        MessageBox.Show("Contact Deleted...");
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text;

            using (RegisterEntities1 db = new RegisterEntities1())
            {
                var searchResult = db.contacts
                    .Where(x => (x.FirstName.Contains(searchText) || x.LastName.Contains(searchText) || x.Address.Contains(searchText)) && x.UserId == userID)
                    .ToList();
                dvgContact.DataSource = searchResult;
            }
            clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var sign = new signup();
            sign.Show();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void txtShow_TextChanged_1(object sender, EventArgs e)
        {
            
        }
    }
}
