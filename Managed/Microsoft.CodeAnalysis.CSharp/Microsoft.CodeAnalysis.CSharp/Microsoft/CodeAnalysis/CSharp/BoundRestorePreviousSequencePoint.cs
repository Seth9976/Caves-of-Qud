using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000189 RID: 393
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundRestorePreviousSequencePoint : BoundStatement
	{
		// Token: 0x06001E75 RID: 7797 RVA: 0x000A0CAC File Offset: 0x0009EEAC
		public BoundRestorePreviousSequencePoint(SyntaxNode syntax, object identifier, bool hasErrors)
			: base(BoundKind.RestorePreviousSequencePoint, syntax, hasErrors)
		{
			this.Identifier = identifier;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000A0CBF File Offset: 0x0009EEBF
		public BoundRestorePreviousSequencePoint(SyntaxNode syntax, object identifier)
			: base(BoundKind.RestorePreviousSequencePoint, syntax)
		{
			this.Identifier = identifier;
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x000A0CD1 File Offset: 0x0009EED1
		public object Identifier { get; }

		// Token: 0x06001E78 RID: 7800 RVA: 0x000A0CD9 File Offset: 0x0009EED9
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitRestorePreviousSequencePoint(this);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000A0CE2 File Offset: 0x0009EEE2
		public BoundRestorePreviousSequencePoint Update(object identifier)
		{
			if (identifier != this.Identifier)
			{
				BoundRestorePreviousSequencePoint boundRestorePreviousSequencePoint = new BoundRestorePreviousSequencePoint(this.Syntax, identifier, base.HasErrors);
				boundRestorePreviousSequencePoint.CopyAttributes(this);
				return boundRestorePreviousSequencePoint;
			}
			return this;
		}
	}
}
