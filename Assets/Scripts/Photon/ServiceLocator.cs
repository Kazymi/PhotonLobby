﻿using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Initialize()
    {
        _services = new Dictionary<Type, object>();
    }
    public static void Subscribe<T>(object service)
    {
        _services.Add(typeof(T), service);
    }

    public static void Unsubscribe<T>()
    {
        _services.Remove(typeof(T));
    }

    public static T GetService<T>()
    {
        try
        {
            return (T) _services[typeof(T)];
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}