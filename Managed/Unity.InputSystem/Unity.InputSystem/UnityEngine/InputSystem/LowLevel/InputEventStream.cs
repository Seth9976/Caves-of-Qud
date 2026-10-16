using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EB RID: 235
	internal struct InputEventStream
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x000466F9 File Offset: 0x000448F9
		public bool isOpen
		{
			get
			{
				return this.m_IsOpen;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00046701 File Offset: 0x00044901
		public int remainingEventCount
		{
			get
			{
				return this.m_RemainingNativeEventCount + this.m_RemainingAppendEventCount;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00046710 File Offset: 0x00044910
		public int numEventsRetainedInBuffer
		{
			get
			{
				return this.m_NumEventsRetainedInBuffer;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00046718 File Offset: 0x00044918
		public unsafe InputEvent* currentEventPtr
		{
			get
			{
				if (this.m_RemainingNativeEventCount > 0)
				{
					return this.m_CurrentNativeEventReadPtr;
				}
				if (this.m_RemainingAppendEventCount <= 0)
				{
					return null;
				}
				return this.m_CurrentAppendEventReadPtr;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0004673C File Offset: 0x0004493C
		public unsafe uint numBytesRetainedInBuffer
		{
			get
			{
				return (uint)((long)((byte*)this.m_CurrentNativeEventWritePtr - (byte*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_NativeBuffer.data)));
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0004675C File Offset: 0x0004495C
		public unsafe InputEventStream(ref InputEventBuffer eventBuffer, int maxAppendedEvents)
		{
			this.m_CurrentNativeEventWritePtr = (this.m_CurrentNativeEventReadPtr = (InputEvent*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(eventBuffer.data));
			this.m_NativeBuffer = eventBuffer;
			this.m_RemainingNativeEventCount = this.m_NativeBuffer.eventCount;
			this.m_NumEventsRetainedInBuffer = 0;
			this.m_CurrentAppendEventReadPtr = (this.m_CurrentAppendEventWritePtr = null);
			this.m_AppendBuffer = default(InputEventBuffer);
			this.m_RemainingAppendEventCount = 0;
			this.m_MaxAppendedEvents = maxAppendedEvents;
			this.m_IsOpen = true;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000467DC File Offset: 0x000449DC
		public unsafe void Close(ref InputEventBuffer eventBuffer)
		{
			if (this.m_NumEventsRetainedInBuffer > 0)
			{
				void* unsafeBufferPointerWithoutChecks = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_NativeBuffer.data);
				long num = (long)((byte*)this.m_CurrentNativeEventWritePtr - (byte*)unsafeBufferPointerWithoutChecks);
				this.m_NativeBuffer = new InputEventBuffer((InputEvent*)unsafeBufferPointerWithoutChecks, this.m_NumEventsRetainedInBuffer, (int)num, (int)this.m_NativeBuffer.capacityInBytes);
			}
			else
			{
				this.m_NativeBuffer.Reset();
			}
			if (this.m_AppendBuffer.data.IsCreated)
			{
				this.m_AppendBuffer.Dispose();
			}
			eventBuffer = this.m_NativeBuffer;
			this.m_IsOpen = false;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00046870 File Offset: 0x00044A70
		public void CleanUpAfterException()
		{
			if (!this.isOpen)
			{
				return;
			}
			this.m_NativeBuffer.Reset();
			if (this.m_AppendBuffer.data.IsCreated)
			{
				this.m_AppendBuffer.Dispose();
			}
			this.m_IsOpen = false;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000468B8 File Offset: 0x00044AB8
		public unsafe void Write(InputEvent* eventPtr)
		{
			if (this.m_AppendBuffer.eventCount >= this.m_MaxAppendedEvents)
			{
				Debug.LogError("Maximum number of queued events exceeded. Set the 'maxQueuedEventsPerUpdate' setting to a higher value if you need to queue more events than this. " + string.Format("Current limit is '{0}'.", this.m_MaxAppendedEvents));
				return;
			}
			bool isCreated = this.m_AppendBuffer.data.IsCreated;
			byte* data = (byte*)this.m_AppendBuffer.bufferPtr.data;
			this.m_AppendBuffer.AppendEvent(eventPtr, 2048, Allocator.Temp);
			if (!isCreated)
			{
				this.m_CurrentAppendEventWritePtr = (this.m_CurrentAppendEventReadPtr = (InputEvent*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_AppendBuffer.data));
			}
			else
			{
				byte* data2 = (byte*)this.m_AppendBuffer.bufferPtr.data;
				if (data != data2)
				{
					long num = (long)((byte*)this.m_CurrentAppendEventWritePtr - (byte*)data);
					long num2 = (long)((byte*)this.m_CurrentAppendEventReadPtr - (byte*)data);
					this.m_CurrentAppendEventWritePtr = (InputEvent*)(data2 + num);
					this.m_CurrentAppendEventReadPtr = (InputEvent*)(data2 + num2);
				}
			}
			this.m_RemainingAppendEventCount++;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000469B4 File Offset: 0x00044BB4
		public unsafe InputEvent* Advance(bool leaveEventInBuffer)
		{
			if (this.m_RemainingNativeEventCount > 0)
			{
				this.m_NativeBuffer.AdvanceToNextEvent(ref this.m_CurrentNativeEventReadPtr, ref this.m_CurrentNativeEventWritePtr, ref this.m_NumEventsRetainedInBuffer, ref this.m_RemainingNativeEventCount, leaveEventInBuffer);
			}
			else if (this.m_RemainingAppendEventCount > 0)
			{
				int num = 0;
				this.m_AppendBuffer.AdvanceToNextEvent(ref this.m_CurrentAppendEventReadPtr, ref this.m_CurrentAppendEventWritePtr, ref num, ref this.m_RemainingAppendEventCount, false);
			}
			return this.currentEventPtr;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00046A24 File Offset: 0x00044C24
		public unsafe InputEvent* Peek()
		{
			if (this.m_RemainingNativeEventCount > 1)
			{
				return InputEvent.GetNextInMemory(this.m_CurrentNativeEventReadPtr);
			}
			if (this.m_RemainingNativeEventCount == 1)
			{
				if (this.m_RemainingAppendEventCount <= 0)
				{
					return null;
				}
				return this.m_CurrentAppendEventReadPtr;
			}
			else
			{
				if (this.m_RemainingAppendEventCount > 1)
				{
					return InputEvent.GetNextInMemory(this.m_CurrentAppendEventReadPtr);
				}
				return null;
			}
		}

		// Token: 0x040005C6 RID: 1478
		private InputEventBuffer m_NativeBuffer;

		// Token: 0x040005C7 RID: 1479
		private unsafe InputEvent* m_CurrentNativeEventReadPtr;

		// Token: 0x040005C8 RID: 1480
		private unsafe InputEvent* m_CurrentNativeEventWritePtr;

		// Token: 0x040005C9 RID: 1481
		private int m_RemainingNativeEventCount;

		// Token: 0x040005CA RID: 1482
		private readonly int m_MaxAppendedEvents;

		// Token: 0x040005CB RID: 1483
		private InputEventBuffer m_AppendBuffer;

		// Token: 0x040005CC RID: 1484
		private unsafe InputEvent* m_CurrentAppendEventReadPtr;

		// Token: 0x040005CD RID: 1485
		private unsafe InputEvent* m_CurrentAppendEventWritePtr;

		// Token: 0x040005CE RID: 1486
		private int m_RemainingAppendEventCount;

		// Token: 0x040005CF RID: 1487
		private int m_NumEventsRetainedInBuffer;

		// Token: 0x040005D0 RID: 1488
		private bool m_IsOpen;
	}
}
