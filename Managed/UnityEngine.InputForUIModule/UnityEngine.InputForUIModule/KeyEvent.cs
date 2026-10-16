using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000012 RID: 18
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct KeyEvent : IEventProperties
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000279F File Offset: 0x0000099F
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000027A7 File Offset: 0x000009A7
		public DiscreteTime timestamp { readonly get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000027B0 File Offset: 0x000009B0
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000027B8 File Offset: 0x000009B8
		public EventSource eventSource { readonly get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000027C1 File Offset: 0x000009C1
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000027C9 File Offset: 0x000009C9
		public uint playerId { readonly get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000027D2 File Offset: 0x000009D2
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000027DA File Offset: 0x000009DA
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x0600004A RID: 74 RVA: 0x000027E4 File Offset: 0x000009E4
		public override string ToString()
		{
			KeyEvent.Type type = this.type;
			KeyEvent.Type type2 = type;
			string text;
			if (type2 - KeyEvent.Type.KeyPressed > 2)
			{
				if (type2 != KeyEvent.Type.State)
				{
					throw new ArgumentOutOfRangeException();
				}
				text = string.Format("{0} Pressed:{1}", this.type, this.buttonsState);
			}
			else
			{
				text = string.Format("{0} {1}", this.type, this.keyCode);
			}
			return text;
		}

		// Token: 0x04000053 RID: 83
		public KeyEvent.Type type;

		// Token: 0x04000054 RID: 84
		public KeyCode keyCode;

		// Token: 0x04000055 RID: 85
		public KeyEvent.ButtonsState buttonsState;

		// Token: 0x02000013 RID: 19
		public enum Type
		{
			// Token: 0x0400005B RID: 91
			KeyPressed = 1,
			// Token: 0x0400005C RID: 92
			KeyRepeated,
			// Token: 0x0400005D RID: 93
			KeyReleased,
			// Token: 0x0400005E RID: 94
			State
		}

		// Token: 0x02000014 RID: 20
		public struct ButtonsState
		{
			// Token: 0x0600004B RID: 75 RVA: 0x00002858 File Offset: 0x00000A58
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal static bool ShouldBeProcessed(KeyCode keyCode)
			{
				return keyCode <= KeyCode.Menu;
			}

			// Token: 0x0600004C RID: 76 RVA: 0x00002878 File Offset: 0x00000A78
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private unsafe bool GetUnchecked(uint index)
			{
				return (*((ref this.buttons.FixedElementField) + (UIntPtr)(index >> 3)) & (byte)(1 << (int)(index & 7U))) > 0;
			}

			// Token: 0x0600004D RID: 77 RVA: 0x000028A8 File Offset: 0x00000AA8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void SetUnchecked(uint index)
			{
				ref byte ptr = (ref this.buttons.FixedElementField) + (UIntPtr)(index >> 3);
				ptr |= (byte)(1 << (int)(index & 7U));
			}

			// Token: 0x0600004E RID: 78 RVA: 0x000028C9 File Offset: 0x00000AC9
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void ClearUnchecked(uint index)
			{
				ref byte ptr = (ref this.buttons.FixedElementField) + (UIntPtr)(index >> 3);
				ptr &= (byte)(~(byte)(1 << (int)(index & 7U)));
			}

			// Token: 0x0600004F RID: 79 RVA: 0x000028EC File Offset: 0x00000AEC
			public bool IsPressed(KeyCode keyCode)
			{
				return KeyEvent.ButtonsState.ShouldBeProcessed(keyCode) && this.GetUnchecked((uint)keyCode);
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00002910 File Offset: 0x00000B10
			public IEnumerable<KeyCode> GetAllPressed()
			{
				uint num;
				for (uint index = 0U; index <= 319U; index = num)
				{
					bool @unchecked = this.GetUnchecked(index);
					if (@unchecked)
					{
						yield return (KeyCode)index;
					}
					num = index + 1U;
				}
				yield break;
			}

			// Token: 0x06000051 RID: 81 RVA: 0x00002928 File Offset: 0x00000B28
			public void SetPressed(KeyCode keyCode, bool pressed)
			{
				bool flag = !KeyEvent.ButtonsState.ShouldBeProcessed(keyCode);
				if (!flag)
				{
					if (pressed)
					{
						this.SetUnchecked((uint)keyCode);
					}
					else
					{
						this.ClearUnchecked((uint)keyCode);
					}
				}
			}

			// Token: 0x06000052 RID: 82 RVA: 0x0000295C File Offset: 0x00000B5C
			public unsafe void Reset()
			{
				int num = 0;
				while ((long)num < 40L)
				{
					*((ref this.buttons.FixedElementField) + num) = 0;
					num++;
				}
			}

			// Token: 0x06000053 RID: 83 RVA: 0x0000298C File Offset: 0x00000B8C
			public override string ToString()
			{
				return string.Join<KeyCode>(",", this.GetAllPressed());
			}

			// Token: 0x0400005F RID: 95
			private const uint kMaxIndex = 319U;

			// Token: 0x04000060 RID: 96
			private const uint kSizeInBytes = 40U;

			// Token: 0x04000061 RID: 97
			[FixedBuffer(typeof(byte), 40)]
			private KeyEvent.ButtonsState.<buttons>e__FixedBuffer buttons;

			// Token: 0x02000016 RID: 22
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, Size = 40)]
			public struct <buttons>e__FixedBuffer
			{
				// Token: 0x04000068 RID: 104
				public byte FixedElementField;
			}
		}
	}
}
