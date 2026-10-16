using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200033C RID: 828
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class DiagnosticQueue
	{
		// Token: 0x060023A5 RID: 9125
		public abstract bool TryComplete();

		// Token: 0x060023A6 RID: 9126
		[NullableContext(2)]
		public abstract bool TryDequeue([NotNullWhen(true)] out Diagnostic d);

		// Token: 0x060023A7 RID: 9127
		public abstract void Enqueue(Diagnostic diagnostic);

		// Token: 0x060023A8 RID: 9128
		public abstract void EnqueueLocal(Diagnostic diagnostic, DiagnosticAnalyzer analyzer, bool isSyntaxDiagnostic);

		// Token: 0x060023A9 RID: 9129
		public abstract void EnqueueNonLocal(Diagnostic diagnostic, DiagnosticAnalyzer analyzer);

		// Token: 0x060023AA RID: 9130
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> DequeueLocalSyntaxDiagnostics(DiagnosticAnalyzer analyzer);

		// Token: 0x060023AB RID: 9131
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> DequeueLocalSemanticDiagnostics(DiagnosticAnalyzer analyzer);

		// Token: 0x060023AC RID: 9132
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> DequeueNonLocalDiagnostics(DiagnosticAnalyzer analyzer);

		// Token: 0x060023AD RID: 9133 RVA: 0x0006FFB4 File Offset: 0x0006E1B4
		public static DiagnosticQueue Create(bool categorized = false)
		{
			if (!categorized)
			{
				return new DiagnosticQueue.SimpleDiagnosticQueue();
			}
			return new DiagnosticQueue.CategorizedDiagnosticQueue();
		}

		// Token: 0x02000713 RID: 1811
		[Nullable(0)]
		private sealed class SimpleDiagnosticQueue : DiagnosticQueue
		{
			// Token: 0x06003B3D RID: 15165 RVA: 0x000A9B45 File Offset: 0x000A7D45
			public SimpleDiagnosticQueue()
			{
				this._queue = new AsyncQueue<Diagnostic>();
			}

			// Token: 0x06003B3E RID: 15166 RVA: 0x000A9B58 File Offset: 0x000A7D58
			public SimpleDiagnosticQueue(Diagnostic diagnostic)
			{
				this._queue = new AsyncQueue<Diagnostic>();
				this._queue.Enqueue(diagnostic);
			}

			// Token: 0x06003B3F RID: 15167 RVA: 0x000A9B77 File Offset: 0x000A7D77
			public override void Enqueue(Diagnostic diagnostic)
			{
				this._queue.Enqueue(diagnostic);
			}

			// Token: 0x06003B40 RID: 15168 RVA: 0x000A9B85 File Offset: 0x000A7D85
			public override void EnqueueLocal(Diagnostic diagnostic, DiagnosticAnalyzer analyzer, bool isSyntaxDiagnostic)
			{
				this._queue.Enqueue(diagnostic);
			}

			// Token: 0x06003B41 RID: 15169 RVA: 0x000A9B93 File Offset: 0x000A7D93
			public override void EnqueueNonLocal(Diagnostic diagnostic, DiagnosticAnalyzer analyzer)
			{
				this._queue.Enqueue(diagnostic);
			}

			// Token: 0x06003B42 RID: 15170 RVA: 0x000A9BA1 File Offset: 0x000A7DA1
			[return: Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<Diagnostic> DequeueLocalSemanticDiagnostics(DiagnosticAnalyzer analyzer)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06003B43 RID: 15171 RVA: 0x000A9BA8 File Offset: 0x000A7DA8
			[return: Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<Diagnostic> DequeueLocalSyntaxDiagnostics(DiagnosticAnalyzer analyzer)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06003B44 RID: 15172 RVA: 0x000A9BAF File Offset: 0x000A7DAF
			[return: Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<Diagnostic> DequeueNonLocalDiagnostics(DiagnosticAnalyzer analyzer)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06003B45 RID: 15173 RVA: 0x000A9BB6 File Offset: 0x000A7DB6
			public override bool TryComplete()
			{
				return this._queue.TryComplete();
			}

			// Token: 0x06003B46 RID: 15174 RVA: 0x000A9BC3 File Offset: 0x000A7DC3
			[NullableContext(2)]
			public override bool TryDequeue([NotNullWhen(true)] out Diagnostic d)
			{
				return this._queue.TryDequeue(out d);
			}

			// Token: 0x04001C3B RID: 7227
			private readonly AsyncQueue<Diagnostic> _queue;
		}

		// Token: 0x02000714 RID: 1812
		[Nullable(0)]
		private sealed class CategorizedDiagnosticQueue : DiagnosticQueue
		{
			// Token: 0x06003B47 RID: 15175 RVA: 0x000A9BD1 File Offset: 0x000A7DD1
			public override void Enqueue(Diagnostic diagnostic)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06003B48 RID: 15176 RVA: 0x000A9BD8 File Offset: 0x000A7DD8
			public override void EnqueueLocal(Diagnostic diagnostic, DiagnosticAnalyzer analyzer, bool isSyntaxDiagnostic)
			{
				if (isSyntaxDiagnostic)
				{
					this.EnqueueCore(ref this._lazyLocalSyntaxDiagnostics, diagnostic, analyzer);
					return;
				}
				this.EnqueueCore(ref this._lazyLocalSemanticDiagnostics, diagnostic, analyzer);
			}

			// Token: 0x06003B49 RID: 15177 RVA: 0x000A9BFA File Offset: 0x000A7DFA
			public override void EnqueueNonLocal(Diagnostic diagnostic, DiagnosticAnalyzer analyzer)
			{
				this.EnqueueCore(ref this._lazyNonLocalDiagnostics, diagnostic, analyzer);
			}

			// Token: 0x06003B4A RID: 15178 RVA: 0x000A9C0C File Offset: 0x000A7E0C
			private void EnqueueCore([Nullable(new byte[] { 2, 1, 1 })] [NotNull] ref Dictionary<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue> lazyDiagnosticsMap, Diagnostic diagnostic, DiagnosticAnalyzer analyzer)
			{
				object gate = this._gate;
				lock (gate)
				{
					if (lazyDiagnosticsMap == null)
					{
						lazyDiagnosticsMap = new Dictionary<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue>();
					}
					DiagnosticQueue.CategorizedDiagnosticQueue.EnqueueCore_NoLock(lazyDiagnosticsMap, diagnostic, analyzer);
				}
			}

			// Token: 0x06003B4B RID: 15179 RVA: 0x000A9C5C File Offset: 0x000A7E5C
			private static void EnqueueCore_NoLock(Dictionary<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue> diagnosticsMap, Diagnostic diagnostic, DiagnosticAnalyzer analyzer)
			{
				DiagnosticQueue.SimpleDiagnosticQueue simpleDiagnosticQueue;
				if (diagnosticsMap.TryGetValue(analyzer, ref simpleDiagnosticQueue))
				{
					simpleDiagnosticQueue.Enqueue(diagnostic);
					return;
				}
				diagnosticsMap[analyzer] = new DiagnosticQueue.SimpleDiagnosticQueue(diagnostic);
			}

			// Token: 0x06003B4C RID: 15180 RVA: 0x000A9C89 File Offset: 0x000A7E89
			public override bool TryComplete()
			{
				return true;
			}

			// Token: 0x06003B4D RID: 15181 RVA: 0x000A9C8C File Offset: 0x000A7E8C
			[NullableContext(2)]
			public override bool TryDequeue([NotNullWhen(true)] out Diagnostic d)
			{
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					flag2 = this.TryDequeue_NoLock(out d);
				}
				return flag2;
			}

			// Token: 0x06003B4E RID: 15182 RVA: 0x000A9CD0 File Offset: 0x000A7ED0
			[NullableContext(2)]
			private bool TryDequeue_NoLock([NotNullWhen(true)] out Diagnostic d)
			{
				return DiagnosticQueue.CategorizedDiagnosticQueue.TryDequeue_NoLock(this._lazyLocalSemanticDiagnostics, out d) || DiagnosticQueue.CategorizedDiagnosticQueue.TryDequeue_NoLock(this._lazyLocalSyntaxDiagnostics, out d) || DiagnosticQueue.CategorizedDiagnosticQueue.TryDequeue_NoLock(this._lazyNonLocalDiagnostics, out d);
			}

			// Token: 0x06003B4F RID: 15183 RVA: 0x000A9CFC File Offset: 0x000A7EFC
			[NullableContext(2)]
			private static bool TryDequeue_NoLock([Nullable(new byte[] { 2, 1, 1 })] Dictionary<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue> lazyDiagnosticsMap, [NotNullWhen(true)] out Diagnostic d)
			{
				Diagnostic diag = null;
				if (lazyDiagnosticsMap != null && Enumerable.Any<KeyValuePair<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue>>(lazyDiagnosticsMap, (KeyValuePair<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue> kvp) => kvp.Value.TryDequeue(out diag)))
				{
					d = diag;
					return true;
				}
				d = null;
				return false;
			}

			// Token: 0x06003B50 RID: 15184 RVA: 0x000A9D3B File Offset: 0x000A7F3B
			[return: Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<Diagnostic> DequeueLocalSyntaxDiagnostics(DiagnosticAnalyzer analyzer)
			{
				return this.DequeueDiagnosticsCore(analyzer, this._lazyLocalSyntaxDiagnostics);
			}

			// Token: 0x06003B51 RID: 15185 RVA: 0x000A9D4A File Offset: 0x000A7F4A
			[return: Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<Diagnostic> DequeueLocalSemanticDiagnostics(DiagnosticAnalyzer analyzer)
			{
				return this.DequeueDiagnosticsCore(analyzer, this._lazyLocalSemanticDiagnostics);
			}

			// Token: 0x06003B52 RID: 15186 RVA: 0x000A9D59 File Offset: 0x000A7F59
			[return: Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<Diagnostic> DequeueNonLocalDiagnostics(DiagnosticAnalyzer analyzer)
			{
				return this.DequeueDiagnosticsCore(analyzer, this._lazyNonLocalDiagnostics);
			}

			// Token: 0x06003B53 RID: 15187 RVA: 0x000A9D68 File Offset: 0x000A7F68
			[return: Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<Diagnostic> DequeueDiagnosticsCore(DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue> lazyDiagnosticsMap)
			{
				DiagnosticQueue.SimpleDiagnosticQueue simpleDiagnosticQueue;
				if (this.TryGetDiagnosticsQueue(analyzer, lazyDiagnosticsMap, out simpleDiagnosticQueue))
				{
					ImmutableArray<Diagnostic>.Builder builder = ImmutableArray.CreateBuilder<Diagnostic>();
					Diagnostic diagnostic;
					while (simpleDiagnosticQueue.TryDequeue(out diagnostic))
					{
						builder.Add(diagnostic);
					}
					return builder.ToImmutable();
				}
				return ImmutableArray<Diagnostic>.Empty;
			}

			// Token: 0x06003B54 RID: 15188 RVA: 0x000A9DA8 File Offset: 0x000A7FA8
			private bool TryGetDiagnosticsQueue(DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue> diagnosticsMap, [Nullable(2)] [NotNullWhen(true)] out DiagnosticQueue.SimpleDiagnosticQueue queue)
			{
				queue = null;
				object gate = this._gate;
				bool flag2;
				lock (gate)
				{
					flag2 = diagnosticsMap != null && diagnosticsMap.TryGetValue(analyzer, ref queue);
				}
				return flag2;
			}

			// Token: 0x04001C3C RID: 7228
			private readonly object _gate = new object();

			// Token: 0x04001C3D RID: 7229
			[Nullable(new byte[] { 2, 1, 1 })]
			private Dictionary<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue> _lazyLocalSemanticDiagnostics;

			// Token: 0x04001C3E RID: 7230
			[Nullable(new byte[] { 2, 1, 1 })]
			private Dictionary<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue> _lazyLocalSyntaxDiagnostics;

			// Token: 0x04001C3F RID: 7231
			[Nullable(new byte[] { 2, 1, 1 })]
			private Dictionary<DiagnosticAnalyzer, DiagnosticQueue.SimpleDiagnosticQueue> _lazyNonLocalDiagnostics;
		}
	}
}
