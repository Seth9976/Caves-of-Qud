using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
	// Token: 0x02000417 RID: 1047
	public class TableCellControlBuilder : ControlBuilder
	{
		/// <summary>Specifies whether white space literals are allowed.</summary>
		/// <returns>false.</returns>
		// Token: 0x06002F50 RID: 12112 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
