using System;

namespace Unity.Properties
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public class InvalidPathException : Exception
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002F73 File Offset: 0x00001173
		public InvalidPathException(string message)
			: base(message)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F7E File Offset: 0x0000117E
		public InvalidPathException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
