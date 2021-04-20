using System;
using System.Diagnostics;

namespace _testNoRef {
    class Program {
        static void Main() {
            var dte = new DateTime(2001, 1, 1, 1, 1, 1, DateTimeKind.Unspecified);
            Debugger.Break();
        }
    }
}
