using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005BC RID: 1468
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ErrorTypeSymbol : NamedTypeSymbol, IErrorTypeSymbol, INamedTypeSymbol, ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B2E RID: 31534 RVA: 0x001C0189 File Offset: 0x001BE389
		public ErrorTypeSymbol(ErrorTypeSymbol underlying, NullableAnnotation nullableAnnotation)
			: base(nullableAnnotation)
		{
			this._underlying = underlying;
		}

		// Token: 0x06007B2F RID: 31535 RVA: 0x001C0199 File Offset: 0x001BE399
		protected override ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation)
		{
			return new ErrorTypeSymbol(this._underlying, nullableAnnotation);
		}

		// Token: 0x17002393 RID: 9107
		// (get) Token: 0x06007B30 RID: 31536 RVA: 0x001C01A7 File Offset: 0x001BE3A7
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002394 RID: 9108
		// (get) Token: 0x06007B31 RID: 31537 RVA: 0x001C01AF File Offset: 0x001BE3AF
		internal override NamespaceOrTypeSymbol UnderlyingNamespaceOrTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002395 RID: 9109
		// (get) Token: 0x06007B32 RID: 31538 RVA: 0x001C01B7 File Offset: 0x001BE3B7
		internal override TypeSymbol UnderlyingTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002396 RID: 9110
		// (get) Token: 0x06007B33 RID: 31539 RVA: 0x001C01BF File Offset: 0x001BE3BF
		internal override NamedTypeSymbol UnderlyingNamedTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002397 RID: 9111
		// (get) Token: 0x06007B34 RID: 31540 RVA: 0x001C01C7 File Offset: 0x001BE3C7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ISymbol> IErrorTypeSymbol.CandidateSymbols
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._underlying.CandidateSymbols.SelectAsArray<Symbol, ISymbol>((Symbol s) => s.GetPublicSymbol());
			}
		}

		// Token: 0x17002398 RID: 9112
		// (get) Token: 0x06007B35 RID: 31541 RVA: 0x001C01F8 File Offset: 0x001BE3F8
		CandidateReason IErrorTypeSymbol.CandidateReason
		{
			get
			{
				return this._underlying.CandidateReason;
			}
		}

		// Token: 0x04001D39 RID: 7481
		private readonly ErrorTypeSymbol _underlying;
	}
}
