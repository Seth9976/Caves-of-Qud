using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001F RID: 31
	public class LdapExtendedOperation : ICloneable
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00007979 File Offset: 0x00005B79
		[CLSCompliant(false)]
		public LdapExtendedOperation(string oid, sbyte[] vals)
		{
			this.oid = oid;
			this.vals = vals;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007990 File Offset: 0x00005B90
		public object Clone()
		{
			object obj2;
			try
			{
				object obj = base.MemberwiseClone();
				Array.Copy(this.vals, 0, ((LdapExtendedOperation)obj).vals, 0, this.vals.Length);
				obj2 = obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj2;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000079E8 File Offset: 0x00005BE8
		public virtual string getID()
		{
			return this.oid;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000079F0 File Offset: 0x00005BF0
		[CLSCompliant(false)]
		public virtual sbyte[] getValue()
		{
			return this.vals;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000079F8 File Offset: 0x00005BF8
		[CLSCompliant(false)]
		protected internal virtual void setValue(sbyte[] newVals)
		{
			this.vals = newVals;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007A01 File Offset: 0x00005C01
		protected internal virtual void setID(string newoid)
		{
			this.oid = newoid;
		}

		// Token: 0x040000FB RID: 251
		private string oid;

		// Token: 0x040000FC RID: 252
		private sbyte[] vals;
	}
}
