using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu_strip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients_F C = new Clients_F();
            bool CO = false;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Clients_F")
                {
                    CO = true;

                }
            }
            if (!CO)
                {

                    C.MdiParent = this;
                    C.Show();

                }
            
        }

        private void produitsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Produits_F P = new Produits_F();    
            bool CO = false;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Produits_F")
                {
                    CO = true;

                }
            }
            if (!CO)
                {

                    P.MdiParent = this;
                    P.Show();

                }
            
        }
    }
}
