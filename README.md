# LockedTask.NET

[![Nuget](https://img.shields.io/nuget/v/LockedTask.NET)](https://www.nuget.org/packages/LockedTask.NET)

# Usage

Create a new `LockedTask`

```cs
private static LockedTask NewLockedTask = new LockedTask();
```

Run the `Task`
```cs
NewLockedTask.RunAsync(YourTaskMethod()).ConfigureAwait(false);
```

Its that Simple.

### Optional

Initialize a `Semiphore` with different parameters (Before you start using RunAsync)

```cs
NewLockedTask.NewSemiphore(1,1);
```

Initialize a new `Semiphore` by providing one (Before you start using RunAsync)
```cs
NewLockedTask.NewSemiphore(semiphore);
```
