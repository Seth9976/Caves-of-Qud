using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000056 RID: 86
	internal struct CharacterSubstitution
	{
		// Token: 0x06000251 RID: 593 RVA: 0x0001FCC5 File Offset: 0x0001DEC5
		public CharacterSubstitution(int index, uint unicode)
		{
			this.index = index;
			this.unicode = unicode;
		}

		// Token: 0x04000308 RID: 776
		public int index;

		// Token: 0x04000309 RID: 777
		public uint unicode;
	}
}
