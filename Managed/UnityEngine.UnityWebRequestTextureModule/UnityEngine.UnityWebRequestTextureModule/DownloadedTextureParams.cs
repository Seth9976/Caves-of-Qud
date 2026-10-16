using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000003 RID: 3
	public struct DownloadedTextureParams
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static DownloadedTextureParams Default
		{
			get
			{
				return new DownloadedTextureParams
				{
					flags = (DownloadedTextureFlags.Readable | DownloadedTextureFlags.MipmapChain),
					mipmapCount = -1
				};
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002076 File Offset: 0x00000276
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000208E File Offset: 0x0000028E
		public bool readable
		{
			get
			{
				return this.flags.HasFlag(DownloadedTextureFlags.Readable);
			}
			set
			{
				this.SetFlags(DownloadedTextureFlags.Readable, value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002099 File Offset: 0x00000299
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020B1 File Offset: 0x000002B1
		public bool mipmapChain
		{
			get
			{
				return this.flags.HasFlag(DownloadedTextureFlags.MipmapChain);
			}
			set
			{
				this.SetFlags(DownloadedTextureFlags.MipmapChain, value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020BC File Offset: 0x000002BC
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020D4 File Offset: 0x000002D4
		public bool linearColorSpace
		{
			get
			{
				return this.flags.HasFlag(DownloadedTextureFlags.LinearColorSpace);
			}
			set
			{
				this.SetFlags(DownloadedTextureFlags.LinearColorSpace, value);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E0 File Offset: 0x000002E0
		private void SetFlags(DownloadedTextureFlags flgs, bool add)
		{
			if (add)
			{
				this.flags |= flgs;
			}
			else
			{
				this.flags &= ~flgs;
			}
		}

		// Token: 0x04000006 RID: 6
		public DownloadedTextureFlags flags;

		// Token: 0x04000007 RID: 7
		public int mipmapCount;
	}
}
