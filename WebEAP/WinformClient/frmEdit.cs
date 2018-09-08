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
    public partial class frmEdit : Form
    {
        public frmEdit()
        {
            InitializeComponent();
        }
        public frmEdit(Student st)
        {
            InitializeComponent();
            txtStudentCode.Text = st.Student_Code;
            txtName.Text = st.Student_Name;
            txtEmail.Text = st.Student_Email;
            txtPhone.Text = st.Student_Phone;
            txtDetails.Text = st.Student_Details;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
           
            txtName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtDetails.Text = "";
        }
        static async Task UpdateStudent(string id, string name, string email, string phone, string details)
        {
            string url = "http://localhost:50180/api/Students";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(url + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                Student student = await response.Content.ReadAsAsync<Student>();
                if (name != "")
                    student.Student_Name = name;
                if (email != "")
                    student.Student_Email = email;
                if (phone != "")
                    student.Student_Phone = phone;
                if (details != "")
                    student.Student_Details = details;
                response = await client.PutAsJsonAsync(url + "/" + id, student);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Ok", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmMain frm = new frmMain();
                    frm.Show();
                    
                }
                else
                {
                    MessageBox.Show("Ok", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateStudent(txtStudentCode.Text, txtName.Text, txtPhone.Text, txtEmail.Text, txtDetails.Text);
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain frm = new frmMain();
            frm.Show();
            this.Hide();
        }
    }
}
