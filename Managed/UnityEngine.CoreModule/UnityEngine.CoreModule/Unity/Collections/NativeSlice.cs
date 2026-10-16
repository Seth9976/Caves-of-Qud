using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Collections
{
	// Token: 0x020000AD RID: 173
	[DebuggerDisplay("Length = {Length}")]
	[NativeContainer]
	[NativeContainerSupportsMinMaxWriteRestriction]
	[DebuggerTypeProxy(typeof(NativeSliceDebugView<>))]
	public struct NativeSlice<T> : IEnumerable<T>, IEnumerable, IEquatable<NativeSlice<T>> where T : struct
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00007666 File Offset: 0x00005866
		public NativeSlice(NativeSlice<T> slice, int start)
		{
			this = new NativeSlice<T>(slice, start, slice.Length - start);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000767B File Offset: 0x0000587B
		public NativeSlice(NativeSlice<T> slice, int start, int length)
		{
			this.m_Stride = slice.m_Stride;
			this.m_Buffer = slice.m_Buffer + this.m_Stride * start;
			this.m_Length = length;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000076A6 File Offset: 0x000058A6
		public NativeSlice(NativeArray<T> array)
		{
			this = new NativeSlice<T>(array, 0, array.Length);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000076B9 File Offset: 0x000058B9
		public NativeSlice(NativeArray<T> array, int start)
		{
			this = new NativeSlice<T>(array, start, array.Length - start);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000076D0 File Offset: 0x000058D0
		public static implicit operator NativeSlice<T>(NativeArray<T> array)
		{
			return new NativeSlice<T>(array);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000076E8 File Offset: 0x000058E8
		public unsafe NativeSlice(NativeArray<T> array, int start, int length)
		{
			this.m_Stride = UnsafeUtility.SizeOf<T>();
			byte* ptr = (byte*)array.m_Buffer + this.m_Stride * start;
			this.m_Buffer = ptr;
			this.m_Length = length;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00007720 File Offset: 0x00005920
		public NativeSlice<U> SliceConvert<U>() where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			NativeSlice<U> nativeSlice;
			nativeSlice.m_Buffer = this.m_Buffer;
			nativeSlice.m_Stride = num;
			nativeSlice.m_Length = this.m_Length * this.m_Stride / num;
			return nativeSlice;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00007764 File Offset: 0x00005964
		public NativeSlice<U> SliceWithStride<U>(int offset) where U : struct
		{
			NativeSlice<U> nativeSlice;
			nativeSlice.m_Buffer = this.m_Buffer + offset;
			nativeSlice.m_Stride = this.m_Stride;
			nativeSlice.m_Length = this.m_Length;
			return nativeSlice;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000077A0 File Offset: 0x000059A0
		public NativeSlice<U> SliceWithStride<U>() where U : struct
		{
			return this.SliceWithStride<U>(0);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReadIndex(int index)
		{
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00002925 File Offset: 0x00000B25
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWriteIndex(int index)
		{
		}

		// Token: 0x1700008D RID: 141
		public unsafe T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride);
			}
			[WriteAccessRequired]
			set
			{
				UnsafeUtility.WriteArrayElementWithStride<T>((void*)this.m_Buffer, index, this.m_Stride, value);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000077F7 File Offset: 0x000059F7
		[WriteAccessRequired]
		public void CopyFrom(NativeSlice<T> slice)
		{
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, slice.GetUnsafeReadOnlyPtr<T>(), slice.Stride, UnsafeUtility.SizeOf<T>(), this.m_Length);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000782C File Offset: 0x00005A2C
		[WriteAccessRequired]
		public unsafe void CopyFrom(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr<T>(), this.Stride, (void*)intPtr, num, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00007880 File Offset: 0x00005A80
		public void CopyTo(NativeArray<T> array)
		{
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(array.GetUnsafePtr<T>(), num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000078BC File Offset: 0x00005ABC
		public unsafe void CopyTo(T[] array)
		{
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr intPtr = gchandle.AddrOfPinnedObject();
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride((void*)intPtr, num, this.GetUnsafeReadOnlyPtr<T>(), this.Stride, num, this.m_Length);
			gchandle.Free();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00007910 File Offset: 0x00005B10
		public T[] ToArray()
		{
			T[] array = new T[this.Length];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00007937 File Offset: 0x00005B37
		public int Stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00007940 File Offset: 0x00005B40
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00007958 File Offset: 0x00005B58
		public NativeSlice<T>.Enumerator GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00007970 File Offset: 0x00005B70
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new NativeSlice<T>.Enumerator(ref this);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00007990 File Offset: 0x00005B90
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000079B0 File Offset: 0x00005BB0
		public bool Equals(NativeSlice<T> other)
		{
			return this.m_Buffer == other.m_Buffer && this.m_Stride == other.m_Stride && this.m_Length == other.m_Length;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000079F0 File Offset: 0x00005BF0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is NativeSlice<T> && this.Equals((NativeSlice<T>)obj);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00007A28 File Offset: 0x00005C28
		public override int GetHashCode()
		{
			int num = this.m_Buffer;
			num = (num * 397) ^ this.m_Stride;
			return (num * 397) ^ this.m_Length;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00007A64 File Offset: 0x00005C64
		public static bool operator ==(NativeSlice<T> left, NativeSlice<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00007A80 File Offset: 0x00005C80
		public static bool operator !=(NativeSlice<T> left, NativeSlice<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000284 RID: 644
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* m_Buffer;

		// Token: 0x04000285 RID: 645
		internal int m_Stride;

		// Token: 0x04000286 RID: 646
		internal int m_Length;

		// Token: 0x020000AE RID: 174
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060003B7 RID: 951 RVA: 0x00007A9D File Offset: 0x00005C9D
			public Enumerator(ref NativeSlice<T> array)
			{
				this.m_Array = array;
				this.m_Index = -1;
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x00002925 File Offset: 0x00000B25
			public void Dispose()
			{
			}

			// Token: 0x060003B9 RID: 953 RVA: 0x00007AB4 File Offset: 0x00005CB4
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_Array.Length;
			}

			// Token: 0x060003BA RID: 954 RVA: 0x00007AE7 File Offset: 0x00005CE7
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060003BB RID: 955 RVA: 0x00007AF1 File Offset: 0x00005CF1
			public T Current
			{
				get
				{
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060003BC RID: 956 RVA: 0x00007B04 File Offset: 0x00005D04
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000287 RID: 647
			private NativeSlice<T> m_Array;

			// Token: 0x04000288 RID: 648
			private int m_Index;
		}
	}
}
