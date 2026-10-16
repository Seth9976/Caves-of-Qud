using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200018A RID: 394
	internal static class DocumentationModeEnumBounds
	{
		// Token: 0x06001031 RID: 4145 RVA: 0x0002F355 File Offset: 0x0002D555
		internal static bool IsValid(this DocumentationMode value)
		{
			return value >= DocumentationMode.None && value <= DocumentationMode.Diagnose;
		}
	}
}
