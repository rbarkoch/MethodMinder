# MethodMinder
A .NET library to debounce method calls.

## Typical Use Cases
- Preventing changes to a textbox from triggering a search until a user is done typing.
- Detecting when a user has completed a scrolling operation.
- Preventing users from spamming button clicks.
- Rate limiting events.


## Examples
```csharp
DebouncedAction action = new DebouncedAction(TimeSpan.FromMilliseconds(150), ExpensiveMethod);
...
public void ExpensiveMethod(){ }
...
action.Debounce(); // Call this to defer the execution of the action.
action.Invoke(); // Call this to cancel any deferred execution and invoke immediately.



DebouncedAction<MyArgument> actionWithArguments = new DebouncedAction<MyArgument>(TimeSpan.FromMilliseconds(150), ExpensiveMethodWithArguments);
...
public void ExpensiveMethodWithArguments(MyArgument arg){ }
...
actionWithArguments.Debounce(arg); // Call this to defer the execution of the action.
action.Invoke(arg); // Call this to cancel any deferred execution and invoke immediately.
```

For WPF, using DebouncedDispatcher can give more flexibility over how the method is dispatched.