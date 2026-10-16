using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000156 RID: 342
	internal class FlowAnalysisPass
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x00091358 File Offset: 0x0008F558
		public static BoundBlock Rewrite(MethodSymbol method, BoundBlock block, DiagnosticBag diagnostics, bool hasTrailingExpression, bool originalBodyNested)
		{
			CSharpCompilation declaringCompilation = method.DeclaringCompilation;
			if (method.ReturnsVoid || method.IsIterator || method.IsAsyncReturningTask(declaringCompilation))
			{
				if ((method.IsImplicitlyDeclared && !method.IsScriptInitializer) || FlowAnalysisPass.Analyze(declaringCompilation, method, block, diagnostics))
				{
					block = FlowAnalysisPass.AppendImplicitReturn(block, method, originalBodyNested);
				}
			}
			else if (FlowAnalysisPass.Analyze(declaringCompilation, method, block, diagnostics))
			{
				SynthesizedInteractiveInitializerMethod synthesizedInteractiveInitializerMethod = method as SynthesizedInteractiveInitializerMethod;
				TypeSymbol typeSymbol = ((synthesizedInteractiveInitializerMethod != null) ? synthesizedInteractiveInitializerMethod.ResultType : null);
				if (!hasTrailingExpression && typeSymbol != null)
				{
					BoundDefaultExpression boundDefaultExpression = new BoundDefaultExpression(method.GetNonNullSyntaxNode(), typeSymbol, false);
					ImmutableArray<BoundStatement> immutableArray = block.Statements.Add(new BoundReturnStatement(boundDefaultExpression.Syntax, RefKind.None, boundDefaultExpression, false));
					block = new BoundBlock(block.Syntax, ImmutableArray<LocalSymbol>.Empty, immutableArray, false)
					{
						WasCompilerGenerated = true
					};
				}
				else if (method.Locations.Length == 1)
				{
					diagnostics.Add(ErrorCode.ERR_ReturnExpected, method.Locations[0], new object[] { method });
				}
			}
			return block;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0009145C File Offset: 0x0008F65C
		private static BoundBlock AppendImplicitReturn(BoundBlock body, MethodSymbol method, bool originalBodyNested)
		{
			if (originalBodyNested)
			{
				ImmutableArray<BoundStatement> statements = body.Statements;
				int length = statements.Length;
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance(length);
				instance.AddRange(statements, length - 1);
				instance.Add(FlowAnalysisPass.AppendImplicitReturn((BoundBlock)statements[length - 1], method));
				return body.Update(body.Locals, ImmutableArray<LocalFunctionSymbol>.Empty, instance.ToImmutableAndFree());
			}
			return FlowAnalysisPass.AppendImplicitReturn(body, method);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000914C8 File Offset: 0x0008F6C8
		internal static BoundBlock AppendImplicitReturn(BoundBlock body, MethodSymbol method)
		{
			SyntaxNode syntax = body.Syntax;
			BoundStatement boundStatement = ((method.IsIterator && !method.IsAsync) ? BoundYieldBreakStatement.Synthesized(syntax, false) : BoundReturnStatement.Synthesized(syntax, RefKind.None, null, false));
			return body.Update(body.Locals, body.LocalFunctions, body.Statements.Add(boundStatement));
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00091520 File Offset: 0x0008F720
		private static bool Analyze(CSharpCompilation compilation, MethodSymbol method, BoundBlock block, DiagnosticBag diagnostics)
		{
			bool flag = ControlFlowPass.Analyze(compilation, method, block, diagnostics);
			DefiniteAssignmentPass.Analyze(compilation, method, block, diagnostics, true);
			return flag;
		}
	}
}
