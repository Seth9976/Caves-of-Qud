using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000069 RID: 105
	public sealed class Utsname : IEquatable<Utsname>
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0000B45C File Offset: 0x0000965C
		public override int GetHashCode()
		{
			return this.sysname.GetHashCode() ^ this.nodename.GetHashCode() ^ this.release.GetHashCode() ^ this.version.GetHashCode() ^ this.machine.GetHashCode() ^ this.domainname.GetHashCode();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Utsname utsname = (Utsname)obj;
			return this.Equals(utsname);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000B4E4 File Offset: 0x000096E4
		public bool Equals(Utsname value)
		{
			return value.sysname == this.sysname && value.nodename == this.nodename && value.release == this.release && value.version == this.version && value.machine == this.machine && value.domainname == this.domainname;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000B563 File Offset: 0x00009763
		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3} {4}", new object[] { this.sysname, this.nodename, this.release, this.version, this.machine });
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000B5A2 File Offset: 0x000097A2
		public static bool operator ==(Utsname lhs, Utsname rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000B5AB File Offset: 0x000097AB
		public static bool operator !=(Utsname lhs, Utsname rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000485 RID: 1157
		public string sysname;

		// Token: 0x04000486 RID: 1158
		public string nodename;

		// Token: 0x04000487 RID: 1159
		public string release;

		// Token: 0x04000488 RID: 1160
		public string version;

		// Token: 0x04000489 RID: 1161
		public string machine;

		// Token: 0x0400048A RID: 1162
		public string domainname;
	}
}
