using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005FA RID: 1530
	internal class Result : CapabilitiesResult
	{
		// Token: 0x06004259 RID: 16985 RVA: 0x000AD084 File Offset: 0x000AB284
		internal Result(IDictionary items)
			: base(items)
		{
			this.AdapterTypeMap = new Dictionary<Type, Type>();
			this.Track = new StringCollection();
			this.MarkupTextWriter = typeof(HtmlTextWriter);
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x000AD0B3 File Offset: 0x000AB2B3
		internal void AddTrack(string track)
		{
			this.Track.Add(track);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x000AD0C2 File Offset: 0x000AB2C2
		internal void AddAdapter(Type controlType, Type adapterType)
		{
			this.AdapterTypeMap[controlType] = adapterType;
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x0600425C RID: 16988 RVA: 0x000AD0D1 File Offset: 0x000AB2D1
		public StringCollection Tracks
		{
			get
			{
				return this.Track;
			}
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000AD0D9 File Offset: 0x000AB2D9
		internal override Type GetTagWriter()
		{
			return this.MarkupTextWriter;
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x000AD0E1 File Offset: 0x000AB2E1
		internal override IDictionary GetAdapters()
		{
			return this.AdapterTypeMap;
		}

		// Token: 0x0400239C RID: 9116
		private Dictionary<Type, Type> AdapterTypeMap;

		// Token: 0x0400239D RID: 9117
		private StringCollection Track;

		// Token: 0x0400239E RID: 9118
		internal Type MarkupTextWriter;
	}
}
