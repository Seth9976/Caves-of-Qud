using System;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C2 RID: 1218
	internal struct AnonymousTypeField
	{
		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x060067F1 RID: 26609 RVA: 0x0018739B File Offset: 0x0018559B
		public TypeSymbol Type
		{
			get
			{
				return this.TypeWithAnnotations.Type;
			}
		}

		// Token: 0x060067F2 RID: 26610 RVA: 0x001873A8 File Offset: 0x001855A8
		public AnonymousTypeField(string name, Location location, TypeWithAnnotations typeWithAnnotations)
		{
			this.Name = name;
			this.Location = location;
			this.TypeWithAnnotations = typeWithAnnotations;
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x001873BF File Offset: 0x001855BF
		[Conditional("DEBUG")]
		internal void AssertIsGood()
		{
		}

		// Token: 0x040019CA RID: 6602
		public readonly string Name;

		// Token: 0x040019CB RID: 6603
		public readonly Location Location;

		// Token: 0x040019CC RID: 6604
		public readonly TypeWithAnnotations TypeWithAnnotations;
	}
}
