using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E8 RID: 1256
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerMethodSymbol : MethodSymbol
	{
		// Token: 0x060069FF RID: 27135 RVA: 0x0018B250 File Offset: 0x00189450
		public static FunctionPointerMethodSymbol CreateFromSource(FunctionPointerTypeSyntax syntax, Binder typeBinder, BindingDiagnosticBag diagnostics, ConsList<TypeSymbol> basesBeingResolved, bool suppressUseSiteDiagnostics)
		{
			ArrayBuilder<CustomModifier> instance = ArrayBuilder<CustomModifier>.GetInstance();
			CallingConvention callingConvention = FunctionPointerMethodSymbol.<CreateFromSource>g__getCallingConvention|2_0(typeBinder.Compilation, syntax.CallingConvention, instance, diagnostics);
			RefKind refKind = RefKind.None;
			SeparatedSyntaxList<FunctionPointerParameterSyntax> separatedSyntaxList = syntax.ParameterList.Parameters;
			TypeWithAnnotations typeWithAnnotations;
			if (separatedSyntaxList.Count == 0)
			{
				typeWithAnnotations = TypeWithAnnotations.Create(typeBinder.CreateErrorType(""), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
			else
			{
				separatedSyntaxList = syntax.ParameterList.Parameters;
				int num = separatedSyntaxList.Count - 1;
				FunctionPointerParameterSyntax functionPointerParameterSyntax = separatedSyntaxList[num];
				SyntaxTokenList modifiers = functionPointerParameterSyntax.Modifiers;
				for (int i = 0; i < modifiers.Count; i++)
				{
					SyntaxToken syntaxToken = modifiers[i];
					if (syntaxToken.Kind() == SyntaxKind.RefKeyword)
					{
						if (refKind == RefKind.None)
						{
							if (modifiers.Count > i + 1 && modifiers[i + 1].Kind() == SyntaxKind.ReadOnlyKeyword)
							{
								i++;
								refKind = RefKind.In;
								instance.AddRange(ParameterHelpers.CreateInModifiers(typeBinder, diagnostics, functionPointerParameterSyntax));
							}
							else
							{
								refKind = RefKind.Ref;
							}
						}
						else
						{
							diagnostics.Add(ErrorCode.ERR_DupReturnTypeMod, syntaxToken.GetLocation(), new object[] { syntaxToken.Text });
						}
					}
					else
					{
						diagnostics.Add(ErrorCode.ERR_InvalidFuncPointerReturnTypeModifier, syntaxToken.GetLocation(), new object[] { syntaxToken.Text });
					}
				}
				typeWithAnnotations = typeBinder.BindType(functionPointerParameterSyntax.Type, diagnostics, basesBeingResolved, suppressUseSiteDiagnostics);
				if (typeWithAnnotations.IsVoidType() && refKind != RefKind.None)
				{
					diagnostics.Add(ErrorCode.ERR_NoVoidHere, functionPointerParameterSyntax.Location);
				}
				else if (typeWithAnnotations.IsStatic)
				{
					diagnostics.Add(ErrorFacts.GetStaticClassReturnCode(false), functionPointerParameterSyntax.Location, new object[] { typeWithAnnotations });
				}
				else if (typeWithAnnotations.IsRestrictedType(true))
				{
					diagnostics.Add(ErrorCode.ERR_MethodReturnCantBeRefAny, functionPointerParameterSyntax.Location, new object[] { typeWithAnnotations });
				}
			}
			ImmutableArray<CustomModifier> immutableArray = ImmutableArray<CustomModifier>.Empty;
			if (refKind != RefKind.None)
			{
				immutableArray = instance.ToImmutableAndFree();
			}
			else
			{
				typeWithAnnotations = typeWithAnnotations.WithModifiers(instance.ToImmutableAndFree());
			}
			return new FunctionPointerMethodSymbol(callingConvention, refKind, typeWithAnnotations, immutableArray, syntax, typeBinder, diagnostics, suppressUseSiteDiagnostics);
		}

		// Token: 0x06006A00 RID: 27136 RVA: 0x0018B45A File Offset: 0x0018965A
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal static FunctionPointerMethodSymbol CreateFromPartsForTest(CallingConvention callingConvention, TypeWithAnnotations returnType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers, RefKind returnRefKind, ImmutableArray<TypeWithAnnotations> parameterTypes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ImmutableArray<CustomModifier>> parameterRefCustomModifiers, ImmutableArray<RefKind> parameterRefKinds, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation)
		{
			return new FunctionPointerMethodSymbol(callingConvention, returnRefKind, returnType, refCustomModifiers, parameterTypes, parameterRefCustomModifiers, parameterRefKinds, compilation);
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x0018B470 File Offset: 0x00189670
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal static FunctionPointerMethodSymbol CreateFromParts(CallingConvention callingConvention, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> callingConventionModifiers, TypeWithAnnotations returnTypeWithAnnotations, RefKind returnRefKind, ImmutableArray<TypeWithAnnotations> parameterTypes, ImmutableArray<RefKind> parameterRefKinds, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation)
		{
			ArrayBuilder<CustomModifier> instance = ArrayBuilder<CustomModifier>.GetInstance();
			if (!callingConventionModifiers.IsDefaultOrEmpty)
			{
				instance.AddRange(callingConventionModifiers);
			}
			ImmutableArray<CustomModifier> immutableArray;
			if (returnRefKind == RefKind.None)
			{
				immutableArray = ImmutableArray<CustomModifier>.Empty;
				returnTypeWithAnnotations = returnTypeWithAnnotations.WithModifiers(instance.ToImmutableAndFree());
			}
			else
			{
				CustomModifier customModifierForRefKind = FunctionPointerMethodSymbol.GetCustomModifierForRefKind(returnRefKind, compilation);
				if (customModifierForRefKind != null)
				{
					instance.Add(customModifierForRefKind);
				}
				immutableArray = instance.ToImmutableAndFree();
			}
			return new FunctionPointerMethodSymbol(callingConvention, returnRefKind, returnTypeWithAnnotations, immutableArray, parameterTypes, default(ImmutableArray<ImmutableArray<CustomModifier>>), parameterRefKinds, compilation);
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x0018B4E0 File Offset: 0x001896E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static CustomModifier GetCustomModifierForRefKind(RefKind refKind, CSharpCompilation compilation)
		{
			NamedTypeSymbol namedTypeSymbol;
			if (refKind != RefKind.Out)
			{
				if (refKind == RefKind.In)
				{
					namedTypeSymbol = compilation.GetWellKnownType(WellKnownType.System_Runtime_InteropServices_InAttribute);
				}
				else
				{
					namedTypeSymbol = null;
				}
			}
			else
			{
				namedTypeSymbol = compilation.GetWellKnownType(WellKnownType.System_Runtime_InteropServices_OutAttribute);
			}
			NamedTypeSymbol namedTypeSymbol2 = namedTypeSymbol;
			if (namedTypeSymbol2 == null)
			{
				return null;
			}
			return CSharpCustomModifier.CreateRequired(namedTypeSymbol2);
		}

		// Token: 0x06006A03 RID: 27139 RVA: 0x0018B520 File Offset: 0x00189720
		public static FunctionPointerMethodSymbol CreateFromMetadata(CallingConvention callingConvention, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ParamInfo<TypeSymbol>> retAndParamTypes)
		{
			return new FunctionPointerMethodSymbol(callingConvention, retAndParamTypes);
		}

		// Token: 0x06006A04 RID: 27140 RVA: 0x0018B529 File Offset: 0x00189729
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public FunctionPointerMethodSymbol SubstituteParameterSymbols(TypeWithAnnotations substitutedReturnType, ImmutableArray<TypeWithAnnotations> substitutedParameterTypes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers = default(ImmutableArray<CustomModifier>), [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ImmutableArray<CustomModifier>> paramRefCustomModifiers = default(ImmutableArray<ImmutableArray<CustomModifier>>))
		{
			return new FunctionPointerMethodSymbol(this.CallingConvention, this.RefKind, substitutedReturnType, refCustomModifiers.IsDefault ? this.RefCustomModifiers : refCustomModifiers, this.Parameters, substitutedParameterTypes, paramRefCustomModifiers);
		}

		// Token: 0x06006A05 RID: 27141 RVA: 0x0018B558 File Offset: 0x00189758
		internal FunctionPointerMethodSymbol MergeEquivalentTypes(FunctionPointerMethodSymbol signature, VarianceKind variance)
		{
			VarianceKind varianceKind = ((this.RefKind == RefKind.None) ? variance : VarianceKind.None);
			TypeWithAnnotations typeWithAnnotations = this.ReturnTypeWithAnnotations.MergeEquivalentTypes(signature.ReturnTypeWithAnnotations, varianceKind);
			ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray<TypeWithAnnotations>.Empty;
			bool flag = false;
			if (this._parameters.Length > 0)
			{
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(this._parameters.Length);
				int i = 0;
				while (i < this._parameters.Length)
				{
					FunctionPointerParameterSymbol functionPointerParameterSymbol = this._parameters[i];
					FunctionPointerParameterSymbol functionPointerParameterSymbol2 = signature._parameters[i];
					RefKind refKind = functionPointerParameterSymbol.RefKind;
					VarianceKind varianceKind2;
					if (variance != VarianceKind.Out)
					{
						if (variance != VarianceKind.In)
						{
							goto IL_00AE;
						}
						if (refKind != RefKind.None)
						{
							goto IL_00AE;
						}
						varianceKind2 = VarianceKind.Out;
					}
					else
					{
						if (refKind != RefKind.None)
						{
							goto IL_00AE;
						}
						varianceKind2 = VarianceKind.In;
					}
					IL_00B1:
					VarianceKind varianceKind3 = varianceKind2;
					TypeWithAnnotations typeWithAnnotations2 = functionPointerParameterSymbol.TypeWithAnnotations.MergeEquivalentTypes(functionPointerParameterSymbol2.TypeWithAnnotations, varianceKind3);
					instance.Add(typeWithAnnotations2);
					if (!typeWithAnnotations2.IsSameAs(functionPointerParameterSymbol.TypeWithAnnotations))
					{
						flag = true;
					}
					i++;
					continue;
					IL_00AE:
					varianceKind2 = VarianceKind.None;
					goto IL_00B1;
				}
				if (flag)
				{
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					instance.Free();
					immutableArray = base.ParameterTypesWithAnnotations;
				}
			}
			if (flag || !typeWithAnnotations.IsSameAs(this.ReturnTypeWithAnnotations))
			{
				return this.SubstituteParameterSymbols(typeWithAnnotations, immutableArray, default(ImmutableArray<CustomModifier>), default(ImmutableArray<ImmutableArray<CustomModifier>>));
			}
			return this;
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x0018B6B8 File Offset: 0x001898B8
		public FunctionPointerMethodSymbol SetNullabilityForReferenceTypes(Func<TypeWithAnnotations, TypeWithAnnotations> transform)
		{
			TypeWithAnnotations typeWithAnnotations = transform.Invoke(this.ReturnTypeWithAnnotations);
			ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray<TypeWithAnnotations>.Empty;
			bool flag = false;
			if (this._parameters.Length > 0)
			{
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(this._parameters.Length);
				foreach (FunctionPointerParameterSymbol functionPointerParameterSymbol in this._parameters)
				{
					TypeWithAnnotations typeWithAnnotations2 = transform.Invoke(functionPointerParameterSymbol.TypeWithAnnotations);
					instance.Add(typeWithAnnotations2);
					if (!typeWithAnnotations2.IsSameAs(functionPointerParameterSymbol.TypeWithAnnotations))
					{
						flag = true;
					}
				}
				if (flag)
				{
					immutableArray = instance.ToImmutableAndFree();
				}
				else
				{
					instance.Free();
					immutableArray = base.ParameterTypesWithAnnotations;
				}
			}
			if (flag || !typeWithAnnotations.IsSameAs(this.ReturnTypeWithAnnotations))
			{
				return this.SubstituteParameterSymbols(typeWithAnnotations, immutableArray, default(ImmutableArray<CustomModifier>), default(ImmutableArray<ImmutableArray<CustomModifier>>));
			}
			return this;
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x0018B79C File Offset: 0x0018999C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private FunctionPointerMethodSymbol(CallingConvention callingConvention, RefKind refKind, TypeWithAnnotations returnType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> originalParameters, ImmutableArray<TypeWithAnnotations> substitutedParameterTypes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ImmutableArray<CustomModifier>> substitutedRefCustomModifiers)
		{
			this.RefCustomModifiers = refCustomModifiers;
			this.CallingConvention = callingConvention;
			this.RefKind = refKind;
			this.ReturnTypeWithAnnotations = returnType;
			if (originalParameters.Length > 0)
			{
				ArrayBuilder<FunctionPointerParameterSymbol> instance = ArrayBuilder<FunctionPointerParameterSymbol>.GetInstance(originalParameters.Length);
				for (int i = 0; i < originalParameters.Length; i++)
				{
					ParameterSymbol parameterSymbol = originalParameters[i];
					TypeWithAnnotations typeWithAnnotations = substitutedParameterTypes[i];
					ImmutableArray<CustomModifier> immutableArray = (substitutedRefCustomModifiers.IsDefault ? parameterSymbol.RefCustomModifiers : substitutedRefCustomModifiers[i]);
					instance.Add(new FunctionPointerParameterSymbol(typeWithAnnotations, parameterSymbol.RefKind, parameterSymbol.Ordinal, this, immutableArray));
				}
				this._parameters = instance.ToImmutableAndFree();
				return;
			}
			this._parameters = ImmutableArray<FunctionPointerParameterSymbol>.Empty;
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x0018B858 File Offset: 0x00189A58
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private FunctionPointerMethodSymbol(CallingConvention callingConvention, RefKind refKind, TypeWithAnnotations returnTypeWithAnnotations, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers, ImmutableArray<TypeWithAnnotations> parameterTypes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ImmutableArray<CustomModifier>> parameterRefCustomModifiers, ImmutableArray<RefKind> parameterRefKinds, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation)
		{
			this.RefCustomModifiers = (refCustomModifiers.IsDefault ? FunctionPointerMethodSymbol.<.ctor>g__getCustomModifierArrayForRefKind|11_1(refKind, compilation) : refCustomModifiers);
			this.RefKind = refKind;
			this.CallingConvention = callingConvention;
			this.ReturnTypeWithAnnotations = returnTypeWithAnnotations;
			this._parameters = parameterTypes.ZipAsArray(parameterRefKinds, new ValueTuple<FunctionPointerMethodSymbol, CSharpCompilation, ImmutableArray<ImmutableArray<CustomModifier>>>(this, compilation, parameterRefCustomModifiers), delegate(TypeWithAnnotations type, RefKind refKind, int i, [TupleElementNames(new string[] { "Method", "Comp", "ParamRefCustomModifiers" })] ValueTuple<FunctionPointerMethodSymbol, CSharpCompilation, ImmutableArray<ImmutableArray<CustomModifier>>> arg)
			{
				ImmutableArray<CustomModifier> immutableArray = (arg.Item3.IsDefault ? FunctionPointerMethodSymbol.<.ctor>g__getCustomModifierArrayForRefKind|11_1(refKind, arg.Item2) : arg.Item3[i]);
				return new FunctionPointerParameterSymbol(type, refKind, i, arg.Item1, immutableArray);
			});
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x0018B8D4 File Offset: 0x00189AD4
		private FunctionPointerMethodSymbol(CallingConvention callingConvention, RefKind refKind, TypeWithAnnotations returnType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers, FunctionPointerTypeSyntax syntax, Binder typeBinder, BindingDiagnosticBag diagnostics, bool suppressUseSiteDiagnostics)
		{
			this.RefCustomModifiers = refCustomModifiers;
			this.CallingConvention = callingConvention;
			this.RefKind = refKind;
			this.ReturnTypeWithAnnotations = returnType;
			this._parameters = ((syntax.ParameterList.Parameters.Count > 1) ? ParameterHelpers.MakeFunctionPointerParameters(typeBinder, this, syntax.ParameterList.Parameters, diagnostics, suppressUseSiteDiagnostics) : ImmutableArray<FunctionPointerParameterSymbol>.Empty);
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x0018B940 File Offset: 0x00189B40
		private FunctionPointerMethodSymbol(CallingConvention callingConvention, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ParamInfo<TypeSymbol>> retAndParamTypes)
		{
			ParamInfo<TypeSymbol> paramInfo = retAndParamTypes[0];
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(paramInfo.Type, NullableAnnotation.Oblivious, CSharpCustomModifier.Convert(paramInfo.CustomModifiers));
			this.RefCustomModifiers = CSharpCustomModifier.Convert(paramInfo.RefCustomModifiers);
			this.CallingConvention = callingConvention;
			this.ReturnTypeWithAnnotations = typeWithAnnotations;
			this.RefKind = FunctionPointerMethodSymbol.<.ctor>g__getRefKind|13_1(paramInfo, this.RefCustomModifiers, RefKind.In, RefKind.Ref);
			ReadOnlySpan<ParamInfo<TypeSymbol>> readOnlySpan = retAndParamTypes.AsSpan();
			int length = readOnlySpan.Length;
			int num = 1;
			int num2 = length - num;
			this._parameters = FunctionPointerMethodSymbol.<.ctor>g__makeParametersFromMetadata|13_0(readOnlySpan.Slice(num, num2), this);
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x0018B9D4 File Offset: 0x00189BD4
		internal void AddNullableTransforms(ArrayBuilder<byte> transforms)
		{
			this.ReturnTypeWithAnnotations.AddNullableTransforms(transforms);
			foreach (ParameterSymbol parameterSymbol in this.Parameters)
			{
				parameterSymbol.TypeWithAnnotations.AddNullableTransforms(transforms);
			}
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x0018BA20 File Offset: 0x00189C20
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal FunctionPointerMethodSymbol ApplyNullableTransforms(byte defaultTransformFlag, ImmutableArray<byte> transforms, ref int position)
		{
			TypeWithAnnotations typeWithAnnotations;
			bool flag = this.ReturnTypeWithAnnotations.ApplyNullableTransforms(defaultTransformFlag, transforms, ref position, out typeWithAnnotations);
			ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray<TypeWithAnnotations>.Empty;
			if (!this.Parameters.IsEmpty)
			{
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(this.Parameters.Length);
				bool flag2 = false;
				foreach (ParameterSymbol parameterSymbol in this.Parameters)
				{
					TypeWithAnnotations typeWithAnnotations2;
					flag2 |= parameterSymbol.TypeWithAnnotations.ApplyNullableTransforms(defaultTransformFlag, transforms, ref position, out typeWithAnnotations2);
					instance.Add(typeWithAnnotations2);
				}
				if (flag2)
				{
					immutableArray = instance.ToImmutableAndFree();
					flag = true;
				}
				else
				{
					instance.Free();
					immutableArray = base.ParameterTypesWithAnnotations;
				}
			}
			if (flag)
			{
				return this.SubstituteParameterSymbols(typeWithAnnotations, immutableArray, default(ImmutableArray<CustomModifier>), default(ImmutableArray<ImmutableArray<CustomModifier>>));
			}
			return this;
		}

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x06006A0D RID: 27149 RVA: 0x0018BAF8 File Offset: 0x00189CF8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> UnmanagedCallingConventionTypes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (!this.CallingConvention.IsCallingConvention(CallingConvention.Unmanaged))
				{
					return ImmutableArray<NamedTypeSymbol>.Empty;
				}
				ImmutableArray<CustomModifier> immutableArray = ((this.RefKind != RefKind.None) ? this.RefCustomModifiers : this.ReturnTypeWithAnnotations.CustomModifiers);
				if (immutableArray.IsEmpty)
				{
					return ImmutableArray<NamedTypeSymbol>.Empty;
				}
				ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance(immutableArray.Length);
				foreach (CustomModifier customModifier in immutableArray)
				{
					CSharpCustomModifier csharpCustomModifier = (CSharpCustomModifier)customModifier;
					if (FunctionPointerTypeSymbol.IsCallingConventionModifier(csharpCustomModifier.ModifierSymbol))
					{
						instance.Add(csharpCustomModifier.ModifierSymbol);
					}
				}
				return instance.ToImmutableAndFree();
			}
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x0018BB98 File Offset: 0x00189D98
		public ImmutableHashSet<CustomModifier> GetCallingConventionModifiers()
		{
			if (this._lazyCallingConventionModifiers == null)
			{
				ImmutableArray<CustomModifier> immutableArray = ((this.RefKind != RefKind.None) ? this.RefCustomModifiers : this.ReturnTypeWithAnnotations.CustomModifiers);
				if (immutableArray.IsEmpty || this.CallingConvention != CallingConvention.Unmanaged)
				{
					this._lazyCallingConventionModifiers = ImmutableHashSet<CustomModifier>.Empty;
				}
				else
				{
					PooledHashSet<CustomModifier> instance = PooledHashSet<CustomModifier>.GetInstance();
					foreach (CustomModifier customModifier in immutableArray)
					{
						if (FunctionPointerTypeSymbol.IsCallingConventionModifier(((CSharpCustomModifier)customModifier).ModifierSymbol))
						{
							instance.Add(customModifier);
						}
					}
					if (instance.Count == 0)
					{
						this._lazyCallingConventionModifiers = ImmutableHashSet<CustomModifier>.Empty;
					}
					else
					{
						this._lazyCallingConventionModifiers = instance.ToImmutableHashSet<CustomModifier>();
					}
					instance.Free();
				}
			}
			return this._lazyCallingConventionModifiers;
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x0018BC5C File Offset: 0x00189E5C
		public override bool Equals(Symbol other, TypeCompareKind compareKind)
		{
			FunctionPointerMethodSymbol functionPointerMethodSymbol = other as FunctionPointerMethodSymbol;
			return functionPointerMethodSymbol != null && this.Equals(functionPointerMethodSymbol, compareKind);
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x0018BC80 File Offset: 0x00189E80
		internal bool Equals(FunctionPointerMethodSymbol other, TypeCompareKind compareKind)
		{
			if (this == other)
			{
				return true;
			}
			if (this.EqualsNoParameters(other, compareKind))
			{
				return this._parameters.SequenceEqual(other._parameters, compareKind, (FunctionPointerParameterSymbol param1, FunctionPointerParameterSymbol param2, TypeCompareKind compareKind) => param1.MethodEqualityChecks(param2, compareKind));
			}
			return false;
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x0018BCD0 File Offset: 0x00189ED0
		private bool EqualsNoParameters(FunctionPointerMethodSymbol other, TypeCompareKind compareKind)
		{
			if (this.CallingConvention != other.CallingConvention || !FunctionPointerTypeSymbol.RefKindEquals(compareKind, this.RefKind, other.RefKind) || !this.ReturnTypeWithAnnotations.Equals(other.ReturnTypeWithAnnotations, compareKind))
			{
				return false;
			}
			if ((compareKind & TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds) != TypeCompareKind.ConsiderEverything)
			{
				if (this.CallingConvention.IsCallingConvention(CallingConvention.Unmanaged) && !this.GetCallingConventionModifiers().SetEquals(other.GetCallingConventionModifiers()))
				{
					return false;
				}
			}
			else if (!this.RefCustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(other.RefCustomModifiers, null))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x0018BD58 File Offset: 0x00189F58
		public override int GetHashCode()
		{
			int num = this.GetHashCodeNoParameters();
			foreach (FunctionPointerParameterSymbol functionPointerParameterSymbol in this._parameters)
			{
				num = Hash.Combine(functionPointerParameterSymbol.MethodHashCode(), num);
			}
			return num;
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x0018BD9C File Offset: 0x00189F9C
		internal int GetHashCodeNoParameters()
		{
			return Hash.Combine<TypeSymbol>(base.ReturnType, Hash.Combine(this.CallingConvention.GetHashCode(), FunctionPointerTypeSymbol.GetRefKindForHashCode(this.RefKind).GetHashCode()));
		}

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x06006A14 RID: 27156 RVA: 0x0018BDE6 File Offset: 0x00189FE6
		internal override CallingConvention CallingConvention { get; }

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x06006A15 RID: 27157 RVA: 0x0018BDF0 File Offset: 0x00189FF0
		public override bool ReturnsVoid
		{
			get
			{
				return this.ReturnTypeWithAnnotations.IsVoidType();
			}
		}

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x06006A16 RID: 27158 RVA: 0x0018BE0B File Offset: 0x0018A00B
		public override RefKind RefKind { get; }

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x06006A17 RID: 27159 RVA: 0x0018BE13 File Offset: 0x0018A013
		public override TypeWithAnnotations ReturnTypeWithAnnotations { get; }

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x06006A18 RID: 27160 RVA: 0x0018BE1B File Offset: 0x0018A01B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._parameters.Cast<FunctionPointerParameterSymbol, ParameterSymbol>();
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x06006A19 RID: 27161 RVA: 0x0018BE28 File Offset: 0x0018A028
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x06006A1A RID: 27162 RVA: 0x0018BE30 File Offset: 0x0018A030
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.FunctionPointerSignature;
			}
		}

		// Token: 0x06006A1B RID: 27163 RVA: 0x0018BE34 File Offset: 0x0018A034
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			UseSiteInfo<AssemblySymbol> useSiteInfo = default(UseSiteInfo<AssemblySymbol>);
			base.CalculateUseSiteDiagnostic(ref useSiteInfo);
			if (this.CallingConvention.IsCallingConvention(CallingConvention.ExtraArguments))
			{
				base.MergeUseSiteInfo(ref useSiteInfo, new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_UnsupportedCallingConvention, new object[] { this })));
			}
			return useSiteInfo;
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x0018BE83 File Offset: 0x0018A083
		internal bool GetUnificationUseSiteDiagnosticRecursive([global::System.Runtime.CompilerServices.Nullable(2)] ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			return base.ReturnType.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref result, this.RefCustomModifiers, owner, ref checkedTypes) || Symbol.GetUnificationUseSiteDiagnosticRecursive(ref result, this.Parameters, owner, ref checkedTypes);
		}

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x06006A1D RID: 27165 RVA: 0x0018BEB5 File Offset: 0x0018A0B5
		public override bool IsVararg
		{
			get
			{
				return this.CallingConvention.IsCallingConvention(CallingConvention.ExtraArguments);
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x06006A1E RID: 27166 RVA: 0x0018BEC3 File Offset: 0x0018A0C3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x06006A1F RID: 27167 RVA: 0x0018BEC6 File Offset: 0x0018A0C6
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x06006A20 RID: 27168 RVA: 0x0018BEC9 File Offset: 0x0018A0C9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x06006A21 RID: 27169 RVA: 0x0018BED0 File Offset: 0x0018A0D0
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x06006A22 RID: 27170 RVA: 0x0018BED3 File Offset: 0x0018A0D3
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x06006A23 RID: 27171 RVA: 0x0018BED6 File Offset: 0x0018A0D6
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x06006A24 RID: 27172 RVA: 0x0018BED9 File Offset: 0x0018A0D9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x06006A25 RID: 27173 RVA: 0x0018BEE0 File Offset: 0x0018A0E0
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol AssociatedSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x06006A26 RID: 27174 RVA: 0x0018BEE3 File Offset: 0x0018A0E3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x06006A27 RID: 27175 RVA: 0x0018BEEA File Offset: 0x0018A0EA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001B2E RID: 6958
		// (get) Token: 0x06006A28 RID: 27176 RVA: 0x0018BEF1 File Offset: 0x0018A0F1
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001B2F RID: 6959
		// (get) Token: 0x06006A29 RID: 27177 RVA: 0x0018BEF4 File Offset: 0x0018A0F4
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x06006A2A RID: 27178 RVA: 0x0018BEF7 File Offset: 0x0018A0F7
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x06006A2B RID: 27179 RVA: 0x0018BEFA File Offset: 0x0018A0FA
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x06006A2C RID: 27180 RVA: 0x0018BEFD File Offset: 0x0018A0FD
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x06006A2D RID: 27181 RVA: 0x0018BF00 File Offset: 0x0018A100
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x06006A2E RID: 27182 RVA: 0x0018BF03 File Offset: 0x0018A103
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x06006A2F RID: 27183 RVA: 0x0018BF06 File Offset: 0x0018A106
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x06006A30 RID: 27184 RVA: 0x0018BF09 File Offset: 0x0018A109
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x06006A31 RID: 27185 RVA: 0x0018BF10 File Offset: 0x0018A110
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x06006A32 RID: 27186 RVA: 0x0018BF13 File Offset: 0x0018A113
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x06006A33 RID: 27187 RVA: 0x0018BF16 File Offset: 0x0018A116
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x06006A34 RID: 27188 RVA: 0x0018BF19 File Offset: 0x0018A119
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x06006A35 RID: 27189 RVA: 0x0018BF1C File Offset: 0x0018A11C
		internal override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x06006A36 RID: 27190 RVA: 0x0018BF1F File Offset: 0x0018A11F
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x06006A37 RID: 27191 RVA: 0x0018BF22 File Offset: 0x0018A122
		internal override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006A38 RID: 27192 RVA: 0x0018BF25 File Offset: 0x0018A125
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x06006A39 RID: 27193 RVA: 0x0018BF2C File Offset: 0x0018A12C
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x06006A3A RID: 27194 RVA: 0x0018BF2F File Offset: 0x0018A12F
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x06006A3B RID: 27195 RVA: 0x0018BF36 File Offset: 0x0018A136
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x0018BF39 File Offset: 0x0018A139
		internal override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x0018BF3C File Offset: 0x0018A13C
		internal override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x0018BF3F File Offset: 0x0018A13F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return null;
		}

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x06006A3F RID: 27199 RVA: 0x0018BF42 File Offset: 0x0018A142
		internal override bool GenerateDebugInfo
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x06006A40 RID: 27200 RVA: 0x0018BF49 File Offset: 0x0018A149
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x06006A41 RID: 27201 RVA: 0x0018BF50 File Offset: 0x0018A150
		public override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x0018BF57 File Offset: 0x0018A157
		public override DllImportData GetDllImportData()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x0018BF5E File Offset: 0x0018A15E
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x0018BF65 File Offset: 0x0018A165
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x0018BF6C File Offset: 0x0018A16C
		internal sealed override bool IsNullableAnalysisEnabled()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x0018BF74 File Offset: 0x0018A174
		[CompilerGenerated]
		internal static CallingConvention <CreateFromSource>g__getCallingConvention|2_0(CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerCallingConventionSyntax callingConventionSyntax, ArrayBuilder<CustomModifier> customModifiers, BindingDiagnosticBag diagnostics)
		{
			SyntaxKind? syntaxKind = ((callingConventionSyntax != null) ? new SyntaxKind?(callingConventionSyntax.ManagedOrUnmanagedKeyword.Kind()) : default(SyntaxKind?));
			if (syntaxKind == null)
			{
				return CallingConvention.Default;
			}
			SyntaxKind valueOrDefault = syntaxKind.GetValueOrDefault();
			if (valueOrDefault == SyntaxKind.ManagedKeyword)
			{
				if (callingConventionSyntax.UnmanagedCallingConventionList != null && !callingConventionSyntax.ContainsDiagnostics)
				{
					diagnostics.Add(ErrorCode.ERR_CannotSpecifyManagedWithUnmanagedSpecifiers, callingConventionSyntax.UnmanagedCallingConventionList.GetLocation());
				}
				return CallingConvention.Default;
			}
			if (valueOrDefault != SyntaxKind.UnmanagedKeyword)
			{
				throw ExceptionUtilities.UnexpectedValue(syntaxKind);
			}
			FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList = callingConventionSyntax.UnmanagedCallingConventionList;
			if (unmanagedCallingConventionList == null)
			{
				FunctionPointerMethodSymbol.<CreateFromSource>g__checkUnmanagedSupport|2_3(compilation, callingConventionSyntax.ManagedOrUnmanagedKeyword.GetLocation(), diagnostics);
				return CallingConvention.Unmanaged;
			}
			SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions = unmanagedCallingConventionList.CallingConventions;
			int count = callingConventions.Count;
			if (count == 0)
			{
				if (!unmanagedCallingConventionList.ContainsDiagnostics)
				{
					diagnostics.Add(ErrorCode.ERR_InvalidFunctionPointerCallingConvention, unmanagedCallingConventionList.OpenBracketToken.GetLocation(), new object[] { "" });
				}
				return CallingConvention.Default;
			}
			if (count != 1)
			{
				SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> separatedSyntaxList = callingConventions;
				FunctionPointerMethodSymbol.<CreateFromSource>g__checkUnmanagedSupport|2_3(compilation, callingConventionSyntax.ManagedOrUnmanagedKeyword.GetLocation(), diagnostics);
				foreach (FunctionPointerUnmanagedCallingConventionSyntax functionPointerUnmanagedCallingConventionSyntax in separatedSyntaxList)
				{
					CustomModifier customModifier = FunctionPointerMethodSymbol.<CreateFromSource>g__handleIndividualUnrecognizedSpecifier|2_2(functionPointerUnmanagedCallingConventionSyntax, compilation, diagnostics);
					if (customModifier != null)
					{
						customModifiers.Add(customModifier);
					}
				}
				return CallingConvention.Unmanaged;
			}
			string valueText = callingConventions[0].Name.ValueText;
			CallingConvention callingConvention;
			if (!(valueText == "Cdecl"))
			{
				if (!(valueText == "Stdcall"))
				{
					if (!(valueText == "Thiscall"))
					{
						if (!(valueText == "Fastcall"))
						{
							callingConvention = FunctionPointerMethodSymbol.<CreateFromSource>g__handleSingleConvention|2_1(callingConventions[0], compilation, customModifiers, diagnostics);
						}
						else
						{
							callingConvention = CallingConvention.FastCall;
						}
					}
					else
					{
						callingConvention = CallingConvention.ThisCall;
					}
				}
				else
				{
					callingConvention = CallingConvention.Standard;
				}
			}
			else
			{
				callingConvention = CallingConvention.CDecl;
			}
			return callingConvention;
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x0018C140 File Offset: 0x0018A340
		[CompilerGenerated]
		internal static CallingConvention <CreateFromSource>g__handleSingleConvention|2_1(FunctionPointerUnmanagedCallingConventionSyntax specifier, CSharpCompilation compilation, ArrayBuilder<CustomModifier> customModifiers, BindingDiagnosticBag diagnostics)
		{
			FunctionPointerMethodSymbol.<CreateFromSource>g__checkUnmanagedSupport|2_3(compilation, specifier.GetLocation(), diagnostics);
			CustomModifier customModifier = FunctionPointerMethodSymbol.<CreateFromSource>g__handleIndividualUnrecognizedSpecifier|2_2(specifier, compilation, diagnostics);
			if (customModifier != null)
			{
				customModifiers.Add(customModifier);
			}
			return CallingConvention.Unmanaged;
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x0018C170 File Offset: 0x0018A370
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static CustomModifier <CreateFromSource>g__handleIndividualUnrecognizedSpecifier|2_2(FunctionPointerUnmanagedCallingConventionSyntax specifier, CSharpCompilation compilation, BindingDiagnosticBag diagnostics)
		{
			string valueText = specifier.Name.ValueText;
			if (string.IsNullOrEmpty(valueText))
			{
				return null;
			}
			string text = "CallConv" + valueText;
			MetadataTypeName metadataTypeName = MetadataTypeName.FromNamespaceAndTypeName("System.Runtime.CompilerServices", text, true, 0);
			NamedTypeSymbol namedTypeSymbol = compilation.Assembly.CorLibrary.LookupTopLevelMetadataType(ref metadataTypeName, false);
			if (namedTypeSymbol is MissingMetadataTypeSymbol)
			{
				namedTypeSymbol = new MissingMetadataTypeSymbol.TopLevel(namedTypeSymbol.ContainingModule, ref metadataTypeName, new CSDiagnosticInfo(ErrorCode.ERR_TypeNotFound, new object[] { text }));
			}
			else if (namedTypeSymbol.DeclaredAccessibility != Accessibility.Public)
			{
				diagnostics.Add(ErrorCode.ERR_TypeMustBePublic, specifier.GetLocation(), new object[] { namedTypeSymbol });
			}
			diagnostics.Add(namedTypeSymbol.GetUseSiteInfo(), specifier.GetLocation());
			return CSharpCustomModifier.CreateOptional(namedTypeSymbol);
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x0018C22F File Offset: 0x0018A42F
		[CompilerGenerated]
		internal static void <CreateFromSource>g__checkUnmanagedSupport|2_3(CSharpCompilation compilation, Location errorLocation, BindingDiagnosticBag diagnostics)
		{
			if (!compilation.Assembly.RuntimeSupportsUnmanagedSignatureCallingConvention)
			{
				diagnostics.Add(ErrorCode.ERR_RuntimeDoesNotSupportUnmanagedDefaultCallConv, errorLocation);
			}
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x0018C24C File Offset: 0x0018A44C
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<CustomModifier> <.ctor>g__getCustomModifierArrayForRefKind|11_1(RefKind refKind, CSharpCompilation compilation)
		{
			CustomModifier customModifierForRefKind = FunctionPointerMethodSymbol.GetCustomModifierForRefKind(refKind, compilation);
			if (customModifierForRefKind == null)
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
			return ImmutableArray.Create<CustomModifier>(customModifierForRefKind);
		}

		// Token: 0x06006A4B RID: 27211 RVA: 0x0018C270 File Offset: 0x0018A470
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal unsafe static ImmutableArray<FunctionPointerParameterSymbol> <.ctor>g__makeParametersFromMetadata|13_0([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ReadOnlySpan<ParamInfo<TypeSymbol>> parameterTypes, FunctionPointerMethodSymbol parent)
		{
			if (parameterTypes.Length > 0)
			{
				ArrayBuilder<FunctionPointerParameterSymbol> instance = ArrayBuilder<FunctionPointerParameterSymbol>.GetInstance(parameterTypes.Length);
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					ParamInfo<TypeSymbol> paramInfo = *parameterTypes[i];
					ImmutableArray<CustomModifier> immutableArray = CSharpCustomModifier.Convert(paramInfo.RefCustomModifiers);
					TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(paramInfo.Type, NullableAnnotation.Oblivious, CSharpCustomModifier.Convert(paramInfo.CustomModifiers));
					RefKind refKind = FunctionPointerMethodSymbol.<.ctor>g__getRefKind|13_1(paramInfo, immutableArray, RefKind.In, RefKind.Out);
					instance.Add(new FunctionPointerParameterSymbol(typeWithAnnotations, refKind, i, parent, immutableArray));
				}
				return instance.ToImmutableAndFree();
			}
			return ImmutableArray<FunctionPointerParameterSymbol>.Empty;
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x0018C304 File Offset: 0x0018A504
		[CompilerGenerated]
		internal static RefKind <.ctor>g__getRefKind|13_1([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ParamInfo<TypeSymbol> param, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> paramRefCustomMods, RefKind hasInRefKind, RefKind hasOutRefKind)
		{
			RefKind refKind;
			if (!param.IsByRef)
			{
				refKind = RefKind.None;
			}
			else if (paramRefCustomMods.HasInAttributeModifier())
			{
				refKind = hasInRefKind;
			}
			else if (paramRefCustomMods.HasOutAttributeModifier())
			{
				refKind = hasOutRefKind;
			}
			else
			{
				refKind = RefKind.Ref;
			}
			return refKind;
		}

		// Token: 0x04001A6A RID: 6762
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<FunctionPointerParameterSymbol> _parameters;

		// Token: 0x04001A6B RID: 6763
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private ImmutableHashSet<CustomModifier> _lazyCallingConventionModifiers;
	}
}
