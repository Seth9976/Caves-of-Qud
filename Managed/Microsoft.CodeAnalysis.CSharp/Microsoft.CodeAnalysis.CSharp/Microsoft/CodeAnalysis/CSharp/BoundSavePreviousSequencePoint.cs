using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000188 RID: 392
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSavePreviousSequencePoint : BoundStatement
	{
		// Token: 0x06001E70 RID: 7792 RVA: 0x000A0C50 File Offset: 0x0009EE50
		public BoundSavePreviousSequencePoint(SyntaxNode syntax, object identifier, bool hasErrors)
			: base(BoundKind.SavePreviousSequencePoint, syntax, hasErrors)
		{
			this.Identifier = identifier;
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x000A0C63 File Offset: 0x0009EE63
		public BoundSavePreviousSequencePoint(SyntaxNode syntax, object identifier)
			: base(BoundKind.SavePreviousSequencePoint, syntax)
		{
			this.Identifier = identifier;
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000A0C75 File Offset: 0x0009EE75
		public object Identifier { get; }

		// Token: 0x06001E73 RID: 7795 RVA: 0x000A0C7D File Offset: 0x0009EE7D
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSavePreviousSequencePoint(this);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000A0C86 File Offset: 0x0009EE86
		public BoundSavePreviousSequencePoint Update(object identifier)
		{
			if (identifier != this.Identifier)
			{
				BoundSavePreviousSequencePoint boundSavePreviousSequencePoint = new BoundSavePreviousSequencePoint(this.Syntax, identifier, base.HasErrors);
				boundSavePreviousSequencePoint.CopyAttributes(this);
				return boundSavePreviousSequencePoint;
			}
			return this;
		}
	}
}
