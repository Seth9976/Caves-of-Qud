using System;

namespace NVorbis
{
	// Token: 0x02000006 RID: 6
	public class InvalidDataException : Exception
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000253D File Offset: 0x0000073D
		public InvalidDataException()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002545 File Offset: 0x00000745
		public InvalidDataException(string message)
			: base(message)
		{
		}
	}
}
