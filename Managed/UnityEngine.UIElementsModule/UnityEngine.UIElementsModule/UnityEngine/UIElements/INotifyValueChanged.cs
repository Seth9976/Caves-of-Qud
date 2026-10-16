using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000113 RID: 275
	public interface INotifyValueChanged<T>
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060008AE RID: 2222
		// (set) Token: 0x060008AF RID: 2223
		T value { get; set; }

		// Token: 0x060008B0 RID: 2224
		void SetValueWithoutNotify(T newValue);
	}
}
