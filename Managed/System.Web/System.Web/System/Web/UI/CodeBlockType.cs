using System;

namespace System.Web.UI
{
	/// <summary>Specifies the type of the code block.</summary>
	// Token: 0x02000784 RID: 1924
	public enum CodeBlockType
	{
		/// <summary>Indicates that the block type is for code. A <see cref="F:System.Web.UI.CodeBlockType.Code" /> block type is contained inside the &lt;%   %&gt; characters.</summary>
		// Token: 0x040025EA RID: 9706
		Code,
		/// <summary>Indicates that the block type is for data binding. A <see cref="F:System.Web.UI.CodeBlockType.DataBinding" /> block type is contained inside the &lt;%#   %&gt; characters.</summary>
		// Token: 0x040025EB RID: 9707
		DataBinding = 2,
		/// <summary>Indicates that the block type is for encoded expressions. A <see cref="F:System.Web.UI.CodeBlockType.EncodedExpression" /> block type is contained inside the &lt;%:   %&gt; characters.</summary>
		// Token: 0x040025EC RID: 9708
		EncodedExpression,
		/// <summary>Indicates that the block type is for data expressions. A <see cref="F:System.Web.UI.CodeBlockType.Expression" /> block type is contained inside the &lt;%=   %&gt; characters.</summary>
		// Token: 0x040025ED RID: 9709
		Expression = 1
	}
}
