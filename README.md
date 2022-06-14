# LockedTask.NET

[![Nuget](https://img.shields.io/nuget/v/LockedTask.NET)](https://www.nuget.org/packages/LockedTask.NET)

```cs
using System.Threading.Tasks.LockedTask;
```

# Usage

Create a new `LockedTask`

```cs
private static LockedTask NewLockedTask = new LockedTask();
```

Run the `Task`
```cs
NewLockedTask.RunAsync(YourTaskMethod()).ConfigureAwait(false);
```

By default the awaiter for your `TaskMethod()` will be set to `false` but you can change it if you want
```cs
NewLockedTask.RunAsync(YourTaskMethod(), true).ConfigureAwait(false);
```

Its that Simple.

[[![example](https://user-images.githubusercontent.com/54571583/173622705-ba160786-c0f2-49c7-af9c-2bd4d19467f0.png)]](https://github.com/HypsyNZ/BinanceTrader.NET)

### Optional

Initialize a `Semiphore` with different parameters (Before you start using RunAsync)

```cs
NewLockedTask.NewSemiphore(1,1);
```

Initialize a new `Semiphore` by providing one (Before you start using RunAsync)
```cs
NewLockedTask.NewSemiphore(semiphore);
```
