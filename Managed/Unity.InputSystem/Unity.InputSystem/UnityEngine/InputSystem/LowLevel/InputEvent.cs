using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;
using UnityEngineInternal.Input;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E6 RID: 230
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 20)]
	public struct InputEvent
	{
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00045AC1 File Offset: 0x00043CC1
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x00045AD3 File Offset: 0x00043CD3
		public FourCC type
		{
			get
			{
				return new FourCC((int)this.m_Event.type);
			}
			set
			{
				this.m_Event.type = (NativeInputEventType)value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00045AE6 File Offset: 0x00043CE6
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00045AF4 File Offset: 0x00043CF4
		public uint sizeInBytes
		{
			get
			{
				return (uint)this.m_Event.sizeInBytes;
			}
			set
			{
				if (value > 65535U)
				{
					throw new ArgumentException("Maximum event size is " + ushort.MaxValue.ToString(), "value");
				}
				this.m_Event.sizeInBytes = (ushort)value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00045B38 File Offset: 0x00043D38
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00045B4E File Offset: 0x00043D4E
		public int eventId
		{
			get
			{
				return (int)((long)this.m_Event.eventId & 2147483647L);
			}
			set
			{
				this.m_Event.eventId = value | (int)((long)this.m_Event.eventId & (long)((ulong)int.MinValue));
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00045B71 File Offset: 0x00043D71
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x00045B7E File Offset: 0x00043D7E
		public int deviceId
		{
			get
			{
				return (int)this.m_Event.deviceId;
			}
			set
			{
				this.m_Event.deviceId = (ushort)value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00045B8D File Offset: 0x00043D8D
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x00045BA0 File Offset: 0x00043DA0
		public double time
		{
			get
			{
				return this.m_Event.time - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			set
			{
				this.m_Event.time = value + InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00045BB4 File Offset: 0x00043DB4
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x00045BC1 File Offset: 0x00043DC1
		internal double internalTime
		{
			get
			{
				return this.m_Event.time;
			}
			set
			{
				this.m_Event.time = value;
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00045BD0 File Offset: 0x00043DD0
		public InputEvent(FourCC type, int sizeInBytes, int deviceId, double time = -1.0)
		{
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			this.m_Event.type = (NativeInputEventType)type;
			this.m_Event.sizeInBytes = (ushort)sizeInBytes;
			this.m_Event.deviceId = (ushort)deviceId;
			this.m_Event.time = time;
			this.m_Event.eventId = 0;
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00045C3A File Offset: 0x00043E3A
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x00045C58 File Offset: 0x00043E58
		public bool handled
		{
			get
			{
				return ((long)this.m_Event.eventId & (long)((ulong)int.MinValue)) == (long)((ulong)int.MinValue);
			}
			set
			{
				if (value)
				{
					this.m_Event.eventId = (int)((long)this.m_Event.eventId | (long)((ulong)int.MinValue));
					return;
				}
				this.m_Event.eventId = (int)((long)this.m_Event.eventId & 2147483647L);
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00045CA8 File Offset: 0x00043EA8
		public override string ToString()
		{
			return string.Format("id={0} type={1} device={2} size={3} time={4}", new object[] { this.eventId, this.type, this.deviceId, this.sizeInBytes, this.time });
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00045D0C File Offset: 0x00043F0C
		internal unsafe static InputEvent* GetNextInMemory(InputEvent* currentPtr)
		{
			uint num = currentPtr->sizeInBytes.AlignToMultipleOf(4U);
			return currentPtr + num / (uint)sizeof(InputEvent);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00045D2C File Offset: 0x00043F2C
		internal unsafe static InputEvent* GetNextInMemoryChecked(InputEvent* currentPtr, ref InputEventBuffer buffer)
		{
			uint num = currentPtr->sizeInBytes.AlignToMultipleOf(4U);
			InputEvent* ptr = currentPtr + num / (uint)sizeof(InputEvent);
			if (!buffer.Contains(ptr))
			{
				throw new InvalidOperationException(string.Format("Event '{0}' is last event in given buffer with size {1}", new InputEventPtr(currentPtr), buffer.sizeInBytes));
			}
			return ptr;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00045D7C File Offset: 0x00043F7C
		public unsafe static bool Equals(InputEvent* first, InputEvent* second)
		{
			return first == second || (first != null && second != null && first->m_Event.sizeInBytes == second->m_Event.sizeInBytes && UnsafeUtility.MemCmp((void*)first, (void*)second, (long)((ulong)first->m_Event.sizeInBytes)) == 0);
		}

		// Token: 0x040005B6 RID: 1462
		private const uint kHandledMask = 2147483648U;

		// Token: 0x040005B7 RID: 1463
		private const uint kIdMask = 2147483647U;

		// Token: 0x040005B8 RID: 1464
		internal const int kBaseEventSize = 20;

		// Token: 0x040005B9 RID: 1465
		public const int InvalidEventId = 0;

		// Token: 0x040005BA RID: 1466
		internal const int kAlignment = 4;

		// Token: 0x040005BB RID: 1467
		[FieldOffset(0)]
		private NativeInputEvent m_Event;
	}
}
