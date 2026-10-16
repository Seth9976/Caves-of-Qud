using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000054 RID: 84
	[DebuggerDisplay("Unicode ({unicode})  '{(char)unicode}'")]
	internal struct TextProcessingElement
	{
		// Token: 0x04000302 RID: 770
		public TextProcessingElementType elementType;

		// Token: 0x04000303 RID: 771
		public uint unicode;

		// Token: 0x04000304 RID: 772
		public int stringIndex;

		// Token: 0x04000305 RID: 773
		public int length;
	}
}
