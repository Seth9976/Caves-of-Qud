using System;
using System.Collections;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000032 RID: 50
	public abstract class LdapSchemaElement : LdapAttribute
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00009FF8 File Offset: 0x000081F8
		private void InitBlock()
		{
			this.hashQualifier = new Hashtable();
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000A008 File Offset: 0x00008208
		public virtual string[] Names
		{
			get
			{
				if (this.names == null)
				{
					return null;
				}
				string[] array = new string[this.names.Length];
				this.names.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000A03B File Offset: 0x0000823B
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000A043 File Offset: 0x00008243
		public virtual string ID
		{
			get
			{
				return this.oid;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000A04B File Offset: 0x0000824B
		public virtual IEnumerator QualifierNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.hashQualifier.Keys).GetEnumerator());
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A067 File Offset: 0x00008267
		public virtual bool Obsolete
		{
			get
			{
				return this.obsolete;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A070 File Offset: 0x00008270
		protected internal LdapSchemaElement(string attrName)
			: base(attrName)
		{
			this.InitBlock();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A0C8 File Offset: 0x000082C8
		public virtual string[] getQualifier(string name)
		{
			AttributeQualifier attributeQualifier = (AttributeQualifier)this.hashQualifier[name];
			if (attributeQualifier != null)
			{
				return attributeQualifier.Values;
			}
			return null;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A0F2 File Offset: 0x000082F2
		public override string ToString()
		{
			return this.formatString();
		}

		// Token: 0x0600020F RID: 527
		protected internal abstract string formatString();

		// Token: 0x06000210 RID: 528 RVA: 0x0000A0FC File Offset: 0x000082FC
		public virtual void setQualifier(string name, string[] values)
		{
			AttributeQualifier attributeQualifier = new AttributeQualifier(name, values);
			SupportClass.PutElement(this.hashQualifier, name, attributeQualifier);
			base.Value = this.formatString();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A12B File Offset: 0x0000832B
		public override void addValue(string value_Renamed)
		{
			throw new NotSupportedException("addValue is not supported by LdapSchemaElement");
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A137 File Offset: 0x00008337
		public virtual void addValue(byte[] value_Renamed)
		{
			throw new NotSupportedException("addValue is not supported by LdapSchemaElement");
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A143 File Offset: 0x00008343
		public override void removeValue(string value_Renamed)
		{
			throw new NotSupportedException("removeValue is not supported by LdapSchemaElement");
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A14F File Offset: 0x0000834F
		public virtual void removeValue(byte[] value_Renamed)
		{
			throw new NotSupportedException("removeValue is not supported by LdapSchemaElement");
		}

		// Token: 0x0400013D RID: 317
		[CLSCompliant(false)]
		protected internal string[] names = new string[] { "" };

		// Token: 0x0400013E RID: 318
		protected internal string oid = "";

		// Token: 0x0400013F RID: 319
		[CLSCompliant(false)]
		protected internal string description = "";

		// Token: 0x04000140 RID: 320
		[CLSCompliant(false)]
		protected internal bool obsolete;

		// Token: 0x04000141 RID: 321
		protected internal string[] qualifier = new string[] { "" };

		// Token: 0x04000142 RID: 322
		protected internal Hashtable hashQualifier;
	}
}
