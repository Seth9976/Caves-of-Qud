using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200052B RID: 1323
	internal sealed class SourceComplexParameterSymbolWithCustomModifiersPrecedingByRef : SourceComplexParameterSymbol
	{
		// Token: 0x06006F44 RID: 28484 RVA: 0x00198FE8 File Offset: 0x001971E8
		internal SourceComplexParameterSymbolWithCustomModifiersPrecedingByRef(Symbol owner, int ordinal, TypeWithAnnotations parameterType, RefKind refKind, ImmutableArray<CustomModifier> refCustomModifiers, string name, ImmutableArray<Location> locations, SyntaxReference syntaxRef, bool isParams, bool isExtensionMethodThis)
			: base(owner, ordinal, parameterType, refKind, name, locations, syntaxRef, isParams, isExtensionMethodThis)
		{
			this._refCustomModifiers = refCustomModifiers;
		}

		// Token: 0x17001DA4 RID: 7588
		// (get) Token: 0x06006F45 RID: 28485 RVA: 0x00199012 File Offset: 0x00197212
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._refCustomModifiers;
			}
		}

		// Token: 0x04001B4C RID: 6988
		private readonly ImmutableArray<CustomModifier> _refCustomModifiers;
	}
}
