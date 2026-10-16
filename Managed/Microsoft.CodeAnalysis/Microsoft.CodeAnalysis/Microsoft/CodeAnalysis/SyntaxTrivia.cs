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
	// Token: 0x02000287 RID: 647
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	[StructLayout(3)]
	public readonly struct SyntaxTrivia : IEquatable<SyntaxTrivia>
	{
		// Token: 0x06001A7F RID: 6783 RVA: 0x0004EAD6 File Offset: 0x0004CCD6
		[NullableContext(2)]
		internal SyntaxTrivia(in SyntaxToken token, GreenNode triviaNode, int position, int index)
		{
			this.Token = token;
			this.UnderlyingNode = triviaNode;
			this.Position = position;
			this.Index = index;
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0004EAFA File Offset: 0x0004CCFA
		public int RawKind
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				if (underlyingNode == null)
				{
					return 0;
				}
				return underlyingNode.RawKind;
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0004EB10 File Offset: 0x0004CD10
		private string GetDebuggerDisplay()
		{
			string[] array = new string[5];
			array[0] = base.GetType().Name;
			array[1] = " ";
			int num = 2;
			GreenNode underlyingNode = this.UnderlyingNode;
			array[num] = ((underlyingNode != null) ? underlyingNode.KindText : null) ?? "None";
			array[3] = " ";
			array[4] = this.ToString();
			return string.Concat(array);
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x0004EB7D File Offset: 0x0004CD7D
		public string Language
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				return ((underlyingNode != null) ? underlyingNode.Language : null) ?? string.Empty;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0004EB9A File Offset: 0x0004CD9A
		public SyntaxToken Token { get; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x0004EBA2 File Offset: 0x0004CDA2
		[Nullable(2)]
		internal GreenNode UnderlyingNode
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x0004EBAA File Offset: 0x0004CDAA
		internal GreenNode RequiredUnderlyingNode
		{
			get
			{
				return this.UnderlyingNode;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0004EBB2 File Offset: 0x0004CDB2
		internal int Position { get; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0004EBBA File Offset: 0x0004CDBA
		internal int Index { get; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0004EBC2 File Offset: 0x0004CDC2
		internal int Width
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				if (underlyingNode == null)
				{
					return 0;
				}
				return underlyingNode.Width;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0004EBD5 File Offset: 0x0004CDD5
		internal int FullWidth
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				if (underlyingNode == null)
				{
					return 0;
				}
				return underlyingNode.FullWidth;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0004EBE8 File Offset: 0x0004CDE8
		public TextSpan Span
		{
			get
			{
				if (this.UnderlyingNode == null)
				{
					return default(TextSpan);
				}
				return new TextSpan(this.Position + this.UnderlyingNode.GetLeadingTriviaWidth(), this.UnderlyingNode.Width);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0004EC29 File Offset: 0x0004CE29
		public int SpanStart
		{
			get
			{
				if (this.UnderlyingNode == null)
				{
					return 0;
				}
				return this.Position + this.UnderlyingNode.GetLeadingTriviaWidth();
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0004EC48 File Offset: 0x0004CE48
		public TextSpan FullSpan
		{
			get
			{
				if (this.UnderlyingNode == null)
				{
					return default(TextSpan);
				}
				return new TextSpan(this.Position, this.UnderlyingNode.FullWidth);
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0004EC7D File Offset: 0x0004CE7D
		public bool ContainsDiagnostics
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				return underlyingNode != null && underlyingNode.ContainsDiagnostics;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0004EC90 File Offset: 0x0004CE90
		public bool HasStructure
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				return underlyingNode != null && underlyingNode.IsStructuredTrivia;
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0004ECA4 File Offset: 0x0004CEA4
		public bool IsPartOfStructuredTrivia()
		{
			SyntaxNode parent = this.Token.Parent;
			return parent != null && parent.IsPartOfStructuredTrivia();
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0004ECCA File Offset: 0x0004CECA
		internal bool ContainsAnnotations
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				return underlyingNode != null && underlyingNode.ContainsAnnotations;
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0004ECDD File Offset: 0x0004CEDD
		public bool HasAnnotations(string annotationKind)
		{
			GreenNode underlyingNode = this.UnderlyingNode;
			return underlyingNode != null && underlyingNode.HasAnnotations(annotationKind);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0004ECF1 File Offset: 0x0004CEF1
		public bool HasAnnotations(params string[] annotationKinds)
		{
			GreenNode underlyingNode = this.UnderlyingNode;
			return underlyingNode != null && underlyingNode.HasAnnotations(annotationKinds);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0004ED05 File Offset: 0x0004CF05
		[NullableContext(2)]
		public bool HasAnnotation([NotNullWhen(true)] SyntaxAnnotation annotation)
		{
			GreenNode underlyingNode = this.UnderlyingNode;
			return underlyingNode != null && underlyingNode.HasAnnotation(annotation);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0004ED19 File Offset: 0x0004CF19
		public IEnumerable<SyntaxAnnotation> GetAnnotations(string annotationKind)
		{
			if (this.UnderlyingNode == null)
			{
				return SpecializedCollections.EmptyEnumerable<SyntaxAnnotation>();
			}
			return this.UnderlyingNode.GetAnnotations(annotationKind);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0004ED35 File Offset: 0x0004CF35
		public IEnumerable<SyntaxAnnotation> GetAnnotations(params string[] annotationKinds)
		{
			if (this.UnderlyingNode == null)
			{
				return SpecializedCollections.EmptyEnumerable<SyntaxAnnotation>();
			}
			return this.UnderlyingNode.GetAnnotations(annotationKinds);
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0004ED51 File Offset: 0x0004CF51
		public bool IsDirective
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				return underlyingNode != null && underlyingNode.IsDirective;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0004ED64 File Offset: 0x0004CF64
		internal bool IsSkippedTokensTrivia
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				return underlyingNode != null && underlyingNode.IsSkippedTokensTrivia;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x0004ED77 File Offset: 0x0004CF77
		internal bool IsDocumentationCommentTrivia
		{
			get
			{
				GreenNode underlyingNode = this.UnderlyingNode;
				return underlyingNode != null && underlyingNode.IsDocumentationCommentTrivia;
			}
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0004ED8A File Offset: 0x0004CF8A
		[NullableContext(2)]
		public SyntaxNode GetStructure()
		{
			if (!this.HasStructure)
			{
				return null;
			}
			return this.UnderlyingNode.GetStructure(this);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0004EDA7 File Offset: 0x0004CFA7
		[NullableContext(2)]
		internal bool TryGetStructure([NotNullWhen(true)] out SyntaxNode structure)
		{
			structure = this.GetStructure();
			return structure != null;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0004EDB6 File Offset: 0x0004CFB6
		public override string ToString()
		{
			if (this.UnderlyingNode == null)
			{
				return string.Empty;
			}
			return this.UnderlyingNode.ToString();
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0004EDD1 File Offset: 0x0004CFD1
		public string ToFullString()
		{
			if (this.UnderlyingNode == null)
			{
				return string.Empty;
			}
			return this.UnderlyingNode.ToFullString();
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0004EDEC File Offset: 0x0004CFEC
		public void WriteTo(TextWriter writer)
		{
			GreenNode underlyingNode = this.UnderlyingNode;
			if (underlyingNode == null)
			{
				return;
			}
			underlyingNode.WriteTo(writer);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0004EDFF File Offset: 0x0004CFFF
		public static bool operator ==(SyntaxTrivia left, SyntaxTrivia right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0004EE09 File Offset: 0x0004D009
		public static bool operator !=(SyntaxTrivia left, SyntaxTrivia right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0004EE18 File Offset: 0x0004D018
		public bool Equals(SyntaxTrivia other)
		{
			return this.Token == other.Token && this.UnderlyingNode == other.UnderlyingNode && this.Position == other.Position && this.Index == other.Index;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0004EE68 File Offset: 0x0004D068
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is SyntaxTrivia)
			{
				SyntaxTrivia syntaxTrivia = (SyntaxTrivia)obj;
				return this.Equals(syntaxTrivia);
			}
			return false;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0004EE90 File Offset: 0x0004D090
		public override int GetHashCode()
		{
			return Hash.Combine(this.Token.GetHashCode(), Hash.Combine<GreenNode>(this.UnderlyingNode, Hash.Combine(this.Position, this.Index)));
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0004EED2 File Offset: 0x0004D0D2
		public SyntaxTrivia WithAdditionalAnnotations(params SyntaxAnnotation[] annotations)
		{
			return this.WithAdditionalAnnotations(annotations);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0004EEDC File Offset: 0x0004D0DC
		public SyntaxTrivia WithAdditionalAnnotations(IEnumerable<SyntaxAnnotation> annotations)
		{
			if (annotations == null)
			{
				throw new ArgumentNullException("annotations");
			}
			if (this.UnderlyingNode != null)
			{
				SyntaxToken syntaxToken = default(SyntaxToken);
				return new SyntaxTrivia(in syntaxToken, this.UnderlyingNode.WithAdditionalAnnotationsGreen(annotations), 0, 0);
			}
			return default(SyntaxTrivia);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0004EF25 File Offset: 0x0004D125
		public SyntaxTrivia WithoutAnnotations(params SyntaxAnnotation[] annotations)
		{
			return this.WithoutAnnotations(annotations);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0004EF30 File Offset: 0x0004D130
		public SyntaxTrivia WithoutAnnotations(IEnumerable<SyntaxAnnotation> annotations)
		{
			if (annotations == null)
			{
				throw new ArgumentNullException("annotations");
			}
			if (this.UnderlyingNode != null)
			{
				SyntaxToken syntaxToken = default(SyntaxToken);
				return new SyntaxTrivia(in syntaxToken, this.UnderlyingNode.WithoutAnnotationsGreen(annotations), 0, 0);
			}
			return default(SyntaxTrivia);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0004EF79 File Offset: 0x0004D179
		public SyntaxTrivia WithoutAnnotations(string annotationKind)
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

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0004EFA8 File Offset: 0x0004D1A8
		public SyntaxTrivia CopyAnnotationsTo(SyntaxTrivia trivia)
		{
			if (trivia.UnderlyingNode == null)
			{
				return default(SyntaxTrivia);
			}
			if (this.UnderlyingNode == null)
			{
				return trivia;
			}
			SyntaxAnnotation[] annotations = this.UnderlyingNode.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return trivia;
			}
			SyntaxToken syntaxToken = default(SyntaxToken);
			return new SyntaxTrivia(in syntaxToken, trivia.UnderlyingNode.WithAdditionalAnnotationsGreen(annotations), 0, 0);
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0004F004 File Offset: 0x0004D204
		[Nullable(2)]
		public SyntaxTree SyntaxTree
		{
			[NullableContext(2)]
			get
			{
				return this.Token.SyntaxTree;
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0004F01F File Offset: 0x0004D21F
		public Location GetLocation()
		{
			return this.SyntaxTree.GetLocation(this.Span);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0004F032 File Offset: 0x0004D232
		public IEnumerable<Diagnostic> GetDiagnostics()
		{
			return this.SyntaxTree.GetDiagnostics(this);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0004F045 File Offset: 0x0004D245
		public bool IsEquivalentTo(SyntaxTrivia trivia)
		{
			return (this.UnderlyingNode == null && trivia.UnderlyingNode == null) || (this.UnderlyingNode != null && trivia.UnderlyingNode != null && this.UnderlyingNode.IsEquivalentTo(trivia.UnderlyingNode));
		}

		// Token: 0x04000A3A RID: 2618
		internal static readonly Func<SyntaxTrivia, bool> Any = (SyntaxTrivia t) => true;
	}
}
