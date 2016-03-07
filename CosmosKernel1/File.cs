using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    class File
    {
        private String name = null;
        private String extension = null;
        private String contents = null;
        private String dateCreated = null;

        public File(String name, String extension)
        {
            this.name = name;
            this.extension = extension;
            this.dateCreated = GetTimestamp();
        }

        public File(String name, String extension, String contents) : this(name, extension)
        {
            this.contents = contents;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getName()
        {
            return this.name;
        }

        public String getFileExtension()
        {
            return extension;
        }

        public void setFileExtension(String extension)
        {
            this.extension = extension;
        }

        public String getDateCreated()
        {
            return this.dateCreated;
        }

        public void setContents(String contents)
        {
            this.contents = contents;
        }

        public String getContents()
        {
            return this.contents;
        }

        public int getFileSize()
        {
            return contents.Length;
        }

        private static String GetTimestamp()
        {
            //The DateTime object isn't implemented, so we can't get the date from that.
            //Presumably we could use the functions under Cosmos.HAL, but these exist in Ring0 and our program is in Ring3,
            //so we can't call the functions from here.
            return "This feature cannot be implemented";
        }

        public Boolean Equals(File other)
        {
            return (this.name.Equals(other.getName()) && this.extension.Equals(other.extension));
        }
    }
}
