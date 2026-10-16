using System;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200001E RID: 30
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct TextInputEvent : IEventProperties
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002FAF File Offset: 0x000011AF
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002FB7 File Offset: 0x000011B7
		public DiscreteTime timestamp { readonly get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002FC0 File Offset: 0x000011C0
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002FC8 File Offset: 0x000011C8
		public EventSource eventSource { readonly get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002FD1 File Offset: 0x000011D1
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002FD9 File Offset: 0x000011D9
		public uint playerId { readonly get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002FE2 File Offset: 0x000011E2
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002FEA File Offset: 0x000011EA
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00002FF4 File Offset: 0x000011F4
		public override string ToString()
		{
			string text = ((this.character == '\0') ? string.Empty : this.character.ToString());
			return string.Format("text input 0x{0:x8} '{1}'", (int)this.character, text);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003038 File Offset: 0x00001238
		public static bool ShouldBeProcessed(char character)
		{
			return character > '\u001f' && character != '\u007f';
		}

		// Token: 0x040000A1 RID: 161
		public char character;
	}
}
