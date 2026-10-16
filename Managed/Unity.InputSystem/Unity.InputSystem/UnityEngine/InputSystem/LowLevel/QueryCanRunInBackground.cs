using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BB RID: 187
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct QueryCanRunInBackground : IInputDeviceCommandInfo
	{
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00044A36 File Offset: 0x00042C36
		public static FourCC Type
		{
			get
			{
				return new FourCC('Q', 'R', 'I', 'B');
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00044A45 File Offset: 0x00042C45
		public FourCC typeStatic
		{
			get
			{
				return QueryCanRunInBackground.Type;
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00044A4C File Offset: 0x00042C4C
		public static QueryCanRunInBackground Create()
		{
			return new QueryCanRunInBackground
			{
				baseCommand = new InputDeviceCommand(QueryCanRunInBackground.Type, 9),
				canRunInBackground = false
			};
		}

		// Token: 0x0400050E RID: 1294
		internal const int kSize = 9;

		// Token: 0x0400050F RID: 1295
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000510 RID: 1296
		[FieldOffset(8)]
		public bool canRunInBackground;
	}
}
