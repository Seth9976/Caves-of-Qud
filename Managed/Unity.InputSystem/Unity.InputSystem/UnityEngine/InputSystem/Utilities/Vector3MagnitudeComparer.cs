using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000130 RID: 304
	public struct Vector3MagnitudeComparer : IComparer<Vector3>
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x00051E94 File Offset: 0x00050094
		public int Compare(Vector3 x, Vector3 y)
		{
			float sqrMagnitude = x.sqrMagnitude;
			float sqrMagnitude2 = y.sqrMagnitude;
			if (sqrMagnitude < sqrMagnitude2)
			{
				return -1;
			}
			if (sqrMagnitude > sqrMagnitude2)
			{
				return 1;
			}
			return 0;
		}
	}
}
