# mir.net

.NET Implementation of Mir Ref-Counted Type System

Mir Type System (MTS) provides fast generic types and handles that are easy to construct, use and pass between managed and unmanaged code.

MTS is faster then protobuffer as well as any other serialization librariry because it is 100% zero copy. MTS requires at lest twice less boilerplate code comparing with Protobuffer.

## Basic Types

 - Array
 - Array slices
 - Matrices
 - Sorted dictionaries (`Series`)
 - Slim shared pointers
 - Shared Pointers with inheritance
 - POD small strings

Mir types can be composed using other Mir types and C# POD types that doesn't require special marshalling.

The library is used in a large private codebase.

MTS for D and C++ can is provided via [Mir Algorithm](https://github.com/libmir/mir-algorithm/).

## Table of correspondence

| D Type | C# Type | C++ Type |
|---|----|-----|
| [SlimRCPtr](http://mir-algorithm.libmir.org/mir_rc_slim_ptr.html)!Type | `MirSlimPtr<Type>` |  `mir_slim_rcptr<Type>` |
| [RCPtr](http://mir-algorithm.libmir.org/mir_rc_ptr.html)!Type | `MirPtr<Type>` |  `mir_rcptr<Type>` |
| [RCArray](http://mir-algorithm.libmir.org/mir_rc_array.html)!Type | `MirArray<Type, @>` ×2 |  `mir_rcarray<Type>` |
| [Slice](http://mir-algorithm.libmir.org/mir_ndslice_slice.html)!([RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Type) | `Slice<Type>` ×2 |  `mir_slice<mir_rci<Type>>` |
| [Slice](http://mir-algorithm.libmir.org/mir_ndslice_slice.html)!([RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Type, 2) | `Matrix<Type>` ×2 |  `mir_slice<mir_rci<Type>, 2>` |
| [Slice](http://mir-algorithm.libmir.org/mir_ndslice_slice.html)!(Type*) | `SliceView<Type, @>` ×2 |  `mir_slice<Type*>` |
| [Series](http://mir-algorithm.libmir.org/mir_ndslice_slice.html)!([RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Key, [RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Value) | `Series<Key, Value, @>` ×2 |  `mir_series<mir_rci<Key>, mir_rci<Value>>` |
| [SmallString](http://mir-algorithm.libmir.org/mir_small_string.html)!N | `SmallStringN`, N=4,31,32,64,128 |  `mir::SmallString<N>` |
| [Series](http://mir-algorithm.libmir.org/mir_ndslice_slice.html)!([RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!([RCArray](http://mir-algorithm.libmir.org/mir_rc_array.html)!(const char)), [RCI](http://mir-algorithm.libmir.org/mir_rc_array.html#.mir_rci)!Value) | `StringSeries<Value>` |  `mir_series<mir_rci<mir_rcarray<const char>>, mir_rci<Value>>` |

 `Name<... , @>` ×2 - means a type has two instations, `Name<... >` and `Name<... , Impl>`, where `Impl`
 is an unmanaged C# handle structure that describes non-POD Composed Mir Type.
 
Composed Mir Type (CMT) is a type that is composed of CMT fields, first order Mir RefCounted fields, and POD(unmanaged) fields.

Unmanaged C# handles should use `byte` instead of `bool`.
