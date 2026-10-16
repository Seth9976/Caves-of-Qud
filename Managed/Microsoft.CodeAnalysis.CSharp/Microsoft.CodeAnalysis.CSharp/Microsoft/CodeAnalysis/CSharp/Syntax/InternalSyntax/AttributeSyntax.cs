using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200041D RID: 1053
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AttributeSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005A5C RID: 23132 RVA: 0x001619E5 File Offset: 0x0015FBE5
		internal AttributeSyntax(SyntaxKind kind, NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (argumentList != null)
			{
				base.AdjustFlagsAndWidth(argumentList);
				this.argumentList = argumentList;
			}
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x00161A18 File Offset: 0x0015FC18
		internal AttributeSyntax(SyntaxKind kind, NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeArgumentListSyntax argumentList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (argumentList != null)
			{
				base.AdjustFlagsAndWidth(argumentList);
				this.argumentList = argumentList;
			}
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x00161A4F File Offset: 0x0015FC4F
		internal AttributeSyntax(SyntaxKind kind, NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeArgumentListSyntax argumentList)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (argumentList != null)
			{
				base.AdjustFlagsAndWidth(argumentList);
				this.argumentList = argumentList;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x00161A7E File Offset: 0x0015FC7E
		public NameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06005A60 RID: 23136 RVA: 0x00161A86 File Offset: 0x0015FC86
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public AttributeArgumentListSyntax ArgumentList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x00161A90 File Offset: 0x0015FC90
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.argumentList;
				}
			}
			else
			{
				greenNode = this.name;
			}
			return greenNode;
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x00161ABB File Offset: 0x0015FCBB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AttributeSyntax(this, parent, position);
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x00161AC5 File Offset: 0x0015FCC5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttribute(this);
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x00161ACE File Offset: 0x0015FCCE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttribute(this);
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x00161AD8 File Offset: 0x0015FCD8
		public AttributeSyntax Update(NameSyntax name, AttributeArgumentListSyntax argumentList)
		{
			if (name != this.Name || argumentList != this.ArgumentList)
			{
				AttributeSyntax attributeSyntax = SyntaxFactory.Attribute(name, argumentList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					attributeSyntax = attributeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					attributeSyntax = attributeSyntax.WithAnnotationsGreen(annotations);
				}
				return attributeSyntax;
			}
			return this;
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x00161B2E File Offset: 0x0015FD2E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AttributeSyntax(base.Kind, this.name, this.argumentList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x00161B4E File Offset: 0x0015FD4E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AttributeSyntax(base.Kind, this.name, this.argumentList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x00161B70 File Offset: 0x0015FD70
		internal AttributeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			NameSyntax nameSyntax = (NameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(nameSyntax);
			this.name = nameSyntax;
			AttributeArgumentListSyntax attributeArgumentListSyntax = (AttributeArgumentListSyntax)reader.ReadValue();
			if (attributeArgumentListSyntax != null)
			{
				base.AdjustFlagsAndWidth(attributeArgumentListSyntax);
				this.argumentList = attributeArgumentListSyntax;
			}
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x00161BC2 File Offset: 0x0015FDC2
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.name);
			writer.WriteValue(this.argumentList);
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x00161BE3 File Offset: 0x0015FDE3
		static AttributeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AttributeSyntax), (ObjectReader r) => new AttributeSyntax(r));
		}

		// Token: 0x040017D1 RID: 6097
		internal readonly NameSyntax name;

		// Token: 0x040017D2 RID: 6098
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly AttributeArgumentListSyntax argumentList;
	}
}
