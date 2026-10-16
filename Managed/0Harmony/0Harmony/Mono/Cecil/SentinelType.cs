using System;
using Mono.Cecil.Metadata;

namespace Mono.Cecil
{
	// Token: 0x02000265 RID: 613
	internal sealed class SentinelType : TypeSpecification
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00018318 File Offset: 0x00016518
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x000189AE File Offset: 0x00016BAE
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

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsSentinel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002E373 File Offset: 0x0002C573
		public SentinelType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.Sentinel;
		}
	}
}
