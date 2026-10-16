using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200064F RID: 1615
	internal interface IMeshGenerator
	{
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002F3D RID: 12093
		// (set) Token: 0x06002F3E RID: 12094
		VisualElement currentElement { get; set; }

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002F3F RID: 12095
		// (set) Token: 0x06002F40 RID: 12096
		TextJobSystem textJobSystem { get; set; }

		// Token: 0x06002F41 RID: 12097
		void DrawText(List<NativeSlice<Vertex>> vertices, List<NativeSlice<ushort>> indices, List<Texture2D> atlases, List<GlyphRenderMode> renderModes, List<float> sdfScales);

		// Token: 0x06002F42 RID: 12098
		void DrawText(List<NativeSlice<Vertex>> vertices, List<NativeSlice<ushort>> indices, List<Material> materials, List<GlyphRenderMode> renderModes);

		// Token: 0x06002F43 RID: 12099
		void DrawText(string text, Vector2 pos, float fontSize, Color color, FontAsset font);

		// Token: 0x06002F44 RID: 12100
		void DrawRectangle(MeshGenerator.RectangleParams rectParams);

		// Token: 0x06002F45 RID: 12101
		void DrawBorder(MeshGenerator.BorderParams borderParams);

		// Token: 0x06002F46 RID: 12102
		void DrawVectorImage(VectorImage vectorImage, Vector2 offset, Angle rotationAngle, Vector2 scale);

		// Token: 0x06002F47 RID: 12103
		void DrawRectangleRepeat(MeshGenerator.RectangleParams rectParams, Rect totalRect, float scaledPixelsPerPoint);

		// Token: 0x06002F48 RID: 12104
		void ScheduleJobs(MeshGenerationContext mgc);
	}
}
