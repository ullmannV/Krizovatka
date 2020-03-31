using System;

namespace TechnologickeDemoKrizovatka
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] pole = { 45, 1, 2, 3 };
            Semafor semafor = new Semafor(2, 0, 1, 2, pole);
            
            Console.WriteLine("{0}", semafor.State);
            Console.ReadKey();
        }
    }
}
