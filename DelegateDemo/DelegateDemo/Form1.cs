using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelegateDemo
{
    public partial class Form1 : Form
    {
        private delegate string fTxDelegate(string s);
        fTxDelegate ftx;
       
        public Form1()
        {
            InitializeComponent();
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            btProcess.Enabled = true;
            ftx = new fTxDelegate(Lower.fixText);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            btProcess.Enabled = true;
            ftx = new fTxDelegate(new Capital().fixText);
        }

        private void btProcess_Click(object sender, EventArgs e)
        {
            lsBox.Items.Add(ftx(txName.Text));
        }
    }
}
