using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000416 RID: 1046
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CompilationUnitSyntax : CSharpSyntaxNode
	{
		// Token: 0x060059EA RID: 23018 RVA: 0x0016010C File Offset: 0x0015E30C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal CompilationUnitSyntax(SyntaxKind kind, GreenNode externs, GreenNode usings, GreenNode attributeLists, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken endOfFileToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			if (externs != null)
			{
				base.AdjustFlagsAndWidth(externs);
				this.externs = externs;
			}
			if (usings != null)
			{
				base.AdjustFlagsAndWidth(usings);
				this.usings = usings;
			}
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			base.AdjustFlagsAndWidth(endOfFileToken);
			this.endOfFileToken = endOfFileToken;
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x00160188 File Offset: 0x0015E388
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal CompilationUnitSyntax(SyntaxKind kind, GreenNode externs, GreenNode usings, GreenNode attributeLists, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken endOfFileToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			if (externs != null)
			{
				base.AdjustFlagsAndWidth(externs);
				this.externs = externs;
			}
			if (usings != null)
			{
				base.AdjustFlagsAndWidth(usings);
				this.usings = usings;
			}
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			base.AdjustFlagsAndWidth(endOfFileToken);
			this.endOfFileToken = endOfFileToken;
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x00160208 File Offset: 0x0015E408
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal CompilationUnitSyntax(SyntaxKind kind, GreenNode externs, GreenNode usings, GreenNode attributeLists, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken endOfFileToken)
			: base(kind)
		{
			base.SlotCount = 5;
			if (externs != null)
			{
				base.AdjustFlagsAndWidth(externs);
				this.externs = externs;
			}
			if (usings != null)
			{
				base.AdjustFlagsAndWidth(usings);
				this.usings = usings;
			}
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			base.AdjustFlagsAndWidth(endOfFileToken);
			this.endOfFileToken = endOfFileToken;
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x060059ED RID: 23021 RVA: 0x0016027D File Offset: 0x0015E47D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<ExternAliasDirectiveSyntax> Externs
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<ExternAliasDirectiveSyntax>(this.externs);
			}
		}

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x060059EE RID: 23022 RVA: 0x0016028A File Offset: 0x0015E48A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<UsingDirectiveSyntax> Usings
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<UsingDirectiveSyntax>(this.usings);
			}
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x060059EF RID: 23023 RVA: 0x00160297 File Offset: 0x0015E497
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x060059F0 RID: 23024 RVA: 0x001602A4 File Offset: 0x0015E4A4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(this.members);
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x060059F1 RID: 23025 RVA: 0x001602B1 File Offset: 0x0015E4B1
		public SyntaxToken EndOfFileToken
		{
			get
			{
				return this.endOfFileToken;
			}
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x001602BC File Offset: 0x0015E4BC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.externs;
				break;
			case 1:
				greenNode = this.usings;
				break;
			case 2:
				greenNode = this.attributeLists;
				break;
			case 3:
				greenNode = this.members;
				break;
			case 4:
				greenNode = this.endOfFileToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060059F3 RID: 23027 RVA: 0x00160315 File Offset: 0x0015E515
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CompilationUnitSyntax(this, parent, position);
		}

		// Token: 0x060059F4 RID: 23028 RVA: 0x0016031F File Offset: 0x0015E51F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCompilationUnit(this);
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x00160328 File Offset: 0x0015E528
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCompilationUnit(this);
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x00160334 File Offset: 0x0015E534
		public CompilationUnitSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken endOfFileToken)
		{
			if (externs != this.Externs || usings != this.Usings || attributeLists != this.AttributeLists || members != this.Members || endOfFileToken != this.EndOfFileToken)
			{
				CompilationUnitSyntax compilationUnitSyntax = SyntaxFactory.CompilationUnit(externs, usings, attributeLists, members, endOfFileToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					compilationUnitSyntax = compilationUnitSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					compilationUnitSyntax = compilationUnitSyntax.WithAnnotationsGreen(annotations);
				}
				return compilationUnitSyntax;
			}
			return this;
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x001603C0 File Offset: 0x0015E5C0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CompilationUnitSyntax(base.Kind, this.externs, this.usings, this.attributeLists, this.members, this.endOfFileToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x001603F2 File Offset: 0x0015E5F2
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CompilationUnitSyntax(base.Kind, this.externs, this.usings, this.attributeLists, this.members, this.endOfFileToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x00160424 File Offset: 0x0015E624
		internal CompilationUnitSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.externs = greenNode;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.usings = greenNode2;
			}
			GreenNode greenNode3 = (GreenNode)reader.ReadValue();
			if (greenNode3 != null)
			{
				base.AdjustFlagsAndWidth(greenNode3);
				this.attributeLists = greenNode3;
			}
			GreenNode greenNode4 = (GreenNode)reader.ReadValue();
			if (greenNode4 != null)
			{
				base.AdjustFlagsAndWidth(greenNode4);
				this.members = greenNode4;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.endOfFileToken = syntaxToken;
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x001604D0 File Offset: 0x0015E6D0
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.externs);
			writer.WriteValue(this.usings);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.members);
			writer.WriteValue(this.endOfFileToken);
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x00160520 File Offset: 0x0015E720
		static CompilationUnitSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CompilationUnitSyntax), (ObjectReader r) => new CompilationUnitSyntax(r));
		}

		// Token: 0x040017B2 RID: 6066
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode externs;

		// Token: 0x040017B3 RID: 6067
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode usings;

		// Token: 0x040017B4 RID: 6068
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040017B5 RID: 6069
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode members;

		// Token: 0x040017B6 RID: 6070
		internal readonly SyntaxToken endOfFileToken;
	}
}
