using System.Collections.Generic;

namespace UportTransactions.BLL
{
    public class TransactionResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<TransactionItemResponse> result { get; set; }
    }
}
