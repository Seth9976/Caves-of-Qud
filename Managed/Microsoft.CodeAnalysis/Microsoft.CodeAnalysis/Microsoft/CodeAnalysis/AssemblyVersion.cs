using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000164 RID: 356
	internal struct AssemblyVersion : IEquatable<AssemblyVersion>, IComparable<AssemblyVersion>
	{
		// Token: 0x06000E56 RID: 3670 RVA: 0x0002970B File Offset: 0x0002790B
		public AssemblyVersion(ushort major, ushort minor, ushort build, ushort revision)
		{
			this._major = major;
			this._minor = minor;
			this._build = build;
			this._revision = revision;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0002972A File Offset: 0x0002792A
		public int Major
		{
			get
			{
				return (int)this._major;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00029732 File Offset: 0x00027932
		public int Minor
		{
			get
			{
				return (int)this._minor;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0002973A File Offset: 0x0002793A
		public int Build
		{
			get
			{
				return (int)this._build;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00029742 File Offset: 0x00027942
		public int Revision
		{
			get
			{
				return (int)this._revision;
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0002974A File Offset: 0x0002794A
		private ulong ToInteger()
		{
			return ((ulong)this._major << 48) | ((ulong)this._minor << 32) | ((ulong)this._build << 16) | (ulong)this._revision;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00029774 File Offset: 0x00027974
		public int CompareTo(AssemblyVersion other)
		{
			ulong num = this.ToInteger();
			ulong num2 = other.ToInteger();
			if (num == num2)
			{
				return 0;
			}
			if (num >= num2)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0002979D File Offset: 0x0002799D
		public bool Equals(AssemblyVersion other)
		{
			return this.ToInteger() == other.ToInteger();
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000297AE File Offset: 0x000279AE
		public override bool Equals(object obj)
		{
			return obj is AssemblyVersion && this.Equals((AssemblyVersion)obj);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000297C6 File Offset: 0x000279C6
		public override int GetHashCode()
		{
			return ((int)(this._major & 15) << 28) | ((int)(this._minor & 255) << 20) | ((int)(this._build & 255) << 12) | (int)(this._revision & 4095);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00029801 File Offset: 0x00027A01
		public static bool operator ==(AssemblyVersion left, AssemblyVersion right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0002980B File Offset: 0x00027A0B
		public static bool operator !=(AssemblyVersion left, AssemblyVersion right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00029818 File Offset: 0x00027A18
		public static bool operator <(AssemblyVersion left, AssemblyVersion right)
		{
			return left.ToInteger() < right.ToInteger();
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0002982A File Offset: 0x00027A2A
		public static bool operator <=(AssemblyVersion left, AssemblyVersion right)
		{
			return left.ToInteger() <= right.ToInteger();
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0002983F File Offset: 0x00027A3F
		public static bool operator >(AssemblyVersion left, AssemblyVersion right)
		{
			return left.ToInteger() > right.ToInteger();
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00029851 File Offset: 0x00027A51
		public static bool operator >=(AssemblyVersion left, AssemblyVersion right)
		{
			return left.ToInteger() >= right.ToInteger();
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00029866 File Offset: 0x00027A66
		public static explicit operator AssemblyVersion(Version version)
		{
			return new AssemblyVersion((ushort)version.Major, (ushort)version.Minor, (ushort)version.Build, (ushort)version.Revision);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00029889 File Offset: 0x00027A89
		public static explicit operator Version(AssemblyVersion version)
		{
			return new Version(version.Major, version.Minor, version.Build, version.Revision);
		}

		// Token: 0x040003FC RID: 1020
		private readonly ushort _major;

		// Token: 0x040003FD RID: 1021
		private readonly ushort _minor;

		// Token: 0x040003FE RID: 1022
		private readonly ushort _build;

		// Token: 0x040003FF RID: 1023
		private readonly ushort _revision;
	}
}
