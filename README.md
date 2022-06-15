# LockedTask.NET

[![example](https://user-images.githubusercontent.com/54571583/173836656-66c6b9d4-cd35-4862-9ecd-d2bc8b645d5e.png)](https://github.com/HypsyNZ/BinanceTrader.NET)

```cs
using System.Threading.Tasks.LockedTask;
```

[![Wiki](https://user-images.githubusercontent.com/54571583/173321360-737e4e55-0e46-40aa-ac4e-0ac01875ce96.png)](https://github.com/HypsyNZ/LockedTask.NET/wiki) [![Nuget](https://img.shields.io/nuget/v/LockedTask.NET)](https://www.nuget.org/packages/LockedTask.NET)

# Usage

Create a new `LockedTask`

```cs
private static LockedTask NewLockedTask = new LockedTask();
```

Run the `Task`
```cs
NewLockedTask.RunAsync(YourTaskMethod);
```

By default the `Awaiter` for your `TaskMethod()` will be set to `false` but you can change it if you want
```cs
NewLockedTask.RunAsync(YourTaskMethod, true);
```

By default the `TimeOut` for the attempt is set to `0` and will enter the lock or return instantly, but you can change it.
```cs
NewLockedTask.RunAsync(YourTaskMethod, 20);
```


# Single Caller

When a single `TaskMethod()` calls `RunAsync()` only one (by default) `TaskMethod()` will be allowed to run concurrently no matter how many times you call `RunAsync()`

```cs
   while (true)
   {
        NewLockedTask.RunAsync(YourTaskMethod);
   }
```


# Multiple Callers

Multiple `TaskMethods()` can attempt to enter a single `LockedTask` and only one (by default) will be allowed to `RunAsync()` at a time, There is no guarantee what order they will `RunAsync()` in, First caller into the lock wins.

```cs
   while (true)
   {
        NewLockedTask.RunAsync(YourTaskMethod1);
        NewLockedTask.RunAsync(YourTaskMethod2, true);
   }
```

# Optional

Initialize a `Semiphore` with different parameters (Before you start using `RunAsync()`)

```cs
NewLockedTask.NewSemiphore(1,1);
```

Initialize a new `Semiphore` by providing one (Before you start using `RunAsync()`)
```cs
NewLockedTask.NewSemiphore(semiphore);
```