namespace MethodMinder.Core.Examples
{
    internal class Program
    {
        static DebouncedAction action;
        static void Main(string[] args)
        {
            action = new DebouncedAction(MyMethod)
            {
                DebounceInterval = TimeSpan.FromSeconds(1),
                MaximumDelay = TimeSpan.FromSeconds(2)
            };

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("Calling...");
                action.Debounce();
                Thread.Sleep(500);
            }

            Thread.Sleep(1000);
        }

        static void MyMethod()
        {
            Console.WriteLine("Called!");
        }
    }
}