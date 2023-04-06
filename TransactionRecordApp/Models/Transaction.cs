using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TransactionRecordApp.Models
{
    /// <summary>
    /// This class represents the columns in the Transaction table in the DB
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// this corresponds with the table PK and will work as an auto number
        /// since it's an int
        /// </summary>
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Please enter a Ticker Symbol")]
        public string TickerSymbol { get; set; }

        [Required(ErrorMessage = "Please enter the Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please enter the quantity of shares")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a whole number greater than zero")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please enter the share price")]
        [Range(0, int.MaxValue, ErrorMessage = "Share price can not be less than 0")]
        public double SharePrice { get; set; }

        // Setting the transaction type by adding both a property
        // with the same name as the foreign key and adding a full
        // TransactionType object

        [Required(ErrorMessage ="Please set transaction type")]
        public string TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Calculate the gross value of the transaction
        /// </summary>
        /// <returns></returns>
        public double GetGrossValue()
        {
            double grossValue = Quantity * SharePrice;
            return grossValue;
        }

        /// <summary>
        /// Calculate the net value of the transaction
        /// </summary>
        /// <returns></returns>
        public double GetNetValue()
        {
            double grossValue = GetGrossValue();
            double netValue = 0;

            if (TransactionTypeId == "S") { 
                netValue = grossValue - TransactionType.CommissionFee;
            }
            else
            {
                netValue = grossValue + TransactionType.CommissionFee;
            }
            
            return netValue;
        }
    }
}
