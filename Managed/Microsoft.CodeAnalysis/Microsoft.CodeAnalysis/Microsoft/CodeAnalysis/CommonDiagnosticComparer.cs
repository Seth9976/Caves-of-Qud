using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200016B RID: 363
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CommonDiagnosticComparer : IEqualityComparer<Diagnostic>
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x0002C632 File Offset: 0x0002A832
		private CommonDiagnosticComparer()
		{
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0002C63A File Offset: 0x0002A83A
		[NullableContext(2)]
		public bool Equals(Diagnostic x, Diagnostic y)
		{
			return x == y || (x != null && y != null && x.Location == y.Location && x.Id == y.Id);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0002C670 File Offset: 0x0002A870
		public int GetHashCode(Diagnostic obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return Hash.Combine<Location>(obj.Location, obj.Id.GetHashCode());
		}

		// Token: 0x04000423 RID: 1059
		internal static readonly CommonDiagnosticComparer Instance = new CommonDiagnosticComparer();
	}
}
