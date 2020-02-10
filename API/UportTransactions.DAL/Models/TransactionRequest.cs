using System;
using System.Collections.Generic;

namespace UportTransactions.DAL.Models
{
    public partial class TransactionRequest
    {
        public TransactionRequest()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int RequestId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
