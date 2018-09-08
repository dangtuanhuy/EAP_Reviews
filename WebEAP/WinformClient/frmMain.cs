using Newtonsoft.Json.Linq;
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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GetAll();
        }
        public async Task GetAll()
        {
            string url = "http://localhost:50180/api/Students";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(url);

            // Chỉnh sửa và Fill Form
            DataTable table = new DataTable();
            table.Columns.Add("st_code");
            table.Columns.Add("st_name");
            table.Columns.Add("st_email");
            table.Columns.Add("st_phone");
            table.Columns.Add("st_details");
            //Kết thúc
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;

                JArray parsed = JArray.Parse(responseData.ToString());

                Console.WriteLine("------------------------List of Student ' Information-------------");
                foreach (var pair in parsed)
                {
                    //dùng DataRow
                    DataRow row = table.NewRow(); //QT
                    JObject obj = JObject.Parse(pair.ToString());
                    int count = 0; //QT
                    foreach (var s in obj)
                    {
                        //Console.WriteLine("{0} : {1}", s.Key, s.Value.ToString().Trim());
                        row[count++] = s.Value.ToString();
                    }
                    // Console.WriteLine();
                    table.Rows.Add(row);
                }
                dataGridView1.DataSource = table;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmCreate frm = new frmCreate();
            frm.Show();
            this.Hide();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            Student st = new Student()
            {
                Student_Code = dataGridView1.Rows[index].Cells[0].Value.ToString(),
                Student_Name = dataGridView1.Rows[index].Cells[1].Value.ToString(),
                Student_Email = dataGridView1.Rows[index].Cells[2].Value.ToString(),
                Student_Phone = dataGridView1.Rows[index].Cells[3].Value.ToString(),
                Student_Details = dataGridView1.Rows[index].Cells[4].Value.ToString()
            };
            frmEdit frm = new frmEdit(st);
            frm.Show();
            this.Hide();
        }
    }
}
