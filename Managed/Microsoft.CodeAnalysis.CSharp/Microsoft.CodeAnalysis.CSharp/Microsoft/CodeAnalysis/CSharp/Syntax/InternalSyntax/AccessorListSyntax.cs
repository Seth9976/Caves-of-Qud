using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000446 RID: 1094
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AccessorListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005CF9 RID: 23801 RVA: 0x0016AD4C File Offset: 0x00168F4C
		internal AccessorListSyntax(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode accessors, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (accessors != null)
			{
				base.AdjustFlagsAndWidth(accessors);
				this.accessors = accessors;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x06005CFA RID: 23802 RVA: 0x0016AD9C File Offset: 0x00168F9C
		internal AccessorListSyntax(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode accessors, SyntaxToken closeBraceToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (accessors != null)
			{
				base.AdjustFlagsAndWidth(accessors);
				this.accessors = accessors;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x0016ADEE File Offset: 0x00168FEE
		internal AccessorListSyntax(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode accessors, SyntaxToken closeBraceToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (accessors != null)
			{
				base.AdjustFlagsAndWidth(accessors);
				this.accessors = accessors;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06005CFC RID: 23804 RVA: 0x0016AE2D File Offset: 0x0016902D
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06005CFD RID: 23805 RVA: 0x0016AE35 File Offset: 0x00169035
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<AccessorDeclarationSyntax> Accessors
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AccessorDeclarationSyntax>(this.accessors);
			}
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06005CFE RID: 23806 RVA: 0x0016AE42 File Offset: 0x00169042
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x0016AE4C File Offset: 0x0016904C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openBraceToken;
				break;
			case 1:
				greenNode = this.accessors;
				break;
			case 2:
				greenNode = this.closeBraceToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x0016AE8B File Offset: 0x0016908B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AccessorListSyntax(this, parent, position);
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x0016AE95 File Offset: 0x00169095
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAccessorList(this);
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x0016AE9E File Offset: 0x0016909E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAccessorList(this);
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x0016AEA8 File Offset: 0x001690A8
		public AccessorListSyntax Update(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AccessorDeclarationSyntax> accessors, SyntaxToken closeBraceToken)
		{
			if (openBraceToken != this.OpenBraceToken || accessors != this.Accessors || closeBraceToken != this.CloseBraceToken)
			{
				AccessorListSyntax accessorListSyntax = SyntaxFactory.AccessorList(openBraceToken, accessors, closeBraceToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					accessorListSyntax = accessorListSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					accessorListSyntax = accessorListSyntax.WithAnnotationsGreen(annotations);
				}
				return accessorListSyntax;
			}
			return this;
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x0016AF0D File Offset: 0x0016910D
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AccessorListSyntax(base.Kind, this.openBraceToken, this.accessors, this.closeBraceToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x0016AF33 File Offset: 0x00169133
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AccessorListSyntax(base.Kind, this.openBraceToken, this.accessors, this.closeBraceToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x0016AF5C File Offset: 0x0016915C
		internal AccessorListSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openBraceToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.accessors = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBraceToken = syntaxToken2;
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x0016AFC8 File Offset: 0x001691C8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.accessors);
			writer.WriteValue(this.closeBraceToken);
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x0016AFF5 File Offset: 0x001691F5
		static AccessorListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AccessorListSyntax), (ObjectReader r) => new AccessorListSyntax(r));
		}

		// Token: 0x0400188C RID: 6284
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x0400188D RID: 6285
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode accessors;

		// Token: 0x0400188E RID: 6286
		internal readonly SyntaxToken closeBraceToken;
	}
}
