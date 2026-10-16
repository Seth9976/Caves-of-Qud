using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DE RID: 222
	[StructLayout(LayoutKind.Explicit, Size = 37)]
	internal struct ActionEvent : IInputEventTypeInfo
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0004553E File Offset: 0x0004373E
		public static FourCC Type
		{
			get
			{
				return new FourCC('A', 'C', 'T', 'N');
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0004554D File Offset: 0x0004374D
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x00045555 File Offset: 0x00043755
		public double startTime
		{
			get
			{
				return this.m_StartTime;
			}
			set
			{
				this.m_StartTime = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0004555E File Offset: 0x0004375E
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x00045566 File Offset: 0x00043766
		public InputActionPhase phase
		{
			get
			{
				return (InputActionPhase)this.m_Phase;
			}
			set
			{
				this.m_Phase = (byte)value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00045570 File Offset: 0x00043770
		public unsafe byte* valueData
		{
			get
			{
				fixed (byte* ptr = &this.m_ValueData.FixedElementField)
				{
					return ptr;
				}
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0004558B File Offset: 0x0004378B
		public int valueSizeInBytes
		{
			get
			{
				return (int)(this.baseEvent.sizeInBytes - 20U - 16U);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0004559E File Offset: 0x0004379E
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x000455A6 File Offset: 0x000437A6
		public int stateIndex
		{
			get
			{
				return (int)this.m_StateIndex;
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw new NotSupportedException("State count cannot exceed byte.MaxValue");
				}
				this.m_StateIndex = (byte)value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x000455C7 File Offset: 0x000437C7
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x000455CF File Offset: 0x000437CF
		public int controlIndex
		{
			get
			{
				return (int)this.m_ControlIndex;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new NotSupportedException("Control count cannot exceed ushort.MaxValue");
				}
				this.m_ControlIndex = (ushort)value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x000455F0 File Offset: 0x000437F0
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x000455F8 File Offset: 0x000437F8
		public int bindingIndex
		{
			get
			{
				return (int)this.m_BindingIndex;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new NotSupportedException("Binding count cannot exceed ushort.MaxValue");
				}
				this.m_BindingIndex = (ushort)value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00045619 File Offset: 0x00043819
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00045630 File Offset: 0x00043830
		public int interactionIndex
		{
			get
			{
				if (this.m_InteractionIndex == 65535)
				{
					return -1;
				}
				return (int)this.m_InteractionIndex;
			}
			set
			{
				if (value == -1)
				{
					this.m_InteractionIndex = ushort.MaxValue;
					return;
				}
				if (value < 0 || value >= 65535)
				{
					throw new NotSupportedException("Interaction count cannot exceed ushort.MaxValue-1");
				}
				this.m_InteractionIndex = (ushort)value;
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00045664 File Offset: 0x00043864
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (ActionEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0004567A File Offset: 0x0004387A
		public FourCC typeStatic
		{
			get
			{
				return ActionEvent.Type;
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00045681 File Offset: 0x00043881
		public static int GetEventSizeWithValueSize(int valueSizeInBytes)
		{
			return 36 + valueSizeInBytes;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00045688 File Offset: 0x00043888
		public unsafe static ActionEvent* From(InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new ArgumentNullException("ptr");
			}
			if (!ptr.IsA<ActionEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into ActionEvent", ptr.type));
			}
			return (ActionEvent*)ptr.data;
		}

		// Token: 0x0400059C RID: 1436
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x0400059D RID: 1437
		[FieldOffset(20)]
		private ushort m_ControlIndex;

		// Token: 0x0400059E RID: 1438
		[FieldOffset(22)]
		private ushort m_BindingIndex;

		// Token: 0x0400059F RID: 1439
		[FieldOffset(24)]
		private ushort m_InteractionIndex;

		// Token: 0x040005A0 RID: 1440
		[FieldOffset(26)]
		private byte m_StateIndex;

		// Token: 0x040005A1 RID: 1441
		[FieldOffset(27)]
		private byte m_Phase;

		// Token: 0x040005A2 RID: 1442
		[FieldOffset(28)]
		private double m_StartTime;

		// Token: 0x040005A3 RID: 1443
		[FixedBuffer(typeof(byte), 1)]
		[FieldOffset(36)]
		public ActionEvent.<m_ValueData>e__FixedBuffer m_ValueData;

		// Token: 0x02000212 RID: 530
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <m_ValueData>e__FixedBuffer
		{
			// Token: 0x04000B9F RID: 2975
			public byte FixedElementField;
		}
	}
}
