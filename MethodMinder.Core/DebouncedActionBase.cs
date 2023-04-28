using System;
using System.Timers;

namespace MethodMinder.Core
{
    /// <summary>
    /// Abstract base class for building up debouncers based on <see cref="System.Timers.Timer"/>.
    /// </summary>
    public abstract class DebouncedActionBase : DebouncerBase, IDisposable
    {
        private Timer _debounceTimer;

        /// <summary>
        /// Abstract constructor for debouncers based on <see cref="System.Timers.Timer"/>. <see cref="DebouncedActionBase"/>
        /// </summary>
        /// <param name="interval">Minimum time from when the method is debounced to when it is invoked.</param>
        public DebouncedActionBase(TimeSpan interval) : base(interval)
        {
            _debounceTimer = new Timer()
            {
                AutoReset = false,
                Interval = interval.TotalMilliseconds
            };
            _debounceTimer.Elapsed += DebounceTimer_Elapsed;
        }

        /// <inheritdoc />
        protected override void RestartTiming()
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private void DebounceTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Halt();
            DebouncedInvoke();
        }

        /// <inheritdoc />
        public override void Halt()
        {
            _debounceTimer.Stop();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _debounceTimer.Dispose();
        }
    }
}