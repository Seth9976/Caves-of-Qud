using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005BF RID: 1471
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerTypeSymbol : TypeSymbol, IFunctionPointerTypeSymbol, ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B55 RID: 31573 RVA: 0x001C03F9 File Offset: 0x001BE5F9
		public FunctionPointerTypeSymbol(FunctionPointerTypeSymbol underlying, NullableAnnotation nullableAnnotation)
			: base(nullableAnnotation)
		{
			this._underlying = underlying;
		}

		// Token: 0x170023B2 RID: 9138
		// (get) Token: 0x06007B56 RID: 31574 RVA: 0x001C0409 File Offset: 0x001BE609
		public IMethodSymbol Signature
		{
			get
			{
				return this._underlying.Signature.GetPublicSymbol();
			}
		}

		// Token: 0x170023B3 RID: 9139
		// (get) Token: 0x06007B57 RID: 31575 RVA: 0x001C041B File Offset: 0x001BE61B
		internal override TypeSymbol UnderlyingTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x170023B4 RID: 9140
		// (get) Token: 0x06007B58 RID: 31576 RVA: 0x001C0423 File Offset: 0x001BE623
		internal override NamespaceOrTypeSymbol UnderlyingNamespaceOrTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x170023B5 RID: 9141
		// (get) Token: 0x06007B59 RID: 31577 RVA: 0x001C042B File Offset: 0x001BE62B
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x001C0433 File Offset: 0x001BE633
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitFunctionPointerType(this);
		}

		// Token: 0x06007B5B RID: 31579 RVA: 0x001C043C File Offset: 0x001BE63C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerType(this);
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x001C0445 File Offset: 0x001BE645
		protected override ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation)
		{
			return new FunctionPointerTypeSymbol(this._underlying, nullableAnnotation);
		}

		// Token: 0x04001D3E RID: 7486
		private readonly FunctionPointerTypeSymbol _underlying;
	}
}
