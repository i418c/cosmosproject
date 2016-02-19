using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    class FileSystem
    {
        static System.Collections.ArrayList files;

        public FileSystem()
        {
            if (files==null)
            {
                files = new System.Collections.ArrayList();
            }
        }

        public void create(string name, string extension)
        {
            files.Add(new File(name, extension));
        }
        
        public string[] list()
        {
            string[] filenames = new string[files.Count];
            var ct = 0;
            foreach (var file in files)
            {
                filenames[ct] = String.Format("--{0,10}--", file.getName(), file.getExtension(), file.getDateCreated(), file.getFileSize();
                ct++;
            }
            return filenames;
        }


    }
}
