using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E7 RID: 231
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OutVariablePendingInference : VariablePendingInference
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0005F117 File Offset: 0x0005D317
		public override object Display
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0005F11E File Offset: 0x0005D31E
		protected override ErrorCode InferenceFailedError
		{
			get
			{
				return ErrorCode.ERR_TypeInferenceFailedForImplicitlyTypedOutVariable;
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0005F125 File Offset: 0x0005D325
		public OutVariablePendingInference(SyntaxNode syntax, Symbol variableSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, bool hasErrors = false)
			: base(BoundKind.OutVariablePendingInference, syntax, variableSymbol, receiverOpt, hasErrors || receiverOpt.HasErrors())
		{
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0005F142 File Offset: 0x0005D342
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitOutVariablePendingInference(this);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0005F14B File Offset: 0x0005D34B
		public OutVariablePendingInference Update(Symbol variableSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(variableSymbol, base.VariableSymbol) || receiverOpt != base.ReceiverOpt)
			{
				OutVariablePendingInference outVariablePendingInference = new OutVariablePendingInference(this.Syntax, variableSymbol, receiverOpt, base.HasErrors);
				outVariablePendingInference.CopyAttributes(this);
				return outVariablePendingInference;
			}
			return this;
		}
	}
}
