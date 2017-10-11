using System;

namespace Lab_03
{
    public class MyDisposable : IDisposable {
        public void Dispose () {
            Console.WriteLine("Done Dispose");
        }
    }
}