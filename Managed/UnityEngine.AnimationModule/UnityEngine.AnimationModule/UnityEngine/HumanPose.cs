using System;

namespace UnityEngine
{
	// Token: 0x0200003C RID: 60
	public struct HumanPose
	{
		// Token: 0x06000357 RID: 855 RVA: 0x000074B8 File Offset: 0x000056B8
		internal void Init()
		{
			bool flag = this.muscles != null;
			if (flag)
			{
				bool flag2 = this.muscles.Length != HumanTrait.MuscleCount;
				if (flag2)
				{
					throw new InvalidOperationException("Bad array size for HumanPose.muscles. Size must equal HumanTrait.MuscleCount");
				}
			}
			bool flag3 = this.muscles == null;
			if (flag3)
			{
				this.muscles = new float[HumanTrait.MuscleCount];
				bool flag4 = this.bodyRotation.x == 0f && this.bodyRotation.y == 0f && this.bodyRotation.z == 0f && this.bodyRotation.w == 0f;
				if (flag4)
				{
					this.bodyRotation.w = 1f;
				}
			}
		}

		// Token: 0x04000144 RID: 324
		public Vector3 bodyPosition;

		// Token: 0x04000145 RID: 325
		public Quaternion bodyRotation;

		// Token: 0x04000146 RID: 326
		public float[] muscles;
	}
}
