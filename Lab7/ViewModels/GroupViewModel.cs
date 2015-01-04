using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Paprotski.Lab7.ViewModels
{
    using System.Linq;

    public class GroupViewModel
    {
        #region Ctors

        public GroupViewModel()
        {
            this.Student = new StudentViewModel();
            this.Group = new ObservableCollection<StudentViewModel>();
            this.RemoveCommand = new DelegateCommand(this.RemoveExecuteMethod, this.CanRemoveExecuteMethod, false);
            this.InsertCommand = new DelegateCommand(this.InsertExecuteMethod, this.CanInsertExecuteMethod, true);
        }

        #endregion

        #region Public Properties

        public ObservableCollection<StudentViewModel> Group { get; set; }

        public StudentViewModel Student { get; set; }

        public ICommand InsertCommand { get; private set; }

        public ICommand RemoveCommand { get; private set; }

        #endregion

        #region Methods

        private bool CanInsertExecuteMethod()
        {
            return this.Group.Count < 20;
        }

        private bool CanRemoveExecuteMethod()
        {
            return this.Group.Count > 0;
        }

        private void InsertExecuteMethod()
        {
            var currentStudent = new StudentViewModel
                                     {
                                         Student =
                                             {
                                                 Name = this.Student.Student.Name,
                                                 Faculty = this.Student.Student.Faculty,
                                                 Gender = this.Student.Student.Gender,
                                                 GPA = this.Student.Student.GPA,
                                                 GroupNumber = this.Student.Student.GroupNumber,
                                                 Speciality = this.Student.Student.Speciality,
                                                 Surname = this.Student.Student.Surname
                                             }
                                     };

            this.Group.Add(currentStudent);
        }

        private void RemoveExecuteMethod()
        {
            var currentStudent = new StudentViewModel
                                     {
                                         Student =
                                             {
                                                 Name = this.Student.Student.Name,
                                                 Faculty = this.Student.Student.Faculty,
                                                 Gender = this.Student.Student.Gender,
                                                 GPA = this.Student.Student.GPA,
                                                 GroupNumber = this.Student.Student.GroupNumber,
                                                 Speciality = this.Student.Student.Speciality,
                                                 Surname = this.Student.Student.Surname
                                             }
                                     };

            var thisRemoveStudent = this.Group.FirstOrDefault(studentViewModel => studentViewModel.Student.Equals(currentStudent.Student));

            if (thisRemoveStudent != null)
            {
                this.Group.Remove(thisRemoveStudent);
            }
        }

        #endregion
    }
}