using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E4 RID: 228
	[StructLayout(LayoutKind.Explicit, Size = 152)]
	public struct IMECompositionEvent : IInputEventTypeInfo
	{
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0004599A File Offset: 0x00043B9A
		public FourCC typeStatic
		{
			get
			{
				return 1229800787;
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000459A8 File Offset: 0x00043BA8
		public static IMECompositionEvent Create(int deviceId, string compositionString, double time)
		{
			return new IMECompositionEvent
			{
				baseEvent = new InputEvent(1229800787, 152, deviceId, time),
				compositionString = new IMECompositionString(compositionString)
			};
		}

		// Token: 0x040005B0 RID: 1456
		internal const int kIMECharBufferSize = 64;

		// Token: 0x040005B1 RID: 1457
		public const int Type = 1229800787;

		// Token: 0x040005B2 RID: 1458
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x040005B3 RID: 1459
		[FieldOffset(20)]
		public IMECompositionString compositionString;
	}
}
