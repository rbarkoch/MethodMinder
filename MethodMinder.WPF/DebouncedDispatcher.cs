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
    /// Debouncer based on <see cref="DispatcherTimer"/> which calls an action with no arguments.
    /// </summary>
    public class DebouncedDispatcher : DebouncedDispatcherBase
    {
        private Action _action;

        /// <summary>
        /// Constructor for <see cref="DebouncedDispatcher"/>.
        /// </summary>
        /// <param name="interval">Minimum time from when the method is debounced to when it is invoked.</param>
        /// <param name="action">Action to invoke when the debounce completes.</param>
        public DebouncedDispatcher(TimeSpan interval, Action action) : base(interval)
        {
            _action = action;
        }

        /// <inheritdoc />
        protected override void DebouncedInvoke()
        {
            Invoke();
        }

        /// <summary>
        /// Defers the invocation of the action.
        /// </summary>
        public void Debounce()
        {
            RestartTiming();
        }

        /// <inheritdoc />
        public void Invoke()
        {
            Halt();
            _action.Invoke();
        }
    }

    public class DebouncedDispatcher<T> : DebouncedDispatcherBase
    {
        private Action<T> _action;

        private T _arg;

        public DebouncedDispatcher(TimeSpan interval, Action<T> action) : base(interval)
        {
            _action = action;
        }

        /// <inheritdoc />
        protected override void DebouncedInvoke()
        {
            Invoke(_arg);
        }

        /// <summary>
        /// Defers the invocation of the action and queues the arguments of the invocation.
        /// </summary>
        /// <remarks>
        /// The invocation will pass arguments based on the latest call to the <see cref="Debounce(T)"/>.
        /// </remarks>
        /// <param name="arg">Arguments which will be passed to the invocation of the action.</param>
        public void Debounce(T arg)
        {
            _arg = arg;
            RestartTiming();
        }

        /// <inheritdoc />
        public void Invoke(T arg)
        {
            Halt();
            Dispatcher.Invoke(() => _action(_arg), DispatcherPriority);
            _arg = default;
        }
    }
}
