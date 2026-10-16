using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI
{
	// Token: 0x02001476 RID: 5238
	[ExecuteAlways]
	[RequireComponent(typeof(Image))]
	public class UIImageSkin : MonoBehaviour
	{
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x0600CE2F RID: 52783 RVA: 0x004AE164 File Offset: 0x004AC364
		public RectTransform rectTransform
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this._rt) == null)
				{
					rectTransform = (this._rt = base.GetComponent<RectTransform>());
				}
				return rectTransform;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x0600CE30 RID: 52784 RVA: 0x004AE18A File Offset: 0x004AC38A
		private Image img
		{
			get
			{
				if (this._img == null)
				{
					this._img = base.GetComponent<Image>();
				}
				return this._img;
			}
		}

		// Token: 0x0600CE31 RID: 52785 RVA: 0x004AE1AC File Offset: 0x004AC3AC
		private Image Updated()
		{
			this.Apply();
			return this.img;
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600CE32 RID: 52786 RVA: 0x004AE1BC File Offset: 0x004AC3BC
		public static Dictionary<UIImageSkin.ImageMaterial, Material> materialMap
		{
			get
			{
				if (UIImageSkin._materialMap == null)
				{
					UIImageSkin._materialMap = new Dictionary<UIImageSkin.ImageMaterial, Material>
					{
						{
							UIImageSkin.ImageMaterial.diag,
							(Material)Resources.Load("Materials/TextureOverlay-Diag")
						},
						{
							UIImageSkin.ImageMaterial.horiz,
							(Material)Resources.Load("Materials/TextureOverlay-Horiz")
						}
					};
				}
				return UIImageSkin._materialMap;
			}
		}

		// Token: 0x0600CE33 RID: 52787 RVA: 0x004AE20C File Offset: 0x004AC40C
		public void Apply()
		{
			if (this.color != UIImageSkin.ImageColor.unset)
			{
				this.img.color = UIImageSkin.colorMap[this.color];
			}
			if (this.material != this.lastMaterial)
			{
				this.lastMaterial = this.material;
				this.img.material = UIImageSkin.materialMap[this.material];
			}
		}

		// Token: 0x0600CE34 RID: 52788 RVA: 0x004AE272 File Offset: 0x004AC472
		public void Start()
		{
			this.Apply();
		}

		// Token: 0x04004FDA RID: 20442
		private RectTransform _rt;

		// Token: 0x04004FDB RID: 20443
		private Image _img;

		// Token: 0x04004FDC RID: 20444
		public static Dictionary<UIImageSkin.ImageColor, Color> colorMap = new Dictionary<UIImageSkin.ImageColor, Color>
		{
			{
				UIImageSkin.ImageColor.borderLine,
				ConsoleLib.Console.ColorUtility.FromWebColor("4D6E7A")
			},
			{
				UIImageSkin.ImageColor.darkBackground,
				ConsoleLib.Console.ColorUtility.FromWebColor("041A1A")
			},
			{
				UIImageSkin.ImageColor.mediumBackground,
				ConsoleLib.Console.ColorUtility.FromWebColor("0A2625")
			},
			{
				UIImageSkin.ImageColor.lightBackground,
				ConsoleLib.Console.ColorUtility.FromWebColor("1A343B")
			},
			{
				UIImageSkin.ImageColor.brightBackground,
				ConsoleLib.Console.ColorUtility.FromWebColor("203C3F")
			},
			{
				UIImageSkin.ImageColor.darkFrame,
				ConsoleLib.Console.ColorUtility.FromWebColor("101F23")
			},
			{
				UIImageSkin.ImageColor.mediumFrame,
				ConsoleLib.Console.ColorUtility.FromWebColor("1A343B")
			},
			{
				UIImageSkin.ImageColor.brightFrame,
				ConsoleLib.Console.ColorUtility.FromWebColor("475D64")
			}
		};

		// Token: 0x04004FDD RID: 20445
		public static Dictionary<UIImageSkin.ImageMaterial, Material> _materialMap = null;

		// Token: 0x04004FDE RID: 20446
		public UIImageSkin.ImageColor color;

		// Token: 0x04004FDF RID: 20447
		public UIImageSkin.ImageMaterial material = UIImageSkin.ImageMaterial.unset;

		// Token: 0x04004FE0 RID: 20448
		private UIImageSkin.ImageMaterial lastMaterial = UIImageSkin.ImageMaterial.unset;

		// Token: 0x02001D8F RID: 7567
		public enum ImageColor
		{
			// Token: 0x04007343 RID: 29507
			unset = -1,
			// Token: 0x04007344 RID: 29508
			borderLine,
			// Token: 0x04007345 RID: 29509
			darkBackground,
			// Token: 0x04007346 RID: 29510
			mediumBackground,
			// Token: 0x04007347 RID: 29511
			lightBackground,
			// Token: 0x04007348 RID: 29512
			brightBackground,
			// Token: 0x04007349 RID: 29513
			darkFrame,
			// Token: 0x0400734A RID: 29514
			mediumFrame,
			// Token: 0x0400734B RID: 29515
			brightFrame
		}

		// Token: 0x02001D90 RID: 7568
		public enum ImageMaterial
		{
			// Token: 0x0400734D RID: 29517
			unset = -1,
			// Token: 0x0400734E RID: 29518
			diag,
			// Token: 0x0400734F RID: 29519
			horiz
		}
	}
}
