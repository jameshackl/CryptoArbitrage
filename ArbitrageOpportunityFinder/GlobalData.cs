﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageOpportunityFinder
{
    public sealed class GlobalData
    {
        //singleton 
        private static readonly object padlock = new object();

        //TODO: Clean this up
        private static GlobalData instance = null;
        private List<Currency> _currencyBlackList;
        private List<Currency> _masterCurrencyList;

        private List<Transaction> _masterTransactionList;

        private List<Transaction> _transactionBlackList;

        private List<TriArbTransaction> _triArbTransactionList;

        private string[] fiatList =
            {
                "USD",
                "CAD",
                "GBP",
                "JPY",
                "KRW",
                "CNY",
                "EUR"
            };

        GlobalData()
        {
            _currencyBlackList = new List<Currency>();
            _masterTransactionList = new List<Transaction>();
            _masterCurrencyList = new List<Currency>();
            _transactionBlackList = new List<Transaction>();

            _triArbTransactionList = new List<TriArbTransaction>();
            _triArbTransactionChainList = new List<TriArbOpportunity>();
        }

        public enum Exchange { Poloniex, Cryptopia, Kraken, Bittrex }

        public static GlobalData Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance == null ? instance = new GlobalData() : instance;
                }
            }
        }

        private List<TriArbOpportunity> _triArbTransactionChainList { get; set; }

        public void AddCoinToBlckList(Currency coin)
        {
            _currencyBlackList.Add(coin);
        }

        public void AddCurrencyToMasterList(Currency c)
        {
            _masterCurrencyList.Add(c);
        }
        public void AddToTriArbTransactionChainList(TriArbOpportunity m)
        {
            _triArbTransactionChainList.Add(m);
        }

        public void AddTransactionToBlckList(Transaction trans)
        {
            _transactionBlackList.Add(trans);
        }

        public void AddTransactionToMasterlist(Transaction t)
        {
            _masterTransactionList.Add(t);
        }

        public void ArbChainGenerator(string startingCurrency, Exchange startingExchange, Exchange endExchange)
        {
            List<TriArbTransaction> startingList = GetTriArbSubList(startingExchange, startingCurrency);

            foreach (TriArbTransaction t in startingList)
            {
                List<TriArbTransaction> u = GetTriArbSubList(t.quoteCurrency.exchange, t.quoteCurrency.symbol);
                FilterSubList(ref u, startingCurrency);

                foreach (TriArbTransaction v in u)
                {
                    List<TriArbTransaction> w = GetFinalSubListBySymbol(v.quoteCurrency.exchange, v.quoteCurrency.symbol, startingCurrency);

                    foreach (TriArbTransaction x in w)
                    {
                        List<TriArbTransaction> ltat = new List<TriArbTransaction>();
                        ltat.Add(_triArbTransactionList.FindLast(y => y.quoteCurrency.exchange == t.quoteCurrency.exchange &&
                                                                y.quoteCurrency.symbol == t.quoteCurrency.symbol &&
                                                                y.baseCurrency.exchange == t.baseCurrency.exchange &&
                                                                y.baseCurrency.symbol == t.baseCurrency.symbol &&
                                                                y.inversed == t.inversed));
                        ltat.Add(_triArbTransactionList.FindLast(y => y.quoteCurrency.exchange == v.quoteCurrency.exchange &&
                                                                y.quoteCurrency.symbol == v.quoteCurrency.symbol &&
                                                                y.baseCurrency.exchange == v.baseCurrency.exchange &&
                                                                y.baseCurrency.symbol == v.baseCurrency.symbol &&
                                                                y.inversed == v.inversed));
                        ltat.Add(_triArbTransactionList.FindLast(y => y.quoteCurrency.exchange == x.quoteCurrency.exchange &&
                                                                y.quoteCurrency.symbol == x.quoteCurrency.symbol &&
                                                                y.baseCurrency.exchange == x.baseCurrency.exchange &&
                                                                y.baseCurrency.symbol == x.baseCurrency.symbol &&
                                                                y.inversed == x.inversed));

                        TriArbOpportunity tao = new TriArbOpportunity(ltat);
                        _triArbTransactionChainList.Add(tao);
                    }
                }
            }
        }

        public bool ExistsTransaction(Exchange e, string id)
        {
            return _masterTransactionList.Exists(x => x.baseCurrency.exchange == e && x.quoteCurrency.exchange == e && x.identifier == id);
        }

        public void FilterSubList(ref List<TriArbTransaction> list, string currency)
        {
            //failure modes: buy then immediately sell, trade back to the starting currency early
            // in pent-arb you can creat a closed off loop of chain that will most likely lose
            int wasted;
            wasted = list.RemoveAll(x => x.quoteCurrency.symbol == currency);
        }

        public void GenerateTriArbTransactionChains(string sc, Exchange se, Exchange ee)
        {
            //clear list
            if (!(_triArbTransactionChainList.Count() == 0))
                _triArbTransactionChainList.Clear();


            //populates the List<TriArbOpportunity>
            ArbChainGenerator(sc, se,ee);

        }

        public void GenerateTriArbTransactionList()
        {
            //clear old list
            if (!(_triArbTransactionList.Count() == 0))
            {
                _triArbTransactionList.Clear();
            }

            //first pass is just every thing in the same order
            foreach (Transaction trans in _masterTransactionList)
            {
                //forward
                AddTriArbTransList(
                    new TriArbTransaction(trans, false));
                //backwards
                AddTriArbTransList(
                    new TriArbTransaction(trans, true));
            }


            //what are all the possible currencies
            List<Currency> currencyList = _masterTransactionList.Select<Transaction, Currency>(x => x.baseCurrency).ToList();
            currencyList.AddRange(_masterTransactionList.Select<Transaction, Currency>(x => x.quoteCurrency));

            //remove duplicates. there may be other ways to do this BUT most of them don't work. this solution is not on the web as far as i know. *pats back*
            List<Currency> currencyCatcher = new List<Currency>();

            foreach (Currency d in currencyList)
            {
                if (!(currencyCatcher.Where(x => x.symbol == d.symbol && x.exchange == d.exchange).Count() > 0))
                {
                    currencyCatcher.Add(d);
                }
            }

            currencyList.Clear();
            currencyList.AddRange(currencyCatcher);

            //remove all fiat currencies before finding transfers between exchanges
            //they can't be transfered automagically
            foreach (string c in fiatList)
            {
                currencyList.RemoveAll(x => x.symbol == c);
            }

            //now find all the possible transfers between exchanges


            List<Currency> intermediateCurrencyList = new List<Currency>();
            foreach (Currency c in currencyList)
            {
                intermediateCurrencyList = currencyList.FindAll(x => x.symbol == c.symbol && c.exchange != x.exchange);
                foreach (Currency t in intermediateCurrencyList)
                {

                    AddTriArbTransList(new TriArbTransaction(new Transaction(c, t, 1, Transaction.type.Transfer), false));
                }

            }


            //maybe not the best but this should add all the the forward transactions, reverse transaction, and the transfers

        }

        public List<TriArbTransaction> GetFinalSubListBySymbol(Exchange lookupE, string lookupS, string lastS)
        {
            return _triArbTransactionList.Where(x => x.baseCurrency.symbol == lookupS && x.baseCurrency.exchange == lookupE && x.quoteCurrency.symbol == lastS).ToList();
        }

        public List<TriArbTransaction> GetFinalSubListBySymbolAndExchange(Exchange lookupE, string lookupS, string lastS, Exchange lastExchange)
        {
            return _triArbTransactionList.Where(x => x.baseCurrency.symbol == lookupS && x.baseCurrency.exchange == lookupE && x.quoteCurrency.symbol == lastS && x.quoteCurrency.exchange == lastExchange).ToList();
        }

        public int GetMasterTransactionCount()
        {
            return _masterTransactionList.Count();
        }

        public string GetTransactionChainsOutput()
        {
            //TODO: this is the old output function. Needs to be called from the view model
            string s = "Found Transaction Chains: " + Environment.NewLine;
            _triArbTransactionChainList = _triArbTransactionChainList.OrderByDescending(x => x.arbitrageRate).ToList();
            foreach (TriArbOpportunity t in _triArbTransactionChainList)
            {
                s = s + t.arbitrageTransactionChain[0].baseCurrency.symbol + "-" + t.arbitrageTransactionChain[1].baseCurrency.symbol + "-" + t.arbitrageTransactionChain[2].baseCurrency.symbol + "-" + t.arbitrageTransactionChain[2].quoteCurrency.symbol + ":" + Math.Round((t.arbitrageRate * 100 - 100),3) + "% " + t.maxInitialVolume + Environment.NewLine;

            }
            return s;
        }

        public ObservableCollection<TriArbViewModel> GetTriArbOutput()
        {
            ObservableCollection<TriArbViewModel> r = new ObservableCollection<TriArbViewModel>();

            _triArbTransactionChainList = _triArbTransactionChainList.OrderByDescending(x => x.arbitrageRate).ToList();

            foreach (TriArbOpportunity t in _triArbTransactionChainList)
            {
                TriArbViewModel triArb = new TriArbViewModel(t);
                r.Add(triArb);
            }

            return r;
        } 

        public List<Transaction> GetTransSubListByExchange(Exchange e)
        {
            return _masterTransactionList.Where(x => x.baseCurrency.exchange == e && x.quoteCurrency.exchange == e).ToList();
        }

        public List<TriArbTransaction> GetTriArbSubList(Exchange e, string symbol)
        {
            return _triArbTransactionList.Where(x => x.baseCurrency.symbol == symbol && x.baseCurrency.exchange == e).ToList();
        }
        public int GetTriArbTransChainCount()
        {
            return _triArbTransactionChainList.Count();
        }

        public int GetTriArbTransListCount()
        {
            return _triArbTransactionList.Count();
        }

        public void RemoveTransactionByCoin(Currency coin)
        {
            _masterTransactionList.RemoveAll(x => x.baseCurrency.exchange == coin.exchange && x.baseCurrency.symbol == coin.symbol);
            _masterTransactionList.RemoveAll(x => x.quoteCurrency.exchange == coin.exchange && x.quoteCurrency.symbol == coin.symbol);
        }

        public void TranslateISOCurrencies()
        {
            foreach (Transaction t in _masterTransactionList)
            {
                //known iso standard conversions
                //XBT => BTC
                if (t.baseCurrency.symbol == "XBT")
                {
                    t.baseCurrency.symbol = "BTC";
                }

                if (t.quoteCurrency.symbol == "XBT")
                {
                    t.quoteCurrency.symbol = "BTC";
                }
            }
        }

        public void UpdateTransaction(Exchange e, string id, List<decimal[]> bidO, List<decimal[]> askO)
        {
            int index = _masterTransactionList.FindIndex(x => x.baseCurrency.exchange == e && x.quoteCurrency.exchange == e && x.identifier == id);
            _masterTransactionList[index].askOrderbook = askO;
            _masterTransactionList[index].bidOrderbook = bidO;
        }

        public void UpdateTransactionOrderbook(Exchange e, string id, Dictionary<decimal, decimal> bidV, Dictionary<decimal, decimal> askV)
        {
            Transaction trans = _masterTransactionList.First(x => x.baseCurrency.exchange == e && x.quoteCurrency.exchange == e && x.identifier == id);

        }

        internal List<Currency> GetCoinBlackList()
        {
            return _currencyBlackList;
        }
        internal List<Transaction> GetMasterTransactionList()
        {
            return _masterTransactionList;
        }
        private void AddTriArbTransList(TriArbTransaction t)
        {
            _triArbTransactionList.Add(t);
        }
    }

}
