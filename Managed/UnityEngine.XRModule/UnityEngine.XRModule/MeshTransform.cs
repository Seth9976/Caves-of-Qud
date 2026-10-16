using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000033 RID: 51
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public readonly struct MeshTransform : IEquatable<MeshTransform>
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005E3E File Offset: 0x0000403E
		public MeshId MeshId { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00005E46 File Offset: 0x00004046
		public ulong Timestamp { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005E4E File Offset: 0x0000404E
		public Vector3 Position { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005E56 File Offset: 0x00004056
		public Quaternion Rotation { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005E5E File Offset: 0x0000405E
		public Vector3 Scale { get; }

		// Token: 0x060001BA RID: 442 RVA: 0x00005E66 File Offset: 0x00004066
		public MeshTransform(in MeshId meshId, ulong timestamp, in Vector3 position, in Quaternion rotation, in Vector3 scale)
		{
			this.MeshId = meshId;
			this.Timestamp = timestamp;
			this.Position = position;
			this.Rotation = rotation;
			this.Scale = scale;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005EA4 File Offset: 0x000040A4
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is MeshTransform)
			{
				MeshTransform meshTransform = (MeshTransform)obj;
				flag = this.Equals(meshTransform);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005ECC File Offset: 0x000040CC
		public bool Equals(MeshTransform other)
		{
			return this.MeshId.Equals(other.MeshId) && this.Timestamp == other.Timestamp && this.Position.Equals(other.Position) && this.Rotation.Equals(other.Rotation) && this.Scale.Equals(other.Scale);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005F45 File Offset: 0x00004145
		public static bool operator ==(MeshTransform lhs, MeshTransform rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005F4F File Offset: 0x0000414F
		public static bool operator !=(MeshTransform lhs, MeshTransform rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005F5C File Offset: 0x0000415C
		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(this.MeshId.GetHashCode(), this.Timestamp.GetHashCode(), this.Position.GetHashCode(), this.Rotation.GetHashCode(), this.Scale.GetHashCode());
		}
	}
}
