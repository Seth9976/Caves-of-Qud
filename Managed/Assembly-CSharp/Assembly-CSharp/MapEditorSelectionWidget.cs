using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
[ExecuteAlways]
public class MapEditorSelectionWidget : MonoBehaviour
{
	// Token: 0x060004FC RID: 1276 RVA: 0x0002101C File Offset: 0x0001F21C
	private void Update()
	{
		this.t += Time.deltaTime / this.duration;
		this.Sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(this.Color1, this.Color2, Math.Abs(1f - this.t % 2f));
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0002107C File Offset: 0x0001F27C
	public void SetTileSize(int x, int y)
	{
		Vector2 vector = this.tileSize * new Vector2((float)x, (float)y) + this.borderPadding;
		this.Sprite.GetComponent<SpriteRenderer>().size = vector;
		Vector2 vector2 = new Vector2(1f, -1f);
		this.Sprite.transform.localPosition = (vector - this.tileSize) / 2f * vector2 + this.borderPadding * new Vector2(-1f, 1f) / 2f;
	}

	// Token: 0x040003D9 RID: 985
	public GameObject Sprite;

	// Token: 0x040003DA RID: 986
	public float duration = 3f;

	// Token: 0x040003DB RID: 987
	private float t;

	// Token: 0x040003DC RID: 988
	private Vector2 tileSize = new Vector2(0.16f, 0.24f);

	// Token: 0x040003DD RID: 989
	private Vector2 borderPadding = new Vector2(0.04f, 0.04f);

	// Token: 0x040003DE RID: 990
	public Color Color1 = Color.green;

	// Token: 0x040003DF RID: 991
	public Color Color2 = new Color(0f, 1f, 0f, 0.5f);
}
