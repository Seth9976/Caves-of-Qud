using System;

namespace System.Web.UI
{
	/// <summary>Supports the page parser in building controls that are connected to a data provider. This class cannot be inherited.</summary>
	// Token: 0x02000158 RID: 344
	public sealed class DataSourceControlBuilder : ControlBuilder
	{
		/// <summary>Determines whether white-space literals are permitted in the content between a control's opening and closing tags.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
