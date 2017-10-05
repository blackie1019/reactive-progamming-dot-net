using System;

namespace Lab_02
{
    public class MyDisposable : IDisposable {
        public void Dispose () {
            Console.WriteLine("Done Dispose");
        }
    }
}