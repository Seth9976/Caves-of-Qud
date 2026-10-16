using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001C0 RID: 448
	[NullableContext(2)]
	[Nullable(0)]
	internal readonly struct ReferenceDirective
	{
		// Token: 0x060012D8 RID: 4824 RVA: 0x00039DA6 File Offset: 0x00037FA6
		[NullableContext(1)]
		public ReferenceDirective(string file, Location location)
		{
			this.File = file;
			this.Location = location;
		}

		// Token: 0x0400059D RID: 1437
		public readonly string File;

		// Token: 0x0400059E RID: 1438
		public readonly Location Location;
	}
}
