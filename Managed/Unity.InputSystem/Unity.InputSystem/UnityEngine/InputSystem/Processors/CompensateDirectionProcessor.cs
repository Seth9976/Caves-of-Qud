using System;
using System.ComponentModel;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Processors
{
	// Token: 0x02000102 RID: 258
	[DesignTimeVisible(false)]
	internal class CompensateDirectionProcessor : InputProcessor<Vector3>
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x0004A7D0 File Offset: 0x000489D0
		public override Vector3 Process(Vector3 value, InputControl control)
		{
			if (!InputSystem.settings.compensateForScreenOrientation)
			{
				return value;
			}
			Quaternion quaternion = Quaternion.identity;
			switch (InputRuntime.s_Instance.screenOrientation)
			{
			case ScreenOrientation.PortraitUpsideDown:
				quaternion = Quaternion.Euler(0f, 0f, 180f);
				break;
			case ScreenOrientation.LandscapeLeft:
				quaternion = Quaternion.Euler(0f, 0f, 90f);
				break;
			case ScreenOrientation.LandscapeRight:
				quaternion = Quaternion.Euler(0f, 0f, 270f);
				break;
			}
			return quaternion * value;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0004A85C File Offset: 0x00048A5C
		public override string ToString()
		{
			return "CompensateDirection()";
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0004A863 File Offset: 0x00048A63
		public override InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.EvaluateOnEveryRead;
			}
		}
	}
}
