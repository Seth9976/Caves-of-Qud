using System;

namespace XRL.Wish
{
	// Token: 0x02000568 RID: 1384
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class WishCommand : Attribute
	{
		// Token: 0x06003C38 RID: 15416 RVA: 0x001266C6 File Offset: 0x001248C6
		public WishCommand(string Command = null, string Regex = null)
		{
			this.Command = Command;
			this.Regex = Regex;
		}

		// Token: 0x04001D90 RID: 7568
		public string Command;

		// Token: 0x04001D91 RID: 7569
		public string Regex;
	}
}
