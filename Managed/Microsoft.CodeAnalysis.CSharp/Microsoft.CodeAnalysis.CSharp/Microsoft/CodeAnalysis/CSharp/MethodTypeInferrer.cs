using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200006A RID: 106
	internal sealed class MethodTypeInferrer
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00049A80 File Offset: 0x00047C80
		private int NumberArgumentsToProcess
		{
			get
			{
				return Math.Min(this._arguments.Length, this._formalParameterTypes.Length);
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00049AB0 File Offset: 0x00047CB0
		public static MethodTypeInferenceResult Infer(Binder binder, ConversionsBase conversions, ImmutableArray<TypeParameterSymbol> methodTypeParameters, NamedTypeSymbol constructedContainingTypeOfMethod, ImmutableArray<TypeWithAnnotations> formalParameterTypes, ImmutableArray<RefKind> formalParameterRefKinds, ImmutableArray<BoundExpression> arguments, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, MethodTypeInferrer.Extensions extensions = null)
		{
			if (formalParameterTypes.Length == 0)
			{
				return new MethodTypeInferenceResult(false, default(ImmutableArray<TypeWithAnnotations>));
			}
			return new MethodTypeInferrer(conversions, methodTypeParameters, constructedContainingTypeOfMethod, formalParameterTypes, formalParameterRefKinds, arguments, extensions).InferTypeArgs(binder, ref useSiteInfo);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00049AF0 File Offset: 0x00047CF0
		private MethodTypeInferrer(ConversionsBase conversions, ImmutableArray<TypeParameterSymbol> methodTypeParameters, NamedTypeSymbol constructedContainingTypeOfMethod, ImmutableArray<TypeWithAnnotations> formalParameterTypes, ImmutableArray<RefKind> formalParameterRefKinds, ImmutableArray<BoundExpression> arguments, MethodTypeInferrer.Extensions extensions)
		{
			this._conversions = conversions;
			this._methodTypeParameters = methodTypeParameters;
			this._constructedContainingTypeOfMethod = constructedContainingTypeOfMethod;
			this._formalParameterTypes = formalParameterTypes;
			this._formalParameterRefKinds = formalParameterRefKinds;
			this._arguments = arguments;
			this._extensions = extensions ?? MethodTypeInferrer.Extensions.Default;
			this._fixedResults = new TypeWithAnnotations[methodTypeParameters.Length];
			this._exactBounds = new HashSet<TypeWithAnnotations>[methodTypeParameters.Length];
			this._upperBounds = new HashSet<TypeWithAnnotations>[methodTypeParameters.Length];
			this._lowerBounds = new HashSet<TypeWithAnnotations>[methodTypeParameters.Length];
			this._dependencies = null;
			this._dependenciesDirty = false;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00049B98 File Offset: 0x00047D98
		private RefKind GetRefKind(int index)
		{
			if (!this._formalParameterRefKinds.IsDefault)
			{
				return this._formalParameterRefKinds[index];
			}
			return RefKind.None;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00049BC8 File Offset: 0x00047DC8
		private ImmutableArray<TypeWithAnnotations> GetResults()
		{
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				if (!this._fixedResults[i].HasType || (this._fixedResults[i].Type.IsErrorType() && this._fixedResults[i].Type.Name == null))
				{
					this._fixedResults[i] = TypeWithAnnotations.Create(new ExtendedErrorTypeSymbol(this._constructedContainingTypeOfMethod, this._methodTypeParameters[i].Name, 0, null, false, false), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
			}
			return this._fixedResults.AsImmutable<TypeWithAnnotations>();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00049C80 File Offset: 0x00047E80
		private bool ValidIndex(int index)
		{
			return 0 <= index && index < this._methodTypeParameters.Length;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00049CA4 File Offset: 0x00047EA4
		private bool IsUnfixed(int methodTypeParameterIndex)
		{
			return !this._fixedResults[methodTypeParameterIndex].HasType;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00049CBC File Offset: 0x00047EBC
		private bool IsUnfixedTypeParameter(TypeWithAnnotations type)
		{
			if (type.TypeKind != TypeKind.TypeParameter)
			{
				return false;
			}
			TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)type.Type;
			int ordinal = typeParameterSymbol.Ordinal;
			return this.ValidIndex(ordinal) && TypeSymbol.Equals(typeParameterSymbol, this._methodTypeParameters[ordinal], TypeCompareKind.ConsiderEverything) && this.IsUnfixed(ordinal);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00049D14 File Offset: 0x00047F14
		private bool AllFixed()
		{
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				if (this.IsUnfixed(i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00049D48 File Offset: 0x00047F48
		private void AddBound(TypeWithAnnotations addedBound, HashSet<TypeWithAnnotations>[] collectedBounds, TypeWithAnnotations methodTypeParameterWithAnnotations)
		{
			int ordinal = ((TypeParameterSymbol)methodTypeParameterWithAnnotations.Type).Ordinal;
			if (collectedBounds[ordinal] == null)
			{
				collectedBounds[ordinal] = new HashSet<TypeWithAnnotations>(TypeWithAnnotations.EqualsComparer.ConsiderEverythingComparer);
			}
			collectedBounds[ordinal].Add(addedBound);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00049D83 File Offset: 0x00047F83
		private bool HasBound(int methodTypeParameterIndex)
		{
			return this._lowerBounds[methodTypeParameterIndex] != null || this._upperBounds[methodTypeParameterIndex] != null || this._exactBounds[methodTypeParameterIndex] != null;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00049DA8 File Offset: 0x00047FA8
		private TypeSymbol GetFixedDelegateOrFunctionPointer(TypeSymbol delegateOrFunctionPointerType)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(this._methodTypeParameters.Length);
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				instance.Add(this.IsUnfixed(i) ? TypeWithAnnotations.Create(this._methodTypeParameters[i], NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)) : this._fixedResults[i]);
			}
			return new TypeMap(this._constructedContainingTypeOfMethod, this._methodTypeParameters, instance.ToImmutableAndFree()).SubstituteType(delegateOrFunctionPointerType).Type;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00049E43 File Offset: 0x00048043
		private MethodTypeInferenceResult InferTypeArgs(Binder binder, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			this.InferTypeArgsFirstPhase(ref useSiteInfo);
			return new MethodTypeInferenceResult(this.InferTypeArgsSecondPhase(binder, ref useSiteInfo), this.GetResults());
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00049E60 File Offset: 0x00048060
		private void InferTypeArgsFirstPhase(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			int i = 0;
			int numberArgumentsToProcess = this.NumberArgumentsToProcess;
			while (i < numberArgumentsToProcess)
			{
				BoundExpression boundExpression = this._arguments[i];
				TypeWithAnnotations typeWithAnnotations = this._formalParameterTypes[i];
				MethodTypeInferrer.ExactOrBoundsKind exactOrBoundsKind = ((this.GetRefKind(i).IsManagedReference() || typeWithAnnotations.Type.IsPointerType()) ? MethodTypeInferrer.ExactOrBoundsKind.Exact : MethodTypeInferrer.ExactOrBoundsKind.LowerBound);
				this.MakeExplicitParameterTypeInferences(boundExpression, typeWithAnnotations, exactOrBoundsKind, ref useSiteInfo);
				i++;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00049ED0 File Offset: 0x000480D0
		private void MakeExplicitParameterTypeInferences(BoundExpression argument, TypeWithAnnotations target, MethodTypeInferrer.ExactOrBoundsKind kind, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (argument.Kind == BoundKind.UnboundLambda)
			{
				this.ExplicitParameterTypeInference(argument, target, ref useSiteInfo);
				return;
			}
			if ((argument.Kind != BoundKind.TupleLiteral || !this.MakeExplicitParameterTypeInferences((BoundTupleLiteral)argument, target, kind, ref useSiteInfo)) && MethodTypeInferrer.IsReallyAType(argument.Type))
			{
				this.ExactOrBoundsInference(kind, this._extensions.GetTypeWithAnnotations(argument), target, ref useSiteInfo);
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00049F38 File Offset: 0x00048138
		private bool MakeExplicitParameterTypeInferences(BoundTupleLiteral argument, TypeWithAnnotations target, MethodTypeInferrer.ExactOrBoundsKind kind, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (target.Type.Kind != SymbolKind.NamedType)
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)target.Type;
			ImmutableArray<BoundExpression> arguments = argument.Arguments;
			if (!namedTypeSymbol.IsTupleTypeOfCardinality(arguments.Length))
			{
				return false;
			}
			ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = namedTypeSymbol.TupleElementTypesWithAnnotations;
			for (int i = 0; i < arguments.Length; i++)
			{
				BoundExpression boundExpression = arguments[i];
				TypeWithAnnotations typeWithAnnotations = tupleElementTypesWithAnnotations[i];
				this.MakeExplicitParameterTypeInferences(boundExpression, typeWithAnnotations, kind, ref useSiteInfo);
			}
			return true;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00049FB8 File Offset: 0x000481B8
		private bool InferTypeArgsSecondPhase(Binder binder, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			this.InitializeDependencies();
			for (;;)
			{
				MethodTypeInferrer.InferenceResult inferenceResult = this.DoSecondPhase(binder, ref useSiteInfo);
				if (inferenceResult == MethodTypeInferrer.InferenceResult.InferenceFailed)
				{
					break;
				}
				if (inferenceResult == MethodTypeInferrer.InferenceResult.Success)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00049FE0 File Offset: 0x000481E0
		private MethodTypeInferrer.InferenceResult DoSecondPhase(Binder binder, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this.AllFixed())
			{
				return MethodTypeInferrer.InferenceResult.Success;
			}
			this.MakeOutputTypeInferences(binder, ref useSiteInfo);
			MethodTypeInferrer.InferenceResult inferenceResult = this.FixNondependentParameters(ref useSiteInfo);
			if (inferenceResult != MethodTypeInferrer.InferenceResult.NoProgress)
			{
				return inferenceResult;
			}
			inferenceResult = this.FixDependentParameters(ref useSiteInfo);
			if (inferenceResult != MethodTypeInferrer.InferenceResult.NoProgress)
			{
				return inferenceResult;
			}
			return MethodTypeInferrer.InferenceResult.InferenceFailed;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0004A01C File Offset: 0x0004821C
		private void MakeOutputTypeInferences(Binder binder, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			int i = 0;
			int numberArgumentsToProcess = this.NumberArgumentsToProcess;
			while (i < numberArgumentsToProcess)
			{
				TypeWithAnnotations typeWithAnnotations = this._formalParameterTypes[i];
				BoundExpression boundExpression = this._arguments[i];
				this.MakeOutputTypeInferences(binder, boundExpression, typeWithAnnotations, ref useSiteInfo);
				i++;
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0004A068 File Offset: 0x00048268
		private void MakeOutputTypeInferences(Binder binder, BoundExpression argument, TypeWithAnnotations formalType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (argument.Kind == BoundKind.TupleLiteral && argument.Type == null)
			{
				this.MakeOutputTypeInferences(binder, (BoundTupleLiteral)argument, formalType, ref useSiteInfo);
				return;
			}
			if (this.HasUnfixedParamInOutputType(argument, formalType.Type) && !this.HasUnfixedParamInInputType(argument, formalType.Type))
			{
				this.OutputTypeInference(binder, argument, formalType, ref useSiteInfo);
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0004A0C8 File Offset: 0x000482C8
		private void MakeOutputTypeInferences(Binder binder, BoundTupleLiteral argument, TypeWithAnnotations formalType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (formalType.Type.Kind != SymbolKind.NamedType)
			{
				return;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)formalType.Type;
			ImmutableArray<BoundExpression> arguments = argument.Arguments;
			if (!namedTypeSymbol.IsTupleTypeOfCardinality(arguments.Length))
			{
				return;
			}
			ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = namedTypeSymbol.TupleElementTypesWithAnnotations;
			for (int i = 0; i < arguments.Length; i++)
			{
				BoundExpression boundExpression = arguments[i];
				TypeWithAnnotations typeWithAnnotations = tupleElementTypesWithAnnotations[i];
				this.MakeOutputTypeInferences(binder, boundExpression, typeWithAnnotations, ref useSiteInfo);
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0004A144 File Offset: 0x00048344
		private MethodTypeInferrer.InferenceResult FixNondependentParameters(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.FixParameters((MethodTypeInferrer inferrer, int index) => !inferrer.DependsOnAny(index), ref useSiteInfo);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0004A16C File Offset: 0x0004836C
		private MethodTypeInferrer.InferenceResult FixDependentParameters(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.FixParameters((MethodTypeInferrer inferrer, int index) => inferrer.AnyDependsOn(index), ref useSiteInfo);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0004A194 File Offset: 0x00048394
		private MethodTypeInferrer.InferenceResult FixParameters(Func<MethodTypeInferrer, int, bool> predicate, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			bool[] array = new bool[this._methodTypeParameters.Length];
			MethodTypeInferrer.InferenceResult inferenceResult = MethodTypeInferrer.InferenceResult.NoProgress;
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				if (this.IsUnfixed(i) && this.HasBound(i) && predicate.Invoke(this, i))
				{
					array[i] = true;
					inferenceResult = MethodTypeInferrer.InferenceResult.MadeProgress;
				}
			}
			for (int j = 0; j < this._methodTypeParameters.Length; j++)
			{
				if (array[j] && !this.Fix(j, ref useSiteInfo))
				{
					inferenceResult = MethodTypeInferrer.InferenceResult.InferenceFailed;
				}
			}
			return inferenceResult;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0004A224 File Offset: 0x00048424
		private static bool DoesInputTypeContain(BoundExpression argument, TypeSymbol formalParameterType, TypeParameterSymbol typeParameter)
		{
			TypeSymbol delegateOrFunctionPointerType = formalParameterType.GetDelegateOrFunctionPointerType();
			if (delegateOrFunctionPointerType == null)
			{
				return false;
			}
			bool flag = delegateOrFunctionPointerType.IsFunctionPointer();
			if (!flag || argument.Kind == BoundKind.UnconvertedAddressOfOperator)
			{
				if (!flag)
				{
					BoundKind kind = argument.Kind;
					if (kind != BoundKind.UnboundLambda && kind != BoundKind.MethodGroup)
					{
						return false;
					}
				}
				ImmutableArray<ParameterSymbol> immutableArray = delegateOrFunctionPointerType.DelegateOrFunctionPointerParameters();
				if (immutableArray.IsDefaultOrEmpty)
				{
					return false;
				}
				ImmutableArray<ParameterSymbol>.Enumerator enumerator = immutableArray.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Type.ContainsTypeParameter(typeParameter))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0004A2AC File Offset: 0x000484AC
		private bool HasUnfixedParamInInputType(BoundExpression pSource, TypeSymbol pDest)
		{
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				if (this.IsUnfixed(i) && MethodTypeInferrer.DoesInputTypeContain(pSource, pDest, this._methodTypeParameters[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0004A2F8 File Offset: 0x000484F8
		private static bool DoesOutputTypeContain(BoundExpression argument, TypeSymbol formalParameterType, TypeParameterSymbol typeParameter)
		{
			TypeSymbol delegateOrFunctionPointerType = formalParameterType.GetDelegateOrFunctionPointerType();
			if (delegateOrFunctionPointerType == null)
			{
				return false;
			}
			bool flag = delegateOrFunctionPointerType.IsFunctionPointer();
			if (!flag || argument.Kind == BoundKind.UnconvertedAddressOfOperator)
			{
				if (!flag)
				{
					BoundKind kind = argument.Kind;
					if (kind != BoundKind.UnboundLambda && kind != BoundKind.MethodGroup)
					{
						return false;
					}
				}
				NamedTypeSymbol namedTypeSymbol = delegateOrFunctionPointerType as NamedTypeSymbol;
				MethodSymbol methodSymbol;
				if (namedTypeSymbol == null)
				{
					FunctionPointerTypeSymbol functionPointerTypeSymbol = delegateOrFunctionPointerType as FunctionPointerTypeSymbol;
					if (functionPointerTypeSymbol == null)
					{
						throw ExceptionUtilities.UnexpectedValue(delegateOrFunctionPointerType);
					}
					methodSymbol = functionPointerTypeSymbol.Signature;
				}
				else
				{
					methodSymbol = namedTypeSymbol.DelegateInvokeMethod;
				}
				MethodSymbol methodSymbol2 = methodSymbol;
				if (methodSymbol2 == null || methodSymbol2.HasUseSiteError)
				{
					return false;
				}
				TypeSymbol returnType = methodSymbol2.ReturnType;
				return returnType != null && returnType.ContainsTypeParameter(typeParameter);
			}
			return false;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0004A3A0 File Offset: 0x000485A0
		private bool HasUnfixedParamInOutputType(BoundExpression argument, TypeSymbol formalParameterType)
		{
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				if (this.IsUnfixed(i) && MethodTypeInferrer.DoesOutputTypeContain(argument, formalParameterType, this._methodTypeParameters[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0004A3EC File Offset: 0x000485EC
		private bool DependsDirectlyOn(int iParam, int jParam)
		{
			int i = 0;
			int numberArgumentsToProcess = this.NumberArgumentsToProcess;
			while (i < numberArgumentsToProcess)
			{
				TypeSymbol type = this._formalParameterTypes[i].Type;
				BoundExpression boundExpression = this._arguments[i];
				if (MethodTypeInferrer.DoesInputTypeContain(boundExpression, type, this._methodTypeParameters[jParam]) && MethodTypeInferrer.DoesOutputTypeContain(boundExpression, type, this._methodTypeParameters[iParam]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0004A46C File Offset: 0x0004866C
		private void InitializeDependencies()
		{
			this._dependencies = new MethodTypeInferrer.Dependency[this._methodTypeParameters.Length, this._methodTypeParameters.Length];
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				for (int j = 0; j < this._methodTypeParameters.Length; j++)
				{
					if (this.DependsDirectlyOn(i, j))
					{
						this._dependencies[i, j] = MethodTypeInferrer.Dependency.Direct;
					}
				}
			}
			this.DeduceAllDependencies();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0004A4F1 File Offset: 0x000486F1
		private bool DependsOn(int iParam, int jParam)
		{
			if (this._dependenciesDirty)
			{
				this.SetIndirectsToUnknown();
				this.DeduceAllDependencies();
			}
			return (this._dependencies[iParam, jParam] & MethodTypeInferrer.Dependency.DependsMask) > MethodTypeInferrer.Dependency.Unknown;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0004A51C File Offset: 0x0004871C
		private bool DependsTransitivelyOn(int iParam, int jParam)
		{
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				if ((this._dependencies[iParam, i] & MethodTypeInferrer.Dependency.DependsMask) != MethodTypeInferrer.Dependency.Unknown && (this._dependencies[i, jParam] & MethodTypeInferrer.Dependency.DependsMask) != MethodTypeInferrer.Dependency.Unknown)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0004A56C File Offset: 0x0004876C
		private void DeduceAllDependencies()
		{
			while (this.DeduceDependencies())
			{
			}
			this.SetUnknownsToNotDependent();
			this._dependenciesDirty = false;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0004A590 File Offset: 0x00048790
		private bool DeduceDependencies()
		{
			bool flag = false;
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				for (int j = 0; j < this._methodTypeParameters.Length; j++)
				{
					if (this._dependencies[i, j] == MethodTypeInferrer.Dependency.Unknown && this.DependsTransitivelyOn(i, j))
					{
						this._dependencies[i, j] = MethodTypeInferrer.Dependency.Indirect;
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0004A5FC File Offset: 0x000487FC
		private void SetUnknownsToNotDependent()
		{
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				for (int j = 0; j < this._methodTypeParameters.Length; j++)
				{
					if (this._dependencies[i, j] == MethodTypeInferrer.Dependency.Unknown)
					{
						this._dependencies[i, j] = MethodTypeInferrer.Dependency.NotDependent;
					}
				}
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0004A658 File Offset: 0x00048858
		private void SetIndirectsToUnknown()
		{
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				for (int j = 0; j < this._methodTypeParameters.Length; j++)
				{
					if (this._dependencies[i, j] == MethodTypeInferrer.Dependency.Indirect)
					{
						this._dependencies[i, j] = MethodTypeInferrer.Dependency.Unknown;
					}
				}
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0004A6B8 File Offset: 0x000488B8
		private void UpdateDependenciesAfterFix(int iParam)
		{
			if (this._dependencies == null)
			{
				return;
			}
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				this._dependencies[iParam, i] = MethodTypeInferrer.Dependency.NotDependent;
				this._dependencies[i, iParam] = MethodTypeInferrer.Dependency.NotDependent;
			}
			this._dependenciesDirty = true;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0004A70C File Offset: 0x0004890C
		private bool DependsOnAny(int iParam)
		{
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				if (this.DependsOn(iParam, i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0004A740 File Offset: 0x00048940
		private bool AnyDependsOn(int iParam)
		{
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				if (this.DependsOn(i, iParam))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0004A774 File Offset: 0x00048974
		private void OutputTypeInference(Binder binder, BoundExpression expression, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this.InferredReturnTypeInference(expression, target, ref useSiteInfo))
			{
				return;
			}
			if (this.MethodGroupReturnTypeInference(binder, expression, target.Type, ref useSiteInfo))
			{
				return;
			}
			TypeWithAnnotations typeWithAnnotations = this._extensions.GetTypeWithAnnotations(expression);
			if (typeWithAnnotations.HasType)
			{
				this.LowerBoundInference(typeWithAnnotations, target, ref useSiteInfo);
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0004A7C4 File Offset: 0x000489C4
		private bool InferredReturnTypeInference(BoundExpression source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol delegateType = target.Type.GetDelegateType();
			if (delegateType == null)
			{
				return false;
			}
			TypeWithAnnotations returnTypeWithAnnotations = delegateType.DelegateInvokeMethod.ReturnTypeWithAnnotations;
			if (!returnTypeWithAnnotations.HasType || returnTypeWithAnnotations.SpecialType == SpecialType.System_Void)
			{
				return false;
			}
			TypeWithAnnotations typeWithAnnotations = this.InferReturnType(source, delegateType, ref useSiteInfo);
			if (!typeWithAnnotations.HasType)
			{
				return false;
			}
			this.LowerBoundInference(typeWithAnnotations, returnTypeWithAnnotations, ref useSiteInfo);
			return true;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0004A824 File Offset: 0x00048A24
		private bool MethodGroupReturnTypeInference(Binder binder, BoundExpression source, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			BoundKind kind = source.Kind;
			if (kind != BoundKind.MethodGroup && kind != BoundKind.UnconvertedAddressOfOperator)
			{
				return false;
			}
			TypeSymbol delegateOrFunctionPointerType = target.GetDelegateOrFunctionPointerType();
			if (delegateOrFunctionPointerType == null)
			{
				return false;
			}
			if (delegateOrFunctionPointerType.IsFunctionPointer() != (source.Kind == BoundKind.UnconvertedAddressOfOperator))
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = delegateOrFunctionPointerType as NamedTypeSymbol;
			ValueTuple<MethodSymbol, bool> valueTuple;
			if (namedTypeSymbol == null)
			{
				FunctionPointerTypeSymbol functionPointerTypeSymbol = delegateOrFunctionPointerType as FunctionPointerTypeSymbol;
				if (functionPointerTypeSymbol == null)
				{
					throw ExceptionUtilities.UnexpectedValue(delegateOrFunctionPointerType);
				}
				valueTuple = new ValueTuple<MethodSymbol, bool>(functionPointerTypeSymbol.Signature, true);
			}
			else
			{
				valueTuple = new ValueTuple<MethodSymbol, bool>(namedTypeSymbol.DelegateInvokeMethod, false);
			}
			ValueTuple<MethodSymbol, bool> valueTuple2 = valueTuple;
			MethodSymbol item = valueTuple2.Item1;
			bool item2 = valueTuple2.Item2;
			TypeWithAnnotations returnTypeWithAnnotations = item.ReturnTypeWithAnnotations;
			if (!returnTypeWithAnnotations.HasType || returnTypeWithAnnotations.SpecialType == SpecialType.System_Void)
			{
				return false;
			}
			ImmutableArray<ParameterSymbol> immutableArray = this.GetFixedDelegateOrFunctionPointer(delegateOrFunctionPointerType).DelegateOrFunctionPointerParameters();
			if (immutableArray.IsDefault)
			{
				return false;
			}
			CallingConventionInfo callingConventionInfo = (item2 ? new CallingConventionInfo(item.CallingConvention, ((FunctionPointerMethodSymbol)item).GetCallingConventionModifiers()) : default(CallingConventionInfo));
			BoundMethodGroup boundMethodGroup = (source as BoundMethodGroup) ?? ((BoundUnconvertedAddressOfOperator)source).Operand;
			TypeWithAnnotations typeWithAnnotations = this.MethodGroupReturnType(binder, boundMethodGroup, immutableArray, item.RefKind, item2, ref useSiteInfo, in callingConventionInfo);
			if (typeWithAnnotations.IsDefault || typeWithAnnotations.IsVoidType())
			{
				return false;
			}
			this.LowerBoundInference(typeWithAnnotations, returnTypeWithAnnotations, ref useSiteInfo);
			return true;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0004A968 File Offset: 0x00048B68
		private TypeWithAnnotations MethodGroupReturnType(Binder binder, BoundMethodGroup source, ImmutableArray<ParameterSymbol> delegateParameters, RefKind delegateRefKind, bool isFunctionPointerResolution, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, in CallingConventionInfo callingConventionInfo)
		{
			AnalyzedArguments instance = AnalyzedArguments.GetInstance();
			Conversions.GetDelegateOrFunctionPointerArguments(source.Syntax, instance, delegateParameters, binder.Compilation);
			MethodGroupResolution methodGroupResolution = binder.ResolveMethodGroup(source, instance, true, ref useSiteInfo, false, delegateRefKind, null, isFunctionPointerResolution, in callingConventionInfo);
			TypeWithAnnotations typeWithAnnotations = default(TypeWithAnnotations);
			if (!methodGroupResolution.IsEmpty)
			{
				OverloadResolutionResult<MethodSymbol> overloadResolutionResult = methodGroupResolution.OverloadResolutionResult;
				if (overloadResolutionResult.Succeeded)
				{
					typeWithAnnotations = this._extensions.GetMethodGroupResultType(source, overloadResolutionResult.BestResult.Member);
				}
			}
			instance.Free();
			methodGroupResolution.Free();
			return typeWithAnnotations;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0004A9EC File Offset: 0x00048BEC
		private void ExplicitParameterTypeInference(BoundExpression source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source.Kind != BoundKind.UnboundLambda)
			{
				return;
			}
			UnboundLambda unboundLambda = (UnboundLambda)source;
			if (!unboundLambda.HasExplicitlyTypedParameterList)
			{
				return;
			}
			NamedTypeSymbol delegateType = target.Type.GetDelegateType();
			if (delegateType == null)
			{
				return;
			}
			ImmutableArray<ParameterSymbol> immutableArray = delegateType.DelegateParameters();
			if (immutableArray.IsDefault)
			{
				return;
			}
			int num = immutableArray.Length;
			if (unboundLambda.ParameterCount < num)
			{
				num = unboundLambda.ParameterCount;
			}
			for (int i = 0; i < num; i++)
			{
				this.ExactInference(unboundLambda.ParameterTypeWithAnnotations(i), immutableArray[i].TypeWithAnnotations, ref useSiteInfo);
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0004AA7E File Offset: 0x00048C7E
		private void ExactInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this.ExactNullableInference(source, target, ref useSiteInfo))
			{
				return;
			}
			if (this.ExactTypeParameterInference(source, target))
			{
				return;
			}
			if (this.ExactArrayInference(source, target, ref useSiteInfo))
			{
				return;
			}
			if (this.ExactConstructedInference(source, target, ref useSiteInfo))
			{
				return;
			}
			this.ExactPointerInference(source, target, ref useSiteInfo);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0004AAB9 File Offset: 0x00048CB9
		private bool ExactTypeParameterInference(TypeWithAnnotations source, TypeWithAnnotations target)
		{
			if (this.IsUnfixedTypeParameter(target))
			{
				this.AddBound(source, this._exactBounds, target);
				return true;
			}
			return false;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0004AAD8 File Offset: 0x00048CD8
		private bool ExactArrayInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!source.Type.IsArray() || !target.Type.IsArray())
			{
				return false;
			}
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)source.Type;
			ArrayTypeSymbol arrayTypeSymbol2 = (ArrayTypeSymbol)target.Type;
			if (!arrayTypeSymbol.HasSameShapeAs(arrayTypeSymbol2))
			{
				return false;
			}
			this.ExactInference(arrayTypeSymbol.ElementTypeWithAnnotations, arrayTypeSymbol2.ElementTypeWithAnnotations, ref useSiteInfo);
			return true;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0004AB3C File Offset: 0x00048D3C
		private void ExactOrBoundsInference(MethodTypeInferrer.ExactOrBoundsKind kind, TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			switch (kind)
			{
			case MethodTypeInferrer.ExactOrBoundsKind.Exact:
				this.ExactInference(source, target, ref useSiteInfo);
				return;
			case MethodTypeInferrer.ExactOrBoundsKind.LowerBound:
				this.LowerBoundInference(source, target, ref useSiteInfo);
				return;
			case MethodTypeInferrer.ExactOrBoundsKind.UpperBound:
				this.UpperBoundInference(source, target, ref useSiteInfo);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0004AB74 File Offset: 0x00048D74
		private bool ExactOrBoundsNullableInference(MethodTypeInferrer.ExactOrBoundsKind kind, TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source.IsNullableType() && target.IsNullableType())
			{
				this.ExactOrBoundsInference(kind, ((NamedTypeSymbol)source.Type).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0], ((NamedTypeSymbol)target.Type).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0], ref useSiteInfo);
				return true;
			}
			if (MethodTypeInferrer.<ExactOrBoundsNullableInference>g__isNullableOnly|66_0(source) && MethodTypeInferrer.<ExactOrBoundsNullableInference>g__isNullableOnly|66_0(target))
			{
				this.ExactOrBoundsInference(kind, source.AsNotNullableReferenceType(), target.AsNotNullableReferenceType(), ref useSiteInfo);
				return true;
			}
			return false;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0004ABFC File Offset: 0x00048DFC
		private bool ExactNullableInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.ExactOrBoundsNullableInference(MethodTypeInferrer.ExactOrBoundsKind.Exact, source, target, ref useSiteInfo);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0004AC08 File Offset: 0x00048E08
		private bool LowerBoundTupleInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ImmutableArray<TypeWithAnnotations> immutableArray;
			ImmutableArray<TypeWithAnnotations> immutableArray2;
			if (!source.Type.TryGetElementTypesWithAnnotationsIfTupleType(out immutableArray) || !target.Type.TryGetElementTypesWithAnnotationsIfTupleType(out immutableArray2) || immutableArray.Length != immutableArray2.Length)
			{
				return false;
			}
			for (int i = 0; i < immutableArray.Length; i++)
			{
				this.LowerBoundInference(immutableArray[i], immutableArray2[i], ref useSiteInfo);
			}
			return true;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0004AC74 File Offset: 0x00048E74
		private bool ExactConstructedInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol namedTypeSymbol = source.Type as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol2 = target.Type as NamedTypeSymbol;
			if (namedTypeSymbol2 == null)
			{
				return false;
			}
			if (!TypeSymbol.Equals(namedTypeSymbol.OriginalDefinition, namedTypeSymbol2.OriginalDefinition, TypeCompareKind.ConsiderEverything))
			{
				return false;
			}
			this.ExactTypeArgumentInference(namedTypeSymbol, namedTypeSymbol2, ref useSiteInfo);
			return true;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0004ACC8 File Offset: 0x00048EC8
		private bool ExactPointerInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source.TypeKind == TypeKind.Pointer && target.TypeKind == TypeKind.Pointer)
			{
				this.ExactInference(((PointerTypeSymbol)source.Type).PointedAtTypeWithAnnotations, ((PointerTypeSymbol)target.Type).PointedAtTypeWithAnnotations, ref useSiteInfo);
				return true;
			}
			FunctionPointerTypeSymbol functionPointerTypeSymbol = source.Type as FunctionPointerTypeSymbol;
			if (functionPointerTypeSymbol != null)
			{
				FunctionPointerMethodSymbol signature = functionPointerTypeSymbol.Signature;
				if (signature != null)
				{
					int parameterCount = signature.ParameterCount;
					functionPointerTypeSymbol = target.Type as FunctionPointerTypeSymbol;
					if (functionPointerTypeSymbol != null)
					{
						FunctionPointerMethodSymbol signature2 = functionPointerTypeSymbol.Signature;
						if (signature2 != null)
						{
							int parameterCount2 = signature2.ParameterCount;
							if (parameterCount == parameterCount2)
							{
								if (!MethodTypeInferrer.FunctionPointerRefKindsEqual(signature, signature2) || !MethodTypeInferrer.FunctionPointerCallingConventionsEqual(signature, signature2))
								{
									return false;
								}
								for (int i = 0; i < parameterCount; i++)
								{
									this.ExactInference(signature.ParameterTypesWithAnnotations[i], signature2.ParameterTypesWithAnnotations[i], ref useSiteInfo);
								}
								this.ExactInference(signature.ReturnTypeWithAnnotations, signature2.ReturnTypeWithAnnotations, ref useSiteInfo);
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0004ADCC File Offset: 0x00048FCC
		private static bool FunctionPointerCallingConventionsEqual(FunctionPointerMethodSymbol sourceSignature, FunctionPointerMethodSymbol targetSignature)
		{
			if (sourceSignature.CallingConvention != targetSignature.CallingConvention)
			{
				return false;
			}
			ImmutableHashSet<CustomModifier> callingConventionModifiers = sourceSignature.GetCallingConventionModifiers();
			ImmutableHashSet<CustomModifier> callingConventionModifiers2 = targetSignature.GetCallingConventionModifiers();
			if (callingConventionModifiers == null)
			{
				if (callingConventionModifiers2 == null)
				{
					return true;
				}
			}
			else if (callingConventionModifiers2 != null)
			{
				if (callingConventionModifiers.SetEquals(callingConventionModifiers2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0004AE18 File Offset: 0x00049018
		private static bool FunctionPointerRefKindsEqual(FunctionPointerMethodSymbol sourceSignature, FunctionPointerMethodSymbol targetSignature)
		{
			bool flag = sourceSignature.RefKind == targetSignature.RefKind;
			if (flag)
			{
				bool isDefault = sourceSignature.ParameterRefKinds.IsDefault;
				bool isDefault2 = targetSignature.ParameterRefKinds.IsDefault;
				bool flag2;
				if (isDefault)
				{
					if (isDefault2)
					{
						flag2 = true;
						goto IL_0054;
					}
				}
				else if (!isDefault2)
				{
					flag2 = sourceSignature.ParameterRefKinds.SequenceEqual<RefKind, RefKind>(targetSignature.ParameterRefKinds, null);
					goto IL_0054;
				}
				flag2 = false;
				IL_0054:
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0004AE7C File Offset: 0x0004907C
		private void ExactTypeArgumentInference(NamedTypeSymbol source, NamedTypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			ArrayBuilder<TypeWithAnnotations> instance2 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			source.GetAllTypeArguments(instance, ref useSiteInfo);
			target.GetAllTypeArguments(instance2, ref useSiteInfo);
			for (int i = 0; i < instance.Count; i++)
			{
				this.ExactInference(instance[i], instance2[i], ref useSiteInfo);
			}
			instance.Free();
			instance2.Free();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0004AED8 File Offset: 0x000490D8
		private void LowerBoundInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this.LowerBoundNullableInference(source, target, ref useSiteInfo))
			{
				return;
			}
			if (this.LowerBoundTypeParameterInference(source, target))
			{
				return;
			}
			if (this.LowerBoundArrayInference(source.Type, target.Type, ref useSiteInfo))
			{
				return;
			}
			if (this.LowerBoundTupleInference(source, target, ref useSiteInfo))
			{
				return;
			}
			if (this.LowerBoundConstructedInference(source.Type, target.Type, ref useSiteInfo))
			{
				return;
			}
			this.LowerBoundFunctionPointerTypeInference(source.Type, target.Type, ref useSiteInfo);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0004AF4E File Offset: 0x0004914E
		private bool LowerBoundTypeParameterInference(TypeWithAnnotations source, TypeWithAnnotations target)
		{
			if (this.IsUnfixedTypeParameter(target))
			{
				this.AddBound(source, this._lowerBounds, target);
				return true;
			}
			return false;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0004AF6C File Offset: 0x0004916C
		private static TypeWithAnnotations GetMatchingElementType(ArrayTypeSymbol source, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (target.IsArray())
			{
				ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)target;
				if (!arrayTypeSymbol.HasSameShapeAs(source))
				{
					return default(TypeWithAnnotations);
				}
				return arrayTypeSymbol.ElementTypeWithAnnotations;
			}
			else
			{
				if (!source.IsSZArray)
				{
					return default(TypeWithAnnotations);
				}
				if (!target.IsPossibleArrayGenericInterface())
				{
					return default(TypeWithAnnotations);
				}
				return ((NamedTypeSymbol)target).TypeArgumentWithDefinitionUseSiteDiagnostics(0, ref useSiteInfo);
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0004AFD4 File Offset: 0x000491D4
		private bool LowerBoundArrayInference(TypeSymbol source, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!source.IsArray())
			{
				return false;
			}
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)source;
			TypeWithAnnotations elementTypeWithAnnotations = arrayTypeSymbol.ElementTypeWithAnnotations;
			TypeWithAnnotations matchingElementType = MethodTypeInferrer.GetMatchingElementType(arrayTypeSymbol, target, ref useSiteInfo);
			if (!matchingElementType.HasType)
			{
				return false;
			}
			if (elementTypeWithAnnotations.Type.IsReferenceType)
			{
				this.LowerBoundInference(elementTypeWithAnnotations, matchingElementType, ref useSiteInfo);
			}
			else
			{
				this.ExactInference(elementTypeWithAnnotations, matchingElementType, ref useSiteInfo);
			}
			return true;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0004B02E File Offset: 0x0004922E
		private bool LowerBoundNullableInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.ExactOrBoundsNullableInference(MethodTypeInferrer.ExactOrBoundsKind.LowerBound, source, target, ref useSiteInfo);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0004B03C File Offset: 0x0004923C
		private bool LowerBoundConstructedInference(TypeSymbol source, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol namedTypeSymbol = target as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			if (namedTypeSymbol.AllTypeArgumentCount() == 0)
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol2 = source as NamedTypeSymbol;
			if (namedTypeSymbol2 != null && TypeSymbol.Equals(namedTypeSymbol2.OriginalDefinition, namedTypeSymbol.OriginalDefinition, TypeCompareKind.ConsiderEverything))
			{
				if (namedTypeSymbol2.IsInterface || namedTypeSymbol2.IsDelegateType())
				{
					this.LowerBoundTypeArgumentInference(namedTypeSymbol2, namedTypeSymbol, ref useSiteInfo);
				}
				else
				{
					this.ExactTypeArgumentInference(namedTypeSymbol2, namedTypeSymbol, ref useSiteInfo);
				}
				return true;
			}
			return this.LowerBoundClassInference(source, namedTypeSymbol, ref useSiteInfo) || this.LowerBoundInterfaceInference(source, namedTypeSymbol, ref useSiteInfo);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0004B0C0 File Offset: 0x000492C0
		private bool LowerBoundClassInference(TypeSymbol source, NamedTypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (target.TypeKind != TypeKind.Class)
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = null;
			if (source.TypeKind == TypeKind.Class)
			{
				namedTypeSymbol = source.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			}
			else if (source.TypeKind == TypeKind.TypeParameter)
			{
				namedTypeSymbol = ((TypeParameterSymbol)source).EffectiveBaseClass(ref useSiteInfo);
			}
			while (namedTypeSymbol != null)
			{
				if (TypeSymbol.Equals(namedTypeSymbol.OriginalDefinition, target.OriginalDefinition, TypeCompareKind.ConsiderEverything))
				{
					this.ExactTypeArgumentInference(namedTypeSymbol, target, ref useSiteInfo);
					return true;
				}
				namedTypeSymbol = namedTypeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			}
			return false;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0004B134 File Offset: 0x00049334
		private bool LowerBoundInterfaceInference(TypeSymbol source, NamedTypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!target.IsInterface)
			{
				return false;
			}
			TypeKind typeKind = source.TypeKind;
			ImmutableArray<NamedTypeSymbol> immutableArray;
			if (typeKind != TypeKind.Class)
			{
				switch (typeKind)
				{
				case TypeKind.Interface:
				case TypeKind.Struct:
					goto IL_0033;
				case TypeKind.TypeParameter:
				{
					TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)source;
					immutableArray = typeParameterSymbol.EffectiveBaseClass(ref useSiteInfo).AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo).Concat<NamedTypeSymbol>(typeParameterSymbol.AllEffectiveInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo));
					goto IL_0062;
				}
				}
				return false;
			}
			IL_0033:
			immutableArray = source.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			IL_0062:
			immutableArray = MethodTypeInferrer.ModuloReferenceTypeNullabilityDifferences(immutableArray, VarianceKind.In);
			NamedTypeSymbol interfaceInferenceBound = MethodTypeInferrer.GetInterfaceInferenceBound(immutableArray, target);
			if (interfaceInferenceBound == null)
			{
				return false;
			}
			this.LowerBoundTypeArgumentInference(interfaceInferenceBound, target, ref useSiteInfo);
			return true;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0004B1C4 File Offset: 0x000493C4
		internal static ImmutableArray<NamedTypeSymbol> ModuloReferenceTypeNullabilityDifferences(ImmutableArray<NamedTypeSymbol> interfaces, VarianceKind variance)
		{
			PooledDictionary<NamedTypeSymbol, NamedTypeSymbol> instance = PooledDictionaryIgnoringNullableModifiersForReferenceTypes.GetInstance();
			foreach (NamedTypeSymbol namedTypeSymbol in interfaces)
			{
				NamedTypeSymbol namedTypeSymbol2;
				if (instance.TryGetValue(namedTypeSymbol, ref namedTypeSymbol2))
				{
					NamedTypeSymbol namedTypeSymbol3 = (NamedTypeSymbol)namedTypeSymbol2.MergeEquivalentTypes(namedTypeSymbol, variance);
					instance[namedTypeSymbol] = namedTypeSymbol3;
				}
				else
				{
					instance.Add(namedTypeSymbol, namedTypeSymbol);
				}
			}
			ImmutableArray<NamedTypeSymbol> immutableArray = ((instance.Count != interfaces.Length) ? instance.Values.ToImmutableArray<NamedTypeSymbol>() : interfaces);
			instance.Free();
			return immutableArray;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0004B244 File Offset: 0x00049444
		private void LowerBoundTypeArgumentInference(NamedTypeSymbol source, NamedTypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance();
			ArrayBuilder<TypeWithAnnotations> instance2 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			ArrayBuilder<TypeWithAnnotations> instance3 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			source.OriginalDefinition.GetAllTypeParameters(instance);
			source.GetAllTypeArguments(instance2, ref useSiteInfo);
			target.GetAllTypeArguments(instance3, ref useSiteInfo);
			for (int i = 0; i < instance2.Count; i++)
			{
				TypeParameterSymbol typeParameterSymbol = instance[i];
				TypeWithAnnotations typeWithAnnotations = instance2[i];
				TypeWithAnnotations typeWithAnnotations2 = instance3[i];
				if (typeWithAnnotations.Type.IsReferenceType && typeParameterSymbol.Variance == VarianceKind.Out)
				{
					this.LowerBoundInference(typeWithAnnotations, typeWithAnnotations2, ref useSiteInfo);
				}
				else if (typeWithAnnotations.Type.IsReferenceType && typeParameterSymbol.Variance == VarianceKind.In)
				{
					this.UpperBoundInference(typeWithAnnotations, typeWithAnnotations2, ref useSiteInfo);
				}
				else
				{
					this.ExactInference(typeWithAnnotations, typeWithAnnotations2, ref useSiteInfo);
				}
			}
			instance.Free();
			instance2.Free();
			instance3.Free();
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0004B314 File Offset: 0x00049514
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private bool LowerBoundFunctionPointerTypeInference(TypeSymbol source, TypeSymbol target, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			FunctionPointerTypeSymbol functionPointerTypeSymbol = source as FunctionPointerTypeSymbol;
			if (functionPointerTypeSymbol != null)
			{
				FunctionPointerMethodSymbol signature = functionPointerTypeSymbol.Signature;
				if (signature != null)
				{
					functionPointerTypeSymbol = target as FunctionPointerTypeSymbol;
					if (functionPointerTypeSymbol != null)
					{
						FunctionPointerMethodSymbol signature2 = functionPointerTypeSymbol.Signature;
						if (signature2 != null)
						{
							if (signature.ParameterCount != signature2.ParameterCount)
							{
								return false;
							}
							if (!MethodTypeInferrer.FunctionPointerRefKindsEqual(signature, signature2) || !MethodTypeInferrer.FunctionPointerCallingConventionsEqual(signature, signature2))
							{
								return false;
							}
							for (int i = 0; i < signature.ParameterCount; i++)
							{
								ParameterSymbol parameterSymbol = signature.Parameters[i];
								ParameterSymbol parameterSymbol2 = signature2.Parameters[i];
								if ((parameterSymbol.Type.IsReferenceType || parameterSymbol.Type.IsFunctionPointer()) && parameterSymbol.RefKind == RefKind.None)
								{
									this.UpperBoundInference(parameterSymbol.TypeWithAnnotations, parameterSymbol2.TypeWithAnnotations, ref useSiteInfo);
								}
								else
								{
									this.ExactInference(parameterSymbol.TypeWithAnnotations, parameterSymbol2.TypeWithAnnotations, ref useSiteInfo);
								}
							}
							if ((signature.ReturnType.IsReferenceType || signature.ReturnType.IsFunctionPointer()) && signature.RefKind == RefKind.None)
							{
								this.LowerBoundInference(signature.ReturnTypeWithAnnotations, signature2.ReturnTypeWithAnnotations, ref useSiteInfo);
							}
							else
							{
								this.ExactInference(signature.ReturnTypeWithAnnotations, signature2.ReturnTypeWithAnnotations, ref useSiteInfo);
							}
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0004B444 File Offset: 0x00049644
		private void UpperBoundInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this.UpperBoundNullableInference(source, target, ref useSiteInfo))
			{
				return;
			}
			if (this.UpperBoundTypeParameterInference(source, target))
			{
				return;
			}
			if (this.UpperBoundArrayInference(source, target, ref useSiteInfo))
			{
				return;
			}
			if (this.UpperBoundConstructedInference(source, target, ref useSiteInfo))
			{
				return;
			}
			this.UpperBoundFunctionPointerTypeInference(source.Type, target.Type, ref useSiteInfo);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0004B496 File Offset: 0x00049696
		private bool UpperBoundTypeParameterInference(TypeWithAnnotations source, TypeWithAnnotations target)
		{
			if (this.IsUnfixedTypeParameter(target))
			{
				this.AddBound(source, this._upperBounds, target);
				return true;
			}
			return false;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0004B4B4 File Offset: 0x000496B4
		private bool UpperBoundArrayInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!target.Type.IsArray())
			{
				return false;
			}
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)target.Type;
			TypeWithAnnotations elementTypeWithAnnotations = arrayTypeSymbol.ElementTypeWithAnnotations;
			TypeWithAnnotations matchingElementType = MethodTypeInferrer.GetMatchingElementType(arrayTypeSymbol, source.Type, ref useSiteInfo);
			if (!matchingElementType.HasType)
			{
				return false;
			}
			if (matchingElementType.Type.IsReferenceType)
			{
				this.UpperBoundInference(matchingElementType, elementTypeWithAnnotations, ref useSiteInfo);
			}
			else
			{
				this.ExactInference(matchingElementType, elementTypeWithAnnotations, ref useSiteInfo);
			}
			return true;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0004B520 File Offset: 0x00049720
		private bool UpperBoundNullableInference(TypeWithAnnotations source, TypeWithAnnotations target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.ExactOrBoundsNullableInference(MethodTypeInferrer.ExactOrBoundsKind.UpperBound, source, target, ref useSiteInfo);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0004B52C File Offset: 0x0004972C
		private bool UpperBoundConstructedInference(TypeWithAnnotations sourceWithAnnotations, TypeWithAnnotations targetWithAnnotations, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeSymbol type = sourceWithAnnotations.Type;
			TypeSymbol type2 = targetWithAnnotations.Type;
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			if (namedTypeSymbol.AllTypeArgumentCount() == 0)
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol2 = type2 as NamedTypeSymbol;
			if (namedTypeSymbol2 != null && TypeSymbol.Equals(namedTypeSymbol.OriginalDefinition, type2.OriginalDefinition, TypeCompareKind.ConsiderEverything))
			{
				if (namedTypeSymbol2.IsInterface || namedTypeSymbol2.IsDelegateType())
				{
					this.UpperBoundTypeArgumentInference(namedTypeSymbol, namedTypeSymbol2, ref useSiteInfo);
				}
				else
				{
					this.ExactTypeArgumentInference(namedTypeSymbol, namedTypeSymbol2, ref useSiteInfo);
				}
				return true;
			}
			return this.UpperBoundClassInference(namedTypeSymbol, type2, ref useSiteInfo) || this.UpperBoundInterfaceInference(namedTypeSymbol, type2, ref useSiteInfo);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0004B5BC File Offset: 0x000497BC
		private bool UpperBoundClassInference(NamedTypeSymbol source, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source.TypeKind != TypeKind.Class || target.TypeKind != TypeKind.Class)
			{
				return false;
			}
			for (NamedTypeSymbol namedTypeSymbol = target.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo); namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				if (TypeSymbol.Equals(namedTypeSymbol.OriginalDefinition, source.OriginalDefinition, TypeCompareKind.ConsiderEverything))
				{
					this.ExactTypeArgumentInference(source, namedTypeSymbol, ref useSiteInfo);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0004B614 File Offset: 0x00049814
		private bool UpperBoundInterfaceInference(NamedTypeSymbol source, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!source.IsInterface)
			{
				return false;
			}
			TypeKind typeKind = target.TypeKind;
			if (typeKind != TypeKind.Class && typeKind != TypeKind.Interface && typeKind != TypeKind.Struct)
			{
				return false;
			}
			NamedTypeSymbol interfaceInferenceBound = MethodTypeInferrer.GetInterfaceInferenceBound(MethodTypeInferrer.ModuloReferenceTypeNullabilityDifferences(target.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo), VarianceKind.Out), source);
			if (interfaceInferenceBound == null)
			{
				return false;
			}
			this.UpperBoundTypeArgumentInference(source, interfaceInferenceBound, ref useSiteInfo);
			return true;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0004B664 File Offset: 0x00049864
		private void UpperBoundTypeArgumentInference(NamedTypeSymbol source, NamedTypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance();
			ArrayBuilder<TypeWithAnnotations> instance2 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			ArrayBuilder<TypeWithAnnotations> instance3 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			source.OriginalDefinition.GetAllTypeParameters(instance);
			source.GetAllTypeArguments(instance2, ref useSiteInfo);
			target.GetAllTypeArguments(instance3, ref useSiteInfo);
			for (int i = 0; i < instance2.Count; i++)
			{
				TypeParameterSymbol typeParameterSymbol = instance[i];
				TypeWithAnnotations typeWithAnnotations = instance2[i];
				TypeWithAnnotations typeWithAnnotations2 = instance3[i];
				if (typeWithAnnotations.Type.IsReferenceType && typeParameterSymbol.Variance == VarianceKind.Out)
				{
					this.UpperBoundInference(typeWithAnnotations, typeWithAnnotations2, ref useSiteInfo);
				}
				else if (typeWithAnnotations.Type.IsReferenceType && typeParameterSymbol.Variance == VarianceKind.In)
				{
					this.LowerBoundInference(typeWithAnnotations, typeWithAnnotations2, ref useSiteInfo);
				}
				else
				{
					this.ExactInference(typeWithAnnotations, typeWithAnnotations2, ref useSiteInfo);
				}
			}
			instance.Free();
			instance2.Free();
			instance3.Free();
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0004B734 File Offset: 0x00049934
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private bool UpperBoundFunctionPointerTypeInference(TypeSymbol source, TypeSymbol target, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			FunctionPointerTypeSymbol functionPointerTypeSymbol = source as FunctionPointerTypeSymbol;
			if (functionPointerTypeSymbol != null)
			{
				FunctionPointerMethodSymbol signature = functionPointerTypeSymbol.Signature;
				if (signature != null)
				{
					functionPointerTypeSymbol = target as FunctionPointerTypeSymbol;
					if (functionPointerTypeSymbol != null)
					{
						FunctionPointerMethodSymbol signature2 = functionPointerTypeSymbol.Signature;
						if (signature2 != null)
						{
							if (signature.ParameterCount != signature2.ParameterCount)
							{
								return false;
							}
							if (!MethodTypeInferrer.FunctionPointerRefKindsEqual(signature, signature2) || !MethodTypeInferrer.FunctionPointerCallingConventionsEqual(signature, signature2))
							{
								return false;
							}
							for (int i = 0; i < signature.ParameterCount; i++)
							{
								ParameterSymbol parameterSymbol = signature.Parameters[i];
								ParameterSymbol parameterSymbol2 = signature2.Parameters[i];
								if ((parameterSymbol.Type.IsReferenceType || parameterSymbol.Type.IsFunctionPointer()) && parameterSymbol.RefKind == RefKind.None)
								{
									this.LowerBoundInference(parameterSymbol.TypeWithAnnotations, parameterSymbol2.TypeWithAnnotations, ref useSiteInfo);
								}
								else
								{
									this.ExactInference(parameterSymbol.TypeWithAnnotations, parameterSymbol2.TypeWithAnnotations, ref useSiteInfo);
								}
							}
							if ((signature.ReturnType.IsReferenceType || signature.ReturnType.IsFunctionPointer()) && signature.RefKind == RefKind.None)
							{
								this.UpperBoundInference(signature.ReturnTypeWithAnnotations, signature2.ReturnTypeWithAnnotations, ref useSiteInfo);
							}
							else
							{
								this.ExactInference(signature.ReturnTypeWithAnnotations, signature2.ReturnTypeWithAnnotations, ref useSiteInfo);
							}
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0004B864 File Offset: 0x00049A64
		private bool Fix(int iParam, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			HashSet<TypeWithAnnotations> hashSet = this._exactBounds[iParam];
			HashSet<TypeWithAnnotations> hashSet2 = this._lowerBounds[iParam];
			HashSet<TypeWithAnnotations> hashSet3 = this._upperBounds[iParam];
			TypeWithAnnotations typeWithAnnotations = MethodTypeInferrer.Fix(hashSet, hashSet2, hashSet3, ref useSiteInfo, this._conversions);
			if (!typeWithAnnotations.HasType)
			{
				return false;
			}
			this._fixedResults[iParam] = typeWithAnnotations;
			this.UpdateDependenciesAfterFix(iParam);
			return true;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0004B8BC File Offset: 0x00049ABC
		private static TypeWithAnnotations Fix(HashSet<TypeWithAnnotations> exact, HashSet<TypeWithAnnotations> lower, HashSet<TypeWithAnnotations> upper, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConversionsBase conversions)
		{
			Dictionary<TypeWithAnnotations, TypeWithAnnotations> dictionary = new Dictionary<TypeWithAnnotations, TypeWithAnnotations>(MethodTypeInferrer.EqualsIgnoringDynamicTupleNamesAndNullabilityComparer.Instance);
			if (exact == null)
			{
				if (lower != null)
				{
					MethodTypeInferrer.AddAllCandidates(dictionary, lower, VarianceKind.Out, conversions);
				}
				if (upper != null)
				{
					MethodTypeInferrer.AddAllCandidates(dictionary, upper, VarianceKind.In, conversions);
				}
			}
			else
			{
				MethodTypeInferrer.AddAllCandidates(dictionary, exact, VarianceKind.None, conversions);
				if (dictionary.Count >= 2)
				{
					return default(TypeWithAnnotations);
				}
			}
			if (dictionary.Count == 0)
			{
				return default(TypeWithAnnotations);
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			MethodTypeInferrer.GetAllCandidates(dictionary, instance);
			if (lower != null)
			{
				MethodTypeInferrer.MergeOrRemoveCandidates(dictionary, lower, instance, conversions, VarianceKind.Out, ref useSiteInfo);
			}
			if (upper != null)
			{
				MethodTypeInferrer.MergeOrRemoveCandidates(dictionary, upper, instance, conversions, VarianceKind.In, ref useSiteInfo);
			}
			instance.Clear();
			MethodTypeInferrer.GetAllCandidates(dictionary, instance);
			TypeWithAnnotations typeWithAnnotations = default(TypeWithAnnotations);
			ArrayBuilder<TypeWithAnnotations>.Enumerator enumerator = instance.GetEnumerator();
			IL_0100:
			while (enumerator.MoveNext())
			{
				TypeWithAnnotations typeWithAnnotations2 = enumerator.Current;
				foreach (TypeWithAnnotations typeWithAnnotations3 in instance)
				{
					if (!typeWithAnnotations2.Equals(typeWithAnnotations3, TypeCompareKind.ConsiderEverything) && !MethodTypeInferrer.ImplicitConversionExists(typeWithAnnotations3, typeWithAnnotations2, ref useSiteInfo, conversions.WithNullability(false)))
					{
						goto IL_0100;
					}
				}
				if (typeWithAnnotations.HasType)
				{
					typeWithAnnotations = default(TypeWithAnnotations);
					break;
				}
				typeWithAnnotations = typeWithAnnotations2;
			}
			instance.Free();
			return typeWithAnnotations;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0004B9DC File Offset: 0x00049BDC
		private static bool ImplicitConversionExists(TypeWithAnnotations sourceWithAnnotations, TypeWithAnnotations destinationWithAnnotations, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConversionsBase conversions)
		{
			TypeSymbol type = sourceWithAnnotations.Type;
			TypeSymbol type2 = destinationWithAnnotations.Type;
			return (!type.IsDynamic() || type2.IsDynamic()) && conversions.HasTopLevelNullabilityImplicitConversion(sourceWithAnnotations, destinationWithAnnotations) && conversions.ClassifyImplicitConversionFromType(type, type2, ref useSiteInfo).Exists;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0004BA28 File Offset: 0x00049C28
		private TypeWithAnnotations InferReturnType(BoundExpression source, NamedTypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source.Kind != BoundKind.UnboundLambda)
			{
				return default(TypeWithAnnotations);
			}
			UnboundLambda unboundLambda = (UnboundLambda)source;
			if (unboundLambda.HasSignature)
			{
				ImmutableArray<ParameterSymbol> immutableArray = target.DelegateParameters();
				if (immutableArray.IsDefault)
				{
					return default(TypeWithAnnotations);
				}
				if (immutableArray.Length != unboundLambda.ParameterCount)
				{
					return default(TypeWithAnnotations);
				}
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this.GetFixedDelegateOrFunctionPointer(target);
			ImmutableArray<ParameterSymbol> immutableArray2 = namedTypeSymbol.DelegateParameters();
			if (unboundLambda.HasExplicitlyTypedParameterList)
			{
				for (int i = 0; i < unboundLambda.ParameterCount; i++)
				{
					if (!unboundLambda.ParameterType(i).Equals(immutableArray2[i].Type, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
					{
						return default(TypeWithAnnotations);
					}
				}
			}
			return unboundLambda.InferReturnType(this._conversions, namedTypeSymbol, ref useSiteInfo);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0004BAF8 File Offset: 0x00049CF8
		private static NamedTypeSymbol GetInterfaceInferenceBound(ImmutableArray<NamedTypeSymbol> interfaces, NamedTypeSymbol target)
		{
			NamedTypeSymbol namedTypeSymbol = null;
			foreach (NamedTypeSymbol namedTypeSymbol2 in interfaces)
			{
				if (TypeSymbol.Equals(namedTypeSymbol2.OriginalDefinition, target.OriginalDefinition, TypeCompareKind.ConsiderEverything))
				{
					if (namedTypeSymbol == null)
					{
						namedTypeSymbol = namedTypeSymbol2;
					}
					else if (!TypeSymbol.Equals(namedTypeSymbol, namedTypeSymbol2, TypeCompareKind.ConsiderEverything))
					{
						return null;
					}
				}
			}
			return namedTypeSymbol;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0004BB4C File Offset: 0x00049D4C
		public static ImmutableArray<TypeWithAnnotations> InferTypeArgumentsFromFirstArgument(ConversionsBase conversions, MethodSymbol method, ImmutableArray<BoundExpression> arguments, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (method.ParameterCount < 1 || arguments.Length < 1)
			{
				return default(ImmutableArray<TypeWithAnnotations>);
			}
			MethodSymbol constructedFrom = method.ConstructedFrom;
			MethodTypeInferrer methodTypeInferrer = new MethodTypeInferrer(conversions, constructedFrom.TypeParameters, constructedFrom.ContainingType, constructedFrom.GetParameterTypes(), constructedFrom.ParameterRefKinds, arguments, null);
			if (!methodTypeInferrer.InferTypeArgumentsFromFirstArgument(ref useSiteInfo))
			{
				return default(ImmutableArray<TypeWithAnnotations>);
			}
			return methodTypeInferrer.GetInferredTypeArguments();
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0004BBB8 File Offset: 0x00049DB8
		private bool InferTypeArgumentsFromFirstArgument(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeWithAnnotations typeWithAnnotations = this._formalParameterTypes[0];
			BoundExpression boundExpression = this._arguments[0];
			if (!MethodTypeInferrer.IsReallyAType(boundExpression.Type))
			{
				return false;
			}
			this.LowerBoundInference(this._extensions.GetTypeWithAnnotations(boundExpression), typeWithAnnotations, ref useSiteInfo);
			for (int i = 0; i < this._methodTypeParameters.Length; i++)
			{
				TypeParameterSymbol typeParameterSymbol = this._methodTypeParameters[i];
				if (typeWithAnnotations.Type.ContainsTypeParameter(typeParameterSymbol) && (!this.HasBound(i) || !this.Fix(i, ref useSiteInfo)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0004BC61 File Offset: 0x00049E61
		private ImmutableArray<TypeWithAnnotations> GetInferredTypeArguments()
		{
			return this._fixedResults.AsImmutable<TypeWithAnnotations>();
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0004BC6E File Offset: 0x00049E6E
		private static bool IsReallyAType(TypeSymbol type)
		{
			return type != null && !type.IsErrorType() && !type.IsVoidType();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0004BC86 File Offset: 0x00049E86
		private static void GetAllCandidates(Dictionary<TypeWithAnnotations, TypeWithAnnotations> candidates, ArrayBuilder<TypeWithAnnotations> builder)
		{
			builder.AddRange(candidates.Values);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0004BC94 File Offset: 0x00049E94
		private static void AddAllCandidates(Dictionary<TypeWithAnnotations, TypeWithAnnotations> candidates, HashSet<TypeWithAnnotations> bounds, VarianceKind variance, ConversionsBase conversions)
		{
			foreach (TypeWithAnnotations typeWithAnnotations in bounds)
			{
				if (!conversions.IncludeNullability)
				{
					typeWithAnnotations = typeWithAnnotations.SetUnknownNullabilityForReferenceTypes();
				}
				MethodTypeInferrer.AddOrMergeCandidate(candidates, typeWithAnnotations, variance, conversions);
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0004BCF4 File Offset: 0x00049EF4
		private static void AddOrMergeCandidate(Dictionary<TypeWithAnnotations, TypeWithAnnotations> candidates, TypeWithAnnotations newCandidate, VarianceKind variance, ConversionsBase conversions)
		{
			TypeWithAnnotations typeWithAnnotations;
			if (candidates.TryGetValue(newCandidate, ref typeWithAnnotations))
			{
				MethodTypeInferrer.MergeAndReplaceIfStillCandidate(candidates, typeWithAnnotations, newCandidate, variance, conversions);
				return;
			}
			candidates.Add(newCandidate, newCandidate);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0004BD20 File Offset: 0x00049F20
		private static void MergeOrRemoveCandidates(Dictionary<TypeWithAnnotations, TypeWithAnnotations> candidates, HashSet<TypeWithAnnotations> bounds, ArrayBuilder<TypeWithAnnotations> initialCandidates, ConversionsBase conversions, VarianceKind variance, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeCompareKind typeCompareKind = (conversions.IncludeNullability ? TypeCompareKind.ConsiderEverything : TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);
			foreach (TypeWithAnnotations typeWithAnnotations in bounds)
			{
				foreach (TypeWithAnnotations typeWithAnnotations2 in initialCandidates)
				{
					if (!typeWithAnnotations.Equals(typeWithAnnotations2, typeCompareKind))
					{
						TypeWithAnnotations typeWithAnnotations3;
						TypeWithAnnotations typeWithAnnotations4;
						if (variance == VarianceKind.Out)
						{
							typeWithAnnotations3 = typeWithAnnotations;
							typeWithAnnotations4 = typeWithAnnotations2;
						}
						else
						{
							typeWithAnnotations3 = typeWithAnnotations2;
							typeWithAnnotations4 = typeWithAnnotations;
						}
						if (!MethodTypeInferrer.ImplicitConversionExists(typeWithAnnotations3, typeWithAnnotations4, ref useSiteInfo, conversions.WithNullability(false)))
						{
							candidates.Remove(typeWithAnnotations2);
							TypeWithAnnotations typeWithAnnotations5;
							if (conversions.IncludeNullability && candidates.TryGetValue(typeWithAnnotations, ref typeWithAnnotations5))
							{
								NullableAnnotation nullableAnnotation = typeWithAnnotations5.NullableAnnotation;
								NullableAnnotation nullableAnnotation2 = nullableAnnotation.MergeNullableAnnotation(typeWithAnnotations2.NullableAnnotation, variance);
								if (nullableAnnotation != nullableAnnotation2)
								{
									TypeWithAnnotations typeWithAnnotations6 = TypeWithAnnotations.Create(typeWithAnnotations5.Type, nullableAnnotation2, default(ImmutableArray<CustomModifier>));
									candidates[typeWithAnnotations] = typeWithAnnotations6;
								}
							}
						}
						else if (typeWithAnnotations.Equals(typeWithAnnotations2, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
						{
							MethodTypeInferrer.MergeAndReplaceIfStillCandidate(candidates, typeWithAnnotations2, typeWithAnnotations, variance, conversions);
						}
					}
				}
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0004BE4C File Offset: 0x0004A04C
		private static void MergeAndReplaceIfStillCandidate(Dictionary<TypeWithAnnotations, TypeWithAnnotations> candidates, TypeWithAnnotations oldCandidate, TypeWithAnnotations newCandidate, VarianceKind variance, ConversionsBase conversions)
		{
			if (newCandidate.Type.IsDynamic())
			{
				return;
			}
			TypeWithAnnotations typeWithAnnotations;
			if (candidates.TryGetValue(oldCandidate, ref typeWithAnnotations))
			{
				TypeWithAnnotations typeWithAnnotations2 = typeWithAnnotations.MergeEquivalentTypes(newCandidate, variance);
				candidates[oldCandidate] = typeWithAnnotations2;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0004BE85 File Offset: 0x0004A085
		[CompilerGenerated]
		internal static bool <ExactOrBoundsNullableInference>g__isNullableOnly|66_0(TypeWithAnnotations type)
		{
			return type.NullableAnnotation.IsAnnotated();
		}

		// Token: 0x04000449 RID: 1097
		private readonly ConversionsBase _conversions;

		// Token: 0x0400044A RID: 1098
		private readonly ImmutableArray<TypeParameterSymbol> _methodTypeParameters;

		// Token: 0x0400044B RID: 1099
		private readonly NamedTypeSymbol _constructedContainingTypeOfMethod;

		// Token: 0x0400044C RID: 1100
		private readonly ImmutableArray<TypeWithAnnotations> _formalParameterTypes;

		// Token: 0x0400044D RID: 1101
		private readonly ImmutableArray<RefKind> _formalParameterRefKinds;

		// Token: 0x0400044E RID: 1102
		private readonly ImmutableArray<BoundExpression> _arguments;

		// Token: 0x0400044F RID: 1103
		private readonly MethodTypeInferrer.Extensions _extensions;

		// Token: 0x04000450 RID: 1104
		private readonly TypeWithAnnotations[] _fixedResults;

		// Token: 0x04000451 RID: 1105
		private readonly HashSet<TypeWithAnnotations>[] _exactBounds;

		// Token: 0x04000452 RID: 1106
		private readonly HashSet<TypeWithAnnotations>[] _upperBounds;

		// Token: 0x04000453 RID: 1107
		private readonly HashSet<TypeWithAnnotations>[] _lowerBounds;

		// Token: 0x04000454 RID: 1108
		private MethodTypeInferrer.Dependency[,] _dependencies;

		// Token: 0x04000455 RID: 1109
		private bool _dependenciesDirty;

		// Token: 0x0200066A RID: 1642
		internal abstract class Extensions
		{
			// Token: 0x060082D3 RID: 33491
			internal abstract TypeWithAnnotations GetTypeWithAnnotations(BoundExpression expr);

			// Token: 0x060082D4 RID: 33492
			internal abstract TypeWithAnnotations GetMethodGroupResultType(BoundMethodGroup group, MethodSymbol method);

			// Token: 0x0400202E RID: 8238
			internal static readonly MethodTypeInferrer.Extensions Default = new MethodTypeInferrer.Extensions.DefaultExtensions();

			// Token: 0x020009BF RID: 2495
			private sealed class DefaultExtensions : MethodTypeInferrer.Extensions
			{
				// Token: 0x060092ED RID: 37613 RVA: 0x002018E4 File Offset: 0x001FFAE4
				internal override TypeWithAnnotations GetTypeWithAnnotations(BoundExpression expr)
				{
					return TypeWithAnnotations.Create(expr.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}

				// Token: 0x060092EE RID: 37614 RVA: 0x00201906 File Offset: 0x001FFB06
				internal override TypeWithAnnotations GetMethodGroupResultType(BoundMethodGroup group, MethodSymbol method)
				{
					return method.ReturnTypeWithAnnotations;
				}
			}
		}

		// Token: 0x0200066B RID: 1643
		private enum InferenceResult
		{
			// Token: 0x04002030 RID: 8240
			InferenceFailed,
			// Token: 0x04002031 RID: 8241
			MadeProgress,
			// Token: 0x04002032 RID: 8242
			NoProgress,
			// Token: 0x04002033 RID: 8243
			Success
		}

		// Token: 0x0200066C RID: 1644
		private enum Dependency
		{
			// Token: 0x04002035 RID: 8245
			Unknown,
			// Token: 0x04002036 RID: 8246
			NotDependent,
			// Token: 0x04002037 RID: 8247
			DependsMask = 16,
			// Token: 0x04002038 RID: 8248
			Direct,
			// Token: 0x04002039 RID: 8249
			Indirect
		}

		// Token: 0x0200066D RID: 1645
		private enum ExactOrBoundsKind
		{
			// Token: 0x0400203B RID: 8251
			Exact,
			// Token: 0x0400203C RID: 8252
			LowerBound,
			// Token: 0x0400203D RID: 8253
			UpperBound
		}

		// Token: 0x0200066E RID: 1646
		private sealed class EqualsIgnoringDynamicTupleNamesAndNullabilityComparer : EqualityComparer<TypeWithAnnotations>
		{
			// Token: 0x060082D7 RID: 33495 RVA: 0x001DAE75 File Offset: 0x001D9075
			public override int GetHashCode(TypeWithAnnotations obj)
			{
				return obj.Type.GetHashCode();
			}

			// Token: 0x060082D8 RID: 33496 RVA: 0x001DAE83 File Offset: 0x001D9083
			public override bool Equals(TypeWithAnnotations x, TypeWithAnnotations y)
			{
				return !(x.Type.IsDynamic() ^ y.Type.IsDynamic()) && x.Equals(y, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);
			}

			// Token: 0x0400203E RID: 8254
			internal static readonly MethodTypeInferrer.EqualsIgnoringDynamicTupleNamesAndNullabilityComparer Instance = new MethodTypeInferrer.EqualsIgnoringDynamicTupleNamesAndNullabilityComparer();
		}
	}
}
