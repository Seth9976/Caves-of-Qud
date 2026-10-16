using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers that indicate information about the context in which a request for Help information originated.</summary>
	// Token: 0x02000456 RID: 1110
	public enum HelpContextType
	{
		/// <summary>A general context.</summary>
		// Token: 0x040010E9 RID: 4329
		Ambient,
		/// <summary>A window.</summary>
		// Token: 0x040010EA RID: 4330
		Window,
		/// <summary>A selection.</summary>
		// Token: 0x040010EB RID: 4331
		Selection,
		/// <summary>A tool window selection.</summary>
		// Token: 0x040010EC RID: 4332
		ToolWindowSelection
	}
}
