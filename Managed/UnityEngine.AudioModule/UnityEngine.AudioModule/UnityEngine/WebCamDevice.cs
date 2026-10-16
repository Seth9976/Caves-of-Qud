using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000024 RID: 36
	[UsedByNativeCode]
	public struct WebCamDevice
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00004D68 File Offset: 0x00002F68
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00004D80 File Offset: 0x00002F80
		public bool isFrontFacing
		{
			get
			{
				return (this.m_Flags & 1) != 0;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public WebCamKind kind
		{
			get
			{
				return this.m_Kind;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public string depthCameraName
		{
			get
			{
				return (this.m_DepthCameraName == "") ? null : this.m_DepthCameraName;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public bool isAutoFocusPointSupported
		{
			get
			{
				return (this.m_Flags & 2) != 0;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00004E08 File Offset: 0x00003008
		public Resolution[] availableResolutions
		{
			get
			{
				return this.m_Resolutions;
			}
		}

		// Token: 0x0400006E RID: 110
		[NativeName("name")]
		internal string m_Name;

		// Token: 0x0400006F RID: 111
		[NativeName("depthCameraName")]
		internal string m_DepthCameraName;

		// Token: 0x04000070 RID: 112
		[NativeName("flags")]
		internal int m_Flags;

		// Token: 0x04000071 RID: 113
		[NativeName("kind")]
		internal WebCamKind m_Kind;

		// Token: 0x04000072 RID: 114
		[NativeName("resolutions")]
		internal Resolution[] m_Resolutions;
	}
}
