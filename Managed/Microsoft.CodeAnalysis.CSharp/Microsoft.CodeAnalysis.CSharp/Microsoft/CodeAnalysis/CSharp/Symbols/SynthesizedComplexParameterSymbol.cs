using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200059B RID: 1435
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedComplexParameterSymbol : SynthesizedParameterSymbolBase
	{
		// Token: 0x0600780F RID: 30735 RVA: 0x001B8B40 File Offset: 0x001B6D40
		public SynthesizedComplexParameterSymbol(MethodSymbol container, TypeWithAnnotations type, int ordinal, RefKind refKind, [global::System.Runtime.CompilerServices.Nullable(1)] string name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers, SourceComplexParameterSymbol baseParameterForAttributes)
			: base(container, type, ordinal, refKind, name)
		{
			this._refCustomModifiers = refCustomModifiers;
			this._baseParameterForAttributes = baseParameterForAttributes;
		}

		// Token: 0x1700221B RID: 8731
		// (get) Token: 0x06007810 RID: 30736 RVA: 0x001B8B5F File Offset: 0x001B6D5F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._refCustomModifiers;
			}
		}

		// Token: 0x06007811 RID: 30737 RVA: 0x001B8B67 File Offset: 0x001B6D67
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			SourceComplexParameterSymbol baseParameterForAttributes = this._baseParameterForAttributes;
			if (baseParameterForAttributes == null)
			{
				return ImmutableArray<CSharpAttributeData>.Empty;
			}
			return baseParameterForAttributes.GetAttributes();
		}

		// Token: 0x1700221C RID: 8732
		// (get) Token: 0x06007812 RID: 30738 RVA: 0x001B8B7E File Offset: 0x001B6D7E
		public bool HasEnumeratorCancellationAttribute
		{
			get
			{
				SourceComplexParameterSymbol baseParameterForAttributes = this._baseParameterForAttributes;
				return baseParameterForAttributes != null && baseParameterForAttributes.HasEnumeratorCancellationAttribute;
			}
		}

		// Token: 0x1700221D RID: 8733
		// (get) Token: 0x06007813 RID: 30739 RVA: 0x001B8B91 File Offset: 0x001B6D91
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				SourceComplexParameterSymbol baseParameterForAttributes = this._baseParameterForAttributes;
				if (baseParameterForAttributes == null)
				{
					return null;
				}
				return baseParameterForAttributes.MarshallingInformation;
			}
		}

		// Token: 0x04001CF3 RID: 7411
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<CustomModifier> _refCustomModifiers;

		// Token: 0x04001CF4 RID: 7412
		private readonly SourceComplexParameterSymbol _baseParameterForAttributes;
	}
}
