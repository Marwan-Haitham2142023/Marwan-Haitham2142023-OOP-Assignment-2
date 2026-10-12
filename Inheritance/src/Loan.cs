using System;

namespace LibrarySystem
{
    public class Loan
    {
        public string LoanId { get; }
        public DateTime BorrowDate { get; }
        public Member Member { get; }
        public LibraryItem Item { get; }  

        public LoanStatus Status { get; private set; }
        public DateTime? ReturnDate { get; private set; }

        public DateTime DueDate
        {
            get { return BorrowDate.AddDays(Item.LoanPeriodDays); }
        }

        public decimal LateFee
        {
            get
            {
                if (Status != LoanStatus.Returned || ReturnDate == null)
                {
                    return 0m;
                }

                int lateDays = (ReturnDate.Value.Date - DueDate.Date).Days;
                if (lateDays <= 0)
                {
                    return 0m;
                }

                decimal fee = lateDays * Item.GetDailyLateFee();
                decimal discount = fee * Member.DiscountPercent / 100m;
                return fee - discount;
            }
        }

        internal Loan(string loanId, Member member, LibraryItem item, DateTime borrowDate)
        {
            if (string.IsNullOrWhiteSpace(loanId))
            {
                throw new ArgumentException("Loan ID must not be null or empty.", nameof(loanId));
            }
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (borrowDate == default(DateTime))
            {
                throw new ArgumentException("Borrow date must be a real date.", nameof(borrowDate));
            }

            LoanId = loanId;
            Member = member;
            Item = item;
            BorrowDate = borrowDate;
            Status = LoanStatus.Borrowed;
        }

        internal void MarkReturned(DateTime returnDate)
        {
            if (Status == LoanStatus.Returned)
            {
                throw new InvalidOperationException("Loan " + LoanId + " was already returned.");
            }
            if (Status != LoanStatus.Borrowed)
            {
                throw new InvalidOperationException("Loan " + LoanId + " is " + Status + " and cannot be returned.");
            }
            if (returnDate < BorrowDate)
            {
                throw new ArgumentException("Return date " + returnDate.ToString("yyyy-MM-dd") +
                    " cannot be earlier than the borrow date " + BorrowDate.ToString("yyyy-MM-dd") + ".");
            }

            Status = LoanStatus.Returned;
            ReturnDate = returnDate;
            Item.MarkReturned();
        }

     
        internal void MarkLost()
        {
            if (Status == LoanStatus.Returned)
            {
                throw new InvalidOperationException("Loan " + LoanId + " was already returned and cannot be marked as lost.");
            }
            if (Status != LoanStatus.Borrowed)
            {
                throw new InvalidOperationException("Loan " + LoanId + " is already " + Status + ".");
            }

            Status = LoanStatus.Lost;
        }
    }
}
