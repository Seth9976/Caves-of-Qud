using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
public class LTUtility
{
	// Token: 0x060009EB RID: 2539 RVA: 0x0003E898 File Offset: 0x0003CA98
	public static Vector3[] reverse(Vector3[] arr)
	{
		int num = arr.Length - 1;
		for (int i = 0; i <= arr.Length / 2; i++)
		{
			Vector3 vector = arr[i];
			arr[i] = arr[num];
			arr[num] = vector;
			num--;
		}
		return arr;
	}
}
