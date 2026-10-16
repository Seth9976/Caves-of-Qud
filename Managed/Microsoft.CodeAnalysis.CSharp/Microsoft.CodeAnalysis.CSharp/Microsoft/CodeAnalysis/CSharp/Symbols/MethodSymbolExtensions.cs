using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F2 RID: 1266
	internal static class MethodSymbolExtensions
	{
		// Token: 0x06006B6A RID: 27498 RVA: 0x0018EF10 File Offset: 0x0018D110
		public static bool IsParams(this MethodSymbol method)
		{
			return method.ParameterCount != 0 && method.Parameters[method.ParameterCount - 1].IsParams;
		}

		// Token: 0x06006B6B RID: 27499 RVA: 0x0018EF42 File Offset: 0x0018D142
		internal static bool IsSynthesizedLambda(this MethodSymbol method)
		{
			return method.IsImplicitlyDeclared && method.MethodKind == MethodKind.AnonymousFunction;
		}

		// Token: 0x06006B6C RID: 27500 RVA: 0x0018EF58 File Offset: 0x0018D158
		public static bool IsRuntimeFinalizer(this MethodSymbol method, bool skipFirstMethodKindCheck = false)
		{
			if (method == null || method.Name != "Finalize" || method.ParameterCount != 0 || method.Arity != 0 || !method.IsMetadataVirtual(true))
			{
				return false;
			}
			while (method != null)
			{
				if (!skipFirstMethodKindCheck && method.MethodKind == MethodKind.Destructor)
				{
					return true;
				}
				if (method.ContainingType.SpecialType == SpecialType.System_Object)
				{
					return true;
				}
				if (method.IsMetadataNewSlot(true))
				{
					return false;
				}
				bool flag;
				method = method.GetFirstRuntimeOverriddenMethodIgnoringNewSlot(out flag);
				skipFirstMethodKindCheck = false;
			}
			return false;
		}

		// Token: 0x06006B6D RID: 27501 RVA: 0x0018EFCF File Offset: 0x0018D1CF
		public static MethodSymbol ConstructIfGeneric(this MethodSymbol method, ImmutableArray<TypeWithAnnotations> typeArguments)
		{
			if (!method.IsGenericMethod)
			{
				return method;
			}
			return method.Construct(typeArguments);
		}

		// Token: 0x06006B6E RID: 27502 RVA: 0x0018EFE4 File Offset: 0x0018D1E4
		public static bool CanBeHiddenByMemberKind(this MethodSymbol hiddenMethod, SymbolKind hidingMemberKind)
		{
			if (hiddenMethod.MethodKind == MethodKind.Destructor)
			{
				return false;
			}
			switch (hidingMemberKind)
			{
			case SymbolKind.ErrorType:
			case SymbolKind.Method:
			case SymbolKind.NamedType:
				break;
			case SymbolKind.Event:
			case SymbolKind.Field:
				return true;
			case SymbolKind.Label:
			case SymbolKind.Local:
			case SymbolKind.NetModule:
				goto IL_0041;
			default:
				if (hidingMemberKind != SymbolKind.Property)
				{
					goto IL_0041;
				}
				break;
			}
			return MethodSymbolExtensions.CanBeHiddenByMethodPropertyOrType(hiddenMethod);
			IL_0041:
			throw ExceptionUtilities.UnexpectedValue(hidingMemberKind);
		}

		// Token: 0x06006B6F RID: 27503 RVA: 0x0018F040 File Offset: 0x0018D240
		private static bool CanBeHiddenByMethodPropertyOrType(MethodSymbol method)
		{
			switch (method.MethodKind)
			{
			case MethodKind.Constructor:
			case MethodKind.Conversion:
			case MethodKind.Destructor:
			case MethodKind.UserDefinedOperator:
			case MethodKind.StaticConstructor:
				return false;
			case MethodKind.EventAdd:
			case MethodKind.EventRemove:
			case MethodKind.PropertyGet:
			case MethodKind.PropertySet:
				return method.IsIndexedPropertyAccessor();
			}
			return true;
		}

		// Token: 0x06006B70 RID: 27504 RVA: 0x0018F0A0 File Offset: 0x0018D2A0
		public static bool IsAsyncReturningVoid(this MethodSymbol method)
		{
			return method.IsAsync && method.ReturnsVoid;
		}

		// Token: 0x06006B71 RID: 27505 RVA: 0x0018F0B2 File Offset: 0x0018D2B2
		public static bool IsAsyncReturningTask(this MethodSymbol method, CSharpCompilation compilation)
		{
			return method.IsAsync && method.ReturnType.IsNonGenericTaskType(compilation);
		}

		// Token: 0x06006B72 RID: 27506 RVA: 0x0018F0CA File Offset: 0x0018D2CA
		public static bool IsAsyncReturningGenericTask(this MethodSymbol method, CSharpCompilation compilation)
		{
			return method.IsAsync && method.ReturnType.IsGenericTaskType(compilation);
		}

		// Token: 0x06006B73 RID: 27507 RVA: 0x0018F0E2 File Offset: 0x0018D2E2
		public static bool IsAsyncReturningIAsyncEnumerable(this MethodSymbol method, CSharpCompilation compilation)
		{
			return method.IsAsync && method.ReturnType.IsIAsyncEnumerableType(compilation);
		}

		// Token: 0x06006B74 RID: 27508 RVA: 0x0018F0FA File Offset: 0x0018D2FA
		public static bool IsAsyncReturningIAsyncEnumerator(this MethodSymbol method, CSharpCompilation compilation)
		{
			return method.IsAsync && method.ReturnType.IsIAsyncEnumeratorType(compilation);
		}

		// Token: 0x06006B75 RID: 27509 RVA: 0x0018F114 File Offset: 0x0018D314
		internal static CSharpSyntaxNode ExtractReturnTypeSyntax(this MethodSymbol method)
		{
			SynthesizedSimpleProgramEntryPointSymbol synthesizedSimpleProgramEntryPointSymbol = method as SynthesizedSimpleProgramEntryPointSymbol;
			if (synthesizedSimpleProgramEntryPointSymbol != null)
			{
				return (CSharpSyntaxNode)synthesizedSimpleProgramEntryPointSymbol.ReturnTypeSyntax;
			}
			method = method.PartialDefinitionPart ?? method;
			foreach (SyntaxReference syntaxReference in method.DeclaringSyntaxReferences)
			{
				SyntaxNode syntax = syntaxReference.GetSyntax(default(CancellationToken));
				MethodDeclarationSyntax methodDeclarationSyntax = syntax as MethodDeclarationSyntax;
				if (methodDeclarationSyntax != null)
				{
					return methodDeclarationSyntax.ReturnType;
				}
				LocalFunctionStatementSyntax localFunctionStatementSyntax = syntax as LocalFunctionStatementSyntax;
				if (localFunctionStatementSyntax != null)
				{
					return localFunctionStatementSyntax.ReturnType;
				}
			}
			return (CSharpSyntaxNode)CSharpSyntaxTree.Dummy.GetRoot(default(CancellationToken));
		}
	}
}
