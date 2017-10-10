using System;

namespace Lab_02
{
    public class OddNumberObserver: IObserver<int> 
    { 
      public void OnNext(int value) {  
        Console.WriteLine("Odd!! Value is {0}",value);
      } 
      public void OnCompleted() { 
        Console.WriteLine("OddNumberObserver Completed");
      } 
      public void OnError(Exception ex){ 
        Console.WriteLine("An Error Encountered on OddNumberObserver"); 
        throw ex; 
      }   
    }
}