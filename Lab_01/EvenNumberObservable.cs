using System;
using System.Collections.Generic;
using System.Reactive;

namespace Lab_01 {
    class EvenNumberObservable : IObservable<int> {
        private IEnumerable<int> _numbers;

        public EvenNumberObservable (IEnumerable<int> numbers) {
            this._numbers = numbers;
        }

        public IDisposable Subscribe (IObserver<int> observer) {
            foreach (var number in _numbers) {
                if (number % 2 == 0)
                    observer.OnNext (number);
            }
            observer.OnCompleted ();

            return new EmptyDisposable { action = () => Console.WriteLine("Dispose Done!") };
        }
    }

    class EmptyDisposable : IDisposable {
        internal Action action { get; set; }
        void IDisposable.Dispose () { this.action (); }
    }
}