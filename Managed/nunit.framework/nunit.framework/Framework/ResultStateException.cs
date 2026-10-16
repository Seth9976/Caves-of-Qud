using System;
using System.Runtime.Serialization;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	public abstract class ResultStateException : Exception
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000659E File Offset: 0x0000479E
		public ResultStateException(string message)
			: base(message)
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000065A7 File Offset: 0x000047A7
		public ResultStateException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000065B1 File Offset: 0x000047B1
		protected ResultStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000270 RID: 624
		public abstract ResultState ResultState { get; }
	}
}
