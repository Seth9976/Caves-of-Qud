using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000109 RID: 265
	public struct DeconstructionInfo
	{
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00074688 File Offset: 0x00072888
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public IMethodSymbol Method
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				if (this._conversion.Kind != ConversionKind.Deconstruction)
				{
					return null;
				}
				return this._conversion.MethodSymbol;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x000746B8 File Offset: 0x000728B8
		public Conversion? Conversion
		{
			get
			{
				if (this._conversion.Kind != ConversionKind.Deconstruction)
				{
					return new Conversion?(this._conversion);
				}
				return default(Conversion?);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x000746EC File Offset: 0x000728EC
		public ImmutableArray<DeconstructionInfo> Nested
		{
			get
			{
				ImmutableArray<Conversion> underlyingConversions = this._conversion.UnderlyingConversions;
				if (!underlyingConversions.IsDefault)
				{
					return underlyingConversions.SelectAsArray<Conversion, DeconstructionInfo>((Conversion c) => new DeconstructionInfo(c));
				}
				return ImmutableArray<DeconstructionInfo>.Empty;
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0007473C File Offset: 0x0007293C
		internal DeconstructionInfo(Conversion conversion)
		{
			this._conversion = conversion;
		}

		// Token: 0x04000628 RID: 1576
		private readonly Conversion _conversion;
	}
}
