using System;

namespace System.ComponentModel
{
	/// <summary>Notifies clients that a property value is changing.</summary>
	// Token: 0x02000405 RID: 1029
	public interface INotifyPropertyChanging
	{
		/// <summary>Occurs when a property value is changing.</summary>
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600214F RID: 8527
		// (remove) Token: 0x06002150 RID: 8528
		event PropertyChangingEventHandler PropertyChanging;
	}
}
