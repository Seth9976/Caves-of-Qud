using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public interface exISpriteBase : IMonoBehaviour
{
	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x06000641 RID: 1601
	// (set) Token: 0x06000642 RID: 1602
	bool customSize { get; set; }

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x06000643 RID: 1603
	// (set) Token: 0x06000644 RID: 1604
	float width { get; set; }

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000645 RID: 1605
	// (set) Token: 0x06000646 RID: 1606
	float height { get; set; }

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000647 RID: 1607
	// (set) Token: 0x06000648 RID: 1608
	Anchor anchor { get; set; }

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000649 RID: 1609
	// (set) Token: 0x0600064A RID: 1610
	Color color { get; set; }

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x0600064B RID: 1611
	// (set) Token: 0x0600064C RID: 1612
	Vector2 offset { get; set; }

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x0600064D RID: 1613
	// (set) Token: 0x0600064E RID: 1614
	Vector2 shear { get; set; }

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x0600064F RID: 1615
	// (set) Token: 0x06000650 RID: 1616
	Shader shader { get; set; }

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000651 RID: 1617
	// (set) Token: 0x06000652 RID: 1618
	exUpdateFlags updateFlags { get; set; }

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000653 RID: 1619
	int vertexCount { get; }

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000654 RID: 1620
	int indexCount { get; }

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000655 RID: 1621
	Material material { get; }

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000656 RID: 1622
	bool visible { get; }

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000657 RID: 1623
	Matrix4x4 cachedWorldMatrix { get; }

	// Token: 0x06000658 RID: 1624
	void UpdateMaterial();

	// Token: 0x06000659 RID: 1625
	float GetScaleX(Space _space);

	// Token: 0x0600065A RID: 1626
	float GetScaleY(Space _space);
}
