using System;
using System.Runtime.Serialization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002AD RID: 685
	[RequiredByNativeCode]
	[Serializable]
	public class UnityException : SystemException
	{
		// Token: 0x060021A8 RID: 8616 RVA: 0x00041AED File Offset: 0x0003FCED
		public UnityException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00041B08 File Offset: 0x0003FD08
		public UnityException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x00041B1F File Offset: 0x0003FD1F
		public UnityException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x00041B37 File Offset: 0x0003FD37
		protected UnityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040009D4 RID: 2516
		private const int Result = -2147467261;

		// Token: 0x040009D5 RID: 2517
		private string unityStackTrace;
	}
}
