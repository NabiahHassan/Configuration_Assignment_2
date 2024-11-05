using Configuration_Assignment_2.Models;
using Configuration_Assignment_2.ServiceContracts;
using Configuration_Assignment_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Configuration_Assignment_2.Controlllers
{
    public class HomeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;
        public HomeController(IFinnhubService finnhubService , IOptions<TradingOptions> tradingOptions, IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
            _configuration = configuration;
        }
        [Route("/")]
        [Route("[action]")]
        [Route("/[controller]")]
        public async Task<IActionResult>  Index()
        {
            //if(_tradingOptions.DefaultStockSymbol == null)
            //{
            //    _tradingOptions.DefaultStockSymbol = "MSFT";
            //}
            //Dictionary<string, Object>? companyprofileDictionary = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
            //Dictionary<string, Object>? stockQouteDictionary =  _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);
            //StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };
            //if (companyprofileDictionary != null && stockQouteDictionary != null)
            //{
            //    stockTrade = new StockTrade() 
            //    { 
            //        StockSymbol = Convert.ToString(companyprofileDictionary["ticker"]),
            //        StockName = Convert.ToString(companyprofileDictionary["name"]), 
            //        Price = Convert.ToDouble(stockQouteDictionary["c"].ToString()) 
            //    };
            //}
            //ViewBag.FinnhubToken = _configuration["FinnhubToken"];
            //return View(stockTrade);
            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
                _tradingOptions.DefaultStockSymbol = "MSFT";


            //get company profile from API server
            Dictionary<string, object>? companyProfileDictionary = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            //get stock price quotes from API server
            Dictionary<string, object>? stockQuoteDictionary = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);


            //create model object
            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };

            //load data from finnHubService into model object
            if (companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade() { StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]), StockName = Convert.ToString(companyProfileDictionary["name"]), Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()) };
            }

            //Send Finnhub token to view
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }
    }
}
