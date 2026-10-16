using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C5 RID: 197
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct SetIMECursorPositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00044EBC File Offset: 0x000430BC
		public static FourCC Type
		{
			get
			{
				return new FourCC('I', 'M', 'E', 'P');
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00044ECB File Offset: 0x000430CB
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00044ED3 File Offset: 0x000430D3
		public FourCC typeStatic
		{
			get
			{
				return SetIMECursorPositionCommand.Type;
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00044EDC File Offset: 0x000430DC
		public static SetIMECursorPositionCommand Create(Vector2 cursorPosition)
		{
			return new SetIMECursorPositionCommand
			{
				baseCommand = new InputDeviceCommand(SetIMECursorPositionCommand.Type, 16),
				m_Position = cursorPosition
			};
		}

		// Token: 0x04000531 RID: 1329
		internal const int kSize = 16;

		// Token: 0x04000532 RID: 1330
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000533 RID: 1331
		[FieldOffset(8)]
		private Vector2 m_Position;
	}
}
