using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WPF.MVVM.WeakEvent;

public partial class WeakEventManager
{
    private readonly Dictionary<string, List<Subscription>> _eventHandlers = new();

    public void AddEventHandler(Delegate handler, [CallerMemberName] string eventName = null!)
    {
        AddEventHandler(in handler, in eventName);
    }

    public void AddEventHandler(in Delegate handler, [CallerMemberName] in string eventName = null!)
    {
        if (string.IsNullOrWhiteSpace(eventName))
        {
            throw new ArgumentNullException(nameof(eventName));
        }

        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        EventManagerService.AddEventHandler(eventName, handler.Target, handler.GetMethodInfo(), _eventHandlers);
    }

    public void RemoveEventHandler(Delegate handler, [CallerMemberName] string eventName = null!)
    {
        RemoveEventHandler(in handler, in eventName);
    }

    public void RemoveEventHandler(in Delegate handler, [CallerMemberName] in string eventName = null!)
    {
        if (string.IsNullOrWhiteSpace(eventName))
        {
            throw new ArgumentNullException(nameof(eventName));
        }

        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        EventManagerService.RemoveEventHandler(eventName, handler.Target, handler.GetMethodInfo(), _eventHandlers);
    }

    public void RaiseEvent(object? sender, object? eventArgs, string eventName)
    {
        RaiseEvent(in sender, in eventArgs, in eventName);
    }

    public void RaiseEvent(in object? sender, in object? eventArgs, in string eventName)
    {
        EventManagerService.HandleEvent(eventName, sender, eventArgs, _eventHandlers);
    }

    public void RaiseEvent(string eventName)
    {
        RaiseEvent(in eventName);
    }

    public void RaiseEvent(in string eventName)
    {
        EventManagerService.HandleEvent(eventName, _eventHandlers);
    }
}

public partial class WeakEventManager<TEventArgs>
{
    private readonly Dictionary<string, List<Subscription>> _eventHandlers = new();

    public void AddEventHandler(EventHandler<TEventArgs> handler, [CallerMemberName] string eventName = null!)
    {
        AddEventHandler(in handler, in eventName);
    }

    public void AddEventHandler(in EventHandler<TEventArgs> handler, [CallerMemberName] in string eventName = null!)
    {
        if (string.IsNullOrWhiteSpace(eventName))
        {
            throw new ArgumentNullException(nameof(eventName));
        }

        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        EventManagerService.AddEventHandler(eventName, handler.Target, handler.GetMethodInfo(), _eventHandlers);
    }

    public void AddEventHandler(Action<TEventArgs> action, [CallerMemberName] string eventName = null!)
    {
        AddEventHandler(in action, in eventName);
    }

    public void AddEventHandler(in Action<TEventArgs> action, [CallerMemberName] in string eventName = null!)
    {
        if (string.IsNullOrWhiteSpace(eventName))
        {
            throw new ArgumentNullException(nameof(eventName));
        }

        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        EventManagerService.AddEventHandler(eventName, action.Target, action.GetMethodInfo(), _eventHandlers);
    }

    public void RemoveEventHandler(EventHandler<TEventArgs> handler, [CallerMemberName] string eventName = null!)
    {
        RemoveEventHandler(in handler, in eventName);
    }

    public void RemoveEventHandler(in EventHandler<TEventArgs> handler, [CallerMemberName] in string eventName = null!)
    {
        if (string.IsNullOrWhiteSpace(eventName))
        {
            throw new ArgumentNullException(nameof(eventName));
        }

        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        EventManagerService.RemoveEventHandler(eventName, handler.Target, handler.GetMethodInfo(), _eventHandlers);
    }

    public void RemoveEventHandler(Action<TEventArgs> action, [CallerMemberName] string eventName = null!)
    {
        RemoveEventHandler(in action, in eventName);
    }

    public void RemoveEventHandler(in Action<TEventArgs> action, [CallerMemberName] in string eventName = null!)
    {
        if (string.IsNullOrWhiteSpace(eventName))
        {
            throw new ArgumentNullException(nameof(eventName));
        }

        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        EventManagerService.RemoveEventHandler(eventName, action.Target, action.GetMethodInfo(), _eventHandlers);
    }

    public void RaiseEvent(object? sender, TEventArgs eventArgs, string eventName)
    {
        RaiseEvent(in sender, in eventArgs, in eventName);
    }

    public void RaiseEvent(in object? sender, in TEventArgs eventArgs, in string eventName)
    {
        EventManagerService.HandleEvent(eventName, sender, eventArgs, _eventHandlers);
    }

    public void RaiseEvent(TEventArgs eventArgs, string eventName)
    {
        RaiseEvent(in eventArgs, in eventName);
    }

    public void RaiseEvent(in TEventArgs eventArgs, in string eventName)
    {
        EventManagerService.HandleEvent(eventName, eventArgs, _eventHandlers);
    }
}