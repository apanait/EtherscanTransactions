using System;

namespace UportTransactions.DAL.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int RequestId { get; set; }
        public string Address { get; set; }
        public string Topics { get; set; }
        public string Data { get; set; }
        public string BlockNumber { get; set; }
        public string TimeStamp { get; set; }
        public string GasPrice { get; set; }
        public string GasUsed { get; set; }
        public string LogIndex { get; set; }
        public string TransactionHash { get; set; }
        public string TransactionIndex { get; set; }

        public virtual TransactionRequest Request { get; set; }
    }
}
