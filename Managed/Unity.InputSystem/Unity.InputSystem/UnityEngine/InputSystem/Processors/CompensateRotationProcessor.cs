using System;
using System.ComponentModel;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000103 RID: 259
	[DesignTimeVisible(false)]
	internal class CompensateRotationProcessor : InputProcessor<Quaternion>
	{
		// Token: 0x06000F2D RID: 3885 RVA: 0x0004A870 File Offset: 0x00048A70
		public override Quaternion Process(Quaternion value, InputControl control)
		{
			if (!InputSystem.settings.compensateForScreenOrientation)
			{
				return value;
			}
			Quaternion identity = Quaternion.identity;
			switch (InputRuntime.s_Instance.screenOrientation)
			{
			case ScreenOrientation.PortraitUpsideDown:
				identity = new Quaternion(0f, 0f, 1f, 0f);
				break;
			case ScreenOrientation.LandscapeLeft:
				identity = new Quaternion(0f, 0f, 0.70710677f, -0.70710677f);
				break;
			case ScreenOrientation.LandscapeRight:
				identity = new Quaternion(0f, 0f, -0.70710677f, -0.70710677f);
				break;
			}
			return value * identity;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0004A90E File Offset: 0x00048B0E
		public override string ToString()
		{
			return "CompensateRotation()";
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0004A915 File Offset: 0x00048B15
		public override InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.EvaluateOnEveryRead;
			}
		}
	}
}
