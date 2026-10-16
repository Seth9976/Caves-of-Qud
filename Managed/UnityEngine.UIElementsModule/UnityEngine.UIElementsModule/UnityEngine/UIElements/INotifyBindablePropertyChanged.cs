using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004B RID: 75
	public interface INotifyBindablePropertyChanged
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000271 RID: 625
		// (remove) Token: 0x06000272 RID: 626
		event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;
	}
}
