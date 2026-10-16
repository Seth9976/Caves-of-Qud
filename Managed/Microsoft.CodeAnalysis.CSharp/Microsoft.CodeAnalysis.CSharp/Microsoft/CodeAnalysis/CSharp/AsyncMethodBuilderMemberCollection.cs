using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.RuntimeMembers;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001EC RID: 492
	internal struct AsyncMethodBuilderMemberCollection
	{
		// Token: 0x0600218C RID: 8588 RVA: 0x000AE600 File Offset: 0x000AC800
		private AsyncMethodBuilderMemberCollection(NamedTypeSymbol builderType, TypeSymbol resultType, MethodSymbol createBuilder, MethodSymbol setException, MethodSymbol setResult, MethodSymbol awaitOnCompleted, MethodSymbol awaitUnsafeOnCompleted, MethodSymbol start, MethodSymbol setStateMachine, PropertySymbol task, bool checkGenericMethodConstraints)
		{
			this.BuilderType = builderType;
			this.ResultType = resultType;
			this.CreateBuilder = createBuilder;
			this.SetException = setException;
			this.SetResult = setResult;
			this.AwaitOnCompleted = awaitOnCompleted;
			this.AwaitUnsafeOnCompleted = awaitUnsafeOnCompleted;
			this.Start = start;
			this.SetStateMachine = setStateMachine;
			this.Task = task;
			this.CheckGenericMethodConstraints = checkGenericMethodConstraints;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000AE664 File Offset: 0x000AC864
		internal static bool TryCreate(SyntheticBoundNodeFactory F, MethodSymbol method, TypeMap typeMap, out AsyncMethodBuilderMemberCollection collection)
		{
			if (method.IsIterator)
			{
				NamedTypeSymbol namedTypeSymbol = F.WellKnownType(WellKnownType.System_Runtime_CompilerServices_AsyncIteratorMethodBuilder);
				MethodSymbol methodSymbol;
				AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncIteratorMethodBuilder__Create), namedTypeSymbol, false, out methodSymbol);
				if (methodSymbol == null)
				{
					collection = default(AsyncMethodBuilderMemberCollection);
					return false;
				}
				return AsyncMethodBuilderMemberCollection.TryCreate(F, false, namedTypeSymbol, F.SpecialType(SpecialType.System_Void), methodSymbol, null, default(WellKnownMember?), WellKnownMember.System_Runtime_CompilerServices_AsyncIteratorMethodBuilder__Complete, WellKnownMember.System_Runtime_CompilerServices_AsyncIteratorMethodBuilder__AwaitOnCompleted, WellKnownMember.System_Runtime_CompilerServices_AsyncIteratorMethodBuilder__AwaitUnsafeOnCompleted, WellKnownMember.System_Runtime_CompilerServices_AsyncIteratorMethodBuilder__MoveNext_T, default(WellKnownMember?), out collection);
			}
			else if (method.IsAsyncReturningVoid())
			{
				NamedTypeSymbol namedTypeSymbol2 = F.WellKnownType(WellKnownType.System_Runtime_CompilerServices_AsyncVoidMethodBuilder);
				bool flag = false;
				MethodSymbol methodSymbol2;
				AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncVoidMethodBuilder__Create), namedTypeSymbol2, flag, out methodSymbol2);
				if (methodSymbol2 == null)
				{
					collection = default(AsyncMethodBuilderMemberCollection);
					return false;
				}
				return AsyncMethodBuilderMemberCollection.TryCreate(F, flag, namedTypeSymbol2, F.SpecialType(SpecialType.System_Void), methodSymbol2, null, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncVoidMethodBuilder__SetException), WellKnownMember.System_Runtime_CompilerServices_AsyncVoidMethodBuilder__SetResult, WellKnownMember.System_Runtime_CompilerServices_AsyncVoidMethodBuilder__AwaitOnCompleted, WellKnownMember.System_Runtime_CompilerServices_AsyncVoidMethodBuilder__AwaitUnsafeOnCompleted, WellKnownMember.System_Runtime_CompilerServices_AsyncVoidMethodBuilder__Start_T, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncVoidMethodBuilder__SetStateMachine), out collection);
			}
			else if (method.IsAsyncReturningTask(F.Compilation))
			{
				NamedTypeSymbol namedTypeSymbol3 = (NamedTypeSymbol)method.ReturnType;
				MethodSymbol methodSymbol3 = null;
				PropertySymbol propertySymbol = null;
				object obj;
				bool flag2 = namedTypeSymbol3.IsCustomTaskType(out obj);
				NamedTypeSymbol namedTypeSymbol4;
				if (flag2)
				{
					namedTypeSymbol4 = AsyncMethodBuilderMemberCollection.ValidateBuilderType(F, obj, namedTypeSymbol3.DeclaredAccessibility, false);
					if (namedTypeSymbol4 != null)
					{
						propertySymbol = AsyncMethodBuilderMemberCollection.GetCustomTaskProperty(F, namedTypeSymbol4, namedTypeSymbol3);
						methodSymbol3 = AsyncMethodBuilderMemberCollection.GetCustomCreateMethod(F, namedTypeSymbol4);
					}
				}
				else
				{
					namedTypeSymbol4 = F.WellKnownType(WellKnownType.System_Runtime_CompilerServices_AsyncTaskMethodBuilder);
					AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder__Create), namedTypeSymbol4, flag2, out methodSymbol3);
					AsyncMethodBuilderMemberCollection.TryGetBuilderMember<PropertySymbol>(F, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder__Task), namedTypeSymbol4, flag2, out propertySymbol);
				}
				if (namedTypeSymbol4 == null || methodSymbol3 == null || propertySymbol == null)
				{
					collection = default(AsyncMethodBuilderMemberCollection);
					return false;
				}
				return AsyncMethodBuilderMemberCollection.TryCreate(F, flag2, namedTypeSymbol4, F.SpecialType(SpecialType.System_Void), methodSymbol3, propertySymbol, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder__SetException), WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder__SetResult, WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder__AwaitOnCompleted, WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder__AwaitUnsafeOnCompleted, WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder__Start_T, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder__SetStateMachine), out collection);
			}
			else
			{
				if (!method.IsAsyncReturningGenericTask(F.Compilation))
				{
					throw ExceptionUtilities.UnexpectedValue(method);
				}
				NamedTypeSymbol namedTypeSymbol5 = (NamedTypeSymbol)method.ReturnType;
				TypeSymbol typeSymbol = namedTypeSymbol5.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.Single<TypeWithAnnotations>().Type;
				if (typeSymbol.IsDynamic())
				{
					typeSymbol = F.SpecialType(SpecialType.System_Object);
				}
				if (typeMap != null)
				{
					typeSymbol = typeMap.SubstituteType(typeSymbol).Type;
				}
				namedTypeSymbol5 = namedTypeSymbol5.ConstructedFrom.Construct(new TypeSymbol[] { typeSymbol });
				MethodSymbol methodSymbol4 = null;
				PropertySymbol propertySymbol2 = null;
				object obj2;
				bool flag3 = namedTypeSymbol5.IsCustomTaskType(out obj2);
				NamedTypeSymbol namedTypeSymbol6;
				if (flag3)
				{
					namedTypeSymbol6 = AsyncMethodBuilderMemberCollection.ValidateBuilderType(F, obj2, namedTypeSymbol5.DeclaredAccessibility, true);
					if (namedTypeSymbol6 != null)
					{
						namedTypeSymbol6 = namedTypeSymbol6.ConstructedFrom.Construct(new TypeSymbol[] { typeSymbol });
						propertySymbol2 = AsyncMethodBuilderMemberCollection.GetCustomTaskProperty(F, namedTypeSymbol6, namedTypeSymbol5);
						methodSymbol4 = AsyncMethodBuilderMemberCollection.GetCustomCreateMethod(F, namedTypeSymbol6);
					}
				}
				else
				{
					namedTypeSymbol6 = F.WellKnownType(WellKnownType.System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T);
					namedTypeSymbol6 = namedTypeSymbol6.Construct(new TypeSymbol[] { typeSymbol });
					AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T__Create), namedTypeSymbol6, flag3, out methodSymbol4);
					AsyncMethodBuilderMemberCollection.TryGetBuilderMember<PropertySymbol>(F, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T__Task), namedTypeSymbol6, flag3, out propertySymbol2);
				}
				if (namedTypeSymbol6 == null || propertySymbol2 == null || methodSymbol4 == null)
				{
					collection = default(AsyncMethodBuilderMemberCollection);
					return false;
				}
				return AsyncMethodBuilderMemberCollection.TryCreate(F, flag3, namedTypeSymbol6, typeSymbol, methodSymbol4, propertySymbol2, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T__SetException), WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T__SetResult, WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T__AwaitOnCompleted, WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T__AwaitUnsafeOnCompleted, WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T__Start_T, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T__SetStateMachine), out collection);
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000AE9C0 File Offset: 0x000ACBC0
		private static NamedTypeSymbol ValidateBuilderType(SyntheticBoundNodeFactory F, object builderAttributeArgument, Accessibility desiredAccessibility, bool isGeneric)
		{
			NamedTypeSymbol namedTypeSymbol = builderAttributeArgument as NamedTypeSymbol;
			if (namedTypeSymbol != null && !namedTypeSymbol.IsErrorType() && !namedTypeSymbol.IsVoidType() && namedTypeSymbol.DeclaredAccessibility == desiredAccessibility)
			{
				bool flag;
				if (!isGeneric)
				{
					flag = !namedTypeSymbol.IsGenericType;
				}
				else
				{
					if (namedTypeSymbol.IsUnboundGenericType)
					{
						NamedTypeSymbol containingType = namedTypeSymbol.ContainingType;
						if (containingType == null || !containingType.IsGenericType)
						{
							flag = namedTypeSymbol.Arity == 1;
							goto IL_005C;
						}
					}
					flag = false;
				}
				IL_005C:
				if (flag)
				{
					return namedTypeSymbol;
				}
			}
			F.Diagnostics.Add(ErrorCode.ERR_BadAsyncReturn, F.Syntax.Location);
			return null;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000AEA4C File Offset: 0x000ACC4C
		private static bool TryCreate(SyntheticBoundNodeFactory F, bool customBuilder, NamedTypeSymbol builderType, TypeSymbol resultType, MethodSymbol createBuilderMethod, PropertySymbol taskProperty, WellKnownMember? setException, WellKnownMember setResult, WellKnownMember awaitOnCompleted, WellKnownMember awaitUnsafeOnCompleted, WellKnownMember start, WellKnownMember? setStateMachine, out AsyncMethodBuilderMemberCollection collection)
		{
			MethodSymbol methodSymbol;
			MethodSymbol methodSymbol2;
			MethodSymbol methodSymbol3;
			MethodSymbol methodSymbol4;
			MethodSymbol methodSymbol5;
			MethodSymbol methodSymbol6;
			if (AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, setException, builderType, customBuilder, out methodSymbol) && AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, new WellKnownMember?(setResult), builderType, customBuilder, out methodSymbol2) && AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, new WellKnownMember?(awaitOnCompleted), builderType, customBuilder, out methodSymbol3) && AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, new WellKnownMember?(awaitUnsafeOnCompleted), builderType, customBuilder, out methodSymbol4) && AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, new WellKnownMember?(start), builderType, customBuilder, out methodSymbol5) && AsyncMethodBuilderMemberCollection.TryGetBuilderMember<MethodSymbol>(F, setStateMachine, builderType, customBuilder, out methodSymbol6))
			{
				collection = new AsyncMethodBuilderMemberCollection(builderType, resultType, createBuilderMethod, methodSymbol, methodSymbol2, methodSymbol3, methodSymbol4, methodSymbol5, methodSymbol6, taskProperty, customBuilder);
				return true;
			}
			collection = default(AsyncMethodBuilderMemberCollection);
			return false;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000AEAE8 File Offset: 0x000ACCE8
		private static bool TryGetBuilderMember<TSymbol>(SyntheticBoundNodeFactory F, WellKnownMember? member, NamedTypeSymbol builderType, bool customBuilder, out TSymbol symbol) where TSymbol : Symbol
		{
			if (member == null)
			{
				symbol = default(TSymbol);
				return true;
			}
			WellKnownMember value = member.Value;
			if (customBuilder)
			{
				MemberDescriptor descriptor = WellKnownMembers.GetDescriptor(value);
				Symbol symbol2 = CSharpCompilation.GetRuntimeMember(builderType.OriginalDefinition, in descriptor, F.Compilation.WellKnownMemberSignatureComparer, null);
				if (symbol2 != null)
				{
					symbol2 = symbol2.SymbolAsMember(builderType);
				}
				symbol = symbol2 as TSymbol;
			}
			else
			{
				symbol = F.WellKnownMember(value, true) as TSymbol;
				if (symbol != null)
				{
					symbol = (TSymbol)((object)symbol.SymbolAsMember(builderType));
				}
			}
			if (symbol == null)
			{
				MemberDescriptor descriptor2 = WellKnownMembers.GetDescriptor(value);
				CSDiagnostic csdiagnostic = new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_MissingPredefinedMember, new object[]
				{
					customBuilder ? builderType : descriptor2.DeclaringTypeMetadataName,
					descriptor2.Name
				}), F.Syntax.Location, false);
				F.Diagnostics.Add(csdiagnostic);
				return false;
			}
			return true;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000AEBFC File Offset: 0x000ACDFC
		private static MethodSymbol GetCustomCreateMethod(SyntheticBoundNodeFactory F, NamedTypeSymbol builderType)
		{
			foreach (Symbol symbol in builderType.GetMembers("Create"))
			{
				if (symbol.Kind == SymbolKind.Method)
				{
					MethodSymbol methodSymbol = (MethodSymbol)symbol;
					if (methodSymbol.DeclaredAccessibility == Accessibility.Public && methodSymbol.IsStatic && methodSymbol.ParameterCount == 0 && !methodSymbol.IsGenericMethod && methodSymbol.ReturnType.Equals(builderType, TypeCompareKind.AllIgnoreOptions))
					{
						return methodSymbol;
					}
				}
			}
			F.Diagnostics.Add(ErrorCode.ERR_MissingPredefinedMember, F.Syntax.Location, new object[] { builderType, "Create" });
			return null;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000AECA4 File Offset: 0x000ACEA4
		private static PropertySymbol GetCustomTaskProperty(SyntheticBoundNodeFactory F, NamedTypeSymbol builderType, NamedTypeSymbol returnType)
		{
			foreach (Symbol symbol in builderType.GetMembers("Task"))
			{
				if (symbol.Kind == SymbolKind.Property)
				{
					PropertySymbol propertySymbol = (PropertySymbol)symbol;
					if (propertySymbol.DeclaredAccessibility == Accessibility.Public && !propertySymbol.IsStatic && propertySymbol.ParameterCount == 0)
					{
						if (!propertySymbol.Type.Equals(returnType, TypeCompareKind.AllIgnoreOptions))
						{
							CSDiagnostic csdiagnostic = new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_BadAsyncMethodBuilderTaskProperty, new object[] { builderType, returnType, propertySymbol.Type }), F.Syntax.Location, false);
							F.Diagnostics.Add(csdiagnostic);
							return null;
						}
						return propertySymbol;
					}
				}
			}
			CSDiagnostic csdiagnostic2 = new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_MissingPredefinedMember, new object[] { builderType, "Task" }), F.Syntax.Location, false);
			F.Diagnostics.Add(csdiagnostic2);
			return null;
		}

		// Token: 0x04001049 RID: 4169
		internal readonly NamedTypeSymbol BuilderType;

		// Token: 0x0400104A RID: 4170
		internal readonly TypeSymbol ResultType;

		// Token: 0x0400104B RID: 4171
		internal readonly MethodSymbol CreateBuilder;

		// Token: 0x0400104C RID: 4172
		internal readonly MethodSymbol SetException;

		// Token: 0x0400104D RID: 4173
		internal readonly MethodSymbol SetResult;

		// Token: 0x0400104E RID: 4174
		internal readonly MethodSymbol AwaitOnCompleted;

		// Token: 0x0400104F RID: 4175
		internal readonly MethodSymbol AwaitUnsafeOnCompleted;

		// Token: 0x04001050 RID: 4176
		internal readonly MethodSymbol Start;

		// Token: 0x04001051 RID: 4177
		internal readonly MethodSymbol SetStateMachine;

		// Token: 0x04001052 RID: 4178
		internal readonly PropertySymbol Task;

		// Token: 0x04001053 RID: 4179
		internal readonly bool CheckGenericMethodConstraints;
	}
}
