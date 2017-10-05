using System;

namespace Lab_03.Domain
{
    public interface IStockTicker
    {
        event EventHandler<StockTick> StockTick;
    }
}