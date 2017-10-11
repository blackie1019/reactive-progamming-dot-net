using System;
using System.Threading;

namespace Lab_03
{
    public class MyObserver: IObserver<int>
    {
        public void OnNext(int value){
            var threadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Console.WriteLine($"Value received on thread with Id:{threadId}:{value}");
        }
        
        public void OnError(Exception ex){
            var threadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Console.WriteLine($"Exception happen on thread with Id:{threadId}:{ex.Message}");
        }
        public void OnCompleted(){
            var threadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Console.WriteLine($"Completed task on thread with Id:{threadId}");
        }
    }
}