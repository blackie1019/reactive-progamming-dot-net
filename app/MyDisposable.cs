using System;
namespace app {
    public class MyDisposable : IDisposable {
        public void Dispose () {
            Console.WriteLine("Done Dispose");
        }
    }
}