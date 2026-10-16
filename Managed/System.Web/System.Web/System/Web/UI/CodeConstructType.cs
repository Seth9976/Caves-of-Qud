using System;

namespace System.Web.UI
{
	/// <summary>Specifies the code constructs that can be parsed in the <see cref="M:System.Web.UI.PageParserFilter.ProcessCodeConstruct(System.Web.UI.CodeConstructType,System.String)" /> method of the <see cref="T:System.Web.UI.PageParserFilter" /> class.</summary>
	// Token: 0x020001AA RID: 426
	public enum CodeConstructType
	{
		/// <summary>An expression in &lt;% ... %&gt; tags.</summary>
		// Token: 0x04001389 RID: 5001
		CodeSnippet,
		/// <summary>An expression in &lt;%# ... %&gt; tags.</summary>
		// Token: 0x0400138A RID: 5002
		ExpressionSnippet,
		/// <summary>An expression in &lt;%= ... %&gt; tags.</summary>
		// Token: 0x0400138B RID: 5003
		DataBindingSnippet,
		/// <summary>An expression in a script element that contains the runat="server" attribute.</summary>
		// Token: 0x0400138C RID: 5004
		ScriptTag,
		/// <summary>An expression in &lt;%: ... %&gt; tags.</summary>
		// Token: 0x0400138D RID: 5005
		EncodedExpressionSnippet
	}
}
