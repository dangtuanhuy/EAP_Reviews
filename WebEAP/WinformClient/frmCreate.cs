using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformClient
{
    public partial class frmCreate : Form
    {
        public frmCreate()
        {
            InitializeComponent();
        }

        private void frmCreate_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain frm = new frmMain();
            frm.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtStudentCode.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtDetails.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewStudent(txtStudentCode.Text, txtName.Text, txtPhone.Text, txtEmail.Text, txtDetails.Text);
          
        }
        static async Task AddNewStudent(string id, string name, string email, string phone, string details)
        {
            string url = "http://localhost:50180/api/Students";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(url);
        
            Student st = new Student()
            {
                Student_Code = id,
                Student_Name = name,
                Student_Email = email,
                Student_Phone = phone,
                Student_Details = details
            };
            response = await client.PostAsJsonAsync(url, st);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Ok", "Infor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMain frm = new frmMain();
                frm.Show();
              
            }
            else
            {
                MessageBox.Show("Fail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
