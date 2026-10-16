using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200050A RID: 1290
	internal static class OverriddenOrHiddenMembersHelpers
	{
		// Token: 0x06006C8D RID: 27789 RVA: 0x00190BEC File Offset: 0x0018EDEC
		internal static OverriddenOrHiddenMembersResult MakeOverriddenOrHiddenMembers(this MethodSymbol member)
		{
			return OverriddenOrHiddenMembersHelpers.MakeOverriddenOrHiddenMembersWorker(member);
		}

		// Token: 0x06006C8E RID: 27790 RVA: 0x00190BF4 File Offset: 0x0018EDF4
		internal static OverriddenOrHiddenMembersResult MakeOverriddenOrHiddenMembers(this PropertySymbol member)
		{
			return OverriddenOrHiddenMembersHelpers.MakeOverriddenOrHiddenMembersWorker(member);
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x00190BFC File Offset: 0x0018EDFC
		internal static OverriddenOrHiddenMembersResult MakeOverriddenOrHiddenMembers(this EventSymbol member)
		{
			return OverriddenOrHiddenMembersHelpers.MakeOverriddenOrHiddenMembersWorker(member);
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x00190C04 File Offset: 0x0018EE04
		private static OverriddenOrHiddenMembersResult MakeOverriddenOrHiddenMembersWorker(Symbol member)
		{
			if (!OverriddenOrHiddenMembersHelpers.CanOverrideOrHide(member))
			{
				return OverriddenOrHiddenMembersResult.Empty;
			}
			if (member.IsAccessor())
			{
				MethodSymbol methodSymbol = member as MethodSymbol;
				Symbol associatedSymbol = methodSymbol.AssociatedSymbol;
				if (associatedSymbol != null)
				{
					if (associatedSymbol.Kind == SymbolKind.Property)
					{
						return OverriddenOrHiddenMembersHelpers.MakePropertyAccessorOverriddenOrHiddenMembers(methodSymbol, (PropertySymbol)associatedSymbol);
					}
					return OverriddenOrHiddenMembersHelpers.MakeEventAccessorOverriddenOrHiddenMembers(methodSymbol, (EventSymbol)associatedSymbol);
				}
			}
			NamedTypeSymbol containingType = member.ContainingType;
			bool dangerous_IsFromSomeCompilation = member.Dangerous_IsFromSomeCompilation;
			if (containingType.IsInterface)
			{
				return OverriddenOrHiddenMembersHelpers.MakeInterfaceOverriddenOrHiddenMembers(member, dangerous_IsFromSomeCompilation);
			}
			ArrayBuilder<Symbol> arrayBuilder;
			ImmutableArray<Symbol> immutableArray;
			OverriddenOrHiddenMembersHelpers.FindOverriddenOrHiddenMembers(member, containingType, dangerous_IsFromSomeCompilation, out arrayBuilder, out immutableArray);
			ImmutableArray<Symbol> immutableArray2 = ((arrayBuilder == null) ? ImmutableArray<Symbol>.Empty : arrayBuilder.ToImmutableAndFree());
			return OverriddenOrHiddenMembersResult.Create(immutableArray, immutableArray2);
		}

		// Token: 0x06006C91 RID: 27793 RVA: 0x00190CAC File Offset: 0x0018EEAC
		private static void FindOverriddenOrHiddenMembers(Symbol member, NamedTypeSymbol containingType, bool memberIsFromSomeCompilation, out ArrayBuilder<Symbol> hiddenBuilder, out ImmutableArray<Symbol> overriddenMembers)
		{
			Symbol symbol = null;
			hiddenBuilder = null;
			MethodSymbol methodSymbol = member as MethodSymbol;
			Symbol symbol2;
			if (methodSymbol == null)
			{
				PEPropertySymbol pepropertySymbol = member as PEPropertySymbol;
				if (pepropertySymbol != null)
				{
					PEMethodSymbol pemethodSymbol = pepropertySymbol.GetMethod as PEMethodSymbol;
					if (pemethodSymbol != null)
					{
						MethodSymbol explicitlyOverriddenClassMethod = pemethodSymbol.ExplicitlyOverriddenClassMethod;
						if (explicitlyOverriddenClassMethod != null)
						{
							PropertySymbol propertySymbol = explicitlyOverriddenClassMethod.AssociatedSymbol as PropertySymbol;
							if (propertySymbol != null)
							{
								symbol2 = propertySymbol;
								goto IL_00A3;
							}
						}
					}
				}
				else
				{
					RetargetingPropertySymbol retargetingPropertySymbol = member as RetargetingPropertySymbol;
					if (retargetingPropertySymbol != null)
					{
						RetargetingMethodSymbol retargetingMethodSymbol = retargetingPropertySymbol.GetMethod as RetargetingMethodSymbol;
						if (retargetingMethodSymbol != null)
						{
							MethodSymbol explicitlyOverriddenClassMethod2 = retargetingMethodSymbol.ExplicitlyOverriddenClassMethod;
							if (explicitlyOverriddenClassMethod2 != null)
							{
								PropertySymbol propertySymbol2 = explicitlyOverriddenClassMethod2.AssociatedSymbol as PropertySymbol;
								if (propertySymbol2 != null)
								{
									symbol2 = propertySymbol2;
									goto IL_00A3;
								}
							}
						}
					}
				}
				symbol2 = null;
			}
			else
			{
				symbol2 = OverriddenOrHiddenMembersHelpers.KnownOverriddenClassMethod(methodSymbol);
			}
			IL_00A3:
			Symbol symbol3 = symbol2;
			NamedTypeSymbol namedTypeSymbol = containingType.BaseTypeNoUseSiteDiagnostics;
			while (namedTypeSymbol != null && symbol == null && hiddenBuilder == null)
			{
				bool flag;
				OverriddenOrHiddenMembersHelpers.FindOverriddenOrHiddenMembersInType(member, memberIsFromSomeCompilation, containingType, symbol3, namedTypeSymbol, out symbol, out flag, out hiddenBuilder);
				namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics;
			}
			OverriddenOrHiddenMembersHelpers.FindRelatedMembers(member.IsOverride, memberIsFromSomeCompilation, member.Kind, symbol, out overriddenMembers, ref hiddenBuilder);
		}

		// Token: 0x06006C92 RID: 27794 RVA: 0x00190DA4 File Offset: 0x0018EFA4
		public static Symbol FindFirstHiddenMemberIfAny(Symbol member, bool memberIsFromSomeCompilation)
		{
			ArrayBuilder<Symbol> arrayBuilder;
			ImmutableArray<Symbol> immutableArray;
			OverriddenOrHiddenMembersHelpers.FindOverriddenOrHiddenMembers(member, member.ContainingType, memberIsFromSomeCompilation, out arrayBuilder, out immutableArray);
			Symbol symbol = ((arrayBuilder != null) ? Enumerable.FirstOrDefault<Symbol>(arrayBuilder) : null);
			if (arrayBuilder != null)
			{
				arrayBuilder.Free();
			}
			return symbol;
		}

		// Token: 0x06006C93 RID: 27795 RVA: 0x00190DD8 File Offset: 0x0018EFD8
		private static MethodSymbol KnownOverriddenClassMethod(MethodSymbol method)
		{
			PEMethodSymbol pemethodSymbol = method as PEMethodSymbol;
			MethodSymbol methodSymbol;
			if (pemethodSymbol == null)
			{
				RetargetingMethodSymbol retargetingMethodSymbol = method as RetargetingMethodSymbol;
				if (retargetingMethodSymbol == null)
				{
					methodSymbol = null;
				}
				else
				{
					methodSymbol = retargetingMethodSymbol.ExplicitlyOverriddenClassMethod;
				}
			}
			else
			{
				methodSymbol = pemethodSymbol.ExplicitlyOverriddenClassMethod;
			}
			return methodSymbol;
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x00190E10 File Offset: 0x0018F010
		private static OverriddenOrHiddenMembersResult MakePropertyAccessorOverriddenOrHiddenMembers(MethodSymbol accessor, PropertySymbol associatedProperty)
		{
			bool flag = accessor.MethodKind == MethodKind.PropertyGet;
			MethodSymbol methodSymbol = null;
			ArrayBuilder<Symbol> arrayBuilder = null;
			OverriddenOrHiddenMembersResult overriddenOrHiddenMembers = associatedProperty.OverriddenOrHiddenMembers;
			foreach (Symbol symbol in overriddenOrHiddenMembers.HiddenMembers)
			{
				if (symbol.Kind == SymbolKind.Property)
				{
					PropertySymbol propertySymbol = (PropertySymbol)symbol;
					MethodSymbol methodSymbol2 = (flag ? propertySymbol.GetMethod : propertySymbol.SetMethod);
					if (methodSymbol2 != null)
					{
						OverriddenOrHiddenMembersHelpers.AccessOrGetInstance<Symbol>(ref arrayBuilder).Add(methodSymbol2);
					}
				}
			}
			if (overriddenOrHiddenMembers.OverriddenMembers.Any<Symbol>())
			{
				PropertySymbol propertySymbol2 = (PropertySymbol)overriddenOrHiddenMembers.OverriddenMembers[0];
				MethodSymbol methodSymbol3 = (flag ? propertySymbol2.GetOwnOrInheritedGetMethod() : propertySymbol2.GetOwnOrInheritedSetMethod());
				if (methodSymbol3 != null)
				{
					methodSymbol = methodSymbol3;
				}
			}
			OverriddenOrHiddenMembersHelpers.<>c__DisplayClass7_0 CS$<>8__locals1;
			CS$<>8__locals1.accessorIsFromSomeCompilation = accessor.Dangerous_IsFromSomeCompilation;
			ImmutableArray<Symbol> empty = ImmutableArray<Symbol>.Empty;
			if (methodSymbol != null && OverriddenOrHiddenMembersHelpers.IsOverriddenSymbolAccessible(methodSymbol, accessor.ContainingType) && OverriddenOrHiddenMembersHelpers.<MakePropertyAccessorOverriddenOrHiddenMembers>g__isAccessorOverride|7_0(accessor, methodSymbol, ref CS$<>8__locals1))
			{
				OverriddenOrHiddenMembersHelpers.FindRelatedMembers(accessor.IsOverride, CS$<>8__locals1.accessorIsFromSomeCompilation, accessor.Kind, methodSymbol, out empty, ref arrayBuilder);
			}
			ImmutableArray<Symbol> immutableArray = ((arrayBuilder == null) ? ImmutableArray<Symbol>.Empty : arrayBuilder.ToImmutableAndFree());
			return OverriddenOrHiddenMembersResult.Create(empty, immutableArray);
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x00190F40 File Offset: 0x0018F140
		private static OverriddenOrHiddenMembersResult MakeEventAccessorOverriddenOrHiddenMembers(MethodSymbol accessor, EventSymbol associatedEvent)
		{
			bool flag = accessor.MethodKind == MethodKind.EventAdd;
			MethodSymbol methodSymbol = null;
			ArrayBuilder<Symbol> arrayBuilder = null;
			OverriddenOrHiddenMembersResult overriddenOrHiddenMembers = associatedEvent.OverriddenOrHiddenMembers;
			foreach (Symbol symbol in overriddenOrHiddenMembers.HiddenMembers)
			{
				if (symbol.Kind == SymbolKind.Event)
				{
					EventSymbol eventSymbol = (EventSymbol)symbol;
					MethodSymbol methodSymbol2 = (flag ? eventSymbol.AddMethod : eventSymbol.RemoveMethod);
					if (methodSymbol2 != null)
					{
						OverriddenOrHiddenMembersHelpers.AccessOrGetInstance<Symbol>(ref arrayBuilder).Add(methodSymbol2);
					}
				}
			}
			if (overriddenOrHiddenMembers.OverriddenMembers.Any<Symbol>())
			{
				MethodSymbol ownOrInheritedAccessor = ((EventSymbol)overriddenOrHiddenMembers.OverriddenMembers[0]).GetOwnOrInheritedAccessor(flag);
				if (ownOrInheritedAccessor != null)
				{
					methodSymbol = ownOrInheritedAccessor;
				}
			}
			bool dangerous_IsFromSomeCompilation = accessor.Dangerous_IsFromSomeCompilation;
			ImmutableArray<Symbol> empty = ImmutableArray<Symbol>.Empty;
			if (methodSymbol != null && OverriddenOrHiddenMembersHelpers.IsOverriddenSymbolAccessible(methodSymbol, accessor.ContainingType) && (dangerous_IsFromSomeCompilation ? MemberSignatureComparer.CSharpAccessorOverrideComparer.Equals(accessor, methodSymbol) : MemberSignatureComparer.RuntimeSignatureComparer.Equals(accessor, methodSymbol)))
			{
				OverriddenOrHiddenMembersHelpers.FindRelatedMembers(accessor.IsOverride, dangerous_IsFromSomeCompilation, accessor.Kind, methodSymbol, out empty, ref arrayBuilder);
			}
			ImmutableArray<Symbol> immutableArray = ((arrayBuilder == null) ? ImmutableArray<Symbol>.Empty : arrayBuilder.ToImmutableAndFree());
			return OverriddenOrHiddenMembersResult.Create(empty, immutableArray);
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x00191068 File Offset: 0x0018F268
		internal static OverriddenOrHiddenMembersResult MakeInterfaceOverriddenOrHiddenMembers(Symbol member, bool memberIsFromSomeCompilation)
		{
			NamedTypeSymbol containingType = member.ContainingType;
			PooledHashSet<NamedTypeSymbol> instance = PooledHashSet<NamedTypeSymbol>.GetInstance();
			PooledHashSet<NamedTypeSymbol> instance2 = PooledHashSet<NamedTypeSymbol>.GetInstance();
			ArrayBuilder<Symbol> arrayBuilder = null;
			foreach (NamedTypeSymbol namedTypeSymbol in containingType.AllInterfacesNoUseSiteDiagnostics)
			{
				if (!instance2.Contains(namedTypeSymbol))
				{
					Symbol symbol;
					bool flag;
					ArrayBuilder<Symbol> arrayBuilder2;
					OverriddenOrHiddenMembersHelpers.FindOverriddenOrHiddenMembersInType(member, memberIsFromSomeCompilation, containingType, null, namedTypeSymbol, out symbol, out flag, out arrayBuilder2);
					bool flag2 = symbol != null;
					if (flag2)
					{
						foreach (NamedTypeSymbol namedTypeSymbol2 in namedTypeSymbol.AllInterfacesNoUseSiteDiagnostics)
						{
							instance2.Add(namedTypeSymbol2);
						}
						OverriddenOrHiddenMembersHelpers.AccessOrGetInstance<Symbol>(ref arrayBuilder).Add(symbol);
					}
					if (arrayBuilder2 != null)
					{
						if (!instance.Contains(namedTypeSymbol))
						{
							if (!flag2)
							{
								foreach (NamedTypeSymbol namedTypeSymbol3 in namedTypeSymbol.AllInterfacesNoUseSiteDiagnostics)
								{
									instance2.Add(namedTypeSymbol3);
								}
							}
							OverriddenOrHiddenMembersHelpers.AccessOrGetInstance<Symbol>(ref arrayBuilder).AddRange(arrayBuilder2);
						}
						arrayBuilder2.Free();
					}
					else if (flag && !flag2)
					{
						foreach (NamedTypeSymbol namedTypeSymbol4 in namedTypeSymbol.AllInterfacesNoUseSiteDiagnostics)
						{
							instance.Add(namedTypeSymbol4);
						}
					}
				}
			}
			instance.Free();
			instance2.Free();
			ImmutableArray<Symbol> empty = ImmutableArray<Symbol>.Empty;
			if (arrayBuilder != null)
			{
				ArrayBuilder<Symbol> arrayBuilder3 = null;
				foreach (Symbol symbol2 in arrayBuilder)
				{
					OverriddenOrHiddenMembersHelpers.FindRelatedMembers(member.IsOverride, memberIsFromSomeCompilation, member.Kind, symbol2, out empty, ref arrayBuilder3);
				}
				arrayBuilder.Free();
				arrayBuilder = arrayBuilder3;
			}
			ImmutableArray<Symbol> immutableArray = ((arrayBuilder == null) ? ImmutableArray<Symbol>.Empty : arrayBuilder.ToImmutableAndFree());
			return OverriddenOrHiddenMembersResult.Create(empty, immutableArray);
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x0019121C File Offset: 0x0018F41C
		private static void FindOverriddenOrHiddenMembersInType(Symbol member, bool memberIsFromSomeCompilation, NamedTypeSymbol memberContainingType, Symbol knownOverriddenMember, NamedTypeSymbol currType, out Symbol currTypeBestMatch, out bool currTypeHasSameKindNonMatch, out ArrayBuilder<Symbol> hiddenBuilder)
		{
			currTypeBestMatch = null;
			currTypeHasSameKindNonMatch = false;
			hiddenBuilder = null;
			bool flag = false;
			int num = int.MaxValue;
			IEqualityComparer<Symbol> equalityComparer = (memberIsFromSomeCompilation ? MemberSignatureComparer.CSharpCustomModifierOverrideComparer : MemberSignatureComparer.RuntimePlusRefOutSignatureComparer);
			IEqualityComparer<Symbol> equalityComparer2 = (memberIsFromSomeCompilation ? MemberSignatureComparer.CSharpOverrideComparer : MemberSignatureComparer.RuntimeSignatureComparer);
			SymbolKind kind = member.Kind;
			int memberArity = member.GetMemberArity();
			foreach (Symbol symbol in currType.GetMembers(member.Name))
			{
				if (OverriddenOrHiddenMembersHelpers.IsOverriddenSymbolAccessible(symbol, memberContainingType) && (!symbol.IsAccessor() || ((MethodSymbol)symbol).IsIndexedPropertyAccessor()))
				{
					if (symbol.Kind != kind)
					{
						int memberArity2 = symbol.GetMemberArity();
						if (memberArity2 == memberArity || (kind == SymbolKind.Method && memberArity2 == 0))
						{
							OverriddenOrHiddenMembersHelpers.AddHiddenMemberIfApplicable(ref hiddenBuilder, kind, symbol);
						}
					}
					else if (!flag)
					{
						if (kind != SymbolKind.Field)
						{
							if (kind != SymbolKind.NamedType)
							{
								if (symbol.Equals(knownOverriddenMember, TypeCompareKind.AllIgnoreOptions))
								{
									flag = true;
									currTypeBestMatch = symbol;
								}
								else if (knownOverriddenMember == null)
								{
									if (equalityComparer.Equals(member, symbol))
									{
										flag = true;
										currTypeBestMatch = symbol;
									}
									else if (equalityComparer2.Equals(member, symbol))
									{
										int num2 = OverriddenOrHiddenMembersHelpers.CustomModifierCount(symbol);
										if (num2 < num)
										{
											num = num2;
											currTypeBestMatch = symbol;
										}
									}
									else
									{
										currTypeHasSameKindNonMatch = true;
									}
								}
							}
							else if (symbol.GetMemberArity() == memberArity)
							{
								flag = true;
								currTypeBestMatch = symbol;
							}
						}
						else
						{
							flag = true;
							currTypeBestMatch = symbol;
						}
					}
				}
			}
			if (kind != SymbolKind.Field && kind != SymbolKind.NamedType && (flag && memberIsFromSomeCompilation) && member.IsDefinition && OverriddenOrHiddenMembersHelpers.TypeOrReturnTypeHasCustomModifiers(currTypeBestMatch))
			{
				Symbol symbol2 = currTypeBestMatch;
				foreach (Symbol symbol3 in currType.GetMembers(member.Name))
				{
					if (symbol3.Kind == currTypeBestMatch.Kind && symbol3 != currTypeBestMatch && MemberSignatureComparer.CSharpOverrideComparer.Equals(symbol3, currTypeBestMatch))
					{
						int num3 = OverriddenOrHiddenMembersHelpers.CustomModifierCount(symbol3);
						if (num3 < num)
						{
							num = num3;
							symbol2 = symbol3;
						}
					}
				}
				currTypeBestMatch = symbol2;
			}
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x00191428 File Offset: 0x0018F628
		private static void FindRelatedMembers(bool isOverride, bool overridingMemberIsFromSomeCompilation, SymbolKind overridingMemberKind, Symbol representativeMember, out ImmutableArray<Symbol> overriddenMembers, ref ArrayBuilder<Symbol> hiddenBuilder)
		{
			overriddenMembers = ImmutableArray<Symbol>.Empty;
			if (representativeMember != null)
			{
				bool flag = representativeMember.Kind != SymbolKind.Field && representativeMember.Kind != SymbolKind.NamedType && (!representativeMember.ContainingType.IsDefinition || representativeMember.IsIndexer());
				if (isOverride)
				{
					if (flag)
					{
						ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance();
						instance.Add(representativeMember);
						OverriddenOrHiddenMembersHelpers.FindOtherOverriddenMethodsInContainingType(representativeMember, overridingMemberIsFromSomeCompilation, instance);
						overriddenMembers = instance.ToImmutableAndFree();
						return;
					}
					overriddenMembers = ImmutableArray.Create<Symbol>(representativeMember);
					return;
				}
				else
				{
					OverriddenOrHiddenMembersHelpers.AddHiddenMemberIfApplicable(ref hiddenBuilder, overridingMemberKind, representativeMember);
					if (flag)
					{
						OverriddenOrHiddenMembersHelpers.FindOtherHiddenMembersInContainingType(overridingMemberKind, representativeMember, ref hiddenBuilder);
					}
				}
			}
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x001914BD File Offset: 0x0018F6BD
		private static void AddHiddenMemberIfApplicable(ref ArrayBuilder<Symbol> hiddenBuilder, SymbolKind hidingMemberKind, Symbol hiddenMember)
		{
			if (hiddenMember.Kind != SymbolKind.Method || ((MethodSymbol)hiddenMember).CanBeHiddenByMemberKind(hidingMemberKind))
			{
				OverriddenOrHiddenMembersHelpers.AccessOrGetInstance<Symbol>(ref hiddenBuilder).Add(hiddenMember);
			}
		}

		// Token: 0x06006C9A RID: 27802 RVA: 0x001914E3 File Offset: 0x0018F6E3
		private static ArrayBuilder<T> AccessOrGetInstance<T>(ref ArrayBuilder<T> builder)
		{
			if (builder == null)
			{
				builder = ArrayBuilder<T>.GetInstance();
			}
			return builder;
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x001914F4 File Offset: 0x0018F6F4
		private static void FindOtherOverriddenMethodsInContainingType(Symbol representativeMember, bool overridingMemberIsFromSomeCompilation, ArrayBuilder<Symbol> overriddenBuilder)
		{
			int num = -1;
			foreach (Symbol symbol in representativeMember.ContainingType.GetMembers(representativeMember.Name))
			{
				if (symbol.Kind == representativeMember.Kind && symbol != representativeMember)
				{
					if (overridingMemberIsFromSomeCompilation)
					{
						if (num < 0)
						{
							num = representativeMember.CustomModifierCount();
						}
						if (MemberSignatureComparer.CSharpOverrideComparer.Equals(symbol, representativeMember) && symbol.CustomModifierCount() == num)
						{
							overriddenBuilder.Add(symbol);
						}
					}
					else if (MemberSignatureComparer.CSharpCustomModifierOverrideComparer.Equals(symbol, representativeMember))
					{
						overriddenBuilder.Add(symbol);
					}
				}
			}
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x0019158C File Offset: 0x0018F78C
		private static void FindOtherHiddenMembersInContainingType(SymbolKind hidingMemberKind, Symbol representativeMember, ref ArrayBuilder<Symbol> hiddenBuilder)
		{
			IEqualityComparer<Symbol> csharpCustomModifierOverrideComparer = MemberSignatureComparer.CSharpCustomModifierOverrideComparer;
			foreach (Symbol symbol in representativeMember.ContainingType.GetMembers(representativeMember.Name))
			{
				if (symbol.Kind == representativeMember.Kind && symbol != representativeMember && csharpCustomModifierOverrideComparer.Equals(symbol, representativeMember))
				{
					OverriddenOrHiddenMembersHelpers.AddHiddenMemberIfApplicable(ref hiddenBuilder, hidingMemberKind, symbol);
				}
			}
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x001915F8 File Offset: 0x0018F7F8
		private static bool CanOverrideOrHide(Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind != SymbolKind.Event)
			{
				if (kind == SymbolKind.Method)
				{
					MethodSymbol methodSymbol = (MethodSymbol)member;
					return MethodSymbol.CanOverrideOrHide(methodSymbol.MethodKind) && methodSymbol == methodSymbol.ConstructedFrom;
				}
				if (kind != SymbolKind.Property)
				{
					throw ExceptionUtilities.UnexpectedValue(member.Kind);
				}
			}
			return !member.IsExplicitInterfaceImplementation();
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x00191654 File Offset: 0x0018F854
		private static bool TypeOrReturnTypeHasCustomModifiers(Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return ((EventSymbol)member).Type.HasCustomModifiers(false);
			}
			if (kind == SymbolKind.Method)
			{
				MethodSymbol methodSymbol = (MethodSymbol)member;
				TypeWithAnnotations returnTypeWithAnnotations = methodSymbol.ReturnTypeWithAnnotations;
				return returnTypeWithAnnotations.CustomModifiers.Any<CustomModifier>() || methodSymbol.RefCustomModifiers.Any<CustomModifier>() || returnTypeWithAnnotations.Type.HasCustomModifiers(false);
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(member.Kind);
			}
			PropertySymbol propertySymbol = (PropertySymbol)member;
			TypeWithAnnotations typeWithAnnotations = propertySymbol.TypeWithAnnotations;
			return typeWithAnnotations.CustomModifiers.Any<CustomModifier>() || propertySymbol.RefCustomModifiers.Any<CustomModifier>() || typeWithAnnotations.Type.HasCustomModifiers(false);
		}

		// Token: 0x06006C9F RID: 27807 RVA: 0x00191718 File Offset: 0x0018F918
		private static int CustomModifierCount(Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return ((EventSymbol)member).Type.CustomModifierCount();
			}
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)member).CustomModifierCount();
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(member.Kind);
			}
			return ((PropertySymbol)member).CustomModifierCount();
		}

		// Token: 0x06006CA0 RID: 27808 RVA: 0x00191778 File Offset: 0x0018F978
		internal static bool RequiresExplicitOverride(this MethodSymbol method, out bool warnAmbiguous)
		{
			warnAmbiguous = false;
			if (!method.IsOverride)
			{
				return false;
			}
			MethodSymbol overriddenMethod = method.OverriddenMethod;
			if (overriddenMethod == null)
			{
				return false;
			}
			bool flag;
			MethodSymbol firstRuntimeOverriddenMethodIgnoringNewSlot = method.GetFirstRuntimeOverriddenMethodIgnoringNewSlot(out flag);
			if (overriddenMethod == firstRuntimeOverriddenMethodIgnoringNewSlot && !flag)
			{
				return false;
			}
			if (method.ContainingAssembly.RuntimeSupportsCovariantReturnsOfClasses)
			{
				return true;
			}
			if (!method.ReturnType.Equals(overriddenMethod.ReturnType, TypeCompareKind.AllIgnoreOptions))
			{
				return true;
			}
			if (!overriddenMethod.MethodHasRuntimeCollision())
			{
				return true;
			}
			bool flag2 = overriddenMethod.IsDefinition || overriddenMethod.OriginalDefinition.MethodHasRuntimeCollision();
			warnAmbiguous = !flag2;
			return !overriddenMethod.ContainingType.Equals(firstRuntimeOverriddenMethodIgnoringNewSlot.ContainingType, TypeCompareKind.CLRSignatureCompareOptions) || (overriddenMethod != firstRuntimeOverriddenMethodIgnoringNewSlot && method.IsAccessor() != firstRuntimeOverriddenMethodIgnoringNewSlot.IsAccessor());
		}

		// Token: 0x06006CA1 RID: 27809 RVA: 0x00191838 File Offset: 0x0018FA38
		internal static bool MethodHasRuntimeCollision(this MethodSymbol method)
		{
			foreach (Symbol symbol in method.ContainingType.GetMembers(method.Name))
			{
				if (symbol != method && MemberSignatureComparer.RuntimeSignatureComparer.Equals(symbol, method))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006CA2 RID: 27810 RVA: 0x0019188C File Offset: 0x0018FA8C
		internal static MethodSymbol GetFirstRuntimeOverriddenMethodIgnoringNewSlot(this MethodSymbol method, out bool wasAmbiguous)
		{
			wasAmbiguous = false;
			if (!method.IsMetadataVirtual(true))
			{
				return null;
			}
			NamedTypeSymbol containingType = method.ContainingType;
			for (NamedTypeSymbol namedTypeSymbol = containingType.BaseTypeNoUseSiteDiagnostics; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics)
			{
				MethodSymbol methodSymbol = null;
				foreach (Symbol symbol in namedTypeSymbol.GetMembers(method.Name))
				{
					if (symbol.Kind == SymbolKind.Method && OverriddenOrHiddenMembersHelpers.IsOverriddenSymbolAccessible(symbol, containingType) && MemberSignatureComparer.RuntimeSignatureComparer.Equals(method, symbol))
					{
						MethodSymbol methodSymbol2 = (MethodSymbol)symbol;
						if (methodSymbol2.IsMetadataVirtual(true))
						{
							if (methodSymbol != null)
							{
								wasAmbiguous = true;
								return methodSymbol;
							}
							methodSymbol = methodSymbol2;
						}
					}
				}
				if (methodSymbol != null)
				{
					return methodSymbol;
				}
			}
			return null;
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x00191938 File Offset: 0x0018FB38
		private static bool IsOverriddenSymbolAccessible(Symbol overridden, NamedTypeSymbol overridingContainingType)
		{
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			return AccessCheck.IsSymbolAccessible(overridden.OriginalDefinition, overridingContainingType.OriginalDefinition, ref discarded, null);
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x0019195F File Offset: 0x0018FB5F
		[CompilerGenerated]
		internal static bool <MakePropertyAccessorOverriddenOrHiddenMembers>g__isAccessorOverride|7_0(MethodSymbol accessor, MethodSymbol overriddenAccessor, ref OverriddenOrHiddenMembersHelpers.<>c__DisplayClass7_0 A_2)
		{
			if (A_2.accessorIsFromSomeCompilation)
			{
				return MemberSignatureComparer.CSharpAccessorOverrideComparer.Equals(accessor, overriddenAccessor);
			}
			return overriddenAccessor.Equals(OverriddenOrHiddenMembersHelpers.KnownOverriddenClassMethod(accessor), TypeCompareKind.AllIgnoreOptions) || MemberSignatureComparer.RuntimeSignatureComparer.Equals(accessor, overriddenAccessor);
		}
	}
}
