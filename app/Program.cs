using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;

namespace app {
    public class Program {
        static void Main (string[] args) {
            
            var threadId = Thread.CurrentThread.ManagedThreadId.ToString ();
            Console.WriteLine ($"Program is runing on thread with Id:{threadId}");
            
            Demo();

            Console.WriteLine("Press any key to exit the program...");
            Console.ReadKey();
        }

        static void Demo(){
            var isExisted = false;
            
            do{
                Console.WriteLine("======Press demo key to represent...=====");
                Console.WriteLine("(a) Demo MyObserver");
                Console.WriteLine("(b) Demo MyObserver with Lambda");
                Console.WriteLine("(c) Demo MyObservable");
                Console.WriteLine("(q) Exit Menu");

                var keyInfo = Console.ReadKey();

                switch(keyInfo.Key){
                    case ConsoleKey.A:
                        Demo_MyObserver();
                        break;
                    case  ConsoleKey.B:
                        Demo_MyObserverLambda();
                        break;
                    case  ConsoleKey.C:
                        Demo_MyObservable();
                        break;
                    default:
                        Console.WriteLine("Unknow, please re-try!!");
                        break;
                } 
                        
                isExisted = (keyInfo.Key == ConsoleKey.Q);

            }while(isExisted); 
        }

        static void Demo_MyObservable () {
            var observable = new MyObservable(5,8);
            
            var observer = new MyObserver();

            var subscription = observable.Subscribe(observer);

            Console.WriteLine("Press any key to dispose the subscription...");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void Demo_MyObserverLambda () {
            var observable = Observable.Range(5, 8);
            
            var subscription = observable.Subscribe(
                (input) =>{Console.WriteLine($"Id:{Thread.CurrentThread.ManagedThreadId.ToString()}:{input}");},
                (err) => {Console.WriteLine(err.Message);},
                () => {Console.WriteLine("completed");}
            );

            Console.WriteLine("Press any key to dispose the subscription...");
            Console.ReadKey();
            subscription.Dispose();
        }

        static void Demo_MyObserver () {
            var observable = Observable.Range (5, 8);

            var observer = new MyObserver();

            var subscription = observable.Subscribe(observer);

            Console.WriteLine("Press any key to dispose the subscription...");
            Console.ReadKey ();
            subscription.Dispose ();
        }

        static void General(){
            int a = 2;
            int b = 3;
            int c = a + b;
            Console.WriteLine("before: the value of c is {0}",c);
            a=7;
            b=2;
            Console.WriteLine("after: the value of c is {0}",c);
        }
    }
}