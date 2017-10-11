using System;

namespace Lab_04.Domain
{
    public interface IStockTicker
    {
        event EventHandler<StockTick> StockTick;
    }
}