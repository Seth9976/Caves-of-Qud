using System;

namespace NAudio
{
	// Token: 0x0200003F RID: 63
	public class InvalidDataException : Exception
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00006B41 File Offset: 0x00004D41
		public InvalidDataException()
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006B49 File Offset: 0x00004D49
		public InvalidDataException(string message)
			: base(message)
		{
		}
	}
}
