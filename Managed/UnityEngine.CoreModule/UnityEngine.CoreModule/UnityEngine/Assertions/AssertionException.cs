using System;

namespace UnityEngine.Assertions
{
	// Token: 0x0200046C RID: 1132
	public class AssertionException : Exception
	{
		// Token: 0x06002CBC RID: 11452 RVA: 0x00054A87 File Offset: 0x00052C87
		public AssertionException(string message, string userMessage)
			: base(message)
		{
			this.m_UserMessage = userMessage;
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x00054A9C File Offset: 0x00052C9C
		public override string Message
		{
			get
			{
				string text = base.Message;
				bool flag = this.m_UserMessage != null;
				if (flag)
				{
					text = this.m_UserMessage + "\n" + text;
				}
				return text;
			}
		}

		// Token: 0x04000C56 RID: 3158
		private string m_UserMessage;
	}
}
