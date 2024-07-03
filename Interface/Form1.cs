using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int iparam);
        public void panelform(object frm)
        {
            Form childForm = frm as Form;

            if (childForm != null && !childForm.IsDisposed)
            {
                // Check if the form is already a child
                if (childForm.MdiParent == this)
                {
                    // The form is already a child, so bring it to the front
                    childForm.BringToFront();
                }
                else
                {
                    // Set properties for new form
                    childForm.MdiParent = this;
                    // childForm.Dock = DockStyle.Fill;

                    // Show the form
                    childForm.Show();
                }
            }
        }
        private void pbmaxim_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbmin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            pbmax.Visible = true;
            pbmin.Visible = false;
        }

        private void pbmax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            pbmax.Visible = false;
            pbmin.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                pbmax.Visible = false;
                pbmin.Visible = true;
            }
            if (Properties.Settings.Default.interfacepositioning == "0")
            {
                foreach (Form a in MdiChildren)
                {
                    if (a is QuickScan)
                    {
                        //the form is open, set focus to it
                        a.BringToFront();
                        a.Activate();
                        break;
                    }
                }
                if (ActiveMdiChild == null || ActiveMdiChild.Name != "QuickScan")
                {
                    //no forms are open
                    //open one
                    QuickScan Accounts = new QuickScan(this);
                    Accounts.MdiParent = this;
                    Accounts.Dock = DockStyle.Fill;
                    Accounts.Show();
                }
            }
            else
            {
                foreach (Form a in MdiChildren)
                {
                    if (a is ResultForm)
                    {
                        //the form is open, set focus to it
                        a.BringToFront();
                        a.Activate();
                        break;
                    }
                }
                if (ActiveMdiChild == null || ActiveMdiChild.Name != "ResultForm")
                {
                    //no forms are open
                    //open one
                    ResultForm Accounts = new ResultForm(this);
                    Accounts.MdiParent = this;
                    Accounts.Dock = DockStyle.Fill;
                    Accounts.Show();
                }
                //panelform(new ResultForm(this));
            }
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is Button)
                {
                    Button textBox = (Button)ctrl;
                    textBox.BackColor = Color.FromArgb(235, 235, 235);
                }
            }
            button1.BackColor = Color.White;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (Properties.Settings.Default.interfacepositioning == "0")
                {
                foreach (Form a in MdiChildren)
                {
                    if (a is QuickScan)
                    {
                        //the form is open, set focus to it
                        a.BringToFront();
                        a.Activate();
                        break;
                    }
                }
                if (ActiveMdiChild == null || ActiveMdiChild.Name != "QuickScan")
                {
                    //no forms are open
                    //open one
                    QuickScan Accounts = new QuickScan(this);
                    Accounts.MdiParent = this;
                    Accounts.Dock = DockStyle.Fill;
                    Accounts.Show();
                }
            }
                else
            {
                foreach (Form a in MdiChildren)
                {
                    if (a is ResultForm)
                    {
                        //the form is open, set focus to it
                        a.BringToFront();
                        a.Activate();
                        break;
                    }
                }
                if (ActiveMdiChild == null || ActiveMdiChild.Name != "ResultForm")
                {
                    //no forms are open
                    //open one
                    ResultForm Accounts = new ResultForm(this);
                    Accounts.MdiParent = this;
                    Accounts.Dock = DockStyle.Fill;
                    Accounts.Show();
                }
                //panelform(new ResultForm(this));
            }
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is Button)
                {
                    Button textBox = (Button)ctrl;
                    textBox.BackColor = Color.FromArgb(235, 235, 235);
                }
            }
            button1.BackColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Form a in MdiChildren)
            {
                if (a is File_Scan)
                {
                    //the form is open, set focus to it
                    a.BringToFront();
                    a.Activate();
                    break;
                }
            }
            if (ActiveMdiChild == null || ActiveMdiChild.Name != "File_Scan")
            {
                //no forms are open
                //open one
                File_Scan Accounts = new File_Scan();
                Accounts.MdiParent = this;
                Accounts.Dock = DockStyle.Fill;
                Accounts.Show();
            }
            //panelform(new File_Scan());
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is Button)
                {
                    Button textBox = (Button)ctrl;
                    textBox.BackColor = Color.FromArgb(235, 235, 235);
                }
            }
            button3.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Form a in MdiChildren)
            {
                if (a is Tasks)
                {
                    //the form is open, set focus to it
                    a.BringToFront();
                    a.Activate();
                    break;
                }
            }
            if (ActiveMdiChild == null || ActiveMdiChild.Name != "Tasks")
            {
                //no forms are open
                //open one
                Tasks Accounts = new Tasks();
                Accounts.MdiParent = this;
                Accounts.Dock = DockStyle.Fill;
                Accounts.Show();
            }
            //panelform(new Tasks());
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is Button)
                {
                    Button textBox = (Button)ctrl;
                    textBox.BackColor = Color.FromArgb(235, 235, 235);
                }
            }
            button2.BackColor = Color.White;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Form a in MdiChildren)
            {
                if (a is Services)
                {
                    //the form is open, set focus to it
                    a.BringToFront();
                    a.Activate();
                    break;
                }
            }
            if (ActiveMdiChild == null || ActiveMdiChild.Name != "Services")
            {
                //no forms are open
                //open one
                Services Accounts = new Services();
                Accounts.MdiParent = this;
                Accounts.Dock = DockStyle.Fill;
                Accounts.Show();
            }
            //  panelform(new Services());
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is Button)
                {
                    Button textBox = (Button)ctrl;
                    textBox.BackColor = Color.FromArgb(235, 235, 235);
                }
            }
            button4.BackColor = Color.White;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

       
    }
}
