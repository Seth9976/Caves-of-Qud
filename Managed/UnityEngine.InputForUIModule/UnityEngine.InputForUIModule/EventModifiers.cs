using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200000D RID: 13
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct EventModifiers
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002508 File Offset: 0x00000708
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPressed(EventModifiers.Modifiers mod)
		{
			return (this._state & (uint)mod) > 0U;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002515 File Offset: 0x00000715
		public bool isShiftPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Shift);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000251E File Offset: 0x0000071E
		public bool isCtrlPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Ctrl);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002528 File Offset: 0x00000728
		public bool isAltPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Alt);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002532 File Offset: 0x00000732
		public bool isMetaPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Meta);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000253F File Offset: 0x0000073F
		public bool isCapsLockEnabled
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.CapsLock);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000254C File Offset: 0x0000074C
		public bool isNumLockEnabled
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Numlock);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002559 File Offset: 0x00000759
		public bool isFunctionKeyPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.FunctionKey);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002566 File Offset: 0x00000766
		public bool isNumericPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Numeric);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002574 File Offset: 0x00000774
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetPressed(EventModifiers.Modifiers modifier, bool pressed)
		{
			if (pressed)
			{
				this._state |= (uint)modifier;
			}
			else
			{
				this._state &= (uint)(~(uint)modifier);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025A6 File Offset: 0x000007A6
		public void Reset()
		{
			this._state = 0U;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025B0 File Offset: 0x000007B0
		private static void Append(ref string str, string value)
		{
			str = (string.IsNullOrEmpty(str) ? value : (str + "," + value));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025D0 File Offset: 0x000007D0
		public override string ToString()
		{
			string empty = string.Empty;
			bool flag = this.IsPressed(EventModifiers.Modifiers.LeftShift);
			if (flag)
			{
				EventModifiers.Append(ref empty, "LeftShift");
			}
			bool flag2 = this.IsPressed(EventModifiers.Modifiers.RightShift);
			if (flag2)
			{
				EventModifiers.Append(ref empty, "RightShift");
			}
			bool flag3 = this.IsPressed(EventModifiers.Modifiers.LeftCtrl);
			if (flag3)
			{
				EventModifiers.Append(ref empty, "LeftCtrl");
			}
			bool flag4 = this.IsPressed(EventModifiers.Modifiers.RightCtrl);
			if (flag4)
			{
				EventModifiers.Append(ref empty, "RightCtrl");
			}
			bool flag5 = this.IsPressed(EventModifiers.Modifiers.LeftAlt);
			if (flag5)
			{
				EventModifiers.Append(ref empty, "LeftAlt");
			}
			bool flag6 = this.IsPressed(EventModifiers.Modifiers.RightAlt);
			if (flag6)
			{
				EventModifiers.Append(ref empty, "RightAlt");
			}
			bool flag7 = this.IsPressed(EventModifiers.Modifiers.LeftMeta);
			if (flag7)
			{
				EventModifiers.Append(ref empty, "LeftMeta");
			}
			bool flag8 = this.IsPressed(EventModifiers.Modifiers.RightMeta);
			if (flag8)
			{
				EventModifiers.Append(ref empty, "RightMeta");
			}
			bool flag9 = this.IsPressed(EventModifiers.Modifiers.CapsLock);
			if (flag9)
			{
				EventModifiers.Append(ref empty, "CapsLock");
			}
			bool flag10 = this.IsPressed(EventModifiers.Modifiers.Numlock);
			if (flag10)
			{
				EventModifiers.Append(ref empty, "Numlock");
			}
			bool flag11 = this.IsPressed(EventModifiers.Modifiers.FunctionKey);
			if (flag11)
			{
				EventModifiers.Append(ref empty, "FunctionKey");
			}
			bool flag12 = this.IsPressed(EventModifiers.Modifiers.Numeric);
			if (flag12)
			{
				EventModifiers.Append(ref empty, "Numeric");
			}
			return empty;
		}

		// Token: 0x04000035 RID: 53
		private uint _state;

		// Token: 0x0200000E RID: 14
		[Flags]
		public enum Modifiers : uint
		{
			// Token: 0x04000037 RID: 55
			LeftShift = 1U,
			// Token: 0x04000038 RID: 56
			RightShift = 2U,
			// Token: 0x04000039 RID: 57
			Shift = 3U,
			// Token: 0x0400003A RID: 58
			LeftCtrl = 4U,
			// Token: 0x0400003B RID: 59
			RightCtrl = 8U,
			// Token: 0x0400003C RID: 60
			Ctrl = 12U,
			// Token: 0x0400003D RID: 61
			LeftAlt = 16U,
			// Token: 0x0400003E RID: 62
			RightAlt = 32U,
			// Token: 0x0400003F RID: 63
			Alt = 48U,
			// Token: 0x04000040 RID: 64
			LeftMeta = 64U,
			// Token: 0x04000041 RID: 65
			RightMeta = 128U,
			// Token: 0x04000042 RID: 66
			Meta = 192U,
			// Token: 0x04000043 RID: 67
			CapsLock = 256U,
			// Token: 0x04000044 RID: 68
			Numlock = 512U,
			// Token: 0x04000045 RID: 69
			FunctionKey = 1024U,
			// Token: 0x04000046 RID: 70
			Numeric = 2048U
		}
	}
}
