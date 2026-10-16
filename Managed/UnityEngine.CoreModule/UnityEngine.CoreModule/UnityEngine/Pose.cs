using System;

namespace UnityEngine
{
	// Token: 0x020002F0 RID: 752
	[Serializable]
	public struct Pose : IEquatable<Pose>
	{
		// Token: 0x060024E2 RID: 9442 RVA: 0x00047396 File Offset: 0x00045596
		public Pose(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000473A8 File Offset: 0x000455A8
		public override string ToString()
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(),
				this.rotation.ToString()
			});
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000473F4 File Offset: 0x000455F4
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(format),
				this.rotation.ToString(format)
			});
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x00047434 File Offset: 0x00045634
		public Pose GetTransformedBy(Pose lhs)
		{
			return new Pose
			{
				position = lhs.position + lhs.rotation * this.position,
				rotation = lhs.rotation * this.rotation
			};
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0004748C File Offset: 0x0004568C
		public Pose GetTransformedBy(Transform lhs)
		{
			return new Pose
			{
				position = lhs.TransformPoint(this.position),
				rotation = lhs.rotation * this.rotation
			};
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x000474D4 File Offset: 0x000456D4
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x000474F8 File Offset: 0x000456F8
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x0004751C File Offset: 0x0004571C
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x00047540 File Offset: 0x00045740
		public static Pose identity
		{
			get
			{
				return Pose.k_Identity;
			}
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00047558 File Offset: 0x00045758
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Pose);
			return !flag && this.Equals((Pose)obj);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0004758C File Offset: 0x0004578C
		public bool Equals(Pose other)
		{
			return this.position.Equals(other.position) && this.rotation.Equals(other.rotation);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000475C8 File Offset: 0x000457C8
		public override int GetHashCode()
		{
			return this.position.GetHashCode() ^ (this.rotation.GetHashCode() << 1);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00047600 File Offset: 0x00045800
		public static bool operator ==(Pose a, Pose b)
		{
			return a.position == b.position && a.rotation.Equals(b.rotation);
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0004763C File Offset: 0x0004583C
		public static bool operator !=(Pose a, Pose b)
		{
			return !(a == b);
		}

		// Token: 0x04000A79 RID: 2681
		public Vector3 position;

		// Token: 0x04000A7A RID: 2682
		public Quaternion rotation;

		// Token: 0x04000A7B RID: 2683
		private static readonly Pose k_Identity = new Pose(Vector3.zero, Quaternion.identity);
	}
}
