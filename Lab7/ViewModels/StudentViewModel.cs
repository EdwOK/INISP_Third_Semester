using Paprotski.Lab7.Models;

namespace Paprotski.Lab7.ViewModels
{
    public class StudentViewModel : BindableBase
    {
        private StudentModel student;

        public StudentViewModel()
        {
            this.Student = new StudentModel();
        }

        public StudentModel Student
        {
            get
            {
                return this.student;
            }
            set
            {
                this.SetProperty(ref this.student, value);
            }
        }
    }
}
