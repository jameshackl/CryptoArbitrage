using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageOpportunityFinder
{
    public class DataController
    {
        private DataController() {}

        public static string WebReq(string URL)
        {
            //TODO: web error handling
            string responseStr;

            HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;

            // Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());

                responseStr = reader.ReadToEnd();

                response.Close();

            }

            return responseStr;
        }

        //these methods are here for no other reason than to have them all in one place and to avoid using reflection
        public static bool GetInitData()
        {
            GetPoloniexInitData();
            GetCryptopiaInitData();
            //GetKrakenInitData();
            //GetBittrexInitData();


            ApplyBlackListsToTransactions();
            TranslateSymbolsCodes();
            return true;
        }

        private static void TranslateSymbolsCodes()
        {
            GlobalData db = GlobalData.Instance;
            db.TranslateISOCurrencies();
        }

        private static void ApplyBlackListsToTransactions()
        {
            GlobalData db = GlobalData.Instance;
            List<Currency> reference = db.GetCoinBlackList();
            
            foreach(Currency c in reference)
            {
                db.RemoveTransactionByCoin(c);
            }
            
            //TODO: remove transanctions based on transaction
        }

        public static void GetTransactionData()
        {
            GetPoloniexTransactionData();
            GetCryptopiaPriceData();
            //TODO:seperate out the transaction getter so the prices can be updated
        }
        #region Parsers
        //Dear Reader, This is not a great way to do this. I did it this way to collect data as fast as possible so I could do analysis on it.
        public static void GetPoloniexTransactionData()
        {
            dynamic orderbook = JsonConvert.DeserializeObject(WebReq("https://poloniex.com/public?command=returnOrderBook&currencyPair=ALL&depth=10"));

            var exchange = GlobalData.Exchange.Poloniex;
            GlobalData db = GlobalData.Instance;
            string id;
            List<decimal[]> orderbookBuilderA;
            List<decimal[]> orderbookBuilderB;


            foreach (var c in orderbook)
            {
                id = c.Name;
                if (db.ExistsTransaction(exchange, id))
                {
                    orderbookBuilderA = new List<decimal[]>();
                    orderbookBuilderB = new List<decimal[]>();


                    foreach (var o in c.First.asks)
                    {
                        orderbookBuilderA.Add(new decimal[] { decimal.Parse(o.First.Value), (decimal)o.Last.Value } );
                    }


                    foreach (var o in c.First.bids)
                    {
                        orderbookBuilderB.Add(new decimal[] { decimal.Parse(o.First.Value), (decimal)o.Last.Value });
                    }

                    db.UpdateTransaction(exchange, 
                        id, 
                        orderbookBuilderB, orderbookBuilderA);
                }
            }
        }

        public static bool GetPoloniexInitData()
        {
            dynamic ticker = JsonConvert.DeserializeObject(WebReq("https://poloniex.com/public?command=returnTicker"));
            dynamic currencies = JsonConvert.DeserializeObject(WebReq("https://poloniex.com/public?command=returnCurrencies"));
            

            var exchange = GlobalData.Exchange.Poloniex;
            GlobalData db = GlobalData.Instance;

            decimal transactionFee = 0.0025M; //hard coded taker fee of 0.25%. this lowers once volume goes over 600btc($1 800 000 as of his writing) per month

            //coin black list
            //disabled means transfers/withdrawls, frozen means market frozen.
            //just avoid both for now.

            foreach (var coin in currencies)
            {
                if (coin.First.disabled.Value == 1 || coin.First.frozen.Value == 1)
                {

                    db.AddCoinToBlckList(new Currency(coin.Name,exchange));
                }
            }

            //transaction black list
            //N/A. well.. this could be done from the frozen coins but it already get removed for now


            
            //master transaction list
            foreach(var trans in ticker)
            {
                Transaction transactionBuilder = new Transaction();
                Currency currencyBuilder = new Currency();
                string[] pairParser;

                pairParser = trans.Name.Split('_');

                //TODO: add more information about currencies to init phase
                currencyBuilder.symbol = pairParser[1];
                currencyBuilder.exchange = exchange;
                db.AddCurrencyToMasterList(currencyBuilder);
                transactionBuilder.baseCurrency = currencyBuilder;

                currencyBuilder = new Currency();
                currencyBuilder.exchange = exchange;
                currencyBuilder.symbol = pairParser[0];
                db.AddCurrencyToMasterList(currencyBuilder);
                transactionBuilder.quoteCurrency = currencyBuilder;


                //transactionBuilder.bid = decimal.Parse(trans.First.highestBid.Value);
                //transactionBuilder.ask = decimal.Parse(trans.First.lowestAsk.Value);
                transactionBuilder.identifier = trans.Name.ToString();
                transactionBuilder.transactionType = Transaction.type.Trade;

                db.AddTransactionToMasterlist(transactionBuilder);
            }

            return true;
        }
        public static void GetCryptopiaPriceData()
        {
            var exchange = GlobalData.Exchange.Cryptopia;
            GlobalData db = GlobalData.Instance;

            List<Transaction> tList = db.GetTransSubListByExchange(exchange);

            string URLBuilder = "https://www.cryptopia.co.nz/api/GetMarketOrderGroups/";

            foreach (Transaction t in tList)
            {
                URLBuilder = URLBuilder + t.identifier + '-';
            }
            URLBuilder = URLBuilder.Remove(URLBuilder.LastIndexOf('-'));
            URLBuilder = URLBuilder + "/10";

            dynamic orderbook = JsonConvert.DeserializeObject(WebReq(URLBuilder));

            string id;
            List<decimal[]> orderbookBuilderA;
            List<decimal[]> orderbookBuilderB;
            decimal[] order;

            foreach (var c in orderbook.Data)
            {
                id = c.TradePairId.Value.ToString();
                if (db.ExistsTransaction(exchange, id))
                {
                    
                    orderbookBuilderA = new List<decimal[]>();
                    orderbookBuilderB = new List<decimal[]>();
                    foreach (var o in c.Sell)
                    { 
                        order = new decimal[] { 0, 0 };
                        order[0] = (decimal)o.Price.Value;
                        order[1] = (decimal)o.Volume.Value;

                        orderbookBuilderA.Add(order);
                    }

                    order = new decimal[] { 0, 0 };
                    foreach (var o in c.Buy)
                    {
                        order = new decimal[] { 0, 0 };
                        order[0] = (decimal)o.Price.Value;
                        order[1] = (decimal)o.Volume.Value;

                        orderbookBuilderB.Add(order);
                    }

                    db.UpdateTransaction(exchange,
                        id,
                        orderbookBuilderB,
                        orderbookBuilderA);
                }
            }
        }
        public static bool GetCryptopiaInitData()
        {
            dynamic ticker = JsonConvert.DeserializeObject(WebReq("https://www.cryptopia.co.nz/api/GetMarkets"));
            dynamic currencies = JsonConvert.DeserializeObject(WebReq("https://www.cryptopia.co.nz/api/GetCurrencies"));
            var exchange = GlobalData.Exchange.Cryptopia;
            GlobalData db = GlobalData.Instance;

            //coin black list

            foreach (var coin in currencies.Data)
            {
                if (coin.Status.Value != "OK")
                {

                    db.AddCoinToBlckList(new Currency(coin.Symbol.Value, exchange));
                }
            }

            //transaction black list




            //master transaction list
            foreach (var trans in ticker.Data)
            {
                Transaction transactionBuilder = new Transaction();
                Currency currencyBuilder = new Currency();
                string[] pairParser;

                pairParser = trans.Label.Value.Split('/');

                currencyBuilder.symbol = pairParser[0];
                currencyBuilder.exchange = exchange;
                transactionBuilder.baseCurrency = currencyBuilder;

                currencyBuilder = new Currency();
                currencyBuilder.symbol = pairParser[1];
                currencyBuilder.exchange = exchange;
                transactionBuilder.quoteCurrency = currencyBuilder;


                //transactionBuilder.bid = decimal.Parse(trans.BidPrice.Value.ToString(),System.Globalization.NumberStyles.Float);
                //transactionBuilder.ask = decimal.Parse(trans.AskPrice.Value.ToString(),System.Globalization.NumberStyles.Float);
                transactionBuilder.identifier = trans.TradePairId.Value.ToString();
                transactionBuilder.transactionType = Transaction.type.Trade;

                db.AddTransactionToMasterlist(transactionBuilder);
            }

            return true;
        }

        public static bool GetKrakenInitData()
        {
            string krakenTickerUrl = "https://api.kraken.com/0/public/Ticker?pair=";
            dynamic currencies = JsonConvert.DeserializeObject(WebReq("https://api.kraken.com/0/public/Assets"));
            dynamic transactions = JsonConvert.DeserializeObject(WebReq("https://api.kraken.com/0/public/AssetPairs"));
            var exchange = GlobalData.Exchange.Kraken;
            GlobalData db = GlobalData.Instance;

            //have to use currency list to get the proper names of the coins
            //also the url for the transaction list is absurd

            var krakenCoinDict = new Dictionary<string, string>();
            foreach(var coin in currencies.result)
            {
                krakenCoinDict.Add(coin.Name, coin.First.altname.Value);
            }

            foreach(var pair in transactions.result)
            {
                if (!pair.Name.ToString().Contains(".d"))
                {
                    krakenTickerUrl = krakenTickerUrl + pair.Name + ",";
                }  
            }
            krakenTickerUrl = krakenTickerUrl.Remove(krakenTickerUrl.LastIndexOf(','));


            dynamic ticker = JsonConvert.DeserializeObject(WebReq(krakenTickerUrl));
            //coin black list

            //doesn't need one, only reurn good currencies, currencies are highly vetted
            //no curent way to do this

            //transaction black list

            //none, only returns good pairs
            //no current way to do this directly. it can be implied that if a pair is not returned then it is not active


            //master transaction list
            //master coin list
            foreach (var trans in transactions.result)
            {

                if(!trans.Name.ToString().Contains(".d"))
                {
                    Transaction transactionBuilder = new Transaction();
                    Currency currencyBuilder = new Currency();

                    transactionBuilder.identifier = trans.Name;
                    transactionBuilder.transactionType = Transaction.type.Trade;



                    currencyBuilder.symbol = krakenCoinDict[trans.First.@base.ToString()];
                    currencyBuilder.exchange = exchange;
                    db.AddCurrencyToMasterList(currencyBuilder);
                    transactionBuilder.baseCurrency = currencyBuilder;

                    currencyBuilder = new Currency();
                    currencyBuilder.symbol = krakenCoinDict[trans.First.quote.ToString()];
                    currencyBuilder.exchange = exchange;
                    db.AddCurrencyToMasterList(currencyBuilder);
                    transactionBuilder.quoteCurrency = currencyBuilder;


                    //transactionBuilder.bid = decimal.Parse(ticker.result[trans.Name].b[0].Value.ToString(), System.Globalization.NumberStyles.Float);
                    //transactionBuilder.ask = decimal.Parse(ticker.result[trans.Name].a[0].Value.ToString(), System.Globalization.NumberStyles.Float);
                

                    db.AddTransactionToMasterlist(transactionBuilder);
                }

                
            }

            return true;
        }

        public static bool GetBittrexInitData()
        {
            dynamic ticker = JsonConvert.DeserializeObject(WebReq("https://bittrex.com/api/v1.1/public/getmarketsummaries"));
            dynamic transactions = JsonConvert.DeserializeObject(WebReq("https://bittrex.com/api/v1.1/public/getmarkets"));
            dynamic currencies = JsonConvert.DeserializeObject(WebReq("https://bittrex.com/api/v1.1/public/getcurrencies   "));
            var exchange = GlobalData.Exchange.Bittrex;
            GlobalData db = GlobalData.Instance;

            //coin black list

            foreach (var coin in currencies.result)
            {
                if (coin.IsActive.Value != true)
                {

                    db.AddCoinToBlckList(new Currency(coin.Currency.Value, exchange));
                }
            }

            //transaction black list
            //TODO: this needs to be done
            //foreach(var t in transactions.result)
            //{
            //    if(!t.IsActive)
            //    {
            //        db.AddTransactionToBlckList(new Transaction(
            //    }
            //}



            //master transaction list
            foreach (var trans in ticker.result)
            {
                Transaction transactionBuilder = new Transaction();
                Currency currencyBuilder = new Currency();
                string[] pairParser;

                pairParser = trans.MarketName.Value.Split('-');

                currencyBuilder.symbol = pairParser[1];
                currencyBuilder.exchange = exchange;
                transactionBuilder.baseCurrency = currencyBuilder;

                currencyBuilder = new Currency();
                currencyBuilder.symbol = pairParser[0];
                currencyBuilder.exchange = exchange;
                transactionBuilder.quoteCurrency = currencyBuilder;


                //transactionBuilder.bid = decimal.Parse(trans.Bid.Value.ToString(), System.Globalization.NumberStyles.Float);
                //transactionBuilder.ask = decimal.Parse(trans.Ask.Value.ToString(), System.Globalization.NumberStyles.Float);
                transactionBuilder.identifier = trans.MarketName.Value.ToString();
                transactionBuilder.transactionType = Transaction.type.Trade;

                db.AddTransactionToMasterlist(transactionBuilder);
            }

            return true;
        }
    }
}
#endregion
