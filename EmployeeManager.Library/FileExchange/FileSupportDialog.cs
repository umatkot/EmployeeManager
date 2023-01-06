using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeManager.Library
{
    public class FileSupportDialog
    {
        public string FilePath { get; set; }
        private string _initialDirectory;

        public FileSupportDialog()
        {
            _initialDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        }

        public bool ShowSaveInstance()
        {
            var sfd = new SaveFileDialog {
            
                Filter = "XML Document(*.xml)|*.xml",
                DefaultExt = ".xml",
                InitialDirectory = _initialDirectory
            };

            if(sfd.ShowDialog() == true)
            {
                FilePath = Path.GetFullPath(sfd.FileName);
            }

            return true;
        }

        public bool ShowOpenInstance()
        {
            var ofd = new OpenFileDialog {
                Multiselect = false,
                Filter = "XML Document(*.xml)|*.xml",
                DefaultExt = ".xml",
                InitialDirectory = _initialDirectory
            };

            if (ofd.ShowDialog() == true && File.Exists(ofd.FileName))
            {
                FilePath = ofd.FileName;
            }

            return true;
        }
    }
}
