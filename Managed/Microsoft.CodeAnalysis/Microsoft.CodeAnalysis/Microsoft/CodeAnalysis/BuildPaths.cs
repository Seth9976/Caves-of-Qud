using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200012D RID: 301
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct BuildPaths
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0002223C File Offset: 0x0002043C
		internal string ClientDirectory { get; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00022244 File Offset: 0x00020444
		internal string WorkingDirectory { get; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0002224C File Offset: 0x0002044C
		[Nullable(2)]
		internal string SdkDirectory
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00022254 File Offset: 0x00020454
		[Nullable(2)]
		internal string TempDirectory
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002225C File Offset: 0x0002045C
		internal BuildPaths(string clientDir, string workingDir, [Nullable(2)] string sdkDir, [Nullable(2)] string tempDir)
		{
			this.ClientDirectory = clientDir;
			this.WorkingDirectory = workingDir;
			this.SdkDirectory = sdkDir;
			this.TempDirectory = tempDir;
		}
	}
}
