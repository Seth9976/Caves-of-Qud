using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BD RID: 189
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct QueryEnabledStateCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00044ABD File Offset: 0x00042CBD
		public static FourCC Type
		{
			get
			{
				return new FourCC('Q', 'E', 'N', 'B');
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00044ACC File Offset: 0x00042CCC
		public FourCC typeStatic
		{
			get
			{
				return QueryEnabledStateCommand.Type;
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00044AD4 File Offset: 0x00042CD4
		public static QueryEnabledStateCommand Create()
		{
			return new QueryEnabledStateCommand
			{
				baseCommand = new InputDeviceCommand(QueryEnabledStateCommand.Type, 9)
			};
		}

		// Token: 0x04000514 RID: 1300
		internal const int kSize = 9;

		// Token: 0x04000515 RID: 1301
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000516 RID: 1302
		[FieldOffset(8)]
		public bool isEnabled;
	}
}
