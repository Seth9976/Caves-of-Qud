using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class TileBehavior : MonoBehaviour
{
	// Token: 0x0600052B RID: 1323 RVA: 0x00021A43 File Offset: 0x0001FC43
	public void SetPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x040003FC RID: 1020
	public int x;

	// Token: 0x040003FD RID: 1021
	public int y;
}
