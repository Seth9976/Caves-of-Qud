using System;

namespace System.Web.SessionState
{
	/// <summary>Specifies that the target HTTP handler requires only read access to session-state values. This is a marker interface and has no methods.</summary>
	// Token: 0x0200048B RID: 1163
	public interface IReadOnlySessionState : IRequiresSessionState
	{
	}
}
