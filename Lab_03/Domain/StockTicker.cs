using System;

namespace Lab_03.Domain
{
    public class StockTicker : IStockTicker
    {
        public event EventHandler<StockTick> StockTick= delegate {};

        public void Notify(StockTick tick)
        {
            StockTick(this, tick);
        }
    }
}