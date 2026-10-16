using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000176 RID: 374
	public class ColorGamutUtility
	{
		// Token: 0x06000D9D RID: 3485
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ColorPrimaries GetColorPrimaries(ColorGamut gamut);

		// Token: 0x06000D9E RID: 3486
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern WhitePoint GetWhitePoint(ColorGamut gamut);

		// Token: 0x06000D9F RID: 3487
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern TransferFunction GetTransferFunction(ColorGamut gamut);
	}
}
