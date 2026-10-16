using System;

// Token: 0x020000B3 RID: 179
[Flags]
public enum exUpdateFlags
{
	// Token: 0x040004B6 RID: 1206
	None = 0,
	// Token: 0x040004B7 RID: 1207
	Index = 1,
	// Token: 0x040004B8 RID: 1208
	Vertex = 2,
	// Token: 0x040004B9 RID: 1209
	UV = 4,
	// Token: 0x040004BA RID: 1210
	Color = 8,
	// Token: 0x040004BB RID: 1211
	Normal = 16,
	// Token: 0x040004BC RID: 1212
	Text = 32,
	// Token: 0x040004BD RID: 1213
	Transparent = 64,
	// Token: 0x040004BE RID: 1214
	VertexAndIndex = 3,
	// Token: 0x040004BF RID: 1215
	AllExcludeIndex = 126,
	// Token: 0x040004C0 RID: 1216
	All = 127
}
