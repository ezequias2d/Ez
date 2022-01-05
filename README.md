# Ez
[![NuGet Version](https://img.shields.io/nuget/v/Ez)](https://nuget.org/packages/Ez)

This project contains a set of APIs used to implement other Ez.* libraries whose goals are to try to provide a varied and working API for things that may or may not be objectionable.

Sometimes you can find more experimental things like DiscontiguousList, which hasn't been tested as meticulously and may contain a lot of bugs.

Below is some detail, even if summarized, of some things in this repository.

## Ez namespace
This is the base namespace of all Ez APIs.

### Disposable 
Abstract class that provides an implementation of IDisposable interface for classes that need to discard unmanaged and managed resources.

### IResettable
Interface to be used commonly between objects that can be reset and set.

The intended use of the methods is **Set** being a method that prepares the object for use and **Reset** that causes the object itself to be defined or collected by the GC.

### IClone
Interface that provides the possibility to clone the object itself to a specific type using a property.

### Messager namespace (experimental)
This API provides dynamic messages between objects using reflection.

#### DynamicMessengerRecipient
A class that parses methods that support preconfigured events and can be called dynamically.

#### MessengerSender
A static class for autogenerate DynamicMessengerRecipient and "SendMessenger".



### Memory namespace
This API contains some classes, interfaces and structures that are somehow memory related.

#### MemUtil
Static class with useful methods for memory manipulation, like SizeOf, Equals, Set, Copy, Alloc and Free.

##### SizeOf
```csharp
public static uint SizeOf<T>() where T : unmanaged;

public static long SizeOf<T>(ReadOnlySpan<T> span) where T : unmanaged;
```

##### Equals
```csharp
public static bool Equals<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b) where T: unmanaged;

public static bool Equals<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b) where T: unmanaged;

public static unsafe bool Equals(void* a, void* b, long byteCount);
```

##### Set
```csharp
public static void Set<T>(Span<T> span, byte value) where T : unmanaged;

public static unsafe void Set(void* memoryPtr, byte value, long byteCount);

public static void Set(IntPtr memoryPtr, byte value, long byteCount);

public static unsafe void Set<T>(IntPtr ptr, in T value, long count) where T : unmanaged;
```

##### Copy
```csharp
public static long Copy<T>(Span<T> destination, ReadOnlySpan<T> source) where T : unmanaged;

public static long Copy<T>(Span<T> destination, IntPtr source) where T : unmanaged;

public static long Copy<TDestination, TSource>(Span<TDestination> destination, ReadOnlySpan<TSource> source) 
            where TDestination : unmanaged 
            where TSource : unmanaged;

public static unsafe long Copy<T>(IntPtr dst, ReadOnlySpan<T> src) where T : unmanaged;

public static unsafe long Copy<T>(void* dst, ReadOnlySpan<T> src) where T : unmanaged;

public static long Copy<T>(IntPtr dst, in T src) where T : unmanaged;

public static unsafe void Copy(IntPtr destination, IntPtr source, long byteCount);

public static unsafe void Copy(void* destination, void* source, long byteCount);
```

##### Alloc 
```csharp
public static unsafe IntPtr Alloc(long size);
```

##### Free
```csharp
public static unsafe void Free(IntPtr ptr);
```


#### MemoryBlock, MemoryBlockPool and EphemeralMemoryBlock
A MemoryBlock is literally a class to allocate a block of memory for any purpose.

Its closest relative, EphemeralMemoryBlock, is a wrapper over a MemoryBlock that has the limitation imposed by the ref structure of being placed on the stack that guarantees that the instance has not been maintained by any method that takes it as an argument.

And finaly, MemoryBlockPool, a pool for MemoryBlock, there's not much to explain, you Get when you need it and Return it when you don't use it.

##### IMemoryBlock
So both MemoryBlock and EphemeralMemoryBlock implement the IMemoryBlock interface which provides a simple way to suballocate memory blocks, IDisposable to free memory and IResettable to reset suballocations.

```csharp
interface IMemoryBlock : IDisposable, IResettable
{
    long RemainingSize { get; }

    long TotalSize { get; }

    long TotalUsed { get; }

    IntPtr Ptr { get; }

    bool TryAlloc(long size, out IntPtr ptr);

    IntPtr AllocIntPtr(long size);

    PinnedMemory<T> AllocPinnedMemory<T>(int length) where T : unmanaged;

    bool TryAllocPinnedMemory<T>(int length, out PinnedMemory<T> memory) where T : unmanaged;
}
```


### Threading namespace
This API contains two thread-related classes.

#### SingleTaskScheduler
This class implements the abstract class TaskScheduler(from System.Threading.Tasks) to run tasks in a single thread.

#### ThreadMethodExecutor 
This class encapsulates a SingleTaskScheduler and a TaskFactory in an interface for creating and executing tasks.


## Thanks

If you've read this far, I don't know what to say other than have a nice day and I'm excited to know if anything here was helpful.