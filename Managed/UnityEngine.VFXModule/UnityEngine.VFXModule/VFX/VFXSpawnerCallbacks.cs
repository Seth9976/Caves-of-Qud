using System;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000019 RID: 25
	[RequiredByNativeCode]
	[Serializable]
	public abstract class VFXSpawnerCallbacks : ScriptableObject
	{
		// Token: 0x060000AF RID: 175
		public abstract void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		// Token: 0x060000B0 RID: 176
		public abstract void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		// Token: 0x060000B1 RID: 177
		public abstract void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);
	}
}
