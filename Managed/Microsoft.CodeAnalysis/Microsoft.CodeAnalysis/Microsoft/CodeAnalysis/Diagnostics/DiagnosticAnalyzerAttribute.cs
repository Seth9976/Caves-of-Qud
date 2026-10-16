using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200033A RID: 826
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(4)]
	public sealed class DiagnosticAnalyzerAttribute : Attribute
	{
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x0006FF39 File Offset: 0x0006E139
		public string[] Languages { get; }

		// Token: 0x060023A2 RID: 9122 RVA: 0x0006FF44 File Offset: 0x0006E144
		public DiagnosticAnalyzerAttribute(string firstLanguage, params string[] additionalLanguages)
		{
			if (firstLanguage == null)
			{
				throw new ArgumentNullException("firstLanguage");
			}
			if (additionalLanguages == null)
			{
				throw new ArgumentNullException("additionalLanguages");
			}
			string[] array = new string[additionalLanguages.Length + 1];
			array[0] = firstLanguage;
			for (int i = 0; i < additionalLanguages.Length; i++)
			{
				array[i + 1] = additionalLanguages[i];
			}
			this.Languages = array;
		}
	}
}
