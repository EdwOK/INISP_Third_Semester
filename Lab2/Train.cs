using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Paprotski.Lab2
{
    public class Train : IEnumerable<Wagon>, IEquatable<Train>, IDisposable
    {
        #region Private Member Variables

        private bool disposed = false;

        #endregion 

        #region Ctors 

        /// <summary>
        /// Initializes a new instance of the <see cref="Train"/> class.
        /// </summary>
        /// <param name="yearIssue">
        /// The year issue.
        /// </param>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="startRoute">
        /// The start route.
        /// </param>
        /// <param name="endRoute">
        /// The end route.
        /// </param>
        /// <param name="departureTime">
        /// The departure time.
        /// </param>
        /// <param name="arrivalTime">
        /// The arrival time.
        /// </param>
        public Train(int yearIssue, TrainType serviceType, string startRoute, string endRoute, DateTime departureTime, DateTime arrivalTime)
        {
            this.YearIssue = yearIssue;
            this.ServiceType = serviceType;
            this.StartRoute = startRoute;
            this.EndRoute = endRoute;
            this.DepartureTime = departureTime;
            this.ArrivalTime = arrivalTime;
            this.Wagons = new ArrayList<Wagon>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Train"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The capacity.
        /// </param>
        public Train(int capacity, int yearIssue, TrainType serviceType, string startRoute, string endRoute, DateTime departureTime, DateTime arrivalTime) 
            : this(yearIssue, serviceType, startRoute, endRoute, departureTime, arrivalTime)
        {
            this.Wagons = new ArrayList<Wagon>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Train"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public Train(Wagon[] items, int yearIssue, TrainType serviceType, string startRoute, string endRoute, DateTime departureTime, DateTime arrivalTime)
            : this(yearIssue, serviceType, startRoute, endRoute, departureTime, arrivalTime)
        {
            this.Wagons = new ArrayList<Wagon>(items);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Train"/> class. 
        /// </summary>
        ~Train()
        {
            this.Dispose(false);    
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the wagons.
        /// </summary>
        public ArrayList<Wagon> Wagons { get; private set; }

        /// <summary>
        /// Gets the year issue.
        /// </summary>
        public int YearIssue { get; private set; }

        /// <summary>
        /// Gets the service type.
        /// </summary>
        public TrainType ServiceType { get; private set; }

        /// <summary>
        /// Gets or sets the start route.
        /// </summary>
        public string StartRoute { get; set; }

        /// <summary>
        /// Gets or sets the end route.
        /// </summary>
        public string EndRoute { get; set; }

        /// <summary>
        /// Gets or sets the departure time.
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the arrival time.
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<Wagon> GetEnumerator()
        {
            return this.Wagons.GetEnumerator();
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
        public bool Equals(Train other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return other.YearIssue.Equals(this.YearIssue) && other.ServiceType.Equals(this.ServiceType)
                   && other.StartRoute.Equals(this.StartRoute) && other.EndRoute.Equals(this.EndRoute)
                   && other.DepartureTime.Equals(this.DepartureTime) && other.ArrivalTime.Equals(this.ArrivalTime);
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
                    this.Wagons.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            if (builder.Length != 0)
            {
                builder.AppendFormat(
                    "Year: {0} ServiceType: {1} Start: {2} End: {3} Departure: {4} Arrival {5}",
                    this.YearIssue,
                    this.ServiceType,
                    this.StartRoute,
                    this.EndRoute,
                    this.DepartureTime,
                    this.ArrivalTime);
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
