using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsTest.BLL;
using WindowsFormsTest.DAL;

namespace WindowsFormsTest
{
    public partial class Form1 : Form
    {

        string errorMessage;

        public Form1()
        {
            InitializeComponent();
        }

        private void CloaseIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonSignInEvent(object sender, EventArgs e)
        {
            GetUserData();
        }

        private void GetUserData()
        {
            try
            {

                if (!IsValidInpot())
                {
                    return;
                }
                Super.CurrentUser = new BO.UserModel()
                {
                    User_Email = tb_Email.Text.Trim(),
                    User_Password = tb_Password.Text.Trim()
                };

                if (!UserDatabaseOperations.GetUserData2(ref Super.CurrentUser, out errorMessage))
                {
                    Super.CurrentUser = null;
                    ShowMessageBox(errorMessage);
                    return;
                }
                this.Hide();
                tb_Email.Text = tb_Password.Text = string.Empty;
                //SwitchForm(AppFormes.MAIN_FORM);
                //ShowMessageBox("User Found >>> User ID : " + CurrentUser.User_IS_Admin);
            }
            catch (Exception ex)
            {
                ShowMessageBox("LoadingForm.getUserFromDB.Exception:" + ex.Message);
                Super.CurrentUser = null;
            }

        }

        private bool IsValidInpot()
        {
            if (tb_Email.Text.Trim().Equals(string.Empty))
            {
                //tb_Email.SelectAll();
                tb_Email.Focus();
                ShowMessageBox("Pleaase Enter Valid Email Address ....");
                return false;
            }
            if (tb_Password.Text.Trim().Equals(string.Empty))
            {
                //tb_Password.SelectAll();
                tb_Password.Focus();
                ShowMessageBox("Pleaase Enter Valid Password ....");
                return false;
            }
            return true;
        }

        public void ShowMessageBox(String message)
        {
            MessageBox.Show(message);
        }
    }
}
