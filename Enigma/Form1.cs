using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enigma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            for (int i = 1; i < 27; i++)
            {
                comboBox4.Items.Add(i);
                comboBox5.Items.Add(i);
                comboBox6.Items.Add(i);
            }
                comboBox1.Items.Add("Rotor I");
                comboBox1.Items.Add("Rotor II");
                comboBox1.Items.Add("Rotor III");
                comboBox1.Items.Add("Rotor IV");
                comboBox1.Items.Add("Rotor V");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            crypto x = new crypto();
            
            short[] a = new short[3];
            a[0] = 1;
            a[1] = 1;
            a[2] = 1;

            rotaNames[] z = new rotaNames[3];
            z[0] = rotaNames.rotaFour;
            z[1] = rotaNames.rotaOne;
            z[2] = rotaNames.rotaThree;

            x.ThreeRotaSelections(z, a);
            string zzzzzz = x.encrypt_sentence("Hello World!");



            crypto J = new crypto();
            J.ThreeRotaSelections(z, a);
            string xxxxx = J.decrypt_sentence("FVNNZCZKNA");
            //char test = x.EncryptLetter('A');

            //for (int i = 1; i <100000;i++) {
            //    x.EncryptLetter('A');
            //}

        }

     

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                comboBox2.Enabled = true;
                comboBox5.Enabled = true;
            }
            comboBox2.Items.Clear();
            foreach (String item in comboBox1.Items) {
                if (item != comboBox1.Text) {
                    comboBox2.Items.Add(item);
                }
             }
            comboBox2.Text = "Choose a Rotor...";
            comboBox3.Text = "Choose a Rotor...";
            comboBox3.Items.Clear();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                comboBox3.Enabled = true;
                comboBox6.Enabled = true;
            }
            comboBox3.Items.Clear();
            foreach (String item in comboBox2.Items)
            {
                if (item != comboBox2.Text)
                {
                    comboBox3.Items.Add(item);
                }
            }
            comboBox3.Text = "Choose a Rotor...";
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            comboBox1.Text = "Choose a Rotor...";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            String text = richTextBox1.Text.Replace(" ", "");
            if (checkRotorNum(comboBox4.Text) && checkRotorNum(comboBox5.Text) && checkRotorNum(comboBox6.Text)
                && checkRotorName(comboBox1.Text) && checkRotorName(comboBox2.Text) && checkRotorName(comboBox3.Text)
                && text !="")
            {
                crypto xxx = new crypto();
                rotaNames[] ListOne = new rotaNames[3];
                ListOne[0] = (rotaChoice(comboBox1.Text));
                ListOne[1] = (rotaChoice(comboBox2.Text));
                ListOne[2] = (rotaChoice(comboBox3.Text));

                short[] ListTwo = new short[3];
                ListTwo[0] = short.Parse(comboBox4.Text);
                ListTwo[1] = short.Parse(comboBox5.Text);
                ListTwo[2] = short.Parse(comboBox6.Text);
                xxx.ThreeRotaSelections(ListOne, ListTwo);
                //if (radioButton1.Checked == true)
                //{
                    richTextBox2.Text = xxx.encrypt_sentence(richTextBox1.Text);
                //}
                //else
                //{
                //    richTextBox2.Text = xxx.decrypt_sentence(richTextBox1.Text);
                //}
            }
            else
            {
                MessageBox.Show("Please check rotor settings and try again. Ensure there is a message to encrypt/decrypt.", "Incorrect Format",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            }
        }

        private rotaNames rotaChoice(String rotaName_P)
        {
            rotaNames x;
            if (rotaName_P== "Rotor I")
            {
                x = rotaNames.rotaOne;
            }
            else if (rotaName_P == "Rotor II")
            {
                x = rotaNames.rotaTwo;
            }
            else if (rotaName_P == "Rotor III")
            {
                x = rotaNames.rotaThree;
            }
            else if (rotaName_P == "Rotor IV")
            {
                x = rotaNames.rotaFour;
            }
            else
            {
                x = rotaNames.rotaFive;
            }

            return x;
        }

        private bool checkRotorNum(String P_num)
        {
            bool check = false;
            try
            {
                int num = int.Parse(P_num);
                if (num >= 1 && num <= 26)
                {
                    check = true;
                }
            }
            catch
            {
                check = false;
            }

            return check;
        }
        private bool checkRotorName(String p_name)
        {
            bool check = false;
            if (p_name == "Rotor I" || p_name == "Rotor II" || p_name == "Rotor III" 
                || p_name == "Rotor IV" || p_name == "Rotor V" ) {
                check = true;
            }
            else
            {
                check = false;
            }
            return check;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
