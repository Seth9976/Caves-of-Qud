using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000101 RID: 257
	public class ClampProcessor : InputProcessor<float>
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x0004A78F File Offset: 0x0004898F
		public override float Process(float value, InputControl control)
		{
			return Mathf.Clamp(value, this.min, this.max);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0004A7A3 File Offset: 0x000489A3
		public override string ToString()
		{
			return string.Format("Clamp(min={0},max={1})", this.min, this.max);
		}

		// Token: 0x04000656 RID: 1622
		public float min;

		// Token: 0x04000657 RID: 1623
		public float max;
	}
}
