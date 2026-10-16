using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000579 RID: 1401
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordInequalityOperator : SynthesizedRecordEqualityOperatorBase
	{
		// Token: 0x06007566 RID: 30054 RVA: 0x001B5A6E File Offset: 0x001B3C6E
		public SynthesizedRecordInequalityOperator(SourceMemberContainerTypeSymbol containingType, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "op_Inequality", memberOffset, diagnostics)
		{
		}

		// Token: 0x06007567 RID: 30055 RVA: 0x001B5A80 File Offset: 0x001B3C80
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.ContainingType.GetNonNullSyntaxNode(), compilationState, diagnostics);
			try
			{
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(new BoundStatement[] { syntheticBoundNodeFactory.Return(syntheticBoundNodeFactory.Not(syntheticBoundNodeFactory.Call(null, Enumerable.Single<SynthesizedRecordEqualityOperator>(this.ContainingType.GetMembers("op_Equality").OfType<SynthesizedRecordEqualityOperator>()), syntheticBoundNodeFactory.Parameter(this.Parameters[0]), syntheticBoundNodeFactory.Parameter(this.Parameters[1])))) }));
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}
	}
}
