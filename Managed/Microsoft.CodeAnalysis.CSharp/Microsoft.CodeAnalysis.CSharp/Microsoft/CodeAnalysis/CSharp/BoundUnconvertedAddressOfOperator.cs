using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000EB RID: 235
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundUnconvertedAddressOfOperator : BoundExpression
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0005F37A File Offset: 0x0005D57A
		public override object Display
		{
			get
			{
				return FormattableStringFactory.Create("&{0}", new object[] { this.Operand.Display });
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0005F39A File Offset: 0x0005D59A
		public BoundUnconvertedAddressOfOperator(SyntaxNode syntax, BoundMethodGroup operand, bool hasErrors = false)
			: base(BoundKind.UnconvertedAddressOfOperator, syntax, null, hasErrors || operand.HasErrors())
		{
			this.Operand = operand;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0005F3B9 File Offset: 0x0005D5B9
		public BoundMethodGroup Operand { get; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x0005F3C1 File Offset: 0x0005D5C1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0005F3C9 File Offset: 0x0005D5C9
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUnconvertedAddressOfOperator(this);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0005F3D2 File Offset: 0x0005D5D2
		public BoundUnconvertedAddressOfOperator Update(BoundMethodGroup operand)
		{
			if (operand != this.Operand)
			{
				BoundUnconvertedAddressOfOperator boundUnconvertedAddressOfOperator = new BoundUnconvertedAddressOfOperator(this.Syntax, operand, base.HasErrors);
				boundUnconvertedAddressOfOperator.CopyAttributes(this);
				return boundUnconvertedAddressOfOperator;
			}
			return this;
		}
	}
}
