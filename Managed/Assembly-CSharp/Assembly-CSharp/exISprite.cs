using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public interface exISprite : exISpriteBase, IMonoBehaviour
{
	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x0600065B RID: 1627
	// (set) Token: 0x0600065C RID: 1628
	exTextureInfo textureInfo { get; set; }

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x0600065D RID: 1629
	// (set) Token: 0x0600065E RID: 1630
	bool useTextureOffset { get; set; }

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x0600065F RID: 1631
	// (set) Token: 0x06000660 RID: 1632
	exSpriteType spriteType { get; set; }

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000661 RID: 1633
	// (set) Token: 0x06000662 RID: 1634
	Vector2 tiledSpacing { get; set; }

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000663 RID: 1635
	// (set) Token: 0x06000664 RID: 1636
	bool borderOnly { get; set; }

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000665 RID: 1637
	// (set) Token: 0x06000666 RID: 1638
	bool customBorderSize { get; set; }

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000667 RID: 1639
	// (set) Token: 0x06000668 RID: 1640
	float leftBorderSize { get; set; }

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000669 RID: 1641
	// (set) Token: 0x0600066A RID: 1642
	float rightBorderSize { get; set; }

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x0600066B RID: 1643
	// (set) Token: 0x0600066C RID: 1644
	float topBorderSize { get; set; }

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x0600066D RID: 1645
	// (set) Token: 0x0600066E RID: 1646
	float bottomBorderSize { get; set; }

	// Token: 0x0600066F RID: 1647
	void UpdateBufferSize();
}
