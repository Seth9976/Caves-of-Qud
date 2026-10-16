using System;

namespace System.Web.UI
{
	/// <summary>Provides access to the <see cref="T:System.Web.UI.CodeBlockType" /> of a code block builder.</summary>
	// Token: 0x02000786 RID: 1926
	public interface ICodeBlockTypeAccessor
	{
		/// <summary>Gets the type of code block.</summary>
		/// <returns>The type of code block.</returns>
		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06004E1C RID: 19996
		CodeBlockType BlockType { get; }
	}
}
