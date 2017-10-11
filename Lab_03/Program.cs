using System;
using System.Threading;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace Lab_03
{
    class Program
    {
        static void Main(string[] args)
        {
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
    }
}
