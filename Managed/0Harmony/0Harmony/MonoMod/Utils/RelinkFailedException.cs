using System;
using System.Text;
using Mono.Cecil;

namespace MonoMod.Utils
{
	// Token: 0x0200045A RID: 1114
	internal class RelinkFailedException : Exception
	{
		// Token: 0x060017BA RID: 6074 RVA: 0x000534C4 File Offset: 0x000516C4
		public RelinkFailedException(IMetadataTokenProvider mtp, IMetadataTokenProvider context = null)
			: this(RelinkFailedException._Format("MonoMod failed relinking", mtp, context), mtp, context)
		{
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000534DA File Offset: 0x000516DA
		public RelinkFailedException(string message, IMetadataTokenProvider mtp, IMetadataTokenProvider context = null)
			: base(message)
		{
			this.MTP = mtp;
			this.Context = context;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000534F1 File Offset: 0x000516F1
		public RelinkFailedException(string message, Exception innerException, IMetadataTokenProvider mtp, IMetadataTokenProvider context = null)
			: base(message ?? RelinkFailedException._Format("MonoMod failed relinking", mtp, context), innerException)
		{
			this.MTP = mtp;
			this.Context = context;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0005351C File Offset: 0x0005171C
		protected static string _Format(string message, IMetadataTokenProvider mtp, IMetadataTokenProvider context)
		{
			if (mtp == null && context == null)
			{
				return message;
			}
			StringBuilder stringBuilder = new StringBuilder(message);
			stringBuilder.Append(" ");
			if (mtp != null)
			{
				stringBuilder.Append(mtp.ToString());
			}
			if (context != null)
			{
				stringBuilder.Append(" ");
			}
			if (context != null)
			{
				stringBuilder.Append("(context: ").Append(context.ToString()).Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400106C RID: 4204
		public const string DefaultMessage = "MonoMod failed relinking";

		// Token: 0x0400106D RID: 4205
		public IMetadataTokenProvider MTP;

		// Token: 0x0400106E RID: 4206
		public IMetadataTokenProvider Context;
	}
}
