using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B5 RID: 181
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct EnableIMECompositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0004491C File Offset: 0x00042B1C
		public static FourCC Type
		{
			get
			{
				return new FourCC('I', 'M', 'E', 'M');
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0004492B File Offset: 0x00042B2B
		public bool imeEnabled
		{
			get
			{
				return this.m_ImeEnabled > 0;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00044936 File Offset: 0x00042B36
		public FourCC typeStatic
		{
			get
			{
				return EnableIMECompositionCommand.Type;
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00044940 File Offset: 0x00042B40
		public static EnableIMECompositionCommand Create(bool enabled)
		{
			return new EnableIMECompositionCommand
			{
				baseCommand = new InputDeviceCommand(EnableIMECompositionCommand.Type, 9),
				m_ImeEnabled = (enabled ? byte.MaxValue : 0)
			};
		}

		// Token: 0x04000503 RID: 1283
		internal const int kSize = 12;

		// Token: 0x04000504 RID: 1284
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000505 RID: 1285
		[FieldOffset(8)]
		private byte m_ImeEnabled;
	}
}
