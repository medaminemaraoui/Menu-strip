using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Menu_strip
{
    public partial class Clients_F : Form
    {
        public Clients_F()
        {
            InitializeComponent();
        
        }
        DataTable a = ds.Tables["Client"];

        static SqlConnection cnx = new SqlConnection(@"Data Source=IBIZA\SQLEXPRESS;Initial Catalog=Gest_Client;Integrated Security=True");
        static SqlDataAdapter DA = new SqlDataAdapter("select * from Client ", cnx);
        static SqlDataAdapter DA2 = new SqlDataAdapter("select * from Produits ", cnx);
        static DataSet ds = new DataSet();
        
        // fonction existe qui recherche un id de type int
        int  existe(int id)
        {
            int c = -1;
            
            for (int i = 0; i<ds.Tables["Client"].Rows.Count;i++)
            {
               if(ds.Tables["Client"].Rows[i].RowState != DataRowState.Deleted)
                {
                    if(id == int.Parse(ds.Tables["Client"].Rows[i][0].ToString()))
                    {
                        c = i;
                        break;
                    }
                }
            }
            return c;
        }
        // Fonction Affichage
        void Affichage()
        {
            // Remplire data grid view
            DA.Fill(ds,"Client");
            DGV.DataSource = ds.Tables["Client"];
            // remplir combobox 
            DA2.Fill(ds, "Produits");
            CB.DataSource = ds.Tables["Produits"];
            CB.DisplayMember = "Nom_produits";
            CB.ValueMember = "idP";
            CB.SelectedIndex = -1;
            

        }
        private void Clients_F_Load(object sender, EventArgs e)
        {
            //Appel fonction Affichage
            Affichage();
        }

        // button Ajouter
        private void button2_Click(object sender, EventArgs e)
        {
            int E = existe(int.Parse(txt_idc.Text));
            if (E == -1)
            {
                DataRow Ligne;
                Ligne = a.NewRow();
                Ligne[0] = txt_idc.Text;
                Ligne[1] = txt_nc.Text;
                Ligne[2] = DTP.Value;
                Ligne[3] = CB.SelectedValue;
                a.Rows.Add(Ligne);
                MessageBox.Show("Ajoute bien fait !");

            }
            else MessageBox.Show("id client est deja existe !!");
        }
        // button Supprimer
        private void button1_Click(object sender, EventArgs e)
        {
              if (txt_idc.Text == "") MessageBox.Show("donner id");
             else
             {

              int E = existe(int.Parse(txt_idc.Text));
             ds.Tables["Client"].Rows[E].Delete();
             }
        }
        //      button Modifier
        private void button4_Click(object sender, EventArgs e)
        {

            if (txt_idc.Text == "" || txt_nc.Text == ""  || CB.Text == "")
            {
                MessageBox.Show("remplir tout les champs");

            }
            else
            {
                int f = existe(int.Parse(txt_idc.Text));
                if (f == -1)
                {
                    MessageBox.Show("id n'existe pas");
                }
                else
                {
                    ds.Tables["Client"].Rows[f][1] = txt_nc.Text;
                    ds.Tables["Client"].Rows[f][2] = DTP.Value.ToString();
                    ds.Tables["Client"].Rows[f][3] = CB.SelectedValue;

                }
            }
        }
        // Button Rechercher
        private void button3_Click(object sender, EventArgs e)
        {

            if (txt_idc.Text == "")
            {
                MessageBox.Show("remplir champ id");

            }
            else
            {
                int f = existe(int.Parse(txt_idc.Text));
                if (f == -1)
                {
                    MessageBox.Show("id n'existe pas");
                }
                else
                {
                    txt_nc.Text = ds.Tables["Client"].Rows[f][1].ToString();
                    DTP.Value =DateTime.Parse(ds.Tables["Client"].Rows[f][2].ToString()) ;
                    CB.SelectedValue = ds.Tables["Client"].Rows[f][3].ToString();
                    

                }
            }
        }
        // Button Enregistrer
        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder cmdb = new SqlCommandBuilder(DA);
            DA.Update(ds, "Client");
            MessageBox.Show("bien enregistrer");
        }
    }
}
