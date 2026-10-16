using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000454 RID: 1108
	internal abstract class MemberCrefSyntax : CrefSyntax
	{
		// Token: 0x06005DB6 RID: 23990 RVA: 0x0016CCFF File Offset: 0x0016AEFF
		internal MemberCrefSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x0016CD0A File Offset: 0x0016AF0A
		internal MemberCrefSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x0016CD13 File Offset: 0x0016AF13
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected MemberCrefSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
