﻿using System;
using System.Reflection;

namespace WPF.MVVM.WeakEvent;

internal struct Subscription
{
	public Subscription(in WeakReference? subscriber, in MethodInfo handler)
	{
		Subscriber = subscriber;
		Handler = handler ?? throw new ArgumentNullException(nameof(handler));
	}

	public WeakReference? Subscriber { get; }

	public MethodInfo Handler { get; }
}