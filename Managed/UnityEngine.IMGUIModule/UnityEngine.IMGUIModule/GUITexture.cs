using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class GUITexture
	{
		// Token: 0x060003DA RID: 986 RVA: 0x0000DA5D File Offset: 0x0000BC5D
		private static void FeatureRemoved()
		{
			throw new Exception("GUITexture has been removed from Unity. Use UI.Image instead.");
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000DA6C File Offset: 0x0000BC6C
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000DA98 File Offset: 0x0000BC98
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Color color
		{
			get
			{
				GUITexture.FeatureRemoved();
				return new Color(0f, 0f, 0f);
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000DA98 File Offset: 0x0000BC98
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Texture texture
		{
			get
			{
				GUITexture.FeatureRemoved();
				return null;
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000DA98 File Offset: 0x0000BC98
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public Rect pixelInset
		{
			get
			{
				GUITexture.FeatureRemoved();
				return default(Rect);
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000DA98 File Offset: 0x0000BC98
		[Obsolete("GUITexture has been removed. Use UI.Image instead.", true)]
		public RectOffset border
		{
			get
			{
				GUITexture.FeatureRemoved();
				return null;
			}
			set
			{
				GUITexture.FeatureRemoved();
			}
		}
	}
}
