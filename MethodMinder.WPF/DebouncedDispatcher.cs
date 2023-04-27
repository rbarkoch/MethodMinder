using MethodMinder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MethodMinder.WPF
{
    public class DebouncedDispatcher
    {
        public Dispatcher Dispatcher { get; set; }
        public DispatcherPriority DispatcherPriority { get; set; } = DispatcherPriority.Normal;

        public TimeSpan DebounceInterval
        {
            get => _debounceTimer.Interval;
            set => _debounceTimer.Interval = value;
        }

        public TimeSpan MaximumDelay { get; set; } = TimeSpan.MaxValue;

        public bool IsDebouncing => _debounceTimer.IsEnabled;

        private DateTime _initialCallTime;
        private DispatcherTimer _debounceTimer = new DispatcherTimer();
        private Action _action;

        public DebouncedDispatcher(Action action)
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
            _action = action;
            _debounceTimer.Tick += DebounceTimer_Tick;
        }

        private void DebounceTimer_Tick(object? sender, EventArgs e)
        {
            Invoke();
        }

        public void Debounce()
        {
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
            Dispatcher.Invoke(_action, DispatcherPriority);
        }

        public void Halt()
        {
            _debounceTimer.Stop();
        }
    }

    public class DebouncedDispatcher<T>
    {
        public Dispatcher Dispatcher { get; set; }
        public DispatcherPriority DispatcherPriority { get; set; } = DispatcherPriority.Normal;

        public TimeSpan DebounceInterval
        {
            get => _debounceTimer.Interval;
            set => _debounceTimer.Interval = value;
        }

        public TimeSpan MaximumDelay { get; set; } = TimeSpan.MaxValue;

        public bool IsDebouncing => _debounceTimer.IsEnabled;


        private DateTime _initialCallTime;
        private DispatcherTimer _debounceTimer = new DispatcherTimer();
        private Action<T> _action;

        private T _arg;

        public DebouncedDispatcher(Action<T> action)
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
            _action = action;
            _debounceTimer.Tick += DebounceTimer_Tick; ;
        }

        private void DebounceTimer_Tick(object? sender, EventArgs e)
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
            Dispatcher.Invoke(() => _action(_arg), DispatcherPriority);
        }

        public void Halt()
        {
            _debounceTimer.Stop();
        }
    }
}
