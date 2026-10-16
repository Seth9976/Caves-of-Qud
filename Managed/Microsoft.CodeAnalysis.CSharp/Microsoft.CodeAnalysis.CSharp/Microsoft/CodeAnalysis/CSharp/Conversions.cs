using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200004D RID: 77
	internal sealed class Conversions : ConversionsBase
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x00041CA0 File Offset: 0x0003FEA0
		public Conversions(Binder binder)
			: this(binder, 0, false, null)
		{
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00041CAC File Offset: 0x0003FEAC
		private Conversions(Binder binder, int currentRecursionDepth, bool includeNullability, Conversions otherNullabilityOpt)
			: base(binder.Compilation.Assembly.CorLibrary, currentRecursionDepth, includeNullability, otherNullabilityOpt)
		{
			this._binder = binder;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00041CCF File Offset: 0x0003FECF
		protected override ConversionsBase CreateInstance(int currentRecursionDepth)
		{
			return new Conversions(this._binder, currentRecursionDepth, this.IncludeNullability, null);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00041CE4 File Offset: 0x0003FEE4
		private CSharpCompilation Compilation
		{
			get
			{
				return this._binder.Compilation;
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00041CF1 File Offset: 0x0003FEF1
		protected override ConversionsBase WithNullabilityCore(bool includeNullability)
		{
			return new Conversions(this._binder, this.currentRecursionDepth, includeNullability, this);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00041D08 File Offset: 0x0003FF08
		public override Conversion GetMethodGroupDelegateConversion(BoundMethodGroup source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!destination.IsDelegateType() && (destination.SpecialType != SpecialType.System_Delegate || !ConversionsBase.IsFeatureInferredDelegateTypeEnabled(source)))
			{
				return Conversion.NoConversion;
			}
			ValueTuple<MethodSymbol, bool, CallingConventionInfo> delegateInvokeOrFunctionPointerMethodIfAvailable = this.GetDelegateInvokeOrFunctionPointerMethodIfAvailable(source, destination, ref useSiteInfo);
			MethodSymbol item = delegateInvokeOrFunctionPointerMethodIfAvailable.Item1;
			bool item2 = delegateInvokeOrFunctionPointerMethodIfAvailable.Item2;
			CallingConventionInfo item3 = delegateInvokeOrFunctionPointerMethodIfAvailable.Item3;
			if (item == null)
			{
				return Conversion.NoConversion;
			}
			MethodGroupResolution methodGroupResolution = Conversions.ResolveDelegateOrFunctionPointerMethodGroup(this._binder, source, item, item2, in item3, ref useSiteInfo);
			Conversion conversion = ((methodGroupResolution.IsEmpty || methodGroupResolution.HasAnyErrors) ? Conversion.NoConversion : Conversions.ToConversion(methodGroupResolution.OverloadResolutionResult, methodGroupResolution.MethodGroup, item.ParameterCount));
			methodGroupResolution.Free();
			return conversion;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00041DA4 File Offset: 0x0003FFA4
		public override Conversion GetMethodGroupFunctionPointerConversion(BoundMethodGroup source, FunctionPointerTypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Binder binder = this._binder;
			MethodSymbol signature = destination.Signature;
			bool flag = true;
			CallingConventionInfo callingConventionInfo = new CallingConventionInfo(destination.Signature.CallingConvention, destination.Signature.GetCallingConventionModifiers());
			MethodGroupResolution methodGroupResolution = Conversions.ResolveDelegateOrFunctionPointerMethodGroup(binder, source, signature, flag, in callingConventionInfo, ref useSiteInfo);
			Conversion conversion = ((methodGroupResolution.IsEmpty || methodGroupResolution.HasAnyErrors) ? Conversion.NoConversion : Conversions.ToConversion(methodGroupResolution.OverloadResolutionResult, methodGroupResolution.MethodGroup, destination.Signature.ParameterCount));
			methodGroupResolution.Free();
			return conversion;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00041E20 File Offset: 0x00040020
		protected override Conversion GetInterpolatedStringConversion(BoundUnconvertedInterpolatedString source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!TypeSymbol.Equals(destination, this.Compilation.GetWellKnownType(WellKnownType.System_IFormattable), TypeCompareKind.ConsiderEverything) && !TypeSymbol.Equals(destination, this.Compilation.GetWellKnownType(WellKnownType.System_FormattableString), TypeCompareKind.ConsiderEverything))
			{
				return Conversion.NoConversion;
			}
			return Conversion.InterpolatedString;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00041E5C File Offset: 0x0004005C
		private static MethodGroupResolution ResolveDelegateOrFunctionPointerMethodGroup(Binder binder, BoundMethodGroup source, MethodSymbol delegateInvokeMethodOpt, bool isFunctionPointer, in CallingConventionInfo callingConventionInfo, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (delegateInvokeMethodOpt != null)
			{
				AnalyzedArguments instance = AnalyzedArguments.GetInstance();
				Conversions.GetDelegateOrFunctionPointerArguments(source.Syntax, instance, delegateInvokeMethodOpt.Parameters, binder.Compilation);
				MethodGroupResolution methodGroupResolution = binder.ResolveMethodGroup(source, instance, true, ref useSiteInfo, true, delegateInvokeMethodOpt.RefKind, delegateInvokeMethodOpt.ReturnType, isFunctionPointer, in callingConventionInfo);
				instance.Free();
				return methodGroupResolution;
			}
			AnalyzedArguments analyzedArguments = null;
			bool flag = true;
			bool flag2 = false;
			RefKind refKind = RefKind.None;
			TypeSymbol typeSymbol = null;
			bool flag3 = false;
			CallingConventionInfo callingConventionInfo2 = default(CallingConventionInfo);
			return binder.ResolveMethodGroup(source, analyzedArguments, flag, ref useSiteInfo, flag2, refKind, typeSymbol, flag3, in callingConventionInfo2);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00041EC4 File Offset: 0x000400C4
		[return: TupleElementNames(new string[] { null, "isFunctionPointer", "callingConventionInfo" })]
		private ValueTuple<MethodSymbol, bool, CallingConventionInfo> GetDelegateInvokeOrFunctionPointerMethodIfAvailable(BoundMethodGroup methodGroup, TypeSymbol type, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			FunctionPointerTypeSymbol functionPointerTypeSymbol = type as FunctionPointerTypeSymbol;
			if (functionPointerTypeSymbol != null)
			{
				FunctionPointerMethodSymbol signature = functionPointerTypeSymbol.Signature;
				if (signature != null)
				{
					return new ValueTuple<MethodSymbol, bool, CallingConventionInfo>(signature, true, new CallingConventionInfo(signature.CallingConvention, signature.GetCallingConventionModifiers()));
				}
			}
			NamedTypeSymbol namedTypeSymbol = ((type.SpecialType == SpecialType.System_Delegate && methodGroup.Syntax.IsFeatureEnabled(MessageID.IDS_FeatureNullableReferenceTypes)) ? this._binder.GetMethodGroupDelegateType(methodGroup, ref useSiteInfo) : type.GetDelegateType());
			if (namedTypeSymbol == null)
			{
				return new ValueTuple<MethodSymbol, bool, CallingConventionInfo>(null, false, default(CallingConventionInfo));
			}
			MethodSymbol delegateInvokeMethod = namedTypeSymbol.DelegateInvokeMethod;
			if (delegateInvokeMethod == null || delegateInvokeMethod.HasUseSiteError)
			{
				return new ValueTuple<MethodSymbol, bool, CallingConventionInfo>(null, false, default(CallingConventionInfo));
			}
			return new ValueTuple<MethodSymbol, bool, CallingConventionInfo>(delegateInvokeMethod, false, default(CallingConventionInfo));
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00041F7C File Offset: 0x0004017C
		public bool ReportDelegateOrFunctionPointerMethodGroupDiagnostics(Binder binder, BoundMethodGroup expr, TypeSymbol targetType, BindingDiagnosticBag diagnostics)
		{
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = binder.GetNewCompoundUseSiteInfo(diagnostics);
			ValueTuple<MethodSymbol, bool, CallingConventionInfo> delegateInvokeOrFunctionPointerMethodIfAvailable = this.GetDelegateInvokeOrFunctionPointerMethodIfAvailable(expr, targetType, ref newCompoundUseSiteInfo);
			MethodSymbol item = delegateInvokeOrFunctionPointerMethodIfAvailable.Item1;
			bool item2 = delegateInvokeOrFunctionPointerMethodIfAvailable.Item2;
			CallingConventionInfo item3 = delegateInvokeOrFunctionPointerMethodIfAvailable.Item3;
			MethodGroupResolution methodGroupResolution = Conversions.ResolveDelegateOrFunctionPointerMethodGroup(binder, expr, item, item2, in item3, ref newCompoundUseSiteInfo);
			diagnostics.Add(expr.Syntax, newCompoundUseSiteInfo);
			bool flag = methodGroupResolution.HasAnyErrors;
			diagnostics.AddRange(methodGroupResolution.Diagnostics, false);
			if (methodGroupResolution.MethodGroup != null)
			{
				OverloadResolutionResult<MethodSymbol> overloadResolutionResult = methodGroupResolution.OverloadResolutionResult;
				if (overloadResolutionResult != null)
				{
					if (overloadResolutionResult.Succeeded)
					{
						MethodSymbol member = overloadResolutionResult.BestResult.Member;
						if (methodGroupResolution.MethodGroup.IsExtensionMethodGroup)
						{
							ParameterSymbol parameterSymbol = member.Parameters[0];
							if (!parameterSymbol.Type.IsReferenceType)
							{
								diagnostics.Add(ErrorCode.ERR_ValueTypeExtDelegate, expr.Syntax.Location, new object[] { member, parameterSymbol.Type });
								flag = true;
							}
						}
						else if (member.ContainingType.IsNullableType() && !member.IsOverride)
						{
							diagnostics.Add(ErrorCode.ERR_DelegateOnNullable, expr.Syntax.Location, new object[] { member });
							flag = true;
						}
					}
					else if (!flag && !methodGroupResolution.IsEmpty && methodGroupResolution.ResultKind == LookupResultKind.Viable)
					{
						BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(true, diagnostics.AccumulatesDependencies);
						overloadResolutionResult.ReportDiagnostics<MethodSymbol>(binder, expr.Syntax.Location, expr.Syntax, instance, expr.Name, methodGroupResolution.MethodGroup.Receiver, expr.Syntax, methodGroupResolution.AnalyzedArguments, methodGroupResolution.MethodGroup.Methods.ToImmutable(), null, null, null, true, (item != null) ? new RefKind?(item.RefKind) : default(RefKind?), targetType);
						flag = instance.HasAnyErrors();
						diagnostics.AddRangeAndFree(instance);
					}
				}
			}
			methodGroupResolution.Free();
			return flag;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00042184 File Offset: 0x00040384
		public Conversion MethodGroupConversion(SyntaxNode syntax, MethodGroup methodGroup, NamedTypeSymbol delegateType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			AnalyzedArguments instance = AnalyzedArguments.GetInstance();
			OverloadResolutionResult<MethodSymbol> instance2 = OverloadResolutionResult<MethodSymbol>.GetInstance();
			MethodSymbol delegateInvokeMethod = delegateType.DelegateInvokeMethod;
			Conversions.GetDelegateOrFunctionPointerArguments(syntax, instance, delegateInvokeMethod.Parameters, this.Compilation);
			OverloadResolution overloadResolution = this._binder.OverloadResolution;
			ArrayBuilder<MethodSymbol> methods = methodGroup.Methods;
			ArrayBuilder<TypeWithAnnotations> typeArguments = methodGroup.TypeArguments;
			BoundExpression receiver = methodGroup.Receiver;
			AnalyzedArguments analyzedArguments = instance;
			OverloadResolutionResult<MethodSymbol> overloadResolutionResult = instance2;
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			RefKind refKind = delegateInvokeMethod.RefKind;
			TypeSymbol returnType = delegateInvokeMethod.ReturnType;
			bool flag5 = false;
			CallingConventionInfo callingConventionInfo = default(CallingConventionInfo);
			overloadResolution.MethodInvocationOverloadResolution(methods, typeArguments, receiver, analyzedArguments, overloadResolutionResult, ref useSiteInfo, flag, flag2, flag3, flag4, refKind, returnType, flag5, in callingConventionInfo);
			Conversion conversion = Conversions.ToConversion(instance2, methodGroup, delegateType.DelegateInvokeMethod.ParameterCount);
			instance.Free();
			instance2.Free();
			return conversion;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00042218 File Offset: 0x00040418
		public static void GetDelegateOrFunctionPointerArguments(SyntaxNode syntax, AnalyzedArguments analyzedArguments, ImmutableArray<ParameterSymbol> delegateParameters, CSharpCompilation compilation)
		{
			foreach (ParameterSymbol parameterSymbol in delegateParameters)
			{
				if (parameterSymbol.Type.IsDynamic())
				{
					parameterSymbol = new SignatureOnlyParameterSymbol(TypeWithAnnotations.Create(compilation.GetSpecialType(SpecialType.System_Object), NullableAnnotation.Oblivious, parameterSymbol.TypeWithAnnotations.CustomModifiers), parameterSymbol.RefCustomModifiers, parameterSymbol.IsParams, parameterSymbol.RefKind);
				}
				analyzedArguments.Arguments.Add(new BoundParameter(syntax, parameterSymbol)
				{
					WasCompilerGenerated = true
				});
				analyzedArguments.RefKinds.Add(parameterSymbol.RefKind);
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000422AC File Offset: 0x000404AC
		private static Conversion ToConversion(OverloadResolutionResult<MethodSymbol> result, MethodGroup methodGroup, int parameterCount)
		{
			if (!result.Succeeded)
			{
				return Conversion.NoConversion;
			}
			MethodSymbol member = result.BestResult.Member;
			if (methodGroup.IsExtensionMethodGroup && !member.Parameters[0].Type.IsReferenceType)
			{
				return Conversion.NoConversion;
			}
			if (member.RequiresInstanceReceiver)
			{
				BoundExpression receiver = methodGroup.Receiver;
				bool flag;
				if (receiver == null)
				{
					flag = false;
				}
				else
				{
					TypeSymbol type = receiver.Type;
					bool? flag2 = ((type != null) ? new bool?(type.IsRestrictedType(false)) : default(bool?));
					bool flag3 = true;
					flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
				}
				if (flag)
				{
					return Conversion.NoConversion;
				}
			}
			if (member.ContainingType.IsNullableType() && !member.IsOverride)
			{
				return Conversion.NoConversion;
			}
			return new Conversion(ConversionKind.MethodGroup, member, methodGroup.IsExtensionMethodGroup);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0004237C File Offset: 0x0004057C
		public override Conversion GetStackAllocConversion(BoundStackAllocArrayCreation sourceExpression, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (sourceExpression.NeedsToBeConverted())
			{
				PointerTypeSymbol pointerTypeSymbol = new PointerTypeSymbol(TypeWithAnnotations.Create(sourceExpression.ElementType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
				Conversion conversion = base.ClassifyImplicitConversionFromType(pointerTypeSymbol, destination, ref useSiteInfo);
				if (conversion.IsValid)
				{
					return Conversion.MakeStackAllocToPointerType(conversion);
				}
				NamedTypeSymbol wellKnownType = this._binder.GetWellKnownType(WellKnownType.System_Span_T, ref useSiteInfo);
				if (wellKnownType.TypeKind == TypeKind.Struct && wellKnownType.IsRefLikeType)
				{
					NamedTypeSymbol namedTypeSymbol = wellKnownType.Construct(new TypeSymbol[] { sourceExpression.ElementType });
					Conversion conversion2 = base.ClassifyImplicitConversionFromType(namedTypeSymbol, destination, ref useSiteInfo);
					if (conversion2.Exists)
					{
						return Conversion.MakeStackAllocToSpanType(conversion2);
					}
				}
			}
			return Conversion.NoConversion;
		}

		// Token: 0x04000119 RID: 281
		private readonly Binder _binder;
	}
}
