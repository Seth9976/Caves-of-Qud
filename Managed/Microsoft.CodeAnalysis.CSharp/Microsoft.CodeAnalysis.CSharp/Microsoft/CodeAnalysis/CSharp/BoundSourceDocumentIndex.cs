using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200017F RID: 383
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSourceDocumentIndex : BoundExpression
	{
		// Token: 0x06001E3C RID: 7740 RVA: 0x000A076B File Offset: 0x0009E96B
		public BoundSourceDocumentIndex(SyntaxNode syntax, DebugSourceDocument document, TypeSymbol type, bool hasErrors)
			: base(BoundKind.SourceDocumentIndex, syntax, type, hasErrors)
		{
			this.Document = document;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000A0780 File Offset: 0x0009E980
		public BoundSourceDocumentIndex(SyntaxNode syntax, DebugSourceDocument document, TypeSymbol type)
			: base(BoundKind.SourceDocumentIndex, syntax, type)
		{
			this.Document = document;
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x000A0793 File Offset: 0x0009E993
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x000A079B File Offset: 0x0009E99B
		public DebugSourceDocument Document { get; }

		// Token: 0x06001E40 RID: 7744 RVA: 0x000A07A3 File Offset: 0x0009E9A3
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSourceDocumentIndex(this);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000A07AC File Offset: 0x0009E9AC
		public BoundSourceDocumentIndex Update(DebugSourceDocument document, TypeSymbol type)
		{
			if (document != this.Document || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundSourceDocumentIndex boundSourceDocumentIndex = new BoundSourceDocumentIndex(this.Syntax, document, type, base.HasErrors);
				boundSourceDocumentIndex.CopyAttributes(this);
				return boundSourceDocumentIndex;
			}
			return this;
		}
	}
}
