using System;
using ConsoleLib.Console;
using Qud.UI;
using QupKit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.QupKit.Components
{
	// Token: 0x020003E5 RID: 997
	public class HoverTextButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000BE194 File Offset: 0x000BC394
		private Text theText
		{
			get
			{
				if (this._theText == null)
				{
					this._theText = base.gameObject.GetComponent<Text>();
				}
				return this._theText;
			}
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000BE1BC File Offset: 0x000BC3BC
		public void Update()
		{
			if (this.parentButton != null && this.parentButton.interactable && this.Hotkey != global::UnityEngine.KeyCode.None && Input.GetKeyDown(this.Hotkey))
			{
				if (this.HotkeyCapitalized != (GameManager.capslock || Input.GetKey(global::UnityEngine.KeyCode.LeftShift) || Input.GetKey(global::UnityEngine.KeyCode.RightShift)))
				{
					return;
				}
				if (!UIManager.instance.AllowPassthroughInput())
				{
					return;
				}
				Keyboard.ClearInput(false, false);
				LegacyViewManager.Instance.OnCommand(this.CommandID);
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000BE248 File Offset: 0x000BC448
		public void Awake()
		{
			if (this.theText != null)
			{
				this.theText.color = this.NormalColor;
			}
			if (this.HighlightClip != null)
			{
				this.HighlightSource = base.gameObject.AddComponent<AudioSource>();
				this.HighlightSource.volume = 1f;
				this.HighlightSource.clip = this.HighlightClip;
				this.HighlightSource.playOnAwake = false;
			}
			this.parentButton = base.GetComponent<Button>();
			base.GetComponent<Button>().onClick.AddListener(delegate
			{
				LegacyViewManager instance = LegacyViewManager.Instance;
				if (instance == null)
				{
					return;
				}
				instance.OnCommand(this.CommandID);
			});
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000BE2E8 File Offset: 0x000BC4E8
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.theText != null)
			{
				this.theText.color = this.HighlightColor;
			}
			if (this.HighlightSource != null)
			{
				this.HighlightSource.PlayOneShot(this.HighlightClip);
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000BE328 File Offset: 0x000BC528
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.theText != null)
			{
				this.theText.color = this.NormalColor;
			}
		}

		// Token: 0x040012A6 RID: 4774
		public string CommandID;

		// Token: 0x040012A7 RID: 4775
		public AudioClip HighlightClip;

		// Token: 0x040012A8 RID: 4776
		public global::UnityEngine.KeyCode Hotkey;

		// Token: 0x040012A9 RID: 4777
		public bool HotkeyCapitalized;

		// Token: 0x040012AA RID: 4778
		private AudioSource HighlightSource;

		// Token: 0x040012AB RID: 4779
		public Color NormalColor = new Color(1f, 1f, 1f, 0.8f);

		// Token: 0x040012AC RID: 4780
		public Color HighlightColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x040012AD RID: 4781
		private Text _theText;

		// Token: 0x040012AE RID: 4782
		private Button parentButton;
	}
}
