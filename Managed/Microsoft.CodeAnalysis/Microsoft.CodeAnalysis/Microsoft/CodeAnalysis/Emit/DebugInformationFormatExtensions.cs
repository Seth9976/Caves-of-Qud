using System;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200047E RID: 1150
	internal static class DebugInformationFormatExtensions
	{
		// Token: 0x06002D17 RID: 11543 RVA: 0x0007E88E File Offset: 0x0007CA8E
		internal static bool IsValid(this DebugInformationFormat value)
		{
			return value >= DebugInformationFormat.Pdb && value <= DebugInformationFormat.Embedded;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x0007E89D File Offset: 0x0007CA9D
		internal static bool IsPortable(this DebugInformationFormat value)
		{
			return value == DebugInformationFormat.PortablePdb || value == DebugInformationFormat.Embedded;
		}
	}
}
