using System;
using System.Collections.Generic;

namespace System.Web.SessionState
{
	/// <summary>When implemented in a type, returns a list of zero or more session keys that indicate to a session-state provider which session-state items have to be retrieved.</summary>
	// Token: 0x020006E3 RID: 1763
	public interface IPartialSessionState
	{
		/// <summary>Gets a list of keys that are associated with session-state values.</summary>
		/// <returns>A generic list of strings that serve as keys for session-state values. </returns>
		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06004AA0 RID: 19104
		IList<string> PartialSessionStateKeys { get; }
	}
}
