using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace MassText
{
    public partial class frmMain : Form
    {
        private string mailServer;
        private string recipient;
        private string emailer;
        private string port;
        private string security;
        private string message;
        private string subject;
        private string password;

        private string tempPhone;
        private string tempCarrier;

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tempPhone = "5639203522";
            tempCarrier = "@email.uscc.net";
            //cboCarrier.Items.Add("@email.uscc.net");
            //cboCarrier.Items.Add("@vtext.com");
        }

        private void btnExit_Click(System.Object sender, System.EventArgs e)
        {   
            Application.Exit();
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            recipient = string.Concat(tempPhone, tempCarrier); //cboCarrier.SelectedItem.ToString());
            emailer = txtSender.Text;
            password = txtPassword.Text;
            port = cboPort.Text;
            security = txtSecurity.Text;
            subject = txtSubject.Text;
            mailServer = txtMailServer.Text;
            message = txtMessage.Text;

            try
            {
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailer, password);
                SmtpServer.Port = 25;
                SmtpServer.Host = mailServer;
                SmtpServer.EnableSsl = true;

                MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(emailer);
                    mail.To.Add(recipient);
                    mail.Subject = subject;
                    mail.Body = message;
                    SmtpServer.Send(mail);
                    MessageBox.Show("Text Message sent successfully!");
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutBox1();
            about.ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var setting = new frmSettings();
            setting.ShowDialog();
        }
    }
}
