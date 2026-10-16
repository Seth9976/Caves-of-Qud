using System;

namespace System.Web.SessionState
{
	/// <summary>Specifies the type of session support that is required in order to handle a request. </summary>
	// Token: 0x0200048D RID: 1165
	public enum SessionStateBehavior
	{
		/// <summary>The default ASP.NET logic is used to determine the session state behavior for the request. The default logic looks for the existence of marker session state interfaces on the <see cref="T:System.Web.IHttpHandler" />.</summary>
		// Token: 0x04001D3E RID: 7486
		Default,
		/// <summary>Full read-write session state behavior is enabled for the request. This setting overrides whatever session behavior would have been determined by inspecting the handler for the request.</summary>
		// Token: 0x04001D3F RID: 7487
		Required,
		/// <summary>Read-only session state is enabled for the request. This means that session state cannot be updated. This setting overrides whatever session state behavior would have been determined by inspecting the handler for the request.</summary>
		// Token: 0x04001D40 RID: 7488
		ReadOnly,
		/// <summary>Session state is not enabled for processing the request. This setting overrides whatever session behavior would have been determined by inspecting the handler for the request.</summary>
		// Token: 0x04001D41 RID: 7489
		Disabled
	}
}
