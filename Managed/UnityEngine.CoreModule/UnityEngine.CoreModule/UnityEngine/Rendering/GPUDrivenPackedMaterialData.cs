using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000521 RID: 1313
	[UsedByNativeCode]
	internal struct GPUDrivenPackedMaterialData
	{
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x00061CDE File Offset: 0x0005FEDE
		public bool isTransparent
		{
			get
			{
				return (this.data & 1U) > 0U;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x00061CEB File Offset: 0x0005FEEB
		public bool isMotionVectorsPassEnabled
		{
			get
			{
				return (this.data & 2U) > 0U;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x00061CF8 File Offset: 0x0005FEF8
		public bool hasTessellation
		{
			get
			{
				return (this.data & 4U) > 0U;
			}
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x00061D05 File Offset: 0x0005FF05
		public GPUDrivenPackedMaterialData()
		{
			this.data = 0U;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x00061D0F File Offset: 0x0005FF0F
		public GPUDrivenPackedMaterialData(bool isTransparent, bool isMotionVectorsPassEnabled, bool hasTessellation)
		{
			this.data = (isTransparent ? 1U : 0U);
			this.data |= (isMotionVectorsPassEnabled ? 2U : 0U);
			this.data |= (hasTessellation ? 4U : 0U);
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x00061D48 File Offset: 0x0005FF48
		public bool Equals(GPUDrivenPackedMaterialData other)
		{
			return (other.data & 7U) == (this.data & 7U);
		}

		// Token: 0x04001061 RID: 4193
		private uint data;
	}
}
