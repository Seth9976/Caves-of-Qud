using System;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000002 RID: 2
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct CommandEvent : IEventProperties
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public DiscreteTime timestamp { readonly get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public EventSource eventSource { readonly get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public uint playerId { readonly get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002094 File Offset: 0x00000294
		public override string ToString()
		{
			return string.Format("{0} {1}", this.type, this.command);
		}

		// Token: 0x04000001 RID: 1
		public CommandEvent.Type type;

		// Token: 0x04000002 RID: 2
		public CommandEvent.Command command;

		// Token: 0x02000003 RID: 3
		public enum Type
		{
			// Token: 0x04000008 RID: 8
			Validate = 1,
			// Token: 0x04000009 RID: 9
			Execute
		}

		// Token: 0x02000004 RID: 4
		public enum Command
		{
			// Token: 0x0400000B RID: 11
			Invalid,
			// Token: 0x0400000C RID: 12
			Cut,
			// Token: 0x0400000D RID: 13
			Copy,
			// Token: 0x0400000E RID: 14
			Paste,
			// Token: 0x0400000F RID: 15
			SelectAll,
			// Token: 0x04000010 RID: 16
			DeselectAll,
			// Token: 0x04000011 RID: 17
			InvertSelection,
			// Token: 0x04000012 RID: 18
			Duplicate,
			// Token: 0x04000013 RID: 19
			Rename,
			// Token: 0x04000014 RID: 20
			Delete,
			// Token: 0x04000015 RID: 21
			SoftDelete,
			// Token: 0x04000016 RID: 22
			Find,
			// Token: 0x04000017 RID: 23
			SelectChildren,
			// Token: 0x04000018 RID: 24
			SelectPrefabRoot,
			// Token: 0x04000019 RID: 25
			UndoRedoPerformed,
			// Token: 0x0400001A RID: 26
			OnLostFocus,
			// Token: 0x0400001B RID: 27
			NewKeyboardFocus,
			// Token: 0x0400001C RID: 28
			ModifierKeysChanged,
			// Token: 0x0400001D RID: 29
			EyeDropperUpdate,
			// Token: 0x0400001E RID: 30
			EyeDropperClicked,
			// Token: 0x0400001F RID: 31
			EyeDropperCancelled,
			// Token: 0x04000020 RID: 32
			ColorPickerChanged,
			// Token: 0x04000021 RID: 33
			FrameSelected,
			// Token: 0x04000022 RID: 34
			FrameSelectedWithLock
		}
	}
}
