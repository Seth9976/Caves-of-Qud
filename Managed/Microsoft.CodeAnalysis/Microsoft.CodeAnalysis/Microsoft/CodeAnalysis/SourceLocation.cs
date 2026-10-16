using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000181 RID: 385
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SourceLocation : Location, IEquatable<SourceLocation>
	{
		// Token: 0x06000FF2 RID: 4082 RVA: 0x0002E930 File Offset: 0x0002CB30
		public SourceLocation(SyntaxTree syntaxTree, TextSpan span)
		{
			this._syntaxTree = syntaxTree;
			this._span = span;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0002E946 File Offset: 0x0002CB46
		public SourceLocation(SyntaxNode node)
			: this(node.SyntaxTree, node.Span)
		{
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0002E95A File Offset: 0x0002CB5A
		public SourceLocation(in SyntaxToken token)
			: this(token.SyntaxTree, token.Span)
		{
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0002E96E File Offset: 0x0002CB6E
		public SourceLocation(in SyntaxNodeOrToken nodeOrToken)
			: this(nodeOrToken.SyntaxTree, nodeOrToken.Span)
		{
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0002E982 File Offset: 0x0002CB82
		public SourceLocation(in SyntaxTrivia trivia)
			: this(trivia.SyntaxTree, trivia.Span)
		{
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0002E996 File Offset: 0x0002CB96
		public SourceLocation(SyntaxReference syntaxRef)
			: this(syntaxRef.SyntaxTree, syntaxRef.Span)
		{
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0002E9AA File Offset: 0x0002CBAA
		public override LocationKind Kind
		{
			get
			{
				return LocationKind.SourceFile;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0002E9AD File Offset: 0x0002CBAD
		public override TextSpan SourceSpan
		{
			get
			{
				return this._span;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0002E9B5 File Offset: 0x0002CBB5
		public override SyntaxTree SourceTree
		{
			get
			{
				return this._syntaxTree;
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0002E9C0 File Offset: 0x0002CBC0
		public override FileLinePositionSpan GetLineSpan()
		{
			if (this._syntaxTree == null)
			{
				return default(FileLinePositionSpan);
			}
			return this._syntaxTree.GetLineSpan(this._span, default(CancellationToken));
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0002E9FC File Offset: 0x0002CBFC
		public override FileLinePositionSpan GetMappedLineSpan()
		{
			if (this._syntaxTree == null)
			{
				return default(FileLinePositionSpan);
			}
			return this._syntaxTree.GetMappedLineSpan(this._span, default(CancellationToken));
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0002EA35 File Offset: 0x0002CC35
		[NullableContext(2)]
		public bool Equals(SourceLocation other)
		{
			return this == other || (other != null && other._syntaxTree == this._syntaxTree && other._span == this._span);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0002EA67 File Offset: 0x0002CC67
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SourceLocation);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0002EA75 File Offset: 0x0002CC75
		public override int GetHashCode()
		{
			return Hash.Combine<SyntaxTree>(this._syntaxTree, this._span.GetHashCode());
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0002EA93 File Offset: 0x0002CC93
		protected override string GetDebuggerDisplay()
		{
			return base.GetDebuggerDisplay() + "\"" + this._syntaxTree.ToString().Substring(this._span.Start, this._span.Length) + "\"";
		}

		// Token: 0x04000464 RID: 1124
		private readonly SyntaxTree _syntaxTree;

		// Token: 0x04000465 RID: 1125
		private readonly TextSpan _span;
	}
}
