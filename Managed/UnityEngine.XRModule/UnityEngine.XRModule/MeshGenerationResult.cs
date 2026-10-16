using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002E RID: 46
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[RequiredByNativeCode]
	public struct MeshGenerationResult : IEquatable<MeshGenerationResult>
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005AE7 File Offset: 0x00003CE7
		public readonly MeshId MeshId { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00005AEF File Offset: 0x00003CEF
		public readonly Mesh Mesh { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00005AF7 File Offset: 0x00003CF7
		public readonly MeshCollider MeshCollider { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005AFF File Offset: 0x00003CFF
		public readonly MeshGenerationStatus Status { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005B07 File Offset: 0x00003D07
		public readonly MeshVertexAttributes Attributes { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00005B0F File Offset: 0x00003D0F
		public readonly ulong Timestamp { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005B17 File Offset: 0x00003D17
		public readonly Vector3 Position { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00005B1F File Offset: 0x00003D1F
		public readonly Quaternion Rotation { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005B27 File Offset: 0x00003D27
		public readonly Vector3 Scale { get; }

		// Token: 0x060001A5 RID: 421 RVA: 0x00005B30 File Offset: 0x00003D30
		public override bool Equals(object obj)
		{
			bool flag = !(obj is MeshGenerationResult);
			return !flag && this.Equals((MeshGenerationResult)obj);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005B64 File Offset: 0x00003D64
		public bool Equals(MeshGenerationResult other)
		{
			return this.MeshId.Equals(other.MeshId) && this.Mesh.Equals(other.Mesh) && this.MeshCollider.Equals(other.MeshCollider) && this.Status == other.Status && this.Attributes == other.Attributes && this.Position.Equals(other.Position) && this.Rotation.Equals(other.Rotation) && this.Scale.Equals(other.Scale);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005C1C File Offset: 0x00003E1C
		public static bool operator ==(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005C38 File Offset: 0x00003E38
		public static bool operator !=(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005C58 File Offset: 0x00003E58
		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(this.MeshId.GetHashCode(), this.Mesh.GetHashCode(), this.MeshCollider.GetHashCode(), ((int)this.Status).GetHashCode(), ((int)this.Attributes).GetHashCode(), this.Position.GetHashCode(), this.Rotation.GetHashCode(), this.Scale.GetHashCode());
		}
	}
}
