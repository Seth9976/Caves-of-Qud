using System;
using Trivial.Mono.Cecil.Metadata;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000B2 RID: 178
	public sealed class SentinelType : TypeSpecification
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x00002B65 File Offset: 0x00000D65
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsSentinel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00015E1E File Offset: 0x0001401E
		public SentinelType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Trivial.Mono.Cecil.Metadata.ElementType.Sentinel;
		}
	}
}
