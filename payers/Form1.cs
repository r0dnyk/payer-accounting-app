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


namespace payers
{
    public partial class Form1 : Form
    {
        SqlConnection con1 = new SqlConnection(@"Data Source=DESKTOP-BPA2H7C\EVM;Initial Catalog=payers;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO Zagalna_info_platnika(Pod_nomer, Tip_osobi, Nazva, Adresa_reestracii, Stan) values('"+Pod_nomer_txt.Text+"', '"+Tip_osobi_txt.Text+"', '"+ Nazva_txt.Text+"', '"+Adresa_reestracii_txt.Text+"', '"+Stan_txt.Text+"')", con1);
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Termin_perebuv_na_obliku (Pod_nomer, Data_vziattia_na_oblik, Data_zniatta_z_obliku) values ('"+Pod_nomer_txt.Text+"', '"+Data_vziattia_na_oblik_txt.Text+"', '"+Data_zniatta_z_obliku_txt.Text+"')", con1);
            SqlCommand cmd3 = new SqlCommand("INSERT INTO Reestr_pdv (Pod_nomer, Data_reestracii_pdv, Data_skasuv_reestracii_pdv) values ('"+Pod_nomer_txt.Text+"', '"+Data_reestracii_pdv_txt.Text+"', '"+Data_skasuv_reestracii_pdv_txt.Text+"')", con1);
            SqlCommand cmd4 = new SqlCommand("INSERT INTO Kilkist_kved_raxunkiv (Pod_nomer, Kilkist_vidiv_dialnosti, Kilkist_bank_rachunkiv) values ('"+Pod_nomer_txt.Text+"', '"+Kilkist_vidiv_dialnosti_txt.Text+"', '"+Kilkist_bank_rachunkiv_txt.Text+"')", con1);

            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            load_data();
            clear_fun();
            MessageBox.Show("ЗБЕРЕЖЕНО");
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("UPDATE Zagalna_info_platnika SET Tip_osobi = '" + Tip_osobi_txt.Text + "', Nazva = '" + Nazva_txt.Text + "', Adresa_reestracii = '" + Adresa_reestracii_txt.Text + "', Stan = '" + Stan_txt.Text + "' WHERE Pod_nomer = '" + Pod_nomer_txt.Text + "'", con1);
            SqlCommand cmd2 = new SqlCommand("UPDATE Termin_perebuv_na_obliku SET Data_vziattia_na_oblik ='" + Data_vziattia_na_oblik_txt.Text + "', Data_zniatta_z_obliku ='" + Data_zniatta_z_obliku_txt.Text + "' WHERE Pod_nomer ='" + Pod_nomer_txt.Text + "'", con1);
            SqlCommand cmd3 = new SqlCommand("UPDATE Reestr_pdv SET Data_reestracii_pdv ='" + Data_reestracii_pdv_txt.Text + "', Data_skasuv_reestracii_pdv ='" + Data_skasuv_reestracii_pdv_txt.Text + "' WHERE Pod_nomer ='" + Pod_nomer_txt.Text + "'", con1);
            SqlCommand cmd4 = new SqlCommand("UPDATE Kilkist_kved_raxunkiv SET Kilkist_vidiv_dialnosti ='" + Kilkist_vidiv_dialnosti_txt.Text + "', Kilkist_bank_rachunkiv ='" + Kilkist_bank_rachunkiv_txt.Text + "' WHERE Pod_nomer ='" + Pod_nomer_txt.Text + "'", con1);

            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            load_data();
            clear_fun();
            MessageBox.Show("ОНОВЛЕНО");
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            SqlCommand del_cmd1 = new SqlCommand("DELETE FROM Zagalna_info_platnika WHERE Pod_nomer ='" + Pod_nomer_txt.Text + "'", con1);
            SqlCommand del_cmd2 = new SqlCommand("DELETE FROM Termin_perebuv_na_obliku WHERE Pod_nomer ='" + Pod_nomer_txt.Text + "'", con1);
            SqlCommand del_cmd3 = new SqlCommand("DELETE FROM Reestr_pdv WHERE Pod_nomer ='" + Pod_nomer_txt.Text + "'", con1);
            SqlCommand del_cmd4 = new SqlCommand("DELETE FROM Kilkist_kved_raxunkiv WHERE Pod_nomer ='" + Pod_nomer_txt.Text + "'", con1);

            del_cmd4.ExecuteNonQuery();
            del_cmd3.ExecuteNonQuery();
            del_cmd2.ExecuteNonQuery();
            del_cmd1.ExecuteNonQuery();
            load_data();
            clear_fun();
            MessageBox.Show("ЗАПИС ВИДАЛЕНО");
        }

               
        private void clr_btn_Click(object sender, EventArgs e)
        {
             clear_fun();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con1.Open();
            load_data();
        }

