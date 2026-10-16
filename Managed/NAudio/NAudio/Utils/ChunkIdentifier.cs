using System;
using System.Text;

namespace NAudio.Utils
{
	// Token: 0x02000045 RID: 69
	public class ChunkIdentifier
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00006D70 File Offset: 0x00004F70
		public static int ChunkIdentifierToInt32(string s)
		{
			if (s.Length != 4)
			{
				throw new ArgumentException("Must be a four character string");
			}
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			if (bytes.Length != 4)
			{
				throw new ArgumentException("Must encode to exactly four bytes");
			}
			return BitConverter.ToInt32(bytes, 0);
		}
	}
}
