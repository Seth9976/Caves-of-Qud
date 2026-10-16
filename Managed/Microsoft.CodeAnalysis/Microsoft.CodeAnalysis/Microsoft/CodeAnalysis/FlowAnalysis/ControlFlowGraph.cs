using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002DC RID: 732
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ControlFlowGraph
	{
		// Token: 0x06001E4D RID: 7757 RVA: 0x00057454 File Offset: 0x00055654
		internal ControlFlowGraph(IOperation originalOperation, [Nullable(2)] ControlFlowGraph parent, ControlFlowGraphBuilder.CaptureIdDispenser captureIdDispenser, [Nullable(new byte[] { 0, 1 })] ImmutableArray<BasicBlock> blocks, ControlFlowRegion root, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IMethodSymbol> localFunctions, [TupleElementNames(new string[] { "region", "operation", "ordinal" })] [Nullable(new byte[] { 1, 1, 0, 1, 1 })] ImmutableDictionary<IMethodSymbol, ValueTuple<ControlFlowRegion, ILocalFunctionOperation, int>> localFunctionsMap, [TupleElementNames(new string[] { "region", "ordinal" })] [Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>> anonymousFunctionsMap)
		{
			this.OriginalOperation = originalOperation;
			this.Parent = parent;
			this.Blocks = blocks;
			this.Root = root;
			this.LocalFunctions = localFunctions;
			this._localFunctionsMap = localFunctionsMap;
			this._anonymousFunctionsMap = anonymousFunctionsMap;
			this._captureIdDispenser = captureIdDispenser;
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000574A4 File Offset: 0x000556A4
		[return: Nullable(2)]
		public static ControlFlowGraph Create(SyntaxNode node, SemanticModel semanticModel, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (semanticModel == null)
			{
				throw new ArgumentNullException("semanticModel");
			}
			IOperation operation = semanticModel.GetOperation(node, cancellationToken);
			cancellationToken.ThrowIfCancellationRequested();
			if (operation != null)
			{
				return ControlFlowGraph.CreateCore(operation, "operation", cancellationToken);
			}
			return null;
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x000574EE File Offset: 0x000556EE
		public static ControlFlowGraph Create(IBlockOperation body, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ControlFlowGraph.CreateCore(body, "body", cancellationToken);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x000574FC File Offset: 0x000556FC
		public static ControlFlowGraph Create(IFieldInitializerOperation initializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ControlFlowGraph.CreateCore(initializer, "initializer", cancellationToken);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0005750A File Offset: 0x0005570A
		public static ControlFlowGraph Create(IPropertyInitializerOperation initializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ControlFlowGraph.CreateCore(initializer, "initializer", cancellationToken);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00057518 File Offset: 0x00055718
		public static ControlFlowGraph Create(IParameterInitializerOperation initializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ControlFlowGraph.CreateCore(initializer, "initializer", cancellationToken);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00057526 File Offset: 0x00055726
		public static ControlFlowGraph Create(IConstructorBodyOperation constructorBody, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ControlFlowGraph.CreateCore(constructorBody, "constructorBody", cancellationToken);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00057534 File Offset: 0x00055734
		public static ControlFlowGraph Create(IMethodBodyOperation methodBody, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ControlFlowGraph.CreateCore(methodBody, "methodBody", cancellationToken);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00057544 File Offset: 0x00055744
		internal static ControlFlowGraph CreateCore(IOperation operation, string argumentNameForException, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (operation == null)
			{
				throw new ArgumentNullException(argumentNameForException);
			}
			if (operation.Parent != null)
			{
				throw new ArgumentException(CodeAnalysisResources.NotARootOperation, argumentNameForException);
			}
			if (((Operation)operation).OwningSemanticModel == null)
			{
				throw new ArgumentException(CodeAnalysisResources.OperationHasNullSemanticModel, argumentNameForException);
			}
			try
			{
				ControlFlowGraph controlFlowGraph = null;
				ControlFlowRegion controlFlowRegion = null;
				ControlFlowGraphBuilder.CaptureIdDispenser captureIdDispenser = null;
				ControlFlowGraphBuilder.Context context = default(ControlFlowGraphBuilder.Context);
				return ControlFlowGraphBuilder.Create(operation, controlFlowGraph, controlFlowRegion, captureIdDispenser, in context);
			}
			catch (Exception ex) when (FatalError.ReportAndCatchUnlessCanceled(ex, cancellationToken))
			{
			}
			return null;
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000575D4 File Offset: 0x000557D4
		public IOperation OriginalOperation { get; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x000575DC File Offset: 0x000557DC
		[Nullable(2)]
		public ControlFlowGraph Parent
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000575E4 File Offset: 0x000557E4
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BasicBlock> Blocks
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x000575EC File Offset: 0x000557EC
		public ControlFlowRegion Root { get; }

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x000575F4 File Offset: 0x000557F4
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IMethodSymbol> LocalFunctions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000575FC File Offset: 0x000557FC
		public ControlFlowGraph GetLocalFunctionControlFlowGraph(IMethodSymbol localFunction, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (localFunction == null)
			{
				throw new ArgumentNullException("localFunction");
			}
			ControlFlowGraph controlFlowGraph;
			if (!this.TryGetLocalFunctionControlFlowGraph(localFunction, cancellationToken, out controlFlowGraph))
			{
				throw new ArgumentOutOfRangeException("localFunction");
			}
			return controlFlowGraph;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00057638 File Offset: 0x00055838
		internal bool TryGetLocalFunctionControlFlowGraph(IMethodSymbol localFunction, CancellationToken cancellationToken, [Nullable(2)] [NotNullWhen(true)] out ControlFlowGraph controlFlowGraph)
		{
			ValueTuple<ControlFlowRegion, ILocalFunctionOperation, int> valueTuple;
			if (!this._localFunctionsMap.TryGetValue(localFunction, out valueTuple))
			{
				controlFlowGraph = null;
				return false;
			}
			if (this._lazyLocalFunctionsGraphs == null)
			{
				Interlocked.CompareExchange<ControlFlowGraph[]>(ref this._lazyLocalFunctionsGraphs, new ControlFlowGraph[this.LocalFunctions.Length], null);
			}
			ref ControlFlowGraph ptr = ref this._lazyLocalFunctionsGraphs[valueTuple.Item3];
			if (ptr == null)
			{
				IOperation item = valueTuple.Item2;
				ControlFlowRegion item2 = valueTuple.Item1;
				ControlFlowGraphBuilder.CaptureIdDispenser captureIdDispenser = this._captureIdDispenser;
				ControlFlowGraphBuilder.Context context = default(ControlFlowGraphBuilder.Context);
				ControlFlowGraph controlFlowGraph2 = ControlFlowGraphBuilder.Create(item, this, item2, captureIdDispenser, in context);
				Interlocked.CompareExchange<ControlFlowGraph>(ref ptr, controlFlowGraph2, null);
			}
			controlFlowGraph = ptr;
			return true;
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000576C8 File Offset: 0x000558C8
		public ControlFlowGraph GetAnonymousFunctionControlFlowGraph(IFlowAnonymousFunctionOperation anonymousFunction, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (anonymousFunction == null)
			{
				throw new ArgumentNullException("anonymousFunction");
			}
			ControlFlowGraph controlFlowGraph;
			if (!this.TryGetAnonymousFunctionControlFlowGraph(anonymousFunction, cancellationToken, out controlFlowGraph))
			{
				throw new ArgumentOutOfRangeException("anonymousFunction");
			}
			return controlFlowGraph;
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00057704 File Offset: 0x00055904
		internal bool TryGetAnonymousFunctionControlFlowGraph(IFlowAnonymousFunctionOperation anonymousFunction, CancellationToken cancellationToken, [Nullable(2)] [NotNullWhen(true)] out ControlFlowGraph controlFlowGraph)
		{
			ValueTuple<ControlFlowRegion, int> valueTuple;
			if (!this._anonymousFunctionsMap.TryGetValue(anonymousFunction, out valueTuple))
			{
				controlFlowGraph = null;
				return false;
			}
			if (this._lazyAnonymousFunctionsGraphs == null)
			{
				Interlocked.CompareExchange<ControlFlowGraph[]>(ref this._lazyAnonymousFunctionsGraphs, new ControlFlowGraph[this._anonymousFunctionsMap.Count], null);
			}
			ref ControlFlowGraph ptr = ref this._lazyAnonymousFunctionsGraphs[valueTuple.Item2];
			if (ptr == null)
			{
				FlowAnonymousFunctionOperation flowAnonymousFunctionOperation = (FlowAnonymousFunctionOperation)anonymousFunction;
				ControlFlowGraph controlFlowGraph2 = ControlFlowGraphBuilder.Create(flowAnonymousFunctionOperation.Original, this, valueTuple.Item1, this._captureIdDispenser, in flowAnonymousFunctionOperation.Context);
				Interlocked.CompareExchange<ControlFlowGraph>(ref ptr, controlFlowGraph2, null);
			}
			controlFlowGraph = ptr;
			return true;
		}

		// Token: 0x04000DAD RID: 3501
		private readonly ControlFlowGraphBuilder.CaptureIdDispenser _captureIdDispenser;

		// Token: 0x04000DAE RID: 3502
		[TupleElementNames(new string[] { "region", "operation", "ordinal" })]
		[Nullable(new byte[] { 1, 1, 0, 1, 1 })]
		private readonly ImmutableDictionary<IMethodSymbol, ValueTuple<ControlFlowRegion, ILocalFunctionOperation, int>> _localFunctionsMap;

		// Token: 0x04000DAF RID: 3503
		[Nullable(2)]
		private ControlFlowGraph[] _lazyLocalFunctionsGraphs;

		// Token: 0x04000DB0 RID: 3504
		[TupleElementNames(new string[] { "region", "ordinal" })]
		[Nullable(new byte[] { 1, 1, 0, 1 })]
		private readonly ImmutableDictionary<IFlowAnonymousFunctionOperation, ValueTuple<ControlFlowRegion, int>> _anonymousFunctionsMap;

		// Token: 0x04000DB1 RID: 3505
		[Nullable(2)]
		private ControlFlowGraph[] _lazyAnonymousFunctionsGraphs;
	}
}
