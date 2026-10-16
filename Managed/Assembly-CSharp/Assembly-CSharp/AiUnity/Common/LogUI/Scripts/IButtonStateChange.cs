using System;
using UnityEngine.EventSystems;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002CA RID: 714
	public interface IButtonStateChange : IEventSystemHandler
	{
		// Token: 0x060018C7 RID: 6343
		void ButtonStateChange(ButtonState buttonState);
	}
}
