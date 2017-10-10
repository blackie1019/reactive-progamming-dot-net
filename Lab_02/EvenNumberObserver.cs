using System;

namespace Lab_02
{
    public class EvenNumberObserver: IObserver<int> 
    { 
      public void OnNext(int value) {  Console.WriteLine(value);} 
      public void OnCompleted() { Console.WriteLine("Completed"); } 
      public void OnError(Exception ex){ 
        Console.WriteLine("An Error Encountered"); 
        throw ex; 
      }   
    }
}