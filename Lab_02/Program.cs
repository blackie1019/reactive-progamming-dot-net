using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Reactive.PlatformServices;
using System.Collections.Generic;

namespace Lab_02
{
    public class Program {
        static void Main (string[] args) 
        {
            
            Console.WriteLine ("Program is runing");
            
            Demo();

            Console.WriteLine("ByeBye");
        }

        static void Demo(){
            var isContinue = true;
            
            do{
                Console.WriteLine("======Press demo key to represent...=====");
                Console.WriteLine("(e) Demo EvenNumbers");
                Console.WriteLine("(r) Demo EvenNumbers by Rx.NET Example");
                Console.WriteLine("(n) Demo Odd and Even Numbers by Rx.NET Example");
                Console.WriteLine("(q) Exit Menu");

                var keyInfo = Console.ReadKey();
                Console.WriteLine();

                switch(keyInfo.Key){
                    case ConsoleKey.Q:
                        isContinue = false;
                        break;
                    case ConsoleKey.E:
                        Demo_EvenNumbers ();
                        break;
                    case ConsoleKey.R:
                        Demo_EvenNumbersByRxNetExample ();
                        break;
                    case ConsoleKey.N:
                        Demo_OddEvenNumbersByRxNetExample ();
                        break;
                    default:
                        Console.WriteLine("Unknow, please re-try!!");
                        break;
                } 

            }while(isContinue); 
        }
        static void Demo_EvenNumbers () {
            IEnumerable<int> number_sequence = new int[] { 1, 2, 3, 4, 5, 6, 8 };

            foreach (var n in number_sequence){
                if (n % 2 == 0)
                    Console.WriteLine (n);
            }

        }

        static void Demo_EvenNumbersByRxNetExample () {

            IDisposable subscription = new EvenNumberObservable (new [] { 1, 2, 3, 4, 6, 7, 8 })
                .Subscribe (new EvenNumberObserver ());

            subscription.Dispose ();
        }
        
        static void Demo_OddEvenNumbersByRxNetExample(){
            NumberGenerator ng = new NumberGenerator(1000);
            var observable = Observable.ToObservable(ng.GenerateNumbers(), Scheduler.Default);
            var oddNumberObserver = new OddNumberObserver();
            var evenNumberObserver = new EvenNumberObserver();

            observable
                .Skip(10)
                .Where(number => number %2 ==0)
                .Subscribe(oddNumberObserver);

            observable
                .Where(number => number % 2 == 1)
                .Subscribe(evenNumberObserver);

        }
    }
}
