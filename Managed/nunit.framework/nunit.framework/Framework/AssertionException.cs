using System;
using System.Runtime.Serialization;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class AssertionException : ResultStateException
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000656C File Offset: 0x0000476C
		public AssertionException(string message)
			: base(message)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00006575 File Offset: 0x00004775
		public AssertionException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000657F File Offset: 0x0000477F
		protected AssertionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00006589 File Offset: 0x00004789
		public override ResultState ResultState
		{
			get
			{
				return ResultState.Failure;
			}
		}
	}
}
