using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000329 RID: 809
	internal static class DiagnosticAnalysisContextHelpers
	{
		// Token: 0x0600236C RID: 9068 RVA: 0x0006FAD8 File Offset: 0x0006DCD8
		[NullableContext(1)]
		internal static void VerifyArguments<[Nullable(2)] TContext>(Action<TContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyAction<TContext>(action);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0006FAE0 File Offset: 0x0006DCE0
		internal static void VerifyArguments<[Nullable(2)] TContext>([Nullable(1)] Action<TContext> action, ImmutableArray<SymbolKind> symbolKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyAction<TContext>(action);
			DiagnosticAnalysisContextHelpers.VerifySymbolKinds(symbolKinds);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0006FAEE File Offset: 0x0006DCEE
		internal static void VerifyArguments<[Nullable(2)] TContext, TLanguageKindEnum>([Nullable(1)] Action<TContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds) where TLanguageKindEnum : struct
		{
			DiagnosticAnalysisContextHelpers.VerifyAction<TContext>(action);
			DiagnosticAnalysisContextHelpers.VerifySyntaxKinds<TLanguageKindEnum>(syntaxKinds);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x0006FAFC File Offset: 0x0006DCFC
		internal static void VerifyArguments<[Nullable(2)] TContext>([Nullable(1)] Action<TContext> action, ImmutableArray<OperationKind> operationKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyAction<TContext>(action);
			DiagnosticAnalysisContextHelpers.VerifyOperationKinds(operationKinds);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0006FB0C File Offset: 0x0006DD0C
		[NullableContext(1)]
		internal static void VerifyArguments(Diagnostic diagnostic, [Nullable(2)] Compilation compilation, Func<Diagnostic, bool> isSupportedDiagnostic)
		{
			if (diagnostic is DiagnosticWithInfo)
			{
				return;
			}
			if (diagnostic == null)
			{
				throw new ArgumentNullException("diagnostic");
			}
			if (compilation != null)
			{
				DiagnosticAnalysisContextHelpers.VerifyDiagnosticLocationsInCompilation(diagnostic, compilation);
			}
			if (!isSupportedDiagnostic.Invoke(diagnostic))
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.UnsupportedDiagnosticReported, diagnostic.Id), "diagnostic");
			}
			if (!UnicodeCharacterUtilities.IsValidIdentifier(diagnostic.Id))
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.InvalidDiagnosticIdReported, diagnostic.Id), "diagnostic");
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0006FB88 File Offset: 0x0006DD88
		[NullableContext(1)]
		internal static void VerifyDiagnosticLocationsInCompilation(Diagnostic diagnostic, Compilation compilation)
		{
			DiagnosticAnalysisContextHelpers.VerifyDiagnosticLocationInCompilation(diagnostic.Id, diagnostic.Location, compilation);
			if (diagnostic.AdditionalLocations != null)
			{
				foreach (Location location in diagnostic.AdditionalLocations)
				{
					DiagnosticAnalysisContextHelpers.VerifyDiagnosticLocationInCompilation(diagnostic.Id, diagnostic.Location, compilation);
				}
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0006FBFC File Offset: 0x0006DDFC
		[NullableContext(1)]
		private static void VerifyDiagnosticLocationInCompilation(string id, Location location, Compilation compilation)
		{
			if (!location.IsInSource)
			{
				return;
			}
			if (!compilation.ContainsSyntaxTree(location.SourceTree))
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.InvalidDiagnosticLocationReported, id, location.SourceTree.FilePath), "diagnostic");
			}
			if (location.SourceSpan.End > location.SourceTree.Length)
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.InvalidDiagnosticSpanReported, id, location.SourceSpan, location.SourceTree.FilePath), "diagnostic");
			}
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0006FC88 File Offset: 0x0006DE88
		[NullableContext(1)]
		private static void VerifyAction<[Nullable(2)] TContext>(Action<TContext> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0006FC98 File Offset: 0x0006DE98
		private static void VerifySymbolKinds(ImmutableArray<SymbolKind> symbolKinds)
		{
			if (symbolKinds.IsDefault)
			{
				throw new ArgumentNullException("symbolKinds");
			}
			if (symbolKinds.IsEmpty)
			{
				throw new ArgumentException(CodeAnalysisResources.ArgumentCannotBeEmpty, "symbolKinds");
			}
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0006FCC7 File Offset: 0x0006DEC7
		private static void VerifySyntaxKinds<TLanguageKindEnum>(ImmutableArray<TLanguageKindEnum> syntaxKinds) where TLanguageKindEnum : struct
		{
			if (syntaxKinds.IsDefault)
			{
				throw new ArgumentNullException("syntaxKinds");
			}
			if (syntaxKinds.IsEmpty)
			{
				throw new ArgumentException(CodeAnalysisResources.ArgumentCannotBeEmpty, "syntaxKinds");
			}
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0006FCF6 File Offset: 0x0006DEF6
		private static void VerifyOperationKinds(ImmutableArray<OperationKind> operationKinds)
		{
			if (operationKinds.IsDefault)
			{
				throw new ArgumentNullException("operationKinds");
			}
			if (operationKinds.IsEmpty)
			{
				throw new ArgumentException(CodeAnalysisResources.ArgumentCannotBeEmpty, "operationKinds");
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0006FD25 File Offset: 0x0006DF25
		[NullableContext(1)]
		internal static void VerifyArguments<TKey, [Nullable(2)] TValue>(TKey key, AnalysisValueProvider<TKey, TValue> valueProvider) where TKey : class
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (valueProvider == null)
			{
				throw new ArgumentNullException("valueProvider");
			}
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0006FD48 File Offset: 0x0006DF48
		[NullableContext(1)]
		internal static ControlFlowGraph GetControlFlowGraph(IOperation operation, [Nullable(new byte[] { 2, 1, 1 })] Func<IOperation, ControlFlowGraph> getControlFlowGraph, CancellationToken cancellationToken)
		{
			IOperation rootOperation = operation.GetRootOperation();
			if (getControlFlowGraph == null)
			{
				return ControlFlowGraph.CreateCore(rootOperation, "rootOperation", cancellationToken);
			}
			return getControlFlowGraph.Invoke(rootOperation);
		}
	}
}
