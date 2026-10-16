using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
[ExecuteInEditMode]
public class MotedAirImposter : MonoBehaviour
{
	// Token: 0x06000290 RID: 656 RVA: 0x0000DAFF File Offset: 0x0000BCFF
	private void Awake()
	{
		MotedAirGlobal.wanted++;
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0000DB0D File Offset: 0x0000BD0D
	private void LateUpdate()
	{
		MotedAirGlobal.wanted++;
	}
}
