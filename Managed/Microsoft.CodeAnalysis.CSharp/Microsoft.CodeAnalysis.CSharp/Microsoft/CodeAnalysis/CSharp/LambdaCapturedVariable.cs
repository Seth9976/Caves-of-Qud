using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001F3 RID: 499
	internal sealed class LambdaCapturedVariable : SynthesizedFieldSymbolBase
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x000B4453 File Offset: 0x000B2653
		private LambdaCapturedVariable(SynthesizedContainer frame, TypeWithAnnotations type, string fieldName, bool isThisParameter)
			: base(frame, fieldName, true, false, false)
		{
			this._type = type;
			this._isThis = isThisParameter;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000B4470 File Offset: 0x000B2670
		public static LambdaCapturedVariable Create(SynthesizedClosureEnvironment frame, Symbol captured, ref int uniqueId)
		{
			string capturedVariableFieldName = LambdaCapturedVariable.GetCapturedVariableFieldName(captured, ref uniqueId);
			TypeSymbol capturedVariableFieldType = LambdaCapturedVariable.GetCapturedVariableFieldType(frame, captured);
			return new LambdaCapturedVariable(frame, TypeWithAnnotations.Create(capturedVariableFieldType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), capturedVariableFieldName, LambdaCapturedVariable.IsThis(captured));
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000B44AC File Offset: 0x000B26AC
		private static bool IsThis(Symbol captured)
		{
			ParameterSymbol parameterSymbol = captured as ParameterSymbol;
			return parameterSymbol != null && parameterSymbol.IsThis;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000B44CC File Offset: 0x000B26CC
		private static string GetCapturedVariableFieldName(Symbol variable, ref int uniqueId)
		{
			if (LambdaCapturedVariable.IsThis(variable))
			{
				return GeneratedNames.ThisProxyFieldName();
			}
			LocalSymbol localSymbol = variable as LocalSymbol;
			if (localSymbol != null)
			{
				if (localSymbol.SynthesizedKind == SynthesizedLocalKind.LambdaDisplayClass)
				{
					int num = uniqueId;
					uniqueId = num + 1;
					return GeneratedNames.MakeLambdaDisplayLocalName(num);
				}
				if (localSymbol.SynthesizedKind == SynthesizedLocalKind.ExceptionFilterAwaitHoistedExceptionLocal)
				{
					SynthesizedLocalKind synthesizedKind = localSymbol.SynthesizedKind;
					int num = uniqueId;
					uniqueId = num + 1;
					return GeneratedNames.MakeHoistedLocalFieldName(synthesizedKind, num, null);
				}
				if (localSymbol.SynthesizedKind == SynthesizedLocalKind.InstrumentationPayload)
				{
					int num = uniqueId;
					uniqueId = num + 1;
					return GeneratedNames.MakeSynthesizedInstrumentationPayloadLocalFieldName(num);
				}
				if (localSymbol.SynthesizedKind == SynthesizedLocalKind.UserDefined)
				{
					SyntaxNode scopeDesignatorOpt = localSymbol.ScopeDesignatorOpt;
					if (scopeDesignatorOpt == null || scopeDesignatorOpt.Kind() != SyntaxKind.SwitchSection)
					{
						SyntaxNode scopeDesignatorOpt2 = localSymbol.ScopeDesignatorOpt;
						if (scopeDesignatorOpt2 == null || scopeDesignatorOpt2.Kind() != SyntaxKind.SwitchExpressionArm)
						{
							goto IL_00C6;
						}
					}
					SynthesizedLocalKind synthesizedKind2 = localSymbol.SynthesizedKind;
					int num = uniqueId;
					uniqueId = num + 1;
					return GeneratedNames.MakeHoistedLocalFieldName(synthesizedKind2, num, localSymbol.Name);
				}
			}
			IL_00C6:
			return variable.Name;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000B45A8 File Offset: 0x000B27A8
		private static TypeSymbol GetCapturedVariableFieldType(SynthesizedContainer frame, Symbol variable)
		{
			LocalSymbol localSymbol = variable as LocalSymbol;
			if (localSymbol != null)
			{
				SynthesizedClosureEnvironment synthesizedClosureEnvironment = localSymbol.Type.OriginalDefinition as SynthesizedClosureEnvironment;
				if (synthesizedClosureEnvironment != null)
				{
					ImmutableArray<TypeWithAnnotations> immutableArray = frame.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics;
					if (immutableArray.Length > synthesizedClosureEnvironment.Arity)
					{
						immutableArray = ImmutableArray.Create<TypeWithAnnotations>(immutableArray, 0, synthesizedClosureEnvironment.Arity);
					}
					return synthesizedClosureEnvironment.ConstructIfGeneric(immutableArray);
				}
			}
			return frame.TypeMap.SubstituteType(((localSymbol != null) ? localSymbol.TypeWithAnnotations : ((ParameterSymbol)variable).TypeWithAnnotations).Type).Type;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000B4630 File Offset: 0x000B2830
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return this._type;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x000B4638 File Offset: 0x000B2838
		internal override bool IsCapturedFrame
		{
			get
			{
				return this._isThis;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x000B4640 File Offset: 0x000B2840
		internal override bool SuppressDynamicAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001090 RID: 4240
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001091 RID: 4241
		private readonly bool _isThis;
	}
}
