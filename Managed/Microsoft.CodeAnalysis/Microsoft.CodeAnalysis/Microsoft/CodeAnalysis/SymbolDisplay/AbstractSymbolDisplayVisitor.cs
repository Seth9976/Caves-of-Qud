using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.SymbolDisplay
{
	// Token: 0x020002C7 RID: 711
	internal abstract class AbstractSymbolDisplayVisitor : SymbolVisitor
	{
		// Token: 0x06001DB7 RID: 7607 RVA: 0x00056046 File Offset: 0x00054246
		protected AbstractSymbolDisplayVisitor(ArrayBuilder<SymbolDisplayPart> builder, SymbolDisplayFormat format, bool isFirstSymbolVisited, SemanticModel semanticModelOpt, int positionOpt, bool inNamespaceOrType = false)
		{
			this.builder = builder;
			this.format = format;
			this.isFirstSymbolVisited = isFirstSymbolVisited;
			this.semanticModelOpt = semanticModelOpt;
			this.positionOpt = positionOpt;
			this.inNamespaceOrType = inNamespaceOrType;
			if (!isFirstSymbolVisited)
			{
				this._lazyNotFirstVisitor = this;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x00056085 File Offset: 0x00054285
		protected AbstractSymbolDisplayVisitor NotFirstVisitor
		{
			get
			{
				if (this._lazyNotFirstVisitor == null)
				{
					this._lazyNotFirstVisitor = this.MakeNotFirstVisitor(false);
				}
				return this._lazyNotFirstVisitor;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x000560A2 File Offset: 0x000542A2
		protected AbstractSymbolDisplayVisitor NotFirstVisitorNamespaceOrType
		{
			get
			{
				if (this._lazyNotFirstVisitorNamespaceOrType == null)
				{
					this._lazyNotFirstVisitorNamespaceOrType = this.MakeNotFirstVisitor(true);
				}
				return this._lazyNotFirstVisitorNamespaceOrType;
			}
		}

		// Token: 0x06001DBA RID: 7610
		protected abstract AbstractSymbolDisplayVisitor MakeNotFirstVisitor(bool inNamespaceOrType = false);

		// Token: 0x06001DBB RID: 7611
		protected abstract void AddLiteralValue(SpecialType type, object value);

		// Token: 0x06001DBC RID: 7612
		protected abstract void AddExplicitlyCastedLiteralValue(INamedTypeSymbol namedType, SpecialType type, object value);

		// Token: 0x06001DBD RID: 7613
		protected abstract void AddSpace();

		// Token: 0x06001DBE RID: 7614
		protected abstract void AddBitwiseOr();

		// Token: 0x06001DBF RID: 7615 RVA: 0x000560BF File Offset: 0x000542BF
		protected void AddNonNullConstantValue(ITypeSymbol type, object constantValue, bool preferNumericValueOrExpandedFlagsForEnum = false)
		{
			if (type.TypeKind == TypeKind.Enum)
			{
				this.AddEnumConstantValue((INamedTypeSymbol)type, constantValue, preferNumericValueOrExpandedFlagsForEnum);
				return;
			}
			this.AddLiteralValue(type.SpecialType, constantValue);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000560E6 File Offset: 0x000542E6
		private void AddEnumConstantValue(INamedTypeSymbol enumType, object constantValue, bool preferNumericValueOrExpandedFlags)
		{
			if (AbstractSymbolDisplayVisitor.IsFlagsEnum(enumType))
			{
				this.AddFlagsEnumConstantValue(enumType, constantValue, preferNumericValueOrExpandedFlags);
				return;
			}
			if (preferNumericValueOrExpandedFlags)
			{
				this.AddLiteralValue(enumType.EnumUnderlyingType.SpecialType, constantValue);
				return;
			}
			this.AddNonFlagsEnumConstantValue(enumType, constantValue);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00056118 File Offset: 0x00054318
		private static bool IsFlagsEnum(ITypeSymbol typeSymbol)
		{
			if (typeSymbol.TypeKind != TypeKind.Enum)
			{
				return false;
			}
			foreach (AttributeData attributeData in typeSymbol.GetAttributes())
			{
				IMethodSymbol attributeConstructor = attributeData.AttributeConstructor;
				if (attributeConstructor != null)
				{
					INamedTypeSymbol containingType = attributeConstructor.ContainingType;
					if (!attributeConstructor.Parameters.Any<IParameterSymbol>() && containingType.Name == "FlagsAttribute")
					{
						ISymbol containingSymbol = containingType.ContainingSymbol;
						if (containingSymbol.Kind == SymbolKind.Namespace && containingSymbol.Name == "System" && ((INamespaceSymbol)containingSymbol.ContainingSymbol).IsGlobalNamespace)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000561BC File Offset: 0x000543BC
		private void AddFlagsEnumConstantValue(INamedTypeSymbol enumType, object constantValue, bool preferNumericValueOrExpandedFlags)
		{
			ArrayBuilder<EnumField> instance = ArrayBuilder<EnumField>.GetInstance();
			AbstractSymbolDisplayVisitor.GetSortedEnumFields(enumType, instance);
			ArrayBuilder<EnumField> instance2 = ArrayBuilder<EnumField>.GetInstance();
			try
			{
				this.AddFlagsEnumConstantValue(enumType, constantValue, instance, instance2, preferNumericValueOrExpandedFlags);
			}
			finally
			{
				instance.Free();
				instance2.Free();
			}
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00056208 File Offset: 0x00054408
		private void AddFlagsEnumConstantValue(INamedTypeSymbol enumType, object constantValue, ArrayBuilder<EnumField> allFieldsAndValues, ArrayBuilder<EnumField> usedFieldsAndValues, bool preferNumericValueOrExpandedFlags)
		{
			SpecialType specialType = enumType.EnumUnderlyingType.SpecialType;
			ulong num = EnumUtilities.ConvertEnumUnderlyingTypeToUInt64(constantValue, specialType);
			ulong num2 = num;
			if (num2 != 0UL)
			{
				foreach (EnumField enumField in allFieldsAndValues)
				{
					ulong value = enumField.Value;
					if ((!preferNumericValueOrExpandedFlags || value != num) && value != 0UL && (num2 & value) == value)
					{
						usedFieldsAndValues.Add(enumField);
						num2 -= value;
						if (num2 == 0UL)
						{
							break;
						}
					}
				}
			}
			if (num2 == 0UL && usedFieldsAndValues.Count > 0)
			{
				for (int i = usedFieldsAndValues.Count - 1; i >= 0; i--)
				{
					if (i != usedFieldsAndValues.Count - 1)
					{
						this.AddSpace();
						this.AddBitwiseOr();
						this.AddSpace();
					}
					((IFieldSymbol)usedFieldsAndValues[i].IdentityOpt).Accept(this.NotFirstVisitor);
				}
				return;
			}
			if (preferNumericValueOrExpandedFlags)
			{
				this.AddLiteralValue(specialType, constantValue);
				return;
			}
			EnumField enumField2 = ((num == 0UL) ? EnumField.FindValue(allFieldsAndValues, 0UL) : default(EnumField));
			if (!enumField2.IsDefault)
			{
				((IFieldSymbol)enumField2.IdentityOpt).Accept(this.NotFirstVisitor);
				return;
			}
			this.AddExplicitlyCastedLiteralValue(enumType, specialType, constantValue);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0005632C File Offset: 0x0005452C
		private static void GetSortedEnumFields(INamedTypeSymbol enumType, ArrayBuilder<EnumField> enumFields)
		{
			SpecialType specialType = enumType.EnumUnderlyingType.SpecialType;
			foreach (ISymbol symbol in enumType.GetMembers())
			{
				if (symbol.Kind == SymbolKind.Field)
				{
					IFieldSymbol fieldSymbol = (IFieldSymbol)symbol;
					if (fieldSymbol.HasConstantValue)
					{
						EnumField enumField = new EnumField(fieldSymbol.Name, EnumUtilities.ConvertEnumUnderlyingTypeToUInt64(fieldSymbol.ConstantValue, specialType), fieldSymbol);
						enumFields.Add(enumField);
					}
				}
			}
			enumFields.Sort(EnumField.Comparer);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000563B4 File Offset: 0x000545B4
		private void AddNonFlagsEnumConstantValue(INamedTypeSymbol enumType, object constantValue)
		{
			SpecialType specialType = enumType.EnumUnderlyingType.SpecialType;
			ulong num = EnumUtilities.ConvertEnumUnderlyingTypeToUInt64(constantValue, specialType);
			ArrayBuilder<EnumField> instance = ArrayBuilder<EnumField>.GetInstance();
			AbstractSymbolDisplayVisitor.GetSortedEnumFields(enumType, instance);
			EnumField enumField = EnumField.FindValue(instance, num);
			if (!enumField.IsDefault)
			{
				((IFieldSymbol)enumField.IdentityOpt).Accept(this.NotFirstVisitor);
			}
			else
			{
				this.AddExplicitlyCastedLiteralValue(enumType, specialType, constantValue);
			}
			instance.Free();
		}

		// Token: 0x06001DC6 RID: 7622
		protected abstract bool ShouldRestrictMinimallyQualifyLookupToNamespacesAndTypes();

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0005641A File Offset: 0x0005461A
		protected bool IsMinimizing
		{
			get
			{
				return this.semanticModelOpt != null;
			}
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00056428 File Offset: 0x00054628
		protected bool NameBoundSuccessfullyToSameSymbol(INamedTypeSymbol symbol)
		{
			ISymbol symbol2 = AbstractSymbolDisplayVisitor.SingleSymbolWithArity(this.ShouldRestrictMinimallyQualifyLookupToNamespacesAndTypes() ? this.semanticModelOpt.LookupNamespacesAndTypes(this.positionOpt, null, symbol.Name) : this.semanticModelOpt.LookupSymbols(this.positionOpt, null, symbol.Name, false), symbol.Arity);
			if (symbol2 == null)
			{
				return false;
			}
			if (symbol2.Equals(symbol.OriginalDefinition))
			{
				return true;
			}
			ISymbol symbol3 = AbstractSymbolDisplayVisitor.SingleSymbolWithArity(this.semanticModelOpt.LookupNamespacesAndTypes(this.positionOpt, null, symbol.Name), symbol.Arity);
			if (symbol3 == null)
			{
				return false;
			}
			ITypeSymbol symbolType = AbstractSymbolDisplayVisitor.GetSymbolType(symbol2);
			ITypeSymbol symbolType2 = AbstractSymbolDisplayVisitor.GetSymbolType(symbol3);
			return symbolType != null && symbolType2 != null && symbolType.Equals(symbolType2) && symbol3.Equals(symbol.OriginalDefinition);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000564E8 File Offset: 0x000546E8
		private static ISymbol SingleSymbolWithArity(ImmutableArray<ISymbol> candidates, int desiredArity)
		{
			ISymbol symbol = null;
			foreach (ISymbol symbol2 in candidates)
			{
				SymbolKind kind = symbol2.Kind;
				int num;
				if (kind != SymbolKind.Method)
				{
					if (kind == SymbolKind.NamedType)
					{
						num = ((INamedTypeSymbol)symbol2).Arity;
					}
					else
					{
						num = 0;
					}
				}
				else
				{
					num = ((IMethodSymbol)symbol2).Arity;
				}
				if (num == desiredArity)
				{
					if (symbol != null)
					{
						symbol = null;
						break;
					}
					symbol = symbol2;
				}
			}
			return symbol;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00056554 File Offset: 0x00054754
		protected static ITypeSymbol GetSymbolType(ISymbol symbol)
		{
			ILocalSymbol localSymbol = symbol as ILocalSymbol;
			if (localSymbol != null)
			{
				return localSymbol.Type;
			}
			IFieldSymbol fieldSymbol = symbol as IFieldSymbol;
			if (fieldSymbol != null)
			{
				return fieldSymbol.Type;
			}
			IPropertySymbol propertySymbol = symbol as IPropertySymbol;
			if (propertySymbol != null)
			{
				return propertySymbol.Type;
			}
			IParameterSymbol parameterSymbol = symbol as IParameterSymbol;
			if (parameterSymbol != null)
			{
				return parameterSymbol.Type;
			}
			IAliasSymbol aliasSymbol = symbol as IAliasSymbol;
			if (aliasSymbol != null)
			{
				return aliasSymbol.Target as ITypeSymbol;
			}
			return symbol as ITypeSymbol;
		}

		// Token: 0x04000D70 RID: 3440
		protected readonly ArrayBuilder<SymbolDisplayPart> builder;

		// Token: 0x04000D71 RID: 3441
		protected readonly SymbolDisplayFormat format;

		// Token: 0x04000D72 RID: 3442
		protected readonly bool isFirstSymbolVisited;

		// Token: 0x04000D73 RID: 3443
		protected readonly bool inNamespaceOrType;

		// Token: 0x04000D74 RID: 3444
		protected readonly SemanticModel semanticModelOpt;

		// Token: 0x04000D75 RID: 3445
		protected readonly int positionOpt;

		// Token: 0x04000D76 RID: 3446
		private AbstractSymbolDisplayVisitor _lazyNotFirstVisitor;

		// Token: 0x04000D77 RID: 3447
		private AbstractSymbolDisplayVisitor _lazyNotFirstVisitorNamespaceOrType;
	}
}
