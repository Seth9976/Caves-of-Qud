using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004EF RID: 1263
	internal static class SymbolExtensions
	{
		// Token: 0x06006ACA RID: 27338 RVA: 0x0018D0F4 File Offset: 0x0018B2F4
		internal static bool HasParamsParameter(this Symbol member)
		{
			ImmutableArray<ParameterSymbol> parameters = member.GetParameters();
			return !parameters.IsEmpty && parameters.Last<ParameterSymbol>().IsParams;
		}

		// Token: 0x06006ACB RID: 27339 RVA: 0x0018D120 File Offset: 0x0018B320
		internal static ImmutableArray<ParameterSymbol> GetParameters(this Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)member).Parameters;
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(member.Kind);
			}
			return ((PropertySymbol)member).Parameters;
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x0018D174 File Offset: 0x0018B374
		internal static ImmutableArray<TypeWithAnnotations> GetParameterTypes(this Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)member).ParameterTypesWithAnnotations;
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(member.Kind);
			}
			return ((PropertySymbol)member).ParameterTypesWithAnnotations;
		}

		// Token: 0x06006ACD RID: 27341 RVA: 0x0018D1C8 File Offset: 0x0018B3C8
		internal static bool GetIsVararg(this Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind != SymbolKind.Event)
			{
				if (kind == SymbolKind.Method)
				{
					return ((MethodSymbol)member).IsVararg;
				}
				if (kind != SymbolKind.Property)
				{
					throw ExceptionUtilities.UnexpectedValue(member.Kind);
				}
			}
			return false;
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x0018D20C File Offset: 0x0018B40C
		internal static ImmutableArray<RefKind> GetParameterRefKinds(this Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return default(ImmutableArray<RefKind>);
			}
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)member).ParameterRefKinds;
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(member.Kind);
			}
			return ((PropertySymbol)member).ParameterRefKinds;
		}

		// Token: 0x06006ACF RID: 27343 RVA: 0x0018D264 File Offset: 0x0018B464
		internal static int GetParameterCount(this Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind - SymbolKind.Event <= 1)
			{
				return 0;
			}
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)member).ParameterCount;
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(member.Kind);
			}
			return ((PropertySymbol)member).ParameterCount;
		}

		// Token: 0x06006AD0 RID: 27344 RVA: 0x0018D2B4 File Offset: 0x0018B4B4
		internal static bool HasUnsafeParameter(this Symbol member)
		{
			foreach (TypeWithAnnotations typeWithAnnotations in member.GetParameterTypes())
			{
				if (typeWithAnnotations.Type.IsUnsafe())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x0018D2F4 File Offset: 0x0018B4F4
		public static bool IsEventOrPropertyWithImplementableNonPublicAccessor(this Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind == SymbolKind.Event)
			{
				EventSymbol eventSymbol = (EventSymbol)symbol;
				return SymbolExtensions.<IsEventOrPropertyWithImplementableNonPublicAccessor>g__isImplementableAndNotPublic|7_0(eventSymbol.AddMethod) || SymbolExtensions.<IsEventOrPropertyWithImplementableNonPublicAccessor>g__isImplementableAndNotPublic|7_0(eventSymbol.RemoveMethod);
			}
			if (kind == SymbolKind.Property)
			{
				PropertySymbol propertySymbol = (PropertySymbol)symbol;
				return SymbolExtensions.<IsEventOrPropertyWithImplementableNonPublicAccessor>g__isImplementableAndNotPublic|7_0(propertySymbol.GetMethod) || SymbolExtensions.<IsEventOrPropertyWithImplementableNonPublicAccessor>g__isImplementableAndNotPublic|7_0(propertySymbol.SetMethod);
			}
			return false;
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x0018D356 File Offset: 0x0018B556
		public static bool IsImplementable(this MethodSymbol methodOpt)
		{
			return methodOpt != null && !methodOpt.IsSealed && (methodOpt.IsAbstract || methodOpt.IsVirtual);
		}

		// Token: 0x06006AD3 RID: 27347 RVA: 0x0018D375 File Offset: 0x0018B575
		public static bool IsAccessor(this MethodSymbol methodSymbol)
		{
			return methodSymbol.AssociatedSymbol != null;
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x0018D380 File Offset: 0x0018B580
		public static bool IsAccessor(this Symbol symbol)
		{
			return symbol.Kind == SymbolKind.Method && ((MethodSymbol)symbol).IsAccessor();
		}

		// Token: 0x06006AD5 RID: 27349 RVA: 0x0018D39C File Offset: 0x0018B59C
		public static bool IsIndexedPropertyAccessor(this MethodSymbol methodSymbol)
		{
			Symbol associatedSymbol = methodSymbol.AssociatedSymbol;
			return associatedSymbol != null && associatedSymbol.IsIndexedProperty();
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x0018D3BB File Offset: 0x0018B5BB
		public static bool IsOperator(this MethodSymbol methodSymbol)
		{
			return methodSymbol.MethodKind == MethodKind.UserDefinedOperator || methodSymbol.MethodKind == MethodKind.Conversion;
		}

		// Token: 0x06006AD7 RID: 27351 RVA: 0x0018D3D2 File Offset: 0x0018B5D2
		public static bool IsOperator(this Symbol symbol)
		{
			return symbol.Kind == SymbolKind.Method && ((MethodSymbol)symbol).IsOperator();
		}

		// Token: 0x06006AD8 RID: 27352 RVA: 0x0018D3EB File Offset: 0x0018B5EB
		public static bool IsIndexer(this Symbol symbol)
		{
			return symbol.Kind == SymbolKind.Property && ((PropertySymbol)symbol).IsIndexer;
		}

		// Token: 0x06006AD9 RID: 27353 RVA: 0x0018D404 File Offset: 0x0018B604
		public static bool IsIndexedProperty(this Symbol symbol)
		{
			return symbol.Kind == SymbolKind.Property && ((PropertySymbol)symbol).IsIndexedProperty;
		}

		// Token: 0x06006ADA RID: 27354 RVA: 0x0018D41D File Offset: 0x0018B61D
		public static bool IsUserDefinedConversion(this Symbol symbol)
		{
			return symbol.Kind == SymbolKind.Method && ((MethodSymbol)symbol).MethodKind == MethodKind.Conversion;
		}

		// Token: 0x06006ADB RID: 27355 RVA: 0x0018D43C File Offset: 0x0018B63C
		public static int CustomModifierCount(this MethodSymbol method)
		{
			int num = 0;
			TypeWithAnnotations returnTypeWithAnnotations = method.ReturnTypeWithAnnotations;
			num += returnTypeWithAnnotations.CustomModifiers.Length + method.RefCustomModifiers.Length;
			num += returnTypeWithAnnotations.Type.CustomModifierCount();
			foreach (ParameterSymbol parameterSymbol in method.Parameters)
			{
				TypeWithAnnotations typeWithAnnotations = parameterSymbol.TypeWithAnnotations;
				num += typeWithAnnotations.CustomModifiers.Length + parameterSymbol.RefCustomModifiers.Length;
				num += typeWithAnnotations.Type.CustomModifierCount();
			}
			return num;
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x0018D4E4 File Offset: 0x0018B6E4
		public static int CustomModifierCount(this Symbol m)
		{
			SymbolKind kind = m.Kind;
			switch (kind)
			{
			case SymbolKind.ArrayType:
			case SymbolKind.ErrorType:
				break;
			case SymbolKind.Assembly:
			case SymbolKind.DynamicType:
				goto IL_0085;
			case SymbolKind.Event:
				return ((EventSymbol)m).CustomModifierCount();
			default:
				switch (kind)
				{
				case SymbolKind.Method:
					return ((MethodSymbol)m).CustomModifierCount();
				case SymbolKind.NetModule:
				case SymbolKind.Namespace:
				case SymbolKind.Parameter:
				case SymbolKind.RangeVariable:
					goto IL_0085;
				case SymbolKind.NamedType:
				case SymbolKind.PointerType:
				case SymbolKind.TypeParameter:
					break;
				case SymbolKind.Property:
					return ((PropertySymbol)m).CustomModifierCount();
				default:
					if (kind != SymbolKind.FunctionPointerType)
					{
						goto IL_0085;
					}
					break;
				}
				break;
			}
			return ((TypeSymbol)m).CustomModifierCount();
			IL_0085:
			throw ExceptionUtilities.UnexpectedValue(m.Kind);
		}

		// Token: 0x06006ADD RID: 27357 RVA: 0x0018D586 File Offset: 0x0018B786
		public static int CustomModifierCount(this EventSymbol e)
		{
			return e.Type.CustomModifierCount();
		}

		// Token: 0x06006ADE RID: 27358 RVA: 0x0018D594 File Offset: 0x0018B794
		public static int CustomModifierCount(this PropertySymbol property)
		{
			int num = 0;
			TypeWithAnnotations typeWithAnnotations = property.TypeWithAnnotations;
			num += typeWithAnnotations.CustomModifiers.Length + property.RefCustomModifiers.Length;
			num += typeWithAnnotations.Type.CustomModifierCount();
			foreach (ParameterSymbol parameterSymbol in property.Parameters)
			{
				TypeWithAnnotations typeWithAnnotations2 = parameterSymbol.TypeWithAnnotations;
				num += typeWithAnnotations2.CustomModifiers.Length + parameterSymbol.RefCustomModifiers.Length;
				num += typeWithAnnotations2.Type.CustomModifierCount();
			}
			return num;
		}

		// Token: 0x06006ADF RID: 27359 RVA: 0x0018D63C File Offset: 0x0018B83C
		internal static Symbol SymbolAsMember(this Symbol s, NamedTypeSymbol newOwner)
		{
			SymbolKind kind = s.Kind;
			switch (kind)
			{
			case SymbolKind.Event:
				return ((EventSymbol)s).AsMember(newOwner);
			case SymbolKind.Field:
				return ((FieldSymbol)s).AsMember(newOwner);
			case SymbolKind.Label:
			case SymbolKind.Local:
			case SymbolKind.NetModule:
				break;
			case SymbolKind.Method:
				return ((MethodSymbol)s).AsMember(newOwner);
			case SymbolKind.NamedType:
				return ((NamedTypeSymbol)s).AsMember(newOwner);
			default:
				if (kind == SymbolKind.Property)
				{
					return ((PropertySymbol)s).AsMember(newOwner);
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(s.Kind);
		}

		// Token: 0x06006AE0 RID: 27360 RVA: 0x0018D6CC File Offset: 0x0018B8CC
		internal static int GetMemberArity(this Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind != SymbolKind.ErrorType)
			{
				if (kind == SymbolKind.Method)
				{
					return ((MethodSymbol)symbol).Arity;
				}
				if (kind != SymbolKind.NamedType)
				{
					return 0;
				}
			}
			return ((NamedTypeSymbol)symbol).Arity;
		}

		// Token: 0x06006AE1 RID: 27361 RVA: 0x0018D70C File Offset: 0x0018B90C
		internal static NamespaceOrTypeSymbol OfMinimalArity(this IEnumerable<NamespaceOrTypeSymbol> symbols)
		{
			NamespaceOrTypeSymbol namespaceOrTypeSymbol = null;
			int num = int.MaxValue;
			foreach (NamespaceOrTypeSymbol namespaceOrTypeSymbol2 in symbols)
			{
				int memberArity = namespaceOrTypeSymbol2.GetMemberArity();
				if (memberArity < num)
				{
					num = memberArity;
					namespaceOrTypeSymbol = namespaceOrTypeSymbol2;
				}
			}
			return namespaceOrTypeSymbol;
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x0018D768 File Offset: 0x0018B968
		internal static ImmutableArray<TypeParameterSymbol> GetMemberTypeParameters(this Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			switch (kind)
			{
			case SymbolKind.ErrorType:
			case SymbolKind.NamedType:
				return ((NamedTypeSymbol)symbol).TypeParameters;
			case SymbolKind.Event:
			case SymbolKind.Field:
				break;
			case SymbolKind.Label:
			case SymbolKind.Local:
			case SymbolKind.NetModule:
				goto IL_0054;
			case SymbolKind.Method:
				return ((MethodSymbol)symbol).TypeParameters;
			default:
				if (kind != SymbolKind.Property)
				{
					goto IL_0054;
				}
				break;
			}
			return ImmutableArray<TypeParameterSymbol>.Empty;
			IL_0054:
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x0018D7DC File Offset: 0x0018B9DC
		internal static ImmutableArray<TypeSymbol> GetMemberTypeArgumentsNoUseSiteDiagnostics(this Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			switch (kind)
			{
			case SymbolKind.ErrorType:
			case SymbolKind.NamedType:
				return ((NamedTypeSymbol)symbol).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.SelectAsArray<TypeWithAnnotations, TypeSymbol>(TypeMap.AsTypeSymbol);
			case SymbolKind.Event:
			case SymbolKind.Field:
				break;
			case SymbolKind.Label:
			case SymbolKind.Local:
			case SymbolKind.NetModule:
				goto IL_0068;
			case SymbolKind.Method:
				return ((MethodSymbol)symbol).TypeArgumentsWithAnnotations.SelectAsArray<TypeWithAnnotations, TypeSymbol>(TypeMap.AsTypeSymbol);
			default:
				if (kind != SymbolKind.Property)
				{
					goto IL_0068;
				}
				break;
			}
			return ImmutableArray<TypeSymbol>.Empty;
			IL_0068:
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x0018D864 File Offset: 0x0018BA64
		internal static bool IsConstructor(this MethodSymbol method)
		{
			MethodKind methodKind = method.MethodKind;
			return methodKind == MethodKind.Constructor || methodKind == MethodKind.StaticConstructor;
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x0018D884 File Offset: 0x0018BA84
		internal static bool HasThisConstructorInitializer(this MethodSymbol method)
		{
			if (method != null && method.MethodKind == MethodKind.Constructor)
			{
				SourceMemberMethodSymbol sourceMemberMethodSymbol = method as SourceMemberMethodSymbol;
				if (sourceMemberMethodSymbol != null)
				{
					ConstructorDeclarationSyntax constructorDeclarationSyntax = sourceMemberMethodSymbol.SyntaxNode as ConstructorDeclarationSyntax;
					if (constructorDeclarationSyntax != null)
					{
						ConstructorInitializerSyntax initializer = constructorDeclarationSyntax.Initializer;
						if (initializer != null)
						{
							return initializer.Kind() == SyntaxKind.ThisConstructorInitializer;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x0018D8CF File Offset: 0x0018BACF
		internal static bool IncludeFieldInitializersInBody(this MethodSymbol methodSymbol)
		{
			return methodSymbol.IsConstructor() && !methodSymbol.HasThisConstructorInitializer() && !(methodSymbol is SynthesizedRecordCopyCtor) && !Binder.IsUserDefinedRecordCopyConstructor(methodSymbol);
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x0018D8F4 File Offset: 0x0018BAF4
		internal static bool IsParameterlessConstructor(this MethodSymbol method)
		{
			return method.MethodKind == MethodKind.Constructor && method.ParameterCount == 0;
		}

		// Token: 0x06006AE8 RID: 27368 RVA: 0x0018D90A File Offset: 0x0018BB0A
		internal static bool IsDefaultValueTypeConstructor(this MethodSymbol method)
		{
			return method.IsImplicitlyDeclared && method.ContainingType.IsValueType && method.IsParameterlessConstructor();
		}

		// Token: 0x06006AE9 RID: 27369 RVA: 0x0018D92C File Offset: 0x0018BB2C
		internal static bool ShouldEmit(this MethodSymbol method)
		{
			if (method.IsDefaultValueTypeConstructor())
			{
				return false;
			}
			SynthesizedStaticConstructor synthesizedStaticConstructor = method as SynthesizedStaticConstructor;
			return (synthesizedStaticConstructor == null || synthesizedStaticConstructor.ShouldEmit(default(ImmutableArray<BoundInitializer>))) && (!method.IsPartialMethod() || method.PartialImplementationPart != null);
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x0018D974 File Offset: 0x0018BB74
		internal static MethodSymbol GetOwnOrInheritedAddMethod(this EventSymbol @event)
		{
			while (@event != null)
			{
				MethodSymbol addMethod = @event.AddMethod;
				if (addMethod != null)
				{
					return addMethod;
				}
				@event = (@event.IsOverride ? @event.OverriddenEvent : null);
			}
			return null;
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x0018D9A8 File Offset: 0x0018BBA8
		internal static MethodSymbol GetOwnOrInheritedRemoveMethod(this EventSymbol @event)
		{
			while (@event != null)
			{
				MethodSymbol removeMethod = @event.RemoveMethod;
				if (removeMethod != null)
				{
					return removeMethod;
				}
				@event = (@event.IsOverride ? @event.OverriddenEvent : null);
			}
			return null;
		}

		// Token: 0x06006AEC RID: 27372 RVA: 0x0018D9DC File Offset: 0x0018BBDC
		internal static bool IsExplicitInterfaceImplementation(this Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return ((EventSymbol)member).IsExplicitInterfaceImplementation;
			}
			if (kind != SymbolKind.Method)
			{
				return kind == SymbolKind.Property && ((PropertySymbol)member).IsExplicitInterfaceImplementation;
			}
			return ((MethodSymbol)member).IsExplicitInterfaceImplementation;
		}

		// Token: 0x06006AED RID: 27373 RVA: 0x0018DA25 File Offset: 0x0018BC25
		internal static bool IsPartialMethod(this Symbol member)
		{
			SourceMemberMethodSymbol sourceMemberMethodSymbol = member as SourceMemberMethodSymbol;
			return sourceMemberMethodSymbol != null && sourceMemberMethodSymbol.IsPartial;
		}

		// Token: 0x06006AEE RID: 27374 RVA: 0x0018DA38 File Offset: 0x0018BC38
		internal static bool IsPartialImplementation(this Symbol member)
		{
			SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = member as SourceOrdinaryMethodSymbol;
			return sourceOrdinaryMethodSymbol != null && sourceOrdinaryMethodSymbol.IsPartialImplementation;
		}

		// Token: 0x06006AEF RID: 27375 RVA: 0x0018DA4B File Offset: 0x0018BC4B
		internal static bool IsPartialDefinition(this Symbol member)
		{
			SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = member as SourceOrdinaryMethodSymbol;
			return sourceOrdinaryMethodSymbol != null && sourceOrdinaryMethodSymbol.IsPartialDefinition;
		}

		// Token: 0x06006AF0 RID: 27376 RVA: 0x0018DA60 File Offset: 0x0018BC60
		internal static bool ContainsTupleNames(this Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return ((EventSymbol)member).Type.ContainsTupleNames();
			}
			if (kind != SymbolKind.Method)
			{
				if (kind != SymbolKind.Property)
				{
					throw ExceptionUtilities.UnexpectedValue(member.Kind);
				}
				return ((PropertySymbol)member).Type.ContainsTupleNames();
			}
			else
			{
				MethodSymbol methodSymbol = (MethodSymbol)member;
				if (!methodSymbol.ReturnType.ContainsTupleNames())
				{
					return methodSymbol.Parameters.Any<ParameterSymbol>((ParameterSymbol p) => p.Type.ContainsTupleNames());
				}
				return true;
			}
		}

		// Token: 0x06006AF1 RID: 27377 RVA: 0x0018DAF8 File Offset: 0x0018BCF8
		internal static ImmutableArray<Symbol> GetExplicitInterfaceImplementations(this Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return ((EventSymbol)member).ExplicitInterfaceImplementations.Cast<EventSymbol, Symbol>();
			}
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)member).ExplicitInterfaceImplementations.Cast<MethodSymbol, Symbol>();
			}
			if (kind != SymbolKind.Property)
			{
				return ImmutableArray<Symbol>.Empty;
			}
			return ((PropertySymbol)member).ExplicitInterfaceImplementations.Cast<PropertySymbol, Symbol>();
		}

		// Token: 0x06006AF2 RID: 27378 RVA: 0x0018DB54 File Offset: 0x0018BD54
		internal static Symbol GetOverriddenMember(this Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return ((EventSymbol)member).OverriddenEvent;
			}
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)member).OverriddenMethod;
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(member.Kind);
			}
			return ((PropertySymbol)member).OverriddenProperty;
		}

		// Token: 0x06006AF3 RID: 27379 RVA: 0x0018DBAC File Offset: 0x0018BDAC
		internal static Symbol GetLeastOverriddenMember(this Symbol member, NamedTypeSymbol accessingTypeOpt)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				return ((EventSymbol)member).GetLeastOverriddenEvent(accessingTypeOpt);
			}
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)member).GetConstructedLeastOverriddenMethod(accessingTypeOpt, false);
			}
			if (kind != SymbolKind.Property)
			{
				return member;
			}
			return ((PropertySymbol)member).GetLeastOverriddenProperty(accessingTypeOpt);
		}

		// Token: 0x06006AF4 RID: 27380 RVA: 0x0018DBFC File Offset: 0x0018BDFC
		internal static bool IsFieldOrFieldLikeEvent(this Symbol member, out FieldSymbol field)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Event)
			{
				field = ((EventSymbol)member).AssociatedField;
				return field != null;
			}
			if (kind == SymbolKind.Field)
			{
				field = (FieldSymbol)member;
				return true;
			}
			field = null;
			return false;
		}

		// Token: 0x06006AF5 RID: 27381 RVA: 0x0018DC3C File Offset: 0x0018BE3C
		internal static string GetMemberCallerName(this Symbol member)
		{
			if (member.Kind == SymbolKind.Method)
			{
				member = ((MethodSymbol)member).AssociatedSymbol ?? member;
			}
			if (member.IsIndexer())
			{
				return member.MetadataName;
			}
			if (!member.IsExplicitInterfaceImplementation())
			{
				return member.Name;
			}
			return ExplicitInterfaceHelpers.GetMemberNameWithoutInterfaceName(member.Name);
		}

		// Token: 0x06006AF6 RID: 27382 RVA: 0x0018DC90 File Offset: 0x0018BE90
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool IsCompilationOutputWinMdObj(this Symbol symbol)
		{
			CSharpCompilation declaringCompilation = symbol.DeclaringCompilation;
			return declaringCompilation != null && declaringCompilation.Options.OutputKind == OutputKind.WindowsRuntimeMetadata;
		}

		// Token: 0x06006AF7 RID: 27383 RVA: 0x0018DCB8 File Offset: 0x0018BEB8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static NamedTypeSymbol ConstructIfGeneric(this NamedTypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> typeArguments)
		{
			if (!type.TypeParameters.IsEmpty)
			{
				return type.Construct(typeArguments, false);
			}
			return type;
		}

		// Token: 0x06006AF8 RID: 27384 RVA: 0x0018DCDF File Offset: 0x0018BEDF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool IsNestedType([NotNullWhen(true)] this Symbol symbol)
		{
			return symbol is NamedTypeSymbol && symbol.ContainingType != null;
		}

		// Token: 0x06006AF9 RID: 27385 RVA: 0x0018DCF4 File Offset: 0x0018BEF4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool IsAccessibleViaInheritance(this NamedTypeSymbol superType, NamedTypeSymbol subType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol originalDefinition = superType.OriginalDefinition;
			for (NamedTypeSymbol namedTypeSymbol = subType; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				if (namedTypeSymbol.OriginalDefinition == originalDefinition)
				{
					return true;
				}
			}
			if (originalDefinition.IsInterface)
			{
				ImmutableArray<NamedTypeSymbol>.Enumerator enumerator = subType.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo).GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.OriginalDefinition == originalDefinition)
					{
						return true;
					}
				}
			}
			return superType.TypeKind == TypeKind.Submission && subType.TypeKind == TypeKind.Submission;
		}

		// Token: 0x06006AFA RID: 27386 RVA: 0x0018DD6C File Offset: 0x0018BF6C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool IsNoMoreVisibleThan(this Symbol symbol, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return type.IsAtLeastAsVisibleAs(symbol, ref useSiteInfo);
		}

		// Token: 0x06006AFB RID: 27387 RVA: 0x0018DD76 File Offset: 0x0018BF76
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool IsNoMoreVisibleThan(this Symbol symbol, TypeWithAnnotations type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return type.IsAtLeastAsVisibleAs(symbol, ref useSiteInfo);
		}

		// Token: 0x06006AFC RID: 27388 RVA: 0x0018DD84 File Offset: 0x0018BF84
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static void AddUseSiteInfo(this Symbol symbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool addDiagnostics = true)
		{
			if (symbol == null)
			{
				return;
			}
			if (!useSiteInfo.AccumulatesDiagnostics)
			{
				return;
			}
			UseSiteInfo<AssemblySymbol> useSiteInfo2 = symbol.GetUseSiteInfo();
			if (addDiagnostics)
			{
				useSiteInfo.AddDiagnostics(useSiteInfo2);
			}
			useSiteInfo.AddDependencies(useSiteInfo2);
		}

		// Token: 0x06006AFD RID: 27389 RVA: 0x0018DDB6 File Offset: 0x0018BFB6
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static LocalizableErrorArgument GetKindText(this Symbol symbol)
		{
			return symbol.Kind.Localize();
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x0018DDC4 File Offset: 0x0018BFC4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static NamespaceOrTypeSymbol ContainingNamespaceOrType(this Symbol symbol)
		{
			Symbol containingSymbol = symbol.ContainingSymbol;
			if (containingSymbol != null)
			{
				SymbolKind kind = containingSymbol.Kind;
				if (kind == SymbolKind.ErrorType || kind - SymbolKind.NamedType <= 1)
				{
					return (NamespaceOrTypeSymbol)containingSymbol;
				}
			}
			return null;
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x0018DDF8 File Offset: 0x0018BFF8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static Symbol ContainingNonLambdaMember(this Symbol containingMember)
		{
			while (containingMember != null && containingMember.Kind == SymbolKind.Method)
			{
				MethodSymbol methodSymbol = (MethodSymbol)containingMember;
				if (methodSymbol.MethodKind != MethodKind.AnonymousFunction && methodSymbol.MethodKind != MethodKind.LocalFunction)
				{
					break;
				}
				containingMember = containingMember.ContainingSymbol;
			}
			return containingMember;
		}

		// Token: 0x06006B00 RID: 27392 RVA: 0x0018DE38 File Offset: 0x0018C038
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static ParameterSymbol EnclosingThisSymbol(this Symbol containingMember)
		{
			Symbol symbol = containingMember;
			SymbolKind kind;
			MethodSymbol methodSymbol;
			for (;;)
			{
				kind = symbol.Kind;
				if (kind == SymbolKind.Field)
				{
					goto IL_0042;
				}
				if (kind != SymbolKind.Method)
				{
					break;
				}
				methodSymbol = (MethodSymbol)symbol;
				if (methodSymbol.MethodKind != MethodKind.AnonymousFunction && methodSymbol.MethodKind != MethodKind.LocalFunction)
				{
					goto IL_003B;
				}
				symbol = methodSymbol.ContainingSymbol;
			}
			if (kind != SymbolKind.NamedType)
			{
				return null;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)symbol;
			goto IL_0056;
			IL_003B:
			return methodSymbol.ThisParameter;
			IL_0042:
			namedTypeSymbol = symbol.ContainingType;
			IL_0056:
			if (!namedTypeSymbol.IsScriptClass)
			{
				return null;
			}
			return namedTypeSymbol.InstanceConstructors.Single<MethodSymbol>().ThisParameter;
		}

		// Token: 0x06006B01 RID: 27393 RVA: 0x0018DEB8 File Offset: 0x0018C0B8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static Symbol ConstructedFrom(this Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)symbol).ConstructedFrom;
			}
			if (kind == SymbolKind.NamedType)
			{
				return ((NamedTypeSymbol)symbol).ConstructedFrom;
			}
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
		}

		// Token: 0x06006B02 RID: 27394 RVA: 0x0018DF00 File Offset: 0x0018C100
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool IsSourceParameterWithEnumeratorCancellationAttribute(this ParameterSymbol parameter)
		{
			SourceComplexParameterSymbol sourceComplexParameterSymbol = parameter as SourceComplexParameterSymbol;
			if (sourceComplexParameterSymbol == null)
			{
				SynthesizedComplexParameterSymbol synthesizedComplexParameterSymbol = parameter as SynthesizedComplexParameterSymbol;
				return synthesizedComplexParameterSymbol != null && synthesizedComplexParameterSymbol.HasEnumeratorCancellationAttribute;
			}
			return sourceComplexParameterSymbol.HasEnumeratorCancellationAttribute;
		}

		// Token: 0x06006B03 RID: 27395 RVA: 0x0018DF32 File Offset: 0x0018C132
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool IsContainingSymbolOfAllTypeParameters(this Symbol containingSymbol, TypeSymbol type)
		{
			return type.VisitType(SymbolExtensions.s_hasInvalidTypeParameterFunc, containingSymbol, false) == null;
		}

		// Token: 0x06006B04 RID: 27396 RVA: 0x0018DF44 File Offset: 0x0018C144
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool IsContainingSymbolOfAllTypeParameters(this Symbol containingSymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<TypeSymbol> types)
		{
			return types.All<TypeSymbol>(new Func<TypeSymbol, bool>(containingSymbol.IsContainingSymbolOfAllTypeParameters));
		}

		// Token: 0x06006B05 RID: 27397 RVA: 0x0018DF58 File Offset: 0x0018C158
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool HasInvalidTypeParameter(TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol containingSymbol)
		{
			if (type.TypeKind == TypeKind.TypeParameter)
			{
				Symbol containingSymbol2 = type.ContainingSymbol;
				while (containingSymbol != null && containingSymbol.Kind != SymbolKind.Namespace)
				{
					if (containingSymbol == containingSymbol2)
					{
						return false;
					}
					containingSymbol = containingSymbol.ContainingSymbol;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06006B06 RID: 27398 RVA: 0x0018DF9C File Offset: 0x0018C19C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool IsTypeOrTypeAlias(this Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind <= SymbolKind.NamedType)
			{
				switch (kind)
				{
				case SymbolKind.Alias:
					return ((AliasSymbol)symbol).Target.IsTypeOrTypeAlias();
				case SymbolKind.ArrayType:
				case SymbolKind.DynamicType:
				case SymbolKind.ErrorType:
					break;
				case SymbolKind.Assembly:
					return false;
				default:
					if (kind != SymbolKind.NamedType)
					{
						return false;
					}
					break;
				}
			}
			else if (kind != SymbolKind.PointerType && kind != SymbolKind.TypeParameter && kind != SymbolKind.FunctionPointerType)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x0018DFF9 File Offset: 0x0018C1F9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static bool CompilationAllowsUnsafe(this Symbol symbol)
		{
			return symbol.DeclaringCompilation.Options.AllowUnsafe;
		}

		// Token: 0x06006B08 RID: 27400 RVA: 0x0018E00C File Offset: 0x0018C20C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static void CheckUnsafeModifier(this Symbol symbol, DeclarationModifiers modifiers, BindingDiagnosticBag diagnostics)
		{
			symbol.CheckUnsafeModifier(modifiers, symbol.Locations[0], diagnostics);
		}

		// Token: 0x06006B09 RID: 27401 RVA: 0x0018E030 File Offset: 0x0018C230
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static void CheckUnsafeModifier(this Symbol symbol, DeclarationModifiers modifiers, Location errorLocation, BindingDiagnosticBag diagnostics)
		{
			if ((modifiers & DeclarationModifiers.Unsafe) == DeclarationModifiers.Unsafe && !symbol.CompilationAllowsUnsafe())
			{
				diagnostics.Add(ErrorCode.ERR_IllegalUnsafe, errorLocation);
			}
		}

		// Token: 0x06006B0A RID: 27402 RVA: 0x0018E058 File Offset: 0x0018C258
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool IsHiddenByCodeAnalysisEmbeddedAttribute(this Symbol symbol)
		{
			NamedTypeSymbol namedTypeSymbol = ((symbol.Kind == SymbolKind.NamedType) ? ((NamedTypeSymbol)symbol) : symbol.ContainingType);
			if (namedTypeSymbol == null)
			{
				return false;
			}
			while (namedTypeSymbol.ContainingType != null)
			{
				namedTypeSymbol = namedTypeSymbol.ContainingType;
			}
			return namedTypeSymbol.HasCodeAnalysisEmbeddedAttribute;
		}

		// Token: 0x06006B0B RID: 27403 RVA: 0x0018E098 File Offset: 0x0018C298
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool MustCallMethodsDirectly(this Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind != SymbolKind.Event)
			{
				return kind == SymbolKind.Property && ((PropertySymbol)symbol).MustCallMethodsDirectly;
			}
			return ((EventSymbol)symbol).MustCallMethodsDirectly;
		}

		// Token: 0x06006B0C RID: 27404 RVA: 0x0018E0D0 File Offset: 0x0018C2D0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static int GetArity(this Symbol symbol)
		{
			if (symbol != null)
			{
				SymbolKind kind = symbol.Kind;
				if (kind == SymbolKind.Method)
				{
					return ((MethodSymbol)symbol).Arity;
				}
				if (kind == SymbolKind.NamedType)
				{
					return ((NamedTypeSymbol)symbol).Arity;
				}
			}
			return 0;
		}

		// Token: 0x06006B0D RID: 27405 RVA: 0x0018E10C File Offset: 0x0018C30C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static CSharpSyntaxNode GetNonNullSyntaxNode([global::System.Runtime.CompilerServices.Nullable(2)] this Symbol symbol)
		{
			if (symbol != null)
			{
				SyntaxReference syntaxReference = symbol.DeclaringSyntaxReferences.FirstOrDefault<SyntaxReference>();
				if (syntaxReference == null && symbol.IsImplicitlyDeclared)
				{
					Symbol containingSymbol = symbol.ContainingSymbol;
					if (containingSymbol != null)
					{
						syntaxReference = containingSymbol.DeclaringSyntaxReferences.FirstOrDefault<SyntaxReference>();
					}
				}
				if (syntaxReference != null)
				{
					return (CSharpSyntaxNode)syntaxReference.GetSyntax(default(CancellationToken));
				}
			}
			return (CSharpSyntaxNode)CSharpSyntaxTree.Dummy.GetRoot(default(CancellationToken));
		}

		// Token: 0x06006B0E RID: 27406 RVA: 0x0018E17C File Offset: 0x0018C37C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static Symbol EnsureCSharpSymbolOrNull(this ISymbol symbol, [global::System.Runtime.CompilerServices.Nullable(1)] string paramName)
		{
			Symbol symbol2 = symbol as Symbol;
			if (symbol2 != null)
			{
				return symbol2.UnderlyingSymbol;
			}
			if (symbol != null)
			{
				throw new ArgumentException(CSharpResources.NotACSharpSymbol, paramName);
			}
			return null;
		}

		// Token: 0x06006B0F RID: 27407 RVA: 0x0018E1AA File Offset: 0x0018C3AA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static AssemblySymbol EnsureCSharpSymbolOrNull(this IAssemblySymbol symbol, [global::System.Runtime.CompilerServices.Nullable(1)] string paramName)
		{
			return (AssemblySymbol)symbol.EnsureCSharpSymbolOrNull(paramName);
		}

		// Token: 0x06006B10 RID: 27408 RVA: 0x0018E1B8 File Offset: 0x0018C3B8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static NamespaceOrTypeSymbol EnsureCSharpSymbolOrNull(this INamespaceOrTypeSymbol symbol, [global::System.Runtime.CompilerServices.Nullable(1)] string paramName)
		{
			return (NamespaceOrTypeSymbol)symbol.EnsureCSharpSymbolOrNull(paramName);
		}

		// Token: 0x06006B11 RID: 27409 RVA: 0x0018E1C6 File Offset: 0x0018C3C6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static NamespaceSymbol EnsureCSharpSymbolOrNull(this INamespaceSymbol symbol, [global::System.Runtime.CompilerServices.Nullable(1)] string paramName)
		{
			return (NamespaceSymbol)symbol.EnsureCSharpSymbolOrNull(paramName);
		}

		// Token: 0x06006B12 RID: 27410 RVA: 0x0018E1D4 File Offset: 0x0018C3D4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static TypeSymbol EnsureCSharpSymbolOrNull(this ITypeSymbol symbol, [global::System.Runtime.CompilerServices.Nullable(1)] string paramName)
		{
			return (TypeSymbol)symbol.EnsureCSharpSymbolOrNull(paramName);
		}

		// Token: 0x06006B13 RID: 27411 RVA: 0x0018E1E2 File Offset: 0x0018C3E2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static NamedTypeSymbol EnsureCSharpSymbolOrNull(this INamedTypeSymbol symbol, [global::System.Runtime.CompilerServices.Nullable(1)] string paramName)
		{
			return (NamedTypeSymbol)symbol.EnsureCSharpSymbolOrNull(paramName);
		}

		// Token: 0x06006B14 RID: 27412 RVA: 0x0018E1F0 File Offset: 0x0018C3F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static TypeParameterSymbol EnsureCSharpSymbolOrNull(this ITypeParameterSymbol symbol, [global::System.Runtime.CompilerServices.Nullable(1)] string paramName)
		{
			return (TypeParameterSymbol)symbol.EnsureCSharpSymbolOrNull(paramName);
		}

		// Token: 0x06006B15 RID: 27413 RVA: 0x0018E1FE File Offset: 0x0018C3FE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static EventSymbol EnsureCSharpSymbolOrNull(this IEventSymbol symbol, [global::System.Runtime.CompilerServices.Nullable(1)] string paramName)
		{
			return (EventSymbol)symbol.EnsureCSharpSymbolOrNull(paramName);
		}

		// Token: 0x06006B16 RID: 27414 RVA: 0x0018E20C File Offset: 0x0018C40C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static TypeWithAnnotations GetTypeOrReturnType(this Symbol symbol)
		{
			RefKind refKind;
			TypeWithAnnotations typeWithAnnotations;
			ImmutableArray<CustomModifier> immutableArray;
			symbol.GetTypeOrReturnType(out refKind, out typeWithAnnotations, out immutableArray);
			return typeWithAnnotations;
		}

		// Token: 0x06006B17 RID: 27415 RVA: 0x0018E228 File Offset: 0x0018C428
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static FlowAnalysisAnnotations GetFlowAnalysisAnnotations(this PropertySymbol property)
		{
			MethodSymbol ownOrInheritedGetMethod = property.GetOwnOrInheritedGetMethod();
			FlowAnalysisAnnotations flowAnalysisAnnotations = ((ownOrInheritedGetMethod != null) ? ownOrInheritedGetMethod.ReturnTypeFlowAnalysisAnnotations : FlowAnalysisAnnotations.None);
			MethodSymbol ownOrInheritedSetMethod = property.GetOwnOrInheritedSetMethod();
			FlowAnalysisAnnotations? flowAnalysisAnnotations2 = ((ownOrInheritedSetMethod != null) ? new FlowAnalysisAnnotations?(ownOrInheritedSetMethod.Parameters.Last<ParameterSymbol>().FlowAnalysisAnnotations) : default(FlowAnalysisAnnotations?));
			if (flowAnalysisAnnotations2 != null)
			{
				FlowAnalysisAnnotations valueOrDefault = flowAnalysisAnnotations2.GetValueOrDefault();
				flowAnalysisAnnotations |= valueOrDefault;
			}
			else
			{
				SourcePropertySymbolBase sourcePropertySymbolBase = property as SourcePropertySymbolBase;
				if (sourcePropertySymbolBase != null)
				{
					if (sourcePropertySymbolBase.HasAllowNull)
					{
						flowAnalysisAnnotations |= FlowAnalysisAnnotations.AllowNull;
					}
					if (sourcePropertySymbolBase.HasDisallowNull)
					{
						flowAnalysisAnnotations |= FlowAnalysisAnnotations.DisallowNull;
					}
				}
			}
			return flowAnalysisAnnotations;
		}

		// Token: 0x06006B18 RID: 27416 RVA: 0x0018E2B0 File Offset: 0x0018C4B0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static FlowAnalysisAnnotations GetFlowAnalysisAnnotations(this Symbol symbol)
		{
			MethodSymbol methodSymbol = symbol as MethodSymbol;
			FlowAnalysisAnnotations flowAnalysisAnnotations;
			if (methodSymbol == null)
			{
				PropertySymbol propertySymbol = symbol as PropertySymbol;
				if (propertySymbol == null)
				{
					ParameterSymbol parameterSymbol = symbol as ParameterSymbol;
					if (parameterSymbol == null)
					{
						FieldSymbol fieldSymbol = symbol as FieldSymbol;
						if (fieldSymbol == null)
						{
							flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
						}
						else
						{
							flowAnalysisAnnotations = fieldSymbol.FlowAnalysisAnnotations;
						}
					}
					else
					{
						flowAnalysisAnnotations = parameterSymbol.FlowAnalysisAnnotations;
					}
				}
				else
				{
					flowAnalysisAnnotations = propertySymbol.GetFlowAnalysisAnnotations();
				}
			}
			else
			{
				flowAnalysisAnnotations = methodSymbol.ReturnTypeFlowAnalysisAnnotations;
			}
			return flowAnalysisAnnotations;
		}

		// Token: 0x06006B19 RID: 27417 RVA: 0x0018E314 File Offset: 0x0018C514
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static void GetTypeOrReturnType(this Symbol symbol, out RefKind refKind, out TypeWithAnnotations returnType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableArray<CustomModifier> refCustomModifiers)
		{
			switch (symbol.Kind)
			{
			case SymbolKind.ErrorType:
				refKind = RefKind.None;
				returnType = TypeWithAnnotations.Create((TypeSymbol)symbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				refCustomModifiers = ImmutableArray<CustomModifier>.Empty;
				return;
			case SymbolKind.Event:
			{
				EventSymbol eventSymbol = (EventSymbol)symbol;
				refKind = RefKind.None;
				returnType = eventSymbol.TypeWithAnnotations;
				refCustomModifiers = ImmutableArray<CustomModifier>.Empty;
				return;
			}
			case SymbolKind.Field:
			{
				FieldSymbol fieldSymbol = (FieldSymbol)symbol;
				refKind = RefKind.None;
				returnType = fieldSymbol.TypeWithAnnotations;
				refCustomModifiers = ImmutableArray<CustomModifier>.Empty;
				return;
			}
			case SymbolKind.Local:
			{
				LocalSymbol localSymbol = (LocalSymbol)symbol;
				refKind = localSymbol.RefKind;
				returnType = localSymbol.TypeWithAnnotations;
				refCustomModifiers = ImmutableArray<CustomModifier>.Empty;
				return;
			}
			case SymbolKind.Method:
			{
				MethodSymbol methodSymbol = (MethodSymbol)symbol;
				refKind = methodSymbol.RefKind;
				returnType = methodSymbol.ReturnTypeWithAnnotations;
				refCustomModifiers = methodSymbol.RefCustomModifiers;
				return;
			}
			case SymbolKind.Parameter:
			{
				ParameterSymbol parameterSymbol = (ParameterSymbol)symbol;
				refKind = parameterSymbol.RefKind;
				returnType = parameterSymbol.TypeWithAnnotations;
				refCustomModifiers = parameterSymbol.RefCustomModifiers;
				return;
			}
			case SymbolKind.Property:
			{
				PropertySymbol propertySymbol = (PropertySymbol)symbol;
				refKind = propertySymbol.RefKind;
				returnType = propertySymbol.TypeWithAnnotations;
				refCustomModifiers = propertySymbol.RefCustomModifiers;
				return;
			}
			}
			throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
		}

		// Token: 0x06006B1A RID: 27418 RVA: 0x0018E48C File Offset: 0x0018C68C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static bool IsImplementableInterfaceMember(this Symbol symbol)
		{
			if (!symbol.IsStatic && !symbol.IsSealed && (symbol.IsAbstract || symbol.IsVirtual))
			{
				NamedTypeSymbol containingType = symbol.ContainingType;
				return containingType != null && containingType.IsInterface;
			}
			return false;
		}

		// Token: 0x06006B1B RID: 27419 RVA: 0x0018E4C4 File Offset: 0x0018C6C4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static bool RequiresInstanceReceiver(this Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			switch (kind)
			{
			case SymbolKind.Event:
				return ((EventSymbol)symbol).RequiresInstanceReceiver;
			case SymbolKind.Field:
				return ((FieldSymbol)symbol).RequiresInstanceReceiver;
			case SymbolKind.Label:
			case SymbolKind.Local:
				break;
			case SymbolKind.Method:
				return ((MethodSymbol)symbol).RequiresInstanceReceiver;
			default:
				if (kind == SymbolKind.Property)
				{
					return ((PropertySymbol)symbol).RequiresInstanceReceiver;
				}
				break;
			}
			throw new ArgumentException("only methods, properties, fields and events can take a receiver", "symbol");
		}

		// Token: 0x06006B1C RID: 27420 RVA: 0x0018E544 File Offset: 0x0018C744
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		[return: NotNullIfNotNull("symbol")]
		private static TISymbol GetPublicSymbol<TISymbol>([global::System.Runtime.CompilerServices.Nullable(2)] this Symbol symbol) where TISymbol : class, ISymbol
		{
			return (TISymbol)((object)((symbol != null) ? symbol.ISymbol : null));
		}

		// Token: 0x06006B1D RID: 27421 RVA: 0x0018E557 File Offset: 0x0018C757
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static ISymbol GetPublicSymbol(this Symbol symbol)
		{
			return symbol.GetPublicSymbol<ISymbol>();
		}

		// Token: 0x06006B1E RID: 27422 RVA: 0x0018E55F File Offset: 0x0018C75F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IMethodSymbol GetPublicSymbol(this MethodSymbol symbol)
		{
			return symbol.GetPublicSymbol<IMethodSymbol>();
		}

		// Token: 0x06006B1F RID: 27423 RVA: 0x0018E567 File Offset: 0x0018C767
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IPropertySymbol GetPublicSymbol(this PropertySymbol symbol)
		{
			return symbol.GetPublicSymbol<IPropertySymbol>();
		}

		// Token: 0x06006B20 RID: 27424 RVA: 0x0018E56F File Offset: 0x0018C76F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static INamedTypeSymbol GetPublicSymbol(this NamedTypeSymbol symbol)
		{
			return symbol.GetPublicSymbol<INamedTypeSymbol>();
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x0018E577 File Offset: 0x0018C777
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static INamespaceSymbol GetPublicSymbol(this NamespaceSymbol symbol)
		{
			return symbol.GetPublicSymbol<INamespaceSymbol>();
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x0018E57F File Offset: 0x0018C77F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static ITypeSymbol GetPublicSymbol(this TypeSymbol symbol)
		{
			return symbol.GetPublicSymbol<ITypeSymbol>();
		}

		// Token: 0x06006B23 RID: 27427 RVA: 0x0018E587 File Offset: 0x0018C787
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static ILocalSymbol GetPublicSymbol(this LocalSymbol symbol)
		{
			return symbol.GetPublicSymbol<ILocalSymbol>();
		}

		// Token: 0x06006B24 RID: 27428 RVA: 0x0018E58F File Offset: 0x0018C78F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IAssemblySymbol GetPublicSymbol(this AssemblySymbol symbol)
		{
			return symbol.GetPublicSymbol<IAssemblySymbol>();
		}

		// Token: 0x06006B25 RID: 27429 RVA: 0x0018E597 File Offset: 0x0018C797
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static INamespaceOrTypeSymbol GetPublicSymbol(this NamespaceOrTypeSymbol symbol)
		{
			return symbol.GetPublicSymbol<INamespaceOrTypeSymbol>();
		}

		// Token: 0x06006B26 RID: 27430 RVA: 0x0018E59F File Offset: 0x0018C79F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IDiscardSymbol GetPublicSymbol(this DiscardSymbol symbol)
		{
			return symbol.GetPublicSymbol<IDiscardSymbol>();
		}

		// Token: 0x06006B27 RID: 27431 RVA: 0x0018E5A7 File Offset: 0x0018C7A7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IFieldSymbol GetPublicSymbol(this FieldSymbol symbol)
		{
			return symbol.GetPublicSymbol<IFieldSymbol>();
		}

		// Token: 0x06006B28 RID: 27432 RVA: 0x0018E5AF File Offset: 0x0018C7AF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IParameterSymbol GetPublicSymbol(this ParameterSymbol symbol)
		{
			return symbol.GetPublicSymbol<IParameterSymbol>();
		}

		// Token: 0x06006B29 RID: 27433 RVA: 0x0018E5B7 File Offset: 0x0018C7B7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IRangeVariableSymbol GetPublicSymbol(this RangeVariableSymbol symbol)
		{
			return symbol.GetPublicSymbol<IRangeVariableSymbol>();
		}

		// Token: 0x06006B2A RID: 27434 RVA: 0x0018E5BF File Offset: 0x0018C7BF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static ILabelSymbol GetPublicSymbol(this LabelSymbol symbol)
		{
			return symbol.GetPublicSymbol<ILabelSymbol>();
		}

		// Token: 0x06006B2B RID: 27435 RVA: 0x0018E5C7 File Offset: 0x0018C7C7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IAliasSymbol GetPublicSymbol(this AliasSymbol symbol)
		{
			return symbol.GetPublicSymbol<IAliasSymbol>();
		}

		// Token: 0x06006B2C RID: 27436 RVA: 0x0018E5CF File Offset: 0x0018C7CF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IModuleSymbol GetPublicSymbol(this ModuleSymbol symbol)
		{
			return symbol.GetPublicSymbol<IModuleSymbol>();
		}

		// Token: 0x06006B2D RID: 27437 RVA: 0x0018E5D7 File Offset: 0x0018C7D7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static ITypeParameterSymbol GetPublicSymbol(this TypeParameterSymbol symbol)
		{
			return symbol.GetPublicSymbol<ITypeParameterSymbol>();
		}

		// Token: 0x06006B2E RID: 27438 RVA: 0x0018E5DF File Offset: 0x0018C7DF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IArrayTypeSymbol GetPublicSymbol(this ArrayTypeSymbol symbol)
		{
			return symbol.GetPublicSymbol<IArrayTypeSymbol>();
		}

		// Token: 0x06006B2F RID: 27439 RVA: 0x0018E5E7 File Offset: 0x0018C7E7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IPointerTypeSymbol GetPublicSymbol(this PointerTypeSymbol symbol)
		{
			return symbol.GetPublicSymbol<IPointerTypeSymbol>();
		}

		// Token: 0x06006B30 RID: 27440 RVA: 0x0018E5EF File Offset: 0x0018C7EF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IFunctionPointerTypeSymbol GetPublicSymbol(this FunctionPointerTypeSymbol symbol)
		{
			return symbol.GetPublicSymbol<IFunctionPointerTypeSymbol>();
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x0018E5F7 File Offset: 0x0018C7F7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static IEventSymbol GetPublicSymbol(this EventSymbol symbol)
		{
			return symbol.GetPublicSymbol<IEventSymbol>();
		}

		// Token: 0x06006B32 RID: 27442 RVA: 0x0018E5FF File Offset: 0x0018C7FF
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })]
		internal static IEnumerable<ISymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })] this IEnumerable<Symbol> symbols)
		{
			return Enumerable.Select<Symbol, ISymbol>(symbols, (Symbol p) => p.GetPublicSymbol<ISymbol>());
		}

		// Token: 0x06006B33 RID: 27443 RVA: 0x0018E628 File Offset: 0x0018C828
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<TISymbol> GetPublicSymbols<TISymbol>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<Symbol> symbols) where TISymbol : class, ISymbol
		{
			if (symbols.IsDefault)
			{
				return default(ImmutableArray<TISymbol>);
			}
			return symbols.SelectAsArray<Symbol, TISymbol>((Symbol p) => p.GetPublicSymbol<TISymbol>());
		}

		// Token: 0x06006B34 RID: 27444 RVA: 0x0018E66D File Offset: 0x0018C86D
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<ISymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<Symbol> symbols)
		{
			return symbols.GetPublicSymbols<ISymbol>();
		}

		// Token: 0x06006B35 RID: 27445 RVA: 0x0018E675 File Offset: 0x0018C875
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<IPropertySymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<PropertySymbol> symbols)
		{
			return StaticCast<Symbol>.From<PropertySymbol>(symbols).GetPublicSymbols<IPropertySymbol>();
		}

		// Token: 0x06006B36 RID: 27446 RVA: 0x0018E682 File Offset: 0x0018C882
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<ITypeSymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<TypeSymbol> symbols)
		{
			return StaticCast<Symbol>.From<TypeSymbol>(symbols).GetPublicSymbols<ITypeSymbol>();
		}

		// Token: 0x06006B37 RID: 27447 RVA: 0x0018E68F File Offset: 0x0018C88F
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<INamedTypeSymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<NamedTypeSymbol> symbols)
		{
			return StaticCast<Symbol>.From<NamedTypeSymbol>(symbols).GetPublicSymbols<INamedTypeSymbol>();
		}

		// Token: 0x06006B38 RID: 27448 RVA: 0x0018E69C File Offset: 0x0018C89C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<ILocalSymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<LocalSymbol> symbols)
		{
			return StaticCast<Symbol>.From<LocalSymbol>(symbols).GetPublicSymbols<ILocalSymbol>();
		}

		// Token: 0x06006B39 RID: 27449 RVA: 0x0018E6A9 File Offset: 0x0018C8A9
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<IEventSymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<EventSymbol> symbols)
		{
			return StaticCast<Symbol>.From<EventSymbol>(symbols).GetPublicSymbols<IEventSymbol>();
		}

		// Token: 0x06006B3A RID: 27450 RVA: 0x0018E6B6 File Offset: 0x0018C8B6
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<ITypeParameterSymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<TypeParameterSymbol> symbols)
		{
			return StaticCast<Symbol>.From<TypeParameterSymbol>(symbols).GetPublicSymbols<ITypeParameterSymbol>();
		}

		// Token: 0x06006B3B RID: 27451 RVA: 0x0018E6C3 File Offset: 0x0018C8C3
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<IParameterSymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<ParameterSymbol> symbols)
		{
			return StaticCast<Symbol>.From<ParameterSymbol>(symbols).GetPublicSymbols<IParameterSymbol>();
		}

		// Token: 0x06006B3C RID: 27452 RVA: 0x0018E6D0 File Offset: 0x0018C8D0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<IMethodSymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<MethodSymbol> symbols)
		{
			return StaticCast<Symbol>.From<MethodSymbol>(symbols).GetPublicSymbols<IMethodSymbol>();
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x0018E6DD File Offset: 0x0018C8DD
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<IAssemblySymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<AssemblySymbol> symbols)
		{
			return StaticCast<Symbol>.From<AssemblySymbol>(symbols).GetPublicSymbols<IAssemblySymbol>();
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x0018E6EA File Offset: 0x0018C8EA
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<IFieldSymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<FieldSymbol> symbols)
		{
			return StaticCast<Symbol>.From<FieldSymbol>(symbols).GetPublicSymbols<IFieldSymbol>();
		}

		// Token: 0x06006B3F RID: 27455 RVA: 0x0018E6F7 File Offset: 0x0018C8F7
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<INamespaceSymbol> GetPublicSymbols([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this ImmutableArray<NamespaceSymbol> symbols)
		{
			return StaticCast<Symbol>.From<NamespaceSymbol>(symbols).GetPublicSymbols<INamespaceSymbol>();
		}

		// Token: 0x06006B40 RID: 27456 RVA: 0x0018E704 File Offset: 0x0018C904
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static TSymbol GetSymbol<[global::System.Runtime.CompilerServices.Nullable(0)] TSymbol>(this ISymbol symbol) where TSymbol : Symbol
		{
			Symbol symbol2 = (Symbol)symbol;
			return (TSymbol)((object)((symbol2 != null) ? symbol2.UnderlyingSymbol : null));
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x0018E71D File Offset: 0x0018C91D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static Symbol GetSymbol(this ISymbol symbol)
		{
			return symbol.GetSymbol<Symbol>();
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x0018E725 File Offset: 0x0018C925
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static TypeSymbol GetSymbol(this ITypeSymbol symbol)
		{
			return symbol.GetSymbol<TypeSymbol>();
		}

		// Token: 0x06006B43 RID: 27459 RVA: 0x0018E72D File Offset: 0x0018C92D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static NamedTypeSymbol GetSymbol(this INamedTypeSymbol symbol)
		{
			return symbol.GetSymbol<NamedTypeSymbol>();
		}

		// Token: 0x06006B44 RID: 27460 RVA: 0x0018E735 File Offset: 0x0018C935
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static AliasSymbol GetSymbol(this IAliasSymbol symbol)
		{
			return symbol.GetSymbol<AliasSymbol>();
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x0018E73D File Offset: 0x0018C93D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static LocalSymbol GetSymbol(this ILocalSymbol symbol)
		{
			return symbol.GetSymbol<LocalSymbol>();
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x0018E745 File Offset: 0x0018C945
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static AssemblySymbol GetSymbol(this IAssemblySymbol symbol)
		{
			return symbol.GetSymbol<AssemblySymbol>();
		}

		// Token: 0x06006B47 RID: 27463 RVA: 0x0018E74D File Offset: 0x0018C94D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static MethodSymbol GetSymbol(this IMethodSymbol symbol)
		{
			return symbol.GetSymbol<MethodSymbol>();
		}

		// Token: 0x06006B48 RID: 27464 RVA: 0x0018E755 File Offset: 0x0018C955
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static PropertySymbol GetSymbol(this IPropertySymbol symbol)
		{
			return symbol.GetSymbol<PropertySymbol>();
		}

		// Token: 0x06006B49 RID: 27465 RVA: 0x0018E75D File Offset: 0x0018C95D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("symbol")]
		internal static FunctionPointerTypeSymbol GetSymbol(this IFunctionPointerTypeSymbol symbol)
		{
			return symbol.GetSymbol<FunctionPointerTypeSymbol>();
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x0018E77C File Offset: 0x0018C97C
		[CompilerGenerated]
		internal static bool <IsEventOrPropertyWithImplementableNonPublicAccessor>g__isImplementableAndNotPublic|7_0(MethodSymbol accessor)
		{
			return accessor.IsImplementable() && accessor.DeclaredAccessibility != Accessibility.Public;
		}

		// Token: 0x04001AA1 RID: 6817
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private static readonly Func<TypeSymbol, Symbol, bool, bool> s_hasInvalidTypeParameterFunc = (TypeSymbol type, Symbol containingSymbol, bool unused) => SymbolExtensions.HasInvalidTypeParameter(type, containingSymbol);
	}
}
