using System;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x0200054E RID: 1358
	[Serializable]
	public sealed class AmbiguousImplementationException : Exception
	{
		// Token: 0x060035AB RID: 13739 RVA: 0x000C1FAB File Offset: 0x000C01AB
		public AmbiguousImplementationException()
			: base("Ambiguous implementation found.")
		{
			base.HResult = -2146234262;
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000C1FC3 File Offset: 0x000C01C3
		public AmbiguousImplementationException(string message)
			: base(message)
		{
			base.HResult = -2146234262;
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000C1FD7 File Offset: 0x000C01D7
		public AmbiguousImplementationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146234262;
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x00020FA7 File Offset: 0x0001F1A7
		private AmbiguousImplementationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
