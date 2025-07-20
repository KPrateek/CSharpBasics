// See https://aka.ms/new-console-template for more information
using System;

namespace CSharpBasics;

public class Delegates
{
    /// <summary>
    /// Represents a custom delegate that takes two integers and returns an integer.
    /// </summary>
    /// <remarks>
    /// Purpose: Demonstrates how to declare and use a custom delegate type for method references.
    /// </remarks>
    private delegate int MathOperation(int x, int y);
    private delegate void LogOperation(string message);

    /// <summary>
    /// Adds two integers.
    /// </summary>
    private static int Add(int x, int y)
    {
        return x + y;
    }

    /// <summary>
    /// Logs a message to the console.
    /// </summary>
    private static void LogMessage(string message)
    {
        Console.WriteLine($"Log: {message}");
    }

    /// <summary>
    /// Warns with a message to the console.
    /// </summary>
    private static void WarnMessage(string message)
    {
        Console.WriteLine($"Warning: {message}");
    }

    /// <summary>
    /// Multiplies two integers.
    /// </summary>
    private static int Multiply(int x, int y)
    {
        return x * y;
    }

    /// <summary>
    /// Entry point demonstrating all major types of delegates in C#.
    /// </summary>
    public static void DelgatesDemo()
    {
        // 1. Custom delegate usage
        // Purpose: Shows how to use a user-defined delegate to reference a method.
        MathOperation addOperation = Add;
        Console.WriteLine($"Custom delegate: {addOperation(2, 3)}");

        // Purpose: Demonstrating memory allocation for the delegate
        // Compiler creates a delegate instance that points to the Add method.
        // When compiler sees the delegate type, it creates a class with same name of the delegate.
        // This class has a method, named "Invoke" that matches the signature of the delegate.
        // The delegate instance is allocated on the heap, which allows it to be passed around and invoked later.
        // This is a simplified explanation of how delegates work under the hood.
        MathOperation newAddOperation = new(Add);
        Console.WriteLine($"Custom delegate with memory allocation {newAddOperation.Invoke(2, 3)}");

        // 2. Multicast delegate (Action)
        // Purpose: Demonstrates combining multiple methods into a single delegate invocation list.
        Action<string> messageDelegate = LogMessage;
        messageDelegate += WarnMessage;
        messageDelegate("Multicast delegate example");

        LogOperation logMessageDelegate = LogMessage;
        logMessageDelegate += WarnMessage;
        logMessageDelegate("Log and Warn using LogOperation delegate");

        // Multicast approach using GetInvocationList with LogOperation
        // Purpose: Demonstrates how to access and invoke each method in a multicast delegate's invocation list.
        // GetInvocationList returns an array of delegates that can be invoked individually.
        // This is useful when you want to execute each method in the multicast delegate separately.
        // Thisallows for more control over the invocation process, such as logging or handling exceptions for each method.
        Console.WriteLine("\nMulticast approach using GetInvocationList for LogOperation:");
        foreach (var del in logMessageDelegate.GetInvocationList())
        {
            try
            {
                // Each delegate in the list is of type Delegate, so cast to LogOperation
                ((LogOperation)del)("Invoked individually via GetInvocationList");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error invoking delegate: {ex.Message}");
            }
        }

        // 3. Generic delegates
        // Func<T, TResult>: Purpose: Used for methods that return a value.
        // Func delegeate can take input as well as output parameters. Last parameter will always be the return type.
        // Func delegate can take 0 or more input parameters, but only one return type.
        Func<int, int, int> multiplyOperation = (x, y) => x * y;
        Console.WriteLine($"Func delegate: {multiplyOperation(3, 4)}");

        Func<int, int, int> FuncDelegate = delegate (int x, int y)
        {
            // This is an example of a Func delegate that takes two integers and returns their sum.
            return x + y;
        };


        // Action<T>: Purpose: Used for methods that do not return a value.
        // It should be used when you want to perform an operation without needing a return value.
        // Action delegate can take 0 or more input parameters, but no return type.
        Action<string> greetAction = name => Console.WriteLine($"Hello, {name}!");
        greetAction("World");

        Action<string> logAction = delegate (string message)
        {
            // This is an example of an Action delegate that takes a string and logs it.
            Console.WriteLine($"Action delegate log: {message}");
        };
        logAction("This is a log message using Action delegate");

        // Predicate<T>: Purpose: Used for methods that return a boolean value, typically for filtering.
        // Predicate delegate is special type of Func delegate that can take one input parameter and returns a boolean value and it should not be mentioned in the signature.
        // It is commonly used in LINQ queries and other scenarios where you need to evaluate a condition.
        Predicate<int> isEvenPredicate = number => number % 2 == 0;
        Console.WriteLine($"Predicate delegate: {isEvenPredicate(10)}");

        Predicate<int> isPositivePredicate = delegate (int number)
        {
            // This is an example of a Predicate delegate that checks if a number is positive.
            return number > 0;
        };
        Console.WriteLine($"Predicate delegate for positive check: {isPositivePredicate(5)}");

        // Note: Func, Action, and Predicate are built-in generic delegates in C# that provide a convenient way to work with methods without needing to define custom delegate types.

        // 4. Delegate with lambda expression
        // Purpose: Shows how to use delegates with inline lambda expressions for concise logic.
        MathOperation subtractOperation = (x, y) => x - y;
        Console.WriteLine($"Lambda delegate: {subtractOperation(5, 2)}");

        // 5. Delegate with method group conversion
        // Purpose: Demonstrates assigning a method directly to a delegate using method group syntax.
        MathOperation multiplyMethodOperation = Multiply;
        Console.WriteLine($"Method group delegate: {multiplyMethodOperation(6, 7)}");

        // 6. Anonymous delegate example
        // Purpose: Shows how to use an anonymous method with a delegate using the 'delegate' keyword.
        // Anonymous delegates allow you to define a method inline without needing to create a separate named method.
        // This is useful for quick, one-off operations where a full method definition is not necessary.
        // Annonymous delegates can only be assigned to a delegate type.
        // Anonymous delegates can capture variables from the surrounding scope, similar to lambda expressions except `ref` and `out` variables.
        // Note: Anonymous delegates are less common in modern C# due to the prevalence of lambda expressions.
        MathOperation anonymousOperation = delegate (int x, int y)
        {
            return x / y;
        };
        Console.WriteLine($"Anonymous delegate: {anonymousOperation(10, 2)}");
    }
}