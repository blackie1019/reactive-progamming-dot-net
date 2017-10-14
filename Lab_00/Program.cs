using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Threading;

namespace Lab_00 {
    class Program {
        static void Main (string[] args) {
            Demo ();

            Console.WriteLine ("ByeBye");
        }

        static void Demo () {
            var isContinue = true;

            do {
                Console.WriteLine ("======Press demo key to represent...=====");
                Console.WriteLine ("(s) Demo Sample");
                Console.WriteLine ("(r) Demo Reactive Sample");
                Console.WriteLine ("(q) Exit Menu");

                var keyInfo = Console.ReadKey ();
                Console.WriteLine();
                
                while (true) {
                    switch (keyInfo.Key) {
                        case ConsoleKey.Q:
                            isContinue = false;
                            break;
                        case ConsoleKey.S:
                            Demo_Sample ();
                            break;
                        case ConsoleKey.R:
                            Demo_ReactiveSample ();
                            break;
                        default:
                            Console.WriteLine ("Unknow, please re-try!!");
                            break;
                    }

                    Console.Write ("enter symbol (or x to exit): ");
                    var symbol = Console.ReadLine ();
                    if (symbol.ToLower () == "x") {
                        break;
                    }
                }

            } while (isContinue);
        }
        static void Demo_Sample () {

            Console.Write ("Press input A:");
            var a = Convert.ToInt32 (Console.ReadLine ());

            Console.Write ("Press input B:");
            var b = Convert.ToInt32 (Console.ReadLine ());

            var c = a + b;
            Console.WriteLine ("before: the value of c is {0}", c);

            Console.Write ("Press input B again:");
            b = Convert.ToInt32 (Console.ReadLine ());
            
            c = a + b;
            Console.WriteLine ("after: the value of c is {0}", c);
        }

        static void Demo_ReactiveSample () {

            Console.Write ("Press input A:");
            var a = Convert.ToInt32 (Console.ReadLine ());

            Console.Write ("Press input B:");
            var subscription = Observable
                .FromAsync (() => Console.In.ReadLineAsync ())
                .Subscribe (b => Console.WriteLine ("the value of c is {0}", a + Convert.ToInt32 (b)));

        }
    }
}