using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000058 RID: 88
	public static class InputExtensions
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x0002F27B File Offset: 0x0002D47B
		public static bool IsInProgress(this InputActionPhase phase)
		{
			return phase == InputActionPhase.Started || phase == InputActionPhase.Performed;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002F287 File Offset: 0x0002D487
		public static bool IsEndedOrCanceled(this TouchPhase phase)
		{
			return phase == TouchPhase.Canceled || phase == TouchPhase.Ended;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002F293 File Offset: 0x0002D493
		public static bool IsActive(this TouchPhase phase)
		{
			return phase - TouchPhase.Began <= 1 || phase == TouchPhase.Stationary;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002F2A2 File Offset: 0x0002D4A2
		public static bool IsModifierKey(this Key key)
		{
			return key - Key.LeftShift <= 7;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
		public static bool IsTextInputKey(this Key key)
		{
			switch (key)
			{
			case Key.None:
			case Key.Space:
			case Key.Enter:
			case Key.Tab:
			case Key.LeftShift:
			case Key.RightShift:
			case Key.LeftAlt:
			case Key.RightAlt:
			case Key.LeftCtrl:
			case Key.RightCtrl:
			case Key.LeftMeta:
			case Key.RightMeta:
			case Key.ContextMenu:
			case Key.Escape:
			case Key.LeftArrow:
			case Key.RightArrow:
			case Key.UpArrow:
			case Key.DownArrow:
			case Key.Backspace:
			case Key.PageDown:
			case Key.PageUp:
			case Key.Home:
			case Key.End:
			case Key.Insert:
			case Key.Delete:
			case Key.CapsLock:
			case Key.NumLock:
			case Key.PrintScreen:
			case Key.ScrollLock:
			case Key.Pause:
			case Key.NumpadEnter:
			case Key.F1:
			case Key.F2:
			case Key.F3:
			case Key.F4:
			case Key.F5:
			case Key.F6:
			case Key.F7:
			case Key.F8:
			case Key.F9:
			case Key.F10:
			case Key.F11:
			case Key.F12:
			case Key.OEM1:
			case Key.OEM2:
			case Key.OEM3:
			case Key.OEM4:
			case Key.OEM5:
			case Key.IMESelected:
			case Key.F13:
			case Key.F14:
			case Key.F15:
			case Key.F16:
			case Key.F17:
			case Key.F18:
			case Key.F19:
			case Key.F20:
			case Key.F21:
			case Key.F22:
			case Key.F23:
			case Key.F24:
			case Key.MediaPlayPause:
			case Key.MediaRewind:
			case Key.MediaForward:
				return false;
			}
			return true;
		}
	}
}
