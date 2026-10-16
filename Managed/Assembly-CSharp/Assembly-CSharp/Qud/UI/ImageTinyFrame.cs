using System;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200041D RID: 1053
	[ExecuteAlways]
	[RequireComponent(typeof(LayoutElement))]
	public class ImageTinyFrame : MonoBehaviour
	{
		// Token: 0x060027FA RID: 10234 RVA: 0x000CEB25 File Offset: 0x000CCD25
		private void Start()
		{
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000CEB27 File Offset: 0x000CCD27
		private void Update()
		{
			this.Sync(false);
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000CEB30 File Offset: 0x000CCD30
		public void Sync(bool force = false)
		{
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
			if (this._layoutElement == null)
			{
				this._layoutElement = base.GetComponent<LayoutElement>();
			}
			if (this._lastImageSize != this.imageSize && this.image != null)
			{
				if (this.totalSize.magnitude < 1f)
				{
					this.image.rectTransform.sizeDelta = (this._lastImageSize = this.imageSize);
					this.totalSize = (this._rectTransform.sizeDelta = this.imageSize + new Vector2(16f, 14f));
				}
				this._layoutElement.preferredWidth = this.totalSize.x;
				this._layoutElement.preferredHeight = this.totalSize.y;
			}
			if (this.useImageSwap)
			{
				if (this._frameworkContext == null)
				{
					this._frameworkContext = base.GetComponentInParent<FrameworkContext>();
				}
				FrameworkContext frameworkContext = this._frameworkContext;
				bool? flag;
				if (frameworkContext == null)
				{
					flag = null;
				}
				else
				{
					NavigationContext context = frameworkContext.context;
					flag = ((context != null) ? new bool?(context.IsActive(true)) : null);
				}
				bool? flag2 = flag;
				bool valueOrDefault = flag2.GetValueOrDefault();
				bool flag3 = valueOrDefault;
				flag2 = this._lastActiveSwap;
				if (!((flag3 == flag2.GetValueOrDefault()) & (flag2 != null)) || force)
				{
					this._lastActiveSwap = new bool?(valueOrDefault);
					this.image.sprite = (valueOrDefault ? this.selectedImageSwap : this.unselectedImageSwap);
				}
			}
			if (this.colorBasedOnSelection)
			{
				if (this._frameworkContext == null)
				{
					this._frameworkContext = base.GetComponentInParent<FrameworkContext>();
				}
				FrameworkContext frameworkContext2 = this._frameworkContext;
				bool? flag4;
				if (frameworkContext2 == null)
				{
					flag4 = null;
				}
				else
				{
					NavigationContext context2 = frameworkContext2.context;
					flag4 = ((context2 != null) ? new bool?(context2.IsActive(true)) : null);
				}
				bool? flag2 = flag4;
				bool valueOrDefault2 = flag2.GetValueOrDefault();
				bool flag5 = valueOrDefault2;
				flag2 = this._lastActive;
				if (!((flag5 == flag2.GetValueOrDefault()) & (flag2 != null)) || force)
				{
					this._lastActive = new bool?(valueOrDefault2);
					this.borderColor = (valueOrDefault2 ? this.selectedBorderColor : this.unselectedBorderColor);
					if (this.useThreeColor)
					{
						this.ThreeColor.SetColors(valueOrDefault2 ? this.selectedForegroundColor : this.unselectedForegroundColor, valueOrDefault2 ? this.selectedDetailColor : this.unselectedDetailColor, Color.clear);
					}
					TitledIconButton component = base.GetComponent<TitledIconButton>();
					UITextSkin uitextSkin = ((component != null) ? component.TitleText : null);
					if (uitextSkin != null)
					{
						uitextSkin.color = this.borderColor;
						uitextSkin.StripFormatting = !valueOrDefault2;
						uitextSkin.Apply();
					}
				}
			}
			if (this._lastBorderColor != this.borderColor && this.borderImage != null)
			{
				this.borderImage.color = (this._lastBorderColor = this.borderColor);
			}
			if (this._lastSprite != this.sprite)
			{
				this.image.sprite = (this._lastSprite = this.sprite);
			}
		}

		// Token: 0x040013BD RID: 5053
		public Sprite sprite;

		// Token: 0x040013BE RID: 5054
		public Color borderColor;

		// Token: 0x040013BF RID: 5055
		public bool colorBasedOnSelection;

		// Token: 0x040013C0 RID: 5056
		public Color selectedBorderColor;

		// Token: 0x040013C1 RID: 5057
		public Color unselectedBorderColor;

		// Token: 0x040013C2 RID: 5058
		public Vector2 imageSize;

		// Token: 0x040013C3 RID: 5059
		public Vector2 totalSize;

		// Token: 0x040013C4 RID: 5060
		public Image image;

		// Token: 0x040013C5 RID: 5061
		public Image borderImage;

		// Token: 0x040013C6 RID: 5062
		public bool useThreeColor;

		// Token: 0x040013C7 RID: 5063
		public UIThreeColorProperties ThreeColor;

		// Token: 0x040013C8 RID: 5064
		public Color selectedForegroundColor = Color.white;

		// Token: 0x040013C9 RID: 5065
		public Color unselectedForegroundColor = Color.gray;

		// Token: 0x040013CA RID: 5066
		public Color selectedDetailColor = Color.yellow;

		// Token: 0x040013CB RID: 5067
		public Color unselectedDetailColor = Color.black;

		// Token: 0x040013CC RID: 5068
		public bool useImageSwap;

		// Token: 0x040013CD RID: 5069
		public Sprite unselectedImageSwap;

		// Token: 0x040013CE RID: 5070
		public Sprite selectedImageSwap;

		// Token: 0x040013CF RID: 5071
		private Vector2 _lastImageSize;

		// Token: 0x040013D0 RID: 5072
		private Color _lastBorderColor;

		// Token: 0x040013D1 RID: 5073
		private RectTransform _rectTransform;

		// Token: 0x040013D2 RID: 5074
		private LayoutElement _layoutElement;

		// Token: 0x040013D3 RID: 5075
		private Sprite _lastSprite;

		// Token: 0x040013D4 RID: 5076
		private bool? _lastActive;

		// Token: 0x040013D5 RID: 5077
		private bool? _lastActiveSwap;

		// Token: 0x040013D6 RID: 5078
		private FrameworkContext _frameworkContext;
	}
}
