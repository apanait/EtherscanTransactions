using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;

namespace UportTransactions.BLL
{
    public class FakeTransactionService : ITransactionService
    {
        public Task<string> DownloadTransactionsAsync(IDictionary<string, string> transactionParameters)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetTransactionsAsync(IDictionary<string, string> transactionParameters)
        {
            //var rootPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
            //var filePath = Path.Combine(rootPath, @"Fake\api.json");
            //var response = await File.ReadAllTextAsync(filePath);
            //var transactionResponse = JsonConvert.DeserializeObject<TransactionResponse>(response);

            return "All good but fake!";
        }

        Task<List<TransactionItemResponse>> ITransactionService.GetTransactionsAsync(IDictionary<string, string> filterParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
