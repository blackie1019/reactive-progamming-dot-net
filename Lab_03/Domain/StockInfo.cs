namespace Lab_03.Domain
{
    class StockInfo
    {
        public StockInfo(string symbol, decimal price)
        {
            Symbol = symbol;
            PrevPrice = price;
        }
        public string Symbol { get; set; }
        public decimal PrevPrice { get; set; }
    }
}