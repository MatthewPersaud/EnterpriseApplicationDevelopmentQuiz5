using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using TransactionRecordApp.Models;

namespace TransactionRecordApp.Controllers
{
    /// <summary>
    /// Controls the Add, Edit and Delete functions
    /// </summary>
    public class TransactionController : Controller
    {
        public TransactionController (TransactionContext transactionConatext)
        {
            _tranactionContext = transactionConatext;
        }

        /// <summary>
        /// Action to allow the user to add a new transaction
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            // Set the action to Add
            ViewBag.Action = "Add";

            // Get the transaction types and add them to the Viewbag
            ViewBag.TransactionTypes = _tranactionContext.TransactionType.OrderBy(t => t.Name).ToList();

            // Return the blank Edit form by naming it
            return View("Edit", new Transaction());
        }
        /// <summary>
        /// If there are no errors in the inputs, the transaction is added
        /// If there are errors, the page is returned with the errors
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Add(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _tranactionContext.Transactions.Add(transaction);
                _tranactionContext.SaveChanges();
                return RedirectToAction("Index", "Transactions");
            }
            else
            {
                ViewBag.Action = "Add";
                return View("Edit", transaction);
            }
        }

        /// <summary>
        /// Allows user to edit a movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            // Set the action to Edit
            ViewBag.Action = "Edit";

            var transaction = _tranactionContext.Transactions.Find(id);

            // Get the transaction types and add them to the Viewbag
            ViewBag.TransactionTypes = _tranactionContext.TransactionType.OrderBy(t => t.Name).ToList();

            // Return the Edit form
            return View(transaction);
        }
        /// <summary>
        /// If there are no errors in the inputs, the transaction is updated
        /// If there are errors, the page is returned with the errors
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                // It's valid so we update the transaction
                _tranactionContext.Transactions.Update(transaction);
                _tranactionContext.SaveChanges();
                return RedirectToAction("Index", "Transactions");
            }
            else
            {
                // It's not valid so we return the Transaction
                // to the Edit view by setting the action to Edit again
                ViewBag.Action = "Edit";
                ViewBag.TransactionTypes = _tranactionContext.TransactionType.OrderBy(t => t.Name).ToList();
                return View(transaction);
            }
        }

        /// <summary>
        /// Allows user to delete a movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            // Find the trnsaction in the DB and save it to a variable
            var transaction = _tranactionContext.Transactions.Find(id);

            // Return the Delete form
            return View(transaction);
        }
        /// <summary>
        /// Delete the movie
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Transaction transaction)
        {
            // Delete the movie
            _tranactionContext.Transactions.Remove(transaction);
            _tranactionContext.SaveChanges();
            return RedirectToAction("Index", "Transactions");
        }

        private TransactionContext _tranactionContext;
    }
}
