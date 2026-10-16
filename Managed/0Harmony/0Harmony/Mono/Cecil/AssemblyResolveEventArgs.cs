using System;

namespace Mono.Cecil
{
	// Token: 0x020001F3 RID: 499
	internal sealed class AssemblyResolveEventArgs : EventArgs
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00025ADA File Offset: 0x00023CDA
		public AssemblyNameReference AssemblyReference
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00025AE2 File Offset: 0x00023CE2
		public AssemblyResolveEventArgs(AssemblyNameReference reference)
		{
			this.reference = reference;
		}

		// Token: 0x040002DC RID: 732
		private readonly AssemblyNameReference reference;
	}
}
