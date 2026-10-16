using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x020002AE RID: 686
	[Serializable]
	public class MissingComponentException : SystemException
	{
		// Token: 0x060021AC RID: 8620 RVA: 0x00041AED File Offset: 0x0003FCED
		public MissingComponentException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x00041B08 File Offset: 0x0003FD08
		public MissingComponentException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x00041B1F File Offset: 0x0003FD1F
		public MissingComponentException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x00041B37 File Offset: 0x0003FD37
		protected MissingComponentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040009D6 RID: 2518
		private const int Result = -2147467261;

		// Token: 0x040009D7 RID: 2519
		private string unityStackTrace;
	}
}
