using System;

namespace LibrarySystem
{
    public class LibraryItem
    {
        private readonly decimal _lateFeeMultiplier;

        public string CatalogNumber { get; }
        public string Title { get; }
        public int LoanPeriodDays { get; }

        public decimal BaseLateFee { get; private set; }
        public bool IsWithdrawn { get; private set; }

       
        public bool IsOnLoan { get; private set; }

        protected LibraryItem(string catalogNumber, string title, decimal baseLateFee,
                              int loanPeriodDays, decimal lateFeeMultiplier)
        {
            if (string.IsNullOrWhiteSpace(catalogNumber))
            {
                throw new ArgumentException("Catalog number must not be null or empty.", nameof(catalogNumber));
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title must not be null or empty.", nameof(title));
            }
            if (baseLateFee <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(baseLateFee), "Base late fee must be greater than zero.");
            }
            if (loanPeriodDays <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(loanPeriodDays), "Loan period must be greater than zero.");
            }
            if (lateFeeMultiplier <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(lateFeeMultiplier), "Late fee multiplier must be greater than zero.");
            }

            CatalogNumber = catalogNumber;
            Title = title;
            BaseLateFee = baseLateFee;
            LoanPeriodDays = loanPeriodDays;
            _lateFeeMultiplier = lateFeeMultiplier;
        }

        public decimal GetDailyLateFee()
        {
            return BaseLateFee * _lateFeeMultiplier;
        }

        internal void SetBaseLateFee(decimal newFee)
        {
            if (newFee <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(newFee), "A late fee must be greater than zero.");
            }
            BaseLateFee = newFee;
        }

        internal void Withdraw()
        {
            if (IsWithdrawn)
            {
                throw new InvalidOperationException("'" + Title + "' is already withdrawn from circulation.");
            }
            IsWithdrawn = true;
        }

        internal void Restore()
        {
            if (!IsWithdrawn)
            {
                throw new InvalidOperationException("'" + Title + "' is not withdrawn, so it cannot be restored.");
            }
            IsWithdrawn = false;
        }

        internal void MarkBorrowed()
        {
            if (IsWithdrawn)
            {
                throw new InvalidOperationException("'" + Title + "' is withdrawn from circulation and cannot be borrowed.");
            }
            if (IsOnLoan)
            {
                throw new InvalidOperationException("'" + Title + "' is already on loan.");
            }
            IsOnLoan = true;
        }

        internal void MarkReturned()
        {
            IsOnLoan = false;
        }
    }
}
