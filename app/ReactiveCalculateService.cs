using System;

namespace app {
    public sealed class ReactiveCalculateService {
        private static readonly Lazy<ReactiveCalculateService> lazy =
            new Lazy<ReactiveCalculateService> (() => new ReactiveCalculateService ());

        public static ReactiveCalculateService Instance { get { return lazy.Value; } }

        private ReactiveCalculateService () {

        }

        public void Sum () {

        }
    }
}