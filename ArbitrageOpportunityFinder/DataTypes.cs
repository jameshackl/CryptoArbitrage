using System;
using System.Collections.Generic;
using System.Linq;

namespace ArbitrageOpportunityFinder
{
    internal class DataTypes
    {
    }

    public class Exchange
    {
        public string name { get; set; }
    }

    public class TriArbOpportunity //triangular arbitrage opportunity
    {
        public int depth { get { return arbitrageTransactionChain.Count(); } }

        public TriArbOpportunity()
        {
        }

        public TriArbOpportunity(List<TriArbTransaction> ltat)
        {
            arbitrageTransactionChain = ltat;
        }

        public decimal arbitrageRate
        {
            get
            {
                return arbitrageTransactionChain.Select(x => x.takerIndicatorRate).Aggregate((x, y) => x * y);
            }
        }

        public int limitingTransactionIndex { get; private set; }

        public decimal maxInitialVolume
        {
            get
            {
                decimal[] x = new decimal[depth];
                for (int i = 0; i < depth; i++)
                {
                    x[i] = arbitrageTransactionChain[i].takerRateVolume;
                    for (int j = 0; j <= i; j++)
                    {
                        if (arbitrageTransactionChain[j].transType == Transaction.type.Transfer)
                        {
                            //multiply by 1 or, in this case, do nothing
                        }
                        else
                        {
                            if (arbitrageTransactionChain[j].takerIndicatorRate != 0)
                            {
                                x[i] /= arbitrageTransactionChain[j].takerIndicatorRate;
                            }
                            else
                            {
                                x[i] = 0;
                            }
                        }
                    }
                }
                limitingTransactionIndex = Array.IndexOf(x, x.Min());
                return x.Min();
            }
        }

        public List<TriArbTransaction> arbitrageTransactionChain { get; set; }
        public int length { get; set; }
    }

    public class TriArbTransaction //triangular arbitrage transaction
    {
        public TriArbTransaction(Transaction t, bool i)
        {
            transaction = t;
            inversed = i;
        }

        private Transaction transaction { get; set; }

        public decimal takerRateVolume
        {
            get
            {
                if (inversed)
                {
                    return transaction.askVolume;
                }
                else
                {
                    return transaction.bidVolume * transaction.bid;
                }
            }
        }

        public Transaction.type transType { get { return transaction.transactionType; } }

        public Currency baseCurrency
        {
            get
            {
                if (inversed)
                {
                    return transaction.quoteCurrency;
                }
                else
                {
                    return transaction.baseCurrency;
                }
            }
        }

        public Currency quoteCurrency
        {
            get
            {
                if (inversed)
                {
                    return transaction.baseCurrency;
                }
                else
                {
                    return transaction.quoteCurrency;
                }
            }
        }

        public decimal takerIndicatorRate
        {
            get
            {
                if (inversed)
                {
                    if (transaction.ask != 0)
                    {
                        return Math.Round(1 / transaction.ask, 10);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return transaction.bid;
                }
            }
        }

        public bool inversed { get; set; } //for when it gets built from one transaction. this tracks the ones that get inversed
    }

    public class Transaction
    {
        public Transaction()
        {
        }

        //for finding cross exchange transfers
        public Transaction(Currency b, Currency q, decimal r, Transaction.type t)
        {
            bidOrderbook = new List<decimal[]>();
            askOrderbook = new List<decimal[]>();
            bidOrderbook.Add(new decimal[] { 1, decimal.MaxValue }); //TODO: get the actual numbers from either side's orderbook
            askOrderbook.Add(new decimal[] { 1, decimal.MaxValue }); //TODO: get the actual numbers from either side's orderbook
            baseCurrency = b;
            quoteCurrency = q;
            transactionType = t;
        }

        public Currency baseCurrency { get; set; }
        public Currency quoteCurrency { get; set; }
        public string identifier { get; set; } //this is for updating transaction data, maybe

        public decimal bid
        {
            get
            {
                try { return bidOrderbook.Count == 0 ? 0 : bidOrderbook.ElementAt(0)[0]; }
                catch { return 0; }
            }
            //set
        }

        public decimal ask
        {
            get
            {
                try { return askOrderbook.Count == 0 ? 0 : askOrderbook.ElementAt(0)[0]; }
                catch { return 0; }
            }
            private set { }
        }

        //quoted in the base currency
        public decimal bidVolume
        {
            get
            {
                if (bidOrderbook.Count() == 0)
                {
                    return 0;
                }
                else
                {
                    return bidOrderbook.ElementAt(0)[1];
                }
            }
            //set;
        }

        public decimal askVolume
        {
            get
            {
                if (askOrderbook.Count() == 0)
                {
                    return 0;
                }
                else
                {
                    return askOrderbook.ElementAt(0)[1];
                }
            }
            //set;
        }

        //price, volume
        public List<decimal[]> bidOrderbook { get; set; }

        public List<decimal[]> askOrderbook { get; set; }

        //to determine data age
        public DateTime lastUpdated { get; set; }

        //TODO: depth analysis. sometimes there is profit to be found further from the best price
        public type transactionType { get; set; }

        public decimal minimunTradeVolume { get; set; }

        public enum type
        {
            MarginTrade, //transaction can utilize margin
            Transfer, //currency is transfered between exchanges.incurrs a transfer fee
            Trade //one currency is echanged for another.
        }

        //this is a "mulitplier"
        public decimal margin { get; set; }

        //typically 0.002 or 0.2%
        public decimal fee { get; set; }

        //TODO:time . some trades take a realively known amount of time that is significant. this is going to be difficult as some exchanges don't broadcast this over the api
    }

    public class Currency
    {
        public Currency()
        {
        }

        public Currency(string s, GlobalData.Exchange e)
        {
            symbol = s;
            exchange = e;
        }

        public string symbol { get; set; }
        public string name { get; set; }
        public GlobalData.Exchange exchange { get; set; }

        //public string id { get; set; }
        public decimal withdrawlFee { get; set; }

        public decimal minimumTransaction { get; set; }//this may get dealt with differently
        public decimal maxWithdrawl { get; set; } //per day
        public decimal minWithdrawl { get; set; } //per day
    }

    //    public class CrossExArbOpportunity //cross exchange arbitrage opportunity
    //    {
    //    }
}