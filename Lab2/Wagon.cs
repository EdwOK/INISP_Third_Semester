using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Paprotski.Lab2
{
    public class Wagon : IEnumerable<Coupe>, IEquatable<Wagon>, IDisposable
    {
        #region Private Member Variables

        private bool disposed = false;

        #endregion 

        #region Ctors 

        /// <summary>
        /// Initializes a new instance of the <see cref="Wagon"/> class.
        /// </summary>
        /// <param name="yearIssue">
        /// The year issue train.
        /// </param>
        /// <param name="width">
        /// The width of the wagon. 
        /// </param>
        /// <param name="length">
        /// The length of the wagon.
        /// </param>
        /// <param name="velocity">
        /// The velocity train.
        /// </param>
        public Wagon(int yearIssue, int width, int length, int velocity)
        {
            this.YearIssue = yearIssue;
            this.Width = width;
            this.Length = length;
            this.Velocity = velocity;
            this.Coupes = new ArrayList<Coupe>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wagon"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The capacity.
        /// </param>
        public Wagon(int capacity, int yearIssue, int width, int length, int velocity)
            : this(yearIssue, width, length, velocity)
        {
            this.Coupes = new ArrayList<Coupe>(capacity);    
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wagon"/> class.
        /// </summary>
        /// <param name="arrayList">
        /// The array list.
        /// </param>
        public Wagon(Coupe[] arrayList, int yearIssue, int width, int length, int velocity) 
            : this(yearIssue, width, length, velocity)
        {
            this.Coupes = new ArrayList<Coupe>(arrayList);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Wagon"/> class. 
        /// </summary>
        ~Wagon()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the coupes.
        /// </summary>
        public ArrayList<Coupe> Coupes { get; private set; }

        /// <summary>
        /// Gets or sets the year issue.
        /// </summary>
        public int YearIssue { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public int Velocity { get; set; }

        #endregion 

        #region Public Methods

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<Coupe> GetEnumerator()
        {
            return this.Coupes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(Wagon other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return other.YearIssue.Equals(this.YearIssue) && other.Width.Equals(this.Width)
                   && other.Length.Equals(this.Length) && other.Velocity.Equals(this.Velocity);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Coupes.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// The to string object. 
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            if (this.Length != 0)
            {
                builder.AppendFormat(
                    "Year: {0} Witdh: {1} Length: {2} Velocity: {3}\n",
                    this.YearIssue,
                    this.Width,
                    this.Length,
                    this.Velocity);
            }

            foreach (var item in this)
            {
                builder.AppendLine(item.ToString());
            }

            return builder.ToString();
        }

        #endregion
    }
}
