using System;
using System.Threading;

namespace Hw3.Tests;

public class SingleInitializationSingleton
{
    private static readonly object Locker = new();

    private static volatile bool _isInitialized = false;

    public const int DefaultDelay = 3_000;
    
    public int Delay { get; private set;}

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;
        // imitation of complex initialization logic
        Thread.Sleep(delay);           
    }

    internal static void Reset()
    {
        if(_isInitialized)
        {
            lock(Locker)
            {
                if(_isInitialized)
                {
                    _isInitialized = false;
                    instance = new(() => new SingleInitializationSingleton());
                }
            }
        }
    }

    public static void Initialize(int delay)
    {
        if(!_isInitialized)
        {
            lock(Locker)
            {
                if(!_isInitialized)
                {
                    _isInitialized = true;
                    instance = new(() => new SingleInitializationSingleton(delay));
                }
                else
                    throw new InvalidOperationException("Double initialization!");
            }
        }
        else
            throw new InvalidOperationException("Double initialization!");
    }

    private static Lazy<SingleInitializationSingleton> instance = new(() => new SingleInitializationSingleton());

    public static SingleInitializationSingleton Instance
    {
        get
        { 
            lock(Locker)
                return instance.Value; 
        }
    }    
}