using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BC RID: 188
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct QueryDimensionsCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00044A7D File Offset: 0x00042C7D
		public static FourCC Type
		{
			get
			{
				return new FourCC('D', 'I', 'M', 'S');
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00044A8C File Offset: 0x00042C8C
		public FourCC typeStatic
		{
			get
			{
				return QueryDimensionsCommand.Type;
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00044A94 File Offset: 0x00042C94
		public static QueryDimensionsCommand Create()
		{
			return new QueryDimensionsCommand
			{
				baseCommand = new InputDeviceCommand(QueryDimensionsCommand.Type, 16)
			};
		}

		// Token: 0x04000511 RID: 1297
		internal const int kSize = 16;

		// Token: 0x04000512 RID: 1298
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000513 RID: 1299
		[FieldOffset(8)]
		public Vector2 outDimensions;
	}
}
