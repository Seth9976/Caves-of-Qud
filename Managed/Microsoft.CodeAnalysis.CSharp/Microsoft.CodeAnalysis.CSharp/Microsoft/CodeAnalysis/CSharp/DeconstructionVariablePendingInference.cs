using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E9 RID: 233
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DeconstructionVariablePendingInference : VariablePendingInference
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0005F226 File Offset: 0x0005D426
		public override object Display
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0005F22D File Offset: 0x0005D42D
		protected override ErrorCode InferenceFailedError
		{
			get
			{
				return ErrorCode.ERR_TypeInferenceFailedForImplicitlyTypedDeconstructionVariable;
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0005F234 File Offset: 0x0005D434
		public DeconstructionVariablePendingInference(SyntaxNode syntax, Symbol variableSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, bool hasErrors = false)
			: base(BoundKind.DeconstructionVariablePendingInference, syntax, variableSymbol, receiverOpt, hasErrors || receiverOpt.HasErrors())
		{
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0005F251 File Offset: 0x0005D451
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDeconstructionVariablePendingInference(this);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0005F25A File Offset: 0x0005D45A
		public DeconstructionVariablePendingInference Update(Symbol variableSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(variableSymbol, base.VariableSymbol) || receiverOpt != base.ReceiverOpt)
			{
				DeconstructionVariablePendingInference deconstructionVariablePendingInference = new DeconstructionVariablePendingInference(this.Syntax, variableSymbol, receiverOpt, base.HasErrors);
				deconstructionVariablePendingInference.CopyAttributes(this);
				return deconstructionVariablePendingInference;
			}
			return this;
		}
	}
}
