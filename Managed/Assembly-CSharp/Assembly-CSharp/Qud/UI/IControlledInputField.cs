using System;

namespace Qud.UI
{
	// Token: 0x02000415 RID: 1045
	public interface IControlledInputField : IControlledSelectable
	{
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060027B4 RID: 10164
		bool isFocused { get; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060027B5 RID: 10165
		// (set) Token: 0x060027B6 RID: 10166
		bool selected { get; set; }

		// Token: 0x060027B7 RID: 10167
		void Init();
	}
}
