using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.ViewModel
{
    class TooMuchRowsException : Exception
    {
        public override string Message { get; } = "W tabeli nadrzędnej znajduje się zbyt mało rekordów. Proszę wygenerować więcej rekordów w tabeli nadrzędnej.";

    }
}
