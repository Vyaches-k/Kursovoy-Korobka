using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using СandyFactoryLibrory;

namespace Slava
{
    public partial class Form1 : Form
    {
        CandyFactory CF;
        Loader lb;
        public Form1()
        {
            InitializeComponent();
            lb = new Loader(pbPogr);
            CF = new CandyFactory(lb,lbSach);
        }                   
        private void Form1_Load(object sender, EventArgs e)
        {            
              CF.konv.Add(new Technic("Конвейер№1", "Германия", 23, pbCandy));
              CF.konv.Add(new Technic("Конвейер№2", "Голландия", 24, pbCandy2));

        }
   
        private void ClUpd(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = CF.otchet;
        }

        private void ClStr(object sender, EventArgs e)
        {
           CF.Open();
        }

        private void ClStp(object sender, EventArgs e)
        {
            CF.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CF.IsWork())
                CF.Close();          
        }       
    }
}
