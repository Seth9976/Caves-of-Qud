using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000028 RID: 40
	public abstract class Marker : ScriptableObject, IMarker
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000084B2 File Offset: 0x000066B2
		// (set) Token: 0x0600022E RID: 558 RVA: 0x000084BA File Offset: 0x000066BA
		public TrackAsset parent { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000084C3 File Offset: 0x000066C3
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000084CB File Offset: 0x000066CB
		public double time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = Math.Max(value, 0.0);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000084E4 File Offset: 0x000066E4
		void IMarker.Initialize(TrackAsset parentTrack)
		{
			if (this.parent == null)
			{
				this.parent = parentTrack;
				try
				{
					this.OnInitialize(parentTrack);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.Message, this);
				}
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000852C File Offset: 0x0000672C
		public virtual void OnInitialize(TrackAsset aPent)
		{
		}

		// Token: 0x040000C8 RID: 200
		[SerializeField]
		[TimeField(TimeFieldAttribute.UseEditMode.ApplyEditMode)]
		[Tooltip("Time for the marker")]
		private double m_Time;
	}
}
