using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200012F RID: 303
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ErrorLogOptions
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00022283 File Offset: 0x00020483
		public string Path { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0002228B File Offset: 0x0002048B
		public SarifVersion SarifVersion { get; }

		// Token: 0x06000B56 RID: 2902 RVA: 0x00022293 File Offset: 0x00020493
		public ErrorLogOptions(string path, SarifVersion sarifVersion)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			this.Path = path;
			this.SarifVersion = sarifVersion;
		}
	}
}
