using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x02000011 RID: 17
	[MovedFrom("UnityEngine")]
	[UsedByNativeCode]
	public struct NavMeshTriangulation
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00003BBA File Offset: 0x00001DBA
		[Obsolete("Use areas instead.")]
		public int[] layers
		{
			get
			{
				return this.areas;
			}
		}

		// Token: 0x0400002D RID: 45
		public Vector3[] vertices;

		// Token: 0x0400002E RID: 46
		public int[] indices;

		// Token: 0x0400002F RID: 47
		public int[] areas;
	}
}
