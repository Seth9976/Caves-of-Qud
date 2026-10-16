using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200051B RID: 1307
	internal static class ExplicitInterfaceHelpers
	{
		// Token: 0x06006DB5 RID: 28085 RVA: 0x0019391C File Offset: 0x00191B1C
		public static string GetMemberName(Binder binder, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierOpt, string name)
		{
			TypeSymbol typeSymbol;
			string text;
			return ExplicitInterfaceHelpers.GetMemberNameAndInterfaceSymbol(binder, explicitInterfaceSpecifierOpt, name, BindingDiagnosticBag.Discarded, out typeSymbol, out text);
		}

		// Token: 0x06006DB6 RID: 28086 RVA: 0x0019393C File Offset: 0x00191B3C
		public static string GetMemberNameAndInterfaceSymbol(Binder binder, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierOpt, string name, BindingDiagnosticBag diagnostics, out TypeSymbol explicitInterfaceTypeOpt, out string aliasQualifierOpt)
		{
			if (explicitInterfaceSpecifierOpt == null)
			{
				explicitInterfaceTypeOpt = null;
				aliasQualifierOpt = null;
				return name;
			}
			binder = binder.WithAdditionalFlags(BinderFlags.SuppressConstraintChecks | BinderFlags.SuppressObsoleteChecks);
			NameSyntax name2 = explicitInterfaceSpecifierOpt.Name;
			explicitInterfaceTypeOpt = binder.BindType(name2, diagnostics, null, false).Type;
			aliasQualifierOpt = name2.GetAliasQualifierOpt();
			return ExplicitInterfaceHelpers.GetMemberName(name, explicitInterfaceTypeOpt, aliasQualifierOpt);
		}

		// Token: 0x06006DB7 RID: 28087 RVA: 0x00193990 File Offset: 0x00191B90
		public static string GetMemberName(string name, TypeSymbol explicitInterfaceTypeOpt, string aliasQualifierOpt)
		{
			if (explicitInterfaceTypeOpt == null)
			{
				return name;
			}
			string text = explicitInterfaceTypeOpt.ToDisplayString(SymbolDisplayFormat.ExplicitInterfaceImplementationFormat);
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			if (!string.IsNullOrEmpty(aliasQualifierOpt))
			{
				builder.Append(aliasQualifierOpt);
				builder.Append("::");
			}
			string text2 = text;
			for (int i = 0; i < text2.Length; i++)
			{
				char c = text2.get_Chars(i);
				if (c != ' ')
				{
					builder.Append(c);
				}
			}
			builder.Append(".");
			builder.Append(name);
			return instance.ToStringAndFree();
		}

		// Token: 0x06006DB8 RID: 28088 RVA: 0x00193A1A File Offset: 0x00191C1A
		public static string GetMethodNameWithoutInterfaceName(this MethodSymbol method)
		{
			if (method.MethodKind != MethodKind.ExplicitInterfaceImplementation)
			{
				return method.Name;
			}
			return ExplicitInterfaceHelpers.GetMemberNameWithoutInterfaceName(method.Name);
		}

		// Token: 0x06006DB9 RID: 28089 RVA: 0x00193A38 File Offset: 0x00191C38
		public static string GetMemberNameWithoutInterfaceName(string fullName)
		{
			int num = fullName.LastIndexOf('.');
			if (num <= 0)
			{
				return fullName;
			}
			return fullName.Substring(num + 1);
		}

		// Token: 0x06006DBA RID: 28090 RVA: 0x00193A60 File Offset: 0x00191C60
		public static ImmutableArray<T> SubstituteExplicitInterfaceImplementations<T>(ImmutableArray<T> unsubstitutedExplicitInterfaceImplementations, TypeMap map) where T : Symbol
		{
			ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance();
			foreach (T t in unsubstitutedExplicitInterfaceImplementations)
			{
				NamedTypeSymbol containingType = t.ContainingType;
				NamespaceOrTypeSymbol namespaceOrTypeSymbol = map.SubstituteNamedType(containingType);
				string name = t.Name;
				T t2 = default(T);
				foreach (Symbol symbol in namespaceOrTypeSymbol.GetMembers(name))
				{
					if (symbol.OriginalDefinition == t.OriginalDefinition)
					{
						t2 = (T)((object)symbol);
						break;
					}
				}
				instance.Add(t2);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06006DBB RID: 28091 RVA: 0x00193B14 File Offset: 0x00191D14
		internal static MethodSymbol FindExplicitlyImplementedMethod(this MethodSymbol implementingMethod, TypeSymbol explicitInterfaceType, string interfaceMethodName, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax, BindingDiagnosticBag diagnostics)
		{
			return (MethodSymbol)ExplicitInterfaceHelpers.FindExplicitlyImplementedMember(implementingMethod, explicitInterfaceType, interfaceMethodName, explicitInterfaceSpecifierSyntax, diagnostics);
		}

		// Token: 0x06006DBC RID: 28092 RVA: 0x00193B26 File Offset: 0x00191D26
		internal static PropertySymbol FindExplicitlyImplementedProperty(this PropertySymbol implementingProperty, TypeSymbol explicitInterfaceType, string interfacePropertyName, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax, BindingDiagnosticBag diagnostics)
		{
			return (PropertySymbol)ExplicitInterfaceHelpers.FindExplicitlyImplementedMember(implementingProperty, explicitInterfaceType, interfacePropertyName, explicitInterfaceSpecifierSyntax, diagnostics);
		}

		// Token: 0x06006DBD RID: 28093 RVA: 0x00193B38 File Offset: 0x00191D38
		internal static EventSymbol FindExplicitlyImplementedEvent(this EventSymbol implementingEvent, TypeSymbol explicitInterfaceType, string interfaceEventName, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax, BindingDiagnosticBag diagnostics)
		{
			return (EventSymbol)ExplicitInterfaceHelpers.FindExplicitlyImplementedMember(implementingEvent, explicitInterfaceType, interfaceEventName, explicitInterfaceSpecifierSyntax, diagnostics);
		}

		// Token: 0x06006DBE RID: 28094 RVA: 0x00193B4C File Offset: 0x00191D4C
		private static Symbol FindExplicitlyImplementedMember(Symbol implementingMember, TypeSymbol explicitInterfaceType, string interfaceMemberName, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax, BindingDiagnosticBag diagnostics)
		{
			ExplicitInterfaceHelpers.<>c__DisplayClass9_0 CS$<>8__locals1;
			CS$<>8__locals1.implementingMember = implementingMember;
			CS$<>8__locals1.diagnostics = diagnostics;
			if (explicitInterfaceType == null)
			{
				return null;
			}
			CS$<>8__locals1.memberLocation = CS$<>8__locals1.implementingMember.Locations[0];
			NamedTypeSymbol containingType = CS$<>8__locals1.implementingMember.ContainingType;
			TypeKind typeKind = containingType.TypeKind;
			if (typeKind != TypeKind.Class && typeKind != TypeKind.Interface && typeKind != TypeKind.Struct)
			{
				CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ExplicitInterfaceImplementationInNonClassOrStruct, CS$<>8__locals1.memberLocation, new object[] { CS$<>8__locals1.implementingMember });
				return null;
			}
			if (!explicitInterfaceType.IsInterfaceType())
			{
				SourceLocation sourceLocation = new SourceLocation(explicitInterfaceSpecifierSyntax.Name);
				CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ExplicitInterfaceImplementationNotInterface, sourceLocation, new object[] { explicitInterfaceType });
				return null;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)explicitInterfaceType;
			MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>.ValueSet valueSet = containingType.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics[namedTypeSymbol];
			int count = valueSet.Count;
			if (count == 0 || !valueSet.Contains(namedTypeSymbol, SymbolEqualityComparer.ObliviousNullableModifierMatchesAny))
			{
				SourceLocation sourceLocation2 = new SourceLocation(explicitInterfaceSpecifierSyntax.Name);
				if (count > 0 && valueSet.Contains(namedTypeSymbol, SymbolEqualityComparer.IgnoringNullable))
				{
					CS$<>8__locals1.diagnostics.Add(ErrorCode.WRN_NullabilityMismatchInExplicitlyImplementedInterface, sourceLocation2);
				}
				else
				{
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ClassDoesntImplementInterface, sourceLocation2, new object[] { CS$<>8__locals1.implementingMember, namedTypeSymbol });
				}
			}
			if (containingType == namedTypeSymbol.OriginalDefinition)
			{
				return null;
			}
			bool flag = CS$<>8__locals1.implementingMember.HasParamsParameter();
			bool flag2 = false;
			Symbol symbol = null;
			foreach (Symbol symbol2 in namedTypeSymbol.GetMembers(interfaceMemberName))
			{
				if (symbol2.Kind == CS$<>8__locals1.implementingMember.Kind && symbol2.IsImplementableInterfaceMember() && MemberSignatureComparer.ExplicitImplementationComparer.Equals(CS$<>8__locals1.implementingMember, symbol2))
				{
					flag2 = true;
					if (!symbol2.IsAccessor() || ((MethodSymbol)symbol2).IsIndexedPropertyAccessor())
					{
						if (symbol2.MustCallMethodsDirectly())
						{
							CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_BogusExplicitImpl, CS$<>8__locals1.memberLocation, new object[] { CS$<>8__locals1.implementingMember, symbol2 });
						}
						else if (flag && !symbol2.HasParamsParameter())
						{
							CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ExplicitImplParams, CS$<>8__locals1.memberLocation, new object[] { CS$<>8__locals1.implementingMember, symbol2 });
						}
						symbol = symbol2;
						break;
					}
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_ExplicitMethodImplAccessor, CS$<>8__locals1.memberLocation, new object[] { CS$<>8__locals1.implementingMember, symbol2 });
				}
			}
			if (!flag2)
			{
				CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_InterfaceMemberNotFound, CS$<>8__locals1.memberLocation, new object[] { CS$<>8__locals1.implementingMember });
			}
			if (symbol != null)
			{
				ExplicitInterfaceHelpers.<>c__DisplayClass9_1 CS$<>8__locals2;
				CS$<>8__locals2.useSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(CS$<>8__locals1.diagnostics, CS$<>8__locals1.implementingMember.ContainingAssembly);
				if (!AccessCheck.IsSymbolAccessible(symbol, CS$<>8__locals1.implementingMember.ContainingType, ref CS$<>8__locals2.useSiteInfo, null))
				{
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_BadAccess, CS$<>8__locals1.memberLocation, new object[] { symbol });
				}
				else
				{
					SymbolKind kind = symbol.Kind;
					if (kind != SymbolKind.Event)
					{
						if (kind == SymbolKind.Property)
						{
							PropertySymbol propertySymbol = (PropertySymbol)symbol;
							ExplicitInterfaceHelpers.<FindExplicitlyImplementedMember>g__checkAccessorIsAccessibleIfImplementable|9_0(propertySymbol.GetMethod, ref CS$<>8__locals1, ref CS$<>8__locals2);
							ExplicitInterfaceHelpers.<FindExplicitlyImplementedMember>g__checkAccessorIsAccessibleIfImplementable|9_0(propertySymbol.SetMethod, ref CS$<>8__locals1, ref CS$<>8__locals2);
						}
					}
					else
					{
						EventSymbol eventSymbol = (EventSymbol)symbol;
						ExplicitInterfaceHelpers.<FindExplicitlyImplementedMember>g__checkAccessorIsAccessibleIfImplementable|9_0(eventSymbol.AddMethod, ref CS$<>8__locals1, ref CS$<>8__locals2);
						ExplicitInterfaceHelpers.<FindExplicitlyImplementedMember>g__checkAccessorIsAccessibleIfImplementable|9_0(eventSymbol.RemoveMethod, ref CS$<>8__locals1, ref CS$<>8__locals2);
					}
				}
				CS$<>8__locals1.diagnostics.Add(CS$<>8__locals1.memberLocation, CS$<>8__locals2.useSiteInfo);
			}
			return symbol;
		}

		// Token: 0x06006DBF RID: 28095 RVA: 0x00193EDC File Offset: 0x001920DC
		internal static void FindExplicitlyImplementedMemberVerification(this Symbol implementingMember, Symbol implementedMember, BindingDiagnosticBag diagnostics)
		{
			if (implementedMember == null)
			{
				return;
			}
			if (implementingMember.ContainsTupleNames() && MemberSignatureComparer.ConsideringTupleNamesCreatesDifference(implementingMember, implementedMember))
			{
				Location location = implementingMember.Locations[0];
				diagnostics.Add(ErrorCode.ERR_ImplBadTupleNames, location, new object[] { implementingMember, implementedMember });
			}
			ExplicitInterfaceHelpers.FindExplicitImplementationCollisions(implementingMember, implementedMember, diagnostics);
		}

		// Token: 0x06006DC0 RID: 28096 RVA: 0x00193F34 File Offset: 0x00192134
		private static void FindExplicitImplementationCollisions(Symbol implementingMember, Symbol implementedMember, BindingDiagnosticBag diagnostics)
		{
			if (implementedMember == null)
			{
				return;
			}
			NamedTypeSymbol containingType = implementedMember.ContainingType;
			bool isDefinition = containingType.IsDefinition;
			foreach (Symbol symbol in containingType.GetMembers(implementedMember.Name))
			{
				if (symbol.Kind == implementingMember.Kind && implementedMember != symbol)
				{
					if (!isDefinition && MemberSignatureComparer.RuntimeSignatureComparer.Equals(implementedMember, symbol))
					{
						bool flag = false;
						ImmutableArray<ParameterSymbol> parameters = implementedMember.GetParameters();
						ImmutableArray<ParameterSymbol> parameters2 = symbol.GetParameters();
						int length = parameters.Length;
						for (int i = 0; i < length; i++)
						{
							if (parameters[i].RefKind != parameters2[i].RefKind)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							diagnostics.Add(ErrorCode.ERR_ExplicitImplCollisionOnRefOut, containingType.Locations[0], new object[] { containingType, implementedMember });
							return;
						}
						diagnostics.Add(ErrorCode.WRN_ExplicitImplCollision, implementingMember.Locations[0], new object[] { implementingMember });
						return;
					}
					else if (MemberSignatureComparer.ExplicitImplementationComparer.Equals(implementedMember, symbol))
					{
						diagnostics.Add(ErrorCode.WRN_ExplicitImplCollision, implementingMember.Locations[0], new object[] { implementingMember });
					}
				}
			}
		}

		// Token: 0x06006DC1 RID: 28097 RVA: 0x00194098 File Offset: 0x00192298
		[CompilerGenerated]
		internal static void <FindExplicitlyImplementedMember>g__checkAccessorIsAccessibleIfImplementable|9_0(MethodSymbol accessor, ref ExplicitInterfaceHelpers.<>c__DisplayClass9_0 A_1, ref ExplicitInterfaceHelpers.<>c__DisplayClass9_1 A_2)
		{
			if (accessor.IsImplementable() && !AccessCheck.IsSymbolAccessible(accessor, A_1.implementingMember.ContainingType, ref A_2.useSiteInfo, null))
			{
				A_1.diagnostics.Add(ErrorCode.ERR_BadAccess, A_1.memberLocation, new object[] { accessor });
			}
		}
	}
}
