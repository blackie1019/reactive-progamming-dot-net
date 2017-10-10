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
        static void Main (string[] args) {
            
            var threadId = Thread.CurrentThread.ManagedThreadId.ToString ();
            Console.WriteLine ($"Program is runing on thread with Id:{threadId}");
            
            Demo();

            Console.WriteLine("ByeBye");
        }

        static void Demo(){
            var isContinue = true;
            
            do{
                Console.WriteLine("======Press demo key to represent...=====");
                Console.WriteLine("(a) Demo MyObserver");
                Console.WriteLine("(b) Demo MyObserver with Lambda");
                Console.WriteLine("(c) Demo MyObserver with Lambda and NewThread");
                Console.WriteLine("(d) Demo MyObservable");
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
                    case ConsoleKey.A:
                        Demo_MyObserver();
                        break;
                    case  ConsoleKey.B:
                        Demo_MyObserverLambda();
                        break;
                    case  ConsoleKey.C:
                        Demo_MyObserverLambdaWithNewThread();
                        break;
                    case  ConsoleKey.D:
                        Demo_MyObservable();
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

        static void Demo_MyObservable () {
            var subject = new MyObservable(5,8);
            
            var observer = new MyObserver();

            var subscription = subject.Subscribe(observer);

            Console.WriteLine("Press any key to dispose the subscription...");
            Console.ReadKey();
            Console.WriteLine();
            subscription.Dispose();
        }
        
        static void Demo_MyObserverLambda () {
            var subject = Observable.Range(5, 8);
            
            var subscription = subject.Subscribe(
                (input) =>{Console.WriteLine($"Id:{Thread.CurrentThread.ManagedThreadId.ToString()}:{input}");},
                (err) => {Console.WriteLine(err.Message);},
                () => {Console.WriteLine("completed");}
            );

            Console.WriteLine("Press any key to dispose the subscription...");
            Console.ReadKey();
            Console.WriteLine();
            subscription.Dispose();
        }


        static void Demo_MyObserverLambdaWithNewThread () {
            
            var subscription = Observable.Range(5, 8)
                .SelectMany(i=>Observable.Start(()=>i, NewThreadScheduler.Default))
                //.ObserveOn(NewThreadScheduler.Default)
                .Subscribe(
                    (input) =>{Console.WriteLine($"Id:{Thread.CurrentThread.ManagedThreadId.ToString()}:{input}");},
                    (err) => {Console.WriteLine(err.Message);},
                    () => {Console.WriteLine("completed");});

            Console.WriteLine("Press any key to dispose the subscription...");
            Console.ReadKey();
            Console.WriteLine();
            subscription.Dispose();
        }

        static void Demo_MyObserver () {
            var subject = Observable.Range (5, 8);

            var observer = new MyObserver();

            var subscription = subject.Subscribe(observer);

            Console.WriteLine("Press any key to dispose the subscription...");
            Console.ReadKey ();
            Console.WriteLine();
            subscription.Dispose ();
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

            Console.WriteLine("Press any key to exit.");

            observable
                .Skip(10)
                .Where(number => number %2 ==0)
                .Subscribe(oddNumberObserver);

            observable
                .Where(number => number % 2 == 1)
                .Subscribe(evenNumberObserver);

            Console.ReadKey();
        }
    }
}
