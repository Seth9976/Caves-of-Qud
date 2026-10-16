using System;
using System.Runtime.Serialization;

namespace System.Web.Util
{
	// Token: 0x0200010A RID: 266
	internal sealed class AppVerifierException : Exception
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x000251A1 File Offset: 0x000233A1
		public AppVerifierException(AppVerifierErrorCode errorCode, string message)
			: base(message)
		{
			this._errorCode = errorCode;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x000251B1 File Offset: 0x000233B1
		private AppVerifierException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x000251BB File Offset: 0x000233BB
		public AppVerifierErrorCode ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x0400118C RID: 4492
		private readonly AppVerifierErrorCode _errorCode;
	}
}
