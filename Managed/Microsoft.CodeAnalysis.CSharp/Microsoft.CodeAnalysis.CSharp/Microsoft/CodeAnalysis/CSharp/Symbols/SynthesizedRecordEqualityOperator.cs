using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000575 RID: 1397
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordEqualityOperator : SynthesizedRecordEqualityOperatorBase
	{
		// Token: 0x0600754E RID: 30030 RVA: 0x001B514E File Offset: 0x001B334E
		public SynthesizedRecordEqualityOperator(SourceMemberContainerTypeSymbol containingType, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "op_Equality", memberOffset, diagnostics)
		{
		}

		// Token: 0x0600754F RID: 30031 RVA: 0x001B5160 File Offset: 0x001B3360
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.ContainingType.GetNonNullSyntaxNode(), compilationState, diagnostics);
			try
			{
				MethodSymbol methodSymbol = null;
				foreach (Symbol symbol in this.ContainingType.GetMembers("Equals"))
				{
					MethodSymbol methodSymbol2 = symbol as MethodSymbol;
					if (methodSymbol2 != null && methodSymbol2.ParameterCount == 1 && methodSymbol2.Parameters[0].RefKind == RefKind.None && methodSymbol2.ReturnType.SpecialType == SpecialType.System_Boolean && !methodSymbol2.IsStatic && methodSymbol2.Parameters[0].Type.Equals(this.ContainingType, TypeCompareKind.AllIgnoreOptions))
					{
						methodSymbol = methodSymbol2;
						break;
					}
				}
				if (methodSymbol == null)
				{
					syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
				}
				else
				{
					BoundParameter boundParameter = syntheticBoundNodeFactory.Parameter(this.Parameters[0]);
					BoundParameter boundParameter2 = syntheticBoundNodeFactory.Parameter(this.Parameters[1]);
					BoundExpression boundExpression;
					if (this.ContainingType.IsRecordStruct)
					{
						boundExpression = syntheticBoundNodeFactory.Call(boundParameter, methodSymbol, boundParameter2);
					}
					else
					{
						BoundExpression boundExpression2 = syntheticBoundNodeFactory.ObjectEqual(boundParameter, boundParameter2);
						BoundExpression boundExpression3 = syntheticBoundNodeFactory.LogicalAnd(syntheticBoundNodeFactory.ObjectNotEqual(boundParameter, syntheticBoundNodeFactory.Null(syntheticBoundNodeFactory.SpecialType(SpecialType.System_Object))), syntheticBoundNodeFactory.Call(boundParameter, methodSymbol, boundParameter2));
						boundExpression = syntheticBoundNodeFactory.LogicalOr(boundExpression2, boundExpression3);
					}
					syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(new BoundStatement[] { syntheticBoundNodeFactory.Return(boundExpression) }));
				}
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}
	}
}
