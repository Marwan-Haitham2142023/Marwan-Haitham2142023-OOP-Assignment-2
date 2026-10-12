using System;
using System.Collections.Generic;
using System.Globalization;

namespace LibrarySystem
{
    public class Program
    {
        public static void Main()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

           
            DateTime hired = new DateTime(2020, 1, 15);
            Librarian librarian = new Librarian("S-001", "Mona Fathy", "0100-111-1111", hired, 3000m);
            Shelver shelver = new Shelver("S-002", "Karim Adel", "0100-222-2222", hired, 2500m, "Fiction");
            HeadLibrarian head = new HeadLibrarian("S-003", "Hassan Ali", "0100-333-3333", hired, 5000m);

            StudentMember ahmed = new StudentMember("M-001", "Ahmed Samir", "0111-000-0001");
            StudentMember sara = new StudentMember("M-002", "Sara Nabil", "0111-000-0002");
            StudentMember omar = new StudentMember("M-003", "Omar Khaled", "0111-000-0003");
            PremiumMember layla = new PremiumMember("M-100", "Layla Hassan", "0122-000-0100", 20m);
            Book cleanCode = new Book("B-001", "Clean Code", 1.00m);

            Console.WriteLine("=== 1. Compile-time protection (lines are commented out) ===");
            // Person p = new Person("P-1", "Nobody", "000");                  // must NOT compile: protected constructor
            // Member m = new Member("M-9", "Nobody", "000", 5, 0m);           // must NOT compile: protected constructor
            // Staff s = new Staff("S-9", "Nobody", "000", hired, 100m, 0m);   // must NOT compile: protected constructor
            // LibraryItem i = new LibraryItem("X-1", "Nothing", 1m, 1, 1m);   // must NOT compile: protected constructor
            // ahmed.FullName = "Someone Else";                                // must NOT compile: get-only property
            // ahmed.Loans.Add(null);                                          // must NOT compile: IReadOnlyList has no Add
            // cleanCode.IsOnLoan = true;                                      // must NOT compile: private set
            // librarian.MonthlySalary = 999999m;                              // must NOT compile: private set
            Console.WriteLine("(see the commented-out lines in Program.cs)");
            Console.WriteLine();

            // ---------- 2. Borrowing rules ----------
            Console.WriteLine("=== 2. Borrowing rules ===");

            Book withdrawnBook = new Book("B-099", "Out Of Print", 1.00m);
            head.Withdraw(withdrawnBook);
            try
            {
                ahmed.Borrow(withdrawnBook, "L-001", new DateTime(2026, 10, 1));
                Console.WriteLine("ERROR: withdrawn item was borrowed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Withdrawn item   -> REJECTED: " + ex.Message);
            }

            ahmed.Borrow(cleanCode, "L-002", new DateTime(2026, 10, 1));
            try
            {
                sara.Borrow(cleanCode, "L-003", new DateTime(2026, 10, 2));
                Console.WriteLine("ERROR: item already on loan was borrowed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Already on loan  -> REJECTED: " + ex.Message);
            }

            omar.Borrow(new Book("B-010", "Book One", 1.00m), "L-010", new DateTime(2026, 10, 1));
            omar.Borrow(new Book("B-011", "Book Two", 1.00m), "L-011", new DateTime(2026, 10, 1));
            omar.Borrow(new Magazine("G-010", "Magazine One", 1.00m), "L-012", new DateTime(2026, 10, 1));
            try
            {
                omar.Borrow(new DVD("D-010", "DVD One", 1.00m), "L-013", new DateTime(2026, 10, 1));
                Console.WriteLine("ERROR: 4th loan was allowed for a student!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Student 4th loan -> REJECTED: " + ex.Message);
            }
            Console.WriteLine("Omar's history still has " + omar.Loans.Count + " loans, " + omar.ActiveLoanCount + " active.");
            Console.WriteLine();

            // ---------- 3. Staff ----------
            Console.WriteLine("=== 3. Monthly pay for every kind of staff ===");
            List<Staff> staff = new List<Staff>();
            staff.Add(librarian);
            staff.Add(head);
            staff.Add(shelver);
            foreach (Staff member in staff)
            {
                Console.WriteLine(member.FullName.PadRight(14) + member.GetType().Name.PadRight(15) +
                                  "salary " + member.MonthlySalary.ToString("F2") +
                                  "  monthly pay " + member.CalculateMonthlyPay().ToString("F2"));
            }
            Console.WriteLine();

            // ---------- 4. Item ----------
            Console.WriteLine("=== 4. Loan period and daily late fee for every kind of item ===");
            List<LibraryItem> items = new List<LibraryItem>();
            items.Add(new Book("B-100", "The Pragmatic Programmer", 1.00m));
            items.Add(new DVD("D-100", "Inception", 1.00m));
            items.Add(new Magazine("G-100", "National Geographic", 1.00m));
            foreach (LibraryItem item in items)
            {
                Console.WriteLine(item.Title.PadRight(26) + item.GetType().Name.PadRight(10) +
                                  "loan period " + item.LoanPeriodDays + " days   " +
                                  "base fee " + item.BaseLateFee.ToString("F2") +
                                  "   daily late fee " + item.GetDailyLateFee().ToString("F2"));
            }
            Console.WriteLine();

