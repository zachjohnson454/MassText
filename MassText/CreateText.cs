using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassText
{
    class CreateText : IDisposable
    {
        private string file;
        private string path;
        private StreamWriter csv;

        public CreateText(string fileName, string savePath)
        {
            file = fileName + ".txt";
            path = savePath;
            csv = new StreamWriter(path + @"\" + file);
        }

        public void Dispose()
        {
            csv.Close();
        }

        public void Save(string line)
        {
            csv.WriteLine(line);
        }
    }
}
