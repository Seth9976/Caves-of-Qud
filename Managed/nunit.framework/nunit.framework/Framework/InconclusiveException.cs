using System;
using System.Runtime.Serialization;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public class InconclusiveException : ResultStateException
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000656C File Offset: 0x0000476C
		public InconclusiveException(string message)
			: base(message)
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00006575 File Offset: 0x00004775
		public InconclusiveException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000657F File Offset: 0x0000477F
		protected InconclusiveException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00006597 File Offset: 0x00004797
		public override ResultState ResultState
		{
			get
			{
				return ResultState.Inconclusive;
			}
		}
	}
}
