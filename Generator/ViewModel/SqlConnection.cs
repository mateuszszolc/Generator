using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.ViewModel
{
    public static class SqlConnection
    {
        public static string connectionString { get; } = "Data Source = exampleServerIP;" +
            " User ID = ExampleUser;" +
            " Password = ExampleUser";

    }
}
