using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000455 RID: 1109
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NameMemberCrefSyntax : MemberCrefSyntax
	{
		// Token: 0x06005DB9 RID: 23993 RVA: 0x0016CD1C File Offset: 0x0016AF1C
		internal NameMemberCrefSyntax(SyntaxKind kind, TypeSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x06005DBA RID: 23994 RVA: 0x0016CD4F File Offset: 0x0016AF4F
		internal NameMemberCrefSyntax(SyntaxKind kind, TypeSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x0016CD86 File Offset: 0x0016AF86
		internal NameMemberCrefSyntax(SyntaxKind kind, TypeSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06005DBC RID: 23996 RVA: 0x0016CDB5 File Offset: 0x0016AFB5
		public TypeSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06005DBD RID: 23997 RVA: 0x0016CDBD File Offset: 0x0016AFBD
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CrefParameterListSyntax Parameters
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x0016CDC8 File Offset: 0x0016AFC8
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
					greenNode = this.parameters;
				}
			}
			else
			{
				greenNode = this.name;
			}
			return greenNode;
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x0016CDF3 File Offset: 0x0016AFF3
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new NameMemberCrefSyntax(this, parent, position);
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x0016CDFD File Offset: 0x0016AFFD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNameMemberCref(this);
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x0016CE06 File Offset: 0x0016B006
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNameMemberCref(this);
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x0016CE10 File Offset: 0x0016B010
		public NameMemberCrefSyntax Update(TypeSyntax name, CrefParameterListSyntax parameters)
		{
			if (name != this.Name || parameters != this.Parameters)
			{
				NameMemberCrefSyntax nameMemberCrefSyntax = SyntaxFactory.NameMemberCref(name, parameters);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					nameMemberCrefSyntax = nameMemberCrefSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					nameMemberCrefSyntax = nameMemberCrefSyntax.WithAnnotationsGreen(annotations);
				}
				return nameMemberCrefSyntax;
			}
			return this;
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x0016CE66 File Offset: 0x0016B066
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new NameMemberCrefSyntax(base.Kind, this.name, this.parameters, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x0016CE86 File Offset: 0x0016B086
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new NameMemberCrefSyntax(base.Kind, this.name, this.parameters, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x0016CEA8 File Offset: 0x0016B0A8
		internal NameMemberCrefSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.name = typeSyntax;
			CrefParameterListSyntax crefParameterListSyntax = (CrefParameterListSyntax)reader.ReadValue();
			if (crefParameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(crefParameterListSyntax);
				this.parameters = crefParameterListSyntax;
			}
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x0016CEFA File Offset: 0x0016B0FA
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.name);
			writer.WriteValue(this.parameters);
		}

		// Token: 0x06005DC7 RID: 24007 RVA: 0x0016CF1B File Offset: 0x0016B11B
		static NameMemberCrefSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(NameMemberCrefSyntax), (ObjectReader r) => new NameMemberCrefSyntax(r));
		}

		// Token: 0x040018AD RID: 6317
		internal readonly TypeSyntax name;

		// Token: 0x040018AE RID: 6318
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly CrefParameterListSyntax parameters;
	}
}
