using System;
using System.Collections.Generic;
using System.Text;

namespace MethodMinder.Core
{
    /// <summary>
    /// Abstract base class for building up debouncers.
    /// </summary>
    public abstract class DebouncerBase
    {
        /// <summary>
        /// Abstract constructor for debouncers. <see cref="DebouncerBase"/>
        /// </summary>
        /// <param name="interval">Minimum time from when the method is debounced to when it is invoked.</param>
        public DebouncerBase(TimeSpan interval) { }

        /// <summary>
        /// Restarts the underlying timers for debouncing.
        /// </summary>
        protected abstract void RestartTiming();

        /// <summary>
        /// Halts any pending execution and executes immediately.
        /// </summary>
        public abstract void Invoke();

        /// <summary>
        /// Halts and pending execution of the debouncer.
        /// </summary>
        public abstract void Halt();
    }
}
