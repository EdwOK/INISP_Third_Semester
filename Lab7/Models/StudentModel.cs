using System;
using System.Runtime.Serialization;

namespace Paprotski.Lab7.Models
{
    [DataContract]
    public class StudentModel : IComparable<StudentModel>, IEquatable<StudentModel>
    {
        #region Public Properties

        [DataMember(Order = 0)]
        public string Faculty { get; set; }

        [DataMember(Order = 1)]
        public double? GPA { get; set; }

        [DataMember(Order = 2)]
        public int? GroupNumber { get; set; }

        [DataMember(Order = 3)]
        public string Name { get; set; }

        [DataMember(Order = 4)]
        public string Speciality { get; set; }

        [DataMember(Order = 5)]
        public string Surname { get; set; }

        [DataMember(Order = 6)]
        public string Gender { get; set; }

        #endregion

        public bool Equals(StudentModel other)
        {
            if (ReferenceEquals(this, null) || ReferenceEquals(other, null))
            {
                return false; 
            }

            return this.Name.Equals(other.Name) && this.Surname.Equals(other.Surname) && this.Gender.Equals(other.Gender)
                   && this.Speciality.Equals(other.Speciality) && this.Faculty.Equals(other.Faculty)
                   && this.GPA.Equals(other.GPA) && this.GroupNumber.Equals(other.GroupNumber);
        }

        public int CompareTo(StudentModel other)
        {
            if (ReferenceEquals(this, null) || ReferenceEquals(other, null))
            {
                throw new NullReferenceException();
            }

            if (ReferenceEquals(this.GPA, null) || ReferenceEquals(other.GPA, null))
            {
                throw new NullReferenceException();
            }

            return this.GPA.Value.CompareTo(other.GPA.Value);
        }
    }
}