using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004A2 RID: 1186
	[UsedByNativeCode]
	public struct VertexAttributeDescriptor : IEquatable<VertexAttributeDescriptor>
	{
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x00056558 File Offset: 0x00054758
		// (set) Token: 0x06002D94 RID: 11668 RVA: 0x00056560 File Offset: 0x00054760
		public VertexAttribute attribute { readonly get; set; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x00056569 File Offset: 0x00054769
		// (set) Token: 0x06002D96 RID: 11670 RVA: 0x00056571 File Offset: 0x00054771
		public VertexAttributeFormat format { readonly get; set; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x0005657A File Offset: 0x0005477A
		// (set) Token: 0x06002D98 RID: 11672 RVA: 0x00056582 File Offset: 0x00054782
		public int dimension { readonly get; set; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x0005658B File Offset: 0x0005478B
		// (set) Token: 0x06002D9A RID: 11674 RVA: 0x00056593 File Offset: 0x00054793
		public int stream { readonly get; set; }

		// Token: 0x06002D9B RID: 11675 RVA: 0x0005659C File Offset: 0x0005479C
		public VertexAttributeDescriptor(VertexAttribute attribute = VertexAttribute.Position, VertexAttributeFormat format = VertexAttributeFormat.Float32, int dimension = 3, int stream = 0)
		{
			this.attribute = attribute;
			this.format = format;
			this.dimension = dimension;
			this.stream = stream;
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000565C0 File Offset: 0x000547C0
		public override string ToString()
		{
			return string.Format("(attr={0} fmt={1} dim={2} stream={3})", new object[] { this.attribute, this.format, this.dimension, this.stream });
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x0005661C File Offset: 0x0005481C
		public override int GetHashCode()
		{
			int num = 17;
			num = (int)(num * 23 + this.attribute);
			num = (int)(num * 23 + this.format);
			num = num * 23 + this.dimension;
			return num * 23 + this.stream;
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x00056664 File Offset: 0x00054864
		public override bool Equals(object other)
		{
			bool flag = !(other is VertexAttributeDescriptor);
			return !flag && this.Equals((VertexAttributeDescriptor)other);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x00056698 File Offset: 0x00054898
		public bool Equals(VertexAttributeDescriptor other)
		{
			return this.attribute == other.attribute && this.format == other.format && this.dimension == other.dimension && this.stream == other.stream;
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000566EC File Offset: 0x000548EC
		public static bool operator ==(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x00056708 File Offset: 0x00054908
		public static bool operator !=(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return !lhs.Equals(rhs);
		}
	}
}
