using System;
using System.Runtime.CompilerServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200001A RID: 26
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct PointerEvent : IEventProperties
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002BE3 File Offset: 0x00000DE3
		public bool isPrimaryPointer
		{
			get
			{
				return this.pointerIndex == 0;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002BEE File Offset: 0x00000DEE
		public float azimuth
		{
			get
			{
				return InputManagerProvider.TiltToAzimuth(this.tilt);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002BFB File Offset: 0x00000DFB
		public float altitude
		{
			get
			{
				return InputManagerProvider.TiltToAltitude(this.tilt);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002C08 File Offset: 0x00000E08
		public bool isPressed
		{
			get
			{
				return this.buttonsState.Get(this.isInverted ? PointerEvent.Button.PenEraserInTouch : PointerEvent.Button.Primary);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002C21 File Offset: 0x00000E21
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002C29 File Offset: 0x00000E29
		public DiscreteTime timestamp { readonly get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002C32 File Offset: 0x00000E32
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002C3A File Offset: 0x00000E3A
		public EventSource eventSource { readonly get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002C43 File Offset: 0x00000E43
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002C4B File Offset: 0x00000E4B
		public uint playerId { readonly get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002C54 File Offset: 0x00000E54
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002C5C File Offset: 0x00000E5C
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x00002C68 File Offset: 0x00000E68
		public override string ToString()
		{
			string text = ((this.eventSource == EventSource.Pen) ? string.Format(" tilt:({0:f1},{1:f1}) az:{2:f2} al:{3:f2} twist:{4} pressure:{5} isInverted:{6}", new object[]
			{
				this.tilt.x,
				this.tilt.y,
				this.azimuth,
				this.altitude,
				this.twist,
				this.pressure,
				this.isInverted ? 1 : 0
			}) : "");
			string text2 = ((this.eventSource == EventSource.Touch) ? string.Format(" finger:{0} tilt:({1:f1},{2:f1}) twist:{3} pressure:{4}", new object[]
			{
				this.pointerIndex,
				this.tilt.x,
				this.tilt.y,
				this.twist,
				this.pressure
			}) : "");
			string text3 = string.Format(" dsp:{0}", this.displayIndex);
			string text4 = text + text2 + text3;
			string text5;
			switch (this.type)
			{
			case PointerEvent.Type.PointerMoved:
				text5 = string.Format("{0} pos:{1} dlt:{2} btns:{3}{4}", new object[] { this.type, this.position, this.deltaPosition, this.buttonsState, text4 });
				break;
			case PointerEvent.Type.Scroll:
				text5 = string.Format("{0} pos:{1} scr:{2}{3}", new object[] { this.type, this.position, this.scroll, text4 });
				break;
			case PointerEvent.Type.ButtonPressed:
			case PointerEvent.Type.ButtonReleased:
				text5 = string.Format("{0} pos:{1} btn:{2} btns:{3} clk:{4}{5}", new object[] { this.type, this.position, this.button, this.buttonsState, this.clickCount, text4 });
				break;
			case PointerEvent.Type.State:
				text5 = string.Format("{0} pos:{1} btns:{2}{3}", new object[] { this.type, this.position, this.buttonsState, text4 });
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return text5;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F0C File Offset: 0x0000110C
		internal static PointerEvent.Button ButtonFromButtonIndex(int index)
		{
			return (PointerEvent.Button)((index <= 31) ? (1 << index) : 0);
		}

		// Token: 0x0400007C RID: 124
		public PointerEvent.Type type;

		// Token: 0x0400007D RID: 125
		public int pointerIndex;

		// Token: 0x0400007E RID: 126
		public Vector2 position;

		// Token: 0x0400007F RID: 127
		public Vector2 deltaPosition;

		// Token: 0x04000080 RID: 128
		public Vector2 scroll;

		// Token: 0x04000081 RID: 129
		public int displayIndex;

		// Token: 0x04000082 RID: 130
		public Vector2 tilt;

		// Token: 0x04000083 RID: 131
		public float twist;

		// Token: 0x04000084 RID: 132
		public float pressure;

		// Token: 0x04000085 RID: 133
		public bool isInverted;

		// Token: 0x04000086 RID: 134
		public PointerEvent.Button button;

		// Token: 0x04000087 RID: 135
		public PointerEvent.ButtonsState buttonsState;

		// Token: 0x04000088 RID: 136
		public int clickCount;

		// Token: 0x0200001B RID: 27
		public enum Type
		{
			// Token: 0x0400008E RID: 142
			PointerMoved = 1,
			// Token: 0x0400008F RID: 143
			Scroll,
			// Token: 0x04000090 RID: 144
			ButtonPressed,
			// Token: 0x04000091 RID: 145
			ButtonReleased,
			// Token: 0x04000092 RID: 146
			State,
			// Token: 0x04000093 RID: 147
			TouchCanceled
		}

		// Token: 0x0200001C RID: 28
		[Flags]
		public enum Button : uint
		{
			// Token: 0x04000095 RID: 149
			None = 0U,
			// Token: 0x04000096 RID: 150
			Primary = 1U,
			// Token: 0x04000097 RID: 151
			FingerInTouch = 1U,
			// Token: 0x04000098 RID: 152
			PenTipInTouch = 1U,
			// Token: 0x04000099 RID: 153
			PenEraserInTouch = 2U,
			// Token: 0x0400009A RID: 154
			PenBarrelButton = 4U,
			// Token: 0x0400009B RID: 155
			MouseLeft = 1U,
			// Token: 0x0400009C RID: 156
			MouseRight = 2U,
			// Token: 0x0400009D RID: 157
			MouseMiddle = 4U,
			// Token: 0x0400009E RID: 158
			MouseForward = 8U,
			// Token: 0x0400009F RID: 159
			MouseBack = 16U
		}

		// Token: 0x0200001D RID: 29
		public struct ButtonsState
		{
			// Token: 0x06000074 RID: 116 RVA: 0x00002F2C File Offset: 0x0000112C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Set(PointerEvent.Button button, bool pressed)
			{
				if (pressed)
				{
					this._state |= (uint)button;
				}
				else
				{
					this._state &= (uint)(~(uint)button);
				}
			}

			// Token: 0x06000075 RID: 117 RVA: 0x00002F60 File Offset: 0x00001160
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool Get(PointerEvent.Button button)
			{
				return (this._state & (uint)button) > 0U;
			}

			// Token: 0x06000076 RID: 118 RVA: 0x00002F7D File Offset: 0x0000117D
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Reset()
			{
				this._state = 0U;
			}

			// Token: 0x06000077 RID: 119 RVA: 0x00002F88 File Offset: 0x00001188
			public override string ToString()
			{
				return string.Format("{0:x2}", this._state);
			}

			// Token: 0x040000A0 RID: 160
			private uint _state;
		}
	}
}
