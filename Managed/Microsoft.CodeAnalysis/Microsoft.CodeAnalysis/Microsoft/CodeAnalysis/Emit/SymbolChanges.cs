using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200048D RID: 1165
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class SymbolChanges
	{
		// Token: 0x06002DC3 RID: 11715 RVA: 0x00081D40 File Offset: 0x0007FF40
		protected SymbolChanges(DefinitionMap definitionMap, IEnumerable<SemanticEdit> edits, Func<ISymbol, bool> isAddedSymbol)
		{
			this._definitionMap = definitionMap;
			this._isAddedSymbol = isAddedSymbol;
			this._changes = SymbolChanges.CalculateChanges(edits);
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x00081D62 File Offset: 0x0007FF62
		public bool IsAdded(ISymbol symbol)
		{
			return this._isAddedSymbol.Invoke(symbol);
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x00081D70 File Offset: 0x0007FF70
		public bool RequiresCompilation(ISymbol symbol)
		{
			return this.GetChange(symbol) > SymbolChange.None;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x00081D7C File Offset: 0x0007FF7C
		public SymbolChange GetChange(IDefinition def)
		{
			ISymbolInternal internalSymbol = def.GetInternalSymbol();
			ISynthesizedMethodBodyImplementationSymbol synthesizedMethodBodyImplementationSymbol = internalSymbol as ISynthesizedMethodBodyImplementationSymbol;
			if (synthesizedMethodBodyImplementationSymbol != null)
			{
				IMethodSymbolInternal method = synthesizedMethodBodyImplementationSymbol.Method;
				ISymbolInternal symbolInternal = synthesizedMethodBodyImplementationSymbol;
				SymbolChange change = this.GetChange((IDefinition)method.GetCciAdapter());
				if (change != SymbolChange.Updated)
				{
					if (change != SymbolChange.Added)
					{
						throw ExceptionUtilities.UnexpectedValue(change);
					}
					if (!this._definitionMap.DefinitionExists(def))
					{
						return SymbolChange.Added;
					}
					if (symbolInternal.Kind == SymbolKind.NamedType)
					{
						return SymbolChange.ContainsChanges;
					}
					if (symbolInternal.Kind == SymbolKind.Method)
					{
						return SymbolChange.Updated;
					}
					return SymbolChange.None;
				}
				else
				{
					if (!this._definitionMap.DefinitionExists((IDefinition)symbolInternal.ContainingType.GetCciAdapter()))
					{
						return SymbolChange.Added;
					}
					if (!this._definitionMap.DefinitionExists(def))
					{
						return SymbolChange.Added;
					}
					if (!synthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency)
					{
						return SymbolChange.None;
					}
					if (symbolInternal.Kind == SymbolKind.NamedType)
					{
						return SymbolChange.ContainsChanges;
					}
					if (symbolInternal.Kind == SymbolKind.Method)
					{
						return SymbolChange.Updated;
					}
					return SymbolChange.None;
				}
			}
			else
			{
				if (internalSymbol != null)
				{
					return this.GetChange(internalSymbol.GetISymbol());
				}
				if (!this._definitionMap.DefinitionExists(def))
				{
					return SymbolChange.Added;
				}
				if (!(def is ITypeDefinition))
				{
					return SymbolChange.None;
				}
				return SymbolChange.ContainsChanges;
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x00081E7C File Offset: 0x0008007C
		private SymbolChange GetChange(ISymbol symbol)
		{
			SymbolChange symbolChange;
			if (this._changes.TryGetValue(symbol, ref symbolChange))
			{
				return symbolChange;
			}
			ISymbol containingSymbol = SymbolChanges.GetContainingSymbol(symbol);
			if (containingSymbol == null)
			{
				return SymbolChange.None;
			}
			SymbolChange change = this.GetChange(containingSymbol);
			switch (change)
			{
			case SymbolChange.None:
				return SymbolChange.None;
			case SymbolChange.ContainsChanges:
			case SymbolChange.Updated:
			{
				ISymbolInternal isymbolInternalOrNull = this.GetISymbolInternalOrNull(symbol);
				IReference reference = ((isymbolInternalOrNull != null) ? isymbolInternalOrNull.GetCciAdapter() : null);
				IDefinition definition = reference as IDefinition;
				if (definition != null)
				{
					if (!this._definitionMap.DefinitionExists(definition))
					{
						return SymbolChange.Added;
					}
					return SymbolChange.None;
				}
				else
				{
					INamespace @namespace = reference as INamespace;
					if (@namespace == null)
					{
						return SymbolChange.None;
					}
					if (!this._definitionMap.NamespaceExists(@namespace))
					{
						return SymbolChange.Added;
					}
					return SymbolChange.ContainsChanges;
				}
				break;
			}
			case SymbolChange.Added:
				return SymbolChange.Added;
			default:
				throw ExceptionUtilities.UnexpectedValue(change);
			}
		}

		// Token: 0x06002DC8 RID: 11720
		[return: Nullable(2)]
		protected abstract ISymbolInternal GetISymbolInternalOrNull(ISymbol symbol);

		// Token: 0x06002DC9 RID: 11721 RVA: 0x00081F2A File Offset: 0x0008012A
		public IEnumerable<INamespaceTypeDefinition> GetTopLevelSourceTypeDefinitions(EmitContext context)
		{
			foreach (ISymbol symbol in this._changes.Keys)
			{
				ISymbolInternal isymbolInternalOrNull = this.GetISymbolInternalOrNull(symbol);
				ITypeDefinition typeDefinition = ((isymbolInternalOrNull != null) ? isymbolInternalOrNull.GetCciAdapter() : null) as ITypeDefinition;
				INamespaceTypeDefinition namespaceTypeDefinition = ((typeDefinition != null) ? typeDefinition.AsNamespaceTypeDefinition(context) : null);
				if (namespaceTypeDefinition != null)
				{
					yield return namespaceTypeDefinition;
				}
			}
			IEnumerator<ISymbol> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x00081F44 File Offset: 0x00080144
		private static IReadOnlyDictionary<ISymbol, SymbolChange> CalculateChanges(IEnumerable<SemanticEdit> edits)
		{
			Dictionary<ISymbol, SymbolChange> dictionary = new Dictionary<ISymbol, SymbolChange>();
			foreach (SemanticEdit semanticEdit in edits)
			{
				SymbolChange symbolChange;
				switch (semanticEdit.Kind)
				{
				case SemanticEditKind.Update:
					symbolChange = SymbolChange.Updated;
					break;
				case SemanticEditKind.Insert:
					symbolChange = SymbolChange.Added;
					break;
				case SemanticEditKind.Delete:
					continue;
				default:
					throw ExceptionUtilities.UnexpectedValue(semanticEdit.Kind);
				}
				ISymbol symbol = semanticEdit.NewSymbol;
				if (symbol.Kind == SymbolKind.Method)
				{
					IMethodSymbol partialDefinitionPart = ((IMethodSymbol)symbol).PartialDefinitionPart;
					if (partialDefinitionPart != null)
					{
						symbol = partialDefinitionPart;
					}
				}
				SymbolChanges.AddContainingTypesAndNamespaces(dictionary, symbol);
				dictionary.Add(symbol, symbolChange);
			}
			return dictionary;
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x00082004 File Offset: 0x00080204
		private static void AddContainingTypesAndNamespaces(Dictionary<ISymbol, SymbolChange> changes, ISymbol symbol)
		{
			for (;;)
			{
				ISymbol containingSymbol = SymbolChanges.GetContainingSymbol(symbol);
				if (containingSymbol == null || changes.ContainsKey(containingSymbol))
				{
					break;
				}
				SymbolKind kind = containingSymbol.Kind;
				SymbolChange symbolChange = ((kind == SymbolKind.Property || kind == SymbolKind.Event) ? SymbolChange.Updated : SymbolChange.ContainsChanges);
				changes.Add(containingSymbol, symbolChange);
				symbol = containingSymbol;
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x00082048 File Offset: 0x00080248
		[return: Nullable(2)]
		private static ISymbol GetContainingSymbol(ISymbol symbol)
		{
			SymbolKind symbolKind = symbol.Kind;
			if (symbolKind != SymbolKind.Field)
			{
				if (symbolKind == SymbolKind.Method)
				{
					ISymbol associatedSymbol = ((IMethodSymbol)symbol).AssociatedSymbol;
					if (associatedSymbol != null)
					{
						return associatedSymbol;
					}
				}
			}
			else
			{
				ISymbol associatedSymbol2 = ((IFieldSymbol)symbol).AssociatedSymbol;
				if (associatedSymbol2 != null)
				{
					return associatedSymbol2;
				}
			}
			symbol = symbol.ContainingSymbol;
			if (symbol != null)
			{
				symbolKind = symbol.Kind;
				if (symbolKind == SymbolKind.Assembly || symbolKind == SymbolKind.NetModule)
				{
					return null;
				}
			}
			return symbol;
		}

		// Token: 0x04001247 RID: 4679
		private readonly DefinitionMap _definitionMap;

		// Token: 0x04001248 RID: 4680
		private readonly IReadOnlyDictionary<ISymbol, SymbolChange> _changes;

		// Token: 0x04001249 RID: 4681
		private readonly Func<ISymbol, bool> _isAddedSymbol;
	}
}
