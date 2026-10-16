using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000467 RID: 1127
	[NullableContext(1)]
	[Nullable(0)]
	public static class OperationExtensions
	{
		// Token: 0x06002C27 RID: 11303 RVA: 0x0007CDF4 File Offset: 0x0007AFF4
		internal static bool HasErrors(this IOperation operation, Compilation compilation, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (operation == null)
			{
				throw new ArgumentNullException("operation");
			}
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			if (operation.Syntax == null)
			{
				return true;
			}
			SemanticModel semanticModel = operation.SemanticModel;
			if (semanticModel == null || semanticModel.SyntaxTree != operation.Syntax.SyntaxTree)
			{
				semanticModel = compilation.GetSemanticModel(operation.Syntax.SyntaxTree, false);
			}
			if (semanticModel.IsSpeculativeSemanticModel)
			{
				return false;
			}
			return semanticModel.GetDiagnostics(new TextSpan?(operation.Syntax.Span), cancellationToken).Any<Diagnostic>((Diagnostic d) => d.DefaultSeverity == DiagnosticSeverity.Error);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x0007CE9C File Offset: 0x0007B09C
		public static IEnumerable<IOperation> Descendants([Nullable(2)] this IOperation operation)
		{
			return OperationExtensions.Descendants(operation, false);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x0007CEA5 File Offset: 0x0007B0A5
		public static IEnumerable<IOperation> DescendantsAndSelf([Nullable(2)] this IOperation operation)
		{
			return OperationExtensions.Descendants(operation, true);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x0007CEAE File Offset: 0x0007B0AE
		private static IEnumerable<IOperation> Descendants([Nullable(2)] IOperation operation, bool includeSelf)
		{
			if (operation == null)
			{
				yield break;
			}
			if (includeSelf)
			{
				yield return operation;
			}
			ArrayBuilder<Operation.Enumerator> stack = ArrayBuilder<Operation.Enumerator>.GetInstance();
			stack.Push(((Operation)operation).ChildOperations.GetEnumerator());
			while (stack.Any())
			{
				Operation.Enumerator enumerator = stack.Pop<Operation.Enumerator>();
				if (enumerator.MoveNext())
				{
					IOperation current = enumerator.Current;
					stack.Push(enumerator);
					if (current != null)
					{
						yield return current;
						stack.Push(((Operation)current).ChildOperations.GetEnumerator());
					}
					current = null;
				}
			}
			stack.Free();
			yield break;
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0007CEC8 File Offset: 0x0007B0C8
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ILocalSymbol> GetDeclaredVariables(this IVariableDeclarationGroupOperation declarationGroup)
		{
			if (declarationGroup == null)
			{
				throw new ArgumentNullException("declarationGroup");
			}
			ArrayBuilder<ILocalSymbol> instance = ArrayBuilder<ILocalSymbol>.GetInstance();
			foreach (IVariableDeclarationOperation variableDeclarationOperation in declarationGroup.Declarations)
			{
				variableDeclarationOperation.GetDeclaredVariables(instance);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x0007CF18 File Offset: 0x0007B118
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ILocalSymbol> GetDeclaredVariables(this IVariableDeclarationOperation declaration)
		{
			if (declaration == null)
			{
				throw new ArgumentNullException("declaration");
			}
			ArrayBuilder<ILocalSymbol> instance = ArrayBuilder<ILocalSymbol>.GetInstance();
			declaration.GetDeclaredVariables(instance);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0007CF48 File Offset: 0x0007B148
		private static void GetDeclaredVariables(this IVariableDeclarationOperation declaration, ArrayBuilder<ILocalSymbol> arrayBuilder)
		{
			foreach (IVariableDeclaratorOperation variableDeclaratorOperation in declaration.Declarators)
			{
				arrayBuilder.Add(variableDeclaratorOperation.Symbol);
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x0007CF83 File Offset: 0x0007B183
		[return: Nullable(2)]
		public static IVariableInitializerOperation GetVariableInitializer(this IVariableDeclaratorOperation declarationOperation)
		{
			if (declarationOperation == null)
			{
				throw new ArgumentNullException("declarationOperation");
			}
			IVariableInitializerOperation variableInitializerOperation;
			if ((variableInitializerOperation = declarationOperation.Initializer) == null)
			{
				IVariableDeclarationOperation variableDeclarationOperation = declarationOperation.Parent as IVariableDeclarationOperation;
				if (variableDeclarationOperation == null)
				{
					return null;
				}
				variableInitializerOperation = variableDeclarationOperation.Initializer;
			}
			return variableInitializerOperation;
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x0007CFB3 File Offset: 0x0007B1B3
		[return: Nullable(2)]
		public static string GetArgumentName(this IDynamicInvocationOperation dynamicOperation, int index)
		{
			if (dynamicOperation == null)
			{
				throw new ArgumentNullException("dynamicOperation");
			}
			return ((HasDynamicArgumentsExpression)dynamicOperation).GetArgumentName(index);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0007CFCF File Offset: 0x0007B1CF
		[return: Nullable(2)]
		public static string GetArgumentName(this IDynamicIndexerAccessOperation dynamicOperation, int index)
		{
			if (dynamicOperation == null)
			{
				throw new ArgumentNullException("dynamicOperation");
			}
			return ((HasDynamicArgumentsExpression)dynamicOperation).GetArgumentName(index);
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x0007CFEB File Offset: 0x0007B1EB
		[return: Nullable(2)]
		public static string GetArgumentName(this IDynamicObjectCreationOperation dynamicOperation, int index)
		{
			if (dynamicOperation == null)
			{
				throw new ArgumentNullException("dynamicOperation");
			}
			return ((HasDynamicArgumentsExpression)dynamicOperation).GetArgumentName(index);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x0007D008 File Offset: 0x0007B208
		[return: Nullable(2)]
		internal static string GetArgumentName(this HasDynamicArgumentsExpression dynamicOperation, int index)
		{
			if (dynamicOperation.Arguments.IsDefaultOrEmpty)
			{
				throw new InvalidOperationException();
			}
			if (index < 0 || index >= dynamicOperation.Arguments.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			ImmutableArray<string> argumentNames = dynamicOperation.ArgumentNames;
			if (!argumentNames.IsDefaultOrEmpty)
			{
				return argumentNames[index];
			}
			return null;
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x0007D065 File Offset: 0x0007B265
		public static RefKind? GetArgumentRefKind(this IDynamicInvocationOperation dynamicOperation, int index)
		{
			if (dynamicOperation == null)
			{
				throw new ArgumentNullException("dynamicOperation");
			}
			return ((HasDynamicArgumentsExpression)dynamicOperation).GetArgumentRefKind(index);
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x0007D081 File Offset: 0x0007B281
		public static RefKind? GetArgumentRefKind(this IDynamicIndexerAccessOperation dynamicOperation, int index)
		{
			if (dynamicOperation == null)
			{
				throw new ArgumentNullException("dynamicOperation");
			}
			return ((HasDynamicArgumentsExpression)dynamicOperation).GetArgumentRefKind(index);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x0007D09D File Offset: 0x0007B29D
		public static RefKind? GetArgumentRefKind(this IDynamicObjectCreationOperation dynamicOperation, int index)
		{
			if (dynamicOperation == null)
			{
				throw new ArgumentNullException("dynamicOperation");
			}
			return ((HasDynamicArgumentsExpression)dynamicOperation).GetArgumentRefKind(index);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x0007D0BC File Offset: 0x0007B2BC
		internal static RefKind? GetArgumentRefKind(this HasDynamicArgumentsExpression dynamicOperation, int index)
		{
			if (dynamicOperation.Arguments.IsDefaultOrEmpty)
			{
				throw new InvalidOperationException();
			}
			if (index < 0 || index >= dynamicOperation.Arguments.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			ImmutableArray<RefKind> argumentRefKinds = dynamicOperation.ArgumentRefKinds;
			if (argumentRefKinds.IsDefault)
			{
				return default(RefKind?);
			}
			if (argumentRefKinds.IsEmpty)
			{
				return new RefKind?(RefKind.None);
			}
			return new RefKind?(argumentRefKinds[index]);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x0007D136 File Offset: 0x0007B336
		internal static IOperation GetRootOperation(this IOperation operation)
		{
			while (operation.Parent != null)
			{
				operation = operation.Parent;
			}
			return operation;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x0007D14C File Offset: 0x0007B34C
		[return: Nullable(2)]
		public static IOperation GetCorrespondingOperation(this IBranchOperation operation)
		{
			if (operation == null)
			{
				throw new ArgumentNullException("operation");
			}
			if (operation.SemanticModel == null)
			{
				throw new InvalidOperationException(CodeAnalysisResources.OperationMustNotBeControlFlowGraphPart);
			}
			if (operation.BranchKind != BranchKind.Break && operation.BranchKind != BranchKind.Continue)
			{
				return null;
			}
			if (operation.Target == null)
			{
				return null;
			}
			IOperation operation2 = operation;
			while (operation2.Parent != null)
			{
				IOperation operation3 = operation2;
				ILoopOperation loopOperation = operation3 as ILoopOperation;
				if (loopOperation != null && (operation.Target.Equals(loopOperation.ExitLabel) || operation.Target.Equals(loopOperation.ContinueLabel)))
				{
					return loopOperation;
				}
				ISwitchOperation switchOperation = operation3 as ISwitchOperation;
				if (switchOperation != null)
				{
					if (operation.Target.Equals(switchOperation.ExitLabel))
					{
						return switchOperation;
					}
				}
				operation2 = operation2.Parent;
			}
			return null;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x0007D201 File Offset: 0x0007B401
		[return: Nullable(2)]
		internal static ConstantValue GetConstantValue(this IOperation operation)
		{
			return ((Operation)operation).OperationConstantValue;
		}
	}
}
