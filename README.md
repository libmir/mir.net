# mir.net

.NET Implementation of Mir Ref-Counted Type System (MTS)

### Features

 - Fast generic types and handles that are easy to construct, use, and pass between managed and unmanaged code.
 - Faster then Protocol Buffers as well as any other serialization library because it is complitely zero-copy.
 - Requires at least twice less user code comparing with Protocol Buffers.
 - D and C++ implementations are provided via [Mir Algorithm](https://github.com/libmir/mir-algorithm/)
 - D, C++, and C# MTS implementations are self-contained. C# implementation requires neither Mir Algorithm nor D/C/C++ runtimes.
 - Hands-free. Just construct, pass, and forget. Mir objects hold all required information to destroy them and free memory.
 
The library is used in a large private codebase.

## [Install with NuGet](https://www.nuget.org/packages/Mir)

## Basic Types

 - Array
 - Array slices
 - Matrices
 - Sorted dictionaries (`Series`)
 - Slim shared pointers
 - Shared Pointers with inheritance
 - POD small strings

## Composed user-defined types

Mir types can be composed using other Mir types and C# POD types that don't require special marshaling. `MirWrapper` is a base class for all non-POD library and user-defined Mir types. It requires the structure payload (`Impl`) to be defined.

`MirPtr` and `MirSlimPtr` can be used to wrap a native type without defining its structure payload in C#.

## Table of correspondence

(check the source repository if the table isn't rendered correctly because of the [nuget issue](https://github.com/NuGet/NuGetGallery/issues/7035))

| D Type | C# Type | C++ Type |
|---|----|-----|
| [SlimRCPtr](http://mir-algorithm.libmir.org/mir_rc_slim_ptr.html)!Type | `MirSlimPtr<Type>` |  `mir_slim_rcptr<Type>` |
| [RCPtr](http://mir-algorithm.libmir.org/mir_rc_ptr.html)!Type | `MirPtr<Type>` |  `mir_rcptr<Type>` |
| [RCArray](http://mir-algorithm.libmir.org/mir_rc_array.html)!Type | `MirArray<Type, @>` ×2 |  `mir_rcarray<Type>` |
| [Slice](http://mir-algorithm.libmir.org/mir_ndslice_slice.html)!([RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Type) | `Slice<Type>` ×2 |  `mir_slice<mir_rci<Type>>` |
| [Slice](http://mir-algorithm.libmir.org/mir_ndslice_slice.html)!([RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Type, 2) | `Matrix<Type>` ×2 |  `mir_slice<mir_rci<Type>, 2>` |
| [Slice](http://mir-algorithm.libmir.org/mir_ndslice_slice.html)!(Type*) | `SliceView<Type, @>` ×2 |  `mir_slice<Type*>` |
| [Series](http://mir-algorithm.libmir.org/mir_series.html)!([RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Key, [RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Value) | `Series<Key, Value, @>` ×2 |  `mir_series<mir_rci<Key>, mir_rci<Value>>` |
| [SmallString](http://mir-algorithm.libmir.org/mir_small_string.html)!N | `SmallStringN`, N=4,31,32,64,128 |  `mir::SmallString<N>` |
| [Series](http://mir-algorithm.libmir.org/mir_series.html)!([RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!([RCArray](http://mir-algorithm.libmir.org/mir_rc_array.html)!(const char)), [RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Value) | `StringSeries<Value>` |  `mir_series<mir_rci<mir_rcarray<const char>>, mir_rci<Value>>` |

 `Name<... , @>` ×2 - means a type has two declarations, `Name<... >` and `Name<... , Impl>`, where `Impl`
 is an unmanaged C# handle structure that describes non-POD Mir Type.
 
Composed Mir Type (CMT) is a type that is composed of CMT fields, first order Mir RefCounted fields, and POD(unmanaged) fields.

Unmanaged C# handles should use `byte` instead of `bool`.
