using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002E RID: 46
	[CustomStyle("SignalEmitter")]
	[ExcludeFromPreset]
	[Serializable]
	public class SignalEmitter : Marker, INotification, INotificationOptionProvider
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00008873 File Offset: 0x00006A73
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000887B File Offset: 0x00006A7B
		public bool retroactive
		{
			get
			{
				return this.m_Retroactive;
			}
			set
			{
				this.m_Retroactive = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00008884 File Offset: 0x00006A84
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000888C File Offset: 0x00006A8C
		public bool emitOnce
		{
			get
			{
				return this.m_EmitOnce;
			}
			set
			{
				this.m_EmitOnce = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00008895 File Offset: 0x00006A95
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000889D File Offset: 0x00006A9D
		public SignalAsset asset
		{
			get
			{
				return this.m_Asset;
			}
			set
			{
				this.m_Asset = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000088A6 File Offset: 0x00006AA6
		PropertyName INotification.id
		{
			get
			{
				if (this.m_Asset != null)
				{
					return new PropertyName(this.m_Asset.name);
				}
				return new PropertyName(string.Empty);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000088D1 File Offset: 0x00006AD1
		NotificationFlags INotificationOptionProvider.flags
		{
			get
			{
				return (this.retroactive ? NotificationFlags.Retroactive : ((NotificationFlags)0)) | (this.emitOnce ? NotificationFlags.TriggerOnce : ((NotificationFlags)0)) | NotificationFlags.TriggerInEditMode;
			}
		}

		// Token: 0x040000CF RID: 207
		[SerializeField]
		private bool m_Retroactive;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		private bool m_EmitOnce;

		// Token: 0x040000D1 RID: 209
		[SerializeField]
		private SignalAsset m_Asset;
	}
}
