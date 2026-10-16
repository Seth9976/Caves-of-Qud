using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E0 RID: 992
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BinaryPatternSyntax : PatternSyntax
	{
		// Token: 0x06005683 RID: 22147 RVA: 0x0015544F File Offset: 0x0015364F
		internal BinaryPatternSyntax(SyntaxKind kind, PatternSyntax left, SyntaxToken operatorToken, PatternSyntax right, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(left);
			this.left = left;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(right);
			this.right = right;
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x00155490 File Offset: 0x00153690
		internal BinaryPatternSyntax(SyntaxKind kind, PatternSyntax left, SyntaxToken operatorToken, PatternSyntax right, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(left);
			this.left = left;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(right);
			this.right = right;
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x001554DF File Offset: 0x001536DF
		internal BinaryPatternSyntax(SyntaxKind kind, PatternSyntax left, SyntaxToken operatorToken, PatternSyntax right)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(left);
			this.left = left;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(right);
			this.right = right;
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06005686 RID: 22150 RVA: 0x0015551B File Offset: 0x0015371B
		public PatternSyntax Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06005687 RID: 22151 RVA: 0x00155523 File Offset: 0x00153723
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06005688 RID: 22152 RVA: 0x0015552B File Offset: 0x0015372B
		public PatternSyntax Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06005689 RID: 22153 RVA: 0x00155534 File Offset: 0x00153734
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.left;
				break;
			case 1:
				greenNode = this.operatorToken;
				break;
			case 2:
				greenNode = this.right;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x00155573 File Offset: 0x00153773
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new BinaryPatternSyntax(this, parent, position);
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x0015557D File Offset: 0x0015377D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBinaryPattern(this);
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x00155586 File Offset: 0x00153786
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBinaryPattern(this);
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x00155590 File Offset: 0x00153790
		public BinaryPatternSyntax Update(PatternSyntax left, SyntaxToken operatorToken, PatternSyntax right)
		{
			if (left != this.Left || operatorToken != this.OperatorToken || right != this.Right)
			{
				BinaryPatternSyntax binaryPatternSyntax = SyntaxFactory.BinaryPattern(base.Kind, left, operatorToken, right);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					binaryPatternSyntax = binaryPatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					binaryPatternSyntax = binaryPatternSyntax.WithAnnotationsGreen(annotations);
				}
				return binaryPatternSyntax;
			}
			return this;
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x001555F6 File Offset: 0x001537F6
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new BinaryPatternSyntax(base.Kind, this.left, this.operatorToken, this.right, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x0015561C File Offset: 0x0015381C
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new BinaryPatternSyntax(base.Kind, this.left, this.operatorToken, this.right, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x00155644 File Offset: 0x00153844
		internal BinaryPatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			PatternSyntax patternSyntax = (PatternSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(patternSyntax);
			this.left = patternSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
			PatternSyntax patternSyntax2 = (PatternSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(patternSyntax2);
			this.right = patternSyntax2;
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x001556AD File Offset: 0x001538AD
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.left);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.right);
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x001556DA File Offset: 0x001538DA
		static BinaryPatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(BinaryPatternSyntax), (ObjectReader r) => new BinaryPatternSyntax(r));
		}

		// Token: 0x040016E1 RID: 5857
		internal readonly PatternSyntax left;

		// Token: 0x040016E2 RID: 5858
		internal readonly SyntaxToken operatorToken;

		// Token: 0x040016E3 RID: 5859
		internal readonly PatternSyntax right;
	}
}
