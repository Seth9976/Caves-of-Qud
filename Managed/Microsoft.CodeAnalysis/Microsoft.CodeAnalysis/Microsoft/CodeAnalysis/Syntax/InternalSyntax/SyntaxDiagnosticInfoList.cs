using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002AF RID: 687
	[NullableContext(1)]
	[Nullable(0)]
	internal struct SyntaxDiagnosticInfoList
	{
		// Token: 0x06001D17 RID: 7447 RVA: 0x0005525B File Offset: 0x0005345B
		internal SyntaxDiagnosticInfoList(GreenNode node)
		{
			this._node = node;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x00055264 File Offset: 0x00053464
		public SyntaxDiagnosticInfoList.Enumerator GetEnumerator()
		{
			return new SyntaxDiagnosticInfoList.Enumerator(this._node);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x00055274 File Offset: 0x00053474
		internal bool Any(Func<DiagnosticInfo, bool> predicate)
		{
			SyntaxDiagnosticInfoList.Enumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (predicate.Invoke(enumerator.Current))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000D64 RID: 3428
		private readonly GreenNode _node;

		// Token: 0x02000657 RID: 1623
		[Nullable(0)]
		public struct Enumerator
		{
			// Token: 0x17000FC5 RID: 4037
			// (get) Token: 0x060038B7 RID: 14519 RVA: 0x0009D886 File Offset: 0x0009BA86
			// (set) Token: 0x060038B8 RID: 14520 RVA: 0x0009D88E File Offset: 0x0009BA8E
			public DiagnosticInfo Current { readonly get; private set; }

			// Token: 0x060038B9 RID: 14521 RVA: 0x0009D897 File Offset: 0x0009BA97
			internal Enumerator(GreenNode node)
			{
				this.Current = null;
				this._stack = null;
				this._count = 0;
				if (node != null && node.ContainsDiagnostics)
				{
					this._stack = new SyntaxDiagnosticInfoList.Enumerator.NodeIteration[8];
					this.PushNodeOrToken(node);
				}
			}

			// Token: 0x060038BA RID: 14522 RVA: 0x0009D8CC File Offset: 0x0009BACC
			public bool MoveNext()
			{
				IL_00D5:
				while (this._count > 0)
				{
					int num = this._stack[this._count - 1].DiagnosticIndex;
					GreenNode node = this._stack[this._count - 1].Node;
					DiagnosticInfo[] diagnostics = node.GetDiagnostics();
					if (num < diagnostics.Length - 1)
					{
						num++;
						this.Current = diagnostics[num];
						this._stack[this._count - 1].DiagnosticIndex = num;
						return true;
					}
					int i = this._stack[this._count - 1].SlotIndex;
					while (i < node.SlotCount - 1)
					{
						i++;
						GreenNode slot = node.GetSlot(i);
						if (slot != null && slot.ContainsDiagnostics)
						{
							this._stack[this._count - 1].SlotIndex = i;
							this.PushNodeOrToken(slot);
							goto IL_00D5;
						}
					}
					this.Pop();
				}
				return false;
			}

			// Token: 0x060038BB RID: 14523 RVA: 0x0009D9BB File Offset: 0x0009BBBB
			private void PushNodeOrToken(GreenNode node)
			{
				if (node.IsToken)
				{
					this.PushToken(node);
					return;
				}
				this.Push(node);
			}

			// Token: 0x060038BC RID: 14524 RVA: 0x0009D9D4 File Offset: 0x0009BBD4
			private void PushToken(GreenNode token)
			{
				GreenNode trailingTriviaCore = token.GetTrailingTriviaCore();
				if (trailingTriviaCore != null)
				{
					this.Push(trailingTriviaCore);
				}
				this.Push(token);
				GreenNode leadingTriviaCore = token.GetLeadingTriviaCore();
				if (leadingTriviaCore != null)
				{
					this.Push(leadingTriviaCore);
				}
			}

			// Token: 0x060038BD RID: 14525 RVA: 0x0009DA0C File Offset: 0x0009BC0C
			private void Push(GreenNode node)
			{
				if (this._count >= this._stack.Length)
				{
					SyntaxDiagnosticInfoList.Enumerator.NodeIteration[] array = new SyntaxDiagnosticInfoList.Enumerator.NodeIteration[this._stack.Length * 2];
					Array.Copy(this._stack, array, this._stack.Length);
					this._stack = array;
				}
				this._stack[this._count] = new SyntaxDiagnosticInfoList.Enumerator.NodeIteration(node);
				this._count++;
			}

			// Token: 0x060038BE RID: 14526 RVA: 0x0009DA79 File Offset: 0x0009BC79
			private void Pop()
			{
				this._count--;
			}

			// Token: 0x04001868 RID: 6248
			[Nullable(2)]
			private SyntaxDiagnosticInfoList.Enumerator.NodeIteration[] _stack;

			// Token: 0x04001869 RID: 6249
			private int _count;

			// Token: 0x020007CC RID: 1996
			[Nullable(0)]
			private struct NodeIteration
			{
				// Token: 0x06003FFE RID: 16382 RVA: 0x000B2BA6 File Offset: 0x000B0DA6
				internal NodeIteration(GreenNode node)
				{
					this.Node = node;
					this.SlotIndex = -1;
					this.DiagnosticIndex = -1;
				}

				// Token: 0x04001DFD RID: 7677
				internal readonly GreenNode Node;

				// Token: 0x04001DFE RID: 7678
				internal int DiagnosticIndex;

				// Token: 0x04001DFF RID: 7679
				internal int SlotIndex;
			}
		}
	}
}
