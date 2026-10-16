using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001E6 RID: 486
	internal class CSharpRequiredLanguageVersion : RequiredLanguageVersion
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x000AC9E8 File Offset: 0x000AABE8
		internal LanguageVersion Version { get; }

		// Token: 0x06002154 RID: 8532 RVA: 0x000AC9F0 File Offset: 0x000AABF0
		internal CSharpRequiredLanguageVersion(LanguageVersion version)
		{
			this.Version = version;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000AC9FF File Offset: 0x000AABFF
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override string ToString()
		{
			return this.Version.ToDisplayString();
		}
	}
}
