using System;
using System.Runtime.Serialization;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public class IgnoreException : ResultStateException
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000656C File Offset: 0x0000476C
		public IgnoreException(string message)
			: base(message)
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00006575 File Offset: 0x00004775
		public IgnoreException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000657F File Offset: 0x0000477F
		protected IgnoreException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00006590 File Offset: 0x00004790
		public override ResultState ResultState
		{
			get
			{
				return ResultState.Ignored;
			}
		}
	}
}
