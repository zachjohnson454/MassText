using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MassText.Shared
{
    class CustDialogs

    {

        public static string ReturnFilePath()
        {
            string path = "";
            var file = new OpenFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "Text File .txt|*.txt|Csv File .csv|*.csv"
            };

            file.ShowDialog();
            foreach (var item in file.FileNames)
            {
                path = file.FileName;
            }
            return path;
        }

        public static StreamReader ReturnStream(string path)
        {
            try
            {
                return new StreamReader(path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ReturnFolderPath()
        {
            string path = "";
            var fldr = new FolderBrowserDialog
            {
                SelectedPath = Directory.GetCurrentDirectory(),
            };

            fldr.ShowDialog();
            path = fldr.SelectedPath;
            return path;
        }
    }
}
