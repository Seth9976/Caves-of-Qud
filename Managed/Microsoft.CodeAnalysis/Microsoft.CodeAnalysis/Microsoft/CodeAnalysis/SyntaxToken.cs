using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000282 RID: 642
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	[StructLayout(3)]
	public readonly struct SyntaxToken : IEquatable<SyntaxToken>
	{
		// Token: 0x060019DE RID: 6622 RVA: 0x0004D657 File Offset: 0x0004B857
		[NullableContext(2)]
		internal SyntaxToken(SyntaxNode parent, GreenNode token, int position, int index)
		{
			this.Parent = parent;
			this.Node = token;
			this.Position = position;
			this.Index = index;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0004D676 File Offset: 0x0004B876
		[NullableContext(2)]
		internal SyntaxToken(GreenNode token)
		{
			this = default(SyntaxToken);
			this.Node = token;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0004D688 File Offset: 0x0004B888
		private string GetDebuggerDisplay()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				" ",
				(this.Node != null) ? this.Node.KindText : "None",
				" ",
				this.ToString()
			});
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0004D6F4 File Offset: 0x0004B8F4
		public int RawKind
		{
			get
			{
				GreenNode node = this.Node;
				if (node == null)
				{
					return 0;
				}
				return node.RawKind;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x0004D707 File Offset: 0x0004B907
		public string Language
		{
			get
			{
				GreenNode node = this.Node;
				return ((node != null) ? node.Language : null) ?? string.Empty;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0004D724 File Offset: 0x0004B924
		internal int RawContextualKind
		{
			get
			{
				GreenNode node = this.Node;
				if (node == null)
				{
					return 0;
				}
				return node.RawContextualKind;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x0004D737 File Offset: 0x0004B937
		[Nullable(2)]
		public SyntaxNode Parent
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0004D73F File Offset: 0x0004B93F
		[Nullable(2)]
		internal GreenNode Node
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x0004D747 File Offset: 0x0004B947
		internal GreenNode RequiredNode
		{
			get
			{
				return this.Node;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x0004D74F File Offset: 0x0004B94F
		internal int Index { get; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x0004D757 File Offset: 0x0004B957
		internal int Position { get; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x0004D75F File Offset: 0x0004B95F
		internal int Width
		{
			get
			{
				GreenNode node = this.Node;
				if (node == null)
				{
					return 0;
				}
				return node.Width;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x0004D772 File Offset: 0x0004B972
		internal int FullWidth
		{
			get
			{
				GreenNode node = this.Node;
				if (node == null)
				{
					return 0;
				}
				return node.FullWidth;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0004D788 File Offset: 0x0004B988
		public TextSpan Span
		{
			get
			{
				if (this.Node == null)
				{
					return default(TextSpan);
				}
				return new TextSpan(this.Position + this.Node.GetLeadingTriviaWidth(), this.Node.Width);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x0004D7C9 File Offset: 0x0004B9C9
		internal int EndPosition
		{
			get
			{
				if (this.Node == null)
				{
					return 0;
				}
				return this.Position + this.Node.FullWidth;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x0004D7E7 File Offset: 0x0004B9E7
		public int SpanStart
		{
			get
			{
				if (this.Node == null)
				{
					return 0;
				}
				return this.Position + this.Node.GetLeadingTriviaWidth();
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x0004D805 File Offset: 0x0004BA05
		public TextSpan FullSpan
		{
			get
			{
				return new TextSpan(this.Position, this.FullWidth);
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x0004D818 File Offset: 0x0004BA18
		public bool IsMissing
		{
			get
			{
				GreenNode node = this.Node;
				return node != null && node.IsMissing;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x0004D82B File Offset: 0x0004BA2B
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				GreenNode node = this.Node;
				if (node == null)
				{
					return null;
				}
				return node.GetValue();
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x0004D83E File Offset: 0x0004BA3E
		public string ValueText
		{
			get
			{
				GreenNode node = this.Node;
				return ((node != null) ? node.GetValueText() : null) ?? string.Empty;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0004D85B File Offset: 0x0004BA5B
		public string Text
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0004D869 File Offset: 0x0004BA69
		public override string ToString()
		{
			if (this.Node == null)
			{
				return string.Empty;
			}
			return this.Node.ToString();
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0004D884 File Offset: 0x0004BA84
		public string ToFullString()
		{
			if (this.Node == null)
			{
				return string.Empty;
			}
			return this.Node.ToFullString();
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0004D89F File Offset: 0x0004BA9F
		public void WriteTo(TextWriter writer)
		{
			GreenNode node = this.Node;
			if (node == null)
			{
				return;
			}
			node.WriteTo(writer);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0004D8B2 File Offset: 0x0004BAB2
		internal void WriteTo(TextWriter writer, bool leading, bool trailing)
		{
			GreenNode node = this.Node;
			if (node == null)
			{
				return;
			}
			node.WriteTo(writer, leading, trailing);
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0004D8C8 File Offset: 0x0004BAC8
		public bool HasLeadingTrivia
		{
			get
			{
				return this.LeadingTrivia.Count > 0;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0004D8E8 File Offset: 0x0004BAE8
		public bool HasTrailingTrivia
		{
			get
			{
				return this.TrailingTrivia.Count > 0;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0004D906 File Offset: 0x0004BB06
		internal int LeadingWidth
		{
			get
			{
				GreenNode node = this.Node;
				if (node == null)
				{
					return 0;
				}
				return node.GetLeadingTriviaWidth();
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x0004D919 File Offset: 0x0004BB19
		internal int TrailingWidth
		{
			get
			{
				GreenNode node = this.Node;
				if (node == null)
				{
					return 0;
				}
				return node.GetTrailingTriviaWidth();
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0004D92C File Offset: 0x0004BB2C
		public bool ContainsDiagnostics
		{
			get
			{
				GreenNode node = this.Node;
				return node != null && node.ContainsDiagnostics;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x0004D93F File Offset: 0x0004BB3F
		public bool ContainsDirectives
		{
			get
			{
				GreenNode node = this.Node;
				return node != null && node.ContainsDirectives;
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0004D952 File Offset: 0x0004BB52
		public bool IsPartOfStructuredTrivia()
		{
			SyntaxNode parent = this.Parent;
			return parent != null && parent.IsPartOfStructuredTrivia();
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x0004D965 File Offset: 0x0004BB65
		public bool HasStructuredTrivia
		{
			get
			{
				GreenNode node = this.Node;
				return node != null && node.ContainsStructuredTrivia;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0004D978 File Offset: 0x0004BB78
		public bool ContainsAnnotations
		{
			get
			{
				GreenNode node = this.Node;
				return node != null && node.ContainsAnnotations;
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0004D98B File Offset: 0x0004BB8B
		public bool HasAnnotations(string annotationKind)
		{
			GreenNode node = this.Node;
			return node != null && node.HasAnnotations(annotationKind);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0004D99F File Offset: 0x0004BB9F
		public bool HasAnnotations(params string[] annotationKinds)
		{
			GreenNode node = this.Node;
			return node != null && node.HasAnnotations(annotationKinds);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0004D9B3 File Offset: 0x0004BBB3
		[NullableContext(2)]
		public bool HasAnnotation([NotNullWhen(true)] SyntaxAnnotation annotation)
		{
			GreenNode node = this.Node;
			return node != null && node.HasAnnotation(annotation);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0004D9C7 File Offset: 0x0004BBC7
		public IEnumerable<SyntaxAnnotation> GetAnnotations(string annotationKind)
		{
			GreenNode node = this.Node;
			return ((node != null) ? node.GetAnnotations(annotationKind) : null) ?? SpecializedCollections.EmptyEnumerable<SyntaxAnnotation>();
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0004D9E5 File Offset: 0x0004BBE5
		public IEnumerable<SyntaxAnnotation> GetAnnotations(params string[] annotationKinds)
		{
			return this.GetAnnotations(annotationKinds);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0004D9EE File Offset: 0x0004BBEE
		public IEnumerable<SyntaxAnnotation> GetAnnotations(IEnumerable<string> annotationKinds)
		{
			GreenNode node = this.Node;
			return ((node != null) ? node.GetAnnotations(annotationKinds) : null) ?? SpecializedCollections.EmptyEnumerable<SyntaxAnnotation>();
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0004DA0C File Offset: 0x0004BC0C
		public SyntaxToken WithAdditionalAnnotations(params SyntaxAnnotation[] annotations)
		{
			return this.WithAdditionalAnnotations(annotations);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0004DA18 File Offset: 0x0004BC18
		public SyntaxToken WithAdditionalAnnotations(IEnumerable<SyntaxAnnotation> annotations)
		{
			if (annotations == null)
			{
				throw new ArgumentNullException("annotations");
			}
			if (this.Node != null)
			{
				return new SyntaxToken(null, this.Node.WithAdditionalAnnotationsGreen(annotations), 0, 0);
			}
			return default(SyntaxToken);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0004DA59 File Offset: 0x0004BC59
		public SyntaxToken WithoutAnnotations(params SyntaxAnnotation[] annotations)
		{
			return this.WithoutAnnotations(annotations);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0004DA64 File Offset: 0x0004BC64
		public SyntaxToken WithoutAnnotations(IEnumerable<SyntaxAnnotation> annotations)
		{
			if (annotations == null)
			{
				throw new ArgumentNullException("annotations");
			}
			if (this.Node != null)
			{
				return new SyntaxToken(null, this.Node.WithoutAnnotationsGreen(annotations), 0, 0);
			}
			return default(SyntaxToken);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0004DAA5 File Offset: 0x0004BCA5
		public SyntaxToken WithoutAnnotations(string annotationKind)
		{
			if (annotationKind == null)
			{
				throw new ArgumentNullException("annotationKind");
			}
			if (this.HasAnnotations(annotationKind))
			{
				return this.WithoutAnnotations(this.GetAnnotations(annotationKind));
			}
			return this;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0004DAD4 File Offset: 0x0004BCD4
		public SyntaxToken CopyAnnotationsTo(SyntaxToken token)
		{
			if (token.Node == null)
			{
				return default(SyntaxToken);
			}
			if (this.Node == null)
			{
				return token;
			}
			SyntaxAnnotation[] annotations = this.Node.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				return new SyntaxToken(null, token.Node.WithAdditionalAnnotationsGreen(annotations), 0, 0);
			}
			return token;
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x0004DB28 File Offset: 0x0004BD28
		public SyntaxTriviaList LeadingTrivia
		{
			get
			{
				if (this.Node == null)
				{
					return default(SyntaxTriviaList);
				}
				return new SyntaxTriviaList(in this, this.Node.GetLeadingTriviaCore(), this.Position, 0);
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0004DB60 File Offset: 0x0004BD60
		public SyntaxTriviaList TrailingTrivia
		{
			get
			{
				if (this.Node == null)
				{
					return default(SyntaxTriviaList);
				}
				GreenNode leadingTriviaCore = this.Node.GetLeadingTriviaCore();
				int num = 0;
				if (leadingTriviaCore != null)
				{
					num = (leadingTriviaCore.IsList ? leadingTriviaCore.SlotCount : 1);
				}
				GreenNode trailingTriviaCore = this.Node.GetTrailingTriviaCore();
				int num2 = this.Position + this.FullWidth;
				if (trailingTriviaCore != null)
				{
					num2 -= trailingTriviaCore.FullWidth;
				}
				return new SyntaxTriviaList(in this, trailingTriviaCore, num2, num);
			}
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0004DBD4 File Offset: 0x0004BDD4
		public SyntaxToken WithTriviaFrom(SyntaxToken token)
		{
			return this.WithLeadingTrivia(token.LeadingTrivia).WithTrailingTrivia(token.TrailingTrivia);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0004DBFD File Offset: 0x0004BDFD
		public SyntaxToken WithLeadingTrivia(SyntaxTriviaList trivia)
		{
			return this.WithLeadingTrivia(trivia);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0004DC0B File Offset: 0x0004BE0B
		[NullableContext(2)]
		public SyntaxToken WithLeadingTrivia(params SyntaxTrivia[] trivia)
		{
			return this.WithLeadingTrivia(trivia);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0004DC14 File Offset: 0x0004BE14
		[NullableContext(2)]
		public SyntaxToken WithLeadingTrivia(IEnumerable<SyntaxTrivia> trivia)
		{
			if (this.Node == null)
			{
				return default(SyntaxToken);
			}
			return new SyntaxToken(null, this.Node.WithLeadingTrivia(GreenNode.CreateList<SyntaxTrivia>(trivia, (SyntaxTrivia t) => t.RequiredUnderlyingNode)), 0, 0);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0004DC6B File Offset: 0x0004BE6B
		public SyntaxToken WithTrailingTrivia(SyntaxTriviaList trivia)
		{
			return this.WithTrailingTrivia(trivia);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0004DC79 File Offset: 0x0004BE79
		[NullableContext(2)]
		public SyntaxToken WithTrailingTrivia(params SyntaxTrivia[] trivia)
		{
			return this.WithTrailingTrivia(trivia);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0004DC84 File Offset: 0x0004BE84
		[NullableContext(2)]
		public SyntaxToken WithTrailingTrivia(IEnumerable<SyntaxTrivia> trivia)
		{
			if (this.Node == null)
			{
				return default(SyntaxToken);
			}
			return new SyntaxToken(null, this.Node.WithTrailingTrivia(GreenNode.CreateList<SyntaxTrivia>(trivia, (SyntaxTrivia t) => t.RequiredUnderlyingNode)), 0, 0);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0004DCDC File Offset: 0x0004BEDC
		public IEnumerable<SyntaxTrivia> GetAllTrivia()
		{
			if (this.HasLeadingTrivia)
			{
				if (this.HasTrailingTrivia)
				{
					return Enumerable.Concat<SyntaxTrivia>(this.LeadingTrivia, this.TrailingTrivia);
				}
				return this.LeadingTrivia;
			}
			else
			{
				if (this.HasTrailingTrivia)
				{
					return this.TrailingTrivia;
				}
				return SpecializedCollections.EmptyEnumerable<SyntaxTrivia>();
			}
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0004DD3A File Offset: 0x0004BF3A
		public static bool operator ==(SyntaxToken left, SyntaxToken right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0004DD44 File Offset: 0x0004BF44
		public static bool operator !=(SyntaxToken left, SyntaxToken right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0004DD51 File Offset: 0x0004BF51
		public bool Equals(SyntaxToken other)
		{
			return this.Parent == other.Parent && this.Node == other.Node && this.Position == other.Position && this.Index == other.Index;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0004DD91 File Offset: 0x0004BF91
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is SyntaxToken && this.Equals((SyntaxToken)obj);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0004DDA9 File Offset: 0x0004BFA9
		public override int GetHashCode()
		{
			return Hash.Combine<SyntaxNode>(this.Parent, Hash.Combine<GreenNode>(this.Node, Hash.Combine(this.Position, this.Index)));
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0004DDD4 File Offset: 0x0004BFD4
		public SyntaxToken GetNextToken(bool includeZeroWidth = false, bool includeSkipped = false, bool includeDirectives = false, bool includeDocumentationComments = false)
		{
			if (this.Node == null)
			{
				return default(SyntaxToken);
			}
			return SyntaxNavigator.Instance.GetNextToken(in this, includeZeroWidth, includeSkipped, includeDirectives, includeDocumentationComments);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0004DE04 File Offset: 0x0004C004
		internal SyntaxToken GetNextToken(Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto = null)
		{
			if (this.Node == null)
			{
				return default(SyntaxToken);
			}
			return SyntaxNavigator.Instance.GetNextToken(in this, predicate, stepInto);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0004DE30 File Offset: 0x0004C030
		public SyntaxToken GetPreviousToken(bool includeZeroWidth = false, bool includeSkipped = false, bool includeDirectives = false, bool includeDocumentationComments = false)
		{
			if (this.Node == null)
			{
				return default(SyntaxToken);
			}
			return SyntaxNavigator.Instance.GetPreviousToken(in this, includeZeroWidth, includeSkipped, includeDirectives, includeDocumentationComments);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0004DE5F File Offset: 0x0004C05F
		internal SyntaxToken GetPreviousToken(Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto = null)
		{
			return SyntaxNavigator.Instance.GetPreviousToken(in this, predicate, stepInto);
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0004DE6E File Offset: 0x0004C06E
		[Nullable(2)]
		public SyntaxTree SyntaxTree
		{
			[NullableContext(2)]
			get
			{
				SyntaxNode parent = this.Parent;
				if (parent == null)
				{
					return null;
				}
				return parent.SyntaxTree;
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0004DE84 File Offset: 0x0004C084
		public Location GetLocation()
		{
			SyntaxTree syntaxTree = this.SyntaxTree;
			if (syntaxTree != null)
			{
				return syntaxTree.GetLocation(this.Span);
			}
			return Location.None;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0004DEB0 File Offset: 0x0004C0B0
		public IEnumerable<Diagnostic> GetDiagnostics()
		{
			if (this.Node == null)
			{
				return SpecializedCollections.EmptyEnumerable<Diagnostic>();
			}
			SyntaxTree syntaxTree = this.SyntaxTree;
			if (syntaxTree != null)
			{
				return syntaxTree.GetDiagnostics(this);
			}
			DiagnosticInfo[] diagnostics = this.Node.GetDiagnostics();
			if (diagnostics.Length != 0)
			{
				return Enumerable.Select<DiagnosticInfo, Diagnostic>(diagnostics, SyntaxToken.s_createDiagnosticWithoutLocation);
			}
			return SpecializedCollections.EmptyEnumerable<Diagnostic>();
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0004DF05 File Offset: 0x0004C105
		public bool IsEquivalentTo(SyntaxToken token)
		{
			return (this.Node == null && token.Node == null) || (this.Node != null && token.Node != null && this.Node.IsEquivalentTo(token.Node));
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0004DF3F File Offset: 0x0004C13F
		public bool IsIncrementallyIdenticalTo(SyntaxToken token)
		{
			return this.Node != null && this.Node == token.Node;
		}

		// Token: 0x04000A2B RID: 2603
		private static readonly Func<DiagnosticInfo, Diagnostic> s_createDiagnosticWithoutLocation = new Func<DiagnosticInfo, Diagnostic>(Diagnostic.Create);

		// Token: 0x04000A2C RID: 2604
		internal static readonly Func<SyntaxToken, bool> NonZeroWidth = (SyntaxToken t) => t.Width > 0;

		// Token: 0x04000A2D RID: 2605
		internal static readonly Func<SyntaxToken, bool> Any = (SyntaxToken t) => true;
	}
}
