using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200071A RID: 1818
	public struct StyleValues
	{
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x000CC4C4 File Offset: 0x000CA6C4
		// (set) Token: 0x06003476 RID: 13430 RVA: 0x000CC4EE File Offset: 0x000CA6EE
		public float top
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Top).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Top, value);
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000CC500 File Offset: 0x000CA700
		// (set) Token: 0x06003478 RID: 13432 RVA: 0x000CC52A File Offset: 0x000CA72A
		public float left
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Left).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Left, value);
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x000CC53C File Offset: 0x000CA73C
		// (set) Token: 0x0600347A RID: 13434 RVA: 0x000CC566 File Offset: 0x000CA766
		public float width
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Width).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Width, value);
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000CC578 File Offset: 0x000CA778
		// (set) Token: 0x0600347C RID: 13436 RVA: 0x000CC5A2 File Offset: 0x000CA7A2
		public float height
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Height).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Height, value);
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000CC5B4 File Offset: 0x000CA7B4
		// (set) Token: 0x0600347E RID: 13438 RVA: 0x000CC5DE File Offset: 0x000CA7DE
		public float right
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Right).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Right, value);
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000CC5F0 File Offset: 0x000CA7F0
		// (set) Token: 0x06003480 RID: 13440 RVA: 0x000CC61A File Offset: 0x000CA81A
		public float bottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Bottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Bottom, value);
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x000CC62C File Offset: 0x000CA82C
		// (set) Token: 0x06003482 RID: 13442 RVA: 0x000CC656 File Offset: 0x000CA856
		public Color color
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.Color).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Color, value);
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x000CC668 File Offset: 0x000CA868
		// (set) Token: 0x06003484 RID: 13444 RVA: 0x000CC692 File Offset: 0x000CA892
		public Color backgroundColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.BackgroundColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BackgroundColor, value);
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000CC6A4 File Offset: 0x000CA8A4
		// (set) Token: 0x06003486 RID: 13446 RVA: 0x000CC6CE File Offset: 0x000CA8CE
		public Color unityBackgroundImageTintColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.UnityBackgroundImageTintColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.UnityBackgroundImageTintColor, value);
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000CC6E0 File Offset: 0x000CA8E0
		// (set) Token: 0x06003488 RID: 13448 RVA: 0x000CC70A File Offset: 0x000CA90A
		public Color borderColor
		{
			get
			{
				return this.Values().GetStyleColor(StylePropertyId.BorderColor).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderColor, value);
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x000CC71C File Offset: 0x000CA91C
		// (set) Token: 0x0600348A RID: 13450 RVA: 0x000CC746 File Offset: 0x000CA946
		public float marginLeft
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginLeft).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginLeft, value);
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x000CC758 File Offset: 0x000CA958
		// (set) Token: 0x0600348C RID: 13452 RVA: 0x000CC782 File Offset: 0x000CA982
		public float marginTop
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginTop).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginTop, value);
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000CC794 File Offset: 0x000CA994
		// (set) Token: 0x0600348E RID: 13454 RVA: 0x000CC7BE File Offset: 0x000CA9BE
		public float marginRight
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginRight).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginRight, value);
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000CC7D0 File Offset: 0x000CA9D0
		// (set) Token: 0x06003490 RID: 13456 RVA: 0x000CC7FA File Offset: 0x000CA9FA
		public float marginBottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.MarginBottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.MarginBottom, value);
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000CC80C File Offset: 0x000CAA0C
		// (set) Token: 0x06003492 RID: 13458 RVA: 0x000CC836 File Offset: 0x000CAA36
		public float paddingLeft
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingLeft).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingLeft, value);
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000CC848 File Offset: 0x000CAA48
		// (set) Token: 0x06003494 RID: 13460 RVA: 0x000CC872 File Offset: 0x000CAA72
		public float paddingTop
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingTop).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingTop, value);
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000CC884 File Offset: 0x000CAA84
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x000CC8AE File Offset: 0x000CAAAE
		public float paddingRight
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingRight).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingRight, value);
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000CC8C0 File Offset: 0x000CAAC0
		// (set) Token: 0x06003498 RID: 13464 RVA: 0x000CC8EA File Offset: 0x000CAAEA
		public float paddingBottom
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingBottom).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingBottom, value);
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000CC8FC File Offset: 0x000CAAFC
		// (set) Token: 0x0600349A RID: 13466 RVA: 0x000CC926 File Offset: 0x000CAB26
		public float borderLeftWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderLeftWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderLeftWidth, value);
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000CC938 File Offset: 0x000CAB38
		// (set) Token: 0x0600349C RID: 13468 RVA: 0x000CC962 File Offset: 0x000CAB62
		public float borderRightWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderRightWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderRightWidth, value);
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000CC974 File Offset: 0x000CAB74
		// (set) Token: 0x0600349E RID: 13470 RVA: 0x000CC99E File Offset: 0x000CAB9E
		public float borderTopWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopWidth, value);
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000CC9B0 File Offset: 0x000CABB0
		// (set) Token: 0x060034A0 RID: 13472 RVA: 0x000CC9DA File Offset: 0x000CABDA
		public float borderBottomWidth
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomWidth).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomWidth, value);
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000CC9EC File Offset: 0x000CABEC
		// (set) Token: 0x060034A2 RID: 13474 RVA: 0x000CCA16 File Offset: 0x000CAC16
		public float borderTopLeftRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopLeftRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopLeftRadius, value);
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060034A3 RID: 13475 RVA: 0x000CCA28 File Offset: 0x000CAC28
		// (set) Token: 0x060034A4 RID: 13476 RVA: 0x000CCA52 File Offset: 0x000CAC52
		public float borderTopRightRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderTopRightRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderTopRightRadius, value);
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000CCA64 File Offset: 0x000CAC64
		// (set) Token: 0x060034A6 RID: 13478 RVA: 0x000CCA8E File Offset: 0x000CAC8E
		public float borderBottomLeftRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomLeftRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomLeftRadius, value);
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060034A7 RID: 13479 RVA: 0x000CCAA0 File Offset: 0x000CACA0
		// (set) Token: 0x060034A8 RID: 13480 RVA: 0x000CCACA File Offset: 0x000CACCA
		public float borderBottomRightRadius
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.BorderBottomRightRadius).value;
			}
			set
			{
				this.SetValue(StylePropertyId.BorderBottomRightRadius, value);
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x000CCADC File Offset: 0x000CACDC
		// (set) Token: 0x060034AA RID: 13482 RVA: 0x000CCB06 File Offset: 0x000CAD06
		public float opacity
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.Opacity).value;
			}
			set
			{
				this.SetValue(StylePropertyId.Opacity, value);
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060034AB RID: 13483 RVA: 0x000CCB18 File Offset: 0x000CAD18
		// (set) Token: 0x060034AC RID: 13484 RVA: 0x000CCB42 File Offset: 0x000CAD42
		public float flexGrow
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.FlexGrow).value;
			}
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060034AD RID: 13485 RVA: 0x000CCB54 File Offset: 0x000CAD54
		// (set) Token: 0x060034AE RID: 13486 RVA: 0x000CCB42 File Offset: 0x000CAD42
		public float flexShrink
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.FlexShrink).value;
			}
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000CCB80 File Offset: 0x000CAD80
		internal void SetValue(StylePropertyId id, float value)
		{
			StyleValue styleValue = default(StyleValue);
			styleValue.id = id;
			styleValue.number = value;
			this.Values().SetStyleValue(styleValue);
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000CCBB4 File Offset: 0x000CADB4
		internal void SetValue(StylePropertyId id, Color value)
		{
			StyleValue styleValue = default(StyleValue);
			styleValue.id = id;
			styleValue.color = value;
			this.Values().SetStyleValue(styleValue);
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000CCBE8 File Offset: 0x000CADE8
		internal StyleValueCollection Values()
		{
			bool flag = this.m_StyleValues == null;
			if (flag)
			{
				this.m_StyleValues = new StyleValueCollection();
			}
			return this.m_StyleValues;
		}

		// Token: 0x04001AE9 RID: 6889
		internal StyleValueCollection m_StyleValues;
	}
}
