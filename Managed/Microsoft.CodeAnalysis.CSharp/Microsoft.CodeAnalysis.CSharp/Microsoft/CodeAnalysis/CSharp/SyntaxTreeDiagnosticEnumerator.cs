using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000237 RID: 567
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct SyntaxTreeDiagnosticEnumerator
	{
		// Token: 0x06002EFA RID: 12026 RVA: 0x000F19F4 File Offset: 0x000EFBF4
		internal SyntaxTreeDiagnosticEnumerator(SyntaxTree syntaxTree, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode node, int position)
		{
			this._syntaxTree = null;
			this._current = null;
			this._position = position;
			if (node != null && node.ContainsDiagnostics)
			{
				this._syntaxTree = syntaxTree;
				this._stack = new SyntaxTreeDiagnosticEnumerator.NodeIterationStack(8);
				this._stack.PushNodeOrToken(node);
				return;
			}
			this._stack = default(SyntaxTreeDiagnosticEnumerator.NodeIterationStack);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000F1A50 File Offset: 0x000EFC50
		public bool MoveNext()
		{
			IL_016C:
			while (this._stack.Any())
			{
				int num = this._stack.Top.DiagnosticIndex;
				GreenNode node = this._stack.Top.Node;
				DiagnosticInfo[] diagnostics = node.GetDiagnostics();
				if (num < diagnostics.Length - 1)
				{
					num++;
					SyntaxDiagnosticInfo syntaxDiagnosticInfo = (SyntaxDiagnosticInfo)diagnostics[num];
					int num2 = (node.IsToken ? node.GetLeadingTriviaWidth() : 0);
					int length = this._syntaxTree.GetRoot(default(CancellationToken)).FullSpan.Length;
					int num3 = Math.Min(this._position - num2 + syntaxDiagnosticInfo.Offset, length);
					int num4 = Math.Min(num3 + syntaxDiagnosticInfo.Width, length) - num3;
					this._current = new CSDiagnostic(syntaxDiagnosticInfo, new SourceLocation(this._syntaxTree, new TextSpan(num3, num4)), false);
					this._stack.UpdateDiagnosticIndexForStackTop(num);
					return true;
				}
				int i = this._stack.Top.SlotIndex;
				while (i < node.SlotCount - 1)
				{
					i++;
					GreenNode slot = node.GetSlot(i);
					if (slot != null)
					{
						if (slot.ContainsDiagnostics)
						{
							this._stack.UpdateSlotIndexForStackTop(i);
							this._stack.PushNodeOrToken(slot);
							goto IL_016C;
						}
						this._position += slot.FullWidth;
					}
				}
				if (node.SlotCount == 0)
				{
					this._position += node.Width;
				}
				this._stack.Pop();
			}
			return false;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000F1BDA File Offset: 0x000EFDDA
		public Diagnostic Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x04001375 RID: 4981
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly SyntaxTree _syntaxTree;

		// Token: 0x04001376 RID: 4982
		private SyntaxTreeDiagnosticEnumerator.NodeIterationStack _stack;

		// Token: 0x04001377 RID: 4983
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Diagnostic _current;

		// Token: 0x04001378 RID: 4984
		private int _position;

		// Token: 0x04001379 RID: 4985
		private const int DefaultStackCapacity = 8;

		// Token: 0x02000762 RID: 1890
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct NodeIteration
		{
			// Token: 0x06008717 RID: 34583 RVA: 0x001E99CA File Offset: 0x001E7BCA
			internal NodeIteration(GreenNode node)
			{
				this.Node = node;
				this.SlotIndex = -1;
				this.DiagnosticIndex = -1;
			}

			// Token: 0x04002384 RID: 9092
			internal readonly GreenNode Node;

			// Token: 0x04002385 RID: 9093
			internal int DiagnosticIndex;

			// Token: 0x04002386 RID: 9094
			internal int SlotIndex;
		}

		// Token: 0x02000763 RID: 1891
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct NodeIterationStack
		{
			// Token: 0x06008718 RID: 34584 RVA: 0x001E99E1 File Offset: 0x001E7BE1
			internal NodeIterationStack(int capacity)
			{
				this._stack = new SyntaxTreeDiagnosticEnumerator.NodeIteration[capacity];
				this._count = 0;
			}

			// Token: 0x06008719 RID: 34585 RVA: 0x001E99F8 File Offset: 0x001E7BF8
			internal void PushNodeOrToken(GreenNode node)
			{
				SyntaxToken syntaxToken = node as SyntaxToken;
				if (syntaxToken != null)
				{
					this.PushToken(syntaxToken);
					return;
				}
				this.Push(node);
			}

			// Token: 0x0600871A RID: 34586 RVA: 0x001E9A20 File Offset: 0x001E7C20
			private void PushToken(SyntaxToken token)
			{
				GreenNode trailingTrivia = token.GetTrailingTrivia();
				if (trailingTrivia != null)
				{
					this.Push(trailingTrivia);
				}
				this.Push(token);
				GreenNode leadingTrivia = token.GetLeadingTrivia();
				if (leadingTrivia != null)
				{
					this.Push(leadingTrivia);
				}
			}

			// Token: 0x0600871B RID: 34587 RVA: 0x001E9A58 File Offset: 0x001E7C58
			private void Push(GreenNode node)
			{
				if (this._count >= this._stack.Length)
				{
					SyntaxTreeDiagnosticEnumerator.NodeIteration[] array = new SyntaxTreeDiagnosticEnumerator.NodeIteration[this._stack.Length * 2];
					Array.Copy(this._stack, array, this._stack.Length);
					this._stack = array;
				}
				this._stack[this._count] = new SyntaxTreeDiagnosticEnumerator.NodeIteration(node);
				this._count++;
			}

			// Token: 0x0600871C RID: 34588 RVA: 0x001E9AC5 File Offset: 0x001E7CC5
			internal void Pop()
			{
				this._count--;
			}

			// Token: 0x0600871D RID: 34589 RVA: 0x001E9AD5 File Offset: 0x001E7CD5
			internal bool Any()
			{
				return this._count > 0;
			}

			// Token: 0x170026EC RID: 9964
			// (get) Token: 0x0600871E RID: 34590 RVA: 0x001E9AE0 File Offset: 0x001E7CE0
			internal SyntaxTreeDiagnosticEnumerator.NodeIteration Top
			{
				get
				{
					return this[this._count - 1];
				}
			}

			// Token: 0x170026ED RID: 9965
			internal SyntaxTreeDiagnosticEnumerator.NodeIteration this[int index]
			{
				get
				{
					return this._stack[index];
				}
			}

			// Token: 0x06008720 RID: 34592 RVA: 0x001E9AFE File Offset: 0x001E7CFE
			internal void UpdateSlotIndexForStackTop(int slotIndex)
			{
				this._stack[this._count - 1].SlotIndex = slotIndex;
			}

			// Token: 0x06008721 RID: 34593 RVA: 0x001E9B19 File Offset: 0x001E7D19
			internal void UpdateDiagnosticIndexForStackTop(int diagnosticIndex)
			{
				this._stack[this._count - 1].DiagnosticIndex = diagnosticIndex;
			}

			// Token: 0x04002387 RID: 9095
			private SyntaxTreeDiagnosticEnumerator.NodeIteration[] _stack;

			// Token: 0x04002388 RID: 9096
			private int _count;
		}
	}
}
