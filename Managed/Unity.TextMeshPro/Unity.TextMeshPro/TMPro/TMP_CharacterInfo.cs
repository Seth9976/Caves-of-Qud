using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000019 RID: 25
	[DebuggerDisplay("Unicode '{character}'  ({((uint)character).ToString(\"X\")})")]
	public struct TMP_CharacterInfo
	{
		// Token: 0x04000040 RID: 64
		public TMP_TextElementType elementType;

		// Token: 0x04000041 RID: 65
		public char character;

		// Token: 0x04000042 RID: 66
		public int index;

		// Token: 0x04000043 RID: 67
		public int stringLength;

		// Token: 0x04000044 RID: 68
		public TMP_TextElement textElement;

		// Token: 0x04000045 RID: 69
		public Glyph alternativeGlyph;

		// Token: 0x04000046 RID: 70
		public TMP_FontAsset fontAsset;

		// Token: 0x04000047 RID: 71
		public Material material;

		// Token: 0x04000048 RID: 72
		public int materialReferenceIndex;

		// Token: 0x04000049 RID: 73
		public bool isUsingAlternateTypeface;

		// Token: 0x0400004A RID: 74
		public float pointSize;

		// Token: 0x0400004B RID: 75
		public int lineNumber;

		// Token: 0x0400004C RID: 76
		public int pageNumber;

		// Token: 0x0400004D RID: 77
		public int vertexIndex;

		// Token: 0x0400004E RID: 78
		public TMP_Vertex vertex_BL;

		// Token: 0x0400004F RID: 79
		public TMP_Vertex vertex_TL;

		// Token: 0x04000050 RID: 80
		public TMP_Vertex vertex_TR;

		// Token: 0x04000051 RID: 81
		public TMP_Vertex vertex_BR;

		// Token: 0x04000052 RID: 82
		public Vector3 topLeft;

		// Token: 0x04000053 RID: 83
		public Vector3 bottomLeft;

		// Token: 0x04000054 RID: 84
		public Vector3 topRight;

		// Token: 0x04000055 RID: 85
		public Vector3 bottomRight;

		// Token: 0x04000056 RID: 86
		public float origin;

		// Token: 0x04000057 RID: 87
		public float xAdvance;

		// Token: 0x04000058 RID: 88
		public float ascender;

		// Token: 0x04000059 RID: 89
		public float baseLine;

		// Token: 0x0400005A RID: 90
		public float descender;

		// Token: 0x0400005B RID: 91
		internal float adjustedAscender;

		// Token: 0x0400005C RID: 92
		internal float adjustedDescender;

		// Token: 0x0400005D RID: 93
		internal float adjustedHorizontalAdvance;

		// Token: 0x0400005E RID: 94
		public float aspectRatio;

		// Token: 0x0400005F RID: 95
		public float scale;

		// Token: 0x04000060 RID: 96
		public Color32 color;

		// Token: 0x04000061 RID: 97
		public Color32 underlineColor;

		// Token: 0x04000062 RID: 98
		public int underlineVertexIndex;

		// Token: 0x04000063 RID: 99
		public Color32 strikethroughColor;

		// Token: 0x04000064 RID: 100
		public int strikethroughVertexIndex;

		// Token: 0x04000065 RID: 101
		public Color32 highlightColor;

		// Token: 0x04000066 RID: 102
		public HighlightState highlightState;

		// Token: 0x04000067 RID: 103
		public FontStyles style;

		// Token: 0x04000068 RID: 104
		public bool isVisible;
	}
}
