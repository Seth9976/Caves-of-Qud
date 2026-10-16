using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000134 RID: 308
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class CSharpDiagnosticFormatter : DiagnosticFormatter
	{
		// Token: 0x060018EE RID: 6382 RVA: 0x000854C4 File Offset: 0x000836C4
		internal CSharpDiagnosticFormatter()
		{
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x000854CC File Offset: 0x000836CC
		public new static CSharpDiagnosticFormatter Instance { get; } = new CSharpDiagnosticFormatter();
	}
}
