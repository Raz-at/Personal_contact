using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Register
{

    public partial class Form1 : Form
    {
        register model = new register();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            model.FirstName = txtFirstName.Text;
            model.LastName = txtLastName.Text;
            model.Gender = comboGender.Text;
            // model.Age = int.Parse(txtAge.Text);
            model.Email = txtEmail.Text;
            model.Age = int.Parse(txtAge.Text);
            model.Password = txtPassword.Text;
            model.RePassword = txtRePassword.Text;

            using (RegisterEntities1 db = new RegisterEntities1())
            {
                bool checkName = db.registers.Any(x => x.FirstName == model.FirstName || x.LastName == model.LastName || x.Email==model.Email);
                if (!checkName)
                {
                    if (model.Password == model.RePassword)
                    {
                        db.registers.Add(model);
                        db.SaveChanges();
                        MessageBox.Show("Data is added");
                        var sign = new signup();
                        sign.Show();
                    }
                    else
                    {
                        MessageBox.Show("Password is not same");
                    }
                }
                else
                {
                    MessageBox.Show("First name or last name or this email already exist...");
                }
            }
            
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            var sign = new signup();
            sign.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
