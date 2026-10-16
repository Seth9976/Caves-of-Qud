using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D5 RID: 725
	internal interface IGroupManager
	{
		// Token: 0x060014C6 RID: 5318
		void Init(IGroupBox groupBox);

		// Token: 0x060014C7 RID: 5319
		IGroupBoxOption GetSelectedOption();

		// Token: 0x060014C8 RID: 5320
		void OnOptionSelectionChanged(IGroupBoxOption selectedOption);

		// Token: 0x060014C9 RID: 5321
		void RegisterOption(IGroupBoxOption option);

		// Token: 0x060014CA RID: 5322
		void UnregisterOption(IGroupBoxOption option);
	}
}
