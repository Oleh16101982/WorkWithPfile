using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;

namespace WorkWithPfile
{
    class clCreateFileToSend
    {

        private NewDataSet dsP7file = new NewDataSet();


        public clCreateFileToSend(Int32 id)
        {
            MessageBox.Show("Class CreateFileToSend");
            fCreateDataSet(id);
        }

        private void fCreateDataSet(Int32 idDeclaration)
        {
            


        }

    }
}
