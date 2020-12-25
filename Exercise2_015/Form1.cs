using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.Windows.Forms;

namespace Exercise2_015
{

    public partial class Form1 : Form
    {
        Mahasiswa mhs = new Mahasiswa();
        string baseUrl = "http://localhost:1907/";

        public Form1()
        {
            InitializeComponent();

            btUpdate.Enabled = false;
            btHapus.Enabled = false;
            tampilData();
            totalMhs();
        }

        public void tampilData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            dGVDataMhs.DataSource = data;
            dGVDataMhs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;   
        }

        private void dGVDataMhs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbNIM.Text = Convert.ToString(dGVDataMhs.Rows[e.RowIndex].Cells[1].Value);
            tbNama.Text = Convert.ToString(dGVDataMhs.Rows[e.RowIndex].Cells[0].Value);
            tbProdi.Text = Convert.ToString(dGVDataMhs.Rows[e.RowIndex].Cells[2].Value);
            tbAngkatan.Text = Convert.ToString(dGVDataMhs.Rows[e.RowIndex].Cells[3].Value);

            btHapus.Enabled = true;
            btUpdate.Enabled = true;
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            tbNIM.Clear();
            tbNama.Clear();
            tbAngkatan.Clear();
            tbProdi.Clear();
            tbCari.Clear();
            tampilData();
            totalMhs();
        }

       public void totalMhs()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            int length = data.Count();
            lbTotal.Text = Convert.ToString(length);
        }

        private void responseMsg()
        {

            if(lblStatus.Text.Length >= 8)
            {
                lblStatus.BackColor = System.Drawing.Color.Green;
                lblStatus.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                lblStatus.BackColor = System.Drawing.Color.Red;
                lblStatus.ForeColor = System.Drawing.Color.White;
            }
        }

       
        
        private void btnTambah_Click(object sender, EventArgs e)
        {
            btHapus.Enabled = false;
            btUpdate.Enabled = false;

            mhs.nim = tbNIM.Text;
            mhs.nama = tbNama.Text;
            mhs.angkatan = tbAngkatan.Text;
            mhs.prodi = tbProdi.Text;

            var data = JsonConvert.SerializeObject(mhs);
            var postdata = new WebClient();
            postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string response = postdata.UploadString(baseUrl + "Mahasiswa", data);

            lblStatus.Text = response;
            responseMsg();
            tampilData();
            totalMhs();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            string nim = tbCari.Text;
            if (nim == null || nim == "")
            {
                dGVDataMhs.DataSource = data;
            }
            else
            {
                var item = data.Where(x => x.nim == tbCari.Text).ToList();

                dGVDataMhs.DataSource = item;
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {

            mhs.nim = tbNIM.Text;
            mhs.nama = tbNama.Text;
            mhs.angkatan = tbAngkatan.Text;
            mhs.prodi = tbProdi.Text;

            var data = JsonConvert.SerializeObject(mhs);
            var updatedata = new WebClient();
            updatedata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string response = updatedata.UploadString(baseUrl + "UpdateMahasiswa", data);

            lblStatus.Text = response;
            responseMsg();
            tampilData();
            totalMhs();
        }

        private void btHapus_Click(object sender, EventArgs e)
        {
            mhs.nim = tbNIM.Text;

            var data = JsonConvert.SerializeObject(mhs);
            var deletedata = new WebClient();
            deletedata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string response = deletedata.UploadString(baseUrl + "DeleteMahasiswa/" + tbNIM.Text, data);

            lblStatus.Text = response;
            responseMsg();
            tampilData();
            totalMhs();
        }
    }

    
}
