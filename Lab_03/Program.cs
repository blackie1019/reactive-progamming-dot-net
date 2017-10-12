using System;
using System.Threading;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive;

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
                Console.WriteLine("(a) Demo Observable.Create");
                Console.WriteLine("(b) Demo MyObserver");
                Console.WriteLine("(c) Demo Observer with Lambda");
                Console.WriteLine("(d) Demo Observer with Lambda and NewThread");
                Console.WriteLine("(e) Demo MyObservable");
                Console.WriteLine("(q) Exit Menu");

                var keyInfo = Console.ReadKey();
                Console.WriteLine();

                switch(keyInfo.Key){
                    case ConsoleKey.Q:
                        isContinue = false;
                        break;
                    case ConsoleKey.A:
                        Demo_ObservableCreate();
                        break;
                    case ConsoleKey.B:
                        Demo_MyObserver();
                        break;
                    case  ConsoleKey.C:
                        Demo_LambdaObserver();
                        break;
                    case  ConsoleKey.D:
                        Demo_ObserverWithNewThread();
                        break;
                    case  ConsoleKey.E:
                        Demo_MyObservable();
                        break;
                    default:
                        Console.WriteLine("Unknow, please re-try!!");
                        break;
                } 

            }while(isContinue); 
        }

        static void Demo_ObservableCreate(){
            var subject = Observable.Range(5,8);

            var observer = Observer.Create<int>(
                x => Console.WriteLine("OnNext: {0}", x),
                ex => Console.WriteLine("OnError: {0}", ex.Message),
                () => Console.WriteLine("OnCompleted"));

            var subscription = subject.Subscribe(observer);
            Console.WriteLine("Press ENTER to unsubscribe...");
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

        static void Demo_LambdaObserver () {
            var subject = Observable.Range(5, 8);
            
            var subscription = subject.Subscribe(
                (input) =>{Console.WriteLine($"Process with Lambda, Value:{input} received on thread with Id:{Thread.CurrentThread.ManagedThreadId.ToString()}");},
                (err) => {Console.WriteLine(err.Message);},
                () => {Console.WriteLine("completed");}
            );

            Console.WriteLine("Press any key to dispose the subscription...");
            Console.ReadKey();
            Console.WriteLine();
            subscription.Dispose();
        }


        static void Demo_ObserverWithNewThread () {
            
            var subscription = Observable.Range(5, 8)
                .SelectMany(i=>Observable.Start(()=>i, NewThreadScheduler.Default))
                .Subscribe(
                    (input) =>{Console.WriteLine($"Value:{input} received on thread with Id:{Thread.CurrentThread.ManagedThreadId.ToString()}");},
                    (err) => {Console.WriteLine(err.Message);},
                    () => {Console.WriteLine("completed");});

            Console.WriteLine("Press any key to dispose the subscription...");
            Console.ReadKey();
            Console.WriteLine();
            subscription.Dispose();
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
    }
}
