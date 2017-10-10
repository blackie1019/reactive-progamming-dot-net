using System;
using System.Reactive;

namespace Lab_01
{
    public class SimpleObserver: IObserver<int> 
    { 
      public void OnNext(int value) {  Console.WriteLine(value);} 
      public void OnCompleted() { Console.WriteLine("Completed"); } 
      public void OnError(Exception ex){ 
        Console.WriteLine("An Error Encountered"); 
        throw ex; 
      }   
    }
}