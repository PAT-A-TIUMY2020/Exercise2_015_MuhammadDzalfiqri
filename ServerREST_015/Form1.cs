using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceREST_015;

namespace Server_015
{
    public partial class Form1 : Form
    {
        ServiceHost hostObject = null;

        string msg = "Tekan OFF untuk Mematikankan Server";
        string msg2 = "OFF";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnON_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    hostObject = new ServiceHost(typeof(TI_UMY));
                    hostObject.Open();
                });

                lbReady.Text = "READY";
                lblMsg.Text = msg;
                lbReady.BackColor = System.Drawing.Color.Green;

            }catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }
        }

        private void btnOFF_Click(object sender, EventArgs e)
        {
            try
            {
                hostObject.Close();
                hostObject = null;
                lblMsg.Text = "Tekan ON untuk Menyalakan Server";
                lbReady.Text = msg2;
                lbReady.BackColor = System.Drawing.Color.Red;
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }

        }
    }
}
