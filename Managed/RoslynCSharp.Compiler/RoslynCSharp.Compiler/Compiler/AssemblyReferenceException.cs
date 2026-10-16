using System;
using System.Collections.Generic;

namespace RoslynCSharp.Compiler
{
	// Token: 0x02000005 RID: 5
	public sealed class AssemblyReferenceException : Exception
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002724 File Offset: 0x00000924
		public Exception[] ReferenceExceptions
		{
			get
			{
				return this.referenceExceptions;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000272C File Offset: 0x0000092C
		public AssemblyReferenceException(ICollection<Exception> allExceptions)
			: base(AssemblyReferenceException.msg)
		{
			this.referenceExceptions = new Exception[allExceptions.Count];
			int num = 0;
			foreach (Exception ex in allExceptions)
			{
				this.referenceExceptions[num] = ex;
				num++;
			}
		}

		// Token: 0x04000005 RID: 5
		private Exception[] referenceExceptions;

		// Token: 0x04000006 RID: 6
		internal static readonly string msg = "One or more assembly references could not be resolved. See ReferenceExceptions for more information";
	}
}
