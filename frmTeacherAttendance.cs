﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeacherAttendance.helper;
using TeacherAttendance.model;

namespace TeacherAttendance
{
    public partial class frmTeacherAttendance : Form
    {
        List<Attendance> attendence_list = new List<Attendance>();
        private AttendanceManagement attendance;
        private BindingSource bindAttendance;
        private int row_selected;
        public frmTeacherAttendance()
        {
            InitializeComponent();
        }

        private void FrmTeacherAttendance_Load(object sender, EventArgs e)
        {
            
            attendance = new AttendanceManagement();
            ShowCourses();
            ShowTeachers();
            ShowRooms();
            
        }

        /*private void CmbCourses_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void CmbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }*/

        private void ShowCourses()
        {
            cmbCourses.DataSource = null;
            cmbCourses.DisplayMember = "CourseName";
            cmbCourses.ValueMember = "CourseId";
            cmbCourses.DataSource = attendance.getAllCourses();
            cmbCourses.SelectedIndex = -1;
        }

        private void ShowTeachers()
        {
            cmbTeacherName.DataSource = null;
            cmbTeacherName.DisplayMember = "TeacherName";
            cmbTeacherName.ValueMember = "TeacherId";
            cmbTeacherName.DataSource = attendance.getAllTeachers();
            cmbTeacherName.SelectedIndex = -1;

        }

        private void ShowRooms()
        {
            cmbRoom.DataSource = null;
            cmbRoom.DisplayMember = "RoomName";
            cmbRoom.ValueMember = "RoomId";
            cmbRoom.DataSource = attendance.getAllRooms();
            cmbRoom.SelectedIndex = -1;

        }
        private void CmbCourses_SelectionChangeCommitted(object sender, EventArgs e)
        {
            

            string value = "";
            

            int id = ((Course)((ComboBox)sender).SelectedItem).CourseId;

            if(id != 0)
            {
                return;
            }

            if (Prompt.InputBox("New course", "New course name:", ref value) == DialogResult.OK)
            {
                if (value.Trim().Length > 0)
                {
                    attendance.addNewCourse(value.Trim());
                    ShowCourses();
                }


            }
        }

        private void CmbTeacherName_SelectionChangeCommitted(object sender, EventArgs e)
        {


            string value = "";


            int id = ((Teacher)((ComboBox)sender).SelectedItem).TeacherId;

            if (id != 0)
            {
                return;
            }

            if (Prompt.InputBox("New teacher", "New teacher name:", ref value) == DialogResult.OK)
            {
                if (value.Trim().Length > 0)
                {
                    attendance.addNewTeacher(value.Trim());
                    ShowTeachers();
                }


            }


        }

        private void CmbRoom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string value = "";


            int id = ((Room)((ComboBox)sender).SelectedItem).RoomId;

            if (id != 0)
            {
                return;
            }

            if (Prompt.InputBox("New Room/Lab", "New Room/Lab name:", ref value) == DialogResult.OK)
            {
                if (value.Trim().Length > 0)
                {
                    attendance.addNewRoom(value.Trim());
                    ShowRooms();
                }


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cmbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(cmbTeacherName.SelectedIndex == -1 || cmbCourses.SelectedIndex == -1|| cmbRoom.SelectedIndex == -1)
            {
                MessageBox.Show("Fields cannot be empty", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            attendence_list.Add(new Attendance(cmbTeacherName.SelectedIndex,
                                 cmbCourses.SelectedIndex,
                                 cmbRoom.SelectedIndex,
                                 dateTimePicker2.Text,
                                 dateTimePicker3.Text,
                                 dateTimePicker1.Text,
                                 textBox2.Text));
            var bindingList = new BindingList<Attendance>(attendence_list);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;



        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button2.Enabled = true;
            row_selected = e.RowIndex;
            cmbTeacherName.SelectedIndex = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            cmbCourses.SelectedIndex = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            cmbRoom.SelectedIndex = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            dateTimePicker3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[row_selected].Cells[0].Value = cmbTeacherName.SelectedIndex;
            dataGridView1.Rows[row_selected].Cells[1].Value = cmbCourses.SelectedIndex;
            dataGridView1.Rows[row_selected].Cells[2].Value = cmbRoom.SelectedIndex;
            dataGridView1.Rows[row_selected].Cells[3].Value = dateTimePicker2.Text;
            dataGridView1.Rows[row_selected].Cells[4].Value = dateTimePicker3.Text;
            dataGridView1.Rows[row_selected].Cells[5].Value = textBox2.Text;
            dataGridView1.Rows[row_selected].Cells[6].Value = dateTimePicker1.Text;
            button2.Enabled = false;

        }
    }
}
