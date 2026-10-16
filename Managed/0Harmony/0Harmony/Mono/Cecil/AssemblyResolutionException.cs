using System;
using System.IO;
using System.Runtime.Serialization;

namespace Mono.Cecil
{
	// Token: 0x020001F4 RID: 500
	[Serializable]
	public sealed class AssemblyResolutionException : FileNotFoundException
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00025AF1 File Offset: 0x00023CF1
		public AssemblyNameReference AssemblyReference
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00025AF9 File Offset: 0x00023CF9
		public AssemblyResolutionException(AssemblyNameReference reference)
			: this(reference, null)
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00025B03 File Offset: 0x00023D03
		public AssemblyResolutionException(AssemblyNameReference reference, Exception innerException)
			: base(string.Format("Failed to resolve assembly: '{0}'", reference), innerException)
		{
			this.reference = reference;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00025B1E File Offset: 0x00023D1E
		private AssemblyResolutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040002DD RID: 733
		private readonly AssemblyNameReference reference;
	}
}
