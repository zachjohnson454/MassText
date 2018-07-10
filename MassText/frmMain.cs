using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using MassText.Shared;

namespace MassText
{
    public partial class frmMain : Form
    {
        private string mailServer;
        private string recipient;
        private string emailer;
        private int port;
        private string security;
        private string message;
        private string subject;
        private string password;
        private string rootPath;

        public frmMain()
        {
            InitializeComponent();
            rootPath = Directory.GetCurrentDirectory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var f in Directory.GetFiles(rootPath))
            {
                if (f.ToString().Contains(".txt"))
                {
                    cboList.Items.Add(f.ToString());
                }
            }
        }

        private void btnExit_Click(System.Object sender, System.EventArgs e)
        {   
            Application.Exit();
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvList.Rows)
            {
                if (bool.Parse(row.Cells[0].FormattedValue.ToString()))
                {
                    emailer = txtSender.Text;
                    password = txtPassword.Text;
                    port = int.Parse(cboPort.Text);
                    security = txtSecurity.Text;
                    subject = txtSubject.Text;
                    mailServer = cboMailServer.Text;
                    message = txtMessage.Text;
                    recipient = string.Concat(row.Cells[2].FormattedValue, row.Cells[3].FormattedValue); 

                    try
                    {
                        SmtpClient SmtpServer = new SmtpClient();
                        SmtpServer.Credentials = new System.Net.NetworkCredential(emailer, password);
                        SmtpServer.Port = port;
                        SmtpServer.Host = mailServer;
                        SmtpServer.EnableSsl = true;

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(emailer);
                        mail.To.Add(recipient);
                        mail.Subject = subject;
                        mail.Body = message;
                        SmtpServer.Send(mail);
                        
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
            }

            MessageBox.Show("Text Messages sent successfully!");
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

        private void createNewListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newlist = new frmNewList();
            newlist.ShowDialog();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string file = CustDialogs.ReturnFilePath();
            if (file == "") { return; }
            LoadList(file);
            cboList.Text = file;
        }

        private void cboList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList(cboList.Text);
        }

        private void LoadList(string path)
        {
            StreamReader sr = CustDialogs.ReturnStream(path);
            dgvList.Rows.Clear();
            do
            {
                string[] line = sr.ReadLine().Split(',');
                var index = dgvList.Rows.Add();
                dgvList.Rows[index].Cells[0].Value = line[0];
                dgvList.Rows[index].Cells[1].Value = line[1];
                dgvList.Rows[index].Cells[2].Value = line[2];
                dgvList.Rows[index].Cells[3].Value = line[3];
            } while (sr.EndOfStream != true);

            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
