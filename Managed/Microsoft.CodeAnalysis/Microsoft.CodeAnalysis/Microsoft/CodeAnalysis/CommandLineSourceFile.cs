using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200012B RID: 299
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{Path,nq}")]
	public struct CommandLineSourceFile
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x0002007C File Offset: 0x0001E27C
		public CommandLineSourceFile(string path, bool isScript)
		{
			this = new CommandLineSourceFile(path, isScript, false);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00020087 File Offset: 0x0001E287
		public CommandLineSourceFile(string path, bool isScript, bool isInputRedirected)
		{
			this.Path = path;
			this.IsScript = isScript;
			this.IsInputRedirected = isInputRedirected;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002009E File Offset: 0x0001E29E
		public readonly string Path { get; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x000200A6 File Offset: 0x0001E2A6
		public readonly bool IsInputRedirected { get; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x000200AE File Offset: 0x0001E2AE
		public readonly bool IsScript { get; }
	}
}
