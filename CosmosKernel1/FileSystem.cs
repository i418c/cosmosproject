using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    class FileSystem
    {
        static ArrayList files;

        public FileSystem()
        {
            if (files==null)
            {
                files = new ArrayList();
            }
        }

        public void create(string name, string extension)
        {
            files.Add(new File(name, extension));
        }
        
        public string[] list()
        {
            String[] filenames = new String[files.Count];
            var ct = 0;
            for (int i = 0; i < files.Count; i++)
            {
                File file = (File) files[i];
                filenames[ct] = "" + file.getName() + " " + file.getFileExtension() + " " + file.getDateCreated() + " " + file.getFileSize();

                ct++;
            }
            return filenames;
        }


    }
}
