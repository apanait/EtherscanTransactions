using System.Collections.Generic;
using System.Threading.Tasks;

namespace UportTransactions.BLL
{
   public interface ITransactionService
    {
        Task<string> DownloadTransactionsAsync(IDictionary<string, string> transactionParameters);

        Task<List<TransactionItemResponse>> GetTransactionsAsync(IDictionary<string, string> filterParameters);
    }
}
