using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UportTransactions.BLL.Services;
using UportTransactions.DAL.Models;

namespace UportTransactions.BLL
{
    public class TransactionServiceEtherscan : ITransactionService
    {
        private readonly UportTransactionsContext _uportTransactionsContext;

        public TransactionServiceEtherscan(UportTransactionsContext uportTransactionsContext)
        {
            _uportTransactionsContext = uportTransactionsContext;
        }

        public async Task<TransactionResponse> GetTransactionsForBlock(Uri uriToBeCalled)
        {
            TransactionResponse transactionResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var httpResponseMessage = await client.GetAsync(uriToBeCalled);
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        transactionResponse = await httpResponseMessage.Content.ReadAsAsync<TransactionResponse>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return transactionResponse;
        }

        public async Task<string> DownloadTransactionsAsync(IDictionary<string, string> transactionParameters)
        {
            var urlBuilder = new UrlBuilder(transactionParameters["apibase"])
              .WithModule(transactionParameters["module"])
              .WithAction(transactionParameters["action"])
              .WithFromBlock(transactionParameters["fromBlock"])
              .WithToBlock(transactionParameters["toBlock"])
              .WithAddress(transactionParameters["address"])
              .WithApiKey(transactionParameters["apikey"]);

            HashSet<string> transactionsHashSet = new HashSet<string>();
            var success = true;
            while (success)
            {
                try
                {
                    var uriForBlock = urlBuilder.Build();
                    var transactionResponse = await GetTransactionsForBlock(uriForBlock);

                    if (transactionResponse == null || transactionResponse.status == 0)
                    {
                        success = false;
                        continue;
                    }

                    transactionResponse.result = transactionResponse.result.Where(p => !transactionsHashSet.Contains(p.transactionHash)).ToList();
                    transactionResponse.result.ForEach(p => transactionsHashSet.Add(p.transactionHash));

                    if (transactionResponse.result.Count == 0)
                    {
                        success = false;
                        continue;
                    }

                    await SaveTransactions(transactionResponse, uriForBlock);

                    var lastTransactionBlock = transactionResponse.result.Select(p => Convert.ToInt32(p.blockNumber, 16)).OrderBy(p => p).LastOrDefault();
                    urlBuilder.WithFromBlock((lastTransactionBlock).ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                }
            }

            return "All transactions saved to database!";
        }

        private async Task SaveTransactions(TransactionResponse transactionResponse, Uri transactionUrl)
        {
            try
            {
                var transactionRequest = new TransactionRequest
                {
                    Message = transactionResponse.message,
                    Status = transactionResponse.status.ToString(),
                    Url = transactionUrl.ToString()
                };

                _uportTransactionsContext.TransactionRequest.Add(transactionRequest);
                await _uportTransactionsContext.SaveChangesAsync();

                NumberStyles styles = NumberStyles.HexNumber | NumberStyles.AllowHexSpecifier;
                var provider = CultureInfo.InvariantCulture;

                var transactions = transactionResponse.result.Select(transaction =>
                {
                    var tran = new Transaction();
                    tran.RequestId = transactionRequest.RequestId;
                    tran.Address = transaction.address;
                    tran.Data = transaction.data;

                    if(Int64.TryParse(transaction.blockNumber.ClearHexPrefix(), styles, provider, out var blockNumber))
                    {
                        tran.BlockNumber = blockNumber.ToString();
                    }
                    else
                    {
                        tran.BlockNumber = transaction.blockNumber;
                    }

                    if (Int64.TryParse(transaction.gasPrice.ClearHexPrefix(), styles, provider, out var gasPrice))
                    {
                        tran.GasPrice = gasPrice.ToString();
                    }
                    else
                    {
                        tran.GasPrice = transaction.gasPrice;
                    }

                    if (Int64.TryParse(transaction.gasUsed.ClearHexPrefix(), styles, provider, out var gasUsed))
                    {
                        tran.GasUsed = gasUsed.ToString();
                    }
                    else
                    {
                        tran.GasUsed = transaction.gasUsed;
                    }

                    if (Int64.TryParse(transaction.logIndex.ClearHexPrefix(), styles, provider, out var logIndex))
                    {
                        tran.LogIndex = logIndex.ToString();
                    }
                    else
                    {
                        tran.LogIndex = transaction.logIndex;
                    }

                    tran.TimeStamp = transaction.timeStamp;
                    tran.Topics = string.Join(",", transaction.topics);
                    //TODO:  decode data web3js
                    tran.TransactionHash = transaction.transactionHash;
                    tran.TransactionIndex = transaction.transactionIndex;

                return tran;
                }).ToList();

                await _uportTransactionsContext.Transaction.AddRangeAsync(transactions);
                await _uportTransactionsContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public async Task<List<TransactionItemResponse>> GetTransactionsAsync(IDictionary<string, string> filterParameters)
        {
            if (!Int32.TryParse(filterParameters["fromBlock"], out var fromBlock) || !Int32.TryParse(filterParameters["toBlock"], out var toBlock))
            {
                return new List<TransactionItemResponse>();
            }

            var address = filterParameters["address"];
            var topic = filterParameters["topic"];

            var query = from transaction in _uportTransactionsContext.Transaction
                        let blockNumber = Convert.ToInt32(transaction.BlockNumber, 16)
                        where fromBlock <= blockNumber && blockNumber >= toBlock && transaction.Address == address
                        select new TransactionItemResponse
                        {
                            address = transaction.Address,
                            blockNumber = transaction.BlockNumber,
                            timeStamp = transaction.TimeStamp
                        };

            return await query.ToListAsync();
        }
    }
}
