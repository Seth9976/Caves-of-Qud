using System;

namespace Genkit.SimplexNoise
{
	// Token: 0x0200039A RID: 922
	public class SamplePoint
	{
		// Token: 0x060022BD RID: 8893 RVA: 0x000B0A5C File Offset: 0x000AEC5C
		public SamplePoint(float _x = 0f, float _y = 0f, float _z = 0f)
		{
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}

		// Token: 0x04001184 RID: 4484
		public float x;

		// Token: 0x04001185 RID: 4485
		public float y;

		// Token: 0x04001186 RID: 4486
		public float z;
	}
}
