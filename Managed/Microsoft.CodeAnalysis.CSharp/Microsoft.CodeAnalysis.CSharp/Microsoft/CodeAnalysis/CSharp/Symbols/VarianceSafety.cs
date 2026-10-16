using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005AF RID: 1455
	internal static class VarianceSafety
	{
		// Token: 0x06007A26 RID: 31270 RVA: 0x001BEC7C File Offset: 0x001BCE7C
		internal static void CheckInterfaceVarianceSafety(this NamedTypeSymbol interfaceType, BindingDiagnosticBag diagnostics)
		{
			foreach (NamedTypeSymbol namedTypeSymbol in interfaceType.InterfacesNoUseSiteDiagnostics(null))
			{
				VarianceSafety.IsVarianceUnsafe<NamedTypeSymbol>(namedTypeSymbol, true, false, namedTypeSymbol, (NamedTypeSymbol i) => null, namedTypeSymbol, diagnostics);
			}
			foreach (Symbol symbol in interfaceType.GetMembersUnordered())
			{
				SymbolKind kind = symbol.Kind;
				if (kind <= SymbolKind.Method)
				{
					if (kind != SymbolKind.Event)
					{
						if (kind == SymbolKind.Method)
						{
							if (!symbol.IsAccessor())
							{
								((MethodSymbol)symbol).CheckMethodVarianceSafety(diagnostics);
							}
						}
					}
					else
					{
						VarianceSafety.CheckEventVarianceSafety((EventSymbol)symbol, diagnostics);
					}
				}
				else if (kind != SymbolKind.NamedType)
				{
					if (kind == SymbolKind.Property)
					{
						VarianceSafety.CheckPropertyVarianceSafety((PropertySymbol)symbol, diagnostics);
					}
				}
				else
				{
					VarianceSafety.CheckNestedTypeVarianceSafety((NamedTypeSymbol)symbol, diagnostics);
				}
			}
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x001BED68 File Offset: 0x001BCF68
		private static void CheckNestedTypeVarianceSafety(NamedTypeSymbol member, BindingDiagnosticBag diagnostics)
		{
			switch (member.TypeKind)
			{
			case TypeKind.Class:
			case TypeKind.Enum:
			case TypeKind.Struct:
				if (VarianceSafety.GetEnclosingVariantInterface(member) != null)
				{
					diagnostics.Add(ErrorCode.ERR_VarianceInterfaceNesting, member.Locations[0]);
				}
				return;
			case TypeKind.Delegate:
			case TypeKind.Interface:
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(member.TypeKind);
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x001BEDE0 File Offset: 0x001BCFE0
		internal static NamedTypeSymbol GetEnclosingVariantInterface(Symbol member)
		{
			NamedTypeSymbol namedTypeSymbol = member.ContainingType;
			while (namedTypeSymbol != null && namedTypeSymbol.IsInterfaceType())
			{
				if (namedTypeSymbol.TypeParameters.Any<TypeParameterSymbol>((TypeParameterSymbol tp) => tp.Variance > VarianceKind.None))
				{
					return namedTypeSymbol;
				}
				namedTypeSymbol = namedTypeSymbol.ContainingType;
			}
			return null;
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x001BEE37 File Offset: 0x001BD037
		internal static void CheckDelegateVarianceSafety(this SourceDelegateMethodSymbol method, BindingDiagnosticBag diagnostics)
		{
			method.CheckMethodVarianceSafety(delegate(MethodSymbol m)
			{
				DelegateDeclarationSyntax declaringSyntax = m.GetDeclaringSyntax<DelegateDeclarationSyntax>();
				if (declaringSyntax != null)
				{
					return declaringSyntax.ReturnType.Location;
				}
				return null;
			}, diagnostics);
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x001BEE5F File Offset: 0x001BD05F
		private static void CheckMethodVarianceSafety(this MethodSymbol method, BindingDiagnosticBag diagnostics)
		{
			method.CheckMethodVarianceSafety(delegate(MethodSymbol m)
			{
				MethodDeclarationSyntax declaringSyntax = m.GetDeclaringSyntax<MethodDeclarationSyntax>();
				if (declaringSyntax != null)
				{
					return declaringSyntax.ReturnType.Location;
				}
				return null;
			}, diagnostics);
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x001BEE87 File Offset: 0x001BD087
		private static void CheckMethodVarianceSafety(this MethodSymbol method, VarianceSafety.LocationProvider<MethodSymbol> returnTypeLocationProvider, BindingDiagnosticBag diagnostics)
		{
			if (VarianceSafety.SkipVarianceSafetyChecks(method))
			{
				return;
			}
			VarianceSafety.CheckTypeParametersVarianceSafety(method.TypeParameters, method, diagnostics);
			VarianceSafety.IsVarianceUnsafe<MethodSymbol>(method.ReturnType, true, method.RefKind > RefKind.None, method, returnTypeLocationProvider, method, diagnostics);
			VarianceSafety.CheckParametersVarianceSafety(method.Parameters, method, diagnostics);
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x001BEEC6 File Offset: 0x001BD0C6
		private static bool SkipVarianceSafetyChecks(Symbol member)
		{
			return member.IsStatic && MessageID.IDS_FeatureVarianceSafetyForStaticInterfaceMembers.RequiredVersion() <= member.DeclaringCompilation.LanguageVersion;
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x001BEEEC File Offset: 0x001BD0EC
		private static void CheckPropertyVarianceSafety(PropertySymbol property, BindingDiagnosticBag diagnostics)
		{
			if (VarianceSafety.SkipVarianceSafetyChecks(property))
			{
				return;
			}
			bool flag = property.GetMethod != null;
			bool flag2 = property.SetMethod != null;
			if (flag || flag2)
			{
				TypeSymbol type = property.Type;
				bool flag3 = flag;
				bool flag4;
				if (!flag2)
				{
					MethodSymbol getMethod = property.GetMethod;
					flag4 = getMethod == null || getMethod.RefKind != RefKind.None;
				}
				else
				{
					flag4 = true;
				}
				VarianceSafety.IsVarianceUnsafe<PropertySymbol>(type, flag3, flag4, property, delegate(PropertySymbol p)
				{
					BasePropertyDeclarationSyntax declaringSyntax = p.GetDeclaringSyntax<BasePropertyDeclarationSyntax>();
					if (declaringSyntax != null)
					{
						return declaringSyntax.Type.Location;
					}
					return null;
				}, property, diagnostics);
			}
			VarianceSafety.CheckParametersVarianceSafety(property.Parameters, property, diagnostics);
		}

		// Token: 0x06007A2E RID: 31278 RVA: 0x001BEF75 File Offset: 0x001BD175
		private static void CheckEventVarianceSafety(EventSymbol @event, BindingDiagnosticBag diagnostics)
		{
			if (VarianceSafety.SkipVarianceSafetyChecks(@event))
			{
				return;
			}
			VarianceSafety.IsVarianceUnsafe<EventSymbol>(@event.Type, false, true, @event, (EventSymbol e) => e.Locations[0], @event, diagnostics);
		}

		// Token: 0x06007A2F RID: 31279 RVA: 0x001BEFB0 File Offset: 0x001BD1B0
		private static void CheckParametersVarianceSafety(ImmutableArray<ParameterSymbol> parameters, Symbol context, BindingDiagnosticBag diagnostics)
		{
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				VarianceSafety.IsVarianceUnsafe<ParameterSymbol>(parameterSymbol.Type, parameterSymbol.RefKind > RefKind.None, true, context, delegate(ParameterSymbol p)
				{
					ParameterSyntax declaringSyntax = p.GetDeclaringSyntax<ParameterSyntax>();
					if (declaringSyntax != null)
					{
						return declaringSyntax.Type.Location;
					}
					return null;
				}, parameterSymbol, diagnostics);
			}
		}

		// Token: 0x06007A30 RID: 31280 RVA: 0x001BF010 File Offset: 0x001BD210
		private static void CheckTypeParametersVarianceSafety(ImmutableArray<TypeParameterSymbol> typeParameters, MethodSymbol context, BindingDiagnosticBag diagnostics)
		{
			foreach (TypeParameterSymbol typeParameterSymbol in typeParameters)
			{
				foreach (TypeWithAnnotations typeWithAnnotations in typeParameterSymbol.ConstraintTypesNoUseSiteDiagnostics)
				{
					VarianceSafety.IsVarianceUnsafe<TypeParameterSymbol>(typeWithAnnotations.Type, false, true, context, (TypeParameterSymbol t) => t.Locations[0], typeParameterSymbol, diagnostics);
				}
			}
		}

		// Token: 0x06007A31 RID: 31281 RVA: 0x001BF08C File Offset: 0x001BD28C
		private static bool IsVarianceUnsafe<T>(TypeSymbol type, bool requireOutputSafety, bool requireInputSafety, Symbol context, VarianceSafety.LocationProvider<T> locationProvider, T locationArg, BindingDiagnosticBag diagnostics) where T : Symbol
		{
			SymbolKind kind = type.Kind;
			if (kind <= SymbolKind.ErrorType)
			{
				if (kind == SymbolKind.ArrayType)
				{
					return VarianceSafety.IsVarianceUnsafe<T>(((ArrayTypeSymbol)type).ElementType, requireOutputSafety, requireInputSafety, context, locationProvider, locationArg, diagnostics);
				}
				if (kind != SymbolKind.ErrorType)
				{
					return false;
				}
			}
			else if (kind != SymbolKind.NamedType)
			{
				if (kind != SymbolKind.TypeParameter)
				{
					return false;
				}
				TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)type;
				if (requireInputSafety && requireOutputSafety && typeParameterSymbol.Variance != VarianceKind.None)
				{
					diagnostics.AddVarianceError(typeParameterSymbol, context, locationProvider, locationArg, MessageID.IDS_Invariantly);
					return true;
				}
				if (requireOutputSafety && typeParameterSymbol.Variance == VarianceKind.In)
				{
					diagnostics.AddVarianceError(typeParameterSymbol, context, locationProvider, locationArg, MessageID.IDS_Covariantly);
					return true;
				}
				if (requireInputSafety && typeParameterSymbol.Variance == VarianceKind.Out)
				{
					diagnostics.AddVarianceError(typeParameterSymbol, context, locationProvider, locationArg, MessageID.IDS_Contravariantly);
					return true;
				}
				return false;
			}
			return VarianceSafety.IsVarianceUnsafe<T>((NamedTypeSymbol)type, requireOutputSafety, requireInputSafety, context, locationProvider, locationArg, diagnostics);
		}

		// Token: 0x06007A32 RID: 31282 RVA: 0x001BF164 File Offset: 0x001BD364
		private static bool IsVarianceUnsafe<T>(NamedTypeSymbol namedType, bool requireOutputSafety, bool requireInputSafety, Symbol context, VarianceSafety.LocationProvider<T> locationProvider, T locationArg, BindingDiagnosticBag diagnostics) where T : Symbol
		{
			switch (namedType.TypeKind)
			{
			case TypeKind.Class:
			case TypeKind.Delegate:
			case TypeKind.Enum:
			case TypeKind.Error:
			case TypeKind.Interface:
			case TypeKind.Struct:
				while (namedType != null)
				{
					for (int i = 0; i < namedType.Arity; i++)
					{
						TypeParameterSymbol typeParameterSymbol = namedType.TypeParameters[i];
						TypeSymbol type = namedType.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[i].Type;
						bool flag;
						bool flag2;
						switch (typeParameterSymbol.Variance)
						{
						case VarianceKind.None:
							flag = true;
							flag2 = true;
							break;
						case VarianceKind.Out:
							flag2 = requireOutputSafety;
							flag = requireInputSafety;
							break;
						case VarianceKind.In:
							flag2 = requireInputSafety;
							flag = requireOutputSafety;
							break;
						default:
							throw ExceptionUtilities.UnexpectedValue(typeParameterSymbol.Variance);
						}
						if (VarianceSafety.IsVarianceUnsafe<T>(type, flag2, flag, context, locationProvider, locationArg, diagnostics))
						{
							return true;
						}
					}
					namedType = namedType.ContainingType;
				}
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x001BF254 File Offset: 0x001BD454
		private static void AddVarianceError<T>(this BindingDiagnosticBag diagnostics, TypeParameterSymbol unsafeTypeParameter, Symbol context, VarianceSafety.LocationProvider<T> locationProvider, T locationArg, MessageID expectedVariance) where T : Symbol
		{
			VarianceKind variance = unsafeTypeParameter.Variance;
			MessageID messageID;
			if (variance != VarianceKind.Out)
			{
				if (variance != VarianceKind.In)
				{
					throw ExceptionUtilities.UnexpectedValue(unsafeTypeParameter.Variance);
				}
				messageID = MessageID.IDS_Contravariant;
			}
			else
			{
				messageID = MessageID.IDS_Covariant;
			}
			Location location = locationProvider(locationArg) ?? unsafeTypeParameter.Locations[0];
			if (!(context is TypeSymbol) && context.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_UnexpectedVarianceStaticMember, location, new object[]
				{
					context,
					unsafeTypeParameter,
					messageID.Localize(),
					expectedVariance.Localize(),
					new CSharpRequiredLanguageVersion(MessageID.IDS_FeatureVarianceSafetyForStaticInterfaceMembers.RequiredVersion())
				});
				return;
			}
			diagnostics.Add(ErrorCode.ERR_UnexpectedVariance, location, new object[]
			{
				context,
				unsafeTypeParameter,
				messageID.Localize(),
				expectedVariance.Localize()
			});
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x001BF340 File Offset: 0x001BD540
		private static T GetDeclaringSyntax<T>(this Symbol symbol) where T : SyntaxNode
		{
			ImmutableArray<SyntaxReference> declaringSyntaxReferences = symbol.DeclaringSyntaxReferences;
			if (declaringSyntaxReferences.Length == 0)
			{
				return default(T);
			}
			return declaringSyntaxReferences[0].GetSyntax(default(CancellationToken)) as T;
		}

		// Token: 0x02000967 RID: 2407
		// (Invoke) Token: 0x0600915A RID: 37210
		private delegate Location LocationProvider<T>(T arg);
	}
}
