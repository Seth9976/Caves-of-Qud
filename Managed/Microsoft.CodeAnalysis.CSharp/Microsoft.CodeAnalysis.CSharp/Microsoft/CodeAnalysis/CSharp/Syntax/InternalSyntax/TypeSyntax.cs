using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000377 RID: 887
	internal abstract class TypeSyntax : ExpressionSyntax
	{
		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x00146069 File Offset: 0x00144269
		public bool IsVar
		{
			get
			{
				return this.IsIdentifierName("var");
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x0600509A RID: 20634 RVA: 0x00146076 File Offset: 0x00144276
		public bool IsUnmanaged
		{
			get
			{
				return this.IsIdentifierName("unmanaged");
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x0600509B RID: 20635 RVA: 0x00146083 File Offset: 0x00144283
		public bool IsNotNull
		{
			get
			{
				return this.IsIdentifierName("notnull");
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x0600509C RID: 20636 RVA: 0x00146090 File Offset: 0x00144290
		public bool IsNint
		{
			get
			{
				return this.IsIdentifierName("nint");
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x0600509D RID: 20637 RVA: 0x0014609D File Offset: 0x0014429D
		public bool IsNuint
		{
			get
			{
				return this.IsIdentifierName("nuint");
			}
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x001460AC File Offset: 0x001442AC
		private bool IsIdentifierName(string id)
		{
			IdentifierNameSyntax identifierNameSyntax = this as IdentifierNameSyntax;
			return identifierNameSyntax != null && identifierNameSyntax.Identifier.ToString() == id;
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x0600509F RID: 20639 RVA: 0x001460D6 File Offset: 0x001442D6
		public bool IsRef
		{
			get
			{
				return base.Kind == SyntaxKind.RefType;
			}
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x001460E5 File Offset: 0x001442E5
		internal TypeSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x001460F0 File Offset: 0x001442F0
		internal TypeSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x001460F9 File Offset: 0x001442F9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected TypeSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
