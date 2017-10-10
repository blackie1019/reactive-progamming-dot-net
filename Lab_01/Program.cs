using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Lab_01 {
    class Program {
        static void Main (string[] args) {
            Demo ();

            Console.WriteLine ("ByeBye");
        }

        static void Demo () {
            var isContinue = true;

            do {
                Console.WriteLine ("======Press demo key to represent...=====");
                Console.WriteLine ("(a) Demo Array");
                Console.WriteLine ("(e) Demo Linq With Subscribling");
                Console.WriteLine ("(q) Exit Menu");

                var keyInfo = Console.ReadKey ();
                Console.WriteLine ();

                switch (keyInfo.Key) {
                    case ConsoleKey.Q:
                        isContinue = false;
                        break;
                    case ConsoleKey.A:
                        Demo_Array ();
                        break;
                    case  ConsoleKey.E:
                        Demo_LinqWithSubscribling();
                        break;
                    default:
                        Console.WriteLine ("Unknow, please re-try!!");
                        break;
                }
                
            } while (isContinue);
        }
        static void Demo_Array () {
            IObservable<string> subject =
                new [] { "Jordan", "Kobe", "James", "Durant" }.ToObservable ();

            Console.WriteLine ("Player Name StartsWith J and ToUpper ");

            IDisposable subscription =
                subject.Where (str => str.StartsWith ("J"))
                .Select (str => str.ToUpper ())
                .Subscribe (Console.WriteLine);

            subscription.Dispose ();
        }
        static void Demo_LinqWithSubscribling(){
            var subject = 
                from i in Observable.Range(1, 100) 
                from j in Observable.Range(1, 100) 
                from k in Observable.Range(1, 100) 
                where k * k == i * i + j * j 
                select new { a = i, b = j, c = k };   
        
            IDisposable subscription = subject.Subscribe( 
            x => Console.WriteLine("OnNext: {0}", x),  
            ex => Console.WriteLine("OnError: {0}", ex.Message), 
            () => Console.WriteLine("OnCompleted")); 
        }
    }
}