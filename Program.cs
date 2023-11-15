using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class Program
    {

        public static void Write(char c)
        {
            int i = 1000;
            while (i-- > 1)
            {
                Console.Write(c);
            }
        }


        public static int TestingText(object o)
        {
            Console.WriteLine($"Processing of task with object{o} with task id {Task.CurrentId}");
            return o.ToString().Length;
        }


        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            token.Register(() => { Console.WriteLine("Cancellation has been requested"); });
            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    if (token.IsCancellationRequested)
                        //token.ThrowIfCancellationRequested();
                        //throw new OperationCanceledException();
                        break;
                    Console.WriteLine($"{i++}");

                }
             },token);
            t.Start();

            Task.Factory.StartNew(() => { token.WaitHandle.WaitOne(); Console.WriteLine("wait handle released,cancellation was requested"); });

            Console.ReadKey();
            cts.Cancel();

            var text1 = "This";
            var text2 = "Testing";

            var Task1=new Task<int>(TestingText, text1);
            Task<int> Task2 = Task.Factory.StartNew(TestingText, text2);
            Task1.Start();

            Console.WriteLine($"the length of {text1} is {Task1.Result}");
            Console.WriteLine($"the length of {text2}is {Task2.Result}");

           /* Task.Factory.StartNew(() => Write('.'));
            var t = new Task(() => Write('?'));
            t.Start();
            Write('-');*/
            Console.WriteLine("Main Program Done.");
            Console.ReadKey();
        }
    }
}
