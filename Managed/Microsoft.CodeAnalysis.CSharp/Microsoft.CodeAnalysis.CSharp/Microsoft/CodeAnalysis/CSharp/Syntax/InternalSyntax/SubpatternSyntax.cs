using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003DB RID: 987
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SubpatternSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005639 RID: 22073 RVA: 0x00154A76 File Offset: 0x00152C76
		internal SubpatternSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			if (nameColon != null)
			{
				base.AdjustFlagsAndWidth(nameColon);
				this.nameColon = nameColon;
			}
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x00154AA9 File Offset: 0x00152CA9
		internal SubpatternSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, PatternSyntax pattern, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			if (nameColon != null)
			{
				base.AdjustFlagsAndWidth(nameColon);
				this.nameColon = nameColon;
			}
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x00154AE0 File Offset: 0x00152CE0
		internal SubpatternSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, PatternSyntax pattern)
			: base(kind)
		{
			base.SlotCount = 2;
			if (nameColon != null)
			{
				base.AdjustFlagsAndWidth(nameColon);
				this.nameColon = nameColon;
			}
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x0600563C RID: 22076 RVA: 0x00154B0F File Offset: 0x00152D0F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameColonSyntax NameColon
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.nameColon;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x0600563D RID: 22077 RVA: 0x00154B17 File Offset: 0x00152D17
		public PatternSyntax Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x00154B20 File Offset: 0x00152D20
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
					greenNode = this.pattern;
				}
			}
			else
			{
				greenNode = this.nameColon;
			}
			return greenNode;
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x00154B4B File Offset: 0x00152D4B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SubpatternSyntax(this, parent, position);
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x00154B55 File Offset: 0x00152D55
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSubpattern(this);
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x00154B5E File Offset: 0x00152D5E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSubpattern(this);
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x00154B68 File Offset: 0x00152D68
		public SubpatternSyntax Update(NameColonSyntax nameColon, PatternSyntax pattern)
		{
			if (nameColon != this.NameColon || pattern != this.Pattern)
			{
				SubpatternSyntax subpatternSyntax = SyntaxFactory.Subpattern(nameColon, pattern);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					subpatternSyntax = subpatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					subpatternSyntax = subpatternSyntax.WithAnnotationsGreen(annotations);
				}
				return subpatternSyntax;
			}
			return this;
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x00154BBE File Offset: 0x00152DBE
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SubpatternSyntax(base.Kind, this.nameColon, this.pattern, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x00154BDE File Offset: 0x00152DDE
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SubpatternSyntax(base.Kind, this.nameColon, this.pattern, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x00154C00 File Offset: 0x00152E00
		internal SubpatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			NameColonSyntax nameColonSyntax = (NameColonSyntax)reader.ReadValue();
			if (nameColonSyntax != null)
			{
				base.AdjustFlagsAndWidth(nameColonSyntax);
				this.nameColon = nameColonSyntax;
			}
			PatternSyntax patternSyntax = (PatternSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(patternSyntax);
			this.pattern = patternSyntax;
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x00154C52 File Offset: 0x00152E52
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.nameColon);
			writer.WriteValue(this.pattern);
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x00154C73 File Offset: 0x00152E73
		static SubpatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SubpatternSyntax), (ObjectReader r) => new SubpatternSyntax(r));
		}

		// Token: 0x040016D8 RID: 5848
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly NameColonSyntax nameColon;

		// Token: 0x040016D9 RID: 5849
		internal readonly PatternSyntax pattern;
	}
}
