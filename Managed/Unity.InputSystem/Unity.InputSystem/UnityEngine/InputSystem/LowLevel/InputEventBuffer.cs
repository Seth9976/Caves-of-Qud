using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E7 RID: 231
	public struct InputEventBuffer : IEnumerable<InputEventPtr>, IEnumerable, IDisposable, ICloneable
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00045DCB File Offset: 0x00043FCB
		public int eventCount
		{
			get
			{
				return this.m_EventCount;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00045DD3 File Offset: 0x00043FD3
		public long sizeInBytes
		{
			get
			{
				return this.m_SizeInBytes;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00045DDB File Offset: 0x00043FDB
		public long capacityInBytes
		{
			get
			{
				if (!this.m_Buffer.IsCreated)
				{
					return 0L;
				}
				return (long)this.m_Buffer.Length;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00045DF9 File Offset: 0x00043FF9
		public NativeArray<byte> data
		{
			get
			{
				return this.m_Buffer;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00045E01 File Offset: 0x00044001
		public unsafe InputEventPtr bufferPtr
		{
			get
			{
				return (InputEvent*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_Buffer);
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00045E14 File Offset: 0x00044014
		public unsafe InputEventBuffer(InputEvent* eventPtr, int eventCount, int sizeInBytes = -1, int capacityInBytes = -1)
		{
			this = default(InputEventBuffer);
			if (eventPtr == null && eventCount != 0)
			{
				throw new ArgumentException("eventPtr is NULL but eventCount is != 0", "eventCount");
			}
			if (capacityInBytes != 0 && capacityInBytes < sizeInBytes)
			{
				throw new ArgumentException(string.Format("capacity({0}) cannot be smaller than size({1})", capacityInBytes, sizeInBytes), "capacityInBytes");
			}
			if (eventPtr != null)
			{
				if (capacityInBytes < 0)
				{
					capacityInBytes = sizeInBytes;
				}
				this.m_Buffer = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)eventPtr, (capacityInBytes > 0) ? capacityInBytes : 0, Allocator.None);
				this.m_SizeInBytes = ((sizeInBytes >= 0) ? ((long)sizeInBytes) : (-1L));
				this.m_EventCount = eventCount;
				this.m_WeOwnTheBuffer = false;
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00045EB0 File Offset: 0x000440B0
		public InputEventBuffer(NativeArray<byte> buffer, int eventCount, int sizeInBytes = -1, bool transferNativeArrayOwnership = false)
		{
			if (eventCount > 0 && !buffer.IsCreated)
			{
				throw new ArgumentException("buffer has no data but eventCount is > 0", "eventCount");
			}
			if (sizeInBytes > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("sizeInBytes");
			}
			this.m_Buffer = buffer;
			this.m_WeOwnTheBuffer = transferNativeArrayOwnership;
			this.m_SizeInBytes = (long)((sizeInBytes >= 0) ? sizeInBytes : buffer.Length);
			this.m_EventCount = eventCount;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00045F1C File Offset: 0x0004411C
		public unsafe void AppendEvent(InputEvent* eventPtr, int capacityIncrementInBytes = 2048, Allocator allocator = Allocator.Persistent)
		{
			if (eventPtr == null)
			{
				throw new ArgumentNullException("eventPtr");
			}
			uint sizeInBytes = eventPtr->sizeInBytes;
			UnsafeUtility.MemCpy((void*)this.AllocateEvent((int)sizeInBytes, capacityIncrementInBytes, allocator), (void*)eventPtr, (long)((ulong)sizeInBytes));
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00045F54 File Offset: 0x00044154
		public unsafe InputEvent* AllocateEvent(int sizeInBytes, int capacityIncrementInBytes = 2048, Allocator allocator = Allocator.Persistent)
		{
			if (sizeInBytes < 20)
			{
				throw new ArgumentException(string.Format("sizeInBytes must be >= sizeof(InputEvent) == {0} (was {1})", 20, sizeInBytes), "sizeInBytes");
			}
			int num = sizeInBytes.AlignToMultipleOf(4);
			long num2 = this.m_SizeInBytes + (long)num;
			if (this.capacityInBytes < num2)
			{
				long num3 = num2.AlignToMultipleOf((long)capacityIncrementInBytes);
				if (num3 > 2147483647L)
				{
					throw new NotImplementedException("NativeArray long support");
				}
				NativeArray<byte> nativeArray = new NativeArray<byte>((int)num3, allocator, NativeArrayOptions.ClearMemory);
				if (this.m_Buffer.IsCreated)
				{
					UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<byte>(), NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_Buffer), this.sizeInBytes);
					if (this.m_WeOwnTheBuffer)
					{
						this.m_Buffer.Dispose();
					}
				}
				this.m_Buffer = nativeArray;
				this.m_WeOwnTheBuffer = true;
			}
			InputEvent* ptr = (InputEvent*)((byte*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_Buffer) + this.m_SizeInBytes);
			ptr->sizeInBytes = (uint)sizeInBytes;
			this.m_SizeInBytes += (long)num;
			this.m_EventCount++;
			return ptr;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00046050 File Offset: 0x00044250
		public unsafe bool Contains(InputEvent* eventPtr)
		{
			if (eventPtr == null)
			{
				return false;
			}
			if (this.sizeInBytes == 0L)
			{
				return false;
			}
			void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.data);
			return eventPtr >= (InputEvent*)unsafeBufferPointerWithoutChecks && (this.sizeInBytes == -1L || eventPtr < (InputEvent*)((byte*)unsafeBufferPointerWithoutChecks + this.sizeInBytes));
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00046099 File Offset: 0x00044299
		public void Reset()
		{
			this.m_EventCount = 0;
			if (this.m_SizeInBytes != -1L)
			{
				this.m_SizeInBytes = 0L;
			}
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000460B4 File Offset: 0x000442B4
		internal unsafe void AdvanceToNextEvent(ref InputEvent* currentReadPos, ref InputEvent* currentWritePos, ref int numEventsRetainedInBuffer, ref int numRemainingEvents, bool leaveEventInBuffer)
		{
			InputEvent* ptr = currentReadPos;
			if (numRemainingEvents > 1)
			{
				ptr = InputEvent.GetNextInMemory(currentReadPos);
			}
			if (leaveEventInBuffer)
			{
				uint sizeInBytes = currentReadPos.sizeInBytes;
				if (currentReadPos != currentWritePos)
				{
					UnsafeUtility.MemMove(currentWritePos, currentReadPos, (long)((ulong)sizeInBytes));
				}
				currentWritePos += (IntPtr)((UIntPtr)sizeInBytes.AlignToMultipleOf(4U));
				numEventsRetainedInBuffer++;
			}
			currentReadPos = ptr;
			numRemainingEvents--;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0004610D File Offset: 0x0004430D
		public IEnumerator<InputEventPtr> GetEnumerator()
		{
			return new InputEventBuffer.Enumerator(this);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0004611F File Offset: 0x0004431F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00046127 File Offset: 0x00044327
		public void Dispose()
		{
			if (!this.m_WeOwnTheBuffer)
			{
				return;
			}
			this.m_Buffer.Dispose();
			this.m_WeOwnTheBuffer = false;
			this.m_SizeInBytes = 0L;
			this.m_EventCount = 0;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00046154 File Offset: 0x00044354
		public InputEventBuffer Clone()
		{
			InputEventBuffer inputEventBuffer = default(InputEventBuffer);
			if (this.m_Buffer.IsCreated)
			{
				inputEventBuffer.m_Buffer = new NativeArray<byte>(this.m_Buffer.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				inputEventBuffer.m_Buffer.CopyFrom(this.m_Buffer);
				inputEventBuffer.m_WeOwnTheBuffer = true;
			}
			inputEventBuffer.m_SizeInBytes = this.m_SizeInBytes;
			inputEventBuffer.m_EventCount = this.m_EventCount;
			return inputEventBuffer;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000461C4 File Offset: 0x000443C4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x040005BC RID: 1468
		public const long BufferSizeUnknown = -1L;

		// Token: 0x040005BD RID: 1469
		private NativeArray<byte> m_Buffer;

		// Token: 0x040005BE RID: 1470
		private long m_SizeInBytes;

		// Token: 0x040005BF RID: 1471
		private int m_EventCount;

		// Token: 0x040005C0 RID: 1472
		private bool m_WeOwnTheBuffer;

		// Token: 0x02000216 RID: 534
		private struct Enumerator : IEnumerator<InputEventPtr>, IEnumerator, IDisposable
		{
			// Token: 0x06001515 RID: 5397 RVA: 0x00062ABC File Offset: 0x00060CBC
			public Enumerator(InputEventBuffer buffer)
			{
				this.m_Buffer = buffer.bufferPtr;
				this.m_EventCount = buffer.m_EventCount;
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001516 RID: 5398 RVA: 0x00062AEC File Offset: 0x00060CEC
			public bool MoveNext()
			{
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				if (this.m_CurrentEvent == null)
				{
					this.m_CurrentEvent = this.m_Buffer;
					return this.m_CurrentEvent != null;
				}
				this.m_CurrentIndex++;
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				this.m_CurrentEvent = InputEvent.GetNextInMemory(this.m_CurrentEvent);
				return true;
			}

			// Token: 0x06001517 RID: 5399 RVA: 0x00062B5D File Offset: 0x00060D5D
			public void Reset()
			{
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001518 RID: 5400 RVA: 0x00062B6E File Offset: 0x00060D6E
			public void Dispose()
			{
			}

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x06001519 RID: 5401 RVA: 0x00062B70 File Offset: 0x00060D70
			public InputEventPtr Current
			{
				get
				{
					return this.m_CurrentEvent;
				}
			}

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x0600151A RID: 5402 RVA: 0x00062B7D File Offset: 0x00060D7D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000BA5 RID: 2981
			private unsafe readonly InputEvent* m_Buffer;

			// Token: 0x04000BA6 RID: 2982
			private readonly int m_EventCount;

			// Token: 0x04000BA7 RID: 2983
			private unsafe InputEvent* m_CurrentEvent;

			// Token: 0x04000BA8 RID: 2984
			private int m_CurrentIndex;
		}
	}
}
