using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Hierarchy
{
	// Token: 0x0200001A RID: 26
	internal struct NativeSparseArray<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> : IDisposable where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000035CF File Offset: 0x000017CF
		public bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Ptr != null;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000035DE File Offset: 0x000017DE
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000035E6 File Offset: 0x000017E6
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Capacity;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.Allocate(value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000035F0 File Offset: 0x000017F0
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000019 RID: 25
		public unsafe TValue this[in TKey key]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				int num = this.m_KeyIndex(in key);
				this.ThrowIfIndexOutOfRange(num);
				ref NativeSparseArray<TKey, TValue>.Pair ptr = ref this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)];
				bool flag = !this.m_KeyEqual(in ptr.Key, in key);
				if (flag)
				{
					TKey tkey = key;
					throw new KeyNotFoundException(tkey.ToString());
				}
				return ptr.Value;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				int num = this.m_KeyIndex(in key);
				this.ThrowIfIndexIsNegative(num);
				this.EnsureCapacity(num + 1, NativeSparseArrayResizePolicy.ExactSize);
				ref NativeSparseArray<TKey, TValue>.Pair ptr = ref this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)];
				NativeSparseArray<TKey, TValue>.KeyEqual keyEqual = this.m_KeyEqual;
				ref NativeSparseArray<TKey, TValue>.Pair ptr2 = ref ptr;
				TKey tkey = default(TKey);
				bool flag = keyEqual(in ptr2.Key, in tkey);
				if (flag)
				{
					this.m_Count++;
				}
				this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)] = new NativeSparseArray<TKey, TValue>.Pair(in key, in value);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000036F4 File Offset: 0x000018F4
		public NativeSparseArray(NativeSparseArray<TKey, TValue>.KeyIndex keyIndex, Allocator allocator)
		{
			this.m_Ptr = null;
			this.m_Capacity = 0;
			this.m_Count = 0;
			this.m_Allocator = allocator;
			this.m_InitValue = default(NativeSparseArray<TKey, TValue>.Pair);
			this.m_KeyIndex = keyIndex;
			this.m_KeyEqual = delegate(in TKey lhs, in TKey rhs)
			{
				TKey tkey = lhs;
				return tkey.Equals(rhs);
			};
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003757 File Offset: 0x00001957
		public NativeSparseArray(NativeSparseArray<TKey, TValue>.KeyIndex keyIndex, NativeSparseArray<TKey, TValue>.KeyEqual keyEqual, Allocator allocator)
		{
			this.m_Ptr = null;
			this.m_Capacity = 0;
			this.m_Count = 0;
			this.m_Allocator = allocator;
			this.m_InitValue = default(NativeSparseArray<TKey, TValue>.Pair);
			this.m_KeyIndex = keyIndex;
			this.m_KeyEqual = keyEqual;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003794 File Offset: 0x00001994
		public NativeSparseArray(in TValue initValue, NativeSparseArray<TKey, TValue>.KeyIndex keyIndex, Allocator allocator)
		{
			this.m_Ptr = null;
			this.m_Capacity = 0;
			this.m_Count = 0;
			this.m_Allocator = allocator;
			TKey tkey = default(TKey);
			this.m_InitValue = new NativeSparseArray<TKey, TValue>.Pair(in tkey, in initValue);
			this.m_KeyIndex = keyIndex;
			this.m_KeyEqual = delegate(in TKey lhs, in TKey rhs)
			{
				TKey tkey2 = lhs;
				return tkey2.Equals(rhs);
			};
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003800 File Offset: 0x00001A00
		public NativeSparseArray(in TValue initValue, NativeSparseArray<TKey, TValue>.KeyIndex keyIndex, NativeSparseArray<TKey, TValue>.KeyEqual keyEqual, Allocator allocator)
		{
			this.m_Ptr = null;
			this.m_Capacity = 0;
			this.m_Count = 0;
			this.m_Allocator = allocator;
			TKey tkey = default(TKey);
			this.m_InitValue = new NativeSparseArray<TKey, TValue>.Pair(in tkey, in initValue);
			this.m_KeyIndex = keyIndex;
			this.m_KeyEqual = keyEqual;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000384F File Offset: 0x00001A4F
		public void Dispose()
		{
			this.Deallocate();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003859 File Offset: 0x00001A59
		public void Reserve(int capacity)
		{
			this.EnsureCapacity(capacity, NativeSparseArrayResizePolicy.ExactSize);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003868 File Offset: 0x00001A68
		public unsafe bool ContainsKey(in TKey key)
		{
			int num = this.m_KeyIndex(in key);
			this.ThrowIfIndexOutOfRange(num);
			ref NativeSparseArray<TKey, TValue>.Pair ptr = ref this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)];
			return this.m_KeyEqual(in ptr.Key, in key);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000038B4 File Offset: 0x00001AB4
		public unsafe void Add(in TKey key, in TValue value, NativeSparseArrayResizePolicy policy = NativeSparseArrayResizePolicy.ExactSize)
		{
			int num = this.m_KeyIndex(in key);
			this.ThrowIfIndexIsNegative(num);
			this.EnsureCapacity(num + 1, policy);
			ref NativeSparseArray<TKey, TValue>.Pair ptr = ref this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)];
			bool flag = this.m_KeyEqual(in ptr.Key, in key);
			if (flag)
			{
				throw new ArgumentException(string.Format("an element with the same key [{0}] already exists", key));
			}
			NativeSparseArray<TKey, TValue>.KeyEqual keyEqual = this.m_KeyEqual;
			ref NativeSparseArray<TKey, TValue>.Pair ptr2 = ref ptr;
			TKey tkey = default(TKey);
			bool flag2 = keyEqual(in ptr2.Key, in tkey);
			if (flag2)
			{
				this.m_Count++;
			}
			this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)] = new NativeSparseArray<TKey, TValue>.Pair(in key, in value);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000396C File Offset: 0x00001B6C
		public unsafe void AddNoResize(in TKey key, in TValue value)
		{
			int num = this.m_KeyIndex(in key);
			this.ThrowIfIndexOutOfRange(num);
			ref NativeSparseArray<TKey, TValue>.Pair ptr = ref this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)];
			bool flag = this.m_KeyEqual(in ptr.Key, in key);
			if (flag)
			{
				throw new ArgumentException(string.Format("an element with the same key [{0}] already exists", key));
			}
			NativeSparseArray<TKey, TValue>.KeyEqual keyEqual = this.m_KeyEqual;
			ref NativeSparseArray<TKey, TValue>.Pair ptr2 = ref ptr;
			TKey tkey = default(TKey);
			bool flag2 = keyEqual(in ptr2.Key, in tkey);
			if (flag2)
			{
				this.m_Count++;
			}
			this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)] = new NativeSparseArray<TKey, TValue>.Pair(in key, in value);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003A1C File Offset: 0x00001C1C
		public unsafe bool TryAdd(in TKey key, in TValue value, NativeSparseArrayResizePolicy policy = NativeSparseArrayResizePolicy.ExactSize)
		{
			int num = this.m_KeyIndex(in key);
			this.ThrowIfIndexIsNegative(num);
			this.EnsureCapacity(num + 1, policy);
			ref NativeSparseArray<TKey, TValue>.Pair ptr = ref this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)];
			bool flag = this.m_KeyEqual(in ptr.Key, in key);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				NativeSparseArray<TKey, TValue>.KeyEqual keyEqual = this.m_KeyEqual;
				ref NativeSparseArray<TKey, TValue>.Pair ptr2 = ref ptr;
				TKey tkey = default(TKey);
				bool flag3 = keyEqual(in ptr2.Key, in tkey);
				if (flag3)
				{
					this.m_Count++;
				}
				this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)] = new NativeSparseArray<TKey, TValue>.Pair(in key, in value);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public unsafe bool TryAddNoResize(in TKey key, in TValue value)
		{
			int num = this.m_KeyIndex(in key);
			this.ThrowIfIndexOutOfRange(num);
			ref NativeSparseArray<TKey, TValue>.Pair ptr = ref this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)];
			bool flag = this.m_KeyEqual(in ptr.Key, in key);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				NativeSparseArray<TKey, TValue>.KeyEqual keyEqual = this.m_KeyEqual;
				ref NativeSparseArray<TKey, TValue>.Pair ptr2 = ref ptr;
				TKey tkey = default(TKey);
				bool flag3 = keyEqual(in ptr2.Key, in tkey);
				if (flag3)
				{
					this.m_Count++;
				}
				this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)] = new NativeSparseArray<TKey, TValue>.Pair(in key, in value);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B64 File Offset: 0x00001D64
		public unsafe bool TryGetValue(in TKey key, out TValue value)
		{
			int num = this.m_KeyIndex(in key);
			this.ThrowIfIndexOutOfRange(num);
			ref NativeSparseArray<TKey, TValue>.Pair ptr = ref this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)];
			bool flag = this.m_KeyEqual(in ptr.Key, in key);
			bool flag2;
			if (flag)
			{
				value = ptr.Value;
				flag2 = true;
			}
			else
			{
				value = default(TValue);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003BCC File Offset: 0x00001DCC
		public unsafe bool Remove(in TKey key)
		{
			int num = this.m_KeyIndex(in key);
			this.ThrowIfIndexOutOfRange(num);
			ref NativeSparseArray<TKey, TValue>.Pair ptr = ref this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)];
			bool flag = !this.m_KeyEqual(in ptr.Key, in key);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.m_Ptr[(IntPtr)num * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)] = this.m_InitValue;
				this.m_Count--;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C4C File Offset: 0x00001E4C
		public unsafe void Clear()
		{
			bool flag = this.m_Ptr != null;
			if (flag)
			{
				fixed (NativeSparseArray<TKey, TValue>.Pair* ptr = &this.m_InitValue)
				{
					void* ptr2 = (void*)ptr;
					UnsafeUtility.MemCpyReplicate((void*)this.m_Ptr, ptr2, UnsafeUtility.SizeOf<NativeSparseArray<TKey, TValue>.Pair>(), this.m_Capacity);
				}
			}
			this.m_Count = 0;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003C9C File Offset: 0x00001E9C
		private unsafe void Allocate(int capacity)
		{
			bool flag = capacity < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("capacity [{0}] cannot be negative", capacity));
			}
			int num = UnsafeUtility.SizeOf<NativeSparseArray<TKey, TValue>.Pair>();
			int num2 = UnsafeUtility.AlignOf<NativeSparseArray<TKey, TValue>.Pair>();
			bool flag2 = this.m_Ptr == null;
			if (flag2)
			{
				this.m_Ptr = (NativeSparseArray<TKey, TValue>.Pair*)UnsafeUtility.Malloc((long)(capacity * num), num2, this.m_Allocator);
				fixed (NativeSparseArray<TKey, TValue>.Pair* ptr = &this.m_InitValue)
				{
					NativeSparseArray<TKey, TValue>.Pair* ptr2 = ptr;
					UnsafeUtility.MemCpyReplicate((void*)this.m_Ptr, (void*)ptr2, num, capacity);
				}
			}
			else
			{
				this.m_Ptr = (NativeSparseArray<TKey, TValue>.Pair*)NativeSparseArray<TKey, TValue>.Realloc((void*)this.m_Ptr, (long)(capacity * num), num2, this.m_Allocator);
				bool flag3 = capacity > this.m_Capacity;
				if (flag3)
				{
					fixed (NativeSparseArray<TKey, TValue>.Pair* ptr3 = &this.m_InitValue)
					{
						NativeSparseArray<TKey, TValue>.Pair* ptr4 = ptr3;
						UnsafeUtility.MemCpyReplicate((void*)(this.m_Ptr + (IntPtr)this.m_Capacity * (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair) / (IntPtr)sizeof(NativeSparseArray<TKey, TValue>.Pair)), (void*)ptr4, num, capacity - this.m_Capacity);
					}
				}
			}
			this.m_Capacity = capacity;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003D90 File Offset: 0x00001F90
		private unsafe void Deallocate()
		{
			bool flag = this.m_Ptr != null;
			if (flag)
			{
				UnsafeUtility.Free((void*)this.m_Ptr, this.m_Allocator);
				this.m_Ptr = null;
			}
			this.m_Capacity = 0;
			this.m_Count = 0;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003DDC File Offset: 0x00001FDC
		private void EnsureCapacity(int capacity, NativeSparseArrayResizePolicy policy)
		{
			bool flag = capacity <= this.m_Capacity;
			if (!flag)
			{
				NativeSparseArrayResizePolicy nativeSparseArrayResizePolicy = policy;
				NativeSparseArrayResizePolicy nativeSparseArrayResizePolicy2 = nativeSparseArrayResizePolicy;
				if (nativeSparseArrayResizePolicy2 != NativeSparseArrayResizePolicy.ExactSize)
				{
					if (nativeSparseArrayResizePolicy2 != NativeSparseArrayResizePolicy.DoubleSize)
					{
						throw new NotImplementedException(policy.ToString());
					}
					this.Allocate(Math.Max(capacity, this.m_Capacity * 2));
				}
				else
				{
					this.Allocate(capacity);
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003E40 File Offset: 0x00002040
		private void ThrowIfIndexIsNegative(int index)
		{
			bool flag = index < 0;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("key index [{0}] cannot be negative", index));
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003E6C File Offset: 0x0000206C
		private void ThrowIfIndexOutOfRange(int index)
		{
			this.ThrowIfIndexIsNegative(index);
			bool flag = index >= this.m_Capacity;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("key index [{0}] is out of range [0, {1}]", index, this.m_Capacity));
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003EB4 File Offset: 0x000020B4
		private unsafe static void* Realloc(void* ptr, long size, int alignment, Allocator allocator)
		{
			bool flag = ptr == null;
			void* ptr2;
			if (flag)
			{
				ptr2 = UnsafeUtility.Malloc(size, alignment, allocator);
			}
			else
			{
				void* ptr3 = UnsafeUtility.Malloc(size, alignment, allocator);
				UnsafeUtility.MemCpy(ptr3, ptr, size);
				UnsafeUtility.Free(ptr, allocator);
				ptr2 = ptr3;
			}
			return ptr2;
		}

		// Token: 0x0400003B RID: 59
		private unsafe NativeSparseArray<TKey, TValue>.Pair* m_Ptr;

		// Token: 0x0400003C RID: 60
		private int m_Capacity;

		// Token: 0x0400003D RID: 61
		private int m_Count;

		// Token: 0x0400003E RID: 62
		private readonly Allocator m_Allocator;

		// Token: 0x0400003F RID: 63
		private readonly NativeSparseArray<TKey, TValue>.Pair m_InitValue;

		// Token: 0x04000040 RID: 64
		private readonly NativeSparseArray<TKey, TValue>.KeyIndex m_KeyIndex;

		// Token: 0x04000041 RID: 65
		private readonly NativeSparseArray<TKey, TValue>.KeyEqual m_KeyEqual;

		// Token: 0x0200001B RID: 27
		private readonly struct Pair
		{
			// Token: 0x060000A7 RID: 167 RVA: 0x00003EF5 File Offset: 0x000020F5
			public Pair(in TKey key, in TValue value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x04000042 RID: 66
			public readonly TKey Key;

			// Token: 0x04000043 RID: 67
			public readonly TValue Value;
		}

		// Token: 0x0200001C RID: 28
		// (Invoke) Token: 0x060000A9 RID: 169
		public delegate int KeyIndex(in TKey key);

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x060000AD RID: 173
		public delegate bool KeyEqual(in TKey lhs, in TKey rhs);
	}
}
