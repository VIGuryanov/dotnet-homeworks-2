using System;
using System.Threading;

namespace Hw3.Tests;

public class SingleInitializationSingleton
{
    private static readonly object Locker = new();

    private static volatile bool _isInitialized = false;

    public const int DefaultDelay = 3_000;

    public int Delay { get; private set; }

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;
        // imitation of complex initialization logic
        Thread.Sleep(delay);
    }

    internal static void Reset() => ChangeInitializationCondition(true);

    public static void Initialize(int delay) => ChangeInitializationCondition(false, delay, true);

    static void ChangeInitializationCondition(bool isInitializedExpected, int singletonDelay = DefaultDelay, bool doubleCallProhibited = false, string throwDoubleInitializeMessage = "Double initialization!")
    {
        InitializationConditionCheck(isInitializedExpected, () =>
        {
            lock (Locker)
            {
                InitializationConditionCheck(isInitializedExpected, () =>
                {
                    _isInitialized = !isInitializedExpected;
                    instance = CreateLazyDefaults(singletonDelay);
                }, doubleCallProhibited, throwDoubleInitializeMessage);
            }
        }, doubleCallProhibited, throwDoubleInitializeMessage);
    }

    static void InitializationConditionCheck(bool isInitializedExpected, Action checkPassed, bool doubleCallProhibited, string throwDoubleInitializeMessage)
    {
        if (_isInitialized == isInitializedExpected)
            checkPassed();
        else if (doubleCallProhibited)
            throw ThrowDoubleInitialize(throwDoubleInitializeMessage);
    }

    static Exception ThrowDoubleInitialize(string? message = null) => new InvalidOperationException(message);

    static Lazy<SingleInitializationSingleton> CreateLazyDefaults(int delay = DefaultDelay) => new(() => new SingleInitializationSingleton(delay));

    private static Lazy<SingleInitializationSingleton> instance = CreateLazyDefaults();

    public static SingleInitializationSingleton Instance => instance.Value;
}