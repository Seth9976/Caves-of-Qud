using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

namespace System.Net.Cache
{
	// Token: 0x02000786 RID: 1926
	internal class RequestCacheEntry
	{
		// Token: 0x06003CA5 RID: 15525 RVA: 0x000CE64C File Offset: 0x000CC84C
		internal RequestCacheEntry()
		{
			this.m_ExpiresUtc = (this.m_LastAccessedUtc = (this.m_LastModifiedUtc = (this.m_LastSynchronizedUtc = DateTime.MinValue)));
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x000CE685 File Offset: 0x000CC885
		// (set) Token: 0x06003CA7 RID: 15527 RVA: 0x000CE68D File Offset: 0x000CC88D
		internal bool IsPrivateEntry
		{
			get
			{
				return this.m_IsPrivateEntry;
			}
			set
			{
				this.m_IsPrivateEntry = value;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06003CA8 RID: 15528 RVA: 0x000CE696 File Offset: 0x000CC896
		// (set) Token: 0x06003CA9 RID: 15529 RVA: 0x000CE69E File Offset: 0x000CC89E
		internal long StreamSize
		{
			get
			{
				return this.m_StreamSize;
			}
			set
			{
				this.m_StreamSize = value;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06003CAA RID: 15530 RVA: 0x000CE6A7 File Offset: 0x000CC8A7
		// (set) Token: 0x06003CAB RID: 15531 RVA: 0x000CE6AF File Offset: 0x000CC8AF
		internal DateTime ExpiresUtc
		{
			get
			{
				return this.m_ExpiresUtc;
			}
			set
			{
				this.m_ExpiresUtc = value;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x000CE6B8 File Offset: 0x000CC8B8
		// (set) Token: 0x06003CAD RID: 15533 RVA: 0x000CE6C0 File Offset: 0x000CC8C0
		internal DateTime LastAccessedUtc
		{
			get
			{
				return this.m_LastAccessedUtc;
			}
			set
			{
				this.m_LastAccessedUtc = value;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06003CAE RID: 15534 RVA: 0x000CE6C9 File Offset: 0x000CC8C9
		// (set) Token: 0x06003CAF RID: 15535 RVA: 0x000CE6D1 File Offset: 0x000CC8D1
		internal DateTime LastModifiedUtc
		{
			get
			{
				return this.m_LastModifiedUtc;
			}
			set
			{
				this.m_LastModifiedUtc = value;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06003CB0 RID: 15536 RVA: 0x000CE6DA File Offset: 0x000CC8DA
		// (set) Token: 0x06003CB1 RID: 15537 RVA: 0x000CE6E2 File Offset: 0x000CC8E2
		internal DateTime LastSynchronizedUtc
		{
			get
			{
				return this.m_LastSynchronizedUtc;
			}
			set
			{
				this.m_LastSynchronizedUtc = value;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x000CE6EB File Offset: 0x000CC8EB
		// (set) Token: 0x06003CB3 RID: 15539 RVA: 0x000CE6F3 File Offset: 0x000CC8F3
		internal TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
			set
			{
				this.m_MaxStale = value;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x000CE6FC File Offset: 0x000CC8FC
		// (set) Token: 0x06003CB5 RID: 15541 RVA: 0x000CE704 File Offset: 0x000CC904
		internal int HitCount
		{
			get
			{
				return this.m_HitCount;
			}
			set
			{
				this.m_HitCount = value;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x000CE70D File Offset: 0x000CC90D
		// (set) Token: 0x06003CB7 RID: 15543 RVA: 0x000CE715 File Offset: 0x000CC915
		internal int UsageCount
		{
			get
			{
				return this.m_UsageCount;
			}
			set
			{
				this.m_UsageCount = value;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x000CE71E File Offset: 0x000CC91E
		// (set) Token: 0x06003CB9 RID: 15545 RVA: 0x000CE726 File Offset: 0x000CC926
		internal bool IsPartialEntry
		{
			get
			{
				return this.m_IsPartialEntry;
			}
			set
			{
				this.m_IsPartialEntry = value;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x000CE72F File Offset: 0x000CC92F
		// (set) Token: 0x06003CBB RID: 15547 RVA: 0x000CE737 File Offset: 0x000CC937
		internal StringCollection EntryMetadata
		{
			get
			{
				return this.m_EntryMetadata;
			}
			set
			{
				this.m_EntryMetadata = value;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x000CE740 File Offset: 0x000CC940
		// (set) Token: 0x06003CBD RID: 15549 RVA: 0x000CE748 File Offset: 0x000CC948
		internal StringCollection SystemMetadata
		{
			get
			{
				return this.m_SystemMetadata;
			}
			set
			{
				this.m_SystemMetadata = value;
			}
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x000CE754 File Offset: 0x000CC954
		internal virtual string ToString(bool verbose)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("\r\nIsPrivateEntry   = ").Append(this.IsPrivateEntry);
			stringBuilder.Append("\r\nIsPartialEntry   = ").Append(this.IsPartialEntry);
			stringBuilder.Append("\r\nStreamSize       = ").Append(this.StreamSize);
			stringBuilder.Append("\r\nExpires          = ").Append((this.ExpiresUtc == DateTime.MinValue) ? "" : this.ExpiresUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastAccessed     = ").Append((this.LastAccessedUtc == DateTime.MinValue) ? "" : this.LastAccessedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastModified     = ").Append((this.LastModifiedUtc == DateTime.MinValue) ? "" : this.LastModifiedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastSynchronized = ").Append((this.LastSynchronizedUtc == DateTime.MinValue) ? "" : this.LastSynchronizedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nMaxStale(sec)    = ").Append((this.MaxStale == TimeSpan.MinValue) ? "" : ((int)this.MaxStale.TotalSeconds).ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nHitCount         = ").Append(this.HitCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nUsageCount       = ").Append(this.UsageCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\n");
			if (verbose)
			{
				stringBuilder.Append("EntryMetadata:\r\n");
				if (this.m_EntryMetadata != null)
				{
					foreach (string text in this.m_EntryMetadata)
					{
						stringBuilder.Append(text).Append("\r\n");
					}
				}
				stringBuilder.Append("---\r\nSystemMetadata:\r\n");
				if (this.m_SystemMetadata != null)
				{
					foreach (string text2 in this.m_SystemMetadata)
					{
						stringBuilder.Append(text2).Append("\r\n");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040023F7 RID: 9207
		private bool m_IsPrivateEntry;

		// Token: 0x040023F8 RID: 9208
		private long m_StreamSize;

		// Token: 0x040023F9 RID: 9209
		private DateTime m_ExpiresUtc;

		// Token: 0x040023FA RID: 9210
		private int m_HitCount;

		// Token: 0x040023FB RID: 9211
		private DateTime m_LastAccessedUtc;

		// Token: 0x040023FC RID: 9212
		private DateTime m_LastModifiedUtc;

		// Token: 0x040023FD RID: 9213
		private DateTime m_LastSynchronizedUtc;

		// Token: 0x040023FE RID: 9214
		private TimeSpan m_MaxStale;

		// Token: 0x040023FF RID: 9215
		private int m_UsageCount;

		// Token: 0x04002400 RID: 9216
		private bool m_IsPartialEntry;

		// Token: 0x04002401 RID: 9217
		private StringCollection m_EntryMetadata;

		// Token: 0x04002402 RID: 9218
		private StringCollection m_SystemMetadata;
	}
}
