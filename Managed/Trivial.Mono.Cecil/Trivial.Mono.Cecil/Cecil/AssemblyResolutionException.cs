using System;
using System.IO;
using System.Runtime.Serialization;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public sealed class AssemblyResolutionException : FileNotFoundException
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000375B File Offset: 0x0000195B
		public AssemblyNameReference AssemblyReference
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003763 File Offset: 0x00001963
		public AssemblyResolutionException(AssemblyNameReference reference)
			: this(reference, null)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000376D File Offset: 0x0000196D
		public AssemblyResolutionException(AssemblyNameReference reference, Exception innerException)
			: base(string.Format("Failed to resolve assembly: '{0}'", reference), innerException)
		{
			this.reference = reference;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003788 File Offset: 0x00001988
		private AssemblyResolutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400002C RID: 44
		private readonly AssemblyNameReference reference;
	}
}
