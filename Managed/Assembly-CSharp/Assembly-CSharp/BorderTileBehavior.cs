using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class BorderTileBehavior : MonoBehaviour
{
	// Token: 0x0600052D RID: 1325 RVA: 0x00021A5B File Offset: 0x0001FC5B
	public void SetDirection(string d, int x, int y)
	{
		this.x = x;
		this.y = y;
		this.Direction = d;
	}

	// Token: 0x040003FE RID: 1022
	public int x;

	// Token: 0x040003FF RID: 1023
	public int y;

	// Token: 0x04000400 RID: 1024
	public string Direction;
}
