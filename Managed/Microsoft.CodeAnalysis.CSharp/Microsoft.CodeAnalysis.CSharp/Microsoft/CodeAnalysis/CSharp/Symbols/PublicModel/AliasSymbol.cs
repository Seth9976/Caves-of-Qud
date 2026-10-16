using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005B7 RID: 1463
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AliasSymbol : Symbol, IAliasSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007AFD RID: 31485 RVA: 0x001BFDF3 File Offset: 0x001BDFF3
		public AliasSymbol(AliasSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x17002379 RID: 9081
		// (get) Token: 0x06007AFE RID: 31486 RVA: 0x001BFE02 File Offset: 0x001BE002
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700237A RID: 9082
		// (get) Token: 0x06007AFF RID: 31487 RVA: 0x001BFE0A File Offset: 0x001BE00A
		INamespaceOrTypeSymbol IAliasSymbol.Target
		{
			get
			{
				return this._underlying.Target.GetPublicSymbol();
			}
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x001BFE1C File Offset: 0x001BE01C
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitAlias(this);
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x001BFE25 File Offset: 0x001BE025
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitAlias(this);
		}

		// Token: 0x04001D33 RID: 7475
		private readonly AliasSymbol _underlying;
	}
}
