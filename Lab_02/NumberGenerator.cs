using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab_02 {
    public class NumberGenerator {
        int sequencer;
        int sleepTime;

        public NumberGenerator (int time=250) {
            sequencer = 0;
            sleepTime= time;
        }

        public IEnumerable<int> GenerateNumbers () {
            for (int i = 0;; i++) {
                if (i % 2 == 0) {
                    Thread.Sleep (this.sleepTime);

                }

                yield return this.sequencer++;

            }

        }
    }
}