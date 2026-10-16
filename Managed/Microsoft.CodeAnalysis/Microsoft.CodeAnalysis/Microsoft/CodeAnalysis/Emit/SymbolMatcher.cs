using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200048E RID: 1166
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class SymbolMatcher
	{
		// Token: 0x06002DCD RID: 11725
		[return: Nullable(2)]
		public abstract ITypeReference MapReference(ITypeReference reference);

		// Token: 0x06002DCE RID: 11726
		[return: Nullable(2)]
		public abstract IDefinition MapDefinition(IDefinition definition);

		// Token: 0x06002DCF RID: 11727
		[return: Nullable(2)]
		public abstract INamespace MapNamespace(INamespace @namespace);

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000820A8 File Offset: 0x000802A8
		[return: Nullable(2)]
		public ISymbolInternal MapDefinitionOrNamespace(ISymbolInternal symbol)
		{
			IReference cciAdapter = symbol.GetCciAdapter();
			IDefinition definition = cciAdapter as IDefinition;
			if (definition == null)
			{
				INamespace @namespace = this.MapNamespace((INamespace)cciAdapter);
				return (@namespace != null) ? @namespace.GetInternalSymbol() : null;
			}
			IDefinition definition2 = this.MapDefinition(definition);
			if (definition2 == null)
			{
				return null;
			}
			return definition2.GetInternalSymbol();
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000820F4 File Offset: 0x000802F4
		public EmitBaseline MapBaselineToCompilation(EmitBaseline baseline, Compilation targetCompilation, CommonPEModuleBuilder targetModuleBuilder, [Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> mappedSynthesizedMembers)
		{
			IReadOnlyDictionary<ITypeDefinition, int> readOnlyDictionary = this.MapDefinitions<ITypeDefinition, int>(baseline.TypesAdded);
			IReadOnlyDictionary<IEventDefinition, int> readOnlyDictionary2 = this.MapDefinitions<IEventDefinition, int>(baseline.EventsAdded);
			IReadOnlyDictionary<IFieldDefinition, int> readOnlyDictionary3 = this.MapDefinitions<IFieldDefinition, int>(baseline.FieldsAdded);
			IReadOnlyDictionary<IMethodDefinition, int> readOnlyDictionary4 = this.MapDefinitions<IMethodDefinition, int>(baseline.MethodsAdded);
			IReadOnlyDictionary<IPropertyDefinition, int> readOnlyDictionary5 = this.MapDefinitions<IPropertyDefinition, int>(baseline.PropertiesAdded);
			return baseline.With(targetCompilation, targetModuleBuilder, baseline.Ordinal, baseline.EncId, readOnlyDictionary, readOnlyDictionary2, readOnlyDictionary3, readOnlyDictionary4, readOnlyDictionary5, baseline.EventMapAdded, baseline.PropertyMapAdded, baseline.MethodImplsAdded, baseline.TableEntriesAdded, baseline.BlobStreamLengthAdded, baseline.StringStreamLengthAdded, baseline.UserStringStreamLengthAdded, baseline.GuidStreamLengthAdded, this.MapAnonymousTypes(baseline.AnonymousTypeMap), mappedSynthesizedMembers, this.MapAddedOrChangedMethods(baseline.AddedOrChangedMethods), baseline.DebugInformationProvider, baseline.LocalSignatureProvider);
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000821B4 File Offset: 0x000803B4
		private IReadOnlyDictionary<K, V> MapDefinitions<K, [Nullable(2)] V>(IReadOnlyDictionary<K, V> items) where K : class, IDefinition
		{
			Dictionary<K, V> dictionary = new Dictionary<K, V>(SymbolEquivalentEqualityComparer.Instance);
			foreach (KeyValuePair<K, V> keyValuePair in items)
			{
				K k = (K)((object)this.MapDefinition(keyValuePair.Key));
				if (k != null)
				{
					dictionary.Add(k, keyValuePair.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x00082230 File Offset: 0x00080430
		private IReadOnlyDictionary<int, AddedOrChangedMethodInfo> MapAddedOrChangedMethods(IReadOnlyDictionary<int, AddedOrChangedMethodInfo> addedOrChangedMethods)
		{
			Dictionary<int, AddedOrChangedMethodInfo> dictionary = new Dictionary<int, AddedOrChangedMethodInfo>();
			foreach (KeyValuePair<int, AddedOrChangedMethodInfo> keyValuePair in addedOrChangedMethods)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value.MapTypes(this));
			}
			return dictionary;
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x00082298 File Offset: 0x00080498
		private IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> MapAnonymousTypes(IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> anonymousTypeMap)
		{
			Dictionary<AnonymousTypeKey, AnonymousTypeValue> dictionary = new Dictionary<AnonymousTypeKey, AnonymousTypeValue>();
			foreach (KeyValuePair<AnonymousTypeKey, AnonymousTypeValue> keyValuePair in anonymousTypeMap)
			{
				AnonymousTypeKey key = keyValuePair.Key;
				AnonymousTypeValue value = keyValuePair.Value;
				ITypeDefinition typeDefinition = (ITypeDefinition)this.MapDefinition(value.Type);
				dictionary.Add(key, new AnonymousTypeValue(value.Name, value.UniqueIndex, typeDefinition));
			}
			return dictionary;
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x00082324 File Offset: 0x00080524
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		internal ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> MapSynthesizedMembers([Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> previousMembers, [Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> newMembers)
		{
			if (previousMembers.Count == 0)
			{
				return newMembers;
			}
			ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>>.Builder builder = ImmutableDictionary.CreateBuilder<ISymbolInternal, ImmutableArray<ISymbolInternal>>();
			builder.AddRange(newMembers);
			foreach (KeyValuePair<ISymbolInternal, ImmutableArray<ISymbolInternal>> keyValuePair in previousMembers)
			{
				ISymbolInternal key = keyValuePair.Key;
				ImmutableArray<ISymbolInternal> value = keyValuePair.Value;
				ISymbolInternal symbolInternal = this.MapDefinitionOrNamespace(key);
				ImmutableArray<ISymbolInternal> immutableArray;
				if (symbolInternal == null)
				{
					builder.Add(key, value);
				}
				else if (!newMembers.TryGetValue(symbolInternal, out immutableArray))
				{
					builder.Add(symbolInternal, value);
				}
				else
				{
					ArrayBuilder<ISymbolInternal> instance = ArrayBuilder<ISymbolInternal>.GetInstance();
					instance.AddRange(immutableArray);
					foreach (ISymbolInternal symbolInternal2 in value)
					{
						if (this.MapDefinitionOrNamespace(symbolInternal2) == null)
						{
							instance.Add(symbolInternal2);
						}
					}
					builder[symbolInternal] = instance.ToImmutableAndFree();
				}
			}
			return builder.ToImmutable();
		}
	}
}
