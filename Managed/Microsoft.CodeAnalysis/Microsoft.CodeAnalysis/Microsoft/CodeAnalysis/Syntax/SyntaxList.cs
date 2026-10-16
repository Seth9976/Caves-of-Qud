using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A3 RID: 675
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class SyntaxList : SyntaxNode
	{
		// Token: 0x06001C87 RID: 7303 RVA: 0x00053F2D File Offset: 0x0005212D
		internal SyntaxList(SyntaxList green, [Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00053F38 File Offset: 0x00052138
		public override string Language
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x00053F3F File Offset: 0x0005213F
		protected override SyntaxTree SyntaxTreeCore
		{
			get
			{
				return base.Parent.SyntaxTree;
			}
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00053F4C File Offset: 0x0005214C
		[NullableContext(2)]
		[return: Nullable(1)]
		protected internal override SyntaxNode ReplaceCore<[Nullable(0)] TNode>([Nullable(new byte[] { 2, 1 })] IEnumerable<TNode> nodes = null, [Nullable(new byte[] { 2, 1, 1, 1 })] Func<TNode, TNode, SyntaxNode> computeReplacementNode = null, IEnumerable<SyntaxToken> tokens = null, Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken = null, IEnumerable<SyntaxTrivia> trivia = null, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia = null)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00053F53 File Offset: 0x00052153
		protected internal override SyntaxNode ReplaceNodeInListCore(SyntaxNode originalNode, IEnumerable<SyntaxNode> replacementNodes)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00053F5A File Offset: 0x0005215A
		protected internal override SyntaxNode InsertNodesInListCore(SyntaxNode nodeInList, IEnumerable<SyntaxNode> nodesToInsert, bool insertBefore)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00053F61 File Offset: 0x00052161
		protected internal override SyntaxNode ReplaceTokenInListCore(SyntaxToken originalToken, IEnumerable<SyntaxToken> newTokens)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00053F68 File Offset: 0x00052168
		protected internal override SyntaxNode InsertTokensInListCore(SyntaxToken originalToken, IEnumerable<SyntaxToken> newTokens, bool insertBefore)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00053F6F File Offset: 0x0005216F
		protected internal override SyntaxNode ReplaceTriviaInListCore(SyntaxTrivia originalTrivia, IEnumerable<SyntaxTrivia> newTrivia)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00053F76 File Offset: 0x00052176
		protected internal override SyntaxNode InsertTriviaInListCore(SyntaxTrivia originalTrivia, IEnumerable<SyntaxTrivia> newTrivia, bool insertBefore)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00053F7D File Offset: 0x0005217D
		protected internal override SyntaxNode RemoveNodesCore(IEnumerable<SyntaxNode> nodes, SyntaxRemoveOptions options)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00053F84 File Offset: 0x00052184
		protected internal override SyntaxNode NormalizeWhitespaceCore(string indentation, string eol, bool elasticTrivia)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00053F8B File Offset: 0x0005218B
		protected override bool IsEquivalentToCore(SyntaxNode node, bool topLevel = false)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0200064F RID: 1615
		[NullableContext(2)]
		[Nullable(0)]
		internal class SeparatedWithManyChildren : SyntaxList
		{
			// Token: 0x0600389F RID: 14495 RVA: 0x0009D481 File Offset: 0x0009B681
			[NullableContext(1)]
			internal SeparatedWithManyChildren(SyntaxList green, [Nullable(2)] SyntaxNode parent, int position)
				: base(green, parent, position)
			{
				this._children = new ArrayElement<SyntaxNode>[green.SlotCount + 1 >> 1];
			}

			// Token: 0x060038A0 RID: 14496 RVA: 0x0009D4A1 File Offset: 0x0009B6A1
			internal override SyntaxNode GetNodeSlot(int i)
			{
				if ((i & 1) != 0)
				{
					return null;
				}
				return base.GetRedElement(ref this._children[i >> 1].Value, i);
			}

			// Token: 0x060038A1 RID: 14497 RVA: 0x0009D4C4 File Offset: 0x0009B6C4
			internal override SyntaxNode GetCachedSlot(int i)
			{
				if ((i & 1) != 0)
				{
					return null;
				}
				return this._children[i >> 1].Value;
			}

			// Token: 0x04001858 RID: 6232
			[Nullable(new byte[] { 1, 0, 2 })]
			private readonly ArrayElement<SyntaxNode>[] _children;
		}

		// Token: 0x02000650 RID: 1616
		[NullableContext(2)]
		[Nullable(0)]
		internal class SeparatedWithManyWeakChildren : SyntaxList
		{
			// Token: 0x060038A2 RID: 14498 RVA: 0x0009D4E0 File Offset: 0x0009B6E0
			[NullableContext(1)]
			internal SeparatedWithManyWeakChildren(SyntaxList green, SyntaxNode parent, int position)
				: base(green, parent, position)
			{
				this._children = new ArrayElement<WeakReference<SyntaxNode>>[(green.SlotCount + 1 >> 1) - 1];
			}

			// Token: 0x060038A3 RID: 14499 RVA: 0x0009D504 File Offset: 0x0009B704
			internal override SyntaxNode GetNodeSlot(int i)
			{
				SyntaxNode syntaxNode = null;
				if ((i & 1) == 0)
				{
					syntaxNode = base.GetWeakRedElement(ref this._children[i >> 1].Value, i);
				}
				return syntaxNode;
			}

			// Token: 0x060038A4 RID: 14500 RVA: 0x0009D534 File Offset: 0x0009B734
			internal override SyntaxNode GetCachedSlot(int i)
			{
				SyntaxNode syntaxNode = null;
				if ((i & 1) == 0)
				{
					WeakReference<SyntaxNode> value = this._children[i >> 1].Value;
					if (value != null)
					{
						value.TryGetTarget(ref syntaxNode);
					}
				}
				return syntaxNode;
			}

			// Token: 0x04001859 RID: 6233
			[Nullable(new byte[] { 1, 0, 2, 1 })]
			private readonly ArrayElement<WeakReference<SyntaxNode>>[] _children;
		}

		// Token: 0x02000651 RID: 1617
		[NullableContext(2)]
		[Nullable(0)]
		internal class WithManyChildren : SyntaxList
		{
			// Token: 0x060038A5 RID: 14501 RVA: 0x0009D569 File Offset: 0x0009B769
			[NullableContext(1)]
			internal WithManyChildren(SyntaxList green, [Nullable(2)] SyntaxNode parent, int position)
				: base(green, parent, position)
			{
				this._children = new ArrayElement<SyntaxNode>[green.SlotCount];
			}

			// Token: 0x060038A6 RID: 14502 RVA: 0x0009D585 File Offset: 0x0009B785
			internal override SyntaxNode GetNodeSlot(int index)
			{
				return base.GetRedElement(ref this._children[index].Value, index);
			}

			// Token: 0x060038A7 RID: 14503 RVA: 0x0009D59F File Offset: 0x0009B79F
			internal override SyntaxNode GetCachedSlot(int index)
			{
				return this._children[index];
			}

			// Token: 0x0400185A RID: 6234
			[Nullable(new byte[] { 1, 0, 2 })]
			private readonly ArrayElement<SyntaxNode>[] _children;
		}

		// Token: 0x02000652 RID: 1618
		[Nullable(0)]
		internal class WithManyWeakChildren : SyntaxList
		{
			// Token: 0x060038A8 RID: 14504 RVA: 0x0009D5B4 File Offset: 0x0009B7B4
			internal WithManyWeakChildren(SyntaxList.WithManyChildrenBase green, SyntaxNode parent, int position)
				: base(green, parent, position)
			{
				int slotCount = green.SlotCount;
				this._children = new ArrayElement<WeakReference<SyntaxNode>>[slotCount];
				int[] array = new int[slotCount];
				int num = position;
				ArrayElement<GreenNode>[] children = green.children;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = num;
					num += children[i].Value.FullWidth;
				}
				this._childPositions = array;
			}

			// Token: 0x060038A9 RID: 14505 RVA: 0x0009D620 File Offset: 0x0009B820
			internal override int GetChildPosition(int index)
			{
				return this._childPositions[index];
			}

			// Token: 0x060038AA RID: 14506 RVA: 0x0009D62A File Offset: 0x0009B82A
			internal override SyntaxNode GetNodeSlot(int index)
			{
				return base.GetWeakRedElement(ref this._children[index].Value, index);
			}

			// Token: 0x060038AB RID: 14507 RVA: 0x0009D644 File Offset: 0x0009B844
			[NullableContext(2)]
			internal override SyntaxNode GetCachedSlot(int index)
			{
				SyntaxNode syntaxNode = null;
				WeakReference<SyntaxNode> value = this._children[index].Value;
				if (value != null)
				{
					value.TryGetTarget(ref syntaxNode);
				}
				return syntaxNode;
			}

			// Token: 0x0400185B RID: 6235
			[Nullable(new byte[] { 1, 0, 2, 1 })]
			private readonly ArrayElement<WeakReference<SyntaxNode>>[] _children;

			// Token: 0x0400185C RID: 6236
			private readonly int[] _childPositions;
		}

		// Token: 0x02000653 RID: 1619
		[NullableContext(2)]
		[Nullable(0)]
		internal class WithThreeChildren : SyntaxList
		{
			// Token: 0x060038AC RID: 14508 RVA: 0x0009D673 File Offset: 0x0009B873
			[NullableContext(1)]
			internal WithThreeChildren(SyntaxList green, [Nullable(2)] SyntaxNode parent, int position)
				: base(green, parent, position)
			{
			}

			// Token: 0x060038AD RID: 14509 RVA: 0x0009D67E File Offset: 0x0009B87E
			internal override SyntaxNode GetNodeSlot(int index)
			{
				switch (index)
				{
				case 0:
					return base.GetRedElement(ref this._child0, 0);
				case 1:
					return base.GetRedElementIfNotToken(ref this._child1);
				case 2:
					return base.GetRedElement(ref this._child2, 2);
				default:
					return null;
				}
			}

			// Token: 0x060038AE RID: 14510 RVA: 0x0009D6BE File Offset: 0x0009B8BE
			internal override SyntaxNode GetCachedSlot(int index)
			{
				switch (index)
				{
				case 0:
					return this._child0;
				case 1:
					return this._child1;
				case 2:
					return this._child2;
				default:
					return null;
				}
			}

			// Token: 0x0400185D RID: 6237
			private SyntaxNode _child0;

			// Token: 0x0400185E RID: 6238
			private SyntaxNode _child1;

			// Token: 0x0400185F RID: 6239
			private SyntaxNode _child2;
		}

		// Token: 0x02000654 RID: 1620
		[NullableContext(2)]
		[Nullable(0)]
		internal class WithTwoChildren : SyntaxList
		{
			// Token: 0x060038AF RID: 14511 RVA: 0x0009D6EA File Offset: 0x0009B8EA
			[NullableContext(1)]
			internal WithTwoChildren(SyntaxList green, [Nullable(2)] SyntaxNode parent, int position)
				: base(green, parent, position)
			{
			}

			// Token: 0x060038B0 RID: 14512 RVA: 0x0009D6F5 File Offset: 0x0009B8F5
			internal override SyntaxNode GetNodeSlot(int index)
			{
				if (index == 0)
				{
					return base.GetRedElement(ref this._child0, 0);
				}
				if (index != 1)
				{
					return null;
				}
				return base.GetRedElementIfNotToken(ref this._child1);
			}

			// Token: 0x060038B1 RID: 14513 RVA: 0x0009D71C File Offset: 0x0009B91C
			internal override SyntaxNode GetCachedSlot(int index)
			{
				if (index == 0)
				{
					return this._child0;
				}
				if (index != 1)
				{
					return null;
				}
				return this._child1;
			}

			// Token: 0x04001860 RID: 6240
			private SyntaxNode _child0;

			// Token: 0x04001861 RID: 6241
			private SyntaxNode _child1;
		}
	}
}
