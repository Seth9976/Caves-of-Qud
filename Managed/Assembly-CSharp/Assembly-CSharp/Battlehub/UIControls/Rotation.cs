using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x020003BF RID: 959
	public class Rotation : MonoBehaviour
	{
		// Token: 0x0600238B RID: 9099 RVA: 0x000B471D File Offset: 0x000B291D
		private void Start()
		{
			this.m_rand = global::UnityEngine.Random.onUnitSphere;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000B472C File Offset: 0x000B292C
		private void Update()
		{
			if (Time.time - this.m_prevT > 10f)
			{
				this.m_rand = global::UnityEngine.Random.onUnitSphere;
				this.m_prevT = Time.time;
			}
			base.transform.rotation *= Quaternion.AngleAxis(12.566371f * Time.deltaTime, this.m_rand);
		}

		// Token: 0x040011E2 RID: 4578
		private Vector3 m_rand;

		// Token: 0x040011E3 RID: 4579
		private float m_prevT;
	}
}
