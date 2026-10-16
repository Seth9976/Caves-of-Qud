using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000480 RID: 1152
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CSharpDefinitionMap : DefinitionMap
	{
		// Token: 0x0600605B RID: 24667 RVA: 0x00174825 File Offset: 0x00172A25
		public CSharpDefinitionMap(IEnumerable<SemanticEdit> edits, MetadataDecoder metadataDecoder, CSharpSymbolMatcher mapToMetadata, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpSymbolMatcher mapToPrevious)
			: base(edits)
		{
			this._metadataDecoder = metadataDecoder;
			this._mapToMetadata = mapToMetadata;
			this._mapToPrevious = mapToPrevious ?? mapToMetadata;
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x0600605C RID: 24668 RVA: 0x00174849 File Offset: 0x00172A49
		protected override SymbolMatcher MapToMetadataSymbolMatcher
		{
			get
			{
				return this._mapToMetadata;
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x0600605D RID: 24669 RVA: 0x00174851 File Offset: 0x00172A51
		protected override SymbolMatcher MapToPreviousSymbolMatcher
		{
			get
			{
				return this._mapToPrevious;
			}
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x00174859 File Offset: 0x00172A59
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override ISymbolInternal GetISymbolInternalOrNull(ISymbol symbol)
		{
			Symbol symbol2 = symbol as Symbol;
			if (symbol2 == null)
			{
				return null;
			}
			return symbol2.UnderlyingSymbol;
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x0600605F RID: 24671 RVA: 0x0017486C File Offset: 0x00172A6C
		internal override CommonMessageProvider MessageProvider
		{
			get
			{
				return Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance;
			}
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x00174873 File Offset: 0x00172A73
		protected override LambdaSyntaxFacts GetLambdaSyntaxFacts()
		{
			return CSharpLambdaSyntaxFacts.Instance;
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x0017487A File Offset: 0x00172A7A
		internal bool TryGetAnonymousTypeName(AnonymousTypeManager.AnonymousTypeTemplateSymbol template, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out string name, out int index)
		{
			return this._mapToPrevious.TryGetAnonymousTypeName(template, out name, out index);
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x0017488C File Offset: 0x00172A8C
		internal override bool TryGetTypeHandle(ITypeDefinition def, out TypeDefinitionHandle handle)
		{
			IDefinition definition = this._mapToMetadata.MapDefinition(def);
			PENamedTypeSymbol penamedTypeSymbol = ((definition != null) ? definition.GetInternalSymbol() : null) as PENamedTypeSymbol;
			if (penamedTypeSymbol != null)
			{
				handle = penamedTypeSymbol.Handle;
				return true;
			}
			handle = default(TypeDefinitionHandle);
			return false;
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x001748D0 File Offset: 0x00172AD0
		internal override bool TryGetEventHandle(IEventDefinition def, out EventDefinitionHandle handle)
		{
			IDefinition definition = this._mapToMetadata.MapDefinition(def);
			PEEventSymbol peeventSymbol = ((definition != null) ? definition.GetInternalSymbol() : null) as PEEventSymbol;
			if (peeventSymbol != null)
			{
				handle = peeventSymbol.Handle;
				return true;
			}
			handle = default(EventDefinitionHandle);
			return false;
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x00174914 File Offset: 0x00172B14
		internal override bool TryGetFieldHandle(IFieldDefinition def, out FieldDefinitionHandle handle)
		{
			IDefinition definition = this._mapToMetadata.MapDefinition(def);
			PEFieldSymbol pefieldSymbol = ((definition != null) ? definition.GetInternalSymbol() : null) as PEFieldSymbol;
			if (pefieldSymbol != null)
			{
				handle = pefieldSymbol.Handle;
				return true;
			}
			handle = default(FieldDefinitionHandle);
			return false;
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x00174958 File Offset: 0x00172B58
		internal override bool TryGetMethodHandle(IMethodDefinition def, out MethodDefinitionHandle handle)
		{
			IDefinition definition = this._mapToMetadata.MapDefinition(def);
			PEMethodSymbol pemethodSymbol = ((definition != null) ? definition.GetInternalSymbol() : null) as PEMethodSymbol;
			if (pemethodSymbol != null)
			{
				handle = pemethodSymbol.Handle;
				return true;
			}
			handle = default(MethodDefinitionHandle);
			return false;
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x0017499C File Offset: 0x00172B9C
		internal override bool TryGetPropertyHandle(IPropertyDefinition def, out PropertyDefinitionHandle handle)
		{
			IDefinition definition = this._mapToMetadata.MapDefinition(def);
			PEPropertySymbol pepropertySymbol = ((definition != null) ? definition.GetInternalSymbol() : null) as PEPropertySymbol;
			if (pepropertySymbol != null)
			{
				handle = pepropertySymbol.Handle;
				return true;
			}
			handle = default(PropertyDefinitionHandle);
			return false;
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x001749E0 File Offset: 0x00172BE0
		protected override void GetStateMachineFieldMapFromMetadata(ITypeSymbolInternal stateMachineType, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<LocalSlotDebugInfo> localSlotDebugInfo, out IReadOnlyDictionary<EncHoistedLocalInfo, int> hoistedLocalMap, out IReadOnlyDictionary<ITypeReference, int> awaiterMap, out int awaiterSlotCount)
		{
			Dictionary<EncHoistedLocalInfo, int> dictionary = new Dictionary<EncHoistedLocalInfo, int>();
			Dictionary<ITypeReference, int> dictionary2 = new Dictionary<ITypeReference, int>(SymbolEquivalentEqualityComparer.Instance);
			int num = -1;
			foreach (Symbol symbol in ((Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol)stateMachineType).GetMembers())
			{
				if (symbol.Kind == SymbolKind.Field)
				{
					string name = symbol.Name;
					GeneratedNameKind kind = GeneratedNames.GetKind(name);
					int num2;
					if (kind != GeneratedNameKind.HoistedLocalField && kind != GeneratedNameKind.HoistedSynthesizedLocalField)
					{
						if (kind == GeneratedNameKind.AwaiterField && GeneratedNames.TryParseSlotIndex(name, out num2))
						{
							Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol fieldSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol)symbol;
							dictionary2[(ITypeReference)fieldSymbol.Type.GetCciAdapter()] = num2;
							if (num2 > num)
							{
								num = num2;
							}
						}
					}
					else if (GeneratedNames.TryParseSlotIndex(name, out num2))
					{
						Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol fieldSymbol2 = (Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol)symbol;
						if (num2 < localSlotDebugInfo.Length)
						{
							EncHoistedLocalInfo encHoistedLocalInfo = new EncHoistedLocalInfo(localSlotDebugInfo[num2], (ITypeReference)fieldSymbol2.Type.GetCciAdapter());
							dictionary[encHoistedLocalInfo] = num2;
						}
					}
				}
			}
			hoistedLocalMap = dictionary;
			awaiterMap = dictionary2;
			awaiterSlotCount = num + 1;
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x00174AF4 File Offset: 0x00172CF4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected override ImmutableArray<EncLocalInfo> GetLocalSlotMapFromMetadata(StandaloneSignatureHandle handle, EditAndContinueMethodDebugInformation debugInfo)
		{
			ImmutableArray<LocalInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> localsOrThrow = this._metadataDecoder.GetLocalsOrThrow(handle);
			return CSharpDefinitionMap.CreateLocalSlotMap(debugInfo, localsOrThrow);
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x00174B18 File Offset: 0x00172D18
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override ITypeSymbolInternal TryGetStateMachineType(EntityHandle methodHandle)
		{
			string text;
			if (this._metadataDecoder.Module.HasStringValuedAttribute(methodHandle, AttributeDescription.AsyncStateMachineAttribute, out text) || this._metadataDecoder.Module.HasStringValuedAttribute(methodHandle, AttributeDescription.IteratorStateMachineAttribute, out text))
			{
				return this._metadataDecoder.GetTypeSymbolForSerializedType(text);
			}
			return null;
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x00174B68 File Offset: 0x00172D68
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static ImmutableArray<EncLocalInfo> CreateLocalSlotMap(EditAndContinueMethodDebugInformation methodEncInfo, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<LocalInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>> slotMetadata)
		{
			EncLocalInfo[] array = new EncLocalInfo[slotMetadata.Length];
			ImmutableArray<LocalSlotDebugInfo> localSlots = methodEncInfo.LocalSlots;
			if (!localSlots.IsDefault)
			{
				int num = Math.Min(localSlots.Length, slotMetadata.Length);
				Dictionary<EncLocalInfo, int> dictionary = new Dictionary<EncLocalInfo, int>();
				for (int i = 0; i < num; i++)
				{
					LocalSlotDebugInfo localSlotDebugInfo = localSlots[i];
					if (localSlotDebugInfo.SynthesizedKind.IsLongLived())
					{
						LocalInfo<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol> localInfo = slotMetadata[i];
						if (localInfo.CustomModifiers.IsDefaultOrEmpty)
						{
							EncLocalInfo encLocalInfo = new EncLocalInfo(localSlotDebugInfo, (ITypeReference)localInfo.Type.GetCciAdapter(), localInfo.Constraints, localInfo.SignatureOpt);
							dictionary.Add(encLocalInfo, i);
						}
					}
				}
				foreach (KeyValuePair<EncLocalInfo, int> keyValuePair in dictionary)
				{
					array[keyValuePair.Value] = keyValuePair.Key;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].IsDefault)
				{
					array[j] = new EncLocalInfo(slotMetadata[j].SignatureOpt);
				}
			}
			return ImmutableArray.Create<EncLocalInfo>(array);
		}

		// Token: 0x0400194A RID: 6474
		private readonly MetadataDecoder _metadataDecoder;

		// Token: 0x0400194B RID: 6475
		private readonly CSharpSymbolMatcher _mapToMetadata;

		// Token: 0x0400194C RID: 6476
		private readonly CSharpSymbolMatcher _mapToPrevious;
	}
}
