using System;
using System.Runtime.Serialization;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class InvalidDataSourceException : Exception
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00007993 File Offset: 0x00005B93
		public InvalidDataSourceException()
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000659E File Offset: 0x0000479E
		public InvalidDataSourceException(string message)
			: base(message)
		{
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000065A7 File Offset: 0x000047A7
		public InvalidDataSourceException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000065B1 File Offset: 0x000047B1
		protected InvalidDataSourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
