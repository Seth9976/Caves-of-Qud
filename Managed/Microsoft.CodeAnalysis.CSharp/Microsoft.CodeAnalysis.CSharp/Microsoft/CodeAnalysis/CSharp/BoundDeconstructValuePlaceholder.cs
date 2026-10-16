using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000166 RID: 358
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDeconstructValuePlaceholder : BoundValuePlaceholderBase
	{
		// Token: 0x06001DAE RID: 7598 RVA: 0x0009FB04 File Offset: 0x0009DD04
		public BoundDeconstructValuePlaceholder(SyntaxNode syntax, uint valEscape, TypeSymbol type, bool hasErrors)
			: base(BoundKind.DeconstructValuePlaceholder, syntax, type, hasErrors)
		{
			this.ValEscape = valEscape;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0009FB18 File Offset: 0x0009DD18
		public BoundDeconstructValuePlaceholder(SyntaxNode syntax, uint valEscape, TypeSymbol type)
			: base(BoundKind.DeconstructValuePlaceholder, syntax, type)
		{
			this.ValEscape = valEscape;
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0009FB2A File Offset: 0x0009DD2A
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x0009FB32 File Offset: 0x0009DD32
		public uint ValEscape { get; }

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0009FB3A File Offset: 0x0009DD3A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDeconstructValuePlaceholder(this);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0009FB43 File Offset: 0x0009DD43
		public BoundDeconstructValuePlaceholder Update(uint valEscape, TypeSymbol type)
		{
			if (valEscape != this.ValEscape || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDeconstructValuePlaceholder boundDeconstructValuePlaceholder = new BoundDeconstructValuePlaceholder(this.Syntax, valEscape, type, base.HasErrors);
				boundDeconstructValuePlaceholder.CopyAttributes(this);
				return boundDeconstructValuePlaceholder;
			}
			return this;
		}
	}
}
