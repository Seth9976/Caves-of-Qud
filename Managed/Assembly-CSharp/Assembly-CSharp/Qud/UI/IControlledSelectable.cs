using System;

namespace Qud.UI
{
	// Token: 0x02000417 RID: 1047
	public interface IControlledSelectable
	{
		// Token: 0x060027C7 RID: 10183
		bool IsSelected();

		// Token: 0x060027C8 RID: 10184
		void Select();

		// Token: 0x060027C9 RID: 10185
		bool IsInFullView();

		// Token: 0x060027CA RID: 10186
		bool IsInView();
	}
}
