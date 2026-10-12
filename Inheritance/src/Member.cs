using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class Member : Person
    {
        
        private readonly List<Loan> _loans = new List<Loan>();

        public IReadOnlyList<Loan> Loans
        {
            get { return _loans.AsReadOnly(); }
        }

        public int MaxLoans { get; }
        public decimal DiscountPercent { get; }

        public int ActiveLoanCount
        {
            get
            {
                int count = 0;
                foreach (Loan loan in _loans)
                {
                    if (loan.Status == LoanStatus.Borrowed)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        protected Member(string personId, string fullName, string phone, int maxLoans, decimal discountPercent)
            : base(personId, fullName, phone)
        {
            if (maxLoans <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLoans), "Maximum loans must be greater than zero.");
            }
            if (discountPercent < 0m || discountPercent > 100m)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercent), "Discount percent must be between 0 and 100.");
            }

            MaxLoans = maxLoans;
            DiscountPercent = discountPercent;
        }

       
        public Loan Borrow(LibraryItem item, string loanId, DateTime borrowDate)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (ActiveLoanCount >= MaxLoans)
            {
                throw new InvalidOperationException(
                    FullName + " already has " + ActiveLoanCount + " active loans (maximum " + MaxLoans + ").");
            }

          
            Loan loan = new Loan(loanId, this, item, borrowDate);
            item.MarkBorrowed();
            _loans.Add(loan);
            return loan;
        }
    }
}
