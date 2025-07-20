using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasics;

/// <summary>
/// Demonstrates the concept and usage of events in C#.
/// </summary>
/// <remarks>
/// Events are a special kind of multicast delegate used to provide notifications.
/// They are commonly used in scenarios such as UI programming, observer patterns, and custom notifications.
/// </remarks>
public class EventsDemo
{
    /// <summary>
    /// Declares a custom delegate for event handling.
    /// </summary>
    public delegate void NotifyEventHandler(string message);

    /// <summary>
    /// Declares an event using the custom delegate.
    /// Subscribers can attach methods to this event to receive notifications.
    /// </summary>
    public event NotifyEventHandler? Notify;

    /// <summary>
    /// Declares an event using the built-in EventHandler delegate.
    /// </summary>
    public event EventHandler<EventArgs>? StandardNotify;

    /// <summary>
    /// Raises the Notify event.
    /// </summary>
    /// <param name="message">The message to send to subscribers.</param>
    public void RaiseNotify(string message)
    {
        // Check for null before invoking the event to avoid NullReferenceException.
        Notify?.Invoke(message);
    }

    /// <summary>
    /// Raises the StandardNotify event.
    /// </summary>
    public void RaiseStandardNotify()
    {
        // Check for null before invoking the event to avoid NullReferenceException i.e. no subscribers presence.
        StandardNotify?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Demonstrates event subscription and invocation.
    /// </summary>
    public static void EventsDemoUsage()
    {
        var demo = new EventsDemo();

        // Subscribe to the custom event using a named method.
        demo.Notify += OnNotifyReceived;

        // Subscribe using a lambda expression.
        demo.Notify += msg => Console.WriteLine($"Lambda received: {msg}");

        // Subscribe to the standard event.
        demo.StandardNotify += (sender, args) =>
        {
            Console.WriteLine("StandardNotify event triggered.");
        };

        // Raise the events.
        demo.RaiseNotify("Hello from custom event!");
        demo.RaiseStandardNotify();
    }

    /// <summary>
    /// Example event handler method.
    /// </summary>
    private static void OnNotifyReceived(string message)
    {
        Console.WriteLine($"Named handler received: {message}");
    }
}
