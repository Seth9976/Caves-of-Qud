using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000481 RID: 1153
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class DefinitionMap
	{
		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06002D24 RID: 11556
		protected abstract SymbolMatcher MapToMetadataSymbolMatcher { get; }

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06002D25 RID: 11557
		protected abstract SymbolMatcher MapToPreviousSymbolMatcher { get; }

		// Token: 0x06002D26 RID: 11558 RVA: 0x0007EF64 File Offset: 0x0007D164
		protected DefinitionMap(IEnumerable<SemanticEdit> edits)
		{
			this.mappedMethods = this.GetMappedMethods(edits);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x0007EF7C File Offset: 0x0007D17C
		private IReadOnlyDictionary<IMethodSymbolInternal, DefinitionMap.MappedMethod> GetMappedMethods(IEnumerable<SemanticEdit> edits)
		{
			Dictionary<IMethodSymbolInternal, DefinitionMap.MappedMethod> dictionary = new Dictionary<IMethodSymbolInternal, DefinitionMap.MappedMethod>();
			foreach (SemanticEdit semanticEdit in edits)
			{
				if (semanticEdit.Kind == SemanticEditKind.Update && semanticEdit.PreserveLocalVariables)
				{
					IMethodSymbolInternal methodSymbolInternal = this.GetISymbolInternalOrNull(semanticEdit.NewSymbol) as IMethodSymbolInternal;
					if (methodSymbolInternal != null)
					{
						IMethodSymbolInternal methodSymbolInternal2 = this.GetISymbolInternalOrNull(semanticEdit.OldSymbol) as IMethodSymbolInternal;
						if (methodSymbolInternal2 != null)
						{
							dictionary.Add(methodSymbolInternal, new DefinitionMap.MappedMethod(methodSymbolInternal2, semanticEdit.SyntaxMap));
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002D28 RID: 11560
		[return: Nullable(2)]
		protected abstract ISymbolInternal GetISymbolInternalOrNull(ISymbol symbol);

		// Token: 0x06002D29 RID: 11561 RVA: 0x0007F01C File Offset: 0x0007D21C
		[return: Nullable(2)]
		internal IDefinition MapDefinition(IDefinition definition)
		{
			IDefinition definition2;
			if ((definition2 = this.MapToPreviousSymbolMatcher.MapDefinition(definition)) == null)
			{
				if (this.MapToMetadataSymbolMatcher == this.MapToPreviousSymbolMatcher)
				{
					return null;
				}
				definition2 = this.MapToMetadataSymbolMatcher.MapDefinition(definition);
			}
			return definition2;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x0007F04A File Offset: 0x0007D24A
		[return: Nullable(2)]
		internal INamespace MapNamespace(INamespace @namespace)
		{
			INamespace namespace2;
			if ((namespace2 = this.MapToPreviousSymbolMatcher.MapNamespace(@namespace)) == null)
			{
				if (this.MapToMetadataSymbolMatcher == this.MapToPreviousSymbolMatcher)
				{
					return null;
				}
				namespace2 = this.MapToMetadataSymbolMatcher.MapNamespace(@namespace);
			}
			return namespace2;
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x0007F078 File Offset: 0x0007D278
		internal bool DefinitionExists(IDefinition definition)
		{
			return this.MapDefinition(definition) != null;
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x0007F084 File Offset: 0x0007D284
		internal bool NamespaceExists(INamespace @namespace)
		{
			return this.MapNamespace(@namespace) != null;
		}

		// Token: 0x06002D2D RID: 11565
		internal abstract bool TryGetTypeHandle(ITypeDefinition def, out TypeDefinitionHandle handle);

		// Token: 0x06002D2E RID: 11566
		internal abstract bool TryGetEventHandle(IEventDefinition def, out EventDefinitionHandle handle);

		// Token: 0x06002D2F RID: 11567
		internal abstract bool TryGetFieldHandle(IFieldDefinition def, out FieldDefinitionHandle handle);

		// Token: 0x06002D30 RID: 11568
		internal abstract bool TryGetMethodHandle(IMethodDefinition def, out MethodDefinitionHandle handle);

		// Token: 0x06002D31 RID: 11569
		internal abstract bool TryGetPropertyHandle(IPropertyDefinition def, out PropertyDefinitionHandle handle);

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002D32 RID: 11570
		internal abstract CommonMessageProvider MessageProvider { get; }

		// Token: 0x06002D33 RID: 11571 RVA: 0x0007F090 File Offset: 0x0007D290
		private bool TryGetMethodHandle(EmitBaseline baseline, IMethodDefinition def, out MethodDefinitionHandle handle)
		{
			if (this.TryGetMethodHandle(def, out handle))
			{
				return true;
			}
			IMethodDefinition methodDefinition = (IMethodDefinition)this.MapToPreviousSymbolMatcher.MapDefinition(def);
			int num;
			if (methodDefinition != null && baseline.MethodsAdded.TryGetValue(methodDefinition, ref num))
			{
				handle = MetadataTokens.MethodDefinitionHandle(num);
				return true;
			}
			handle = default(MethodDefinitionHandle);
			return false;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0007F0E4 File Offset: 0x0007D2E4
		protected static IReadOnlyDictionary<SyntaxNode, int> CreateDeclaratorToSyntaxOrdinalMap([Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxNode> declarators)
		{
			Dictionary<SyntaxNode, int> dictionary = new Dictionary<SyntaxNode, int>();
			for (int i = 0; i < declarators.Length; i++)
			{
				dictionary.Add(declarators[i], i);
			}
			return dictionary;
		}

		// Token: 0x06002D35 RID: 11573
		protected abstract void GetStateMachineFieldMapFromMetadata(ITypeSymbolInternal stateMachineType, [Nullable(0)] ImmutableArray<LocalSlotDebugInfo> localSlotDebugInfo, out IReadOnlyDictionary<EncHoistedLocalInfo, int> hoistedLocalMap, out IReadOnlyDictionary<ITypeReference, int> awaiterMap, out int awaiterSlotCount);

		// Token: 0x06002D36 RID: 11574
		[NullableContext(0)]
		protected abstract ImmutableArray<EncLocalInfo> GetLocalSlotMapFromMetadata(StandaloneSignatureHandle handle, EditAndContinueMethodDebugInformation debugInfo);

		// Token: 0x06002D37 RID: 11575
		[NullableContext(2)]
		protected abstract ITypeSymbolInternal TryGetStateMachineType(EntityHandle methodHandle);

		// Token: 0x06002D38 RID: 11576 RVA: 0x0007F11C File Offset: 0x0007D31C
		[return: Nullable(2)]
		internal VariableSlotAllocator TryCreateVariableSlotAllocator(EmitBaseline baseline, Compilation compilation, IMethodSymbolInternal method, IMethodSymbolInternal topLevelMethod, DiagnosticBag diagnostics)
		{
			DefinitionMap.MappedMethod mappedMethod;
			if (!this.mappedMethods.TryGetValue(topLevelMethod, ref mappedMethod))
			{
				return null;
			}
			MethodDefinitionHandle methodDefinitionHandle;
			if (!this.TryGetMethodHandle(baseline, (IMethodDefinition)method.GetCciAdapter(), out methodDefinitionHandle))
			{
				return null;
			}
			IReadOnlyDictionary<EncHoistedLocalInfo, int> readOnlyDictionary = null;
			IReadOnlyDictionary<ITypeReference, int> readOnlyDictionary2 = null;
			IReadOnlyDictionary<int, KeyValuePair<DebugId, int>> readOnlyDictionary3 = null;
			IReadOnlyDictionary<int, DebugId> readOnlyDictionary4 = null;
			int num = 0;
			int num2 = 0;
			string text = null;
			int rowNumber = MetadataTokens.GetRowNumber(methodDefinitionHandle);
			AddedOrChangedMethodInfo addedOrChangedMethodInfo;
			DebugId methodId;
			ImmutableArray<EncLocalInfo> immutableArray;
			SymbolMatcher symbolMatcher;
			if (baseline.AddedOrChangedMethods.TryGetValue(rowNumber, ref addedOrChangedMethodInfo))
			{
				methodId = addedOrChangedMethodInfo.MethodId;
				DefinitionMap.MakeLambdaAndClosureMaps(addedOrChangedMethodInfo.LambdaDebugInfo, addedOrChangedMethodInfo.ClosureDebugInfo, out readOnlyDictionary3, out readOnlyDictionary4);
				if (addedOrChangedMethodInfo.StateMachineTypeName != null)
				{
					DefinitionMap.GetStateMachineFieldMapFromPreviousCompilation(addedOrChangedMethodInfo.StateMachineHoistedLocalSlotsOpt, addedOrChangedMethodInfo.StateMachineAwaiterSlotsOpt, out readOnlyDictionary, out readOnlyDictionary2);
					num = addedOrChangedMethodInfo.StateMachineHoistedLocalSlotsOpt.Length;
					num2 = addedOrChangedMethodInfo.StateMachineAwaiterSlotsOpt.Length;
					immutableArray = ImmutableArray<EncLocalInfo>.Empty;
					text = addedOrChangedMethodInfo.StateMachineTypeName;
				}
				else
				{
					immutableArray = addedOrChangedMethodInfo.Locals;
				}
				symbolMatcher = this.MapToPreviousSymbolMatcher;
			}
			else
			{
				EditAndContinueMethodDebugInformation editAndContinueMethodDebugInformation;
				StandaloneSignatureHandle standaloneSignatureHandle;
				try
				{
					editAndContinueMethodDebugInformation = baseline.DebugInformationProvider.Invoke(methodDefinitionHandle);
					standaloneSignatureHandle = baseline.LocalSignatureProvider.Invoke(methodDefinitionHandle);
				}
				catch (Exception ex) when (ex is InvalidDataException || ex is IOException)
				{
					diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_InvalidDebugInfo, method.Locations.First<Location>(), new object[]
					{
						method,
						MetadataTokens.GetToken(methodDefinitionHandle),
						method.ContainingAssembly
					}));
					return null;
				}
				methodId = new DebugId(editAndContinueMethodDebugInformation.MethodOrdinal, 0);
				if (!editAndContinueMethodDebugInformation.Lambdas.IsDefaultOrEmpty)
				{
					DefinitionMap.MakeLambdaAndClosureMaps(editAndContinueMethodDebugInformation.Lambdas, editAndContinueMethodDebugInformation.Closures, out readOnlyDictionary3, out readOnlyDictionary4);
				}
				ITypeSymbolInternal typeSymbolInternal = this.TryGetStateMachineType(methodDefinitionHandle);
				if (typeSymbolInternal != null)
				{
					ImmutableArray<LocalSlotDebugInfo> immutableArray2 = editAndContinueMethodDebugInformation.LocalSlots.NullToEmpty<LocalSlotDebugInfo>();
					this.GetStateMachineFieldMapFromMetadata(typeSymbolInternal, immutableArray2, out readOnlyDictionary, out readOnlyDictionary2, out num2);
					num = immutableArray2.Length;
					immutableArray = ImmutableArray<EncLocalInfo>.Empty;
					text = typeSymbolInternal.Name;
				}
				else
				{
					if (method.IsAsync)
					{
						if (compilation.CommonGetWellKnownTypeMember(WellKnownMember.System_Runtime_CompilerServices_AsyncStateMachineAttribute__ctor) == null)
						{
							this.ReportMissingStateMachineAttribute(diagnostics, method, AttributeDescription.AsyncStateMachineAttribute.FullName);
							return null;
						}
					}
					else if (method.IsIterator && compilation.CommonGetWellKnownTypeMember(WellKnownMember.System_Runtime_CompilerServices_IteratorStateMachineAttribute__ctor) == null)
					{
						this.ReportMissingStateMachineAttribute(diagnostics, method, AttributeDescription.IteratorStateMachineAttribute.FullName);
						return null;
					}
					try
					{
						immutableArray = (standaloneSignatureHandle.IsNil ? ImmutableArray<EncLocalInfo>.Empty : this.GetLocalSlotMapFromMetadata(standaloneSignatureHandle, editAndContinueMethodDebugInformation));
					}
					catch (Exception ex2) when (ex2 is UnsupportedSignatureContent || ex2 is BadImageFormatException || ex2 is IOException)
					{
						diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_InvalidDebugInfo, method.Locations.First<Location>(), new object[]
						{
							method,
							MetadataTokens.GetToken(standaloneSignatureHandle),
							method.ContainingAssembly
						}));
						return null;
					}
				}
				symbolMatcher = this.MapToMetadataSymbolMatcher;
			}
			return new EncVariableSlotAllocator(symbolMatcher, mappedMethod.SyntaxMap, mappedMethod.PreviousMethod, methodId, immutableArray, readOnlyDictionary3, readOnlyDictionary4, text, num, readOnlyDictionary, num2, readOnlyDictionary2, this.GetLambdaSyntaxFacts());
		}

		// Token: 0x06002D39 RID: 11577
		protected abstract LambdaSyntaxFacts GetLambdaSyntaxFacts();

		// Token: 0x06002D3A RID: 11578 RVA: 0x0007F490 File Offset: 0x0007D690
		private void ReportMissingStateMachineAttribute(DiagnosticBag diagnostics, IMethodSymbolInternal method, string stateMachineAttributeFullName)
		{
			diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_EncUpdateFailedMissingAttribute, method.Locations.First<Location>(), new object[]
			{
				this.MessageProvider.GetErrorDisplayString(method.GetISymbol()),
				stateMachineAttributeFullName
			}));
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x0007F4E4 File Offset: 0x0007D6E4
		[NullableContext(0)]
		private static void MakeLambdaAndClosureMaps(ImmutableArray<LambdaDebugInfo> lambdaDebugInfo, ImmutableArray<ClosureDebugInfo> closureDebugInfo, [Nullable(new byte[] { 1, 0 })] out IReadOnlyDictionary<int, KeyValuePair<DebugId, int>> lambdaMap, [Nullable(1)] out IReadOnlyDictionary<int, DebugId> closureMap)
		{
			Dictionary<int, KeyValuePair<DebugId, int>> dictionary = new Dictionary<int, KeyValuePair<DebugId, int>>(lambdaDebugInfo.Length);
			Dictionary<int, DebugId> dictionary2 = new Dictionary<int, DebugId>(closureDebugInfo.Length);
			for (int i = 0; i < lambdaDebugInfo.Length; i++)
			{
				LambdaDebugInfo lambdaDebugInfo2 = lambdaDebugInfo[i];
				dictionary[lambdaDebugInfo2.SyntaxOffset] = KeyValuePairUtil.Create<DebugId, int>(lambdaDebugInfo2.LambdaId, lambdaDebugInfo2.ClosureOrdinal);
			}
			for (int j = 0; j < closureDebugInfo.Length; j++)
			{
				ClosureDebugInfo closureDebugInfo2 = closureDebugInfo[j];
				dictionary2[closureDebugInfo2.SyntaxOffset] = closureDebugInfo2.ClosureId;
			}
			lambdaMap = dictionary;
			closureMap = dictionary2;
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0007F580 File Offset: 0x0007D780
		private static void GetStateMachineFieldMapFromPreviousCompilation([Nullable(0)] ImmutableArray<EncHoistedLocalInfo> hoistedLocalSlots, [Nullable(new byte[] { 0, 2 })] ImmutableArray<ITypeReference> hoistedAwaiters, out IReadOnlyDictionary<EncHoistedLocalInfo, int> hoistedLocalMap, out IReadOnlyDictionary<ITypeReference, int> awaiterMap)
		{
			Dictionary<EncHoistedLocalInfo, int> dictionary = new Dictionary<EncHoistedLocalInfo, int>();
			Dictionary<ITypeReference, int> dictionary2 = new Dictionary<ITypeReference, int>(SymbolEquivalentEqualityComparer.Instance);
			for (int i = 0; i < hoistedLocalSlots.Length; i++)
			{
				EncHoistedLocalInfo encHoistedLocalInfo = hoistedLocalSlots[i];
				if (!encHoistedLocalInfo.IsUnused)
				{
					dictionary.Add(encHoistedLocalInfo, i);
				}
			}
			for (int j = 0; j < hoistedAwaiters.Length; j++)
			{
				ITypeReference typeReference = hoistedAwaiters[j];
				if (typeReference != null)
				{
					dictionary2.Add(typeReference, j);
				}
			}
			hoistedLocalMap = dictionary;
			awaiterMap = dictionary2;
		}

		// Token: 0x040011EF RID: 4591
		protected readonly IReadOnlyDictionary<IMethodSymbolInternal, DefinitionMap.MappedMethod> mappedMethods;

		// Token: 0x02000730 RID: 1840
		[Nullable(0)]
		protected readonly struct MappedMethod
		{
			// Token: 0x06003BCF RID: 15311 RVA: 0x000AC0FB File Offset: 0x000AA2FB
			public MappedMethod(IMethodSymbolInternal previousMethod, [Nullable(new byte[] { 2, 1, 2 })] Func<SyntaxNode, SyntaxNode> syntaxMap)
			{
				this.PreviousMethod = previousMethod;
				this.SyntaxMap = syntaxMap;
			}

			// Token: 0x04001CAD RID: 7341
			public readonly IMethodSymbolInternal PreviousMethod;

			// Token: 0x04001CAE RID: 7342
			[Nullable(new byte[] { 2, 1, 2 })]
			public readonly Func<SyntaxNode, SyntaxNode> SyntaxMap;
		}
	}
}
