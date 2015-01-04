using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Paprotski.Lab7.ViewModels
{
    public class GroupViewModel
    {
        public GroupViewModel()
        {
            this.Student = new StudentViewModel();
            this.SelectedStudent = new StudentViewModel();
            this.Group = new ObservableCollection<StudentViewModel>();
            this.RemoveCommand = new DelegateCommand(RemoveExecuteMethod, CanRemoveExecuteMethod, true);
            this.InsertCommand = new DelegateCommand(InsertExecuteMethod, CanInsertExecuteMethod, true);
        }

        private bool CanRemoveExecuteMethod()
        {
            return this.SelectedStudent != null;
        }

        private void RemoveExecuteMethod()
        {
            this.Group.Remove(this.SelectedStudent);
        }

        private bool CanInsertExecuteMethod()
        {
            return this.Group.Count < 20;
        }

        private void InsertExecuteMethod()
        {
            this.CurrentStudent = new StudentViewModel
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

            this.Group.Add(this.CurrentStudent); 
        }

        public ICommand RemoveCommand { get; private set; }

        public ICommand InsertCommand { get; private set; }

        public StudentViewModel Student { get; set; }

        public StudentViewModel CurrentStudent { get; set; }

        public StudentViewModel SelectedStudent { get; set; }

        public ObservableCollection<StudentViewModel> Group { get; set; }
    }
}
