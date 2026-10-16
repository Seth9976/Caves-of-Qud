using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200027B RID: 635
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	public abstract class SyntaxNode
	{
		// Token: 0x060018B3 RID: 6323 RVA: 0x0004A8A3 File Offset: 0x00048AA3
		internal SyntaxNode(GreenNode green, [Nullable(2)] SyntaxNode parent, int position)
		{
			this.Position = position;
			this.Green = green;
			this._parent = parent;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0004A8C0 File Offset: 0x00048AC0
		internal SyntaxNode(GreenNode green, int position, SyntaxTree syntaxTree)
			: this(green, null, position)
		{
			this._syntaxTree = syntaxTree;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0004A8D2 File Offset: 0x00048AD2
		private string GetDebuggerDisplay()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				" ",
				this.KindText,
				" ",
				this.ToString()
			});
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0004A90F File Offset: 0x00048B0F
		public int RawKind
		{
			get
			{
				return this.Green.RawKind;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0004A91C File Offset: 0x00048B1C
		protected string KindText
		{
			get
			{
				return this.Green.KindText;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060018B8 RID: 6328
		public abstract string Language { get; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0004A929 File Offset: 0x00048B29
		internal GreenNode Green { get; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0004A931 File Offset: 0x00048B31
		internal int Position { get; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0004A939 File Offset: 0x00048B39
		internal int EndPosition
		{
			get
			{
				return this.Position + this.Green.FullWidth;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0004A94D File Offset: 0x00048B4D
		public SyntaxTree SyntaxTree
		{
			get
			{
				return this.SyntaxTreeCore;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x0004A955 File Offset: 0x00048B55
		internal bool IsList
		{
			get
			{
				return this.Green.IsList;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0004A962 File Offset: 0x00048B62
		public TextSpan FullSpan
		{
			get
			{
				return new TextSpan(this.Position, this.Green.FullWidth);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x0004A97A File Offset: 0x00048B7A
		internal int SlotCount
		{
			get
			{
				return this.Green.SlotCount;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0004A988 File Offset: 0x00048B88
		public TextSpan Span
		{
			get
			{
				int position = this.Position;
				int num = this.Green.FullWidth;
				int leadingTriviaWidth = this.Green.GetLeadingTriviaWidth();
				int num2 = position + leadingTriviaWidth;
				num -= leadingTriviaWidth;
				num -= this.Green.GetTrailingTriviaWidth();
				return new TextSpan(num2, num);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0004A9CD File Offset: 0x00048BCD
		public int SpanStart
		{
			get
			{
				return this.Position + this.Green.GetLeadingTriviaWidth();
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0004A9E1 File Offset: 0x00048BE1
		internal int Width
		{
			get
			{
				return this.Green.Width;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0004A9EE File Offset: 0x00048BEE
		internal int FullWidth
		{
			get
			{
				return this.Green.FullWidth;
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0004A9FC File Offset: 0x00048BFC
		[NullableContext(2)]
		internal SyntaxNode GetRed(ref SyntaxNode field, int slot)
		{
			SyntaxNode syntaxNode = field;
			if (syntaxNode == null)
			{
				GreenNode slot2 = this.Green.GetSlot(slot);
				if (slot2 != null)
				{
					Interlocked.CompareExchange<SyntaxNode>(ref field, slot2.CreateRed(this, this.GetChildPosition(slot)), null);
					syntaxNode = field;
				}
			}
			return syntaxNode;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0004AA3C File Offset: 0x00048C3C
		[NullableContext(2)]
		internal SyntaxNode GetRedAtZero(ref SyntaxNode field)
		{
			SyntaxNode syntaxNode = field;
			if (syntaxNode == null)
			{
				GreenNode slot = this.Green.GetSlot(0);
				if (slot != null)
				{
					Interlocked.CompareExchange<SyntaxNode>(ref field, slot.CreateRed(this, this.Position), null);
					syntaxNode = field;
				}
			}
			return syntaxNode;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0004AA78 File Offset: 0x00048C78
		[NullableContext(2)]
		protected T GetRed<[Nullable(0)] T>(ref T field, int slot) where T : SyntaxNode
		{
			T t = field;
			if (t == null)
			{
				GreenNode slot2 = this.Green.GetSlot(slot);
				if (slot2 != null)
				{
					Interlocked.CompareExchange<T>(ref field, (T)((object)slot2.CreateRed(this, this.GetChildPosition(slot))), default(T));
					t = field;
				}
			}
			return t;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0004AAD0 File Offset: 0x00048CD0
		[NullableContext(2)]
		protected T GetRedAtZero<[Nullable(0)] T>(ref T field) where T : SyntaxNode
		{
			T t = field;
			if (t == null)
			{
				GreenNode slot = this.Green.GetSlot(0);
				if (slot != null)
				{
					Interlocked.CompareExchange<T>(ref field, (T)((object)slot.CreateRed(this, this.Position)), default(T));
					t = field;
				}
			}
			return t;
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0004AB28 File Offset: 0x00048D28
		[NullableContext(2)]
		internal SyntaxNode GetRedElement(ref SyntaxNode element, int slot)
		{
			SyntaxNode syntaxNode = element;
			if (syntaxNode == null)
			{
				GreenNode requiredSlot = this.Green.GetRequiredSlot(slot);
				Interlocked.CompareExchange<SyntaxNode>(ref element, requiredSlot.CreateRed(this.Parent, this.GetChildPosition(slot)), null);
				syntaxNode = element;
			}
			return syntaxNode;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0004AB68 File Offset: 0x00048D68
		[NullableContext(2)]
		internal SyntaxNode GetRedElementIfNotToken(ref SyntaxNode element)
		{
			SyntaxNode syntaxNode = element;
			if (syntaxNode == null)
			{
				GreenNode requiredSlot = this.Green.GetRequiredSlot(1);
				if (!requiredSlot.IsToken)
				{
					Interlocked.CompareExchange<SyntaxNode>(ref element, requiredSlot.CreateRed(this.Parent, this.GetChildPosition(1)), null);
					syntaxNode = element;
				}
			}
			return syntaxNode;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0004ABB0 File Offset: 0x00048DB0
		internal SyntaxNode GetWeakRedElement([Nullable(new byte[] { 2, 1 })] ref WeakReference<SyntaxNode> slot, int index)
		{
			SyntaxNode syntaxNode = null;
			WeakReference<SyntaxNode> weakReference = slot;
			if (weakReference != null && weakReference.TryGetTarget(ref syntaxNode))
			{
				return syntaxNode;
			}
			return this.CreateWeakItem(ref slot, index);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0004ABDC File Offset: 0x00048DDC
		private SyntaxNode CreateWeakItem([Nullable(new byte[] { 2, 1 })] ref WeakReference<SyntaxNode> slot, int index)
		{
			SyntaxNode syntaxNode = this.Green.GetRequiredSlot(index).CreateRed(this.Parent, this.GetChildPosition(index));
			WeakReference<SyntaxNode> weakReference = new WeakReference<SyntaxNode>(syntaxNode);
			SyntaxNode syntaxNode2;
			for (;;)
			{
				syntaxNode2 = null;
				WeakReference<SyntaxNode> weakReference2 = slot;
				if (weakReference2 != null && weakReference2.TryGetTarget(ref syntaxNode2))
				{
					break;
				}
				if (Interlocked.CompareExchange<WeakReference<SyntaxNode>>(ref slot, weakReference, weakReference2) == weakReference2)
				{
					return syntaxNode;
				}
			}
			return syntaxNode2;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0004AC2F File Offset: 0x00048E2F
		public override string ToString()
		{
			return this.Green.ToString();
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0004AC3C File Offset: 0x00048E3C
		public virtual string ToFullString()
		{
			return this.Green.ToFullString();
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0004AC49 File Offset: 0x00048E49
		public virtual void WriteTo(TextWriter writer)
		{
			this.Green.WriteTo(writer, true, true);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0004AC5C File Offset: 0x00048E5C
		public SourceText GetText([Nullable(2)] Encoding encoding = null, SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.WriteTo(new StringWriter(stringBuilder));
			return new StringBuilderText(stringBuilder, encoding, checksumAlgorithm);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0004AC83 File Offset: 0x00048E83
		[NullableContext(2)]
		public bool IsEquivalentTo([NotNullWhen(true)] SyntaxNode other)
		{
			return this == other || (other != null && this.Green.IsEquivalentTo(other.Green));
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0004ACA1 File Offset: 0x00048EA1
		[NullableContext(2)]
		public bool IsIncrementallyIdenticalTo([NotNullWhen(true)] SyntaxNode other)
		{
			return this.Green != null && this.Green == ((other != null) ? other.Green : null);
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0004ACC1 File Offset: 0x00048EC1
		public bool IsMissing
		{
			get
			{
				return this.Green.IsMissing;
			}
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0004ACD0 File Offset: 0x00048ED0
		public bool IsPartOfStructuredTrivia()
		{
			for (SyntaxNode syntaxNode = this; syntaxNode != null; syntaxNode = syntaxNode.Parent)
			{
				if (syntaxNode.IsStructuredTrivia)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0004ACF6 File Offset: 0x00048EF6
		public bool IsStructuredTrivia
		{
			get
			{
				return this.Green.IsStructuredTrivia;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0004AD03 File Offset: 0x00048F03
		public bool HasStructuredTrivia
		{
			get
			{
				return this.Green.ContainsStructuredTrivia && !this.Green.IsStructuredTrivia;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0004AD22 File Offset: 0x00048F22
		public bool ContainsSkippedText
		{
			get
			{
				return this.Green.ContainsSkippedText;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0004AD2F File Offset: 0x00048F2F
		public bool ContainsDirectives
		{
			get
			{
				return this.Green.ContainsDirectives;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0004AD3C File Offset: 0x00048F3C
		public bool ContainsDiagnostics
		{
			get
			{
				return this.Green.ContainsDiagnostics;
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0004AD4C File Offset: 0x00048F4C
		[NullableContext(2)]
		public bool Contains(SyntaxNode node)
		{
			if (node == null || !this.FullSpan.Contains(node.FullSpan))
			{
				return false;
			}
			while (node != null)
			{
				if (node == this)
				{
					return true;
				}
				if (node.Parent != null)
				{
					node = node.Parent;
				}
				else if (node.IsStructuredTrivia)
				{
					node = ((IStructuredTriviaSyntax)node).ParentTrivia.Token.Parent;
				}
				else
				{
					node = null;
				}
			}
			return false;
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0004ADBC File Offset: 0x00048FBC
		public bool HasLeadingTrivia
		{
			get
			{
				return this.GetLeadingTrivia().Count > 0;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0004ADDC File Offset: 0x00048FDC
		public bool HasTrailingTrivia
		{
			get
			{
				return this.GetTrailingTrivia().Count > 0;
			}
		}

		// Token: 0x060018DC RID: 6364
		[NullableContext(2)]
		internal abstract SyntaxNode GetCachedSlot(int index);

		// Token: 0x060018DD RID: 6365 RVA: 0x0004ADFC File Offset: 0x00048FFC
		internal int GetChildIndex(int slot)
		{
			int num = 0;
			for (int i = 0; i < slot; i++)
			{
				GreenNode slot2 = this.Green.GetSlot(i);
				if (slot2 != null)
				{
					if (slot2.IsList)
					{
						num += slot2.SlotCount;
					}
					else
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0004AE40 File Offset: 0x00049040
		internal virtual int GetChildPosition(int index)
		{
			int num = 0;
			GreenNode green = this.Green;
			while (index > 0)
			{
				index--;
				SyntaxNode cachedSlot = this.GetCachedSlot(index);
				if (cachedSlot != null)
				{
					return cachedSlot.EndPosition + num;
				}
				GreenNode slot = green.GetSlot(index);
				if (slot != null)
				{
					num += slot.FullWidth;
				}
			}
			return this.Position + num;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0004AE91 File Offset: 0x00049091
		public Location GetLocation()
		{
			return this.SyntaxTree.GetLocation(this.Span);
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0004AEA4 File Offset: 0x000490A4
		internal Location Location
		{
			get
			{
				if (this.SyntaxTree.SupportsLocations)
				{
					return new SourceLocation(this);
				}
				return NoLocation.Singleton;
			}
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0004AEBF File Offset: 0x000490BF
		public IEnumerable<Diagnostic> GetDiagnostics()
		{
			return this.SyntaxTree.GetDiagnostics(this);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0004AECD File Offset: 0x000490CD
		public SyntaxReference GetReference()
		{
			return this.SyntaxTree.GetReference(this);
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x0004AEDB File Offset: 0x000490DB
		[Nullable(2)]
		public SyntaxNode Parent
		{
			[NullableContext(2)]
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0004AEE4 File Offset: 0x000490E4
		public virtual SyntaxTrivia ParentTrivia
		{
			get
			{
				return default(SyntaxTrivia);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0004AEFA File Offset: 0x000490FA
		[Nullable(2)]
		internal SyntaxNode ParentOrStructuredTriviaParent
		{
			[NullableContext(2)]
			get
			{
				return SyntaxNode.GetParent(this, true);
			}
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0004AF03 File Offset: 0x00049103
		public ChildSyntaxList ChildNodesAndTokens()
		{
			return new ChildSyntaxList(this);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0004AF0C File Offset: 0x0004910C
		public virtual SyntaxNodeOrToken ChildThatContainsPosition(int position)
		{
			if (!this.FullSpan.Contains(position))
			{
				throw new ArgumentOutOfRangeException("position");
			}
			return ChildSyntaxList.ChildThatContainsPosition(this, position);
		}

		// Token: 0x060018E8 RID: 6376
		[NullableContext(2)]
		internal abstract SyntaxNode GetNodeSlot(int slot);

		// Token: 0x060018E9 RID: 6377 RVA: 0x0004AF3C File Offset: 0x0004913C
		internal SyntaxNode GetRequiredNodeSlot(int slot)
		{
			return this.GetNodeSlot(slot);
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0004AF45 File Offset: 0x00049145
		public IEnumerable<SyntaxNode> ChildNodes()
		{
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in this.ChildNodesAndTokens())
			{
				SyntaxNode syntaxNode;
				if (syntaxNodeOrToken.AsNode(out syntaxNode))
				{
					yield return syntaxNode;
				}
			}
			ChildSyntaxList.Enumerator enumerator = default(ChildSyntaxList.Enumerator);
			yield break;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0004AF55 File Offset: 0x00049155
		public IEnumerable<SyntaxNode> Ancestors(bool ascendOutOfTrivia = true)
		{
			SyntaxNode parent = this.Parent;
			return ((parent != null) ? parent.AncestorsAndSelf(ascendOutOfTrivia) : null) ?? SpecializedCollections.EmptyEnumerable<SyntaxNode>();
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0004AF73 File Offset: 0x00049173
		public IEnumerable<SyntaxNode> AncestorsAndSelf(bool ascendOutOfTrivia = true)
		{
			SyntaxNode node;
			for (node = this; node != null; node = SyntaxNode.GetParent(node, ascendOutOfTrivia))
			{
				yield return node;
			}
			node = null;
			yield break;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0004AF8C File Offset: 0x0004918C
		[return: Nullable(2)]
		private static SyntaxNode GetParent(SyntaxNode node, bool ascendOutOfTrivia)
		{
			SyntaxNode syntaxNode = node.Parent;
			if (syntaxNode == null && ascendOutOfTrivia)
			{
				IStructuredTriviaSyntax structuredTriviaSyntax = node as IStructuredTriviaSyntax;
				if (structuredTriviaSyntax != null)
				{
					syntaxNode = structuredTriviaSyntax.ParentTrivia.Token.Parent;
				}
			}
			return syntaxNode;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0004AFCC File Offset: 0x000491CC
		[NullableContext(0)]
		[return: Nullable(2)]
		public TNode FirstAncestorOrSelf<TNode>([Nullable(new byte[] { 2, 1 })] Func<TNode, bool> predicate = null, bool ascendOutOfTrivia = true) where TNode : SyntaxNode
		{
			for (SyntaxNode syntaxNode = this; syntaxNode != null; syntaxNode = SyntaxNode.GetParent(syntaxNode, ascendOutOfTrivia))
			{
				TNode tnode = syntaxNode as TNode;
				if (tnode != null && (predicate == null || predicate.Invoke(tnode)))
				{
					return tnode;
				}
			}
			return default(TNode);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0004B014 File Offset: 0x00049214
		[return: Nullable(2)]
		public TNode FirstAncestorOrSelf<[Nullable(0)] TNode, [Nullable(2)] TArg>(Func<TNode, TArg, bool> predicate, TArg argument, bool ascendOutOfTrivia = true) where TNode : SyntaxNode
		{
			for (SyntaxNode syntaxNode = this; syntaxNode != null; syntaxNode = SyntaxNode.GetParent(syntaxNode, ascendOutOfTrivia))
			{
				TNode tnode = syntaxNode as TNode;
				if (tnode != null && predicate.Invoke(tnode, argument))
				{
					return tnode;
				}
			}
			return default(TNode);
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0004B059 File Offset: 0x00049259
		public IEnumerable<SyntaxNode> DescendantNodes([Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantNodesImpl(this.FullSpan, descendIntoChildren, descendIntoTrivia, false);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0004B06A File Offset: 0x0004926A
		public IEnumerable<SyntaxNode> DescendantNodes(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantNodesImpl(span, descendIntoChildren, descendIntoTrivia, false);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0004B076 File Offset: 0x00049276
		public IEnumerable<SyntaxNode> DescendantNodesAndSelf([Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantNodesImpl(this.FullSpan, descendIntoChildren, descendIntoTrivia, true);
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0004B087 File Offset: 0x00049287
		public IEnumerable<SyntaxNode> DescendantNodesAndSelf(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantNodesImpl(span, descendIntoChildren, descendIntoTrivia, true);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0004B093 File Offset: 0x00049293
		public IEnumerable<SyntaxNodeOrToken> DescendantNodesAndTokens([Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantNodesAndTokensImpl(this.FullSpan, descendIntoChildren, descendIntoTrivia, false);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0004B0A4 File Offset: 0x000492A4
		public IEnumerable<SyntaxNodeOrToken> DescendantNodesAndTokens(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantNodesAndTokensImpl(span, descendIntoChildren, descendIntoTrivia, false);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0004B0B0 File Offset: 0x000492B0
		public IEnumerable<SyntaxNodeOrToken> DescendantNodesAndTokensAndSelf([Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantNodesAndTokensImpl(this.FullSpan, descendIntoChildren, descendIntoTrivia, true);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0004B0C1 File Offset: 0x000492C1
		public IEnumerable<SyntaxNodeOrToken> DescendantNodesAndTokensAndSelf(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantNodesAndTokensImpl(span, descendIntoChildren, descendIntoTrivia, true);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0004B0D0 File Offset: 0x000492D0
		public SyntaxNode FindNode(TextSpan span, bool findInsideTrivia = false, bool getInnermostNodeForTie = false)
		{
			if (!this.FullSpan.Contains(span))
			{
				throw new ArgumentOutOfRangeException("span");
			}
			SyntaxNode syntaxNode = this.FindToken(span.Start, findInsideTrivia).Parent.FirstAncestorOrSelf<SyntaxNode, TextSpan>((SyntaxNode a, TextSpan span) => a.FullSpan.Contains(span), span, true);
			SyntaxTree syntaxTree = syntaxNode.SyntaxTree;
			SyntaxNode syntaxNode2 = ((syntaxTree != null) ? syntaxTree.GetRoot(default(CancellationToken)) : null);
			if (!getInnermostNodeForTie)
			{
				for (;;)
				{
					SyntaxNode parent = syntaxNode.Parent;
					if (parent == null || parent.FullWidth != syntaxNode.FullWidth || parent == syntaxNode2)
					{
						break;
					}
					syntaxNode = parent;
				}
			}
			return syntaxNode;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0004B17C File Offset: 0x0004937C
		public SyntaxToken FindToken(int position, bool findInsideTrivia = false)
		{
			return this.FindTokenCore(position, findInsideTrivia);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0004B186 File Offset: 0x00049386
		public SyntaxToken GetFirstToken(bool includeZeroWidth = false, bool includeSkipped = false, bool includeDirectives = false, bool includeDocumentationComments = false)
		{
			return SyntaxNavigator.Instance.GetFirstToken(in this, includeZeroWidth, includeSkipped, includeDirectives, includeDocumentationComments);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0004B199 File Offset: 0x00049399
		public SyntaxToken GetLastToken(bool includeZeroWidth = false, bool includeSkipped = false, bool includeDirectives = false, bool includeDocumentationComments = false)
		{
			return SyntaxNavigator.Instance.GetLastToken(in this, includeZeroWidth, includeSkipped, includeDirectives, includeDocumentationComments);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0004B1AC File Offset: 0x000493AC
		public IEnumerable<SyntaxToken> ChildTokens()
		{
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in this.ChildNodesAndTokens())
			{
				if (syntaxNodeOrToken.IsToken)
				{
					yield return syntaxNodeOrToken.AsToken();
				}
			}
			ChildSyntaxList.Enumerator enumerator = default(ChildSyntaxList.Enumerator);
			yield break;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0004B1BC File Offset: 0x000493BC
		public IEnumerable<SyntaxToken> DescendantTokens([Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return Enumerable.Select<SyntaxNodeOrToken, SyntaxToken>(Enumerable.Where<SyntaxNodeOrToken>(this.DescendantNodesAndTokens(descendIntoChildren, descendIntoTrivia), (SyntaxNodeOrToken sn) => sn.IsToken), (SyntaxNodeOrToken sn) => sn.AsToken());
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0004B21C File Offset: 0x0004941C
		public IEnumerable<SyntaxToken> DescendantTokens(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return Enumerable.Select<SyntaxNodeOrToken, SyntaxToken>(Enumerable.Where<SyntaxNodeOrToken>(this.DescendantNodesAndTokens(span, descendIntoChildren, descendIntoTrivia), (SyntaxNodeOrToken sn) => sn.IsToken), (SyntaxNodeOrToken sn) => sn.AsToken());
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0004B27C File Offset: 0x0004947C
		public SyntaxTriviaList GetLeadingTrivia()
		{
			return this.GetFirstToken(true, false, false, false).LeadingTrivia;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0004B29C File Offset: 0x0004949C
		public SyntaxTriviaList GetTrailingTrivia()
		{
			return this.GetLastToken(true, false, false, false).TrailingTrivia;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0004B2BB File Offset: 0x000494BB
		public SyntaxTrivia FindTrivia(int position, bool findInsideTrivia = false)
		{
			return this.FindTrivia(position, findInsideTrivia ? SyntaxTrivia.Any : null);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0004B2D0 File Offset: 0x000494D0
		[NullableContext(2)]
		public SyntaxTrivia FindTrivia(int position, Func<SyntaxTrivia, bool> stepInto)
		{
			if (this.FullSpan.Contains(position))
			{
				return SyntaxNode.FindTriviaByOffset(this, position - this.Position, stepInto);
			}
			return default(SyntaxTrivia);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0004B308 File Offset: 0x00049508
		internal static SyntaxTrivia FindTriviaByOffset(SyntaxNode node, int textOffset, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto = null)
		{
			IL_0000:
			while (textOffset >= 0)
			{
				foreach (SyntaxNodeOrToken syntaxNodeOrToken in node.ChildNodesAndTokens())
				{
					int fullWidth = syntaxNodeOrToken.FullWidth;
					if (textOffset < fullWidth)
					{
						SyntaxNode syntaxNode;
						if (syntaxNodeOrToken.AsNode(out syntaxNode))
						{
							node = syntaxNode;
							goto IL_0000;
						}
						if (syntaxNodeOrToken.IsToken)
						{
							SyntaxToken syntaxToken = syntaxNodeOrToken.AsToken();
							int leadingWidth = syntaxToken.LeadingWidth;
							if (textOffset < syntaxToken.LeadingWidth)
							{
								foreach (SyntaxTrivia syntaxTrivia in syntaxToken.LeadingTrivia)
								{
									if (textOffset < syntaxTrivia.FullWidth)
									{
										if (syntaxTrivia.HasStructure && stepInto != null && stepInto.Invoke(syntaxTrivia))
										{
											node = syntaxTrivia.GetStructure();
											goto IL_0000;
										}
										return syntaxTrivia;
									}
									else
									{
										textOffset -= syntaxTrivia.FullWidth;
									}
								}
							}
							else if (textOffset >= leadingWidth + syntaxToken.Width)
							{
								textOffset -= leadingWidth + syntaxToken.Width;
								foreach (SyntaxTrivia syntaxTrivia2 in syntaxToken.TrailingTrivia)
								{
									if (textOffset < syntaxTrivia2.FullWidth)
									{
										if (syntaxTrivia2.HasStructure && stepInto != null && stepInto.Invoke(syntaxTrivia2))
										{
											node = syntaxTrivia2.GetStructure();
											goto IL_0000;
										}
										return syntaxTrivia2;
									}
									else
									{
										textOffset -= syntaxTrivia2.FullWidth;
									}
								}
							}
							return default(SyntaxTrivia);
						}
					}
					textOffset -= fullWidth;
				}
				break;
			}
			return default(SyntaxTrivia);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0004B487 File Offset: 0x00049687
		public IEnumerable<SyntaxTrivia> DescendantTrivia([Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantTriviaImpl(this.FullSpan, descendIntoChildren, descendIntoTrivia);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0004B497 File Offset: 0x00049697
		public IEnumerable<SyntaxTrivia> DescendantTrivia(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			return this.DescendantTriviaImpl(span, descendIntoChildren, descendIntoTrivia);
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0004B4A2 File Offset: 0x000496A2
		public bool ContainsAnnotations
		{
			get
			{
				return this.Green.ContainsAnnotations;
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0004B4AF File Offset: 0x000496AF
		public bool HasAnnotations(string annotationKind)
		{
			return this.Green.HasAnnotations(annotationKind);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0004B4BD File Offset: 0x000496BD
		public bool HasAnnotations(IEnumerable<string> annotationKinds)
		{
			return this.Green.HasAnnotations(annotationKinds);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0004B4CB File Offset: 0x000496CB
		[NullableContext(2)]
		public bool HasAnnotation([NotNullWhen(true)] SyntaxAnnotation annotation)
		{
			return this.Green.HasAnnotation(annotation);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0004B4D9 File Offset: 0x000496D9
		public IEnumerable<SyntaxAnnotation> GetAnnotations(string annotationKind)
		{
			return this.Green.GetAnnotations(annotationKind);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0004B4E7 File Offset: 0x000496E7
		public IEnumerable<SyntaxAnnotation> GetAnnotations(IEnumerable<string> annotationKinds)
		{
			return this.Green.GetAnnotations(annotationKinds);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0004B4F5 File Offset: 0x000496F5
		internal SyntaxAnnotation[] GetAnnotations()
		{
			return this.Green.GetAnnotations();
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0004B504 File Offset: 0x00049704
		public IEnumerable<SyntaxNodeOrToken> GetAnnotatedNodesAndTokens(string annotationKind)
		{
			return Enumerable.Where<SyntaxNodeOrToken>(this.DescendantNodesAndTokensAndSelf((SyntaxNode n) => n.ContainsAnnotations, true), (SyntaxNodeOrToken t) => t.HasAnnotations(annotationKind));
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0004B558 File Offset: 0x00049758
		public IEnumerable<SyntaxNodeOrToken> GetAnnotatedNodesAndTokens(params string[] annotationKinds)
		{
			return Enumerable.Where<SyntaxNodeOrToken>(this.DescendantNodesAndTokensAndSelf((SyntaxNode n) => n.ContainsAnnotations, true), (SyntaxNodeOrToken t) => t.HasAnnotations(annotationKinds));
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0004B5AC File Offset: 0x000497AC
		public IEnumerable<SyntaxNodeOrToken> GetAnnotatedNodesAndTokens(SyntaxAnnotation annotation)
		{
			return Enumerable.Where<SyntaxNodeOrToken>(this.DescendantNodesAndTokensAndSelf((SyntaxNode n) => n.ContainsAnnotations, true), (SyntaxNodeOrToken t) => t.HasAnnotation(annotation));
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0004B600 File Offset: 0x00049800
		public IEnumerable<SyntaxNode> GetAnnotatedNodes(SyntaxAnnotation syntaxAnnotation)
		{
			return Enumerable.Select<SyntaxNodeOrToken, SyntaxNode>(Enumerable.Where<SyntaxNodeOrToken>(this.GetAnnotatedNodesAndTokens(syntaxAnnotation), (SyntaxNodeOrToken n) => n.IsNode), (SyntaxNodeOrToken n) => n.AsNode());
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0004B65C File Offset: 0x0004985C
		public IEnumerable<SyntaxNode> GetAnnotatedNodes(string annotationKind)
		{
			return Enumerable.Select<SyntaxNodeOrToken, SyntaxNode>(Enumerable.Where<SyntaxNodeOrToken>(this.GetAnnotatedNodesAndTokens(annotationKind), (SyntaxNodeOrToken n) => n.IsNode), (SyntaxNodeOrToken n) => n.AsNode());
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0004B6B8 File Offset: 0x000498B8
		public IEnumerable<SyntaxToken> GetAnnotatedTokens(SyntaxAnnotation syntaxAnnotation)
		{
			return Enumerable.Select<SyntaxNodeOrToken, SyntaxToken>(Enumerable.Where<SyntaxNodeOrToken>(this.GetAnnotatedNodesAndTokens(syntaxAnnotation), (SyntaxNodeOrToken n) => n.IsToken), (SyntaxNodeOrToken n) => n.AsToken());
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0004B714 File Offset: 0x00049914
		public IEnumerable<SyntaxToken> GetAnnotatedTokens(string annotationKind)
		{
			return Enumerable.Select<SyntaxNodeOrToken, SyntaxToken>(Enumerable.Where<SyntaxNodeOrToken>(this.GetAnnotatedNodesAndTokens(annotationKind), (SyntaxNodeOrToken n) => n.IsToken), (SyntaxNodeOrToken n) => n.AsToken());
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0004B770 File Offset: 0x00049970
		public IEnumerable<SyntaxTrivia> GetAnnotatedTrivia(string annotationKind)
		{
			return Enumerable.Where<SyntaxTrivia>(this.DescendantTrivia((SyntaxNode n) => n.ContainsAnnotations, true), (SyntaxTrivia tr) => tr.HasAnnotations(annotationKind));
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0004B7C4 File Offset: 0x000499C4
		public IEnumerable<SyntaxTrivia> GetAnnotatedTrivia(params string[] annotationKinds)
		{
			return Enumerable.Where<SyntaxTrivia>(this.DescendantTrivia((SyntaxNode n) => n.ContainsAnnotations, true), (SyntaxTrivia tr) => tr.HasAnnotations(annotationKinds));
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0004B818 File Offset: 0x00049A18
		public IEnumerable<SyntaxTrivia> GetAnnotatedTrivia(SyntaxAnnotation annotation)
		{
			return Enumerable.Where<SyntaxTrivia>(this.DescendantTrivia((SyntaxNode n) => n.ContainsAnnotations, true), (SyntaxTrivia tr) => tr.HasAnnotation(annotation));
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0004B869 File Offset: 0x00049A69
		internal SyntaxNode WithAdditionalAnnotationsInternal(IEnumerable<SyntaxAnnotation> annotations)
		{
			return this.Green.WithAdditionalAnnotationsGreen(annotations).CreateRed();
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0004B87C File Offset: 0x00049A7C
		internal SyntaxNode GetNodeWithoutAnnotations(IEnumerable<SyntaxAnnotation> annotations)
		{
			return this.Green.WithoutAnnotationsGreen(annotations).CreateRed();
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0004B890 File Offset: 0x00049A90
		[NullableContext(2)]
		public T CopyAnnotationsTo<[Nullable(0)] T>(T node) where T : SyntaxNode
		{
			if (node == null)
			{
				return default(T);
			}
			SyntaxAnnotation[] annotations = this.Green.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				return (T)((object)node.Green.WithAdditionalAnnotationsGreen(annotations).CreateRed());
			}
			return node;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0004B8DF File Offset: 0x00049ADF
		public bool IsEquivalentTo(SyntaxNode node, bool topLevel = false)
		{
			return this.IsEquivalentToCore(node, topLevel);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0004B8EC File Offset: 0x00049AEC
		public virtual void SerializeTo(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new InvalidOperationException(CodeAnalysisResources.TheStreamCannotBeWrittenTo);
			}
			using (ObjectWriter objectWriter = new ObjectWriter(stream, true, cancellationToken))
			{
				objectWriter.WriteValue(this.Green);
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0004B94C File Offset: 0x00049B4C
		protected virtual bool EquivalentToCore(SyntaxNode other)
		{
			return this.IsEquivalentTo(other);
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600191D RID: 6429
		protected abstract SyntaxTree SyntaxTreeCore { get; }

		// Token: 0x0600191E RID: 6430 RVA: 0x0004B958 File Offset: 0x00049B58
		protected virtual SyntaxToken FindTokenCore(int position, bool findInsideTrivia)
		{
			if (findInsideTrivia)
			{
				return this.FindToken(position, SyntaxTrivia.Any);
			}
			SyntaxToken syntaxToken;
			if (this.TryGetEofAt(position, out syntaxToken))
			{
				return syntaxToken;
			}
			if (!this.FullSpan.Contains(position))
			{
				throw new ArgumentOutOfRangeException("position");
			}
			return this.FindTokenInternal(position);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0004B9A8 File Offset: 0x00049BA8
		private bool TryGetEofAt(int position, out SyntaxToken Eof)
		{
			if (position == this.EndPosition)
			{
				ICompilationUnitSyntax compilationUnitSyntax = this as ICompilationUnitSyntax;
				if (compilationUnitSyntax != null)
				{
					Eof = compilationUnitSyntax.EndOfFileToken;
					return true;
				}
			}
			Eof = default(SyntaxToken);
			return false;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0004B9E0 File Offset: 0x00049BE0
		internal SyntaxToken FindTokenInternal(int position)
		{
			SyntaxNodeOrToken syntaxNodeOrToken = this;
			for (;;)
			{
				SyntaxNode syntaxNode = syntaxNodeOrToken.AsNode();
				if (syntaxNode == null)
				{
					break;
				}
				syntaxNodeOrToken = syntaxNode.ChildThatContainsPosition(position);
			}
			return syntaxNodeOrToken.AsToken();
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0004BA10 File Offset: 0x00049C10
		private SyntaxToken FindToken(int position, Func<SyntaxTrivia, bool> findInsideTrivia)
		{
			return this.FindTokenCore(position, findInsideTrivia);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0004BA1C File Offset: 0x00049C1C
		protected virtual SyntaxToken FindTokenCore(int position, Func<SyntaxTrivia, bool> stepInto)
		{
			SyntaxToken syntaxToken = this.FindToken(position, false);
			if (stepInto != null)
			{
				SyntaxTrivia triviaFromSyntaxToken = SyntaxNode.GetTriviaFromSyntaxToken(position, in syntaxToken);
				if (triviaFromSyntaxToken.HasStructure && stepInto.Invoke(triviaFromSyntaxToken))
				{
					syntaxToken = triviaFromSyntaxToken.GetStructure().FindTokenInternal(position);
				}
			}
			return syntaxToken;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0004BA60 File Offset: 0x00049C60
		internal static SyntaxTrivia GetTriviaFromSyntaxToken(int position, in SyntaxToken token)
		{
			TextSpan span = token.Span;
			SyntaxTrivia syntaxTrivia = default(SyntaxTrivia);
			if (position < span.Start && token.HasLeadingTrivia)
			{
				SyntaxTriviaList syntaxTriviaList = token.LeadingTrivia;
				syntaxTrivia = SyntaxNode.GetTriviaThatContainsPosition(in syntaxTriviaList, position);
			}
			else if (position >= span.End && token.HasTrailingTrivia)
			{
				SyntaxTriviaList syntaxTriviaList = token.TrailingTrivia;
				syntaxTrivia = SyntaxNode.GetTriviaThatContainsPosition(in syntaxTriviaList, position);
			}
			return syntaxTrivia;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0004BAC4 File Offset: 0x00049CC4
		internal static SyntaxTrivia GetTriviaThatContainsPosition(in SyntaxTriviaList list, int position)
		{
			foreach (SyntaxTrivia syntaxTrivia in list)
			{
				if (syntaxTrivia.FullSpan.Contains(position))
				{
					return syntaxTrivia;
				}
				if (syntaxTrivia.Position > position)
				{
					break;
				}
			}
			return default(SyntaxTrivia);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0004BB12 File Offset: 0x00049D12
		protected virtual SyntaxTrivia FindTriviaCore(int position, bool findInsideTrivia)
		{
			return this.FindTrivia(position, findInsideTrivia);
		}

		// Token: 0x06001926 RID: 6438
		[NullableContext(2)]
		[return: Nullable(1)]
		protected internal abstract SyntaxNode ReplaceCore<[Nullable(0)] TNode>([Nullable(new byte[] { 2, 1 })] IEnumerable<TNode> nodes = null, [Nullable(new byte[] { 2, 1, 1, 1 })] Func<TNode, TNode, SyntaxNode> computeReplacementNode = null, IEnumerable<SyntaxToken> tokens = null, Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken = null, IEnumerable<SyntaxTrivia> trivia = null, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia = null) where TNode : SyntaxNode;

		// Token: 0x06001927 RID: 6439
		protected internal abstract SyntaxNode ReplaceNodeInListCore(SyntaxNode originalNode, IEnumerable<SyntaxNode> replacementNodes);

		// Token: 0x06001928 RID: 6440
		protected internal abstract SyntaxNode InsertNodesInListCore(SyntaxNode nodeInList, IEnumerable<SyntaxNode> nodesToInsert, bool insertBefore);

		// Token: 0x06001929 RID: 6441
		protected internal abstract SyntaxNode ReplaceTokenInListCore(SyntaxToken originalToken, IEnumerable<SyntaxToken> newTokens);

		// Token: 0x0600192A RID: 6442
		protected internal abstract SyntaxNode InsertTokensInListCore(SyntaxToken originalToken, IEnumerable<SyntaxToken> newTokens, bool insertBefore);

		// Token: 0x0600192B RID: 6443
		protected internal abstract SyntaxNode ReplaceTriviaInListCore(SyntaxTrivia originalTrivia, IEnumerable<SyntaxTrivia> newTrivia);

		// Token: 0x0600192C RID: 6444
		protected internal abstract SyntaxNode InsertTriviaInListCore(SyntaxTrivia originalTrivia, IEnumerable<SyntaxTrivia> newTrivia, bool insertBefore);

		// Token: 0x0600192D RID: 6445
		[return: Nullable(2)]
		protected internal abstract SyntaxNode RemoveNodesCore(IEnumerable<SyntaxNode> nodes, SyntaxRemoveOptions options);

		// Token: 0x0600192E RID: 6446
		protected internal abstract SyntaxNode NormalizeWhitespaceCore(string indentation, string eol, bool elasticTrivia);

		// Token: 0x0600192F RID: 6447
		protected abstract bool IsEquivalentToCore(SyntaxNode node, bool topLevel = false);

		// Token: 0x06001930 RID: 6448 RVA: 0x0004BB1C File Offset: 0x00049D1C
		internal virtual bool ShouldCreateWeakList()
		{
			return false;
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x0004BB1F File Offset: 0x00049D1F
		internal bool HasErrors
		{
			get
			{
				return this.ContainsDiagnostics && this.HasErrorsSlow();
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0004BB34 File Offset: 0x00049D34
		private bool HasErrorsSlow()
		{
			return new SyntaxDiagnosticInfoList(this.Green).Any((DiagnosticInfo info) => info.Severity == DiagnosticSeverity.Error);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0004BB73 File Offset: 0x00049D73
		internal static T CloneNodeAsRoot<[Nullable(0)] T>(T node, SyntaxTree syntaxTree) where T : SyntaxNode
		{
			T t = (T)((object)node.Green.CreateRed(null, 0));
			t._syntaxTree = syntaxTree;
			return t;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0004BB98 File Offset: 0x00049D98
		private IEnumerable<SyntaxNode> DescendantNodesImpl(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren, bool descendIntoTrivia, bool includeSelf)
		{
			if (!descendIntoTrivia)
			{
				return this.DescendantNodesOnly(span, descendIntoChildren, includeSelf);
			}
			return Enumerable.Select<SyntaxNodeOrToken, SyntaxNode>(Enumerable.Where<SyntaxNodeOrToken>(this.DescendantNodesAndTokensImpl(span, descendIntoChildren, true, includeSelf), (SyntaxNodeOrToken e) => e.IsNode), (SyntaxNodeOrToken e) => e.AsNode());
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0004BC08 File Offset: 0x00049E08
		private IEnumerable<SyntaxNodeOrToken> DescendantNodesAndTokensImpl(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren, bool descendIntoTrivia, bool includeSelf)
		{
			if (!descendIntoTrivia)
			{
				return this.DescendantNodesAndTokensOnly(span, descendIntoChildren, includeSelf);
			}
			return this.DescendantNodesAndTokensIntoTrivia(span, descendIntoChildren, includeSelf);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0004BC22 File Offset: 0x00049E22
		private IEnumerable<SyntaxTrivia> DescendantTriviaImpl(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false)
		{
			if (!descendIntoTrivia)
			{
				return this.DescendantTriviaOnly(span, descendIntoChildren);
			}
			return this.DescendantTriviaIntoTrivia(span, descendIntoChildren);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0004BC38 File Offset: 0x00049E38
		private static bool IsInSpan(in TextSpan span, TextSpan childSpan)
		{
			return span.OverlapsWith(childSpan) || (childSpan.Length == 0 && span.IntersectsWith(childSpan));
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0004BC57 File Offset: 0x00049E57
		private IEnumerable<SyntaxNode> DescendantNodesOnly(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren, bool includeSelf)
		{
			if (includeSelf && SyntaxNode.IsInSpan(in span, this.FullSpan))
			{
				yield return this;
			}
			using (SyntaxNode.ChildSyntaxListEnumeratorStack stack = new SyntaxNode.ChildSyntaxListEnumeratorStack(this, descendIntoChildren))
			{
				while (stack.IsNotEmpty)
				{
					SyntaxNode syntaxNode = stack.TryGetNextAsNodeInSpan(in span);
					if (syntaxNode != null)
					{
						stack.PushChildren(syntaxNode, descendIntoChildren);
						yield return syntaxNode;
					}
				}
			}
			SyntaxNode.ChildSyntaxListEnumeratorStack stack = default(SyntaxNode.ChildSyntaxListEnumeratorStack);
			yield break;
			yield break;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0004BC7C File Offset: 0x00049E7C
		private IEnumerable<SyntaxNodeOrToken> DescendantNodesAndTokensOnly(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren, bool includeSelf)
		{
			if (includeSelf && SyntaxNode.IsInSpan(in span, this.FullSpan))
			{
				yield return this;
			}
			using (SyntaxNode.ChildSyntaxListEnumeratorStack stack = new SyntaxNode.ChildSyntaxListEnumeratorStack(this, descendIntoChildren))
			{
				while (stack.IsNotEmpty)
				{
					SyntaxNodeOrToken syntaxNodeOrToken;
					if (stack.TryGetNextInSpan(in span, out syntaxNodeOrToken))
					{
						SyntaxNode syntaxNode = syntaxNodeOrToken.AsNode();
						if (syntaxNode != null)
						{
							stack.PushChildren(syntaxNode, descendIntoChildren);
						}
						yield return syntaxNodeOrToken;
					}
				}
			}
			SyntaxNode.ChildSyntaxListEnumeratorStack stack = default(SyntaxNode.ChildSyntaxListEnumeratorStack);
			yield break;
			yield break;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0004BCA1 File Offset: 0x00049EA1
		private IEnumerable<SyntaxNodeOrToken> DescendantNodesAndTokensIntoTrivia(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren, bool includeSelf)
		{
			if (includeSelf && SyntaxNode.IsInSpan(in span, this.FullSpan))
			{
				yield return this;
			}
			using (SyntaxNode.ThreeEnumeratorListStack stack = new SyntaxNode.ThreeEnumeratorListStack(this, descendIntoChildren))
			{
				while (stack.IsNotEmpty)
				{
					switch (stack.PeekNext())
					{
					case SyntaxNode.ThreeEnumeratorListStack.Which.Node:
					{
						SyntaxNodeOrToken syntaxNodeOrToken;
						if (stack.TryGetNextInSpan(in span, out syntaxNodeOrToken))
						{
							if (syntaxNodeOrToken.IsNode)
							{
								stack.PushChildren(syntaxNodeOrToken.AsNode(), descendIntoChildren);
							}
							else if (syntaxNodeOrToken.IsToken)
							{
								SyntaxToken syntaxToken = syntaxNodeOrToken.AsToken();
								if (syntaxToken.HasStructuredTrivia)
								{
									if (syntaxToken.HasTrailingTrivia)
									{
										stack.PushTrailingTrivia(in syntaxToken);
									}
									stack.PushToken(in syntaxNodeOrToken);
									if (syntaxToken.HasLeadingTrivia)
									{
										stack.PushLeadingTrivia(in syntaxToken);
										break;
									}
									break;
								}
							}
							yield return syntaxNodeOrToken;
						}
						break;
					}
					case SyntaxNode.ThreeEnumeratorListStack.Which.Trivia:
					{
						SyntaxTrivia syntaxTrivia;
						SyntaxNode syntaxNode;
						if (stack.TryGetNext(out syntaxTrivia) && syntaxTrivia.TryGetStructure(out syntaxNode) && SyntaxNode.IsInSpan(in span, syntaxTrivia.FullSpan))
						{
							stack.PushChildren(syntaxNode, descendIntoChildren);
							yield return syntaxNode;
						}
						break;
					}
					case SyntaxNode.ThreeEnumeratorListStack.Which.Token:
						yield return stack.PopToken();
						break;
					}
				}
			}
			SyntaxNode.ThreeEnumeratorListStack stack = default(SyntaxNode.ThreeEnumeratorListStack);
			yield break;
			yield break;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0004BCC6 File Offset: 0x00049EC6
		private IEnumerable<SyntaxTrivia> DescendantTriviaOnly(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren)
		{
			using (SyntaxNode.ChildSyntaxListEnumeratorStack stack = new SyntaxNode.ChildSyntaxListEnumeratorStack(this, descendIntoChildren))
			{
				while (stack.IsNotEmpty)
				{
					SyntaxNodeOrToken syntaxNodeOrToken;
					if (stack.TryGetNextInSpan(in span, out syntaxNodeOrToken))
					{
						SyntaxNode syntaxNode;
						if (syntaxNodeOrToken.AsNode(out syntaxNode))
						{
							stack.PushChildren(syntaxNode, descendIntoChildren);
						}
						else if (syntaxNodeOrToken.IsToken)
						{
							SyntaxToken token = syntaxNodeOrToken.AsToken();
							foreach (SyntaxTrivia syntaxTrivia in token.LeadingTrivia)
							{
								if (SyntaxNode.IsInSpan(in span, syntaxTrivia.FullSpan))
								{
									yield return syntaxTrivia;
								}
							}
							SyntaxTriviaList.Enumerator enumerator = default(SyntaxTriviaList.Enumerator);
							foreach (SyntaxTrivia syntaxTrivia2 in token.TrailingTrivia)
							{
								if (SyntaxNode.IsInSpan(in span, syntaxTrivia2.FullSpan))
								{
									yield return syntaxTrivia2;
								}
							}
							enumerator = default(SyntaxTriviaList.Enumerator);
							token = default(SyntaxToken);
						}
					}
				}
			}
			SyntaxNode.ChildSyntaxListEnumeratorStack stack = default(SyntaxNode.ChildSyntaxListEnumeratorStack);
			yield break;
			yield break;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0004BCE4 File Offset: 0x00049EE4
		private IEnumerable<SyntaxTrivia> DescendantTriviaIntoTrivia(TextSpan span, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren)
		{
			using (SyntaxNode.TwoEnumeratorListStack stack = new SyntaxNode.TwoEnumeratorListStack(this, descendIntoChildren))
			{
				while (stack.IsNotEmpty)
				{
					SyntaxNode.TwoEnumeratorListStack.Which which = stack.PeekNext();
					SyntaxNodeOrToken syntaxNodeOrToken;
					if (which != SyntaxNode.TwoEnumeratorListStack.Which.Node)
					{
						if (which == SyntaxNode.TwoEnumeratorListStack.Which.Trivia)
						{
							SyntaxTrivia syntaxTrivia;
							if (stack.TryGetNext(out syntaxTrivia))
							{
								SyntaxNode syntaxNode;
								if (syntaxTrivia.TryGetStructure(out syntaxNode))
								{
									stack.PushChildren(syntaxNode, descendIntoChildren);
								}
								if (SyntaxNode.IsInSpan(in span, syntaxTrivia.FullSpan))
								{
									yield return syntaxTrivia;
								}
							}
						}
					}
					else if (stack.TryGetNextInSpan(in span, out syntaxNodeOrToken))
					{
						SyntaxNode syntaxNode2;
						if (syntaxNodeOrToken.AsNode(out syntaxNode2))
						{
							stack.PushChildren(syntaxNode2, descendIntoChildren);
						}
						else if (syntaxNodeOrToken.IsToken)
						{
							SyntaxToken syntaxToken = syntaxNodeOrToken.AsToken();
							if (syntaxToken.HasTrailingTrivia)
							{
								stack.PushTrailingTrivia(in syntaxToken);
							}
							if (syntaxToken.HasLeadingTrivia)
							{
								stack.PushLeadingTrivia(in syntaxToken);
							}
						}
					}
				}
			}
			SyntaxNode.TwoEnumeratorListStack stack = default(SyntaxNode.TwoEnumeratorListStack);
			yield break;
			yield break;
		}

		// Token: 0x04000A12 RID: 2578
		[Nullable(2)]
		private readonly SyntaxNode _parent;

		// Token: 0x04000A13 RID: 2579
		[Nullable(2)]
		internal SyntaxTree _syntaxTree;

		// Token: 0x0200060A RID: 1546
		[Nullable(0)]
		private struct ChildSyntaxListEnumeratorStack : IDisposable
		{
			// Token: 0x06003760 RID: 14176 RVA: 0x0009AD20 File Offset: 0x00098F20
			public ChildSyntaxListEnumeratorStack(SyntaxNode startingNode, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren)
			{
				if (descendIntoChildren == null || descendIntoChildren.Invoke(startingNode))
				{
					this._stack = SyntaxNode.ChildSyntaxListEnumeratorStack.s_stackPool.Allocate();
					this._stackPtr = 0;
					this._stack[0].InitializeFrom(startingNode);
					return;
				}
				this._stack = null;
				this._stackPtr = -1;
			}

			// Token: 0x17000FA0 RID: 4000
			// (get) Token: 0x06003761 RID: 14177 RVA: 0x0009AD71 File Offset: 0x00098F71
			public bool IsNotEmpty
			{
				get
				{
					return this._stackPtr >= 0;
				}
			}

			// Token: 0x06003762 RID: 14178 RVA: 0x0009AD7F File Offset: 0x00098F7F
			public bool TryGetNextInSpan(in TextSpan span, out SyntaxNodeOrToken value)
			{
				while (this._stack[this._stackPtr].TryMoveNextAndGetCurrent(out value))
				{
					if (SyntaxNode.IsInSpan(in span, value.FullSpan))
					{
						return true;
					}
				}
				this._stackPtr--;
				return false;
			}

			// Token: 0x06003763 RID: 14179 RVA: 0x0009ADBC File Offset: 0x00098FBC
			[NullableContext(2)]
			public SyntaxNode TryGetNextAsNodeInSpan(in TextSpan span)
			{
				SyntaxNode syntaxNode;
				while ((syntaxNode = this._stack[this._stackPtr].TryMoveNextAndGetCurrentAsNode()) != null)
				{
					if (SyntaxNode.IsInSpan(in span, syntaxNode.FullSpan))
					{
						return syntaxNode;
					}
				}
				this._stackPtr--;
				return null;
			}

			// Token: 0x06003764 RID: 14180 RVA: 0x0009AE04 File Offset: 0x00099004
			public void PushChildren(SyntaxNode node)
			{
				int num = this._stackPtr + 1;
				this._stackPtr = num;
				if (num >= this._stack.Length)
				{
					Array.Resize<ChildSyntaxList.Enumerator>(ref this._stack, checked(this._stackPtr * 2));
				}
				this._stack[this._stackPtr].InitializeFrom(node);
			}

			// Token: 0x06003765 RID: 14181 RVA: 0x0009AE56 File Offset: 0x00099056
			public void PushChildren(SyntaxNode node, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren)
			{
				if (descendIntoChildren == null || descendIntoChildren.Invoke(node))
				{
					this.PushChildren(node);
				}
			}

			// Token: 0x06003766 RID: 14182 RVA: 0x0009AE6B File Offset: 0x0009906B
			public void Dispose()
			{
				ChildSyntaxList.Enumerator[] stack = this._stack;
				if (stack != null && stack.Length < 256)
				{
					Array.Clear(this._stack, 0, this._stack.Length);
					SyntaxNode.ChildSyntaxListEnumeratorStack.s_stackPool.Free(this._stack);
				}
			}

			// Token: 0x0400178F RID: 6031
			private static readonly ObjectPool<ChildSyntaxList.Enumerator[]> s_stackPool = new ObjectPool<ChildSyntaxList.Enumerator[]>(() => new ChildSyntaxList.Enumerator[16]);

			// Token: 0x04001790 RID: 6032
			[Nullable(2)]
			private ChildSyntaxList.Enumerator[] _stack;

			// Token: 0x04001791 RID: 6033
			private int _stackPtr;
		}

		// Token: 0x0200060B RID: 1547
		[NullableContext(0)]
		private struct TriviaListEnumeratorStack : IDisposable
		{
			// Token: 0x06003768 RID: 14184 RVA: 0x0009AEC5 File Offset: 0x000990C5
			public bool TryGetNext(out SyntaxTrivia value)
			{
				if (this._stack[this._stackPtr].TryMoveNextAndGetCurrent(out value))
				{
					return true;
				}
				this._stackPtr--;
				return false;
			}

			// Token: 0x06003769 RID: 14185 RVA: 0x0009AEF1 File Offset: 0x000990F1
			public void PushLeadingTrivia(in SyntaxToken token)
			{
				this.Grow();
				this._stack[this._stackPtr].InitializeFromLeadingTrivia(in token);
			}

			// Token: 0x0600376A RID: 14186 RVA: 0x0009AF10 File Offset: 0x00099110
			public void PushTrailingTrivia(in SyntaxToken token)
			{
				this.Grow();
				this._stack[this._stackPtr].InitializeFromTrailingTrivia(in token);
			}

			// Token: 0x0600376B RID: 14187 RVA: 0x0009AF30 File Offset: 0x00099130
			private void Grow()
			{
				if (this._stack == null)
				{
					this._stack = SyntaxNode.TriviaListEnumeratorStack.s_stackPool.Allocate();
					this._stackPtr = -1;
				}
				int num = this._stackPtr + 1;
				this._stackPtr = num;
				if (num >= this._stack.Length)
				{
					Array.Resize<SyntaxTriviaList.Enumerator>(ref this._stack, checked(this._stackPtr * 2));
				}
			}

			// Token: 0x0600376C RID: 14188 RVA: 0x0009AF8A File Offset: 0x0009918A
			public void Dispose()
			{
				SyntaxTriviaList.Enumerator[] stack = this._stack;
				if (stack != null && stack.Length < 256)
				{
					Array.Clear(this._stack, 0, this._stack.Length);
					SyntaxNode.TriviaListEnumeratorStack.s_stackPool.Free(this._stack);
				}
			}

			// Token: 0x04001792 RID: 6034
			[Nullable(1)]
			private static readonly ObjectPool<SyntaxTriviaList.Enumerator[]> s_stackPool = new ObjectPool<SyntaxTriviaList.Enumerator[]>(() => new SyntaxTriviaList.Enumerator[16]);

			// Token: 0x04001793 RID: 6035
			[Nullable(1)]
			private SyntaxTriviaList.Enumerator[] _stack;

			// Token: 0x04001794 RID: 6036
			private int _stackPtr;
		}

		// Token: 0x0200060C RID: 1548
		[NullableContext(0)]
		private struct TwoEnumeratorListStack : IDisposable
		{
			// Token: 0x0600376E RID: 14190 RVA: 0x0009AFE4 File Offset: 0x000991E4
			[NullableContext(1)]
			public TwoEnumeratorListStack(SyntaxNode startingNode, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren)
			{
				this._nodeStack = new SyntaxNode.ChildSyntaxListEnumeratorStack(startingNode, descendIntoChildren);
				this._triviaStack = default(SyntaxNode.TriviaListEnumeratorStack);
				if (this._nodeStack.IsNotEmpty)
				{
					this._discriminatorStack = ArrayBuilder<SyntaxNode.TwoEnumeratorListStack.Which>.GetInstance();
					this._discriminatorStack.Push(SyntaxNode.TwoEnumeratorListStack.Which.Node);
					return;
				}
				this._discriminatorStack = null;
			}

			// Token: 0x17000FA1 RID: 4001
			// (get) Token: 0x0600376F RID: 14191 RVA: 0x0009B036 File Offset: 0x00099236
			public bool IsNotEmpty
			{
				get
				{
					ArrayBuilder<SyntaxNode.TwoEnumeratorListStack.Which> discriminatorStack = this._discriminatorStack;
					return discriminatorStack != null && discriminatorStack.Count > 0;
				}
			}

			// Token: 0x06003770 RID: 14192 RVA: 0x0009B04C File Offset: 0x0009924C
			public SyntaxNode.TwoEnumeratorListStack.Which PeekNext()
			{
				return this._discriminatorStack.Peek<SyntaxNode.TwoEnumeratorListStack.Which>();
			}

			// Token: 0x06003771 RID: 14193 RVA: 0x0009B059 File Offset: 0x00099259
			public bool TryGetNextInSpan(in TextSpan span, out SyntaxNodeOrToken value)
			{
				if (this._nodeStack.TryGetNextInSpan(in span, out value))
				{
					return true;
				}
				this._discriminatorStack.Pop<SyntaxNode.TwoEnumeratorListStack.Which>();
				return false;
			}

			// Token: 0x06003772 RID: 14194 RVA: 0x0009B079 File Offset: 0x00099279
			public bool TryGetNext(out SyntaxTrivia value)
			{
				if (this._triviaStack.TryGetNext(out value))
				{
					return true;
				}
				this._discriminatorStack.Pop<SyntaxNode.TwoEnumeratorListStack.Which>();
				return false;
			}

			// Token: 0x06003773 RID: 14195 RVA: 0x0009B098 File Offset: 0x00099298
			[NullableContext(1)]
			public void PushChildren(SyntaxNode node, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren)
			{
				if (descendIntoChildren == null || descendIntoChildren.Invoke(node))
				{
					this._nodeStack.PushChildren(node);
					this._discriminatorStack.Push(SyntaxNode.TwoEnumeratorListStack.Which.Node);
				}
			}

			// Token: 0x06003774 RID: 14196 RVA: 0x0009B0BE File Offset: 0x000992BE
			public void PushLeadingTrivia(in SyntaxToken token)
			{
				this._triviaStack.PushLeadingTrivia(in token);
				this._discriminatorStack.Push(SyntaxNode.TwoEnumeratorListStack.Which.Trivia);
			}

			// Token: 0x06003775 RID: 14197 RVA: 0x0009B0D8 File Offset: 0x000992D8
			public void PushTrailingTrivia(in SyntaxToken token)
			{
				this._triviaStack.PushTrailingTrivia(in token);
				this._discriminatorStack.Push(SyntaxNode.TwoEnumeratorListStack.Which.Trivia);
			}

			// Token: 0x06003776 RID: 14198 RVA: 0x0009B0F2 File Offset: 0x000992F2
			public void Dispose()
			{
				this._nodeStack.Dispose();
				this._triviaStack.Dispose();
				ArrayBuilder<SyntaxNode.TwoEnumeratorListStack.Which> discriminatorStack = this._discriminatorStack;
				if (discriminatorStack == null)
				{
					return;
				}
				discriminatorStack.Free();
			}

			// Token: 0x04001795 RID: 6037
			private SyntaxNode.ChildSyntaxListEnumeratorStack _nodeStack;

			// Token: 0x04001796 RID: 6038
			private SyntaxNode.TriviaListEnumeratorStack _triviaStack;

			// Token: 0x04001797 RID: 6039
			[Nullable(2)]
			private readonly ArrayBuilder<SyntaxNode.TwoEnumeratorListStack.Which> _discriminatorStack;

			// Token: 0x020007C3 RID: 1987
			public enum Which : byte
			{
				// Token: 0x04001DDD RID: 7645
				Node,
				// Token: 0x04001DDE RID: 7646
				Trivia
			}
		}

		// Token: 0x0200060D RID: 1549
		[NullableContext(0)]
		private struct ThreeEnumeratorListStack : IDisposable
		{
			// Token: 0x06003777 RID: 14199 RVA: 0x0009B11C File Offset: 0x0009931C
			[NullableContext(1)]
			public ThreeEnumeratorListStack(SyntaxNode startingNode, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren)
			{
				this._nodeStack = new SyntaxNode.ChildSyntaxListEnumeratorStack(startingNode, descendIntoChildren);
				this._triviaStack = default(SyntaxNode.TriviaListEnumeratorStack);
				if (this._nodeStack.IsNotEmpty)
				{
					this._tokenStack = ArrayBuilder<SyntaxNodeOrToken>.GetInstance();
					this._discriminatorStack = ArrayBuilder<SyntaxNode.ThreeEnumeratorListStack.Which>.GetInstance();
					this._discriminatorStack.Push(SyntaxNode.ThreeEnumeratorListStack.Which.Node);
					return;
				}
				this._tokenStack = null;
				this._discriminatorStack = null;
			}

			// Token: 0x17000FA2 RID: 4002
			// (get) Token: 0x06003778 RID: 14200 RVA: 0x0009B180 File Offset: 0x00099380
			public bool IsNotEmpty
			{
				get
				{
					ArrayBuilder<SyntaxNode.ThreeEnumeratorListStack.Which> discriminatorStack = this._discriminatorStack;
					return discriminatorStack != null && discriminatorStack.Count > 0;
				}
			}

			// Token: 0x06003779 RID: 14201 RVA: 0x0009B196 File Offset: 0x00099396
			public SyntaxNode.ThreeEnumeratorListStack.Which PeekNext()
			{
				return this._discriminatorStack.Peek<SyntaxNode.ThreeEnumeratorListStack.Which>();
			}

			// Token: 0x0600377A RID: 14202 RVA: 0x0009B1A3 File Offset: 0x000993A3
			public bool TryGetNextInSpan(in TextSpan span, out SyntaxNodeOrToken value)
			{
				if (this._nodeStack.TryGetNextInSpan(in span, out value))
				{
					return true;
				}
				this._discriminatorStack.Pop<SyntaxNode.ThreeEnumeratorListStack.Which>();
				return false;
			}

			// Token: 0x0600377B RID: 14203 RVA: 0x0009B1C3 File Offset: 0x000993C3
			public bool TryGetNext(out SyntaxTrivia value)
			{
				if (this._triviaStack.TryGetNext(out value))
				{
					return true;
				}
				this._discriminatorStack.Pop<SyntaxNode.ThreeEnumeratorListStack.Which>();
				return false;
			}

			// Token: 0x0600377C RID: 14204 RVA: 0x0009B1E2 File Offset: 0x000993E2
			public SyntaxNodeOrToken PopToken()
			{
				this._discriminatorStack.Pop<SyntaxNode.ThreeEnumeratorListStack.Which>();
				return this._tokenStack.Pop<SyntaxNodeOrToken>();
			}

			// Token: 0x0600377D RID: 14205 RVA: 0x0009B1FB File Offset: 0x000993FB
			[NullableContext(1)]
			public void PushChildren(SyntaxNode node, [Nullable(new byte[] { 2, 1 })] Func<SyntaxNode, bool> descendIntoChildren)
			{
				if (descendIntoChildren == null || descendIntoChildren.Invoke(node))
				{
					this._nodeStack.PushChildren(node);
					this._discriminatorStack.Push(SyntaxNode.ThreeEnumeratorListStack.Which.Node);
				}
			}

			// Token: 0x0600377E RID: 14206 RVA: 0x0009B221 File Offset: 0x00099421
			public void PushLeadingTrivia(in SyntaxToken token)
			{
				this._triviaStack.PushLeadingTrivia(in token);
				this._discriminatorStack.Push(SyntaxNode.ThreeEnumeratorListStack.Which.Trivia);
			}

			// Token: 0x0600377F RID: 14207 RVA: 0x0009B23B File Offset: 0x0009943B
			public void PushTrailingTrivia(in SyntaxToken token)
			{
				this._triviaStack.PushTrailingTrivia(in token);
				this._discriminatorStack.Push(SyntaxNode.ThreeEnumeratorListStack.Which.Trivia);
			}

			// Token: 0x06003780 RID: 14208 RVA: 0x0009B255 File Offset: 0x00099455
			public void PushToken(in SyntaxNodeOrToken value)
			{
				this._tokenStack.Push(value);
				this._discriminatorStack.Push(SyntaxNode.ThreeEnumeratorListStack.Which.Token);
			}

			// Token: 0x06003781 RID: 14209 RVA: 0x0009B274 File Offset: 0x00099474
			public void Dispose()
			{
				this._nodeStack.Dispose();
				this._triviaStack.Dispose();
				ArrayBuilder<SyntaxNodeOrToken> tokenStack = this._tokenStack;
				if (tokenStack != null)
				{
					tokenStack.Free();
				}
				ArrayBuilder<SyntaxNode.ThreeEnumeratorListStack.Which> discriminatorStack = this._discriminatorStack;
				if (discriminatorStack == null)
				{
					return;
				}
				discriminatorStack.Free();
			}

			// Token: 0x04001798 RID: 6040
			private SyntaxNode.ChildSyntaxListEnumeratorStack _nodeStack;

			// Token: 0x04001799 RID: 6041
			private SyntaxNode.TriviaListEnumeratorStack _triviaStack;

			// Token: 0x0400179A RID: 6042
			[Nullable(2)]
			private readonly ArrayBuilder<SyntaxNodeOrToken> _tokenStack;

			// Token: 0x0400179B RID: 6043
			[Nullable(2)]
			private readonly ArrayBuilder<SyntaxNode.ThreeEnumeratorListStack.Which> _discriminatorStack;

			// Token: 0x020007C4 RID: 1988
			public enum Which : byte
			{
				// Token: 0x04001DE0 RID: 7648
				Node,
				// Token: 0x04001DE1 RID: 7649
				Trivia,
				// Token: 0x04001DE2 RID: 7650
				Token
			}
		}
	}
}
