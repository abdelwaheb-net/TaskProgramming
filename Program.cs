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
            var t = new Task(()=> {
                Console.WriteLine("I Take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                    Console.WriteLine($"{i}s");
                }
                
                Console.WriteLine("I'm done");

            },token);
            t.Start();
            
           var t2=Task.Factory.StartNew(() => { Console.WriteLine("begining Task2"); Thread.Sleep(3000); Console.WriteLine("End Task 2 after 3 seconds"); },token);
            //Console.ReadKey();
            //cts.Cancel();
            //Task.WaitAll(t, t2);
            Task.WaitAll(new[] { t, t2 },4000,token);
          
            Console.WriteLine($"Task t status is{t.Status}");
            Console.WriteLine($"Task t2 status is{t2.Status}");

            Console.WriteLine("Main Program Done.");
            Console.ReadKey();
        }
    }
}
