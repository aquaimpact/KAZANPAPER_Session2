using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kazanpaper2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Session2Entities db = new Session2Entities();
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            var username = usernameTxt.Text.Trim();
            var password = PasswordTxt.Text.Trim();
            var login = db.Employees.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            if (login != null)
            {
                Home home = new Home(login);
                Home_manager_ home_Manager_ = new Home_manager_();
                if (login.isAdmin == null)
                {
                    this.Hide();
                    home.ShowDialog();
                    this.Close();
                }
                else
                {
                    this.Hide();
                    home_Manager_.ShowDialog();
                    this.Close();

                }
            }
            else
            {
                MessageBox.Show("Invalid User!");
            }
        }
    }
}
