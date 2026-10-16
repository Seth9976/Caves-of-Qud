using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000472 RID: 1138
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RegionDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005F5E RID: 24414 RVA: 0x00171510 File Offset: 0x0016F710
		internal RegionDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(regionKeyword);
			this.regionKeyword = regionKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x00171564 File Offset: 0x0016F764
		internal RegionDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(regionKeyword);
			this.regionKeyword = regionKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x001715BC File Offset: 0x0016F7BC
		internal RegionDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(regionKeyword);
			this.regionKeyword = regionKeyword;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06005F61 RID: 24417 RVA: 0x0017160B File Offset: 0x0016F80B
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06005F62 RID: 24418 RVA: 0x00171613 File Offset: 0x0016F813
		public SyntaxToken RegionKeyword
		{
			get
			{
				return this.regionKeyword;
			}
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06005F63 RID: 24419 RVA: 0x0017161B File Offset: 0x0016F81B
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06005F64 RID: 24420 RVA: 0x00171623 File Offset: 0x0016F823
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x0017162C File Offset: 0x0016F82C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			switch (index)
			{
			case 0:
				syntaxToken = this.hashToken;
				break;
			case 1:
				syntaxToken = this.regionKeyword;
				break;
			case 2:
				syntaxToken = this.endOfDirectiveToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x0017166B File Offset: 0x0016F86B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new RegionDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x00171675 File Offset: 0x0016F875
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRegionDirectiveTrivia(this);
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x0017167E File Offset: 0x0016F87E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRegionDirectiveTrivia(this);
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x00171688 File Offset: 0x0016F888
		public RegionDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || regionKeyword != this.RegionKeyword || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				RegionDirectiveTriviaSyntax regionDirectiveTriviaSyntax = SyntaxFactory.RegionDirectiveTrivia(hashToken, regionKeyword, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					regionDirectiveTriviaSyntax = regionDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					regionDirectiveTriviaSyntax = regionDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return regionDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x001716EA File Offset: 0x0016F8EA
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new RegionDirectiveTriviaSyntax(base.Kind, this.hashToken, this.regionKeyword, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x00171716 File Offset: 0x0016F916
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new RegionDirectiveTriviaSyntax(base.Kind, this.hashToken, this.regionKeyword, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x00171744 File Offset: 0x0016F944
		internal RegionDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.regionKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endOfDirectiveToken = syntaxToken3;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x001717B9 File Offset: 0x0016F9B9
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.regionKeyword);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x001717F2 File Offset: 0x0016F9F2
		static RegionDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(RegionDirectiveTriviaSyntax), (ObjectReader r) => new RegionDirectiveTriviaSyntax(r));
		}

		// Token: 0x04001903 RID: 6403
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001904 RID: 6404
		internal readonly SyntaxToken regionKeyword;

		// Token: 0x04001905 RID: 6405
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x04001906 RID: 6406
		internal readonly bool isActive;
	}
}
