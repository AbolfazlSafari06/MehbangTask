using Mehbang.GUI.Desktop.Models.Students;
using Mehbang.GUI.Desktop.Services.StudentServices;
using System.Data;

namespace Mehbang.GUI.Desktop;

public partial class Home : Form
{
    private readonly StudentService _studentService = new StudentService();

    public Home()
    {
        InitializeComponent();
        GetStudentsList(string.Empty, string.Empty);
    }

    // Get Students List
    private async Task GetStudentsList(string? fullname, string? nationalCode)
    {
        this.label6.Text = "در حال بارگزاری اطلاعات";
        DisableButtons();

        try
        {
            // Get Students
            var students = await _studentService.GetAllStudentsAsync(fullname, nationalCode);

            var datatable = new DataTable();
            dataGridView1.AutoGenerateColumns = true;

            // Binding DataGridView using DataTable
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("نام و نام خانوادگی", typeof(string));
            table.Columns.Add("کد ملی", typeof(string));
            table.Columns.Add("تاریخ تولد", typeof(string));
            foreach (var student in students)
            {
                table.Rows.Add(student.Id, student.FullName, student.NationalCode, student.BirthDate.ToString("F"));
            }
            dataGridView1.DataSource = table;

        }
        catch (ApplicationException ex)
        {
            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("خطای غیرمنتظره: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        label6.Visible = false;
        EnableButtons();

    }

    // Click Add Button
    private void button2_Click(object sender, EventArgs e)
    {
        var studentForm = new AddStudent();
        studentForm.StudentFormClosed += async (s, args) =>
        {
            await GetStudentsList(string.Empty, string.Empty);
        };
        studentForm.Show();
    }

    // Click Delete Button
    private async void button4_Click(object sender, EventArgs e)
    {
        var selectedItem = dataGridView1.SelectedRows;
        if (selectedItem.Count == 0)
        {
            MessageBox.Show("لطفا ردیف دانشجو را انتخاب کنید");
            return;
        }

        // Get Selected Student Id
        int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
        DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
        int selectedStudentId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
        if (selectedStudentId == 0)
        {
            MessageBox.Show("لطفا ردیف دانشجو را انتخاب کنید");
            return;
        }

        //Confirm Delete
        string message = "آیا از حذف دانشجو مطمئن هستید؟";
        string title = "حذف دانشجو";
        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        DialogResult result = MessageBox.Show(message, title, buttons);

        if (result == DialogResult.Yes)
        {

            try
            {
                DisableButtons();
                await _studentService.DeleteStudentAsync(selectedStudentId);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطای غیرمنتظره: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            EnableButtons();
            GetStudentsList(string.Empty, string.Empty);
        }
    }

    // Click Update Button
    private void button5_Click(object sender, EventArgs e)
    {
        var selectedItem = dataGridView1.SelectedRows;
        if (selectedItem.Count == 0)
        {
            MessageBox.Show("لطفا ردیف دانشجو را انتخاب کنید");
            return;
        }
        // Get Selected Student Id
        int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
        DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
        int selectedStudentId = Convert.ToInt32(selectedRow.Cells["Id"].Value); 
        if (selectedStudentId == 0)
        {
            MessageBox.Show("لطفا ردیف دانشجو را انتخاب کنید");
            return;
        }

        var updateStudentForm = new UpdateStudent(selectedStudentId);
        updateStudentForm.StudentFormClosed += async (s, args) =>
        {
            await GetStudentsList(string.Empty, string.Empty);
        };
        updateStudentForm.Show();
    }

    // Click Search Button
    private void button3_Click(object sender, EventArgs e)
    {
        var filterFullName = textBox1.Text;
        var filternationalCode = textBox2.Text;
        GetStudentsList(filterFullName, filternationalCode);
    }

    // Click Reset Button
    private void button7_Click(object sender, EventArgs e)
    {
        textBox1.Text = string.Empty;
        textBox2.Text = string.Empty;
        GetStudentsList(string.Empty, string.Empty);
    }

    private void DisableButtons()
    {
        groupBox1.Enabled = false;
        button2.Enabled = false;
        button4.Enabled = false;
        button3.Enabled = false;
        button5.Enabled = false;
    }

    private void EnableButtons()
    {
        groupBox1.Enabled = true;
        button2.Enabled = true;
        button4.Enabled = true;
        button3.Enabled = true;
        button5.Enabled = true;
    }
}
