using System;
using System.Reactive.Linq;

namespace Lab_01
{
    class Program
    {
        static void Main(string[] args)
        {
            IObservable<string> strings =
                new[] { "Jordan", "Kobe", "James", "Durant" }.ToObservable(); 

            IDisposable subscription =  
                strings.Where(str => str.StartsWith("J"))  
                    .Select(str => str.ToUpper()) 
                    .Subscribe(Console.WriteLine); 

            subscription.Dispose();
        }
    }
}
