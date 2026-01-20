using System;
using System.Collections.Generic;
using System.Text;

namespace services.Classes.Worker
{
    public class PeriodicWorker: IDisposable
    {
        private CancellationTokenSource? _cts;
        private Task? _task;

        public TimeSpan Interval { get; }

        private readonly Func<CancellationToken, Task> _work;

        public PeriodicWorker(TimeSpan interval, Func<CancellationToken, Task> work)
        {
            Interval = interval;
            _work = work;
        }

        public void Start()
        {
            if (_task != null && !_task.IsCompleted)
                return;

            _cts = new CancellationTokenSource();

            _task = Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        await _work(_cts.Token);
                        await Task.Delay(Interval, _cts.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        // cancelación esperada
                    }
                }
            }, _cts.Token);
        }

        public void Stop()
        {
            _cts?.Cancel();
        }

        public void Dispose()
        {
            Stop();
            _cts?.Dispose();
        }


    }
}
