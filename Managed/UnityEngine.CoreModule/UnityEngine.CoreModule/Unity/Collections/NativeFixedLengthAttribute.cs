using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x0200009C RID: 156
	[AttributeUsage(AttributeTargets.Field)]
	[RequiredByNativeCode]
	public sealed class NativeFixedLengthAttribute : Attribute
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00006631 File Offset: 0x00004831
		public NativeFixedLengthAttribute(int fixedLength)
		{
			this.FixedLength = fixedLength;
		}

		// Token: 0x04000259 RID: 601
		public int FixedLength;
	}
}
