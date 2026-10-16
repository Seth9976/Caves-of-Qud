using System;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000006 RID: 6
	[UsedByNativeCode]
	public struct XRNodeState
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023FC File Offset: 0x000005FC
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002414 File Offset: 0x00000614
		public ulong uniqueID
		{
			get
			{
				return this.m_UniqueID;
			}
			set
			{
				this.m_UniqueID = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002420 File Offset: 0x00000620
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002438 File Offset: 0x00000638
		public XRNode nodeType
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002444 File Offset: 0x00000644
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000245F File Offset: 0x0000065F
		public bool tracked
		{
			get
			{
				return this.m_Tracked == 1;
			}
			set
			{
				this.m_Tracked = (value ? 1 : 0);
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000246F File Offset: 0x0000066F
		public Vector3 position
		{
			set
			{
				this.m_Position = value;
				this.m_AvailableFields |= AvailableTrackingData.PositionAvailable;
			}
		}

		// Token: 0x17000006 RID: 6
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002487 File Offset: 0x00000687
		public Quaternion rotation
		{
			set
			{
				this.m_Rotation = value;
				this.m_AvailableFields |= AvailableTrackingData.RotationAvailable;
			}
		}

		// Token: 0x17000007 RID: 7
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000249F File Offset: 0x0000069F
		public Vector3 velocity
		{
			set
			{
				this.m_Velocity = value;
				this.m_AvailableFields |= AvailableTrackingData.VelocityAvailable;
			}
		}

		// Token: 0x17000008 RID: 8
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000024B7 File Offset: 0x000006B7
		public Vector3 angularVelocity
		{
			set
			{
				this.m_AngularVelocity = value;
				this.m_AvailableFields |= AvailableTrackingData.AngularVelocityAvailable;
			}
		}

		// Token: 0x17000009 RID: 9
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000024CF File Offset: 0x000006CF
		public Vector3 acceleration
		{
			set
			{
				this.m_Acceleration = value;
				this.m_AvailableFields |= AvailableTrackingData.AccelerationAvailable;
			}
		}

		// Token: 0x1700000A RID: 10
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000024E8 File Offset: 0x000006E8
		public Vector3 angularAcceleration
		{
			set
			{
				this.m_AngularAcceleration = value;
				this.m_AvailableFields |= AvailableTrackingData.AngularAccelerationAvailable;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002504 File Offset: 0x00000704
		public bool TryGetPosition(out Vector3 position)
		{
			return this.TryGet(this.m_Position, AvailableTrackingData.PositionAvailable, out position);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002524 File Offset: 0x00000724
		public bool TryGetRotation(out Quaternion rotation)
		{
			return this.TryGet(this.m_Rotation, AvailableTrackingData.RotationAvailable, out rotation);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002544 File Offset: 0x00000744
		public bool TryGetVelocity(out Vector3 velocity)
		{
			return this.TryGet(this.m_Velocity, AvailableTrackingData.VelocityAvailable, out velocity);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002564 File Offset: 0x00000764
		public bool TryGetAngularVelocity(out Vector3 angularVelocity)
		{
			return this.TryGet(this.m_AngularVelocity, AvailableTrackingData.AngularVelocityAvailable, out angularVelocity);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002584 File Offset: 0x00000784
		public bool TryGetAcceleration(out Vector3 acceleration)
		{
			return this.TryGet(this.m_Acceleration, AvailableTrackingData.AccelerationAvailable, out acceleration);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025A8 File Offset: 0x000007A8
		public bool TryGetAngularAcceleration(out Vector3 angularAcceleration)
		{
			return this.TryGet(this.m_AngularAcceleration, AvailableTrackingData.AngularAccelerationAvailable, out angularAcceleration);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025CC File Offset: 0x000007CC
		private bool TryGet(Vector3 inValue, AvailableTrackingData availabilityFlag, out Vector3 outValue)
		{
			bool flag = (this.m_AvailableFields & availabilityFlag) > AvailableTrackingData.None;
			bool flag2;
			if (flag)
			{
				outValue = inValue;
				flag2 = true;
			}
			else
			{
				outValue = Vector3.zero;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002608 File Offset: 0x00000808
		private bool TryGet(Quaternion inValue, AvailableTrackingData availabilityFlag, out Quaternion outValue)
		{
			bool flag = (this.m_AvailableFields & availabilityFlag) > AvailableTrackingData.None;
			bool flag2;
			if (flag)
			{
				outValue = inValue;
				flag2 = true;
			}
			else
			{
				outValue = Quaternion.identity;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0400001C RID: 28
		private XRNode m_Type;

		// Token: 0x0400001D RID: 29
		private AvailableTrackingData m_AvailableFields;

		// Token: 0x0400001E RID: 30
		private Vector3 m_Position;

		// Token: 0x0400001F RID: 31
		private Quaternion m_Rotation;

		// Token: 0x04000020 RID: 32
		private Vector3 m_Velocity;

		// Token: 0x04000021 RID: 33
		private Vector3 m_AngularVelocity;

		// Token: 0x04000022 RID: 34
		private Vector3 m_Acceleration;

		// Token: 0x04000023 RID: 35
		private Vector3 m_AngularAcceleration;

		// Token: 0x04000024 RID: 36
		private int m_Tracked;

		// Token: 0x04000025 RID: 37
		private ulong m_UniqueID;
	}
}
