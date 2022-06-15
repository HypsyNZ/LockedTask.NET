/*
*MIT License
*
*Copyright (c) 2022 S Christison
*
*Permission is hereby granted, free of charge, to any person obtaining a copy
*of this software and associated documentation files (the "Software"), to deal
*in the Software without restriction, including without limitation the rights
*to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
*copies of the Software, and to permit persons to whom the Software is
*furnished to do so, subject to the following conditions:
*
*The above copyright notice and this permission notice shall be included in all
*copies or substantial portions of the Software.
*
*THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
*IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
*FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
*AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
*LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
*OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
*SOFTWARE.
*/

using System;
using System.Threading.Tasks;
using System.Threading.Tasks.LockedTask;

namespace Example
{
    internal class Program
    {
        public static LockedTask lockedTask = new LockedTask();
        public static LockedTask lockedTask2 = new LockedTask();

        private static void Main(string[] args)
        {
            _ = Task.Run(() =>
            {
                while (true)
                {
                    lockedTask.RunAsync(RunTask1());
                }
            }).ConfigureAwait(false);

            _ = Task.Run(() =>
            {
                while (true)
                {
                    lockedTask.RunAsync(RunTask3(), 20);
                }
            }).ConfigureAwait(false);

            _ = Task.Run(() =>
            {
                while (true)
                {
                    lockedTask2.RunAsync(RunTask2(), 0);
                }
            }).ConfigureAwait(false);

            Console.ReadLine();
        }

        private static async Task RunTask1()
        {
            Console.WriteLine("1");
            await Task.Delay(128).ConfigureAwait(false);
        }

        private static async Task RunTask2()
        {
            Console.WriteLine("2");
            await Task.Delay(256).ConfigureAwait(false);
        }

        private static async Task RunTask3()
        {
            Console.WriteLine("3");
            await Task.Delay(512).ConfigureAwait(false);
        }
    }
}
