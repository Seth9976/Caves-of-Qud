using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	// Token: 0x020003D7 RID: 983
	public class MultiViewControlBuilder : ControlBuilder
	{
		/// <summary>Adds builders to the <see cref="T:System.Web.UI.ControlBuilder" /> object for any child controls that belong to the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="subBuilder">The ControlBuilder object assigned to the child control. </param>
		// Token: 0x06002A57 RID: 10839 RVA: 0x0006E689 File Offset: 0x0006C889
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			base.AppendSubBuilder(subBuilder);
		}
	}
}
