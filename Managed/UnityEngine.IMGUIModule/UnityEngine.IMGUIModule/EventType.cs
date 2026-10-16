using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	public enum EventType
	{
		// Token: 0x0400001E RID: 30
		MouseDown,
		// Token: 0x0400001F RID: 31
		MouseUp,
		// Token: 0x04000020 RID: 32
		MouseMove,
		// Token: 0x04000021 RID: 33
		MouseDrag,
		// Token: 0x04000022 RID: 34
		KeyDown,
		// Token: 0x04000023 RID: 35
		KeyUp,
		// Token: 0x04000024 RID: 36
		ScrollWheel,
		// Token: 0x04000025 RID: 37
		Repaint,
		// Token: 0x04000026 RID: 38
		Layout,
		// Token: 0x04000027 RID: 39
		DragUpdated,
		// Token: 0x04000028 RID: 40
		DragPerform,
		// Token: 0x04000029 RID: 41
		DragExited = 15,
		// Token: 0x0400002A RID: 42
		Ignore = 11,
		// Token: 0x0400002B RID: 43
		Used,
		// Token: 0x0400002C RID: 44
		ValidateCommand,
		// Token: 0x0400002D RID: 45
		ExecuteCommand,
		// Token: 0x0400002E RID: 46
		ContextClick = 16,
		// Token: 0x0400002F RID: 47
		MouseEnterWindow = 20,
		// Token: 0x04000030 RID: 48
		MouseLeaveWindow,
		// Token: 0x04000031 RID: 49
		TouchDown = 30,
		// Token: 0x04000032 RID: 50
		TouchUp,
		// Token: 0x04000033 RID: 51
		TouchMove,
		// Token: 0x04000034 RID: 52
		TouchEnter,
		// Token: 0x04000035 RID: 53
		TouchLeave,
		// Token: 0x04000036 RID: 54
		TouchStationary,
		// Token: 0x04000037 RID: 55
		[Obsolete("Use MouseDown instead (UnityUpgradable) -> MouseDown", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		mouseDown = 0,
		// Token: 0x04000038 RID: 56
		[Obsolete("Use MouseUp instead (UnityUpgradable) -> MouseUp", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		mouseUp,
		// Token: 0x04000039 RID: 57
		[Obsolete("Use MouseMove instead (UnityUpgradable) -> MouseMove", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		mouseMove,
		// Token: 0x0400003A RID: 58
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use MouseDrag instead (UnityUpgradable) -> MouseDrag", true)]
		mouseDrag,
		// Token: 0x0400003B RID: 59
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use KeyDown instead (UnityUpgradable) -> KeyDown", true)]
		keyDown,
		// Token: 0x0400003C RID: 60
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use KeyUp instead (UnityUpgradable) -> KeyUp", true)]
		keyUp,
		// Token: 0x0400003D RID: 61
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ScrollWheel instead (UnityUpgradable) -> ScrollWheel", true)]
		scrollWheel,
		// Token: 0x0400003E RID: 62
		[Obsolete("Use Repaint instead (UnityUpgradable) -> Repaint", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		repaint,
		// Token: 0x0400003F RID: 63
		[Obsolete("Use Layout instead (UnityUpgradable) -> Layout", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		layout,
		// Token: 0x04000040 RID: 64
		[Obsolete("Use DragUpdated instead (UnityUpgradable) -> DragUpdated", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		dragUpdated,
		// Token: 0x04000041 RID: 65
		[Obsolete("Use DragPerform instead (UnityUpgradable) -> DragPerform", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		dragPerform,
		// Token: 0x04000042 RID: 66
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Ignore instead (UnityUpgradable) -> Ignore", true)]
		ignore,
		// Token: 0x04000043 RID: 67
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Used instead (UnityUpgradable) -> Used", true)]
		used
	}
}
