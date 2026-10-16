using System;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000492 RID: 1170
	internal static class InstrumentationKindExtensions
	{
		// Token: 0x06002E2D RID: 11821 RVA: 0x00082E30 File Offset: 0x00081030
		internal static bool IsValid(this InstrumentationKind value)
		{
			return value >= InstrumentationKind.None && value <= InstrumentationKind.TestCoverage;
		}
	}
}
