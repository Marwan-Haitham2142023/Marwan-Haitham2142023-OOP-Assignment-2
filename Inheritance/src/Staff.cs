using System;

namespace LibrarySystem
{
    public class Staff : Person
    {
        
        private readonly decimal _allowance;

        public DateTime HireDate { get; }
        public decimal MonthlySalary { get; private set; }

        protected Staff(string personId, string fullName, string phone,
                        DateTime hireDate, decimal monthlySalary, decimal allowance)
            : base(personId, fullName, phone)
        {
            if (hireDate == default(DateTime))
            {
                throw new ArgumentException("Hire date must be a real date.", nameof(hireDate));
            }
            if (monthlySalary <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(monthlySalary), "Monthly salary must be greater than zero.");
            }
            if (allowance < 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(allowance), "Allowance cannot be negative.");
            }

            HireDate = hireDate;
            MonthlySalary = monthlySalary;
            _allowance = allowance;
        }

        public void GiveRaise(decimal percent)
        {
            if (percent <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(percent), "A raise percentage must be greater than zero.");
            }

            MonthlySalary = MonthlySalary + MonthlySalary * percent / 100m;
        }

       
        public decimal CalculateMonthlyPay()
        {
            return MonthlySalary + _allowance;
        }
    }
}
