using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004A5 RID: 1189
	internal sealed class SynthesizedStringSwitchHashMethod : SynthesizedGlobalMethodSymbol
	{
		// Token: 0x06006268 RID: 25192 RVA: 0x0017A2A0 File Offset: 0x001784A0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static uint ComputeStringHash(string text)
		{
			uint num = 0U;
			if (text != null)
			{
				num = 2166136261U;
				for (int i = 0; i < text.Length; i++)
				{
					num = ((uint)text.get_Chars(i) ^ num) * 16777619U;
				}
			}
			return num;
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x0017A2DC File Offset: 0x001784DC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.GetNonNullSyntaxNode(), compilationState, diagnostics);
			syntheticBoundNodeFactory.CurrentFunction = this;
			try
			{
				LocalSymbol localSymbol = syntheticBoundNodeFactory.SynthesizedLocal(syntheticBoundNodeFactory.SpecialType(SpecialType.System_Int32), null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				LocalSymbol localSymbol2 = syntheticBoundNodeFactory.SynthesizedLocal(syntheticBoundNodeFactory.SpecialType(SpecialType.System_UInt32), null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				LabelSymbol labelSymbol = syntheticBoundNodeFactory.GenerateLabel("again");
				LabelSymbol labelSymbol2 = syntheticBoundNodeFactory.GenerateLabel("start");
				ParameterSymbol parameterSymbol = this.Parameters[0];
				BoundBlock boundBlock = syntheticBoundNodeFactory.Block(ImmutableArray.Create<LocalSymbol>(localSymbol2, localSymbol), new BoundStatement[]
				{
					syntheticBoundNodeFactory.If(syntheticBoundNodeFactory.Binary(BinaryOperatorKind.ObjectNotEqual, syntheticBoundNodeFactory.SpecialType(SpecialType.System_Boolean), syntheticBoundNodeFactory.Parameter(parameterSymbol), syntheticBoundNodeFactory.Null(parameterSymbol.Type)), syntheticBoundNodeFactory.Block(new BoundStatement[]
					{
						syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.Local(localSymbol2), syntheticBoundNodeFactory.Literal(2166136261U), false),
						syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.Local(localSymbol), syntheticBoundNodeFactory.Literal(0), false),
						syntheticBoundNodeFactory.Goto(labelSymbol2),
						syntheticBoundNodeFactory.Label(labelSymbol),
						syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.Local(localSymbol2), syntheticBoundNodeFactory.Binary(BinaryOperatorKind.Multiplication, localSymbol2.Type, syntheticBoundNodeFactory.Binary(BinaryOperatorKind.Xor, localSymbol2.Type, syntheticBoundNodeFactory.Convert(localSymbol2.Type, syntheticBoundNodeFactory.Call(syntheticBoundNodeFactory.Parameter(parameterSymbol), syntheticBoundNodeFactory.SpecialMethod(SpecialMember.System_String__Chars), syntheticBoundNodeFactory.Local(localSymbol)), Conversion.ImplicitNumeric, false), syntheticBoundNodeFactory.Local(localSymbol2)), syntheticBoundNodeFactory.Literal(16777619)), false),
						syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.Local(localSymbol), syntheticBoundNodeFactory.Binary(BinaryOperatorKind.Addition, localSymbol.Type, syntheticBoundNodeFactory.Local(localSymbol), syntheticBoundNodeFactory.Literal(1)), false),
						syntheticBoundNodeFactory.Label(labelSymbol2),
						syntheticBoundNodeFactory.If(syntheticBoundNodeFactory.Binary(BinaryOperatorKind.LessThan, syntheticBoundNodeFactory.SpecialType(SpecialType.System_Boolean), syntheticBoundNodeFactory.Local(localSymbol), syntheticBoundNodeFactory.Call(syntheticBoundNodeFactory.Parameter(parameterSymbol), syntheticBoundNodeFactory.SpecialMethod(SpecialMember.System_String__Length))), syntheticBoundNodeFactory.Goto(labelSymbol), null)
					}), null),
					syntheticBoundNodeFactory.Return(syntheticBoundNodeFactory.Local(localSymbol2))
				});
				syntheticBoundNodeFactory.CloseMethod(boundBlock);
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
			}
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x0017A530 File Offset: 0x00178730
		internal SynthesizedStringSwitchHashMethod(SourceModuleSymbol containingModule, PrivateImplementationDetails privateImplType, TypeSymbol returnType, TypeSymbol paramType)
			: base(containingModule, privateImplType, returnType, "ComputeStringHash")
		{
			base.SetParameters(ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(paramType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "s", default(ImmutableArray<CustomModifier>), null)));
		}
	}
}
