using System;
using Unity.IL2CPP.CompilerServices;

namespace UnityEngineInternal
{
	// Token: 0x0200000F RID: 15
	[Il2CppEagerStaticClassConstruction]
	public struct MathfInternal
	{
		// Token: 0x0400001F RID: 31
		public static volatile float FloatMinNormal = 1.1754944E-38f;

		// Token: 0x04000020 RID: 32
		public static volatile float FloatMinDenormal = float.Epsilon;

		// Token: 0x04000021 RID: 33
		public static bool IsFlushToZeroEnabled = MathfInternal.FloatMinDenormal == 0f;
	}
}
