using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012F RID: 303
	public struct Vector2MagnitudeComparer : IComparer<Vector2>
	{
		// Token: 0x06001119 RID: 4377 RVA: 0x00051E68 File Offset: 0x00050068
		public int Compare(Vector2 x, Vector2 y)
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
