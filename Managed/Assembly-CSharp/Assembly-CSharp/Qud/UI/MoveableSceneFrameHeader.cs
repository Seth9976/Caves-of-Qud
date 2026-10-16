using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x0200049C RID: 1180
	public class MoveableSceneFrameHeader : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IDragHandler
	{
		// Token: 0x06002D3D RID: 11581 RVA: 0x000F4DB9 File Offset: 0x000F2FB9
		public virtual void Init()
		{
			this.ToggleBackgroundOpacity(PlayerPrefs.GetInt(this.OpacityID));
			this.ToggleSideMovePin(PlayerPrefs.GetInt(this.SidePinID, 0) == 1);
			this.backgroundOpacityToggled.AddListener(new UnityAction<int>(this.UpdateBackgroundOpacity));
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000F4DF8 File Offset: 0x000F2FF8
		public void OnPointerEnter(PointerEventData eventData)
		{
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000F4DFA File Offset: 0x000F2FFA
		public void OnPointerExit(PointerEventData eventData)
		{
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000F4DFC File Offset: 0x000F2FFC
		public void OnDrag(PointerEventData eventData)
		{
			this.dragTarget.GetComponent<RectTransform>().anchoredPosition += new Vector2(eventData.delta.x, eventData.delta.y) / UIManager.scale;
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000F4E49 File Offset: 0x000F3049
		public string OpacityID
		{
			get
			{
				return this.prefsKey + "-Opacity";
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x000F4E5B File Offset: 0x000F305B
		public string SidePinID
		{
			get
			{
				return this.prefsKey + "-SidePin";
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000F4E6D File Offset: 0x000F306D
		public void UpdateBackgroundOpacity(int opacityLevel)
		{
			PlayerPrefs.SetInt(this.OpacityID, opacityLevel);
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000F4E7B File Offset: 0x000F307B
		public void ToggleBackgroundOpacity()
		{
			this.ToggleBackgroundOpacity(this.BackgroundOpacity + 1);
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000F4E8C File Offset: 0x000F308C
		public void ToggleBackgroundOpacity(int setTo)
		{
			if (setTo > 2)
			{
				setTo = 0;
			}
			if (setTo == this.BackgroundOpacity)
			{
				return;
			}
			this.BackgroundOpacity = setTo;
			Color color = this.backgroundImage.color;
			int backgroundOpacity = this.BackgroundOpacity;
			float num;
			if (backgroundOpacity != 0)
			{
				if (backgroundOpacity != 2)
				{
					num = 0.1f;
				}
				else
				{
					num = 1f;
				}
			}
			else
			{
				num = 0f;
			}
			color.a = num;
			this.backgroundImage.color = color;
			this.backgroundOpacityToggled.Invoke(this.BackgroundOpacity);
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000F4F08 File Offset: 0x000F3108
		public void ToggleSideMovePin()
		{
			this.ToggleSideMovePin(!this.sideMovePin);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000F4F1C File Offset: 0x000F311C
		public void ToggleSideMovePin(bool setTo)
		{
			PlayerPrefs.SetInt(this.SidePinID, setTo ? 1 : 0);
			this.sideMovePin = setTo;
			if (setTo)
			{
				this.sideMoveDisplay.color = Color.red;
			}
			else
			{
				this.sideMoveDisplay.color = Color.white;
			}
			this.sideMovePinToggled.Invoke(setTo);
		}

		// Token: 0x0400195B RID: 6491
		public GameObject highlightTarget;

		// Token: 0x0400195C RID: 6492
		public GameObject dragTarget;

		// Token: 0x0400195D RID: 6493
		public Image backgroundImage;

		// Token: 0x0400195E RID: 6494
		public UnityEvent<int> backgroundOpacityToggled = new UnityEvent<int>();

		// Token: 0x0400195F RID: 6495
		public UnityEvent<bool> sideMovePinToggled = new UnityEvent<bool>();

		// Token: 0x04001960 RID: 6496
		public bool sideMovePin;

		// Token: 0x04001961 RID: 6497
		public Image sideMoveDisplay;

		// Token: 0x04001962 RID: 6498
		public string prefsKey = "MessageLog";

		// Token: 0x04001963 RID: 6499
		public int BackgroundOpacity = 1;
	}
}
