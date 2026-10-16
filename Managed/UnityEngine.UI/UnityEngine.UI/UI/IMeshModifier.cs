using System;

namespace UnityEngine.UI
{
	// Token: 0x02000042 RID: 66
	public interface IMeshModifier
	{
		// Token: 0x060004B3 RID: 1203
		[Obsolete("use IMeshModifier.ModifyMesh (VertexHelper verts) instead", false)]
		void ModifyMesh(Mesh mesh);

		// Token: 0x060004B4 RID: 1204
		void ModifyMesh(VertexHelper verts);
	}
}
