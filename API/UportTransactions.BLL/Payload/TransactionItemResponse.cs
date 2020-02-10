﻿using System.Collections.Generic;

namespace UportTransactions.BLL
{
    public class TransactionItemResponse
    {
        public string address { get; set; }
        public List<string> topics { get; set; }
        public string data { get; set; }
        public string blockNumber { get; set; }
        public string timeStamp { get; set; }
        public string gasPrice { get; set; }
        public string gasUsed { get; set; }
        public string logIndex { get; set; }
        public string transactionHash { get; set; }
        public string transactionIndex { get; set; }
    }
}
