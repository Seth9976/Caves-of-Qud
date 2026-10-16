using System;
using System.Collections.Generic;

namespace PlayFab
{
	// Token: 0x02000022 RID: 34
	public class PlayFabApiSettings
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000F951 File Offset: 0x0000DB51
		public virtual Dictionary<string, string> RequestGetParams
		{
			get
			{
				return this._requestGetParams;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000F959 File Offset: 0x0000DB59
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000F961 File Offset: 0x0000DB61
		public virtual string ProductionEnvironmentUrl
		{
			get
			{
				return this._ProductionEnvironmentUrl;
			}
			set
			{
				this._ProductionEnvironmentUrl = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000F96A File Offset: 0x0000DB6A
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000F972 File Offset: 0x0000DB72
		public virtual string TitleId { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000F97B File Offset: 0x0000DB7B
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000F983 File Offset: 0x0000DB83
		internal virtual string VerticalName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000F98C File Offset: 0x0000DB8C
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000F994 File Offset: 0x0000DB94
		public virtual string AdvertisingIdType { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000F99D File Offset: 0x0000DB9D
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000F9A5 File Offset: 0x0000DBA5
		public virtual string AdvertisingIdValue { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000F9AE File Offset: 0x0000DBAE
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000F9B6 File Offset: 0x0000DBB6
		public virtual bool DisableAdvertising { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000F9BF File Offset: 0x0000DBBF
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000F9C7 File Offset: 0x0000DBC7
		public virtual bool DisableDeviceInfo { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		public virtual bool DisableFocusTimeCollection { get; set; }

		// Token: 0x060002CE RID: 718 RVA: 0x0000F9E1 File Offset: 0x0000DBE1
		public virtual string GetFullUrl(string apiCall, Dictionary<string, string> getParams)
		{
			return PlayFabSettings.GetFullUrl(apiCall, getParams, this);
		}

		// Token: 0x04000284 RID: 644
		private string _ProductionEnvironmentUrl = "playfabapi.com";

		// Token: 0x04000285 RID: 645
		public readonly Dictionary<string, string> _requestGetParams = new Dictionary<string, string> { { "sdk", "UnitySDK-2.90.200713" } };
	}
}
