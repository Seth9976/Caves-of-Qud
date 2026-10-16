using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000433 RID: 1075
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ClassOrStructConstraintSyntax : TypeParameterConstraintSyntax
	{
		// Token: 0x06005BBC RID: 23484 RVA: 0x00166647 File Offset: 0x00164847
		internal ClassOrStructConstraintSyntax(SyntaxKind kind, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken questionToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(classOrStructKeyword);
			this.classOrStructKeyword = classOrStructKeyword;
			if (questionToken != null)
			{
				base.AdjustFlagsAndWidth(questionToken);
				this.questionToken = questionToken;
			}
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x0016667A File Offset: 0x0016487A
		internal ClassOrStructConstraintSyntax(SyntaxKind kind, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken questionToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(classOrStructKeyword);
			this.classOrStructKeyword = classOrStructKeyword;
			if (questionToken != null)
			{
				base.AdjustFlagsAndWidth(questionToken);
				this.questionToken = questionToken;
			}
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x001666B1 File Offset: 0x001648B1
		internal ClassOrStructConstraintSyntax(SyntaxKind kind, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken questionToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(classOrStructKeyword);
			this.classOrStructKeyword = classOrStructKeyword;
			if (questionToken != null)
			{
				base.AdjustFlagsAndWidth(questionToken);
				this.questionToken = questionToken;
			}
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06005BBF RID: 23487 RVA: 0x001666E0 File Offset: 0x001648E0
		public SyntaxToken ClassOrStructKeyword
		{
			get
			{
				return this.classOrStructKeyword;
			}
		}

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06005BC0 RID: 23488 RVA: 0x001666E8 File Offset: 0x001648E8
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken QuestionToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.questionToken;
			}
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x001666F0 File Offset: 0x001648F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxToken = null;
				}
				else
				{
					syntaxToken = this.questionToken;
				}
			}
			else
			{
				syntaxToken = this.classOrStructKeyword;
			}
			return syntaxToken;
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x0016671B File Offset: 0x0016491B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ClassOrStructConstraintSyntax(this, parent, position);
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x00166725 File Offset: 0x00164925
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitClassOrStructConstraint(this);
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x0016672E File Offset: 0x0016492E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitClassOrStructConstraint(this);
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x00166738 File Offset: 0x00164938
		public ClassOrStructConstraintSyntax Update(SyntaxToken classOrStructKeyword, SyntaxToken questionToken)
		{
			if (classOrStructKeyword != this.ClassOrStructKeyword || questionToken != this.QuestionToken)
			{
				ClassOrStructConstraintSyntax classOrStructConstraintSyntax = SyntaxFactory.ClassOrStructConstraint(base.Kind, classOrStructKeyword, questionToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					classOrStructConstraintSyntax = classOrStructConstraintSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					classOrStructConstraintSyntax = classOrStructConstraintSyntax.WithAnnotationsGreen(annotations);
				}
				return classOrStructConstraintSyntax;
			}
			return this;
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x00166794 File Offset: 0x00164994
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ClassOrStructConstraintSyntax(base.Kind, this.classOrStructKeyword, this.questionToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x001667B4 File Offset: 0x001649B4
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ClassOrStructConstraintSyntax(base.Kind, this.classOrStructKeyword, this.questionToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x001667D4 File Offset: 0x001649D4
		internal ClassOrStructConstraintSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.classOrStructKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.questionToken = syntaxToken2;
			}
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x00166826 File Offset: 0x00164A26
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.classOrStructKeyword);
			writer.WriteValue(this.questionToken);
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x00166847 File Offset: 0x00164A47
		static ClassOrStructConstraintSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ClassOrStructConstraintSyntax), (ObjectReader r) => new ClassOrStructConstraintSyntax(r));
		}

		// Token: 0x04001831 RID: 6193
		internal readonly SyntaxToken classOrStructKeyword;

		// Token: 0x04001832 RID: 6194
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken questionToken;
	}
}
