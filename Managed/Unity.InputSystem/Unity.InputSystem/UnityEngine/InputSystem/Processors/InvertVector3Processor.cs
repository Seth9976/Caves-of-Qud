using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000106 RID: 262
	public class InvertVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x0004A9A4 File Offset: 0x00048BA4
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			if (this.invertX)
			{
				value.x *= -1f;
			}
			if (this.invertY)
			{
				value.y *= -1f;
			}
			if (this.invertZ)
			{
				value.z *= -1f;
			}
			return value;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0004A9FA File Offset: 0x00048BFA
		public override string ToString()
		{
			return string.Format("InvertVector3(invertX={0},invertY={1},invertZ={2})", this.invertX, this.invertY, this.invertZ);
		}

		// Token: 0x0400065A RID: 1626
		public bool invertX = true;

		// Token: 0x0400065B RID: 1627
		public bool invertY = true;

		// Token: 0x0400065C RID: 1628
		public bool invertZ = true;
	}
}
