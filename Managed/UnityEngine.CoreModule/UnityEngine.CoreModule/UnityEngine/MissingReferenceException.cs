using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x020002B0 RID: 688
	[Serializable]
	public class MissingReferenceException : SystemException
	{
		// Token: 0x060021B4 RID: 8628 RVA: 0x00041AED File Offset: 0x0003FCED
		public MissingReferenceException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x00041B08 File Offset: 0x0003FD08
		public MissingReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x00041B1F File Offset: 0x0003FD1F
		public MissingReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x00041B37 File Offset: 0x0003FD37
		protected MissingReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040009DA RID: 2522
		private const int Result = -2147467261;

		// Token: 0x040009DB RID: 2523
		private string unityStackTrace;
	}
}