        void load_data()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT Zagalna_info_platnika.Pod_nomer, Zagalna_info_platnika.Tip_osobi, Zagalna_info_platnika.Nazva, Zagalna_info_platnika.Adresa_reestracii, Zagalna_info_platnika.Stan, Termin_perebuv_na_obliku.Data_vziattia_na_oblik, Termin_perebuv_na_obliku.Data_zniatta_z_obliku, Reestr_pdv.Data_reestracii_pdv, Reestr_pdv.Data_skasuv_reestracii_pdv, Kilkist_kved_raxunkiv.Kilkist_vidiv_dialnosti, Kilkist_kved_raxunkiv.Kilkist_bank_rachunkiv FROM Zagalna_info_platnika INNER JOIN Termin_perebuv_na_obliku ON Zagalna_info_platnika.Pod_nomer = Termin_perebuv_na_obliku.Pod_nomer INNER JOIN Reestr_pdv ON Zagalna_info_platnika.Pod_nomer = Reestr_pdv.Pod_nomer INNER JOIN Kilkist_kved_raxunkiv ON Zagalna_info_platnika.Pod_nomer = Kilkist_kved_raxunkiv.Pod_nomer", con1);
            DataTable dt = new DataTable();
           da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void clear_fun()
        {
            Pod_nomer_txt.Text = "";
            Tip_osobi_txt.Text = "";
            Nazva_txt.Text = "";
            Adresa_reestracii_txt.Text = "";
            Stan_txt.Text = "";
            Data_vziattia_na_oblik_txt.Text = "";
            Data_zniatta_z_obliku_txt.Text = "";
            Data_reestracii_pdv_txt.Text = "";
            Data_skasuv_reestracii_pdv_txt.Text = "";
            Kilkist_vidiv_dialnosti_txt.Text = "";
            Kilkist_bank_rachunkiv_txt.Text = "";
        }

        private void Pod_nomer_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int cri = dataGridView1.CurrentCell.RowIndex;

            Pod_nomer_txt.Text = dataGridView1.Rows[cri].Cells[0].Value.ToString();
            Tip_osobi_txt.Text = dataGridView1.Rows[cri].Cells[1].Value.ToString();
            Nazva_txt.Text = dataGridView1.Rows[cri].Cells[2].Value.ToString();
            Adresa_reestracii_txt.Text = dataGridView1.Rows[cri].Cells[3].Value.ToString();
            Stan_txt.Text = dataGridView1.Rows[cri].Cells[4].Value.ToString();
            Data_vziattia_na_oblik_txt.Text = dataGridView1.Rows[cri].Cells[5].Value.ToString();
            Data_zniatta_z_obliku_txt.Text = dataGridView1.Rows[cri].Cells[6].Value.ToString();
            Data_reestracii_pdv_txt.Text = dataGridView1.Rows[cri].Cells[7].Value.ToString();
            Data_skasuv_reestracii_pdv_txt.Text = dataGridView1.Rows[cri].Cells[8].Value.ToString();
            Kilkist_vidiv_dialnosti_txt.Text = dataGridView1.Rows[cri].Cells[9].Value.ToString();
            Kilkist_bank_rachunkiv_txt.Text = dataGridView1.Rows[cri].Cells[10].Value.ToString();
        }

        

        

        private void ok_btn_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT Zagalna_info_platnika.Pod_nomer, Zagalna_info_platnika.Tip_osobi, Zagalna_info_platnika.Nazva, Zagalna_info_platnika.Adresa_reestracii, Zagalna_info_platnika.Stan, Termin_perebuv_na_obliku.Data_vziattia_na_oblik, Termin_perebuv_na_obliku.Data_zniatta_z_obliku, Reestr_pdv.Data_reestracii_pdv, Reestr_pdv.Data_skasuv_reestracii_pdv, Kilkist_kved_raxunkiv.Kilkist_vidiv_dialnosti, Kilkist_kved_raxunkiv.Kilkist_bank_rachunkiv FROM Zagalna_info_platnika INNER JOIN Termin_perebuv_na_obliku ON Zagalna_info_platnika.Pod_nomer = Termin_perebuv_na_obliku.Pod_nomer INNER JOIN Reestr_pdv ON Zagalna_info_platnika.Pod_nomer = Reestr_pdv.Pod_nomer INNER JOIN Kilkist_kved_raxunkiv ON Zagalna_info_platnika.Pod_nomer = Kilkist_kved_raxunkiv.Pod_nomer WHERE " + op_cmb.Text + " LIKE '%" + key_txt.Text + "%'", con1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
