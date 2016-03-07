using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    class FileSystem
    {
        ArrayList files;

        public FileSystem()
        {
            files = new ArrayList();
        }

        public File create(string name, string extension)
        {
            File file = new File(name, extension);
            files.Add(file);
            return file;
        }
        
        public string[] list()
        {
            String[] filenames = new String[files.Count];
            
            for (int i = 0; i < files.Count; i++)
            {
                File file = (File)files[i];
                StringBuilder builder = new StringBuilder("");

                builder.Append(file.getName());
                builder.Append(".");
                builder.Append(file.getFileExtension());
                builder.Append(" Date Created: ");
                builder.Append(file.getDateCreated());
                builder.Append(" File Size: ");
                builder.Append(file.getFileSize());

                filenames[i] = builder.ToString();
            }
            return filenames;
        }

        public File findFile(String fileName)
        {
            for (int i = 0; i < files.Count; i++)
            {
                File fle = (File) files[i];
                String name = fle.getName() + "." + fle.getFileExtension();
                if (name == fileName)
                {
                    return fle;
                }
            }

            return null;
        }

        public bool exists(String file)
        {
            for (int i = 0; i < files.Count; i++)
            {
                File fle = (File) files[i];
                String name = fle.getName() + "." + fle.getFileExtension();
                if (name == file)
                {
                    return true;
                }
            }

            return false;
        }

        public bool exists(File file)
        {
            for (int i = 0; i < files.Count; i++)
            {
                File fle = (File) files[i];
                if (fle.Equals(file))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
