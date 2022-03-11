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
using System.IO;

namespace Menu_strip
{
    public partial class Exporter : Form
    {
        public Exporter()
        {
            InitializeComponent();
        }

        static SqlConnection cnx = new SqlConnection(@"Data Source=IBIZA\SQLEXPRESS;Initial Catalog=Gest_Client;Integrated Security=True");
        static DataSet ds2 = new DataSet();
        static SqlDataAdapter DA2 = new SqlDataAdapter("select * from Client ", cnx);

        void cb()
        {
            DA2.Fill(ds2, "Client");
            DA2 = new SqlDataAdapter("select * from Produits", cnx);
            DA2.Fill(ds2, "Produits");
            // remplire combobox par table chauffeur
            CB.DataSource = ds2.Tables["Produits"];
            // les membres qui s'afficheront dans combobox a partir de la colone Nom
            CB.DisplayMember = "Nom_produits";
            // les membres prendront des valeurs de la colone  idc
            CB.ValueMember = "idP";
            CB.SelectedIndex = -1;
            CB.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    
        private void CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idC_selected_index = CB.SelectedIndex;

            if (idC_selected_index == -1) DGV.DataSource = null;

            else
            {

                string idC_Selected = CB.SelectedValue.ToString();

                DataTable copy = ds2.Tables["Client"].Copy();


                for (int i = 0; i < copy.Rows.Count; i++)
                {
                    if (copy.Rows[i].RowState != DataRowState.Deleted)
                    {

                        if (idC_Selected != copy.Rows[i][3].ToString())
                        {
                            copy.Rows[i].Delete();
                        }
                    }

                }

                DGV.DataSource = null;
                DGV.DataSource = copy;

            }
        }

        private void Exporter_Load(object sender, EventArgs e)
        {

            cb();
        }
    }
}
