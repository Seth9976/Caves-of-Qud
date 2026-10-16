using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000355 RID: 853
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SuppressionInfo
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x00071CE3 File Offset: 0x0006FEE3
		public string Id { get; }

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x00071CEB File Offset: 0x0006FEEB
		[Nullable(2)]
		public AttributeData Attribute
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x00071CF3 File Offset: 0x0006FEF3
		internal SuppressionInfo(string id, [Nullable(2)] AttributeData attribute)
		{
			this.Id = id;
			this.Attribute = attribute;
		}
	}
}
