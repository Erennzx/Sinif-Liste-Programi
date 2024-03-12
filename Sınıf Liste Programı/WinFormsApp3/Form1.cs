using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Windows.Forms;
using WinFormsApp3.Modeller;
using static Azure.Core.HttpHeader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        Veritabani vt = new Veritabani();

        public int secilenIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnKayitEkle_Click(object sender, EventArgs e)
        {
            Ogrenci yeniOgrenci = new Ogrenci();

            yeniOgrenci.Ad = txtAd.Text;
            yeniOgrenci.Soyad = txtSoyad.Text;
            yeniOgrenci.OkulNo = Convert.ToInt32(txtOkulNo.Text);
            yeniOgrenci.Sinif = (Sinif)comboBoxYeniOgr.SelectedItem;

            vt.Ogrenciler.Add(yeniOgrenci);
            vt.SaveChanges();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vt.Ogrenciler.Load();
            vt.Siniflar.Load();

            foreach (var item in vt.Siniflar.Local)
            {
                comboBoxSiniflar.Items.Add(item);
            }
            comboBoxSiniflar.DisplayMember = "SinifAd";

            foreach (var item in vt.Siniflar.Local)
            {
                comboBoxYeniOgr.Items.Add(item);
            }

            comboBoxYeniOgr.DisplayMember = "SinifAd";

            Sinif tumList = new Sinif();
            tumList.Sube = "T�m ��renciler";
            tumList.Seviye = 0;
            tumList.Id = 0;
            comboBoxSirala.Items.Add(tumList);
            comboBoxSirala.SelectedIndex = 0;
            foreach (var item in vt.Siniflar.Local)
            {
                comboBoxSirala.Items.Add(item);
            }
            
            comboBoxSirala.DisplayMember = "SinifAd";

            dataGridView1.DataSource = vt.Ogrenciler.Local.ToBindingList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;

            dataGridView1.ColumnHeadersVisible = true;

            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();

            foreach (var item in vt.Siniflar.Local)
            {
                combo.Items.Add(item);
            }

            combo.ReadOnly = true;

            combo.HeaderText = "S�n�f";
            combo.DataPropertyName = "Sinif";
            combo.DisplayMember = "SinifAd";
            combo.ValueMember = "Kendisi";

            dataGridView1.Columns.Add(combo);

            //--

            dataGridView2.DataSource = vt.Ogrenciler.Local.ToBindingList();
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[4].Visible = false;

            dataGridView2.ColumnHeadersVisible = true;

            DataGridViewComboBoxColumn combo2 = new DataGridViewComboBoxColumn();

            foreach (var item in vt.Siniflar.Local)
            {
                combo2.Items.Add(item);
            }

            combo2.ReadOnly = true;

            combo2.HeaderText = "S�n�f";
            combo2.DataPropertyName = "Sinif";
            combo2.DisplayMember = "SinifAd";
            combo2.ValueMember = "Kendisi";

            dataGridView2.Columns.Add(combo2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            vt.Ogrenciler.Load();
            Sinif secilenSinif = (Sinif)comboBoxSirala.SelectedItem;
            if (comboBoxSirala.SelectedIndex != 0)
            {
                dataGridView1.DataSource = vt.Ogrenciler.Local.Where(x => x.Sinif.SinifAd == secilenSinif.SinifAd).ToList();
            }
            if (comboBoxSirala.SelectedIndex == 0)
            {
                dataGridView1.DataSource = vt.Ogrenciler.Local.ToBindingList();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            vt.SaveChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnKayitEkle_Click(sender, e);
        }

        private void btnSil2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            vt.SaveChanges();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                dataGridView1.Rows[secilenIndex].Cells[2].Value = txtAd.Text;
                dataGridView1.Rows[secilenIndex].Cells[3].Value = txtSoyad.Text;
                dataGridView1.Rows[secilenIndex].Cells[1].Value = Convert.ToInt32(txtOkulNo.Text);
                dataGridView1.Rows[secilenIndex].Cells[4].Value = (Sinif)comboBoxYeniOgr.SelectedItem;
            }
            vt.SaveChanges();
            dataGridView2.Refresh();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            secilenIndex = e.RowIndex;

            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtOkulNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBoxYeniOgr.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[4].Value;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Sinif yeniSinif = new Sinif();

            yeniSinif.Seviye = Convert.ToInt32(txtSeviye.Text);
            yeniSinif.Sube = txtSube.Text;

            vt.Siniflar.Add(yeniSinif);
            vt.SaveChanges();

            comboBoxYeniOgr.Items.Clear();
            comboBoxSirala.Items.Clear();
            comboBoxSiniflar.Items.Clear();

            foreach (var item in vt.Siniflar.Local)
            {
                comboBoxYeniOgr.Items.Add(item);
            }
            comboBoxYeniOgr.DisplayMember = "SinifAd";

            foreach (var item in vt.Siniflar.Local)
            {
                comboBoxSiniflar.Items.Add(item);
            }
            comboBoxSiniflar.DisplayMember = "SinifAd";

            Sinif tumList = new Sinif();
            tumList.Sube = "T�m ��renciler";
            tumList.Seviye = 0;
            tumList.Id = 0;
            comboBoxSirala.Items.Add(tumList);
            comboBoxSirala.SelectedIndex = 0;
            foreach (var item in vt.Siniflar.Local)
            {
                comboBoxSirala.Items.Add(item);
            }

            vt.SaveChanges();
        }

        private void btnSinifSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                vt.Siniflar.Remove((Sinif)comboBoxSiniflar.SelectedItem);
            }

            vt.SaveChanges();

            comboBoxYeniOgr.Items.Clear();
            comboBoxSirala.Items.Clear();
            comboBoxSiniflar.Items.Clear();

            foreach (var item in vt.Siniflar.Local)
            {
                comboBoxYeniOgr.Items.Add(item);
            }
            comboBoxYeniOgr.DisplayMember = "SinifAd";

            foreach (var item in vt.Siniflar.Local)
            {
                comboBoxSiniflar.Items.Add(item);
            }
            comboBoxSiniflar.DisplayMember = "SinifAd";

            Sinif tumList = new Sinif();
            tumList.Sube = "T�m ��renciler";
            tumList.Seviye = 0;
            tumList.Id = 0;
            comboBoxSirala.Items.Add(tumList);
            comboBoxSirala.SelectedIndex = 0;
            foreach (var item in vt.Siniflar.Local)
            {
                comboBoxSirala.Items.Add(item);
            }

            vt.SaveChanges();
        }
    }
}
