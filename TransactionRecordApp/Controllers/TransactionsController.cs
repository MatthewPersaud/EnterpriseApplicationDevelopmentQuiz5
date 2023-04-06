using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using TransactionRecordApp.Models;

namespace TransactionRecordApp.Controllers
{
    /// <summary>
    /// Contoller of the Transactions Index (main) page
    /// </summary>
    public class TransactionsController : Controller
    {
        public TransactionsController(TransactionContext transactionContext)
        {
            _transactionContext = transactionContext;
        }

        /// <summary>
        /// Calls the Transactions Index (main) page and passes all the transactions to it
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // make the database into a list ordered by the Company Name
            var transactions = _transactionContext.Transactions.Include(o => o.TransactionType).OrderBy(o => o.CompanyName).ToList();

            // Then pass the list of traansactions to the view
            return View(transactions);
        }

        // Set the private variable
        private TransactionContext _transactionContext;
    }
}
