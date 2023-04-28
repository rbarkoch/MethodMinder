using MethodMinder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MethodMinder.WPF
{
    /// <summary>
    /// Abstract base class for building up debouncers based on <see cref="DispatcherTimer"/>.
    /// </summary>
    public abstract class DebouncedDispatcherBase : DebouncerBase
    {
        /// <summary>
        /// The dispatcher which will dispatch the final invocation of the action.
        /// </summary>
        public Dispatcher Dispatcher { get; set; }

        /// <summary>
        /// The dispatcher priority to invoke the action with.
        /// </summary>
        public DispatcherPriority DispatcherPriority { get; set; } = DispatcherPriority.Normal;

        private DispatcherTimer _debounceTimer;

        /// <summary>
        /// Abstract constructor for debouncers based on <see cref="DispatcherTimer"/>. <see cref="DebouncedDispatcherBase"/>
        /// </summary>
        /// <param name="interval">Minimum time from when the method is debounced to when it is invoked.</param>
        public DebouncedDispatcherBase(TimeSpan interval) : base(interval)
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
            _debounceTimer = new DispatcherTimer()
            {
                Interval = interval
            };
            _debounceTimer.Tick += DebounceTimer_Tick;
        }

        private void DebounceTimer_Tick(object? sender, EventArgs e)
        {
            Halt();
            DebouncedInvoke();
        }

        /// <inheritdoc />
        protected override void RestartTiming()
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        /// <inheritdoc />
        public override void Halt()
        {
            _debounceTimer.Stop();
        }
    }
}
