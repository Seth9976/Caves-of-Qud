using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200041F RID: 1055
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AttributeArgumentSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005A7B RID: 23163 RVA: 0x00161EDC File Offset: 0x001600DC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal AttributeArgumentSyntax(SyntaxKind kind, NameEqualsSyntax nameEquals, NameColonSyntax nameColon, [global::System.Runtime.CompilerServices.Nullable(1)] ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (nameEquals != null)
			{
				base.AdjustFlagsAndWidth(nameEquals);
				this.nameEquals = nameEquals;
			}
			if (nameColon != null)
			{
				base.AdjustFlagsAndWidth(nameColon);
				this.nameColon = nameColon;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005A7C RID: 23164 RVA: 0x00161F30 File Offset: 0x00160130
		internal AttributeArgumentSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, [global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (nameEquals != null)
			{
				base.AdjustFlagsAndWidth(nameEquals);
				this.nameEquals = nameEquals;
			}
			if (nameColon != null)
			{
				base.AdjustFlagsAndWidth(nameColon);
				this.nameColon = nameColon;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x00161F88 File Offset: 0x00160188
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal AttributeArgumentSyntax(SyntaxKind kind, NameEqualsSyntax nameEquals, NameColonSyntax nameColon, [global::System.Runtime.CompilerServices.Nullable(1)] ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 3;
			if (nameEquals != null)
			{
				base.AdjustFlagsAndWidth(nameEquals);
				this.nameEquals = nameEquals;
			}
			if (nameColon != null)
			{
				base.AdjustFlagsAndWidth(nameColon);
				this.nameColon = nameColon;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06005A7E RID: 23166 RVA: 0x00161FD5 File Offset: 0x001601D5
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameEqualsSyntax NameEquals
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.nameEquals;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06005A7F RID: 23167 RVA: 0x00161FDD File Offset: 0x001601DD
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameColonSyntax NameColon
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.nameColon;
			}
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06005A80 RID: 23168 RVA: 0x00161FE5 File Offset: 0x001601E5
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x00161FF0 File Offset: 0x001601F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.nameEquals;
				break;
			case 1:
				greenNode = this.nameColon;
				break;
			case 2:
				greenNode = this.expression;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x0016202F File Offset: 0x0016022F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AttributeArgumentSyntax(this, parent, position);
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x00162039 File Offset: 0x00160239
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttributeArgument(this);
		}

		// Token: 0x06005A84 RID: 23172 RVA: 0x00162042 File Offset: 0x00160242
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttributeArgument(this);
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x0016204C File Offset: 0x0016024C
		public AttributeArgumentSyntax Update(NameEqualsSyntax nameEquals, NameColonSyntax nameColon, ExpressionSyntax expression)
		{
			if (nameEquals != this.NameEquals || nameColon != this.NameColon || expression != this.Expression)
			{
				AttributeArgumentSyntax attributeArgumentSyntax = SyntaxFactory.AttributeArgument(nameEquals, nameColon, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					attributeArgumentSyntax = attributeArgumentSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					attributeArgumentSyntax = attributeArgumentSyntax.WithAnnotationsGreen(annotations);
				}
				return attributeArgumentSyntax;
			}
			return this;
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x001620AC File Offset: 0x001602AC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AttributeArgumentSyntax(base.Kind, this.nameEquals, this.nameColon, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x001620D2 File Offset: 0x001602D2
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AttributeArgumentSyntax(base.Kind, this.nameEquals, this.nameColon, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x001620F8 File Offset: 0x001602F8
		internal AttributeArgumentSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			NameEqualsSyntax nameEqualsSyntax = (NameEqualsSyntax)reader.ReadValue();
			if (nameEqualsSyntax != null)
			{
				base.AdjustFlagsAndWidth(nameEqualsSyntax);
				this.nameEquals = nameEqualsSyntax;
			}
			NameColonSyntax nameColonSyntax = (NameColonSyntax)reader.ReadValue();
			if (nameColonSyntax != null)
			{
				base.AdjustFlagsAndWidth(nameColonSyntax);
				this.nameColon = nameColonSyntax;
			}
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x00162167 File Offset: 0x00160367
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.nameEquals);
			writer.WriteValue(this.nameColon);
			writer.WriteValue(this.expression);
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x00162194 File Offset: 0x00160394
		static AttributeArgumentSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AttributeArgumentSyntax), (ObjectReader r) => new AttributeArgumentSyntax(r));
		}

		// Token: 0x040017D6 RID: 6102
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly NameEqualsSyntax nameEquals;

		// Token: 0x040017D7 RID: 6103
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly NameColonSyntax nameColon;

		// Token: 0x040017D8 RID: 6104
		internal readonly ExpressionSyntax expression;
	}
}
