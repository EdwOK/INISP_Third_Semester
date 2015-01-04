using System;
using System.Collections.Generic;

namespace Paprotski.Lab2
{
    public class Coupe : IEquatable<Coupe>, IDisposable
    {
        #region Private Member Variables

        private bool disposed = false;

        #endregion 

        #region Ctors

        public Coupe(bool isVipStatus = false, bool airConditioning = false, int seatsNumber = 4, int seatsNumberOccupied = 0)
        {
            this.IsVipStatus = isVipStatus;
            this.SeatsNumber = seatsNumber;
            this.SeatsNumberOccupied = seatsNumberOccupied;
            this.AirConditioning = airConditioning;
        }

        ~Coupe()
        {
            this.Dispose(false);
        }

        #endregion 

        #region Public Properties

        public bool IsVipStatus { get; set; }

        public bool AirConditioning { get; set; }

        public int SeatsNumber { get; set; }

        public int SeatsNumberOccupied { get; set; }

        #endregion 

        #region Public Methods

        public bool Equals(Coupe other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return other.AirConditioning.Equals(this.AirConditioning) && other.IsVipStatus.Equals(this.IsVipStatus)
                   && other.SeatsNumber.Equals(this.SeatsNumber)
                   && other.SeatsNumberOccupied.Equals(this.SeatsNumberOccupied);
        }

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
                    GC.ReRegisterForFinalize(this);
                }

                this.disposed = true;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "VIP: {0} Conditioner: {1} Seats:{2} Occupied:{3}",
                this.IsVipStatus,
                this.AirConditioning,
                this.SeatsNumber,
                this.SeatsNumberOccupied);
        }

        #endregion

    }
}
