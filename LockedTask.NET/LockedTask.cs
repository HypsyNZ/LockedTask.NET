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

namespace System.Threading.Tasks.LockedTask
{
    /// <summary>
    /// A Task that uses a Semiphore Automatically (Lock)
    /// <para>Only one of this Task can be running at a time, No exceptions</para>
    /// <para>Exceptions will be returned to caller</para>
    /// </summary>
    public class LockedTask
    {
        protected Func<Task> _task;
        protected bool _configureAwait = false;

        public SemaphoreSlim Semaphore { get; protected set; } = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <returns></returns>
        public async void RunAsync(Func<Task> theTask) => await Run(theTask, 0, false);

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <param name="timeout">How long to wait in milliseconds before returning without completing the Task</param>
        /// <returns></returns>
        public async void RunAsync(Func<Task> theTask, int timeout) => await Run(theTask, timeout, false);

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <param name="configureAwaiter">Set to true to wait for the Synchronization Context</param>
        /// <returns></returns>
        public async void RunAsync(Func<Task> theTask, bool configureAwaiter) => await Run(theTask, 0, configureAwaiter);

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <param name="timeout">How long to wait in milliseconds before returning without completing the Task</param>
        /// <param name="configureAwaiter">Set to true to wait for the Synchronization Context</param>
        /// <returns></returns>
        public async void RunAsync(Func<Task> theTask, int timeout, bool configureAwaiter) => await Run(theTask, timeout, configureAwaiter);

        /// <summary>
        /// Initialize a New Semiphore
        /// <para>You should do this before you start calling <see href="RunAsync()"/> if you want to change the default; especially if you are calling <see href="RunAsync()"/> in a loop</para>
        /// </summary>
        /// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
        /// <param name="maxCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
        public void NewSemiphore(int initialCount, int maxCount)
        {
            Semaphore = new SemaphoreSlim(initialCount, maxCount);
        }

        /// <summary>
        /// Initialize a New Semiphore
        /// <para>Represents a lightweight alternative to System.Threading.Semaphore</para>
        /// </summary>
        /// <param name="semaphore">Represents a lightweight alternative to System.Threading.Semaphore</param>
        public void NewSemiphore(SemaphoreSlim semaphore)
        {
            Semaphore = semaphore;
        }

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <returns></returns>
        public async void RunAsync(Func<Task> theTask, CancellationToken token) => await Run(theTask, 0, false, token);

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <param name="timeout">How long to wait in milliseconds before returning without completing the Task</param>
        /// <returns></returns>
        public async void RunAsync(Func<Task> theTask, int timeout, CancellationToken token) => await Run(theTask, timeout, false, token);

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <param name="configureAwaiter">Set to true to wait for the Synchronization Context</param>
        /// <returns></returns>
        public async void RunAsync(Func<Task> theTask, bool configureAwaiter, CancellationToken token) => await Run(theTask, 0, configureAwaiter, token);

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <param name="timeout">How long to wait in milliseconds before returning without completing the Task</param>
        /// <param name="configureAwaiter">Set to true to wait for the Synchronization Context</param>
        /// <returns></returns>
        public async void RunAsync(Func<Task> theTask, int timeout, bool configureAwaiter, CancellationToken token) => await Run(theTask, timeout, configureAwaiter, token);

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <param name="timeout">How long to wait in milliseconds before returning without completing the Task</param>
        /// <param name="configureAwaiter">Set to true to wait for the Synchronization Context</param>
        /// <returns></returns>
        protected async Task Run(Func<Task> theTask, int timeout = 0, bool configureAwaiter = false)
        {
            var r = await Semaphore.WaitAsync(timeout).ConfigureAwait(configureAwaiter);
            if (r)
            {
                try
                {
                    _task = theTask;
                    _configureAwait = configureAwaiter;
                    await _task.Invoke().ConfigureAwait(_configureAwait);
                }
                finally
                {
                    Semaphore.Release();
                }
            }
        }

        /// <summary>
        /// Run the Task Asynchronously, Waiting for the lock if it is busy.
        /// </summary>
        /// <param name="theTask">Task to run inside the lock</param>
        /// <param name="timeout">How long to wait in milliseconds before returning without completing the Task</param>
        /// <param name="configureAwaiter">Set to true to wait for the Synchronization Context</param>
        /// <returns></returns>
        protected async Task Run(Func<Task> theTask, int timeout = 0, bool configureAwaiter = false, CancellationToken token = default)
        {
            var r = await Semaphore.WaitAsync(timeout, token).ConfigureAwait(configureAwaiter);
            if (r)
            {
                try
                {
                    _task = theTask;
                    _configureAwait = configureAwaiter;
                    await _task.Invoke().ConfigureAwait(_configureAwait);
                }
                finally
                {
                    Semaphore.Release();
                }
            }
        }
    }
}
