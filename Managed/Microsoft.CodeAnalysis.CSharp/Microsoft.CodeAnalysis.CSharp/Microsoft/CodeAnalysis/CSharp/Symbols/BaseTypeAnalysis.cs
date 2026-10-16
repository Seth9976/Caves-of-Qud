using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D0 RID: 1232
	internal static class BaseTypeAnalysis
	{
		// Token: 0x060068A8 RID: 26792 RVA: 0x0018851C File Offset: 0x0018671C
		internal static bool TypeDependsOn(NamedTypeSymbol depends, NamedTypeSymbol on)
		{
			PooledHashSet<Symbol> instance = PooledHashSet<Symbol>.GetInstance();
			BaseTypeAnalysis.TypeDependsClosure(depends, depends.DeclaringCompilation, instance);
			bool flag = instance.Contains(on);
			instance.Free();
			return flag;
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x0018854C File Offset: 0x0018674C
		private static void TypeDependsClosure(NamedTypeSymbol type, CSharpCompilation currentCompilation, HashSet<Symbol> partialClosure)
		{
			if (type == null)
			{
				return;
			}
			type = type.OriginalDefinition;
			if (partialClosure.Add(type))
			{
				if (type.IsInterface)
				{
					foreach (NamedTypeSymbol namedTypeSymbol in type.GetDeclaredInterfaces(null))
					{
						BaseTypeAnalysis.TypeDependsClosure(namedTypeSymbol, currentCompilation, partialClosure);
					}
				}
				else
				{
					BaseTypeAnalysis.TypeDependsClosure(type.GetDeclaredBaseType(null), currentCompilation, partialClosure);
				}
				if (currentCompilation != null && type.IsFromCompilation(currentCompilation))
				{
					BaseTypeAnalysis.TypeDependsClosure(type.ContainingType, currentCompilation, partialClosure);
				}
			}
		}

		// Token: 0x060068AA RID: 26794 RVA: 0x001885C8 File Offset: 0x001867C8
		internal static bool StructDependsOn(NamedTypeSymbol depends, NamedTypeSymbol on)
		{
			PooledHashSet<Symbol> instance = PooledHashSet<Symbol>.GetInstance();
			BaseTypeAnalysis.StructDependsClosure(depends, instance, on);
			bool flag = instance.Contains(on);
			instance.Free();
			return flag;
		}

		// Token: 0x060068AB RID: 26795 RVA: 0x001885F0 File Offset: 0x001867F0
		private static void StructDependsClosure(NamedTypeSymbol type, HashSet<Symbol> partialClosure, NamedTypeSymbol on)
		{
			if (type.OriginalDefinition == on)
			{
				partialClosure.Add(on);
				return;
			}
			if (partialClosure.Add(type))
			{
				foreach (Symbol symbol in type.GetMembersUnordered())
				{
					FieldSymbol fieldSymbol = symbol as FieldSymbol;
					TypeSymbol typeSymbol = ((fieldSymbol != null) ? fieldSymbol.NonPointerType() : null);
					if (typeSymbol != null && typeSymbol.TypeKind == TypeKind.Struct && !fieldSymbol.IsStatic)
					{
						BaseTypeAnalysis.StructDependsClosure((NamedTypeSymbol)typeSymbol, partialClosure, on);
					}
				}
			}
		}

		// Token: 0x060068AC RID: 26796 RVA: 0x00188670 File Offset: 0x00186870
		internal static ManagedKind GetManagedKind(NamedTypeSymbol type, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ValueTuple<ThreeState, bool> valueTuple = BaseTypeAnalysis.IsManagedTypeHelper(type);
			ThreeState item = valueTuple.Item1;
			bool flag = valueTuple.Item2;
			bool flag2 = item == ThreeState.True;
			if (item == ThreeState.Unknown)
			{
				PooledHashSet<Symbol> instance = PooledHashSet<Symbol>.GetInstance();
				ValueTuple<bool, bool> valueTuple2 = BaseTypeAnalysis.DependsOnDefinitelyManagedType(type, instance, ref useSiteInfo);
				flag2 = valueTuple2.Item1;
				flag = flag || valueTuple2.Item2;
				instance.Free();
			}
			if (flag2)
			{
				return ManagedKind.Managed;
			}
			if (flag)
			{
				return ManagedKind.UnmanagedWithGenerics;
			}
			return ManagedKind.Unmanaged;
		}

		// Token: 0x060068AD RID: 26797 RVA: 0x001886CF File Offset: 0x001868CF
		internal static TypeSymbol NonPointerType(this FieldSymbol field)
		{
			if (!field.HasPointerType)
			{
				return field.Type;
			}
			return null;
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x001886E4 File Offset: 0x001868E4
		[return: TupleElementNames(new string[] { "definitelyManaged", "hasGenerics" })]
		private static ValueTuple<bool, bool> DependsOnDefinitelyManagedType(NamedTypeSymbol type, HashSet<Symbol> partialClosure, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			bool flag = false;
			if (partialClosure.Add(type))
			{
				foreach (Symbol symbol in type.GetInstanceFieldsAndEvents())
				{
					SymbolKind kind = symbol.Kind;
					FieldSymbol fieldSymbol;
					if (kind != SymbolKind.Event)
					{
						if (kind != SymbolKind.Field)
						{
							throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
						}
						fieldSymbol = (FieldSymbol)symbol;
					}
					else
					{
						fieldSymbol = ((EventSymbol)symbol).AssociatedField;
					}
					if (fieldSymbol != null)
					{
						TypeSymbol typeSymbol = fieldSymbol.NonPointerType();
						if (typeSymbol != null)
						{
							typeSymbol.AddUseSiteInfo(ref useSiteInfo, true);
							NamedTypeSymbol namedTypeSymbol = typeSymbol as NamedTypeSymbol;
							if (namedTypeSymbol == null)
							{
								if (typeSymbol.IsManagedType(ref useSiteInfo))
								{
									return new ValueTuple<bool, bool>(true, flag);
								}
							}
							else
							{
								ValueTuple<ThreeState, bool> valueTuple = BaseTypeAnalysis.IsManagedTypeHelper(namedTypeSymbol);
								flag = flag || valueTuple.Item2;
								switch (valueTuple.Item1)
								{
								case ThreeState.Unknown:
									if (!namedTypeSymbol.OriginalDefinition.KnownCircularStruct)
									{
										ValueTuple<bool, bool> valueTuple2 = BaseTypeAnalysis.DependsOnDefinitelyManagedType(namedTypeSymbol, partialClosure, ref useSiteInfo);
										bool item = valueTuple2.Item1;
										bool item2 = valueTuple2.Item2;
										flag = flag || item2;
										if (item)
										{
											return new ValueTuple<bool, bool>(true, flag);
										}
									}
									break;
								case ThreeState.True:
									return new ValueTuple<bool, bool>(true, flag);
								}
							}
						}
					}
				}
			}
			return new ValueTuple<bool, bool>(false, flag);
		}

		// Token: 0x060068AF RID: 26799 RVA: 0x00188850 File Offset: 0x00186A50
		[return: TupleElementNames(new string[] { "isManaged", "hasGenerics" })]
		private static ValueTuple<ThreeState, bool> IsManagedTypeHelper(NamedTypeSymbol type)
		{
			if (type.IsEnumType())
			{
				type = type.GetEnumUnderlyingType();
			}
			switch (type.SpecialType)
			{
			case SpecialType.System_Void:
			case SpecialType.System_Boolean:
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Int64:
			case SpecialType.System_UInt64:
			case SpecialType.System_Decimal:
			case SpecialType.System_Single:
			case SpecialType.System_Double:
			case SpecialType.System_IntPtr:
			case SpecialType.System_UIntPtr:
			case SpecialType.System_TypedReference:
			case SpecialType.System_ArgIterator:
			case SpecialType.System_RuntimeArgumentHandle:
				return new ValueTuple<ThreeState, bool>(ThreeState.False, false);
			}
			bool isGenericType = type.IsGenericType;
			TypeKind typeKind = type.TypeKind;
			if (typeKind == TypeKind.Enum)
			{
				return new ValueTuple<ThreeState, bool>(ThreeState.False, isGenericType);
			}
			if (typeKind != TypeKind.Struct)
			{
				return new ValueTuple<ThreeState, bool>(ThreeState.True, isGenericType);
			}
			return new ValueTuple<ThreeState, bool>(ThreeState.Unknown, isGenericType);
		}
	}
}
