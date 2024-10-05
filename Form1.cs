using QuanLyStudent.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyStudent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Model1 db = new Model1();
                List<Faculty> listFalcuty = db.Faculty.ToList();
                List<Student> listStudent = db.Student.ToList();
                FillFaculty(listFalcuty);
                BindGrid(listStudent);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFaculty(List<Faculty> listFalcuty)
        {
            this.comboBox1.DataSource = listFalcuty;
            this.comboBox1.DisplayMember = "FacultyName";
            this.comboBox1.ValueMember = "FacultyID";
        }

        private void BindGrid(List<Student> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["Col_MaSV"].Value = item.StudentID;
                dataGridView1.Rows[index].Cells["Col_HoTen"].Value = item.FullName;
                dataGridView1.Rows[index].Cells["Col_Khoa"].Value = item.Faculty?.FacultyName;
                dataGridView1.Rows[index].Cells["Col_DTB"].Value = item.AverageScore?.ToString("0.0");


            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    int studentID = int.Parse(textBox1.Text);
                    string fullName = textBox2.Text;
                    int facultyID = int.Parse(comboBox1.SelectedValue.ToString());
                    decimal averageScore = decimal.Parse(textBox3.Text, System.Globalization.CultureInfo.InvariantCulture);

                    Student student = new Student()
                    {
                        StudentID = studentID,
                        FullName = fullName,
                        FacultyID = facultyID,
                        AverageScore = averageScore
                    };

                    db.Student.Add(student);
                    db.SaveChanges();

                    BindGrid(db.Student.ToList());


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                using (Model1 db = new Model1())
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int studentID = int.Parse(row.Cells["Col_MaSV"].Value.ToString());
                        Student student = db.Student.Find(studentID);
                        db.Student.Remove(student);
                    }

                    db.SaveChanges();
                    BindGrid(db.Student.ToList());
                    MessageBox.Show("Xóa thành công");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                    int studentID = int.Parse(textBox1.Text);

                    Student student = db.Student.Find(studentID);

                    if (student != null)
                    {
                        student.FullName = textBox2.Text;
                        student.FacultyID = int.Parse(comboBox1.SelectedValue.ToString());
                        student.AverageScore = decimal.Parse(textBox3.Text, System.Globalization.CultureInfo.InvariantCulture);

                        db.SaveChanges();

                        MessageBox.Show("Cập nhật dữ liệu thành công!");

                        // Reset lại dữ liệu về giá trị ban đầu
                        ResetFields();

                        BindGrid(db.Student.ToList());
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy MSSV cần sửa!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1; 
        }

    }
}
