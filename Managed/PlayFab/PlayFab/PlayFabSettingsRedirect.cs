using System;

namespace PlayFab
{
	// Token: 0x02000023 RID: 35
	internal class PlayFabSettingsRedirect : PlayFabApiSettings
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000FA19 File Offset: 0x0000DC19
		public PlayFabSettingsRedirect(Func<PlayFabSharedSettings> getSO)
		{
			this.GetSO = getSO;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000FA28 File Offset: 0x0000DC28
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000FA58 File Offset: 0x0000DC58
		public override string ProductionEnvironmentUrl
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (!(playFabSharedSettings == null))
				{
					return playFabSharedSettings.ProductionEnvironmentUrl;
				}
				return base.ProductionEnvironmentUrl;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.ProductionEnvironmentUrl = value;
				}
				base.ProductionEnvironmentUrl = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000FA88 File Offset: 0x0000DC88
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		internal override string VerticalName
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (!(playFabSharedSettings == null))
				{
					return playFabSharedSettings.VerticalName;
				}
				return base.VerticalName;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.VerticalName = value;
				}
				base.VerticalName = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000FB18 File Offset: 0x0000DD18
		public override string TitleId
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (!(playFabSharedSettings == null))
				{
					return playFabSharedSettings.TitleId;
				}
				return base.TitleId;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.TitleId = value;
				}
				base.TitleId = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000FB48 File Offset: 0x0000DD48
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000FB78 File Offset: 0x0000DD78
		public override string AdvertisingIdType
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (!(playFabSharedSettings == null))
				{
					return playFabSharedSettings.AdvertisingIdType;
				}
				return base.AdvertisingIdType;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.AdvertisingIdType = value;
				}
				base.AdvertisingIdType = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		public override string AdvertisingIdValue
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (!(playFabSharedSettings == null))
				{
					return playFabSharedSettings.AdvertisingIdValue;
				}
				return base.AdvertisingIdValue;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.AdvertisingIdValue = value;
				}
				base.AdvertisingIdValue = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000FC08 File Offset: 0x0000DE08
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000FC38 File Offset: 0x0000DE38
		public override bool DisableAdvertising
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (!(playFabSharedSettings == null))
				{
					return playFabSharedSettings.DisableAdvertising;
				}
				return base.DisableAdvertising;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.DisableAdvertising = value;
				}
				base.DisableAdvertising = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000FC68 File Offset: 0x0000DE68
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000FC98 File Offset: 0x0000DE98
		public override bool DisableDeviceInfo
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (!(playFabSharedSettings == null))
				{
					return playFabSharedSettings.DisableDeviceInfo;
				}
				return base.DisableDeviceInfo;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.DisableDeviceInfo = value;
				}
				base.DisableDeviceInfo = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
		public override bool DisableFocusTimeCollection
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (!(playFabSharedSettings == null))
				{
					return playFabSharedSettings.DisableFocusTimeCollection;
				}
				return base.DisableFocusTimeCollection;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = this.GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.DisableFocusTimeCollection = value;
				}
				base.DisableFocusTimeCollection = value;
			}
		}

		// Token: 0x0400028D RID: 653
		private readonly Func<PlayFabSharedSettings> GetSO;
	}
}
