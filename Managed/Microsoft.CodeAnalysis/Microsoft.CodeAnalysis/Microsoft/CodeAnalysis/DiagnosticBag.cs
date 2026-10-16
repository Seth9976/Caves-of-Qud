using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000170 RID: 368
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	[DebuggerTypeProxy(typeof(DiagnosticBag.DebuggerProxy))]
	internal class DiagnosticBag
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0002CDC8 File Offset: 0x0002AFC8
		public bool IsEmptyWithoutResolution
		{
			get
			{
				ConcurrentQueue<Diagnostic> lazyBag = this._lazyBag;
				return lazyBag == null || lazyBag.IsEmpty;
			}
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0002CDE8 File Offset: 0x0002AFE8
		public bool HasAnyErrors()
		{
			if (this.IsEmptyWithoutResolution)
			{
				return false;
			}
			using (IEnumerator<Diagnostic> enumerator = this.Bag.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.DefaultSeverity == DiagnosticSeverity.Error)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0002CE48 File Offset: 0x0002B048
		internal bool HasAnyResolvedErrors()
		{
			if (this.IsEmptyWithoutResolution)
			{
				return false;
			}
			foreach (Diagnostic diagnostic in this.Bag)
			{
				DiagnosticWithInfo diagnosticWithInfo = diagnostic as DiagnosticWithInfo;
				if ((diagnosticWithInfo == null || !diagnosticWithInfo.HasLazyInfo) && diagnostic.DefaultSeverity == DiagnosticSeverity.Error)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0002CEC0 File Offset: 0x0002B0C0
		public void Add(Diagnostic diag)
		{
			this.Bag.Enqueue(diag);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0002CED0 File Offset: 0x0002B0D0
		[NullableContext(0)]
		public void AddRange<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> diagnostics) where T : Diagnostic
		{
			if (!diagnostics.IsDefaultOrEmpty)
			{
				ConcurrentQueue<Diagnostic> bag = this.Bag;
				for (int i = 0; i < diagnostics.Length; i++)
				{
					bag.Enqueue(diagnostics[i]);
				}
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0002CF14 File Offset: 0x0002B114
		public void AddRange(IEnumerable<Diagnostic> diagnostics)
		{
			foreach (Diagnostic diagnostic in diagnostics)
			{
				this.Bag.Enqueue(diagnostic);
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0002CF64 File Offset: 0x0002B164
		public void AddRange(DiagnosticBag bag)
		{
			if (!bag.IsEmptyWithoutResolution)
			{
				this.AddRange(bag.Bag);
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0002CF7A File Offset: 0x0002B17A
		public void AddRangeAndFree(DiagnosticBag bag)
		{
			this.AddRange(bag);
			bag.Free();
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0002CF89 File Offset: 0x0002B189
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TDiagnostic> ToReadOnlyAndFree<TDiagnostic>() where TDiagnostic : Diagnostic
		{
			ConcurrentQueue<Diagnostic> lazyBag = this._lazyBag;
			this.Free();
			return DiagnosticBag.ToReadOnlyCore<TDiagnostic>(lazyBag);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0002CF9C File Offset: 0x0002B19C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> ToReadOnlyAndFree()
		{
			return this.ToReadOnlyAndFree<Diagnostic>();
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0002CFA4 File Offset: 0x0002B1A4
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<TDiagnostic> ToReadOnly<TDiagnostic>() where TDiagnostic : Diagnostic
		{
			return DiagnosticBag.ToReadOnlyCore<TDiagnostic>(this._lazyBag);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0002CFB1 File Offset: 0x0002B1B1
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> ToReadOnly()
		{
			return this.ToReadOnly<Diagnostic>();
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0002CFBC File Offset: 0x0002B1BC
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<TDiagnostic> ToReadOnlyCore<TDiagnostic>([Nullable(new byte[] { 2, 1 })] ConcurrentQueue<Diagnostic> oldBag) where TDiagnostic : Diagnostic
		{
			if (oldBag == null)
			{
				return ImmutableArray<TDiagnostic>.Empty;
			}
			ArrayBuilder<TDiagnostic> instance = ArrayBuilder<TDiagnostic>.GetInstance();
			foreach (Diagnostic diagnostic in oldBag)
			{
				TDiagnostic tdiagnostic = (TDiagnostic)((object)diagnostic);
				if (tdiagnostic.Severity != (DiagnosticSeverity)(-2))
				{
					instance.Add(tdiagnostic);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0002D030 File Offset: 0x0002B230
		public IEnumerable<Diagnostic> AsEnumerable()
		{
			ConcurrentQueue<Diagnostic> bag = this.Bag;
			bool flag = false;
			using (IEnumerator<Diagnostic> enumerator = bag.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Severity == (DiagnosticSeverity)(-2))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return bag;
			}
			return this.AsEnumerableFiltered();
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0002D094 File Offset: 0x0002B294
		private IEnumerable<Diagnostic> AsEnumerableFiltered()
		{
			foreach (Diagnostic diagnostic in this.Bag)
			{
				if (diagnostic.Severity != (DiagnosticSeverity)(-2))
				{
					yield return diagnostic;
				}
			}
			IEnumerator<Diagnostic> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0002D0A4 File Offset: 0x0002B2A4
		internal IEnumerable<Diagnostic> AsEnumerableWithoutResolution()
		{
			IEnumerable<Diagnostic> lazyBag = this._lazyBag;
			return lazyBag ?? SpecializedCollections.EmptyEnumerable<Diagnostic>();
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0002D0C2 File Offset: 0x0002B2C2
		internal int Count
		{
			get
			{
				ConcurrentQueue<Diagnostic> lazyBag = this._lazyBag;
				if (lazyBag == null)
				{
					return 0;
				}
				return lazyBag.Count;
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0002D0D8 File Offset: 0x0002B2D8
		public override string ToString()
		{
			if (this.IsEmptyWithoutResolution)
			{
				return "<no errors>";
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Diagnostic diagnostic in this.Bag)
			{
				stringBuilder.AppendLine(diagnostic.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0002D148 File Offset: 0x0002B348
		private ConcurrentQueue<Diagnostic> Bag
		{
			get
			{
				ConcurrentQueue<Diagnostic> lazyBag = this._lazyBag;
				if (lazyBag != null)
				{
					return lazyBag;
				}
				ConcurrentQueue<Diagnostic> concurrentQueue = new ConcurrentQueue<Diagnostic>();
				return Interlocked.CompareExchange<ConcurrentQueue<Diagnostic>>(ref this._lazyBag, concurrentQueue, null) ?? concurrentQueue;
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0002D179 File Offset: 0x0002B379
		internal void Clear()
		{
			if (this._lazyBag != null)
			{
				this._lazyBag = null;
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0002D18A File Offset: 0x0002B38A
		internal static DiagnosticBag GetInstance()
		{
			return DiagnosticBag.s_poolInstance.Allocate();
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0002D196 File Offset: 0x0002B396
		internal void Free()
		{
			this.Clear();
			DiagnosticBag.s_poolInstance.Free(this);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0002D1A9 File Offset: 0x0002B3A9
		private static ObjectPool<DiagnosticBag> CreatePool(int size)
		{
			return new ObjectPool<DiagnosticBag>(() => new DiagnosticBag(), size);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0002D1D0 File Offset: 0x0002B3D0
		private string GetDebuggerDisplay()
		{
			string text = "Count = ";
			ConcurrentQueue<Diagnostic> lazyBag = this._lazyBag;
			return text + ((lazyBag != null) ? lazyBag.Count : 0).ToString();
		}

		// Token: 0x0400042B RID: 1067
		[Nullable(new byte[] { 2, 1 })]
		private ConcurrentQueue<Diagnostic> _lazyBag;

		// Token: 0x0400042C RID: 1068
		private static readonly ObjectPool<DiagnosticBag> s_poolInstance = DiagnosticBag.CreatePool(128);

		// Token: 0x020005A3 RID: 1443
		[Nullable(0)]
		internal sealed class DebuggerProxy
		{
			// Token: 0x06003595 RID: 13717 RVA: 0x00096C47 File Offset: 0x00094E47
			public DebuggerProxy(DiagnosticBag bag)
			{
				this._bag = bag;
			}

			// Token: 0x17000F41 RID: 3905
			// (get) Token: 0x06003596 RID: 13718 RVA: 0x00096C58 File Offset: 0x00094E58
			[DebuggerBrowsable(3)]
			public object[] Diagnostics
			{
				get
				{
					ConcurrentQueue<Diagnostic> lazyBag = this._bag._lazyBag;
					if (lazyBag != null)
					{
						return lazyBag.ToArray();
					}
					return Array.Empty<object>();
				}
			}

			// Token: 0x04001647 RID: 5703
			private readonly DiagnosticBag _bag;
		}
	}
}
