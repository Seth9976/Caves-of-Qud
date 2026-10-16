using System;
using System.Runtime.Serialization;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	public class SuccessException : ResultStateException
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0000656C File Offset: 0x0000476C
		public SuccessException(string message)
			: base(message)
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00006575 File Offset: 0x00004775
		public SuccessException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000657F File Offset: 0x0000477F
		protected SuccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000065BB File Offset: 0x000047BB
		public override ResultState ResultState
		{
			get
			{
				return ResultState.Success;
			}
		}
	}
}
