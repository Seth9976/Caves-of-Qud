using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000104 RID: 260
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ParametersEncoder
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0001B332 File Offset: 0x00019532
		public BlobBuilder Builder { get; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0001B33A File Offset: 0x0001953A
		public bool HasVarArgs { get; }

		/// <param name="builder" />
		/// <param name="hasVarArgs" />
		// Token: 0x060009BE RID: 2494 RVA: 0x0001B342 File Offset: 0x00019542
		public ParametersEncoder(BlobBuilder builder, bool hasVarArgs = false)
		{
			this.Builder = builder;
			this.HasVarArgs = hasVarArgs;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001B352 File Offset: 0x00019552
		public ParameterTypeEncoder AddParameter()
		{
			return new ParameterTypeEncoder(this.Builder);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001B35F File Offset: 0x0001955F
		public ParametersEncoder StartVarArgs()
		{
			if (!this.HasVarArgs)
			{
				Throw.SignatureNotVarArg();
			}
			this.Builder.WriteByte(65);
			return new ParametersEncoder(this.Builder, false);
		}
	}
}
