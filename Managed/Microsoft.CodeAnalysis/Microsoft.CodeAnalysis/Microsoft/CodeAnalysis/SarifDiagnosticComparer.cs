using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000130 RID: 304
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SarifDiagnosticComparer : IEqualityComparer<DiagnosticDescriptor>
	{
		// Token: 0x06000B57 RID: 2903 RVA: 0x000222BC File Offset: 0x000204BC
		private SarifDiagnosticComparer()
		{
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000222C4 File Offset: 0x000204C4
		[NullableContext(2)]
		public bool Equals(DiagnosticDescriptor x, DiagnosticDescriptor y)
		{
			return x == y || (x != null && y != null && (x.Category == y.Category && x.DefaultSeverity == y.DefaultSeverity && x.Description.Equals(y.Description) && x.HelpLinkUri == y.HelpLinkUri && x.Id == y.Id && x.IsEnabledByDefault == y.IsEnabledByDefault && x.Title.Equals(y.Title)) && Enumerable.SequenceEqual<string>(x.CustomTags, y.CustomTags));
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00022370 File Offset: 0x00020570
		public int GetHashCode(DiagnosticDescriptor obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return Hash.Combine(obj.Category.GetHashCode(), Hash.Combine(obj.DefaultSeverity.GetHashCode(), Hash.Combine(obj.Description.GetHashCode(), Hash.Combine(obj.HelpLinkUri.GetHashCode(), Hash.Combine(obj.Id.GetHashCode(), Hash.Combine(obj.IsEnabledByDefault.GetHashCode(), Hash.Combine(obj.Title.GetHashCode(), Hash.CombineValues<string>(obj.CustomTags, int.MaxValue))))))));
		}

		// Token: 0x04000334 RID: 820
		public static readonly SarifDiagnosticComparer Instance = new SarifDiagnosticComparer();
	}
}
