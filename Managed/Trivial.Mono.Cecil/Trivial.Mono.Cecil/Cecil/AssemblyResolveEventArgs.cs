using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000016 RID: 22
	public sealed class AssemblyResolveEventArgs : EventArgs
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003792 File Offset: 0x00001992
		public AssemblyNameReference AssemblyReference
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000379A File Offset: 0x0000199A
		public AssemblyResolveEventArgs(AssemblyNameReference reference)
		{
			this.reference = reference;
		}

		// Token: 0x0400002D RID: 45
		private readonly AssemblyNameReference reference;
	}
}
