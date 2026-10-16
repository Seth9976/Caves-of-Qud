using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006B9 RID: 1721
	internal struct LayoutList<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000BCC10 File Offset: 0x000BAE10
		public unsafe int Count
		{
			get
			{
				return this.m_Data->Count;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x000BCC1D File Offset: 0x000BAE1D
		public bool IsCreated
		{
			get
			{
				return null != this.m_Data;
			}
		}

		// Token: 0x17000C18 RID: 3096
		public unsafe ref T this[int index]
		{
			get
			{
				bool flag = (ulong)index > (ulong)((long)this.m_Data->Count);
				if (flag)
				{
					throw new ArgumentOutOfRangeException();
				}
				return ref this.m_Data->Values[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000BCC6D File Offset: 0x000BAE6D
		public LayoutList()
		{
			this.m_Data = null;
			this.m_Allocator = Allocator.Invalid;
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000BCC80 File Offset: 0x000BAE80
		public unsafe LayoutList(int initialCapacity, Allocator allocator)
		{
			this.m_Allocator = allocator;
			this.m_Data = (LayoutList<T>.Data*)UnsafeUtility.Malloc((long)UnsafeUtility.SizeOf<LayoutList<T>.Data>(), 16, allocator);
			Assert.IsTrue(this.m_Data != null);
			UnsafeUtility.MemClear((void*)this.m_Data, (long)UnsafeUtility.SizeOf<LayoutList<T>.Data>());
			this.ResizeCapacity(initialCapacity);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000BCCD8 File Offset: 0x000BAED8
		public unsafe void Dispose()
		{
			bool flag = null == this.m_Data;
			if (!flag)
			{
				bool flag2 = this.m_Data->Values != null;
				if (flag2)
				{
					UnsafeUtility.Free((void*)this.m_Data->Values, this.m_Allocator);
				}
				UnsafeUtility.Free((void*)this.m_Data, this.m_Allocator);
				this.m_Data = null;
			}
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000BCD40 File Offset: 0x000BAF40
		public unsafe void Insert(int index, T value)
		{
			bool flag = (ulong)index > (ulong)((long)this.m_Data->Count);
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			bool flag2 = this.m_Data->Capacity == this.m_Data->Count;
			if (flag2)
			{
				this.IncreaseCapacity();
			}
			bool flag3 = index < this.m_Data->Count;
			if (flag3)
			{
				UnsafeUtility.MemMove((void*)(this.m_Data->Values + (IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T) + sizeof(T) / sizeof(T)), (void*)(this.m_Data->Values + (IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (long)(UnsafeUtility.SizeOf<T>() * (this.m_Data->Count - index)));
			}
			this.m_Data->Values[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			this.m_Data->Count++;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000BCE18 File Offset: 0x000BB018
		public unsafe int IndexOf(T value)
		{
			int count = this.m_Data->Count;
			T* ptr = &value;
			T* ptr2 = this.m_Data->Values;
			int num = UnsafeUtility.SizeOf<T>();
			int i = 0;
			while (i < count)
			{
				bool flag = UnsafeUtility.MemCmp((void*)ptr2, (void*)ptr, (long)num) == 0;
				if (flag)
				{
					return i;
				}
				i++;
				ptr2 += sizeof(T) / sizeof(T);
			}
			return -1;
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000BCE88 File Offset: 0x000BB088
		public unsafe void RemoveAt(int index)
		{
			bool flag = (ulong)index >= (ulong)((long)this.m_Data->Count);
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_Data->Count--;
			UnsafeUtility.MemMove((void*)(this.m_Data->Values + (IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)(this.m_Data->Values + (IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T) + sizeof(T) / sizeof(T)), (long)(UnsafeUtility.SizeOf<T>() * (this.m_Data->Count - index)));
			this.m_Data->Values[(IntPtr)this.m_Data->Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = default(T);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000BCF33 File Offset: 0x000BB133
		public unsafe void Clear()
		{
			this.m_Data->Count = 0;
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x000BCF42 File Offset: 0x000BB142
		private unsafe void IncreaseCapacity()
		{
			this.EnsureCapacity(this.m_Data->Capacity * 2);
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000BCF5C File Offset: 0x000BB15C
		private unsafe void EnsureCapacity(int capacity)
		{
			bool flag = capacity <= this.m_Data->Capacity;
			if (!flag)
			{
				this.ResizeCapacity(capacity);
			}
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000BCF8C File Offset: 0x000BB18C
		private unsafe void ResizeCapacity(int capacity)
		{
			Assert.IsTrue(capacity > 0);
			this.m_Data->Values = (T*)LayoutList<T>.ResizeArray((void*)this.m_Data->Values, (long)this.m_Data->Capacity, (long)capacity, (long)UnsafeUtility.SizeOf<T>(), 16, this.m_Allocator);
			this.m_Data->Capacity = capacity;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000BCFE8 File Offset: 0x000BB1E8
		private unsafe static void* ResizeArray(void* fromPtr, long fromCount, long toCount, long size, int align, Allocator allocator)
		{
			Assert.IsTrue(toCount > 0L);
			void* ptr = UnsafeUtility.Malloc(size * toCount, align, allocator);
			Assert.IsTrue(ptr != null);
			bool flag = fromCount <= 0L;
			void* ptr2;
			if (flag)
			{
				ptr2 = ptr;
			}
			else
			{
				long num = ((toCount < fromCount) ? toCount : fromCount);
				long num2 = num * size;
				UnsafeUtility.MemCpy(ptr, fromPtr, num2);
				UnsafeUtility.Free(fromPtr, allocator);
				ptr2 = ptr;
			}
			return ptr2;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000BD054 File Offset: 0x000BB254
		public LayoutList<T>.Enumerator GetEnumerator()
		{
			return new LayoutList<T>.Enumerator(this);
		}

		// Token: 0x040018D9 RID: 6361
		private readonly Allocator m_Allocator;

		// Token: 0x040018DA RID: 6362
		private unsafe LayoutList<T>.Data* m_Data;

		// Token: 0x020006BA RID: 1722
		private struct Data
		{
			// Token: 0x040018DB RID: 6363
			public int Capacity;

			// Token: 0x040018DC RID: 6364
			public int Count;

			// Token: 0x040018DD RID: 6365
			public unsafe T* Values;
		}

		// Token: 0x020006BB RID: 1723
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x17000C19 RID: 3097
			// (get) Token: 0x06003208 RID: 12808 RVA: 0x000BD071 File Offset: 0x000BB271
			public T Current
			{
				get
				{
					return this.m_Current;
				}
			}

			// Token: 0x17000C1A RID: 3098
			// (get) Token: 0x06003209 RID: 12809 RVA: 0x000BD079 File Offset: 0x000BB279
			object IEnumerator.Current
			{
				get
				{
					return this.m_Current;
				}
			}

			// Token: 0x0600320A RID: 12810 RVA: 0x000BD086 File Offset: 0x000BB286
			public Enumerator(LayoutList<T> list)
			{
				this.m_List = list;
				this.m_Index = 0;
				this.m_Current = default(T);
			}

			// Token: 0x0600320B RID: 12811 RVA: 0x00002152 File Offset: 0x00000352
			public void Dispose()
			{
			}

			// Token: 0x0600320C RID: 12812 RVA: 0x000BD0A4 File Offset: 0x000BB2A4
			public unsafe bool MoveNext()
			{
				bool flag = !this.m_List.IsCreated;
				bool flag2;
				if (flag)
				{
					this.m_Current = default(T);
					flag2 = false;
				}
				else
				{
					bool flag3 = (ulong)this.m_Index >= (ulong)((long)this.m_List.Count);
					if (flag3)
					{
						this.m_Current = default(T);
						flag2 = false;
					}
					else
					{
						this.m_Current = *this.m_List[this.m_Index];
						this.m_Index++;
						flag2 = true;
					}
				}
				return flag2;
			}

			// Token: 0x0600320D RID: 12813 RVA: 0x000BD131 File Offset: 0x000BB331
			public void Reset()
			{
				this.m_Index = 0;
			}

			// Token: 0x040018DE RID: 6366
			private LayoutList<T> m_List;

			// Token: 0x040018DF RID: 6367
			private int m_Index;

			// Token: 0x040018E0 RID: 6368
			private T m_Current;
		}
	}
}
