using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000494 RID: 1172
	[NullableContext(2)]
	[Nullable(0)]
	public struct SemanticEdit : IEquatable<SemanticEdit>
	{
		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x00082E48 File Offset: 0x00081048
		public readonly SemanticEditKind Kind { get; }

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x00082E50 File Offset: 0x00081050
		public readonly ISymbol OldSymbol { get; }

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x00082E58 File Offset: 0x00081058
		public readonly ISymbol NewSymbol { get; }

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x00082E60 File Offset: 0x00081060
		[Nullable(new byte[] { 2, 1, 2 })]
		public readonly Func<SyntaxNode, SyntaxNode> SyntaxMap
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x00082E68 File Offset: 0x00081068
		public readonly bool PreserveLocalVariables { get; }

		// Token: 0x06002E34 RID: 11828 RVA: 0x00082E70 File Offset: 0x00081070
		public SemanticEdit(SemanticEditKind kind, ISymbol oldSymbol, ISymbol newSymbol, [Nullable(new byte[] { 2, 1, 2 })] Func<SyntaxNode, SyntaxNode> syntaxMap = null, bool preserveLocalVariables = false)
		{
			if (oldSymbol == null && kind != SemanticEditKind.Insert)
			{
				throw new ArgumentNullException("oldSymbol");
			}
			if (newSymbol == null && kind != SemanticEditKind.Delete)
			{
				throw new ArgumentNullException("newSymbol");
			}
			if (kind <= SemanticEditKind.None || kind > SemanticEditKind.Delete)
			{
				throw new ArgumentOutOfRangeException("kind");
			}
			this.Kind = kind;
			this.OldSymbol = oldSymbol;
			this.NewSymbol = newSymbol;
			this.PreserveLocalVariables = preserveLocalVariables;
			this.SyntaxMap = syntaxMap;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x00082ED9 File Offset: 0x000810D9
		[NullableContext(1)]
		internal static SemanticEdit Create(SemanticEditKind kind, ISymbolInternal oldSymbol, ISymbolInternal newSymbol, [Nullable(new byte[] { 2, 1, 1 })] Func<SyntaxNode, SyntaxNode> syntaxMap = null, bool preserveLocalVariables = false)
		{
			return new SemanticEdit(kind, (oldSymbol != null) ? oldSymbol.GetISymbol() : null, (newSymbol != null) ? newSymbol.GetISymbol() : null, syntaxMap, preserveLocalVariables);
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x00082EFC File Offset: 0x000810FC
		public override int GetHashCode()
		{
			return Hash.Combine<ISymbol>(this.OldSymbol, Hash.Combine<ISymbol>(this.NewSymbol, (int)this.Kind));
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x00082F1A File Offset: 0x0008111A
		public override bool Equals(object obj)
		{
			return obj is SemanticEdit && this.Equals((SemanticEdit)obj);
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x00082F34 File Offset: 0x00081134
		public bool Equals(SemanticEdit other)
		{
			if (this.Kind != other.Kind || !((this.OldSymbol == null) ? (other.OldSymbol == null) : this.OldSymbol.Equals(other.OldSymbol)))
			{
				return false;
			}
			if (this.NewSymbol != null)
			{
				return this.NewSymbol.Equals(other.NewSymbol);
			}
			return other.NewSymbol == null;
		}
	}
}
