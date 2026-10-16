using System;

namespace Microsoft.DiaSymReader
{
	// Token: 0x02000103 RID: 259
	internal sealed class SymUnmanagedWriterException : Exception
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000190E5 File Offset: 0x000172E5
		public string ImplementationModuleName { get; }

		// Token: 0x060008D3 RID: 2259 RVA: 0x000190ED File Offset: 0x000172ED
		public SymUnmanagedWriterException()
		{
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000190F5 File Offset: 0x000172F5
		public SymUnmanagedWriterException(string message)
			: base(message)
		{
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000190FE File Offset: 0x000172FE
		public SymUnmanagedWriterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00019108 File Offset: 0x00017308
		public SymUnmanagedWriterException(string message, Exception innerException, string implementationModuleName)
			: base(message, innerException)
		{
			this.ImplementationModuleName = implementationModuleName;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00019119 File Offset: 0x00017319
		internal SymUnmanagedWriterException(Exception innerException, string implementationModuleName)
			: this(innerException.Message, innerException, implementationModuleName)
		{
		}
	}
}
