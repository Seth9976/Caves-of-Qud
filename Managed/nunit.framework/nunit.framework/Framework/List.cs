using System;
using System.Collections;

namespace NUnit.Framework
{
	// Token: 0x02000057 RID: 87
	public class List
	{
		// Token: 0x060002CC RID: 716 RVA: 0x00006A87 File Offset: 0x00004C87
		public static ListMapper Map(ICollection actual)
		{
			return new ListMapper(actual);
		}
	}
}
