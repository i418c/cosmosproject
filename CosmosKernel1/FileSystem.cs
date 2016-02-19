﻿using System;
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
            foreach (File file in files)
            {
                //filenames[ct] = String.Format("--{0,10}--", file.getName(), file.getFileExtension(), file.getDateCreated(), file.getFileSize());
                filenames[ct] = file.getName() + " " + file.getFileExtension() + " " + file.getDateCreated() + " " + file.getFileSize();

                ct++;
            }
            return filenames;
        }


    }
}
