using System;
using System.Runtime.Serialization;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class NUnitException : Exception
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x00007993 File Offset: 0x00005B93
		public NUnitException()
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000659E File Offset: 0x0000479E
		public NUnitException(string message)
			: base(message)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000065A7 File Offset: 0x000047A7
		public NUnitException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000065B1 File Offset: 0x000047B1
		protected NUnitException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
