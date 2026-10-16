using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace Unity.Collections
{
	// Token: 0x020000A4 RID: 164
	[NativeContainerSupportsMinMaxWriteRestriction]
	[NativeContainerSupportsDeallocateOnJobCompletion]
	[NativeContainerSupportsDeferredConvertListToArray]
	[DebuggerTypeProxy(typeof(NativeArrayDebugView<>))]
	[DebuggerDisplay("Length = {m_Length}")]
	[NativeContainer]
	public struct NativeArray<T> : IDisposable, IEnumerable<T>, IEnumerable, IEquatable<NativeArray<T>> where T : struct
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000668C File Offset: 0x0000488C
		public NativeArray(int length, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			NativeArray<T>.Allocate(length, allocator, out this);
			bool flag = (options & NativeArrayOptions.ClearMemory) == NativeArrayOptions.ClearMemory;
			if (flag)
			{
				UnsafeUtility.MemClear(this.m_Buffer, (long)this.Length * (long)UnsafeUtility.SizeOf<T>());
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000066C7 File Offset: 0x000048C7
		public NativeArray(T[] array, Allocator allocator)
		{
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000066E2 File Offset: 0x000048E2
		public NativeArray(NativeArray<T> array, Allocator allocator)
		{
			NativeArray<T>.Allocate(array.Length, allocator, out this);
			NativeArray<T>.Copy(array, 0, this, 0, array.Length);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000670C File Offset: 0x0000490C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckAllocateArguments(int length, Allocator allocator)
		{
			bool flag = allocator <= Allocator.None;
			if (flag)
			{
				throw new ArgumentException("Allocator must be Temp, TempJob or Persistent", "allocator");
			}
			bool flag2 = allocator >= Allocator.FirstUserIndex;
			if (flag2)
			{
				throw new ArgumentException("Use CollectionHelper.CreateNativeArray in com.unity.collections package for custom allocator", "allocator");
			}
			bool flag3 = length < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000676C File Offset: 0x0000496C
		private static void Allocate(int length, Allocator allocator, out NativeArray<T> array)
		{
			long num = (long)UnsafeUtility.SizeOf<T>() * (long)length;
			array = default(NativeArray<T>);
			array.m_Buffer = UnsafeUtility.MallocTracked(num, UnsafeUtility.AlignOf<T>(), allocator, 0);
			array.m_Length = length;
			array.m_AllocatorLabel = allocator;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000336 RID: 822 RVA: 0x000067AC File Offset: 0x000049AC
		public int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000067C4 File Offset: 0x000049C4
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[BurstDiscard]
		internal static void IsUnmanagedAndThrow()
		{
			bool flag = !UnsafeUtility.IsUnmanaged<T>();
			if (flag)
			{
				throw new InvalidOperationException(string.Format("{0} used in NativeArray<{1}> must be unmanaged (contain no managed types).", typeof(T), typeof(T)));
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckElementReadAccess(int index)
		{
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckElementWriteAccess(int index)
		{
		}

		// Token: 0x17000082 RID: 130
		public T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
			}
			[WriteAccessRequired]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				UnsafeUtility.WriteArrayElement<T>(this.m_Buffer, index, value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00006833 File Offset: 0x00004A33
		public bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00006844 File Offset: 0x00004A44
		[WriteAccessRequired]
		public void Dispose()
		{
			bool flag = !this.IsCreated;
			if (!flag)
			{
				bool flag2 = this.m_AllocatorLabel == Allocator.Invalid;
				if (flag2)
				{
					throw new InvalidOperationException("The NativeArray can not be Disposed because it was not allocated with a valid allocator.");
				}
				bool flag3 = this.m_AllocatorLabel >= Allocator.FirstUserIndex;
				if (flag3)
				{
					throw new InvalidOperationException("The NativeArray can not be Disposed because it was allocated with a custom allocator, use CollectionHelper.Dispose in com.unity.collections package.");
				}
				bool flag4 = this.m_AllocatorLabel > Allocator.None;
				if (flag4)
				{
					UnsafeUtility.FreeTracked(this.m_Buffer, this.m_AllocatorLabel);
					this.m_AllocatorLabel = Allocator.Invalid;
				}
				this.m_Buffer = null;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000068C8 File Offset: 0x00004AC8
		public JobHandle Dispose(JobHandle inputDeps)
		{
			bool flag = !this.IsCreated;
			JobHandle jobHandle;
			if (flag)
			{
				jobHandle = inputDeps;
			}
			else
			{
				bool flag2 = this.m_AllocatorLabel >= Allocator.FirstUserIndex;
				if (flag2)
				{
					throw new InvalidOperationException("The NativeArray can not be Disposed because it was allocated with a custom allocator, use CollectionHelper.Dispose in com.unity.collections package.");
				}
				bool flag3 = this.m_AllocatorLabel > Allocator.None;
				if (flag3)
				{
					JobHandle jobHandle2 = new NativeArrayDisposeJob
					{
						Data = new NativeArrayDispose
						{
							m_Buffer = this.m_Buffer,
							m_AllocatorLabel = this.m_AllocatorLabel
						}
					}.Schedule(inputDeps);
					this.m_Buffer = null;
					this.m_AllocatorLabel = Allocator.Invalid;
					jobHandle = jobHandle2;
				}
				else
				{
					this.m_Buffer = null;
					jobHandle = inputDeps;
				}
			}
			return jobHandle;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00006974 File Offset: 0x00004B74
		[WriteAccessRequired]
		public void CopyFrom(T[] array)
		{
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00006984 File Offset: 0x00004B84
		[WriteAccessRequired]
		public void CopyFrom(NativeArray<T> array)
		{
			NativeArray<T>.Copy(array, this);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00006994 File Offset: 0x00004B94
		public void CopyTo(T[] array)
		{
			NativeArray<T>.Copy(this, array);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000069A4 File Offset: 0x00004BA4
		public void CopyTo(NativeArray<T> array)
		{
			NativeArray<T>.Copy(this, array);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000069B4 File Offset: 0x00004BB4
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			NativeArray<T>.Copy(this, array, this.Length);
			return array;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000069E8 File Offset: 0x00004BE8
		public NativeArray<T>.Enumerator GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00006A00 File Offset: 0x00004C00
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeArray<T>.Enumerator(ref this);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00006A20 File Offset: 0x00004C20
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00006A40 File Offset: 0x00004C40
		public bool Equals(NativeArray<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Length == other.m_Length;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00006A74 File Offset: 0x00004C74
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeArray<T> && this.Equals((NativeArray<T>)obj);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00006AAC File Offset: 0x00004CAC
		public override int GetHashCode()
		{
			return (this.m_Buffer * 397) ^ this.m_Length;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public static bool operator ==(NativeArray<T> left, NativeArray<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public static bool operator !=(NativeArray<T> left, NativeArray<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00006B0D File Offset: 0x00004D0D
		public static void Copy(NativeArray<T> src, NativeArray<T> dst)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, src.Length);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00006B21 File Offset: 0x00004D21
		public static void Copy(NativeArray<T>.ReadOnly src, NativeArray<T> dst)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, src.Length);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00006B35 File Offset: 0x00004D35
		public static void Copy(T[] src, NativeArray<T> dst)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, src.Length);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00006B45 File Offset: 0x00004D45
		public static void Copy(NativeArray<T> src, T[] dst)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, src.Length);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00006B59 File Offset: 0x00004D59
		public static void Copy(NativeArray<T>.ReadOnly src, T[] dst)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, src.Length);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00006B6D File Offset: 0x00004D6D
		public static void Copy(NativeArray<T> src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, length);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00006B7B File Offset: 0x00004D7B
		public static void Copy(NativeArray<T>.ReadOnly src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, length);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00006B89 File Offset: 0x00004D89
		public static void Copy(T[] src, NativeArray<T> dst, int length)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, length);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00006B97 File Offset: 0x00004D97
		public static void Copy(NativeArray<T> src, T[] dst, int length)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, length);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00006BA5 File Offset: 0x00004DA5
		public static void Copy(NativeArray<T>.ReadOnly src, T[] dst, int length)
		{
			NativeArray<T>.CopySafe(src, 0, dst, 0, length);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00006BB3 File Offset: 0x00004DB3
		public static void Copy(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			NativeArray<T>.CopySafe(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00006BC2 File Offset: 0x00004DC2
		public static void Copy(NativeArray<T>.ReadOnly src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			NativeArray<T>.CopySafe(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00006BD1 File Offset: 0x00004DD1
		public static void Copy(T[] src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			NativeArray<T>.CopySafe(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00006BE0 File Offset: 0x00004DE0
		public static void Copy(NativeArray<T> src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			NativeArray<T>.CopySafe(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00006BEF File Offset: 0x00004DEF
		public static void Copy(NativeArray<T>.ReadOnly src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			NativeArray<T>.CopySafe(src, srcIndex, dst, dstIndex, length);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00006BFE File Offset: 0x00004DFE
		private unsafe static void CopySafe(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00006C2C File Offset: 0x00004E2C
		private unsafe static void CopySafe(NativeArray<T>.ReadOnly src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00006C5C File Offset: 0x00004E5C
		private unsafe static void CopySafe(T[] src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			GCHandle gchandle = GCHandle.Alloc(src, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)(void*)intPtr + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gchandle.Free();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00006CB0 File Offset: 0x00004EB0
		private unsafe static void CopySafe(NativeArray<T> src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			GCHandle gchandle = GCHandle.Alloc(dst, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)(void*)intPtr + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gchandle.Free();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00006D04 File Offset: 0x00004F04
		private unsafe static void CopySafe(NativeArray<T>.ReadOnly src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			GCHandle gchandle = GCHandle.Alloc(dst, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((void*)((byte*)(void*)intPtr + dstIndex * UnsafeUtility.SizeOf<T>()), (void*)((byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>()), (long)(length * UnsafeUtility.SizeOf<T>()));
			gchandle.Free();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00006D58 File Offset: 0x00004F58
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyPtr(T[] ptr)
		{
			bool flag = ptr == null;
			if (flag)
			{
				throw new ArgumentNullException("ptr");
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00006D7C File Offset: 0x00004F7C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyLengths(int srcLength, int dstLength)
		{
			bool flag = srcLength != dstLength;
			if (flag)
			{
				throw new ArgumentException("source and destination length must be the same");
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00006DA0 File Offset: 0x00004FA0
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckCopyArguments(int srcLength, int srcIndex, int dstLength, int dstIndex, int length)
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("length", "length must be equal or greater than zero.");
			}
			bool flag2 = srcIndex < 0 || srcIndex > srcLength || (srcIndex == srcLength && srcLength > 0);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("srcIndex", "srcIndex is outside the range of valid indexes for the source NativeArray.");
			}
			bool flag3 = dstIndex < 0 || dstIndex > dstLength || (dstIndex == dstLength && dstLength > 0);
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("dstIndex", "dstIndex is outside the range of valid indexes for the destination NativeArray.");
			}
			bool flag4 = srcIndex + length > srcLength;
			if (flag4)
			{
				throw new ArgumentException("length is greater than the number of elements from srcIndex to the end of the source NativeArray.", "length");
			}
			bool flag5 = srcIndex + length < 0;
			if (flag5)
			{
				throw new ArgumentException("srcIndex + length causes an integer overflow");
			}
			bool flag6 = dstIndex + length > dstLength;
			if (flag6)
			{
				throw new ArgumentException("length is greater than the number of elements from dstIndex to the end of the destination NativeArray.", "length");
			}
			bool flag7 = dstIndex + length < 0;
			if (flag7)
			{
				throw new ArgumentException("dstIndex + length causes an integer overflow");
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretLoadRange<U>(int sourceIndex) where U : struct
		{
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretStoreRange<U>(int destIndex) where U : struct
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00006E84 File Offset: 0x00005084
		public unsafe U ReinterpretLoad<U>(int sourceIndex) where U : struct
		{
			byte* ptr = (byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)sourceIndex;
			return UnsafeUtility.ReadArrayElement<U>((void*)ptr, 0);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00006EB0 File Offset: 0x000050B0
		public unsafe void ReinterpretStore<U>(int destIndex, U data) where U : struct
		{
			byte* ptr = (byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)destIndex;
			UnsafeUtility.WriteArrayElement<U>((void*)ptr, 0, data);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00006EDC File Offset: 0x000050DC
		private NativeArray<U> InternalReinterpret<U>(int length) where U : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<U>(this.m_Buffer, length, this.m_AllocatorLabel);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00006F04 File Offset: 0x00005104
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckReinterpretSize<U>() where U : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != UnsafeUtility.SizeOf<U>();
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Types {0} and {1} are different sizes - direct reinterpretation is not possible. If this is what you intended, use Reinterpret(<type size>)", typeof(T), typeof(U)));
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00006F4C File Offset: 0x0000514C
		public NativeArray<U> Reinterpret<U>() where U : struct
		{
			return this.InternalReinterpret<U>(this.Length);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00006F6C File Offset: 0x0000516C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReinterpretSize<U>(long tSize, long uSize, int expectedTypeSize, long byteLen, long uLen)
		{
			bool flag = tSize != (long)expectedTypeSize;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Type {0} was expected to be {1} but is {2} bytes", typeof(T), expectedTypeSize, tSize));
			}
			bool flag2 = uLen * uSize != byteLen;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("Types {0} (array length {1}) and {2} cannot be aliased due to size constraints. The size of the types and lengths involved must line up.", typeof(T), this.Length, typeof(U)));
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00006FEC File Offset: 0x000051EC
		public NativeArray<U> Reinterpret<U>(int expectedTypeSize) where U : struct
		{
			long num = (long)UnsafeUtility.SizeOf<T>();
			long num2 = (long)UnsafeUtility.SizeOf<U>();
			long num3 = (long)this.Length * num;
			long num4 = num3 / num2;
			return this.InternalReinterpret<U>((int)num4);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00007024 File Offset: 0x00005224
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckGetSubArrayArguments(int start, int length)
		{
			bool flag = start < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("start", "start must be >= 0");
			}
			bool flag2 = start + length > this.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", string.Format("sub array range {0}-{1} is outside the range of the native array 0-{2}", start, start + length - 1, this.Length - 1));
			}
			bool flag3 = start + length < 0;
			if (flag3)
			{
				throw new ArgumentException(string.Format("sub array range {0}-{1} caused an integer overflow and is outside the range of the native array 0-{2}", start, start + length - 1, this.Length - 1));
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000070C8 File Offset: 0x000052C8
		public unsafe NativeArray<T> GetSubArray(int start, int length)
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)((byte*)this.m_Buffer + (long)UnsafeUtility.SizeOf<T>() * (long)start), length, Allocator.None);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000070F4 File Offset: 0x000052F4
		public NativeArray<T>.ReadOnly AsReadOnly()
		{
			return new NativeArray<T>.ReadOnly(this.m_Buffer, this.m_Length);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00007118 File Offset: 0x00005318
		[WriteAccessRequired]
		public readonly Span<T> AsSpan()
		{
			return new Span<T>(this.m_Buffer, this.m_Length);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000713C File Offset: 0x0000533C
		public readonly ReadOnlySpan<T> AsReadOnlySpan()
		{
			return new ReadOnlySpan<T>(this.m_Buffer, this.m_Length);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00007160 File Offset: 0x00005360
		public static implicit operator Span<T>(in NativeArray<T> source)
		{
			return source.AsSpan();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00007178 File Offset: 0x00005378
		public static implicit operator ReadOnlySpan<T>(in NativeArray<T> source)
		{
			return source.AsReadOnlySpan();
		}

		// Token: 0x04000274 RID: 628
		[NativeDisableUnsafePtrRestriction]
		[VisibleToOtherModules(new string[] { "UnityEngine.ContentLoadModule", "UnityEngine.TilemapModule" })]
		internal unsafe void* m_Buffer;

		// Token: 0x04000275 RID: 629
		internal int m_Length;

		// Token: 0x04000276 RID: 630
		internal Allocator m_AllocatorLabel;

		// Token: 0x020000A5 RID: 165
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000373 RID: 883 RVA: 0x00007190 File Offset: 0x00005390
			public Enumerator(ref NativeArray<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
				this.value = default(T);
			}

			// Token: 0x06000374 RID: 884 RVA: 0x00002925 File Offset: 0x00000B25
			public void Dispose()
			{
			}

			// Token: 0x06000375 RID: 885 RVA: 0x000071B4 File Offset: 0x000053B4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				this.m_Index++;
				bool flag = this.m_Index < this.m_Array.m_Length;
				bool flag2;
				if (flag)
				{
					this.value = UnsafeUtility.ReadArrayElement<T>(this.m_Array.m_Buffer, this.m_Index);
					flag2 = true;
				}
				else
				{
					this.value = default(T);
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x06000376 RID: 886 RVA: 0x00007219 File Offset: 0x00005419
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000377 RID: 887 RVA: 0x00007224 File Offset: 0x00005424
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.value;
				}
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000378 RID: 888 RVA: 0x0000723C File Offset: 0x0000543C
			object IEnumerator.Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000277 RID: 631
			private NativeArray<T> m_Array;

			// Token: 0x04000278 RID: 632
			private int m_Index;

			// Token: 0x04000279 RID: 633
			private T value;
		}

		// Token: 0x020000A6 RID: 166
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[DebuggerDisplay("Length = {Length}")]
		[DebuggerTypeProxy(typeof(NativeArrayReadOnlyDebugView<>))]
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000379 RID: 889 RVA: 0x00007259 File Offset: 0x00005459
			internal unsafe ReadOnly(void* buffer, int length)
			{
				this.m_Buffer = buffer;
				this.m_Length = length;
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x0600037A RID: 890 RVA: 0x0000726C File Offset: 0x0000546C
			public int Length
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Length;
				}
			}

			// Token: 0x0600037B RID: 891 RVA: 0x00007284 File Offset: 0x00005484
			public void CopyTo(T[] array)
			{
				NativeArray<T>.Copy(this, array);
			}

			// Token: 0x0600037C RID: 892 RVA: 0x00007293 File Offset: 0x00005493
			public void CopyTo(NativeArray<T> array)
			{
				NativeArray<T>.Copy(this, array);
			}

			// Token: 0x0600037D RID: 893 RVA: 0x000072A4 File Offset: 0x000054A4
			public T[] ToArray()
			{
				T[] array = new T[this.m_Length];
				NativeArray<T>.Copy(this, array, this.m_Length);
				return array;
			}

			// Token: 0x0600037E RID: 894 RVA: 0x000072D8 File Offset: 0x000054D8
			public NativeArray<U>.ReadOnly Reinterpret<U>() where U : struct
			{
				return new NativeArray<U>.ReadOnly(this.m_Buffer, this.m_Length);
			}

			// Token: 0x17000087 RID: 135
			public T this[int index]
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return UnsafeUtility.ReadArrayElement<T>(this.m_Buffer, index);
				}
			}

			// Token: 0x06000380 RID: 896 RVA: 0x0000731C File Offset: 0x0000551C
			public readonly ref T UnsafeElementAt(int index)
			{
				return UnsafeUtility.ArrayElementAsRef<T>(this.m_Buffer, index);
			}

			// Token: 0x06000381 RID: 897 RVA: 0x0000733C File Offset: 0x0000553C
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void CheckElementReadAccess(int index)
			{
				bool flag = index >= this.m_Length;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Index {0} is out of range (must be between 0 and {1}).", index, this.m_Length - 1));
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x06000382 RID: 898 RVA: 0x0000737E File Offset: 0x0000557E
			public bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Buffer != null;
				}
			}

			// Token: 0x06000383 RID: 899 RVA: 0x00007390 File Offset: 0x00005590
			public NativeArray<T>.ReadOnly.Enumerator GetEnumerator()
			{
				return new NativeArray<T>.ReadOnly.Enumerator(in this);
			}

			// Token: 0x06000384 RID: 900 RVA: 0x000073A8 File Offset: 0x000055A8
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000385 RID: 901 RVA: 0x000073C8 File Offset: 0x000055C8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000386 RID: 902 RVA: 0x000073E8 File Offset: 0x000055E8
			public readonly ReadOnlySpan<T> AsReadOnlySpan()
			{
				return new ReadOnlySpan<T>(this.m_Buffer, this.m_Length);
			}

			// Token: 0x06000387 RID: 903 RVA: 0x0000740C File Offset: 0x0000560C
			public static implicit operator ReadOnlySpan<T>(in NativeArray<T>.ReadOnly source)
			{
				return source.AsReadOnlySpan();
			}

			// Token: 0x0400027A RID: 634
			[NativeDisableUnsafePtrRestriction]
			internal unsafe void* m_Buffer;

			// Token: 0x0400027B RID: 635
			internal int m_Length;

			// Token: 0x020000A7 RID: 167
			[ExcludeFromDocs]
			public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
			{
				// Token: 0x06000388 RID: 904 RVA: 0x00007424 File Offset: 0x00005624
				public Enumerator(in NativeArray<T>.ReadOnly array)
				{
					this.m_Array = array;
					this.m_Index = -1;
					this.value = default(T);
				}

				// Token: 0x06000389 RID: 905 RVA: 0x00002925 File Offset: 0x00000B25
				public void Dispose()
				{
				}

				// Token: 0x0600038A RID: 906 RVA: 0x00007448 File Offset: 0x00005648
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public bool MoveNext()
				{
					this.m_Index++;
					bool flag = this.m_Index < this.m_Array.m_Length;
					bool flag2;
					if (flag)
					{
						this.value = UnsafeUtility.ReadArrayElement<T>(this.m_Array.m_Buffer, this.m_Index);
						flag2 = true;
					}
					else
					{
						this.value = default(T);
						flag2 = false;
					}
					return flag2;
				}

				// Token: 0x0600038B RID: 907 RVA: 0x000074AD File Offset: 0x000056AD
				public void Reset()
				{
					this.m_Index = -1;
				}

				// Token: 0x17000089 RID: 137
				// (get) Token: 0x0600038C RID: 908 RVA: 0x000074B7 File Offset: 0x000056B7
				public T Current
				{
					[MethodImpl(MethodImplOptions.AggressiveInlining)]
					get
					{
						return this.value;
					}
				}

				// Token: 0x1700008A RID: 138
				// (get) Token: 0x0600038D RID: 909 RVA: 0x000074BF File Offset: 0x000056BF
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0400027C RID: 636
				private NativeArray<T>.ReadOnly m_Array;

				// Token: 0x0400027D RID: 637
				private int m_Index;

				// Token: 0x0400027E RID: 638
				private T value;
			}
		}
	}
}
