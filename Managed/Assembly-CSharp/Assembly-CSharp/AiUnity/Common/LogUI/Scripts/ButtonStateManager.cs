using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002C5 RID: 709
	public class ButtonStateManager : MonoBehaviour
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0007F747 File Offset: 0x0007D947
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0007F74F File Offset: 0x0007D94F
		public Button Button { get; set; }

		// Token: 0x06001897 RID: 6295 RVA: 0x0007F758 File Offset: 0x0007D958
		public void ToggleButtonState()
		{
			this.ToggleButtonState(!this.pressed);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0007F76C File Offset: 0x0007D96C
		public void ToggleButtonState(bool pressed)
		{
			this.pressed = pressed;
			this.buttonImage.overrideSprite = (this.pressed ? this.pressedSprite : null);
			ButtonState buttonState = (this.pressed ? ButtonState.Pressed : ((ButtonState)0));
			ExecuteEvents.ExecuteHierarchy<IButtonStateChange>(base.gameObject, null, delegate(IButtonStateChange x, BaseEventData y)
			{
				x.ButtonStateChange(buttonState);
			});
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0007F7CD File Offset: 0x0007D9CD
		private void Start()
		{
			this.Button = base.GetComponent<Button>();
			this.Button.onClick.AddListener(new UnityAction(this.ToggleButtonState));
			this.buttonImage = base.GetComponent<Image>();
			this.ToggleButtonState(true);
		}

		// Token: 0x04000E93 RID: 3731
		public bool pressed;

		// Token: 0x04000E94 RID: 3732
		public Sprite pressedSprite;

		// Token: 0x04000E95 RID: 3733
		private Image buttonImage;
	}
}
