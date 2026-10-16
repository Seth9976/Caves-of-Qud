using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000C3 RID: 195
	[StaticAccessor("UnsafeUtility", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Unsafe/UnsafeUtility.bindings.h")]
	public static class UnsafeUtility
	{
		// Token: 0x060003E0 RID: 992
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFieldOffsetInStruct(FieldInfo field);

		// Token: 0x060003E1 RID: 993
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFieldOffsetInClass(FieldInfo field);

		// Token: 0x060003E2 RID: 994 RVA: 0x00007E38 File Offset: 0x00006038
		public static int GetFieldOffset(FieldInfo field)
		{
			bool isValueType = field.DeclaringType.IsValueType;
			int num;
			if (isValueType)
			{
				num = UnsafeUtility.GetFieldOffsetInStruct(field);
			}
			else
			{
				bool isClass = field.DeclaringType.IsClass;
				if (isClass)
				{
					num = UnsafeUtility.GetFieldOffsetInClass(field);
				}
				else
				{
					num = -1;
				}
			}
			return num;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00007E7C File Offset: 0x0000607C
		public unsafe static void* PinGCObjectAndGetAddress(object target, out ulong gcHandle)
		{
			return UnsafeUtility.PinSystemObjectAndGetAddress(target, out gcHandle);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00007E98 File Offset: 0x00006098
		public unsafe static void* PinGCArrayAndGetDataAddress(Array target, out ulong gcHandle)
		{
			return UnsafeUtility.PinSystemArrayAndGetAddress(target, out gcHandle);
		}

		// Token: 0x060003E5 RID: 997
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* PinSystemArrayAndGetAddress(object target, out ulong gcHandle);

		// Token: 0x060003E6 RID: 998
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* PinSystemObjectAndGetAddress(object target, out ulong gcHandle);

		// Token: 0x060003E7 RID: 999
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ReleaseGCObject(ulong gcHandle);

		// Token: 0x060003E8 RID: 1000
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void CopyObjectAddressToPtr(object target, void* dstPtr);

		// Token: 0x060003E9 RID: 1001 RVA: 0x00007EB4 File Offset: 0x000060B4
		public static bool IsBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable(typeof(T));
		}

		// Token: 0x060003EA RID: 1002
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CheckForLeaks();

		// Token: 0x060003EB RID: 1003
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ForgiveLeaks();

		// Token: 0x060003EC RID: 1004
		[ThreadSafe(ThrowsException = false)]
		[BurstAuthorizedExternalMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern NativeLeakDetectionMode GetLeakDetectionMode();

		// Token: 0x060003ED RID: 1005
		[ThreadSafe(ThrowsException = false)]
		[BurstAuthorizedExternalMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLeakDetectionMode(NativeLeakDetectionMode value);

		// Token: 0x060003EE RID: 1006
		[VisibleToOtherModules(new string[] { "UnityEngine.AIModule" })]
		[BurstAuthorizedExternalMethod]
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LeakRecord(IntPtr handle, LeakCategory category, int callstacksToSkip);

		// Token: 0x060003EF RID: 1007
		[ThreadSafe(ThrowsException = false)]
		[VisibleToOtherModules(new string[] { "UnityEngine.AIModule" })]
		[BurstAuthorizedExternalMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LeakErase(IntPtr handle, LeakCategory category);

		// Token: 0x060003F0 RID: 1008
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* MallocTracked(long size, int alignment, Allocator allocator, int callstacksToSkip);

		// Token: 0x060003F1 RID: 1009
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void FreeTracked(void* memory, Allocator allocator);

		// Token: 0x060003F2 RID: 1010
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* Malloc(long size, int alignment, Allocator allocator);

		// Token: 0x060003F3 RID: 1011
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Free(void* memory, Allocator allocator);

		// Token: 0x060003F4 RID: 1012 RVA: 0x00007ED8 File Offset: 0x000060D8
		public static bool IsValidAllocator(Allocator allocator)
		{
			return allocator > Allocator.None;
		}

		// Token: 0x060003F5 RID: 1013
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpy(void* destination, void* source, long size);

		// Token: 0x060003F6 RID: 1014
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpyReplicate(void* destination, void* source, int size, int count);

		// Token: 0x060003F7 RID: 1015
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpyStride(void* destination, int destinationStride, void* source, int sourceStride, int elementSize, int count);

		// Token: 0x060003F8 RID: 1016
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemMove(void* destination, void* source, long size);

		// Token: 0x060003F9 RID: 1017
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemSwap(void* ptr1, void* ptr2, long size);

		// Token: 0x060003FA RID: 1018
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemSet(void* destination, byte value, long size);

		// Token: 0x060003FB RID: 1019 RVA: 0x00007EEE File Offset: 0x000060EE
		public unsafe static void MemClear(void* destination, long size)
		{
			UnsafeUtility.MemSet(destination, 0, size);
		}

		// Token: 0x060003FC RID: 1020
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int MemCmp(void* ptr1, void* ptr2, long size);

		// Token: 0x060003FD RID: 1021
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int SizeOf(Type type);

		// Token: 0x060003FE RID: 1022
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBlittable(Type type);

		// Token: 0x060003FF RID: 1023
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsUnmanaged(Type type);

		// Token: 0x06000400 RID: 1024
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsValidNativeContainerElementType(Type type);

		// Token: 0x06000401 RID: 1025
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetScriptingTypeFlags(Type type);

		// Token: 0x06000402 RID: 1026 RVA: 0x00007EFC File Offset: 0x000060FC
		[ThreadSafe]
		internal unsafe static void LogError(string msg, string filename, int linenumber)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(msg, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = msg.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filename, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = filename.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				UnsafeUtility.LogError_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, linenumber);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00007F84 File Offset: 0x00006184
		private static bool IsBlittableValueType(Type t)
		{
			return t.IsValueType && UnsafeUtility.IsBlittable(t);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00007FA8 File Offset: 0x000061A8
		private static string GetReasonForTypeNonBlittableImpl(Type t, string name)
		{
			bool flag = !t.IsValueType;
			string text;
			if (flag)
			{
				text = string.Format("{0} is not blittable because it is not of value type ({1})\n", name, t);
			}
			else
			{
				bool isPrimitive = t.IsPrimitive;
				if (isPrimitive)
				{
					text = string.Format("{0} is not blittable ({1})\n", name, t);
				}
				else
				{
					string text2 = "";
					foreach (FieldInfo fieldInfo in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
					{
						bool flag2 = !UnsafeUtility.IsBlittableValueType(fieldInfo.FieldType);
						if (flag2)
						{
							text2 += UnsafeUtility.GetReasonForTypeNonBlittableImpl(fieldInfo.FieldType, string.Format("{0}.{1}", name, fieldInfo.Name));
						}
					}
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000805C File Offset: 0x0000625C
		internal static bool IsArrayBlittable(Array arr)
		{
			return UnsafeUtility.IsBlittableValueType(arr.GetType().GetElementType());
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00008080 File Offset: 0x00006280
		internal static bool IsGenericListBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable<T>();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00008098 File Offset: 0x00006298
		internal static string GetReasonForArrayNonBlittable(Array arr)
		{
			Type elementType = arr.GetType().GetElementType();
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(elementType, elementType.Name);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000080C4 File Offset: 0x000062C4
		internal static string GetReasonForGenericListNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000080F0 File Offset: 0x000062F0
		internal static string GetReasonForTypeNonBlittable(Type t)
		{
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(t, t.Name);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00008110 File Offset: 0x00006310
		internal static string GetReasonForValueTypeNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000813C File Offset: 0x0000633C
		public static bool IsUnmanaged<T>()
		{
			return (UnsafeUtility.TypeFlagsCache<T>.flags & 1) == 0;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00008158 File Offset: 0x00006358
		public static bool IsNativeContainerType<T>()
		{
			return (UnsafeUtility.TypeFlagsCache<T>.flags & 2) != 0;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00008174 File Offset: 0x00006374
		public static bool IsValidNativeContainerElementType<T>()
		{
			return UnsafeUtility.TypeFlagsCache<T>.flags == 0;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00008190 File Offset: 0x00006390
		public static int AlignOf<T>() where T : struct
		{
			return UnsafeUtility.SizeOf<UnsafeUtility.AlignOfHelper<T>>() - UnsafeUtility.SizeOf<T>();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000081B0 File Offset: 0x000063B0
		[VisibleToOtherModules(new string[] { "UnityEngine.ImageConversionModule" })]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static Span<byte> GetByteSpanFromArray(Array array, int elementSize)
		{
			bool flag = array == null || array.Length == 0;
			Span<byte> span;
			if (flag)
			{
				span = default(Span<byte>);
			}
			else
			{
				byte[] array2 = *UnsafeUtility.As<Array, byte[]>(ref array);
				span = new Span<byte>(UnsafeUtility.AddressOf<byte>(ref array2[0]), array.Length * elementSize);
			}
			return span;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00008204 File Offset: 0x00006404
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Span<byte> GetByteSpanFromList<T>(List<T> list) where T : struct
		{
			return MemoryMarshal.AsBytes<T>(NoAllocHelpers.ExtractArrayFromList<T>(list).AsSpan<T>());
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00008226 File Offset: 0x00006426
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			UnsafeUtility.InternalCopyPtrToStructure<T>(ptr, out output);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00008231 File Offset: 0x00006431
		private unsafe static void InternalCopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			output = *(T*)ptr;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000823F File Offset: 0x0000643F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			UnsafeUtility.InternalCopyStructureToPtr<T>(ref input, ptr);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00008231 File Offset: 0x00006431
		private unsafe static void InternalCopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			*(T*)ptr = input;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000824A File Offset: 0x0000644A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static T ReadArrayElement<T>(void* source, int index)
		{
			return *(T*)((byte*)source + (long)index * (long)sizeof(T));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000825E File Offset: 0x0000645E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static T ReadArrayElementWithStride<T>(void* source, int index, int stride)
		{
			return *(T*)((byte*)source + (long)index * (long)stride);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000826D File Offset: 0x0000646D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteArrayElement<T>(void* destination, int index, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)sizeof(T)) = value;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00008282 File Offset: 0x00006482
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteArrayElementWithStride<T>(void* destination, int index, int stride, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)stride) = value;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00008292 File Offset: 0x00006492
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AddressOf<T>(ref T output) where T : struct
		{
			return (void*)(&output);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00008295 File Offset: 0x00006495
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SizeOf<T>() where T : struct
		{
			return sizeof(T);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00008292 File Offset: 0x00006492
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T As<U, T>(ref U from)
		{
			return ref from;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00008292 File Offset: 0x00006492
		internal static T As<T>(object from) where T : class
		{
			return from;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00008292 File Offset: 0x00006492
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ref T AsRef<T>(void* ptr) where T : struct
		{
			return ref *(T*)ptr;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00008292 File Offset: 0x00006492
		internal unsafe static ref T ClassAsRef<T>(void* ptr) where T : class
		{
			return ref *(T*)ptr;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000829D File Offset: 0x0000649D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ref T ArrayElementAsRef<T>(void* ptr, int index) where T : struct
		{
			return ref *(T*)((byte*)ptr + (long)index * (long)sizeof(T));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000082AC File Offset: 0x000064AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int EnumToInt<T>(T enumValue) where T : struct, IConvertible
		{
			int num = 0;
			UnsafeUtility.InternalEnumToInt<T>(ref enumValue, ref num);
			return num;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000082CB File Offset: 0x000064CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void InternalEnumToInt<T>(ref T enumValue, ref int intValue)
		{
			intValue = enumValue;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000082D1 File Offset: 0x000064D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EnumEquals<T>(T lhs, T rhs) where T : struct, IConvertible
		{
			return lhs == rhs;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000082D9 File Offset: 0x000064D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T Add<[IsUnmanaged] T>(ref T source, int elementOffset) where T : struct, ValueType
		{
			return (ref source) + sizeof(T) * elementOffset;
		}

		// Token: 0x06000424 RID: 1060
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogError_Injected(ref ManagedSpanWrapper msg, ref ManagedSpanWrapper filename, int linenumber);

		// Token: 0x0400028C RID: 652
		private const int kIsManaged = 1;

		// Token: 0x0400028D RID: 653
		private const int kIsNativeContainer = 2;

		// Token: 0x020000C4 RID: 196
		internal struct TypeFlagsCache<T>
		{
			// Token: 0x06000425 RID: 1061 RVA: 0x000082E5 File Offset: 0x000064E5
			static TypeFlagsCache()
			{
				UnsafeUtility.TypeFlagsCache<T>.Init(ref UnsafeUtility.TypeFlagsCache<T>.flags);
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x000082F3 File Offset: 0x000064F3
			[BurstDiscard]
			private static void Init(ref int flags)
			{
				flags = UnsafeUtility.GetScriptingTypeFlags(typeof(T));
			}

			// Token: 0x0400028E RID: 654
			internal static readonly int flags;
		}

		// Token: 0x020000C5 RID: 197
		private struct AlignOfHelper<T> where T : struct
		{
			// Token: 0x0400028F RID: 655
			public byte dummy;

			// Token: 0x04000290 RID: 656
			public T data;
		}
	}
}
