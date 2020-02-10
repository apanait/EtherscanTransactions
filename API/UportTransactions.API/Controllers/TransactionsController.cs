using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UportTransactions.BLL;

namespace UportTransactions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService, ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/download")]
        public async Task<IActionResult> Download(string fromBlock, string toBlock, string address, string version)
        {
            if (string.IsNullOrEmpty(fromBlock))
            {
                return BadRequest(new { ErrorMessage = $"Parameter @{nameof(fromBlock)} is required!" });
            }

            if (string.IsNullOrEmpty(toBlock))
            {
                return BadRequest(new { ErrorMessage = $"Parameter @{nameof(toBlock)} is required!" });
            }

            if (string.IsNullOrEmpty(address))
            {
                return BadRequest(new { ErrorMessage = $"Parameter @{nameof(address)} is required!" });
            }

            if (string.IsNullOrEmpty(version))
            {
                return BadRequest(new { ErrorMessage = $"Parameter @{nameof(version)} is required!" });
            }

            string result = "NOTOK";

            //TODO populate parameters from query string data
            var parameters = new Dictionary<string, string>();
            parameters.Add("module", "logs");
            parameters.Add("action", "getLogs");
            //parameters.Add("fromBlock", "7049729");
            //parameters.Add("toBlock", "latest");
            //parameters.Add("address", "0xdca7ef03e98e0dc2b855be647c39abe984fcf21b");
            //parameters.Add("apikey", "A62G2VWWJRDJZUGMXYVGZAWDKFZ7YZYVQH");
            // V2
            //https://api-rinkeby.etherscan.io
            //fromBlock=2463642
            //&toBlock=latest
            //&address=0xdca7ef03e98e0dc2b855be647c39abe984fcf21b

            parameters.Add("fromBlock", fromBlock);
            parameters.Add("toBlock", toBlock);
            parameters.Add("address", address);
            parameters.Add("apikey", Environment.GetEnvironmentVariable("ETHERSCAN_API_KEY"));
            var apiBase = Environment.GetEnvironmentVariable($"ETHERSCAN_API_{version.ToUpper()}_URL");
            parameters.Add("apibase", apiBase);

            try
            {
                result = await _transactionService.DownloadTransactionsAsync(parameters);
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
                _logger.LogError(ex.Message, ex);
            }

            return Ok(result);
        }

        // GET api/transactions
        [HttpGet]
        [Route("/list")]
        public async Task<IActionResult> GetTransactions(string fromBlock, string toBlock, string address, string topic)
        {
            if (string.IsNullOrEmpty(fromBlock))
            {
                return BadRequest(new { ErrorMessage = $"Parameter @{nameof(fromBlock)} is required!" });
            }

            if (string.IsNullOrEmpty(toBlock))
            {
                return BadRequest(new { ErrorMessage = $"Parameter @{nameof(toBlock)} is required!" });
            }

            if (string.IsNullOrEmpty(address))
            {
                return BadRequest(new { ErrorMessage = $"Parameter @{nameof(address)} is required!" });
            }

            if (string.IsNullOrEmpty(topic))
            {
                return BadRequest(new { ErrorMessage = $"Parameter @{nameof(topic)} is required!" });
            }

            var parameters = new Dictionary<string, string>();
            parameters.Add("fromBlock", fromBlock);
            parameters.Add("toBlock", toBlock);
            parameters.Add("address", address);
            parameters.Add("topic", topic);

            try
            {
                var result = await _transactionService.GetTransactionsAsync(parameters);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return NotFound(new { ErrorMessage = $"Error: { ex.Message }" });
            }
        }

        // POST api/transactions
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}

