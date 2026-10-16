using System;
using System.Diagnostics.CodeAnalysis;

namespace UnityEngine.Rendering
{
	// Token: 0x02000541 RID: 1345
	public class ObjectIdResult
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x00064B61 File Offset: 0x00062D61
		public Object[] idToObjectMapping
		{
			[return: NotNull]
			get;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x00064B69 File Offset: 0x00062D69
		internal ObjectIdResult(Object[] idToObjectMapping)
		{
			this.idToObjectMapping = idToObjectMapping;
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x00064B7C File Offset: 0x00062D7C
		public static int DecodeIdFromColor(Color color)
		{
			return (int)(color.r * 255f) + ((int)(color.g * 255f) << 8) + ((int)(color.b * 255f) << 16) + ((int)(color.a * 255f) << 24);
		}
	}
}
