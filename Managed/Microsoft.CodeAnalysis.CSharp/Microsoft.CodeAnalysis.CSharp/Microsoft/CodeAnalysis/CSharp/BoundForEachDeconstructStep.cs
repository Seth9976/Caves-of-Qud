using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200019F RID: 415
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundForEachDeconstructStep : BoundNode
	{
		// Token: 0x06001EE7 RID: 7911 RVA: 0x000A18FE File Offset: 0x0009FAFE
		public BoundForEachDeconstructStep(SyntaxNode syntax, BoundDeconstructionAssignmentOperator deconstructionAssignment, BoundDeconstructValuePlaceholder targetPlaceholder, bool hasErrors = false)
			: base(BoundKind.ForEachDeconstructStep, syntax, hasErrors || deconstructionAssignment.HasErrors() || targetPlaceholder.HasErrors())
		{
			this.DeconstructionAssignment = deconstructionAssignment;
			this.TargetPlaceholder = targetPlaceholder;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x000A192C File Offset: 0x0009FB2C
		public BoundDeconstructionAssignmentOperator DeconstructionAssignment { get; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000A1934 File Offset: 0x0009FB34
		public BoundDeconstructValuePlaceholder TargetPlaceholder { get; }

		// Token: 0x06001EEA RID: 7914 RVA: 0x000A193C File Offset: 0x0009FB3C
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitForEachDeconstructStep(this);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000A1945 File Offset: 0x0009FB45
		public BoundForEachDeconstructStep Update(BoundDeconstructionAssignmentOperator deconstructionAssignment, BoundDeconstructValuePlaceholder targetPlaceholder)
		{
			if (deconstructionAssignment != this.DeconstructionAssignment || targetPlaceholder != this.TargetPlaceholder)
			{
				BoundForEachDeconstructStep boundForEachDeconstructStep = new BoundForEachDeconstructStep(this.Syntax, deconstructionAssignment, targetPlaceholder, base.HasErrors);
				boundForEachDeconstructStep.CopyAttributes(this);
				return boundForEachDeconstructStep;
			}
			return this;
		}
	}
}
