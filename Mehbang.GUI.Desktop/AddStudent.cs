using Mehbang.GUI.Desktop.Models.Students;
using Mehbang.GUI.Desktop.Services.StudentServices;

namespace Mehbang.GUI.Desktop;

public partial class AddStudent : Form
{
    public event EventHandler StudentFormClosed;
    private readonly StudentService _studentService = new StudentService();
   
    public AddStudent()
    {
        InitializeComponent();
    }

    //Click Close Button
    private void button2_Click(object sender, EventArgs e)
    {
        this.Close();
    }
    
    // Click Add Button
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
            MessageBox.Show("لطفا  کدملی را وارد کنید");
            return;
        }
        if (birthDate == null)
        {
            MessageBox.Show("لطفا  تاریخ تولد را وارد کنید");
            return;
        }
        button1.Enabled = false;
        button2.Enabled = false;
        textBox1.Enabled = false;
        textBox2.Enabled = false;
        dateTimePicker1.Enabled = false;

        await CreateStudentAsync(fullName, nationalCode, birthDate);

        textBox1.Text= string.Empty;
        textBox2.Text = string.Empty;
        button1.Enabled = true;
        button2.Enabled = true;
        textBox1.Enabled = true;
        textBox2.Enabled = true;
        dateTimePicker1.Enabled = true;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        StudentFormClosed?.Invoke(this, EventArgs.Empty);
    }

    private async Task CreateStudentAsync(string fullName, string nationalCode, DateTime birthDate)
    {
        try
        {
            var newStudent = new CreateStudentModel { BirthDate = birthDate, FullName = fullName, NationalCode = nationalCode };
            await _studentService.AddStudentAsync(newStudent);
            MessageBox.Show("دانشجو با موفقیت افزوده شد");
        }
        catch (ApplicationException ex)
        {
            MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("خطای غیرمنتظره: " + ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
