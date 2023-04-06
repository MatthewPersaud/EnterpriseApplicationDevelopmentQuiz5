using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionRecordApp.Models
{
    public class TransactionType
    {
        public string TransactionTypeId { get; set; }
        public string Name { get; set; }
        public double CommissionFee { get; set; }
    }
}
