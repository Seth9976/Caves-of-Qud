using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class exAtlas : ScriptableObject
{
	// Token: 0x17000097 RID: 151
	// (get) Token: 0x0600061B RID: 1563 RVA: 0x0002C78A File Offset: 0x0002A98A
	// (set) Token: 0x0600061C RID: 1564 RVA: 0x0002C792 File Offset: 0x0002A992
	public int trimThreshold
	{
		get
		{
			return this.trimThreshold_;
		}
		set
		{
			this.trimThreshold_ = Math.Min(Math.Max(value, 1), 255);
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x0600061D RID: 1565 RVA: 0x0002C7AB File Offset: 0x0002A9AB
	// (set) Token: 0x0600061E RID: 1566 RVA: 0x0002C7B4 File Offset: 0x0002A9B4
	public float scale
	{
		get
		{
			return this.scale_;
		}
		set
		{
			if (this.scale_ != value)
			{
				this.scale_ = value;
				this.scale_ = Mathf.Clamp(this.scale_, 0.1f, 2f);
				this.scale_ = Mathf.Round(this.scale_ * 100f) / 100f;
			}
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x0600061F RID: 1567 RVA: 0x0002C809 File Offset: 0x0002AA09
	public int actualPadding
	{
		get
		{
			if (this.paddingMode == exAtlas.PaddingMode.None)
			{
				return 0;
			}
			if (this.paddingMode == exAtlas.PaddingMode.Custom)
			{
				return this.customPadding;
			}
			if (!this.usePaddingBleed)
			{
				return 1;
			}
			return 2;
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0002C830 File Offset: 0x0002AA30
	public exTextureInfo GetTextureInfoByName(string _name)
	{
		for (int i = 0; i < this.textureInfos.Count; i++)
		{
			exTextureInfo exTextureInfo = this.textureInfos[i];
			if (exTextureInfo.name == _name)
			{
				return exTextureInfo;
			}
		}
		return null;
	}

	// Token: 0x0400046B RID: 1131
	public int width = 512;

	// Token: 0x0400046C RID: 1132
	public int height = 512;

	// Token: 0x0400046D RID: 1133
	public List<exTextureInfo> textureInfos = new List<exTextureInfo>();

	// Token: 0x0400046E RID: 1134
	public List<exBitmapFont> bitmapFonts = new List<exBitmapFont>();

	// Token: 0x0400046F RID: 1135
	public Texture2D texture;

	// Token: 0x04000470 RID: 1136
	public bool customBuildColor;

	// Token: 0x04000471 RID: 1137
	public Color buildColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x04000472 RID: 1138
	public bool useContourBleed = true;

	// Token: 0x04000473 RID: 1139
	public bool usePaddingBleed = true;

	// Token: 0x04000474 RID: 1140
	public bool trimElements = true;

	// Token: 0x04000475 RID: 1141
	[SerializeField]
	public int trimThreshold_ = 1;

	// Token: 0x04000476 RID: 1142
	public bool readable;

	// Token: 0x04000477 RID: 1143
	public Color bgColor = Color.white;

	// Token: 0x04000478 RID: 1144
	public bool showCheckerboard = true;

	// Token: 0x04000479 RID: 1145
	public exAtlas.Algorithm algorithm = exAtlas.Algorithm.MaxRect;

	// Token: 0x0400047A RID: 1146
	public exAtlas.SortBy sortBy;

	// Token: 0x0400047B RID: 1147
	public exAtlas.SortOrder sortOrder;

	// Token: 0x0400047C RID: 1148
	public exAtlas.PaddingMode paddingMode = exAtlas.PaddingMode.Auto;

	// Token: 0x0400047D RID: 1149
	public int customPadding = 1;

	// Token: 0x0400047E RID: 1150
	public bool allowRotate = true;

	// Token: 0x0400047F RID: 1151
	public Color elementBgColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x04000480 RID: 1152
	public Color elementSelectColor = new Color(0f, 0f, 1f, 1f);

	// Token: 0x04000481 RID: 1153
	[SerializeField]
	private float scale_ = 1f;

	// Token: 0x04000482 RID: 1154
	public bool needRebuild;

	// Token: 0x04000483 RID: 1155
	public bool needLayout;

	// Token: 0x020015B5 RID: 5557
	public enum Algorithm
	{
		// Token: 0x04005771 RID: 22385
		Basic,
		// Token: 0x04005772 RID: 22386
		Tree,
		// Token: 0x04005773 RID: 22387
		MaxRect
	}

	// Token: 0x020015B6 RID: 5558
	public enum SortBy
	{
		// Token: 0x04005775 RID: 22389
		UseBest,
		// Token: 0x04005776 RID: 22390
		Width,
		// Token: 0x04005777 RID: 22391
		Height,
		// Token: 0x04005778 RID: 22392
		Area,
		// Token: 0x04005779 RID: 22393
		Name
	}

	// Token: 0x020015B7 RID: 5559
	public enum SortOrder
	{
		// Token: 0x0400577B RID: 22395
		UseBest,
		// Token: 0x0400577C RID: 22396
		Ascending,
		// Token: 0x0400577D RID: 22397
		Descending
	}

	// Token: 0x020015B8 RID: 5560
	public enum PaddingMode
	{
		// Token: 0x0400577F RID: 22399
		None,
		// Token: 0x04005780 RID: 22400
		Auto,
		// Token: 0x04005781 RID: 22401
		Custom
	}
}
