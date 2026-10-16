using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A5 RID: 421
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundHostObjectMemberReference : BoundExpression
	{
		// Token: 0x06001F0F RID: 7951 RVA: 0x000A1D10 File Offset: 0x0009FF10
		public BoundHostObjectMemberReference(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.HostObjectMemberReference, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000A1D1D File Offset: 0x0009FF1D
		public BoundHostObjectMemberReference(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.HostObjectMemberReference, syntax, type)
		{
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x000A1D29 File Offset: 0x0009FF29
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000A1D31 File Offset: 0x0009FF31
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitHostObjectMemberReference(this);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000A1D3A File Offset: 0x0009FF3A
		public BoundHostObjectMemberReference Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundHostObjectMemberReference boundHostObjectMemberReference = new BoundHostObjectMemberReference(this.Syntax, type, base.HasErrors);
				boundHostObjectMemberReference.CopyAttributes(this);
				return boundHostObjectMemberReference;
			}
			return this;
		}
	}
}
