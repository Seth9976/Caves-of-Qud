using System;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x020005DF RID: 1503
	internal class InternalException : SystemException
	{
		// Token: 0x06003041 RID: 12353 RVA: 0x000A6473 File Offset: 0x000A4673
		internal InternalException()
		{
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x00055723 File Offset: 0x00053923
		internal InternalException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
