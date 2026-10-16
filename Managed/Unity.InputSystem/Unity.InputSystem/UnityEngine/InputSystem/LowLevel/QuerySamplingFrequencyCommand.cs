using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C1 RID: 193
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	internal struct QuerySamplingFrequencyCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00044D8C File Offset: 0x00042F8C
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'M', 'P', 'L');
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00044D9B File Offset: 0x00042F9B
		public FourCC typeStatic
		{
			get
			{
				return QuerySamplingFrequencyCommand.Type;
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00044DA4 File Offset: 0x00042FA4
		public static QuerySamplingFrequencyCommand Create()
		{
			return new QuerySamplingFrequencyCommand
			{
				baseCommand = new InputDeviceCommand(QuerySamplingFrequencyCommand.Type, 12)
			};
		}

		// Token: 0x04000526 RID: 1318
		internal const int kSize = 12;

		// Token: 0x04000527 RID: 1319
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000528 RID: 1320
		[FieldOffset(8)]
		public float frequency;
	}
}
