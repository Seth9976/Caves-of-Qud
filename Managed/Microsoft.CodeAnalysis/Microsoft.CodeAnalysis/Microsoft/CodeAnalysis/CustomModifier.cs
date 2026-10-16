using System;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000237 RID: 567
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CustomModifier : ICustomModifier
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001601 RID: 5633
		public abstract bool IsOptional { get; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001602 RID: 5634
		public abstract INamedTypeSymbol Modifier { get; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x0004552F File Offset: 0x0004372F
		bool ICustomModifier.IsOptional
		{
			get
			{
				return this.IsOptional;
			}
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00045537 File Offset: 0x00043737
		ITypeReference ICustomModifier.GetModifier(EmitContext context)
		{
			throw new NotImplementedException();
		}
	}
}
