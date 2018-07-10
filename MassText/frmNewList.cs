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

namespace MassText
{
    public partial class frmNewList : Form
    {
        public frmNewList()
        {
            InitializeComponent();
            txtSavePath.Text = Directory.GetCurrentDirectory();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txtSavePath.Text = Shared.CustDialogs.ReturnFolderPath();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string line = "";
            var csv = new CreateText(txtListName.Text, txtSavePath.Text);

            foreach (DataGridViewRow row in dgvList.Rows)
            {
                if (row.Cells["name"].Value is null) break;
                line = row.Cells["active"].FormattedValue + ",";
                line += row.Cells["name"].Value + ",";
                line += row.Cells["phone"].Value + ",";
                line += row.Cells["carrier"].Value;
                csv.Save(line);
            }
            csv.Dispose();

            MessageBox.Show("List has been successfully saved!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void dgvList_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            lblListTotal.Text = (int.Parse(lblListTotal.Text) + 1).ToString();
        }

        private void dgvList_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            lblListTotal.Text = (int.Parse(lblListTotal.Text) - 1).ToString();
        }
    }
}
