using Mehbang.GUI.Desktop.Models.Students;
using Mehbang.GUI.Desktop.Services.StudentServices;

namespace Mehbang.GUI.Desktop;

public partial class UpdateStudent : Form
{
    public event EventHandler StudentFormClosed;
    public readonly StudentService _studentService;
    private int _studentId;
    public UpdateStudent(int Id)
    {
        _studentService = new StudentService();
        _studentId = Id;
        InitializeComponent();
        GetStudent(Id);
    }

    private async Task GetStudent(int id)
    {
        groupBox1.Enabled = false;
       
        // Get Student
        try
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            textBox1.Text = student.FullName;
            textBox2.Text = student.NationalCode;
            dateTimePicker1.Value = student.BirthDate; 
        }
        catch (ApplicationException ex)
        {
            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("خطای غیرمنتظره: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        groupBox1.Enabled = true;

    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        StudentFormClosed?.Invoke(this, EventArgs.Empty);
    }

    // Click Close 
    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    //Click Update
    private async void button1_Click(object sender, EventArgs e)
    {
        var fullName = textBox1.Text;
        var nationalCode = textBox2.Text;
        var birthDate = dateTimePicker1.Value;

        if (string.IsNullOrEmpty(fullName))
        {
            MessageBox.Show("لطفا نام و نام خانوادگی را وارد کنید");
            return;
        }
        if (string.IsNullOrEmpty(nationalCode))
        {
            MessageBox.Show("لطفا کدملی را وارد کنید");
            return;
        }
        if (birthDate == null)
        {
            MessageBox.Show("لطفا تاریخ تولد را وارد کنید");
            return;
        }


        // Update Student
        try
        {
            groupBox1.Enabled = false;
            var student = new UpdateStudentModel
            {
                BirthDate = birthDate,
                FullName = fullName,
                Id = _studentId,
                NationalCode = nationalCode
            };

            await _studentService.UpdateStudentAsync(student);
            MessageBox.Show("تغیرات با موفقیت انجام شد");
            groupBox1.Enabled = true;
        }
        catch (ApplicationException ex)
        {
            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("خطای غیرمنتظره: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        groupBox1.Enabled = true;

        Close();
    }
}
