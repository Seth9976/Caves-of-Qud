using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000511 RID: 1297
	internal sealed class ReducedExtensionMethodSymbol : MethodSymbol
	{
		// Token: 0x06006CD7 RID: 27863 RVA: 0x00191F1C File Offset: 0x0019011C
		public static MethodSymbol Create(MethodSymbol method, TypeSymbol receiverType, CSharpCompilation compilation)
		{
			CompoundUseSiteInfo<AssemblySymbol> discardedDependencies = CompoundUseSiteInfo<AssemblySymbol>.DiscardedDependencies;
			method = ReducedExtensionMethodSymbol.InferExtensionMethodTypeArguments(method, receiverType, compilation, ref discardedDependencies);
			if (method == null)
			{
				return null;
			}
			if (!new TypeConversions(method.ContainingAssembly.CorLibrary, false).ConvertExtensionMethodThisArg(method.Parameters[0].Type, receiverType, ref discardedDependencies).Exists)
			{
				return null;
			}
			if (discardedDependencies.Diagnostics != null)
			{
				using (IEnumerator<DiagnosticInfo> enumerator = discardedDependencies.Diagnostics.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Severity == DiagnosticSeverity.Error)
						{
							return null;
						}
					}
				}
			}
			return ReducedExtensionMethodSymbol.Create(method);
		}

		// Token: 0x06006CD8 RID: 27864 RVA: 0x00191FD4 File Offset: 0x001901D4
		public static MethodSymbol Create(MethodSymbol method)
		{
			MethodSymbol constructedFrom = method.ConstructedFrom;
			ReducedExtensionMethodSymbol reducedExtensionMethodSymbol = new ReducedExtensionMethodSymbol(constructedFrom);
			if (constructedFrom == method)
			{
				return reducedExtensionMethodSymbol;
			}
			return reducedExtensionMethodSymbol.Construct(method.TypeArgumentsWithAnnotations);
		}

		// Token: 0x06006CD9 RID: 27865 RVA: 0x00192004 File Offset: 0x00190204
		private ReducedExtensionMethodSymbol(MethodSymbol reducedFrom)
		{
			this._reducedFrom = reducedFrom;
			this._typeMap = TypeMap.Empty.WithAlphaRename(reducedFrom, this, out this._typeParameters);
			this._typeArguments = this._typeMap.SubstituteTypes(reducedFrom.TypeArgumentsWithAnnotations);
		}

		// Token: 0x06006CDA RID: 27866 RVA: 0x00192044 File Offset: 0x00190244
		private static MethodSymbol InferExtensionMethodTypeArguments(MethodSymbol method, TypeSymbol thisType, CSharpCompilation compilation, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!method.IsGenericMethod || method != method.ConstructedFrom)
			{
				return method;
			}
			if (thisType.IsDynamic())
			{
				return null;
			}
			AssemblySymbol containingAssembly = method.ContainingAssembly;
			NamespaceSymbol globalNamespace = containingAssembly.GlobalNamespace;
			TypeConversions typeConversions = new TypeConversions(containingAssembly.CorLibrary, false);
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)CSharpSyntaxTree.Dummy.GetRoot(default(CancellationToken));
			BoundLiteral boundLiteral = new BoundLiteral(csharpSyntaxNode, ConstantValue.Bad, thisType)
			{
				WasCompilerGenerated = true
			};
			ExtendedErrorTypeSymbol extendedErrorTypeSymbol = new ExtendedErrorTypeSymbol(globalNamespace, string.Empty, 0, null, false, false);
			BoundLiteral boundLiteral2 = new BoundLiteral(csharpSyntaxNode, ConstantValue.Bad, extendedErrorTypeSymbol)
			{
				WasCompilerGenerated = true
			};
			int parameterCount = method.ParameterCount;
			BoundExpression[] array = new BoundExpression[parameterCount];
			for (int i = 0; i < parameterCount; i++)
			{
				BoundLiteral boundLiteral3 = ((i == 0) ? boundLiteral : boundLiteral2);
				array[i] = boundLiteral3;
			}
			ImmutableArray<TypeWithAnnotations> immutableArray = MethodTypeInferrer.InferTypeArgumentsFromFirstArgument(typeConversions, method, array.AsImmutable<BoundExpression>(), ref useSiteInfo);
			if (immutableArray.IsDefault)
			{
				return null;
			}
			PooledHashSet<TypeParameterSymbol> instance = PooledHashSet<TypeParameterSymbol>.GetInstance();
			ImmutableArray<TypeParameterSymbol> typeParameters = method.TypeParameters;
			ImmutableArray<TypeWithAnnotations> immutableArray2 = immutableArray;
			for (int j = 0; j < immutableArray2.Length; j++)
			{
				if (!immutableArray2[j].HasType)
				{
					int num = j;
					ArrayBuilder<TypeWithAnnotations> instance2 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
					instance2.AddRange(immutableArray2, num);
					while (j < immutableArray2.Length)
					{
						TypeWithAnnotations typeWithAnnotations = immutableArray2[j];
						if (!typeWithAnnotations.HasType)
						{
							instance.Add(typeParameters[j]);
							instance2.Add(TypeWithAnnotations.Create(ErrorTypeSymbol.UnknownResultType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
						}
						else
						{
							instance2.Add(typeWithAnnotations);
						}
						j++;
					}
					immutableArray2 = instance2.ToImmutableAndFree();
					break;
				}
			}
			ArrayBuilder<TypeParameterDiagnosticInfo> instance3 = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			TypeMap typeMap = new TypeMap(typeParameters, immutableArray2, false);
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder = null;
			ConstraintsHelper.CheckConstraintsArgs checkConstraintsArgs = new ConstraintsHelper.CheckConstraintsArgs(compilation, typeConversions, false, NoLocation.Singleton, null, new CompoundUseSiteInfo<AssemblySymbol>(useSiteInfo));
			TypeMap typeMap2 = typeMap;
			ImmutableArray<TypeParameterSymbol> immutableArray3 = typeParameters;
			ImmutableArray<TypeWithAnnotations> immutableArray4 = immutableArray2;
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder2 = instance3;
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder3 = null;
			HashSet<TypeParameterSymbol> hashSet = ((instance.Count > 0) ? instance : null);
			bool flag = method.CheckConstraints(in checkConstraintsArgs, typeMap2, immutableArray3, immutableArray4, arrayBuilder2, arrayBuilder3, ref arrayBuilder, default(BitVector), hashSet);
			instance3.Free();
			instance.Free();
			if (arrayBuilder != null && arrayBuilder.Count > 0)
			{
				foreach (TypeParameterDiagnosticInfo typeParameterDiagnosticInfo in arrayBuilder)
				{
					useSiteInfo.Add(typeParameterDiagnosticInfo.UseSiteInfo);
				}
			}
			if (!flag)
			{
				return null;
			}
			ImmutableArray<TypeWithAnnotations> immutableArray5 = immutableArray;
			if (immutableArray.Any<TypeWithAnnotations>((TypeWithAnnotations t) => !t.HasType))
			{
				immutableArray5 = immutableArray.ZipAsArray<TypeWithAnnotations, TypeParameterSymbol, TypeWithAnnotations>(method.TypeParameters, delegate(TypeWithAnnotations t, TypeParameterSymbol tp)
				{
					if (!t.HasType)
					{
						return TypeWithAnnotations.Create(tp, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					}
					return t;
				});
			}
			return method.Construct(immutableArray5);
		}

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x06006CDB RID: 27867 RVA: 0x00192307 File Offset: 0x00190507
		internal override MethodSymbol CallsiteReducedFromMethod
		{
			get
			{
				return this._reducedFrom.ConstructIfGeneric(this._typeArguments);
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x06006CDC RID: 27868 RVA: 0x0019231C File Offset: 0x0019051C
		public override TypeSymbol ReceiverType
		{
			get
			{
				return this._reducedFrom.Parameters[0].Type;
			}
		}

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x06006CDD RID: 27869 RVA: 0x00192344 File Offset: 0x00190544
		internal override NullableAnnotation ReceiverNullableAnnotation
		{
			get
			{
				return this._reducedFrom.Parameters[0].TypeWithAnnotations.ToPublicAnnotation();
			}
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x0019236F File Offset: 0x0019056F
		public override TypeSymbol GetTypeInferredDuringReduction(TypeParameterSymbol reducedFromTypeParameter)
		{
			if (reducedFromTypeParameter == null)
			{
				throw new ArgumentNullException();
			}
			if (reducedFromTypeParameter.ContainingSymbol != this._reducedFrom)
			{
				throw new ArgumentException();
			}
			return null;
		}

		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x06006CDF RID: 27871 RVA: 0x00192394 File Offset: 0x00190594
		public override MethodSymbol ReducedFrom
		{
			get
			{
				return this._reducedFrom;
			}
		}

		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x06006CE0 RID: 27872 RVA: 0x0019239C File Offset: 0x0019059C
		public override MethodSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x06006CE1 RID: 27873 RVA: 0x0019239F File Offset: 0x0019059F
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return this._typeParameters;
			}
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x06006CE2 RID: 27874 RVA: 0x001923A7 File Offset: 0x001905A7
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return this._typeArguments;
			}
		}

		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x06006CE3 RID: 27875 RVA: 0x001923AF File Offset: 0x001905AF
		internal override CallingConvention CallingConvention
		{
			get
			{
				return this._reducedFrom.CallingConvention;
			}
		}

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x06006CE4 RID: 27876 RVA: 0x001923BC File Offset: 0x001905BC
		public override int Arity
		{
			get
			{
				return this._reducedFrom.Arity;
			}
		}

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x06006CE5 RID: 27877 RVA: 0x001923C9 File Offset: 0x001905C9
		public override string Name
		{
			get
			{
				return this._reducedFrom.Name;
			}
		}

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x06006CE6 RID: 27878 RVA: 0x001923D6 File Offset: 0x001905D6
		internal override bool HasSpecialName
		{
			get
			{
				return this._reducedFrom.HasSpecialName;
			}
		}

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x06006CE7 RID: 27879 RVA: 0x001923E3 File Offset: 0x001905E3
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return this._reducedFrom.ImplementationAttributes;
			}
		}

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x06006CE8 RID: 27880 RVA: 0x001923F0 File Offset: 0x001905F0
		internal override bool RequiresSecurityObject
		{
			get
			{
				return this._reducedFrom.RequiresSecurityObject;
			}
		}

		// Token: 0x06006CE9 RID: 27881 RVA: 0x001923FD File Offset: 0x001905FD
		public override DllImportData GetDllImportData()
		{
			return this._reducedFrom.GetDllImportData();
		}

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x06006CEA RID: 27882 RVA: 0x0019240A File Offset: 0x0019060A
		public override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x06006CEB RID: 27883 RVA: 0x00192411 File Offset: 0x00190611
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return this._reducedFrom.ReturnValueMarshallingInformation;
			}
		}

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x06006CEC RID: 27884 RVA: 0x0019241E File Offset: 0x0019061E
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return this._reducedFrom.HasDeclarativeSecurity;
			}
		}

		// Token: 0x06006CED RID: 27885 RVA: 0x0019242B File Offset: 0x0019062B
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			return this._reducedFrom.GetSecurityInformation();
		}

		// Token: 0x06006CEE RID: 27886 RVA: 0x00192438 File Offset: 0x00190638
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return this._reducedFrom.GetAppliedConditionalSymbols();
		}

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x06006CEF RID: 27887 RVA: 0x00192445 File Offset: 0x00190645
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._reducedFrom.ContainingAssembly;
			}
		}

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x06006CF0 RID: 27888 RVA: 0x00192452 File Offset: 0x00190652
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._reducedFrom.Locations;
			}
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x06006CF1 RID: 27889 RVA: 0x0019245F File Offset: 0x0019065F
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this._reducedFrom.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x0019246C File Offset: 0x0019066C
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._reducedFrom.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x06006CF3 RID: 27891 RVA: 0x0019247C File Offset: 0x0019067C
		public override MethodSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x06006CF4 RID: 27892 RVA: 0x0019247F File Offset: 0x0019067F
		public override bool IsExtern
		{
			get
			{
				return this._reducedFrom.IsExtern;
			}
		}

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x06006CF5 RID: 27893 RVA: 0x0019248C File Offset: 0x0019068C
		public override bool IsSealed
		{
			get
			{
				return this._reducedFrom.IsSealed;
			}
		}

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x06006CF6 RID: 27894 RVA: 0x00192499 File Offset: 0x00190699
		public override bool IsVirtual
		{
			get
			{
				return this._reducedFrom.IsVirtual;
			}
		}

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x06006CF7 RID: 27895 RVA: 0x001924A6 File Offset: 0x001906A6
		public override bool IsAbstract
		{
			get
			{
				return this._reducedFrom.IsAbstract;
			}
		}

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x06006CF8 RID: 27896 RVA: 0x001924B3 File Offset: 0x001906B3
		public override bool IsOverride
		{
			get
			{
				return this._reducedFrom.IsOverride;
			}
		}

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x06006CF9 RID: 27897 RVA: 0x001924C0 File Offset: 0x001906C0
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x06006CFA RID: 27898 RVA: 0x001924C3 File Offset: 0x001906C3
		public override bool IsAsync
		{
			get
			{
				return this._reducedFrom.IsAsync;
			}
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x06006CFB RID: 27899 RVA: 0x001924D0 File Offset: 0x001906D0
		public override bool IsExtensionMethod
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x001924D3 File Offset: 0x001906D3
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x001924D6 File Offset: 0x001906D6
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x06006CFE RID: 27902 RVA: 0x001924D9 File Offset: 0x001906D9
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x06006CFF RID: 27903 RVA: 0x001924DC File Offset: 0x001906DC
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return this._reducedFrom.ObsoleteAttributeData;
			}
		}

		// Token: 0x06006D00 RID: 27904 RVA: 0x001924E9 File Offset: 0x001906E9
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return this._reducedFrom.GetUnmanagedCallersOnlyAttributeData(forceComplete);
		}

		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x06006D01 RID: 27905 RVA: 0x001924F7 File Offset: 0x001906F7
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return this._reducedFrom.DeclaredAccessibility;
			}
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x06006D02 RID: 27906 RVA: 0x00192504 File Offset: 0x00190704
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._reducedFrom.ContainingSymbol;
			}
		}

		// Token: 0x06006D03 RID: 27907 RVA: 0x00192511 File Offset: 0x00190711
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this._reducedFrom.GetAttributes();
		}

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x06006D04 RID: 27908 RVA: 0x0019251E File Offset: 0x0019071E
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x06006D05 RID: 27909 RVA: 0x00192521 File Offset: 0x00190721
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.ReducedExtension;
			}
		}

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x06006D06 RID: 27910 RVA: 0x00192525 File Offset: 0x00190725
		public override bool ReturnsVoid
		{
			get
			{
				return this._reducedFrom.ReturnsVoid;
			}
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x06006D07 RID: 27911 RVA: 0x00192532 File Offset: 0x00190732
		public override bool IsGenericMethod
		{
			get
			{
				return this._reducedFrom.IsGenericMethod;
			}
		}

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x06006D08 RID: 27912 RVA: 0x0019253F File Offset: 0x0019073F
		public override bool IsVararg
		{
			get
			{
				return this._reducedFrom.IsVararg;
			}
		}

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x06006D09 RID: 27913 RVA: 0x0019254C File Offset: 0x0019074C
		public override RefKind RefKind
		{
			get
			{
				return this._reducedFrom.RefKind;
			}
		}

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x06006D0A RID: 27914 RVA: 0x00192559 File Offset: 0x00190759
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return this._typeMap.SubstituteType(this._reducedFrom.ReturnTypeWithAnnotations);
			}
		}

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x06006D0B RID: 27915 RVA: 0x00192571 File Offset: 0x00190771
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return this._reducedFrom.ReturnTypeFlowAnalysisAnnotations;
			}
		}

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x06006D0C RID: 27916 RVA: 0x0019257E File Offset: 0x0019077E
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return this._reducedFrom.ReturnNotNullIfParameterNotNull;
			}
		}

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x06006D0D RID: 27917 RVA: 0x0019258B File Offset: 0x0019078B
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return this._reducedFrom.FlowAnalysisAnnotations;
			}
		}

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x06006D0E RID: 27918 RVA: 0x00192598 File Offset: 0x00190798
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._typeMap.SubstituteCustomModifiers(this._reducedFrom.RefCustomModifiers);
			}
		}

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x06006D0F RID: 27919 RVA: 0x001925B0 File Offset: 0x001907B0
		internal override int ParameterCount
		{
			get
			{
				return this._reducedFrom.ParameterCount - 1;
			}
		}

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x06006D10 RID: 27920 RVA: 0x001925BF File Offset: 0x001907BF
		internal override bool GenerateDebugInfo
		{
			get
			{
				return this._reducedFrom.GenerateDebugInfo;
			}
		}

		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x06006D11 RID: 27921 RVA: 0x001925CC File Offset: 0x001907CC
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				if (this._lazyParameters.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<ParameterSymbol>(ref this._lazyParameters, this.MakeParameters(), default(ImmutableArray<ParameterSymbol>));
				}
				return this._lazyParameters;
			}
		}

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x06006D12 RID: 27922 RVA: 0x00192607 File Offset: 0x00190807
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x06006D13 RID: 27923 RVA: 0x0019260A File Offset: 0x0019080A
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x06006D14 RID: 27924 RVA: 0x0019260D File Offset: 0x0019080D
		internal override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x06006D15 RID: 27925 RVA: 0x00192610 File Offset: 0x00190810
		internal override bool IsEffectivelyReadOnly
		{
			get
			{
				return this._reducedFrom.Parameters[0].RefKind == RefKind.In;
			}
		}

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x06006D16 RID: 27926 RVA: 0x00192639 File Offset: 0x00190839
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x06006D17 RID: 27927 RVA: 0x00192640 File Offset: 0x00190840
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x00192643 File Offset: 0x00190843
		internal override bool CallsAreOmitted(SyntaxTree syntaxTree)
		{
			return this._reducedFrom.CallsAreOmitted(syntaxTree);
		}

		// Token: 0x06006D19 RID: 27929 RVA: 0x00192654 File Offset: 0x00190854
		private ImmutableArray<ParameterSymbol> MakeParameters()
		{
			ImmutableArray<ParameterSymbol> parameters = this._reducedFrom.Parameters;
			int length = parameters.Length;
			if (length <= 1)
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
			ParameterSymbol[] array = new ParameterSymbol[length - 1];
			for (int i = 0; i < length - 1; i++)
			{
				array[i] = new ReducedExtensionMethodSymbol.ReducedExtensionMethodParameterSymbol(this, parameters[i + 1]);
			}
			return array.AsImmutableOrNull<ParameterSymbol>();
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x001926AF File Offset: 0x001908AF
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x001926B6 File Offset: 0x001908B6
		internal override bool IsNullableAnalysisEnabled()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x001926C0 File Offset: 0x001908C0
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (this == obj)
			{
				return true;
			}
			ReducedExtensionMethodSymbol reducedExtensionMethodSymbol = obj as ReducedExtensionMethodSymbol;
			return reducedExtensionMethodSymbol != null && this._reducedFrom.Equals(reducedExtensionMethodSymbol._reducedFrom, compareKind);
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x001926F1 File Offset: 0x001908F1
		public override int GetHashCode()
		{
			return this._reducedFrom.GetHashCode();
		}

		// Token: 0x04001AEF RID: 6895
		private readonly MethodSymbol _reducedFrom;

		// Token: 0x04001AF0 RID: 6896
		private readonly TypeMap _typeMap;

		// Token: 0x04001AF1 RID: 6897
		private readonly ImmutableArray<TypeParameterSymbol> _typeParameters;

		// Token: 0x04001AF2 RID: 6898
		private readonly ImmutableArray<TypeWithAnnotations> _typeArguments;

		// Token: 0x04001AF3 RID: 6899
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x02000907 RID: 2311
		private sealed class ReducedExtensionMethodParameterSymbol : WrappedParameterSymbol
		{
			// Token: 0x06008F8D RID: 36749 RVA: 0x001F9234 File Offset: 0x001F7434
			public ReducedExtensionMethodParameterSymbol(ReducedExtensionMethodSymbol containingMethod, ParameterSymbol underlyingParameter)
				: base(underlyingParameter)
			{
				this._containingMethod = containingMethod;
			}

			// Token: 0x1700288A RID: 10378
			// (get) Token: 0x06008F8E RID: 36750 RVA: 0x001F9244 File Offset: 0x001F7444
			public override Symbol ContainingSymbol
			{
				get
				{
					return this._containingMethod;
				}
			}

			// Token: 0x1700288B RID: 10379
			// (get) Token: 0x06008F8F RID: 36751 RVA: 0x001F924C File Offset: 0x001F744C
			public override int Ordinal
			{
				get
				{
					return this._underlyingParameter.Ordinal - 1;
				}
			}

			// Token: 0x1700288C RID: 10380
			// (get) Token: 0x06008F90 RID: 36752 RVA: 0x001F925B File Offset: 0x001F745B
			public override TypeWithAnnotations TypeWithAnnotations
			{
				get
				{
					return this._containingMethod._typeMap.SubstituteType(this._underlyingParameter.TypeWithAnnotations);
				}
			}

			// Token: 0x1700288D RID: 10381
			// (get) Token: 0x06008F91 RID: 36753 RVA: 0x001F9278 File Offset: 0x001F7478
			public override ImmutableArray<CustomModifier> RefCustomModifiers
			{
				get
				{
					return this._containingMethod._typeMap.SubstituteCustomModifiers(this._underlyingParameter.RefCustomModifiers);
				}
			}

			// Token: 0x06008F92 RID: 36754 RVA: 0x001F9298 File Offset: 0x001F7498
			public sealed override bool Equals(Symbol obj, TypeCompareKind compareKind)
			{
				if (this == obj)
				{
					return true;
				}
				ReducedExtensionMethodSymbol.ReducedExtensionMethodParameterSymbol reducedExtensionMethodParameterSymbol = obj as ReducedExtensionMethodSymbol.ReducedExtensionMethodParameterSymbol;
				return reducedExtensionMethodParameterSymbol != null && this.Ordinal == reducedExtensionMethodParameterSymbol.Ordinal && this.ContainingSymbol.Equals(reducedExtensionMethodParameterSymbol.ContainingSymbol, compareKind);
			}

			// Token: 0x06008F93 RID: 36755 RVA: 0x001F92D7 File Offset: 0x001F74D7
			public sealed override int GetHashCode()
			{
				return Hash.Combine<Symbol>(this.ContainingSymbol, this._underlyingParameter.Ordinal);
			}

			// Token: 0x04002748 RID: 10056
			private readonly ReducedExtensionMethodSymbol _containingMethod;
		}
	}
}
