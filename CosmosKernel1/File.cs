using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    class File
    {
        private string name = null;
        private string extension = null;
        private string contents = null;
        private string dateCreated = null;

        public File(string name, string extension)
        {
            this.name = name;
            this.extension = extension;
            this.dateCreated = Hardware.Time.getDate();
        }

        public File(string name, string extension, string contents) : this(name, extension)
        {
            this.contents = contents;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }

        public string getFileExtension()
        {
            return extension;
        }

        public void setFileExtension(string extension)
        {
            this.extension = extension;
        }

        public string getDateCreated()
        {
            return this.dateCreated;
        }

        public void setContents(string contents)
        {
            this.contents = contents;
        }

        public string getContents()
        {
            return this.contents;
        }

        public int getFileSize()
        {
            return contents.Length;
        }

        public Boolean Equals(File other)
        {
            return (this.name.Equals(other.getName()) && this.extension.Equals(other.extension));
        }
    }
}
