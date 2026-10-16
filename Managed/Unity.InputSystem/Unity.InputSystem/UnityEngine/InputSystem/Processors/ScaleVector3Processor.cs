using System;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x0200010C RID: 268
	public class ScaleVector3Processor : InputProcessor<Vector3>
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x0004ABD4 File Offset: 0x00048DD4
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			return new Vector3(value.x * this.x, value.y * this.y, value.z * this.z);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0004AC02 File Offset: 0x00048E02
		public override string ToString()
		{
			return string.Format("ScaleVector3(x={0},y={1},z={2})", this.x, this.y, this.z);
		}

		// Token: 0x04000663 RID: 1635
		[Tooltip("Scale factor to multiply the incoming Vector3's X component by.")]
		public float x = 1f;

		// Token: 0x04000664 RID: 1636
		[Tooltip("Scale factor to multiply the incoming Vector3's Y component by.")]
		public float y = 1f;

		// Token: 0x04000665 RID: 1637
		[Tooltip("Scale factor to multiply the incoming Vector3's Z component by.")]
		public float z = 1f;
	}
}
