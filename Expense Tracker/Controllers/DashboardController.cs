using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ExpenseTrackerDbContext _context;
        public DashboardController(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //lats 7 days transactions
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> SelectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(d => d.Date >= StartDate && d.Date <= EndDate)
                .ToListAsync();

            //total income
            int TotalIncome = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");


            //total Expense
            int TotalExpense = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            //Balance
            int Balance = TotalIncome - TotalExpense;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = string.Format(culture, "{0:C0}", Balance);

            //Doughnut chart - Expense By Category
            ViewBag.DoughnutChartData = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.Id)
                .Select(k => new
                {
                    CategoryTitle = k.First().Category.Title,
                    amount = k.Sum(j => j.Amount),
                    formatedAmount = k.Sum(j => j.Amount).ToString("C0")
                })
                .OrderByDescending(l => l.amount)
                .ToList();


            //spline chart - Income vs Expense
            //Income
            List<SplineChartData> IncomeSummery = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .GroupBy(d => d.Date)
                .Select(s => new SplineChartData()
                {
                    day = s.First().Date.ToString("dd-MMM"),
                    income = s.Sum(a => a.Amount),
                }).ToList();


            //Expense
            List<SplineChartData> ExpenseSummary = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(d => d.Date)
                .Select(s => new SplineChartData()
                {
                    day = s.First().Date.ToString("dd-MMM"),
                    expense = s.Sum(a => a.Amount),
                }).ToList();

            //combine Income and Expense
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM")).ToArray();

            ViewBag.SplineChartData = from day in Last7Days
                                      join income in IncomeSummery on day equals income.day
                                      into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in ExpenseSummary on day equals expense.day 
                                      into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };
            //Recent Transactions
            ViewBag.RecentTransactions = await _context.Transactions
                .Include(c => c.Category)
                .OrderByDescending(d => d.Date)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }

    public class SplineChartData
    {
        public string day;
        public int income;
        public int expense;
    }
}
