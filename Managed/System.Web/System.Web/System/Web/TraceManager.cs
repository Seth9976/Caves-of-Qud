using System;
using System.Web.Configuration;

namespace System.Web
{
	// Token: 0x020000E3 RID: 227
	internal sealed class TraceManager
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x00020570 File Offset: 0x0001E770
		public TraceManager()
		{
			try
			{
				this.mode = TraceMode.SortByTime;
				TraceSection traceSection = WebConfigurationManager.GetWebApplicationSection("system.web/trace") as TraceSection;
				if (traceSection == null)
				{
					traceSection = new TraceSection();
				}
				if (traceSection != null)
				{
					this.enabled = traceSection.Enabled;
					this.local_only = traceSection.LocalOnly;
					this.page_output = traceSection.PageOutput;
					if (traceSection.TraceMode == TraceDisplayMode.SortByTime)
					{
						this.mode = TraceMode.SortByTime;
					}
					else
					{
						this.mode = TraceMode.SortByCategory;
					}
					this.request_limit = traceSection.RequestLimit;
				}
			}
			catch (Exception ex)
			{
				this.initialException = ex;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0002061C File Offset: 0x0001E81C
		public bool HasException
		{
			get
			{
				return this.initialException != null;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00020627 File Offset: 0x0001E827
		public Exception InitialException
		{
			get
			{
				return this.initialException;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0002062F File Offset: 0x0001E82F
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00020637 File Offset: 0x0001E837
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00020640 File Offset: 0x0001E840
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x00020648 File Offset: 0x0001E848
		public bool LocalOnly
		{
			get
			{
				return this.local_only;
			}
			set
			{
				this.local_only = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00020651 File Offset: 0x0001E851
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x00020659 File Offset: 0x0001E859
		public bool PageOutput
		{
			get
			{
				return this.page_output;
			}
			set
			{
				this.page_output = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00020662 File Offset: 0x0001E862
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x0002066C File Offset: 0x0001E86C
		public int RequestLimit
		{
			get
			{
				return this.request_limit;
			}
			set
			{
				if (this.request_limit == value)
				{
					return;
				}
				TraceData[] array = new TraceData[value];
				Array.Copy(this.data, array, (this.cur_item > value) ? value : this.cur_item);
				if (this.cur_item > value)
				{
					this.cur_item = value;
				}
				this.request_limit = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000206BF File Offset: 0x0001E8BF
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x000206C7 File Offset: 0x0001E8C7
		public TraceMode TraceMode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x000206D0 File Offset: 0x0001E8D0
		public TraceData[] TraceData
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000206D8 File Offset: 0x0001E8D8
		public void AddTraceData(TraceData item)
		{
			if (this.data == null)
			{
				this.data = new TraceData[this.request_limit];
			}
			if (this.cur_item == this.request_limit)
			{
				return;
			}
			TraceData[] array = this.data;
			int num = this.cur_item;
			this.cur_item = num + 1;
			array[num] = item;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00020726 File Offset: 0x0001E926
		public void Clear()
		{
			if (this.data == null)
			{
				return;
			}
			Array.Clear(this.data, 0, this.data.Length);
			this.cur_item = 0;
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002074C File Offset: 0x0001E94C
		public int ItemCount
		{
			get
			{
				return this.cur_item;
			}
		}

		// Token: 0x040010F0 RID: 4336
		private bool enabled;

		// Token: 0x040010F1 RID: 4337
		private bool local_only = true;

		// Token: 0x040010F2 RID: 4338
		private bool page_output;

		// Token: 0x040010F3 RID: 4339
		private TraceMode mode;

		// Token: 0x040010F4 RID: 4340
		private int request_limit = 10;

		// Token: 0x040010F5 RID: 4341
		private int cur_item;

		// Token: 0x040010F6 RID: 4342
		private TraceData[] data;

		// Token: 0x040010F7 RID: 4343
		private Exception initialException;
	}
}
