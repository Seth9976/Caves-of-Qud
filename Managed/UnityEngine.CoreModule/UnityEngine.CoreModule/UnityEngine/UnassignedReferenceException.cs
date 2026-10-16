using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x020002AF RID: 687
	[Serializable]
	public class UnassignedReferenceException : SystemException
	{
		// Token: 0x060021B0 RID: 8624 RVA: 0x00041AED File Offset: 0x0003FCED
		public UnassignedReferenceException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00041B08 File Offset: 0x0003FD08
		public UnassignedReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00041B1F File Offset: 0x0003FD1F
		public UnassignedReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x00041B37 File Offset: 0x0003FD37
		protected UnassignedReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040009D8 RID: 2520
		private const int Result = -2147467261;

		// Token: 0x040009D9 RID: 2521
		private string unityStackTrace;
	}
}
