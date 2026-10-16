using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000FD RID: 253
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CSharpCommandLineArguments : CommandLineArguments
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x000651D9 File Offset: 0x000633D9
		// (set) Token: 0x06001326 RID: 4902 RVA: 0x000651E1 File Offset: 0x000633E1
		public new CSharpCompilationOptions CompilationOptions { get; internal set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x000651EA File Offset: 0x000633EA
		// (set) Token: 0x06001328 RID: 4904 RVA: 0x000651F2 File Offset: 0x000633F2
		public new CSharpParseOptions ParseOptions { get; internal set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x000651FB File Offset: 0x000633FB
		protected override ParseOptions ParseOptionsCore
		{
			get
			{
				return this.ParseOptions;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00065203 File Offset: 0x00063403
		protected override CompilationOptions CompilationOptionsCore
		{
			get
			{
				return this.CompilationOptions;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0006520B File Offset: 0x0006340B
		// (set) Token: 0x0600132C RID: 4908 RVA: 0x00065213 File Offset: 0x00063413
		internal bool ShouldIncludeErrorEndLocation { get; set; }

		// Token: 0x0600132D RID: 4909 RVA: 0x0006521C File Offset: 0x0006341C
		internal CSharpCommandLineArguments()
		{
			this.CompilationOptions = null;
			this.ParseOptions = null;
		}
	}
}
