using System;
using Mono.Cecil;

namespace MonoMod.Utils
{
	// Token: 0x0200045B RID: 1115
	internal class RelinkTargetNotFoundException : RelinkFailedException
	{
		// Token: 0x060017BE RID: 6078 RVA: 0x0005358D File Offset: 0x0005178D
		public RelinkTargetNotFoundException(IMetadataTokenProvider mtp, IMetadataTokenProvider context = null)
			: base(RelinkFailedException._Format("MonoMod relinker failed finding", mtp, context), mtp, context)
		{
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000535A3 File Offset: 0x000517A3
		public RelinkTargetNotFoundException(string message, IMetadataTokenProvider mtp, IMetadataTokenProvider context = null)
			: base(message ?? "MonoMod relinker failed finding", mtp, context)
		{
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000535B7 File Offset: 0x000517B7
		public RelinkTargetNotFoundException(string message, Exception innerException, IMetadataTokenProvider mtp, IMetadataTokenProvider context = null)
			: base(message ?? "MonoMod relinker failed finding", innerException, mtp, context)
		{
		}

		// Token: 0x0400106F RID: 4207
		public new const string DefaultMessage = "MonoMod relinker failed finding";
	}
}
