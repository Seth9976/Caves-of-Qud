using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A4 RID: 420
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundPreviousSubmissionReference : BoundExpression
	{
		// Token: 0x06001F0A RID: 7946 RVA: 0x000A1CBA File Offset: 0x0009FEBA
		public BoundPreviousSubmissionReference(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.PreviousSubmissionReference, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000A1CC7 File Offset: 0x0009FEC7
		public BoundPreviousSubmissionReference(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.PreviousSubmissionReference, syntax, type)
		{
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x000A1CD3 File Offset: 0x0009FED3
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000A1CDB File Offset: 0x0009FEDB
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitPreviousSubmissionReference(this);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000A1CE4 File Offset: 0x0009FEE4
		public BoundPreviousSubmissionReference Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundPreviousSubmissionReference boundPreviousSubmissionReference = new BoundPreviousSubmissionReference(this.Syntax, type, base.HasErrors);
				boundPreviousSubmissionReference.CopyAttributes(this);
				return boundPreviousSubmissionReference;
			}
			return this;
		}
	}
}
