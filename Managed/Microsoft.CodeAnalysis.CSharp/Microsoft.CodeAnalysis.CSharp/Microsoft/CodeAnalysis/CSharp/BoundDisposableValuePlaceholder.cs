using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000169 RID: 361
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDisposableValuePlaceholder : BoundValuePlaceholderBase
	{
		// Token: 0x06001DBF RID: 7615 RVA: 0x0009FC42 File Offset: 0x0009DE42
		public BoundDisposableValuePlaceholder(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.DisposableValuePlaceholder, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0009FC4E File Offset: 0x0009DE4E
		public BoundDisposableValuePlaceholder(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.DisposableValuePlaceholder, syntax, type)
		{
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x0009FC59 File Offset: 0x0009DE59
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0009FC61 File Offset: 0x0009DE61
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDisposableValuePlaceholder(this);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0009FC6A File Offset: 0x0009DE6A
		public BoundDisposableValuePlaceholder Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDisposableValuePlaceholder boundDisposableValuePlaceholder = new BoundDisposableValuePlaceholder(this.Syntax, type, base.HasErrors);
				boundDisposableValuePlaceholder.CopyAttributes(this);
				return boundDisposableValuePlaceholder;
			}
			return this;
		}
	}
}
