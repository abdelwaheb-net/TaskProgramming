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
            //Thread.SpinWait(100000000);
            //SpinWait.SpinUntil();

            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var task = new Task(()=> {
                Console.WriteLine("Press Any key to disarm, you have 5 second to disarm");
                bool cancelled=token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled?"Bomb Disarmed":"Boom!!!");

            },token);
            task.Start();
            Console.ReadKey();
            cts.Cancel();
            
          
            Console.WriteLine("Main Program Done.");
            Console.ReadKey();
        }
    }
}
