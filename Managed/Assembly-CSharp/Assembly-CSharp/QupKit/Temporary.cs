using System;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003AA RID: 938
	public class Temporary : MonoBehaviour
	{
		// Token: 0x06002307 RID: 8967 RVA: 0x000B20B6 File Offset: 0x000B02B6
		public void LateUpdate()
		{
			this.Delay -= Time.deltaTime;
			if (this.Delay <= 0f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x040011A8 RID: 4520
		public float Delay;
	}
}
