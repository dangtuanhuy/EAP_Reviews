using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCF_Winform.ServiceReference1;

namespace WCF_Winform
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            try
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                dataGridView1.DataSource = client.GetStudents();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                //Create new Student Demo
                txtCode.Text = txtName.Text = txtEmail.Text = txtPhone.Text = txtDetails.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow != null)
                {
                    //Manual binding for demo purpose only
                    txtCode.Text = selectedRow.Cells["Student_Code"].Value.ToString();
                    txtName.Text = selectedRow.Cells["Student_Name"].Value.ToString();
                    txtEmail.Text = selectedRow.Cells["Student_Email"].Value.ToString();
                    txtPhone.Text = selectedRow.Cells["Student_Phone"].Value.ToString();
                    txtDetails.Text = selectedRow.Cells["Student_Details"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                if (MessageBox.Show("Are you sure you want to delete this student?", "frmMain", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //int deleteStudentCode = int.Parse(dataGridView1.SelectedRows[0].Cells["LecturerID"].Value.ToString());
                    String deleteStudentCode = dataGridView1.SelectedRows[0].Cells["Student_Code"].Value.ToString();
                    if (client.DeleteStudent(deleteStudentCode))
                    {
                        dataGridView1.DataSource = client.GetStudents();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                dataGridView1.DataSource = client.GetStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                bool isNewStudent = true;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells["Student_Code"].Value.ToString() == txtCode.Text)
                    {
                        isNewStudent = false;
                        break;
                    }
                }
                //Student_Code = int.Parse(txtid.Text)
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                Student student = new Student() { Student_Code = txtCode.Text, Student_Name = txtName.Text, Student_Email = txtEmail.Text, Student_Phone = txtPhone.Text, Student_Details = txtDetails.Text };
                if (isNewStudent ? client.AddNewStudent(student) : client.UpdateStudent(student))
                {
                    dataGridView1.DataSource = client.GetStudents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
