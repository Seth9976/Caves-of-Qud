using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C7 RID: 967
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class JoinClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x0600550A RID: 21770 RVA: 0x0015194C File Offset: 0x0014FB4C
		internal JoinClauseSyntax(SyntaxKind kind, SyntaxToken joinKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax inExpression, SyntaxToken onKeyword, ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, ExpressionSyntax rightExpression, [global::System.Runtime.CompilerServices.Nullable(2)] JoinIntoClauseSyntax into, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 10;
			base.AdjustFlagsAndWidth(joinKeyword);
			this.joinKeyword = joinKeyword;
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(inExpression);
			this.inExpression = inExpression;
			base.AdjustFlagsAndWidth(onKeyword);
			this.onKeyword = onKeyword;
			base.AdjustFlagsAndWidth(leftExpression);
			this.leftExpression = leftExpression;
			base.AdjustFlagsAndWidth(equalsKeyword);
			this.equalsKeyword = equalsKeyword;
			base.AdjustFlagsAndWidth(rightExpression);
			this.rightExpression = rightExpression;
			if (into != null)
			{
				base.AdjustFlagsAndWidth(into);
				this.into = into;
			}
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x00151A10 File Offset: 0x0014FC10
		internal JoinClauseSyntax(SyntaxKind kind, SyntaxToken joinKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax inExpression, SyntaxToken onKeyword, ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, ExpressionSyntax rightExpression, [global::System.Runtime.CompilerServices.Nullable(2)] JoinIntoClauseSyntax into, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 10;
			base.AdjustFlagsAndWidth(joinKeyword);
			this.joinKeyword = joinKeyword;
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(inExpression);
			this.inExpression = inExpression;
			base.AdjustFlagsAndWidth(onKeyword);
			this.onKeyword = onKeyword;
			base.AdjustFlagsAndWidth(leftExpression);
			this.leftExpression = leftExpression;
			base.AdjustFlagsAndWidth(equalsKeyword);
			this.equalsKeyword = equalsKeyword;
			base.AdjustFlagsAndWidth(rightExpression);
			this.rightExpression = rightExpression;
			if (into != null)
			{
				base.AdjustFlagsAndWidth(into);
				this.into = into;
			}
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x00151AD8 File Offset: 0x0014FCD8
		internal JoinClauseSyntax(SyntaxKind kind, SyntaxToken joinKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax inExpression, SyntaxToken onKeyword, ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, ExpressionSyntax rightExpression, [global::System.Runtime.CompilerServices.Nullable(2)] JoinIntoClauseSyntax into)
			: base(kind)
		{
			base.SlotCount = 10;
			base.AdjustFlagsAndWidth(joinKeyword);
			this.joinKeyword = joinKeyword;
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(inKeyword);
			this.inKeyword = inKeyword;
			base.AdjustFlagsAndWidth(inExpression);
			this.inExpression = inExpression;
			base.AdjustFlagsAndWidth(onKeyword);
			this.onKeyword = onKeyword;
			base.AdjustFlagsAndWidth(leftExpression);
			this.leftExpression = leftExpression;
			base.AdjustFlagsAndWidth(equalsKeyword);
			this.equalsKeyword = equalsKeyword;
			base.AdjustFlagsAndWidth(rightExpression);
			this.rightExpression = rightExpression;
			if (into != null)
			{
				base.AdjustFlagsAndWidth(into);
				this.into = into;
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x00151B97 File Offset: 0x0014FD97
		public SyntaxToken JoinKeyword
		{
			get
			{
				return this.joinKeyword;
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x0600550E RID: 21774 RVA: 0x00151B9F File Offset: 0x0014FD9F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x0600550F RID: 21775 RVA: 0x00151BA7 File Offset: 0x0014FDA7
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06005510 RID: 21776 RVA: 0x00151BAF File Offset: 0x0014FDAF
		public SyntaxToken InKeyword
		{
			get
			{
				return this.inKeyword;
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06005511 RID: 21777 RVA: 0x00151BB7 File Offset: 0x0014FDB7
		public ExpressionSyntax InExpression
		{
			get
			{
				return this.inExpression;
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06005512 RID: 21778 RVA: 0x00151BBF File Offset: 0x0014FDBF
		public SyntaxToken OnKeyword
		{
			get
			{
				return this.onKeyword;
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06005513 RID: 21779 RVA: 0x00151BC7 File Offset: 0x0014FDC7
		public ExpressionSyntax LeftExpression
		{
			get
			{
				return this.leftExpression;
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06005514 RID: 21780 RVA: 0x00151BCF File Offset: 0x0014FDCF
		public SyntaxToken EqualsKeyword
		{
			get
			{
				return this.equalsKeyword;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x00151BD7 File Offset: 0x0014FDD7
		public ExpressionSyntax RightExpression
		{
			get
			{
				return this.rightExpression;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x00151BDF File Offset: 0x0014FDDF
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public JoinIntoClauseSyntax Into
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.into;
			}
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x00151BE8 File Offset: 0x0014FDE8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.joinKeyword;
				break;
			case 1:
				greenNode = this.type;
				break;
			case 2:
				greenNode = this.identifier;
				break;
			case 3:
				greenNode = this.inKeyword;
				break;
			case 4:
				greenNode = this.inExpression;
				break;
			case 5:
				greenNode = this.onKeyword;
				break;
			case 6:
				greenNode = this.leftExpression;
				break;
			case 7:
				greenNode = this.equalsKeyword;
				break;
			case 8:
				greenNode = this.rightExpression;
				break;
			case 9:
				greenNode = this.into;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x00151C82 File Offset: 0x0014FE82
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new JoinClauseSyntax(this, parent, position);
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x00151C8C File Offset: 0x0014FE8C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitJoinClause(this);
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x00151C95 File Offset: 0x0014FE95
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitJoinClause(this);
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x00151CA0 File Offset: 0x0014FEA0
		public JoinClauseSyntax Update(SyntaxToken joinKeyword, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax inExpression, SyntaxToken onKeyword, ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, ExpressionSyntax rightExpression, JoinIntoClauseSyntax into)
		{
			if (joinKeyword != this.JoinKeyword || type != this.Type || identifier != this.Identifier || inKeyword != this.InKeyword || inExpression != this.InExpression || onKeyword != this.OnKeyword || leftExpression != this.LeftExpression || equalsKeyword != this.EqualsKeyword || rightExpression != this.RightExpression || into != this.Into)
			{
				JoinClauseSyntax joinClauseSyntax = SyntaxFactory.JoinClause(joinKeyword, type, identifier, inKeyword, inExpression, onKeyword, leftExpression, equalsKeyword, rightExpression, into);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					joinClauseSyntax = joinClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					joinClauseSyntax = joinClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return joinClauseSyntax;
			}
			return this;
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x00151D54 File Offset: 0x0014FF54
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new JoinClauseSyntax(base.Kind, this.joinKeyword, this.type, this.identifier, this.inKeyword, this.inExpression, this.onKeyword, this.leftExpression, this.equalsKeyword, this.rightExpression, this.into, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x00151DB0 File Offset: 0x0014FFB0
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new JoinClauseSyntax(base.Kind, this.joinKeyword, this.type, this.identifier, this.inKeyword, this.inExpression, this.onKeyword, this.leftExpression, this.equalsKeyword, this.rightExpression, this.into, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x00151E0C File Offset: 0x0015000C
		internal JoinClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 10;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.joinKeyword = syntaxToken;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			if (typeSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeSyntax);
				this.type = typeSyntax;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.inKeyword = syntaxToken3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.inExpression = expressionSyntax;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.onKeyword = syntaxToken4;
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax2);
			this.leftExpression = expressionSyntax2;
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.equalsKeyword = syntaxToken5;
			ExpressionSyntax expressionSyntax3 = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax3);
			this.rightExpression = expressionSyntax3;
			JoinIntoClauseSyntax joinIntoClauseSyntax = (JoinIntoClauseSyntax)reader.ReadValue();
			if (joinIntoClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(joinIntoClauseSyntax);
				this.into = joinIntoClauseSyntax;
			}
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x00151F48 File Offset: 0x00150148
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.joinKeyword);
			writer.WriteValue(this.type);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.inKeyword);
			writer.WriteValue(this.inExpression);
			writer.WriteValue(this.onKeyword);
			writer.WriteValue(this.leftExpression);
			writer.WriteValue(this.equalsKeyword);
			writer.WriteValue(this.rightExpression);
			writer.WriteValue(this.into);
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x00151FD4 File Offset: 0x001501D4
		static JoinClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(JoinClauseSyntax), (ObjectReader r) => new JoinClauseSyntax(r));
		}

		// Token: 0x040016A3 RID: 5795
		internal readonly SyntaxToken joinKeyword;

		// Token: 0x040016A4 RID: 5796
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly TypeSyntax type;

		// Token: 0x040016A5 RID: 5797
		internal readonly SyntaxToken identifier;

		// Token: 0x040016A6 RID: 5798
		internal readonly SyntaxToken inKeyword;

		// Token: 0x040016A7 RID: 5799
		internal readonly ExpressionSyntax inExpression;

		// Token: 0x040016A8 RID: 5800
		internal readonly SyntaxToken onKeyword;

		// Token: 0x040016A9 RID: 5801
		internal readonly ExpressionSyntax leftExpression;

		// Token: 0x040016AA RID: 5802
		internal readonly SyntaxToken equalsKeyword;

		// Token: 0x040016AB RID: 5803
		internal readonly ExpressionSyntax rightExpression;

		// Token: 0x040016AC RID: 5804
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly JoinIntoClauseSyntax into;
	}
}
