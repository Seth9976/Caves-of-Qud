using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200027E RID: 638
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	[StructLayout(3)]
	public readonly struct SyntaxNodeOrToken : IEquatable<SyntaxNodeOrToken>
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x0004C335 File Offset: 0x0004A535
		internal SyntaxNodeOrToken(SyntaxNode node)
		{
			this = default(SyntaxNodeOrToken);
			this._position = node.Position;
			this._nodeOrParent = node;
			this._tokenIndex = -1;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0004C358 File Offset: 0x0004A558
		[NullableContext(2)]
		internal SyntaxNodeOrToken(SyntaxNode parent, GreenNode token, int position, int index)
		{
			this._position = position;
			this._tokenIndex = index;
			this._nodeOrParent = parent;
			this._token = token;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0004C378 File Offset: 0x0004A578
		internal string GetDebuggerDisplay()
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

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x0004C3D0 File Offset: 0x0004A5D0
		private string KindText
		{
			get
			{
				if (this._token != null)
				{
					return this._token.KindText;
				}
				if (this._nodeOrParent != null)
				{
					return this._nodeOrParent.Green.KindText;
				}
				return "None";
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0004C404 File Offset: 0x0004A604
		public int RawKind
		{
			get
			{
				GreenNode token = this._token;
				if (token != null)
				{
					return token.RawKind;
				}
				SyntaxNode nodeOrParent = this._nodeOrParent;
				if (nodeOrParent == null)
				{
					return 0;
				}
				return nodeOrParent.RawKind;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0004C427 File Offset: 0x0004A627
		public string Language
		{
			get
			{
				if (this._token != null)
				{
					return this._token.Language;
				}
				if (this._nodeOrParent != null)
				{
					return this._nodeOrParent.Language;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0004C456 File Offset: 0x0004A656
		public bool IsMissing
		{
			get
			{
				GreenNode token = this._token;
				if (token == null)
				{
					SyntaxNode nodeOrParent = this._nodeOrParent;
					return nodeOrParent != null && nodeOrParent.IsMissing;
				}
				return token.IsMissing;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0004C479 File Offset: 0x0004A679
		[Nullable(2)]
		public SyntaxNode Parent
		{
			[NullableContext(2)]
			get
			{
				if (this._token != null)
				{
					return this._nodeOrParent;
				}
				SyntaxNode nodeOrParent = this._nodeOrParent;
				if (nodeOrParent == null)
				{
					return null;
				}
				return nodeOrParent.Parent;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0004C49B File Offset: 0x0004A69B
		[Nullable(2)]
		internal GreenNode UnderlyingNode
		{
			[NullableContext(2)]
			get
			{
				GreenNode greenNode;
				if ((greenNode = this._token) == null)
				{
					SyntaxNode nodeOrParent = this._nodeOrParent;
					if (nodeOrParent == null)
					{
						return null;
					}
					greenNode = nodeOrParent.Green;
				}
				return greenNode;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0004C4B8 File Offset: 0x0004A6B8
		internal int Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x0004C4C0 File Offset: 0x0004A6C0
		internal GreenNode RequiredUnderlyingNode
		{
			get
			{
				return this.UnderlyingNode;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0004C4C8 File Offset: 0x0004A6C8
		public bool IsToken
		{
			get
			{
				return !this.IsNode;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x0004C4D3 File Offset: 0x0004A6D3
		public bool IsNode
		{
			get
			{
				return this._tokenIndex < 0;
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0004C4E0 File Offset: 0x0004A6E0
		public SyntaxToken AsToken()
		{
			if (this._token != null)
			{
				return new SyntaxToken(this._nodeOrParent, this._token, this.Position, this._tokenIndex);
			}
			return default(SyntaxToken);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0004C51C File Offset: 0x0004A71C
		internal bool AsToken(out SyntaxToken token)
		{
			if (this.IsToken)
			{
				token = this.AsToken();
				return true;
			}
			token = default(SyntaxToken);
			return false;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0004C53C File Offset: 0x0004A73C
		[NullableContext(2)]
		public SyntaxNode AsNode()
		{
			if (this._token != null)
			{
				return null;
			}
			return this._nodeOrParent;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0004C54E File Offset: 0x0004A74E
		[NullableContext(2)]
		internal bool AsNode([NotNullWhen(true)] out SyntaxNode node)
		{
			if (this.IsNode)
			{
				node = this._nodeOrParent;
				return node != null;
			}
			node = null;
			return false;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0004C56C File Offset: 0x0004A76C
		public ChildSyntaxList ChildNodesAndTokens()
		{
			SyntaxNode syntaxNode;
			if (this.AsNode(out syntaxNode))
			{
				return syntaxNode.ChildNodesAndTokens();
			}
			return default(ChildSyntaxList);
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x0004C594 File Offset: 0x0004A794
		public TextSpan Span
		{
			get
			{
				if (this._token != null)
				{
					return this.AsToken().Span;
				}
				if (this._nodeOrParent != null)
				{
					return this._nodeOrParent.Span;
				}
				return default(TextSpan);
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x0004C5D5 File Offset: 0x0004A7D5
		public int SpanStart
		{
			get
			{
				if (this._token != null)
				{
					return this._position + this._token.GetLeadingTriviaWidth();
				}
				if (this._nodeOrParent != null)
				{
					return this._nodeOrParent.SpanStart;
				}
				return 0;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x0004C608 File Offset: 0x0004A808
		public TextSpan FullSpan
		{
			get
			{
				if (this._token != null)
				{
					return new TextSpan(this.Position, this._token.FullWidth);
				}
				if (this._nodeOrParent != null)
				{
					return this._nodeOrParent.FullSpan;
				}
				return default(TextSpan);
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0004C651 File Offset: 0x0004A851
		public override string ToString()
		{
			if (this._token != null)
			{
				return this._token.ToString();
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0004C680 File Offset: 0x0004A880
		public string ToFullString()
		{
			if (this._token != null)
			{
				return this._token.ToFullString();
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.ToFullString();
			}
			return string.Empty;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0004C6AF File Offset: 0x0004A8AF
		public void WriteTo(TextWriter writer)
		{
			if (this._token != null)
			{
				this._token.WriteTo(writer);
				return;
			}
			SyntaxNode nodeOrParent = this._nodeOrParent;
			if (nodeOrParent == null)
			{
				return;
			}
			nodeOrParent.WriteTo(writer);
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0004C6D8 File Offset: 0x0004A8D8
		public bool HasLeadingTrivia
		{
			get
			{
				return this.GetLeadingTrivia().Count > 0;
			}
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0004C6F8 File Offset: 0x0004A8F8
		public SyntaxTriviaList GetLeadingTrivia()
		{
			if (this._token != null)
			{
				return this.AsToken().LeadingTrivia;
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.GetLeadingTrivia();
			}
			return default(SyntaxTriviaList);
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0004C73C File Offset: 0x0004A93C
		public bool HasTrailingTrivia
		{
			get
			{
				return this.GetTrailingTrivia().Count > 0;
			}
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0004C75C File Offset: 0x0004A95C
		public SyntaxTriviaList GetTrailingTrivia()
		{
			if (this._token != null)
			{
				return this.AsToken().TrailingTrivia;
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.GetTrailingTrivia();
			}
			return default(SyntaxTriviaList);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0004C7A0 File Offset: 0x0004A9A0
		public SyntaxNodeOrToken WithLeadingTrivia(IEnumerable<SyntaxTrivia> trivia)
		{
			if (this._token != null)
			{
				return this.AsToken().WithLeadingTrivia(trivia);
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.WithLeadingTrivia(trivia);
			}
			return this;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0004C7EA File Offset: 0x0004A9EA
		public SyntaxNodeOrToken WithLeadingTrivia(params SyntaxTrivia[] trivia)
		{
			return this.WithLeadingTrivia(trivia);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0004C7F4 File Offset: 0x0004A9F4
		public SyntaxNodeOrToken WithTrailingTrivia(IEnumerable<SyntaxTrivia> trivia)
		{
			if (this._token != null)
			{
				return this.AsToken().WithTrailingTrivia(trivia);
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.WithTrailingTrivia(trivia);
			}
			return this;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0004C83E File Offset: 0x0004AA3E
		public SyntaxNodeOrToken WithTrailingTrivia(params SyntaxTrivia[] trivia)
		{
			return this.WithTrailingTrivia(trivia);
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x0004C847 File Offset: 0x0004AA47
		public bool ContainsDiagnostics
		{
			get
			{
				if (this._token != null)
				{
					return this._token.ContainsDiagnostics;
				}
				return this._nodeOrParent != null && this._nodeOrParent.ContainsDiagnostics;
			}
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0004C874 File Offset: 0x0004AA74
		public IEnumerable<Diagnostic> GetDiagnostics()
		{
			if (this._token != null)
			{
				return this.AsToken().GetDiagnostics();
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.GetDiagnostics();
			}
			return SpecializedCollections.EmptyEnumerable<Diagnostic>();
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x0004C8B1 File Offset: 0x0004AAB1
		public bool ContainsDirectives
		{
			get
			{
				if (this._token != null)
				{
					return this._token.ContainsDirectives;
				}
				return this._nodeOrParent != null && this._nodeOrParent.ContainsDirectives;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0004C8DC File Offset: 0x0004AADC
		public bool ContainsAnnotations
		{
			get
			{
				if (this._token != null)
				{
					return this._token.ContainsAnnotations;
				}
				return this._nodeOrParent != null && this._nodeOrParent.ContainsAnnotations;
			}
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0004C907 File Offset: 0x0004AB07
		public bool HasAnnotations(string annotationKind)
		{
			if (this._token != null)
			{
				return this._token.HasAnnotations(annotationKind);
			}
			return this._nodeOrParent != null && this._nodeOrParent.HasAnnotations(annotationKind);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0004C934 File Offset: 0x0004AB34
		public bool HasAnnotations(IEnumerable<string> annotationKinds)
		{
			if (this._token != null)
			{
				return this._token.HasAnnotations(annotationKinds);
			}
			return this._nodeOrParent != null && this._nodeOrParent.HasAnnotations(annotationKinds);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0004C961 File Offset: 0x0004AB61
		[NullableContext(2)]
		public bool HasAnnotation([NotNullWhen(true)] SyntaxAnnotation annotation)
		{
			if (this._token != null)
			{
				return this._token.HasAnnotation(annotation);
			}
			return this._nodeOrParent != null && this._nodeOrParent.HasAnnotation(annotation);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0004C98E File Offset: 0x0004AB8E
		public IEnumerable<SyntaxAnnotation> GetAnnotations(string annotationKind)
		{
			if (this._token != null)
			{
				return this._token.GetAnnotations(annotationKind);
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.GetAnnotations(annotationKind);
			}
			return SpecializedCollections.EmptyEnumerable<SyntaxAnnotation>();
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0004C9BF File Offset: 0x0004ABBF
		public IEnumerable<SyntaxAnnotation> GetAnnotations(IEnumerable<string> annotationKinds)
		{
			if (this._token != null)
			{
				return this._token.GetAnnotations(annotationKinds);
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.GetAnnotations(annotationKinds);
			}
			return SpecializedCollections.EmptyEnumerable<SyntaxAnnotation>();
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0004C9F0 File Offset: 0x0004ABF0
		public SyntaxNodeOrToken WithAdditionalAnnotations(params SyntaxAnnotation[] annotations)
		{
			return this.WithAdditionalAnnotations(annotations);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0004C9FC File Offset: 0x0004ABFC
		public SyntaxNodeOrToken WithAdditionalAnnotations(IEnumerable<SyntaxAnnotation> annotations)
		{
			if (annotations == null)
			{
				throw new ArgumentNullException("annotations");
			}
			if (this._token != null)
			{
				return this.AsToken().WithAdditionalAnnotations(annotations);
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.WithAdditionalAnnotations(annotations);
			}
			return this;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0004CA54 File Offset: 0x0004AC54
		public SyntaxNodeOrToken WithoutAnnotations(params SyntaxAnnotation[] annotations)
		{
			return this.WithoutAnnotations(annotations);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0004CA60 File Offset: 0x0004AC60
		public SyntaxNodeOrToken WithoutAnnotations(IEnumerable<SyntaxAnnotation> annotations)
		{
			if (annotations == null)
			{
				throw new ArgumentNullException("annotations");
			}
			if (this._token != null)
			{
				return this.AsToken().WithoutAnnotations(annotations);
			}
			if (this._nodeOrParent != null)
			{
				return this._nodeOrParent.WithoutAnnotations(annotations);
			}
			return this;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		public SyntaxNodeOrToken WithoutAnnotations(string annotationKind)
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

		// Token: 0x06001997 RID: 6551 RVA: 0x0004CAE5 File Offset: 0x0004ACE5
		public bool Equals(SyntaxNodeOrToken other)
		{
			return this._nodeOrParent == other._nodeOrParent && this._token == other._token && this._tokenIndex == other._tokenIndex;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0004CB13 File Offset: 0x0004AD13
		public static bool operator ==(SyntaxNodeOrToken left, SyntaxNodeOrToken right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0004CB1D File Offset: 0x0004AD1D
		public static bool operator !=(SyntaxNodeOrToken left, SyntaxNodeOrToken right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0004CB2C File Offset: 0x0004AD2C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is SyntaxNodeOrToken)
			{
				SyntaxNodeOrToken syntaxNodeOrToken = (SyntaxNodeOrToken)obj;
				return this.Equals(syntaxNodeOrToken);
			}
			return false;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0004CB51 File Offset: 0x0004AD51
		public override int GetHashCode()
		{
			return Hash.Combine<SyntaxNode>(this._nodeOrParent, Hash.Combine<GreenNode>(this._token, this._tokenIndex));
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0004CB70 File Offset: 0x0004AD70
		public bool IsEquivalentTo(SyntaxNodeOrToken other)
		{
			if (this.IsNode != other.IsNode)
			{
				return false;
			}
			GreenNode underlyingNode = this.UnderlyingNode;
			GreenNode underlyingNode2 = other.UnderlyingNode;
			return underlyingNode == underlyingNode2 || (underlyingNode != null && underlyingNode.IsEquivalentTo(underlyingNode2));
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0004CBAF File Offset: 0x0004ADAF
		public bool IsIncrementallyIdenticalTo(SyntaxNodeOrToken other)
		{
			return this.UnderlyingNode != null && this.UnderlyingNode == other.UnderlyingNode;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0004CBCA File Offset: 0x0004ADCA
		public static implicit operator SyntaxNodeOrToken(SyntaxToken token)
		{
			return new SyntaxNodeOrToken(token.Parent, token.Node, token.Position, token.Index);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0004CBED File Offset: 0x0004ADED
		public static explicit operator SyntaxToken(SyntaxNodeOrToken nodeOrToken)
		{
			return nodeOrToken.AsToken();
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0004CBF8 File Offset: 0x0004ADF8
		[NullableContext(2)]
		public static implicit operator SyntaxNodeOrToken(SyntaxNode node)
		{
			if (node == null)
			{
				return default(SyntaxNodeOrToken);
			}
			return new SyntaxNodeOrToken(node);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0004CC18 File Offset: 0x0004AE18
		[NullableContext(2)]
		public static explicit operator SyntaxNode(SyntaxNodeOrToken nodeOrToken)
		{
			return nodeOrToken.AsNode();
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x0004CC21 File Offset: 0x0004AE21
		[Nullable(2)]
		public SyntaxTree SyntaxTree
		{
			[NullableContext(2)]
			get
			{
				SyntaxNode nodeOrParent = this._nodeOrParent;
				if (nodeOrParent == null)
				{
					return null;
				}
				return nodeOrParent.SyntaxTree;
			}
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0004CC34 File Offset: 0x0004AE34
		[NullableContext(2)]
		public Location GetLocation()
		{
			SyntaxToken syntaxToken;
			if (this.AsToken(out syntaxToken))
			{
				return syntaxToken.GetLocation();
			}
			SyntaxNode nodeOrParent = this._nodeOrParent;
			if (nodeOrParent == null)
			{
				return null;
			}
			return nodeOrParent.GetLocation();
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0004CC64 File Offset: 0x0004AE64
		internal IList<TDirective> GetDirectives<[Nullable(0)] TDirective>([Nullable(new byte[] { 2, 1 })] Func<TDirective, bool> filter = null) where TDirective : SyntaxNode
		{
			List<TDirective> list = null;
			SyntaxNodeOrToken.GetDirectives<TDirective>(in this, filter, ref list);
			IList<TDirective> list2 = list;
			return list2 ?? SpecializedCollections.EmptyList<TDirective>();
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0004CC88 File Offset: 0x0004AE88
		[NullableContext(0)]
		private static void GetDirectives<TDirective>(in SyntaxNodeOrToken node, [Nullable(new byte[] { 2, 1 })] Func<TDirective, bool> filter, [Nullable(new byte[] { 2, 1 })] ref List<TDirective> directives) where TDirective : SyntaxNode
		{
			if (node._token != null)
			{
				SyntaxToken syntaxToken = node.AsToken();
				if (syntaxToken.ContainsDirectives)
				{
					SyntaxTriviaList syntaxTriviaList = syntaxToken.LeadingTrivia;
					SyntaxNodeOrToken.GetDirectives<TDirective>(in syntaxTriviaList, filter, ref directives);
					syntaxTriviaList = syntaxToken.TrailingTrivia;
					SyntaxNodeOrToken.GetDirectives<TDirective>(in syntaxTriviaList, filter, ref directives);
					return;
				}
			}
			if (node._nodeOrParent != null)
			{
				SyntaxNodeOrToken.GetDirectives<TDirective>(node._nodeOrParent, filter, ref directives);
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0004CCE8 File Offset: 0x0004AEE8
		private static void GetDirectives<[Nullable(0)] TDirective>(SyntaxNode node, [Nullable(new byte[] { 2, 1 })] Func<TDirective, bool> filter, [Nullable(new byte[] { 2, 1 })] ref List<TDirective> directives) where TDirective : SyntaxNode
		{
			foreach (SyntaxTrivia syntaxTrivia in node.DescendantTrivia((SyntaxNode node) => node.ContainsDirectives, true))
			{
				SyntaxNodeOrToken.GetDirectivesInTrivia<TDirective>(in syntaxTrivia, filter, ref directives);
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0004CD58 File Offset: 0x0004AF58
		[NullableContext(0)]
		private static bool GetDirectivesInTrivia<TDirective>(in SyntaxTrivia trivia, [Nullable(new byte[] { 2, 1 })] Func<TDirective, bool> filter, [Nullable(new byte[] { 2, 1 })] ref List<TDirective> directives) where TDirective : SyntaxNode
		{
			if (trivia.IsDirective)
			{
				TDirective tdirective = trivia.GetStructure() as TDirective;
				if (tdirective != null && (filter == null || filter.Invoke(tdirective)))
				{
					if (directives == null)
					{
						directives = new List<TDirective>();
					}
					directives.Add(tdirective);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0004CDAC File Offset: 0x0004AFAC
		[NullableContext(0)]
		private static void GetDirectives<TDirective>(in SyntaxTriviaList trivia, [Nullable(new byte[] { 2, 1 })] Func<TDirective, bool> filter, [Nullable(new byte[] { 2, 1 })] ref List<TDirective> directives) where TDirective : SyntaxNode
		{
			foreach (SyntaxTrivia syntaxTrivia in trivia)
			{
				if (!SyntaxNodeOrToken.GetDirectivesInTrivia<TDirective>(in syntaxTrivia, filter, ref directives))
				{
					SyntaxNode structure = syntaxTrivia.GetStructure();
					if (structure != null)
					{
						SyntaxNodeOrToken.GetDirectives<TDirective>(structure, filter, ref directives);
					}
				}
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0004CDF1 File Offset: 0x0004AFF1
		internal int Width
		{
			get
			{
				GreenNode token = this._token;
				if (token != null)
				{
					return token.Width;
				}
				SyntaxNode nodeOrParent = this._nodeOrParent;
				if (nodeOrParent == null)
				{
					return 0;
				}
				return nodeOrParent.Width;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0004CE14 File Offset: 0x0004B014
		internal int FullWidth
		{
			get
			{
				GreenNode token = this._token;
				if (token != null)
				{
					return token.FullWidth;
				}
				SyntaxNode nodeOrParent = this._nodeOrParent;
				if (nodeOrParent == null)
				{
					return 0;
				}
				return nodeOrParent.FullWidth;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0004CE37 File Offset: 0x0004B037
		internal int EndPosition
		{
			get
			{
				return this._position + this.FullWidth;
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0004CE48 File Offset: 0x0004B048
		public static int GetFirstChildIndexSpanningPosition(SyntaxNode node, int position)
		{
			if (!node.FullSpan.IntersectsWith(position))
			{
				throw new ArgumentException("Must be within node's FullSpan", "position");
			}
			return SyntaxNodeOrToken.GetFirstChildIndexSpanningPosition(node.ChildNodesAndTokens(), position);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0004CE84 File Offset: 0x0004B084
		internal static int GetFirstChildIndexSpanningPosition(ChildSyntaxList list, int position)
		{
			int i = 0;
			int num = list.Count - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				SyntaxNodeOrToken syntaxNodeOrToken = list[num2];
				if (position < syntaxNodeOrToken.Position)
				{
					num = num2 - 1;
				}
				else
				{
					if (position == syntaxNodeOrToken.Position)
					{
						while (num2 > 0 && list[num2 - 1].FullWidth == 0)
						{
							num2--;
						}
						return num2;
					}
					if (position < syntaxNodeOrToken.EndPosition)
					{
						return num2;
					}
					i = num2 + 1;
				}
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0004CF08 File Offset: 0x0004B108
		public SyntaxNodeOrToken GetNextSibling()
		{
			SyntaxNode parent = this.Parent;
			if (parent == null)
			{
				return default(SyntaxNodeOrToken);
			}
			ChildSyntaxList childSyntaxList = parent.ChildNodesAndTokens();
			if (childSyntaxList.Count >= 8)
			{
				return this.GetNextSiblingWithSearch(childSyntaxList);
			}
			return this.GetNextSiblingFromStart(childSyntaxList);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0004CF4C File Offset: 0x0004B14C
		public SyntaxNodeOrToken GetPreviousSibling()
		{
			if (this.Parent != null)
			{
				bool flag = false;
				foreach (SyntaxNodeOrToken syntaxNodeOrToken in this.Parent.ChildNodesAndTokens().Reverse())
				{
					if (flag)
					{
						return syntaxNodeOrToken;
					}
					if (syntaxNodeOrToken == this)
					{
						flag = true;
					}
				}
			}
			return default(SyntaxNodeOrToken);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0004CFB4 File Offset: 0x0004B1B4
		private SyntaxNodeOrToken GetNextSiblingFromStart(ChildSyntaxList siblings)
		{
			bool flag = false;
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in siblings)
			{
				if (flag)
				{
					return syntaxNodeOrToken;
				}
				if (syntaxNodeOrToken == this)
				{
					flag = true;
				}
			}
			return default(SyntaxNodeOrToken);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		private SyntaxNodeOrToken GetNextSiblingWithSearch(ChildSyntaxList siblings)
		{
			int firstChildIndexSpanningPosition = SyntaxNodeOrToken.GetFirstChildIndexSpanningPosition(siblings, this._position);
			int count = siblings.Count;
			bool flag = false;
			for (int i = firstChildIndexSpanningPosition; i < count; i++)
			{
				if (flag)
				{
					return siblings[i];
				}
				if (siblings[i] == this)
				{
					flag = true;
				}
			}
			return default(SyntaxNodeOrToken);
		}

		// Token: 0x04000A1C RID: 2588
		[Nullable(2)]
		private readonly SyntaxNode _nodeOrParent;

		// Token: 0x04000A1D RID: 2589
		[Nullable(2)]
		private readonly GreenNode _token;

		// Token: 0x04000A1E RID: 2590
		private readonly int _position;

		// Token: 0x04000A1F RID: 2591
		private readonly int _tokenIndex;
	}
}
