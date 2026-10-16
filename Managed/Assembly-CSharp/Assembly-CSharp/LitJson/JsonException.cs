using System;

namespace LitJson
{
	// Token: 0x0200031D RID: 797
	public class JsonException : ApplicationException
	{
		// Token: 0x06001D9A RID: 7578 RVA: 0x00091195 File Offset: 0x0008F395
		public JsonException()
		{
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0009119D File Offset: 0x0008F39D
		internal JsonException(ParserToken token)
			: base(string.Format("Invalid token '{0}' in input string", token))
		{
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000911B5 File Offset: 0x0008F3B5
		internal JsonException(ParserToken token, Exception inner_exception)
			: base(string.Format("Invalid token '{0}' in input string", token), inner_exception)
		{
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000911CE File Offset: 0x0008F3CE
		internal JsonException(int c)
			: base(string.Format("Invalid character '{0}' in input string", (char)c))
		{
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000911E7 File Offset: 0x0008F3E7
		internal JsonException(int c, Exception inner_exception)
			: base(string.Format("Invalid character '{0}' in input string", (char)c), inner_exception)
		{
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00091201 File Offset: 0x0008F401
		public JsonException(string message)
			: base(message)
		{
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0009120A File Offset: 0x0008F40A
		public JsonException(string message, Exception inner_exception)
			: base(message, inner_exception)
		{
		}
	}
}
