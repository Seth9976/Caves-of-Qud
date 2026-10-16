using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000079 RID: 121
	internal sealed class WithCrefTypeParametersBinder : WithTypeParametersBinder
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x00050FE8 File Offset: 0x0004F1E8
		internal WithCrefTypeParametersBinder(CrefSyntax crefSyntax, Binder next)
			: base(next)
		{
			this._crefSyntax = crefSyntax;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00050FF8 File Offset: 0x0004F1F8
		protected override MultiDictionary<string, TypeParameterSymbol> TypeParameterMap
		{
			get
			{
				if (this._lazyTypeParameterMap == null)
				{
					MultiDictionary<string, TypeParameterSymbol> multiDictionary = this.CreateTypeParameterMap();
					Interlocked.CompareExchange<MultiDictionary<string, TypeParameterSymbol>>(ref this._lazyTypeParameterMap, multiDictionary, null);
				}
				return this._lazyTypeParameterMap;
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00051028 File Offset: 0x0004F228
		private MultiDictionary<string, TypeParameterSymbol> CreateTypeParameterMap()
		{
			MultiDictionary<string, TypeParameterSymbol> multiDictionary = new MultiDictionary<string, TypeParameterSymbol>();
			switch (this._crefSyntax.Kind())
			{
			case SyntaxKind.TypeCref:
				this.AddTypeParameters(((TypeCrefSyntax)this._crefSyntax).Type, multiDictionary);
				break;
			case SyntaxKind.QualifiedCref:
			{
				QualifiedCrefSyntax qualifiedCrefSyntax = (QualifiedCrefSyntax)this._crefSyntax;
				this.AddTypeParameters(qualifiedCrefSyntax.Member, multiDictionary);
				this.AddTypeParameters(qualifiedCrefSyntax.Container, multiDictionary);
				break;
			}
			case SyntaxKind.NameMemberCref:
			case SyntaxKind.IndexerMemberCref:
			case SyntaxKind.OperatorMemberCref:
			case SyntaxKind.ConversionOperatorMemberCref:
				this.AddTypeParameters((MemberCrefSyntax)this._crefSyntax, multiDictionary);
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(this._crefSyntax.Kind());
			}
			return multiDictionary;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000510DC File Offset: 0x0004F2DC
		private void AddTypeParameters(TypeSyntax typeSyntax, MultiDictionary<string, TypeParameterSymbol> map)
		{
			switch (typeSyntax.Kind())
			{
			case SyntaxKind.IdentifierName:
			case SyntaxKind.PredefinedType:
				return;
			case SyntaxKind.QualifiedName:
			{
				QualifiedNameSyntax qualifiedNameSyntax = (QualifiedNameSyntax)typeSyntax;
				this.AddTypeParameters(qualifiedNameSyntax.Right, map);
				this.AddTypeParameters(qualifiedNameSyntax.Left, map);
				return;
			}
			case SyntaxKind.GenericName:
				WithCrefTypeParametersBinder.AddTypeParameters((GenericNameSyntax)typeSyntax, map);
				return;
			case SyntaxKind.AliasQualifiedName:
				this.AddTypeParameters(((AliasQualifiedNameSyntax)typeSyntax).Name, map);
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(typeSyntax.Kind());
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00051169 File Offset: 0x0004F369
		private void AddTypeParameters(MemberCrefSyntax memberSyntax, MultiDictionary<string, TypeParameterSymbol> map)
		{
			if (memberSyntax.Kind() == SyntaxKind.NameMemberCref)
			{
				this.AddTypeParameters(((NameMemberCrefSyntax)memberSyntax).Name, map);
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0005118C File Offset: 0x0004F38C
		private static void AddTypeParameters(GenericNameSyntax genericNameSyntax, MultiDictionary<string, TypeParameterSymbol> map)
		{
			SeparatedSyntaxList<TypeSyntax> arguments = genericNameSyntax.TypeArgumentList.Arguments;
			for (int i = arguments.Count - 1; i >= 0; i--)
			{
				if (arguments[i].Kind() == SyntaxKind.IdentifierName)
				{
					IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)arguments[i];
					string valueText = identifierNameSyntax.Identifier.ValueText;
					if (SyntaxFacts.IsValidIdentifier(valueText) && !map.ContainsKey(valueText))
					{
						TypeParameterSymbol typeParameterSymbol = new CrefTypeParameterSymbol(valueText, i, identifierNameSyntax);
						map.Add(valueText, typeParameterSymbol);
					}
				}
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00051210 File Offset: 0x0004F410
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if (base.CanConsiderTypeParameters(options))
			{
				foreach (KeyValuePair<string, MultiDictionary<string, TypeParameterSymbol>.ValueSet> keyValuePair in this.TypeParameterMap)
				{
					foreach (TypeParameterSymbol typeParameterSymbol in keyValuePair.Value)
					{
						result.AddSymbol(typeParameterSymbol, keyValuePair.Key, 0);
					}
				}
			}
		}

		// Token: 0x04000479 RID: 1145
		private readonly CrefSyntax _crefSyntax;

		// Token: 0x0400047A RID: 1146
		private MultiDictionary<string, TypeParameterSymbol> _lazyTypeParameterMap;
	}
}
