using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004DB RID: 1243
	internal sealed class InstrumentationPayloadRootField : SynthesizedStaticField
	{
		// Token: 0x06003210 RID: 12816 RVA: 0x0008E8A2 File Offset: 0x0008CAA2
		[NullableContext(1)]
		internal InstrumentationPayloadRootField(INamedTypeDefinition containingType, int analysisIndex, ITypeReference payloadType)
			: base("PayloadRoot" + analysisIndex.ToString(), containingType, payloadType)
		{
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x0008E8C0 File Offset: 0x0008CAC0
		public override ImmutableArray<byte> MappedData
		{
			get
			{
				return default(ImmutableArray<byte>);
			}
		}
	}
}
