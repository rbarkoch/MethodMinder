using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace MethodMinder.Core
{
    public class DebouncedAction : IDisposable
    {
        public TimeSpan DebounceInterval
        {
            get => TimeSpan.FromMilliseconds(_debounceTimer.Interval);
            set => _debounceTimer.Interval = value.TotalMilliseconds;
        }

        public TimeSpan MaximumDelay { get; set; } = TimeSpan.MaxValue;

        public bool IsDebouncing => _debounceTimer.Enabled;

        private DateTime _initialCallTime;
        private Timer _debounceTimer;
        private Action _action;

        public DebouncedAction(Action action)
        {
            _action = action;
            _debounceTimer = new Timer()
            {
                AutoReset = false
            };
            _debounceTimer.Elapsed += DebounceTimer_Elapsed;
        }

        private void DebounceTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Invoke();
        }

        public void Debounce()
        {
            if(IsDebouncing)
            {
                if(DateTime.Now - _initialCallTime > MaximumDelay)
                {
                    Invoke();
                    return;
                }

                _debounceTimer.Stop();
                _debounceTimer.Start();
            }
            else
            {
                _initialCallTime = DateTime.Now;
                _debounceTimer.Start();
            }
        }

        public void Invoke()
        {
            Halt();
            _action.Invoke();
        }

        public void Halt()
        {
            _debounceTimer.Stop();
        }

        public void Dispose()
        {
            _debounceTimer.Dispose();
        }
    }

    public class DebouncedAction<T> : IDisposable
    {
        public TimeSpan DebounceInterval
        {
            get => TimeSpan.FromMilliseconds(_debounceTimer.Interval);
            set => _debounceTimer.Interval = value.TotalMilliseconds;
        }

        public TimeSpan MaximumDelay { get; set; } = TimeSpan.MaxValue;

        public bool IsDebouncing => _debounceTimer.Enabled;


        private DateTime _initialCallTime;
        private Timer _debounceTimer;
        private Action<T> _action;

        private T _arg;

        public DebouncedAction(Action<T> action)
        {
            _action = action;
            _debounceTimer = new Timer()
            {
                AutoReset = false
            };
            _debounceTimer.Elapsed += DebounceTimer_Elapsed;
        }

        private void DebounceTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Invoke();
        }

        public void Debounce(T arg)
        {
            _arg = arg;

            if (IsDebouncing)
            {
                if (DateTime.Now - _initialCallTime > MaximumDelay)
                {
                    Invoke();
                    return;
                }

                _debounceTimer.Stop();
                _debounceTimer.Start();
            }
            else
            {
                _initialCallTime = DateTime.Now;
                _debounceTimer.Start();
            }
        }

        public void Invoke()
        {
            Halt();
            _action.Invoke(_arg);
        }

        public void Halt()
        {
            _debounceTimer.Stop();
        }

        public void Dispose()
        {
            _debounceTimer.Dispose();
        }
    }
}
