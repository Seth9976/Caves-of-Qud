using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000199 RID: 409
	internal static class ThreeStateHelpers
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x000301D8 File Offset: 0x0002E3D8
		public static ThreeState ToThreeState(this bool value)
		{
			if (!value)
			{
				return ThreeState.False;
			}
			return ThreeState.True;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x000301E0 File Offset: 0x0002E3E0
		public static bool HasValue(this ThreeState value)
		{
			return value > ThreeState.Unknown;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000301E6 File Offset: 0x0002E3E6
		public static bool Value(this ThreeState value)
		{
			return value == ThreeState.True;
		}
	}
}
