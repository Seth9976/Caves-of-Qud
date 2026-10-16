using System;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003AD RID: 941
	public static class Vector2Extensions
	{
		// Token: 0x06002316 RID: 8982 RVA: 0x000B24F4 File Offset: 0x000B06F4
		public static Vector2i ToIntVector2(this Vector2 vector2)
		{
			int[] array = new int[2];
			for (int i = 0; i < 2; i++)
			{
				array[i] = Mathf.RoundToInt(vector2[i]);
			}
			return new Vector2i(array);
		}
	}
}
