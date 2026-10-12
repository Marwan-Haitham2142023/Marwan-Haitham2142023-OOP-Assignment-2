namespace LibrarySystem
{
    public class PremiumMember : Member
    {
        private const int PointsPerReturnedLoan = 5;

       
        public int ReadingPoints
        {
            get
            {
                int returned = 0;
                foreach (Loan loan in Loans)
                {
                    if (loan.Status == LoanStatus.Returned)
                    {
                        returned++;
                    }
                }
                return returned * PointsPerReturnedLoan;
            }
        }

       
        public PremiumMember(string personId, string fullName, string phone, decimal discountPercent)
            : base(personId, fullName, phone, 10, discountPercent)
        {
        }
    }
}