            // ---------- 5. Premium member returns a DVD 5 days late ----------
            Console.WriteLine("=== 5. Premium member returns a DVD 5 days late ===");
            DVD inception = new DVD("D-001", "Inception", 1.00m);
            Loan dvdLoan = layla.Borrow(inception, "L-100", new DateTime(2026, 10, 1));
            Console.WriteLine("Borrowed on  : " + dvdLoan.BorrowDate.ToString("yyyy-MM-dd") + "   (item on loan: " + inception.IsOnLoan + ")");
            Console.WriteLine("Due date     : " + dvdLoan.DueDate.ToString("yyyy-MM-dd"));
            librarian.ProcessReturn(dvdLoan, new DateTime(2026, 10, 13));
            Console.WriteLine("Returned on  : " + dvdLoan.ReturnDate.GetValueOrDefault().ToString("yyyy-MM-dd") + "   (item on loan: " + inception.IsOnLoan + ")");
            Console.WriteLine("Late fee     : " + dvdLoan.LateFee.ToString("F2") +
                              "   (5 days x " + inception.GetDailyLateFee().ToString("F2") +
                              " minus " + layla.DiscountPercent + "% discount)");
            Console.WriteLine("Reading points: " + layla.ReadingPoints);
            Console.WriteLine();

            // ---------- 6. Illegal status changes ----------
            Console.WriteLine("=== 6. Illegal status changes ===");
            try
            {
                librarian.ProcessReturn(dvdLoan, new DateTime(2026, 10, 14));
                Console.WriteLine("ERROR: loan was returned twice!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Return twice      -> REJECTED: " + ex.Message);
            }
            try
            {
                librarian.MarkAsLost(dvdLoan);
                Console.WriteLine("ERROR: returned loan was marked as lost!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lost after return -> REJECTED: " + ex.Message);
            }
            Console.WriteLine("Loan status is still: " + dvdLoan.Status);
            Console.WriteLine();

            // ---------- 7. Remaining rules ----------
            Console.WriteLine("=== 7. Other validation rules ===");
            try
            {
                librarian.GiveRaise(0m);
                Console.WriteLine("ERROR: zero raise accepted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Raise of 0        -> REJECTED: " + ex.Message);
            }
            try
            {
                librarian.GiveRaise(-5m);
                Console.WriteLine("ERROR: negative raise accepted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Raise of -5       -> REJECTED: " + ex.Message);
            }
            librarian.GiveRaise(10m);
            Console.WriteLine("Raise of 10%      -> OK: salary is now " + librarian.MonthlySalary.ToString("F2"));

            try
            {
                head.SetLateFee(cleanCode, 0m);
                Console.WriteLine("ERROR: zero late fee accepted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("New late fee of 0 -> REJECTED: " + ex.Message);
            }
            head.SetLateFee(cleanCode, 1.50m);
            Console.WriteLine("New late fee 1.50 -> OK: base " + cleanCode.BaseLateFee.ToString("F2") +
                              ", daily " + cleanCode.GetDailyLateFee().ToString("F2"));

            try
            {
                StudentMember bad = new StudentMember("M-777", "", "0100");
                Console.WriteLine("ERROR: member with empty name was created for " + bad.PersonId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Empty full name   -> REJECTED: " + ex.Message);
            }
            try
            {
                Book bad = new Book("", "No Catalog Number", 1.00m);
                Console.WriteLine("ERROR: item with empty catalog number was created: " + bad.Title);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Empty catalog no. -> REJECTED: " + ex.Message);
            }

            Magazine natGeo = new Magazine("G-200", "Wired", 1.00m);
            Loan magLoan = sara.Borrow(natGeo, "L-200", new DateTime(2026, 10, 10));
            try
            {
                librarian.ProcessReturn(magLoan, new DateTime(2026, 10, 5));
                Console.WriteLine("ERROR: return before borrow date accepted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Return before borrow -> REJECTED: " + ex.Message);
            }
            Console.WriteLine("Loan " + magLoan.LoanId + " is still " + magLoan.Status + " (item on loan: " + natGeo.IsOnLoan + ")");

            shelver.Reassign("Children");
            Console.WriteLine("Shelver reassigned -> OK: section is now " + shelver.Section);

            librarian.MarkAsLost(magLoan);
            Console.WriteLine("Borrowed -> Lost  -> OK: loan " + magLoan.LoanId + " is now " + magLoan.Status);
            Console.WriteLine("Sara's history: " + sara.Loans.Count + " loan(s), " + sara.ActiveLoanCount + " active.");
        }
    }
}
