using System;

namespace Lab_02
{
    class EmptyDisposable : IDisposable {
        internal Action action { get; set; }
        void IDisposable.Dispose () { this.action (); }
    }
}