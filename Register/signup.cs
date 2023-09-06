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
    public partial class signup : Form
    {
        register model = new register();
        public signup()
        {
            InitializeComponent();
            
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            model.Email = txtEmail.Text;
            model.Password = txtPassword.Text;
            using(RegisterEntities1 db = new RegisterEntities1())
            {
                var findEmail = db.registers.FirstOrDefault(x => x.Email == model.Email);
                if (findEmail != null)
                {
                    if(model.Password == findEmail.Password)
                    {
                        //MessageBox.Show("Hoore");
                        var cont = new ContactList(findEmail.UserID , findEmail.FirstName);
                        cont.Show();
                        this.Close();
                        
                    }
                    else
                    {
                        MessageBox.Show("Passworrd incorrect");
                    }
                }
                else
                {
                    MessageBox.Show("Email or password is incorredt");
                }
            }
        }

        private void signup_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtEmail;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var regi = new Form1();
            regi.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
