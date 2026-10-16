using System;
using System.Runtime.Serialization;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000068 RID: 104
	[Serializable]
	public class InvalidTestFixtureException : Exception
	{
		// Token: 0x06000389 RID: 905 RVA: 0x00007993 File Offset: 0x00005B93
		public InvalidTestFixtureException()
		{
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000659E File Offset: 0x0000479E
		public InvalidTestFixtureException(string message)
			: base(message)
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000065A7 File Offset: 0x000047A7
		public InvalidTestFixtureException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000065B1 File Offset: 0x000047B1
		protected InvalidTestFixtureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
