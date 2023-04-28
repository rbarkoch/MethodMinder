using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace MethodMinder.Core
{
    /// <summary>
    /// Debouncer based on <see cref="System.Timers.Timer"/> which calls an action with no arguments.
    /// </summary>
    public class DebouncedAction : DebouncedActionBase
    {
        private Action _action;

        /// <summary>
        /// Constructor for <see cref="DebouncedAction"/>.
        /// </summary>
        /// <param name="interval">Minimum time from when the method is debounced to when it is invoked.</param>
        /// <param name="action">Action to invoke when the debounce completes.</param>
        public DebouncedAction(TimeSpan interval, Action action) : base(interval)
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

    /// <summary>
    /// Debouncer based on <see cref="System.Timers.Timer"/> which calls an action with one arguments.
    /// </summary>
    public class DebouncedAction<T> : DebouncedActionBase
    {
        private Action<T> _action;

        private T? _arg;

        /// <summary>
        /// Constructor for <see cref="DebouncedAction{T}"/>.
        /// </summary>
        /// <param name="interval">Minimum time from when the method is debounced to when it is invoked.</param>
        /// <param name="action">Action to invoke when the debounce completes.</param>
        public DebouncedAction(TimeSpan debounceInterval, Action<T> action) : base(debounceInterval)
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
            _action.Invoke(arg);
            _arg = default;
        }
    }
}
