using System;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000487 RID: 1159
	[NullableContext(1)]
	[Nullable(0)]
	internal struct EncHoistedLocalMetadata
	{
		// Token: 0x06002DA6 RID: 11686 RVA: 0x0008179E File Offset: 0x0007F99E
		public EncHoistedLocalMetadata(string name, ITypeReference type, SynthesizedLocalKind synthesizedKind)
		{
			this.Name = name;
			this.Type = type;
			this.SynthesizedKind = synthesizedKind;
		}

		// Token: 0x0400122C RID: 4652
		public readonly string Name;

		// Token: 0x0400122D RID: 4653
		public readonly ITypeReference Type;

		// Token: 0x0400122E RID: 4654
		public readonly SynthesizedLocalKind SynthesizedKind;
	}
}
