using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006A6 RID: 1702
	internal struct LayoutDataStore : IDisposable
	{
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x000BBE6E File Offset: 0x000BA06E
		public bool IsValid
		{
			get
			{
				return null != this.m_Data;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x000BBE7D File Offset: 0x000BA07D
		public unsafe int Capacity
		{
			get
			{
				return this.m_Data->Capacity;
			}
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000BBE8C File Offset: 0x000BA08C
		public unsafe LayoutDataStore(ComponentType[] components, int initialCapacity, Allocator allocator)
		{
			Assert.IsTrue(components.Length != 0, "LayoutDataStore requires at least one component size.");
			Assert.IsTrue(components[0].Size >= 4, string.Format("{0} requires a minimum element size of {1} to alias", "LayoutDataStore", 4));
			this.m_Allocator = allocator;
			this.m_Data = (LayoutDataStore.Data*)UnsafeUtility.Malloc((long)UnsafeUtility.SizeOf<LayoutDataStore.Data>(), UnsafeUtility.AlignOf<LayoutDataStore.Data>(), this.m_Allocator);
			UnsafeUtility.MemClear((void*)this.m_Data, (long)UnsafeUtility.SizeOf<LayoutDataStore.Data>());
			this.m_Data->ComponentCount = components.Length;
			this.m_Data->Components = (LayoutDataStore.ComponentDataStore*)UnsafeUtility.Malloc((long)(UnsafeUtility.SizeOf<LayoutDataStore.ComponentDataStore>() * components.Length), UnsafeUtility.AlignOf<LayoutDataStore.ComponentDataStore>(), allocator);
			for (int i = 0; i < components.Length; i++)
			{
				this.m_Data->Components[i] = new LayoutDataStore.ComponentDataStore(components[i].Size, allocator);
			}
			this.ResizeCapacity(initialCapacity);
			this.m_Data->NextFreeIndex = 0;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000BBF90 File Offset: 0x000BA190
		public unsafe void Dispose()
		{
			for (int i = 0; i < this.m_Data->ComponentCount; i++)
			{
				this.m_Data->Components[i].Dispose();
			}
			UnsafeUtility.Free((void*)this.m_Data->Versions, this.m_Allocator);
			UnsafeUtility.Free((void*)this.m_Data->Components, this.m_Allocator);
			UnsafeUtility.Free((void*)this.m_Data, this.m_Allocator);
			this.m_Data = null;
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000BC01C File Offset: 0x000BA21C
		public unsafe bool Exists(in LayoutHandle handle)
		{
			bool flag = (ulong)handle.Index >= (ulong)((long)this.m_Data->Capacity);
			return !flag && this.m_Data->Versions[handle.Index] == handle.Version;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000BC06C File Offset: 0x000BA26C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe readonly void* GetComponentDataPtr(int index, int componentIndex)
		{
			return (void*)this.m_Data->Components[componentIndex].GetComponentDataPtr(index);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000BC09C File Offset: 0x000BA29C
		private unsafe LayoutHandle Allocate(byte** data, int count)
		{
			int nextFreeIndex = this.m_Data->NextFreeIndex;
			int num = LayoutDataStore.GetNextFreeIndex(this.m_Data->Components, nextFreeIndex);
			bool flag = num == -1;
			if (flag)
			{
				this.IncreaseCapacity();
				num = LayoutDataStore.GetNextFreeIndex(this.m_Data->Components, nextFreeIndex);
			}
			int num2 = this.m_Data->Versions[nextFreeIndex];
			this.m_Data->NextFreeIndex = num;
			Debug.Assert(this.m_Data->ComponentCount == count, "All components must be initialized");
			Debug.Assert(data != null);
			for (int i = 0; i < count; i++)
			{
				Debug.Assert(*(IntPtr*)(data + (IntPtr)i * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) != (IntPtr)((UIntPtr)0));
				byte* componentDataPtr = this.m_Data->Components[i].GetComponentDataPtr(nextFreeIndex);
				UnsafeUtility.MemCpy((void*)componentDataPtr, *(IntPtr*)(data + (IntPtr)i * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)), (long)this.m_Data->Components[i].Size);
			}
			return new LayoutHandle(nextFreeIndex, num2);
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000BC1C0 File Offset: 0x000BA3C0
		public unsafe void Free(in LayoutHandle handle)
		{
			bool flag = !this.Exists(in handle);
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Failed to Free handle with Index={0} Version={1}", handle.Index, handle.Version));
			}
			this.m_Data->Versions[handle.Index]++;
			LayoutDataStore.SetNextFreeIndex(this.m_Data->Components, handle.Index, this.m_Data->NextFreeIndex);
			this.m_Data->NextFreeIndex = handle.Index;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000BC24F File Offset: 0x000BA44F
		private unsafe static void SetNextFreeIndex(LayoutDataStore.ComponentDataStore* ptr, int index, int value)
		{
			*(int*)ptr->GetComponentDataPtr(index) = value;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000BC25C File Offset: 0x000BA45C
		private unsafe static int GetNextFreeIndex(LayoutDataStore.ComponentDataStore* ptr, int index)
		{
			return *(int*)ptr->GetComponentDataPtr(index);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000BC276 File Offset: 0x000BA476
		private unsafe void IncreaseCapacity()
		{
			this.ResizeCapacity((int)((float)this.m_Data->Capacity * 1.5f));
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000BC294 File Offset: 0x000BA494
		private unsafe void ResizeCapacity(int capacity)
		{
			Assert.IsTrue(capacity > 0);
			this.m_Data->Versions = (int*)LayoutDataStore.ResizeArray((void*)this.m_Data->Versions, (long)this.m_Data->Capacity, (long)capacity, 4L, 4, this.m_Allocator);
			for (int i = 0; i < this.m_Data->ComponentCount; i++)
			{
				this.m_Data->Components[i].ResizeCapacity(capacity);
			}
			int num = ((this.m_Data->Capacity > 0) ? (this.m_Data->Capacity - 1) : 0);
			for (int j = num; j < capacity; j++)
			{
				this.m_Data->Versions[j] = 1;
				LayoutDataStore.SetNextFreeIndex(this.m_Data->Components, j, j + 1);
			}
			LayoutDataStore.SetNextFreeIndex(this.m_Data->Components, capacity - 1, -1);
			this.m_Data->Capacity = capacity;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000BC390 File Offset: 0x000BA590
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

		// Token: 0x060031E4 RID: 12772 RVA: 0x000BC3FC File Offset: 0x000BA5FC
		public unsafe LayoutHandle Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T0>(in T0 component0) where T0 : struct, ValueType
		{
			checked
			{
				fixed (T0* ptr = &component0)
				{
					T0* ptr2 = ptr;
					byte** ptr3 = stackalloc byte*[unchecked((UIntPtr)1) * (UIntPtr)sizeof(byte*)];
					*(IntPtr*)ptr3 = ptr2;
					return this.Allocate(ptr3, 1);
				}
			}
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000BC42C File Offset: 0x000BA62C
		public unsafe LayoutHandle Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2>(in T0 component0, in T1 component1, in T2 component2) where T0 : struct, ValueType where T1 : struct, ValueType where T2 : struct, ValueType
		{
			fixed (T0* ptr = &component0)
			{
				T0* ptr2 = ptr;
				fixed (T1* ptr3 = &component1)
				{
					T1* ptr4 = ptr3;
					fixed (T2* ptr5 = &component2)
					{
						T2* ptr6 = ptr5;
						byte** ptr7;
						checked
						{
							ptr7 = stackalloc byte*[unchecked((UIntPtr)3) * (UIntPtr)sizeof(byte*)];
							*(IntPtr*)ptr7 = ptr2;
						}
						*(IntPtr*)(ptr7 + sizeof(byte*) / sizeof(byte*)) = ptr4;
						*(IntPtr*)(ptr7 + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr6;
						return this.Allocate(ptr7, 3);
					}
				}
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000BC488 File Offset: 0x000BA688
		public unsafe LayoutHandle Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(in T0 component0, in T1 component1, in T2 component2, in T3 component3) where T0 : struct, ValueType where T1 : struct, ValueType where T2 : struct, ValueType where T3 : struct, ValueType
		{
			fixed (T0* ptr = &component0)
			{
				T0* ptr2 = ptr;
				fixed (T1* ptr3 = &component1)
				{
					T1* ptr4 = ptr3;
					fixed (T2* ptr5 = &component2)
					{
						T2* ptr6 = ptr5;
						fixed (T3* ptr7 = &component3)
						{
							T3* ptr8 = ptr7;
							byte** ptr9;
							checked
							{
								ptr9 = stackalloc byte*[unchecked((UIntPtr)4) * (UIntPtr)sizeof(byte*)];
								*(IntPtr*)ptr9 = ptr2;
							}
							*(IntPtr*)(ptr9 + sizeof(byte*) / sizeof(byte*)) = ptr4;
							*(IntPtr*)(ptr9 + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr6;
							*(IntPtr*)(ptr9 + (IntPtr)3 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr8;
							return this.Allocate(ptr9, 4);
						}
					}
				}
			}
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000BC4FC File Offset: 0x000BA6FC
		public unsafe LayoutHandle Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4>(in T0 component0, in T1 component1, in T2 component2, in T3 component3, in T4 component4) where T0 : struct, ValueType where T1 : struct, ValueType where T2 : struct, ValueType where T3 : struct, ValueType where T4 : struct, ValueType
		{
			fixed (T0* ptr = &component0)
			{
				T0* ptr2 = ptr;
				fixed (T1* ptr3 = &component1)
				{
					T1* ptr4 = ptr3;
					fixed (T2* ptr5 = &component2)
					{
						T2* ptr6 = ptr5;
						fixed (T3* ptr7 = &component3)
						{
							T3* ptr8 = ptr7;
							fixed (T4* ptr9 = &component4)
							{
								T4* ptr10 = ptr9;
								byte** ptr11;
								checked
								{
									ptr11 = stackalloc byte*[unchecked((UIntPtr)5) * (UIntPtr)sizeof(byte*)];
									*(IntPtr*)ptr11 = ptr2;
								}
								*(IntPtr*)(ptr11 + sizeof(byte*) / sizeof(byte*)) = ptr4;
								*(IntPtr*)(ptr11 + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr6;
								*(IntPtr*)(ptr11 + (IntPtr)3 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr8;
								*(IntPtr*)(ptr11 + (IntPtr)4 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr10;
								return this.Allocate(ptr11, 5);
							}
						}
					}
				}
			}
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000BC588 File Offset: 0x000BA788
		public unsafe LayoutHandle Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4, [global::System.Runtime.CompilerServices.IsUnmanaged] T5>(in T0 component0, in T1 component1, in T2 component2, in T3 component3, in T4 component4, in T5 component5) where T0 : struct, ValueType where T1 : struct, ValueType where T2 : struct, ValueType where T3 : struct, ValueType where T4 : struct, ValueType where T5 : struct, ValueType
		{
			fixed (T0* ptr = &component0)
			{
				T0* ptr2 = ptr;
				fixed (T1* ptr3 = &component1)
				{
					T1* ptr4 = ptr3;
					fixed (T2* ptr5 = &component2)
					{
						T2* ptr6 = ptr5;
						fixed (T3* ptr7 = &component3)
						{
							T3* ptr8 = ptr7;
							fixed (T4* ptr9 = &component4)
							{
								T4* ptr10 = ptr9;
								fixed (T5* ptr11 = &component5)
								{
									T5* ptr12 = ptr11;
									byte** ptr13;
									checked
									{
										ptr13 = stackalloc byte*[unchecked((UIntPtr)6) * (UIntPtr)sizeof(byte*)];
										*(IntPtr*)ptr13 = ptr2;
									}
									*(IntPtr*)(ptr13 + sizeof(byte*) / sizeof(byte*)) = ptr4;
									*(IntPtr*)(ptr13 + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr6;
									*(IntPtr*)(ptr13 + (IntPtr)3 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr8;
									*(IntPtr*)(ptr13 + (IntPtr)4 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr10;
									*(IntPtr*)(ptr13 + (IntPtr)5 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr12;
									return this.Allocate(ptr13, 6);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000BC62C File Offset: 0x000BA82C
		public unsafe LayoutHandle Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3, [global::System.Runtime.CompilerServices.IsUnmanaged] T4, [global::System.Runtime.CompilerServices.IsUnmanaged] T5, [global::System.Runtime.CompilerServices.IsUnmanaged] T6>(in T0 component0, in T1 component1, in T2 component2, in T3 component3, in T4 component4, in T5 component5, in T6 component6) where T0 : struct, ValueType where T1 : struct, ValueType where T2 : struct, ValueType where T3 : struct, ValueType where T4 : struct, ValueType where T5 : struct, ValueType where T6 : struct, ValueType
		{
			fixed (T0* ptr = &component0)
			{
				T0* ptr2 = ptr;
				fixed (T1* ptr3 = &component1)
				{
					T1* ptr4 = ptr3;
					fixed (T2* ptr5 = &component2)
					{
						T2* ptr6 = ptr5;
						fixed (T3* ptr7 = &component3)
						{
							T3* ptr8 = ptr7;
							fixed (T4* ptr9 = &component4)
							{
								T4* ptr10 = ptr9;
								fixed (T5* ptr11 = &component5)
								{
									T5* ptr12 = ptr11;
									fixed (T6* ptr13 = &component6)
									{
										T6* ptr14 = ptr13;
										byte** ptr15;
										checked
										{
											ptr15 = stackalloc byte*[unchecked((UIntPtr)7) * (UIntPtr)sizeof(byte*)];
											*(IntPtr*)ptr15 = ptr2;
										}
										*(IntPtr*)(ptr15 + sizeof(byte*) / sizeof(byte*)) = ptr4;
										*(IntPtr*)(ptr15 + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr6;
										*(IntPtr*)(ptr15 + (IntPtr)3 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr8;
										*(IntPtr*)(ptr15 + (IntPtr)4 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr10;
										*(IntPtr*)(ptr15 + (IntPtr)5 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr12;
										*(IntPtr*)(ptr15 + (IntPtr)6 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr14;
										return this.Allocate(ptr15, 7);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04001862 RID: 6242
		private const int k_ChunkSize = 32768;

		// Token: 0x04001863 RID: 6243
		private readonly Allocator m_Allocator;

		// Token: 0x04001864 RID: 6244
		[NativeDisableUnsafePtrRestriction]
		private unsafe LayoutDataStore.Data* m_Data;

		// Token: 0x020006A7 RID: 1703
		private struct Chunk
		{
			// Token: 0x04001865 RID: 6245
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* Buffer;
		}

		// Token: 0x020006A8 RID: 1704
		private struct ComponentDataStore : IDisposable
		{
			// Token: 0x060031EA RID: 12778 RVA: 0x000BC6E7 File Offset: 0x000BA8E7
			public ComponentDataStore(int size, Allocator allocator)
			{
				this.Allocator = allocator;
				this.Size = size;
				this.ComponentCountPerChunk = 32768 / size;
				this.ChunkCount = 0;
				this.m_Chunks = null;
			}

			// Token: 0x060031EB RID: 12779 RVA: 0x000BC714 File Offset: 0x000BA914
			public unsafe void Dispose()
			{
				bool flag = null == this.m_Chunks;
				if (!flag)
				{
					for (int i = 0; i < this.ChunkCount; i++)
					{
						UnsafeUtility.Free((void*)this.m_Chunks[i].Buffer, this.Allocator);
					}
					UnsafeUtility.Free((void*)this.m_Chunks, this.Allocator);
					this.ChunkCount = 0;
					this.m_Chunks = null;
				}
			}

			// Token: 0x060031EC RID: 12780 RVA: 0x000BC78C File Offset: 0x000BA98C
			public unsafe byte* GetComponentDataPtr(int index)
			{
				int num = index / this.ComponentCountPerChunk;
				int num2 = index % this.ComponentCountPerChunk;
				return this.m_Chunks[num].Buffer + num2 * this.Size;
			}

			// Token: 0x060031ED RID: 12781 RVA: 0x000BC7D0 File Offset: 0x000BA9D0
			public unsafe void ResizeCapacity(int capacity)
			{
				int num = capacity / this.ComponentCountPerChunk + 1;
				bool flag = num > this.ChunkCount;
				if (flag)
				{
					this.m_Chunks = (LayoutDataStore.Chunk*)LayoutDataStore.ResizeArray((void*)this.m_Chunks, (long)this.ChunkCount, (long)num, (long)UnsafeUtility.SizeOf<LayoutDataStore.Chunk>(), UnsafeUtility.AlignOf<LayoutDataStore.Chunk>(), this.Allocator);
					for (int i = this.ChunkCount; i < num; i++)
					{
						this.m_Chunks[i] = new LayoutDataStore.Chunk
						{
							Buffer = (byte*)UnsafeUtility.Malloc(32768L, 4, this.Allocator)
						};
					}
				}
				else
				{
					bool flag2 = num < this.ChunkCount;
					if (flag2)
					{
						for (int j = this.ChunkCount - 1; j >= num; j--)
						{
							UnsafeUtility.Free((void*)this.m_Chunks[j].Buffer, this.Allocator);
						}
						this.m_Chunks = (LayoutDataStore.Chunk*)LayoutDataStore.ResizeArray((void*)this.m_Chunks, (long)this.ChunkCount, (long)num, (long)UnsafeUtility.SizeOf<LayoutDataStore.Chunk>(), UnsafeUtility.AlignOf<LayoutDataStore.Chunk>(), this.Allocator);
					}
				}
				this.ChunkCount = num;
			}

			// Token: 0x04001866 RID: 6246
			public Allocator Allocator;

			// Token: 0x04001867 RID: 6247
			public int Size;

			// Token: 0x04001868 RID: 6248
			public int ComponentCountPerChunk;

			// Token: 0x04001869 RID: 6249
			public int ChunkCount;

			// Token: 0x0400186A RID: 6250
			[NativeDisableUnsafePtrRestriction]
			private unsafe LayoutDataStore.Chunk* m_Chunks;
		}

		// Token: 0x020006A9 RID: 1705
		private struct Data
		{
			// Token: 0x0400186B RID: 6251
			public int Capacity;

			// Token: 0x0400186C RID: 6252
			public int NextFreeIndex;

			// Token: 0x0400186D RID: 6253
			public int ComponentCount;

			// Token: 0x0400186E RID: 6254
			[NativeDisableUnsafePtrRestriction]
			public unsafe int* Versions;

			// Token: 0x0400186F RID: 6255
			[NativeDisableUnsafePtrRestriction]
			public unsafe LayoutDataStore.ComponentDataStore* Components;
		}
	}
}
