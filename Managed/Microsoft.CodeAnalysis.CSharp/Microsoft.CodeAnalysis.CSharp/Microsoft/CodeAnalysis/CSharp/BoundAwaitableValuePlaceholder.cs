using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000168 RID: 360
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundAwaitableValuePlaceholder : BoundValuePlaceholderBase
	{
		// Token: 0x06001DB9 RID: 7609 RVA: 0x0009FBCD File Offset: 0x0009DDCD
		public BoundAwaitableValuePlaceholder(SyntaxNode syntax, uint valEscape, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors)
			: base(BoundKind.AwaitableValuePlaceholder, syntax, type, hasErrors)
		{
			this.ValEscape = valEscape;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0009FBE1 File Offset: 0x0009DDE1
		public BoundAwaitableValuePlaceholder(SyntaxNode syntax, uint valEscape, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
			: base(BoundKind.AwaitableValuePlaceholder, syntax, type)
		{
			this.ValEscape = valEscape;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x0009FBF3 File Offset: 0x0009DDF3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x0009FBFB File Offset: 0x0009DDFB
		public uint ValEscape { get; }

		// Token: 0x06001DBD RID: 7613 RVA: 0x0009FC03 File Offset: 0x0009DE03
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitAwaitableValuePlaceholder(this);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0009FC0C File Offset: 0x0009DE0C
		public BoundAwaitableValuePlaceholder Update(uint valEscape, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (valEscape != this.ValEscape || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundAwaitableValuePlaceholder boundAwaitableValuePlaceholder = new BoundAwaitableValuePlaceholder(this.Syntax, valEscape, type, base.HasErrors);
				boundAwaitableValuePlaceholder.CopyAttributes(this);
				return boundAwaitableValuePlaceholder;
			}
			return this;
		}
	}
}
