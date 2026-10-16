using System;
using System.Collections;
using System.Text;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200000E RID: 14
	public class LdapAttributeSet : SupportClass.AbstractSetSupport, ICloneable
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004629 File Offset: 0x00002829
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004636 File Offset: 0x00002836
		public LdapAttributeSet()
		{
			this.map = new Hashtable();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000464C File Offset: 0x0000284C
		public override object Clone()
		{
			object obj3;
			try
			{
				object obj = base.MemberwiseClone();
				foreach (object obj2 in this)
				{
					((LdapAttributeSet)obj).Add(((LdapAttribute)obj2).Clone());
				}
				obj3 = obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj3;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000046B0 File Offset: 0x000028B0
		public virtual LdapAttribute getAttribute(string attrName)
		{
			return (LdapAttribute)this.map[attrName.ToUpper()];
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000046C8 File Offset: 0x000028C8
		public virtual LdapAttribute getAttribute(string attrName, string lang)
		{
			string text = attrName + ";" + lang;
			return (LdapAttribute)this.map[text.ToUpper()];
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000046F8 File Offset: 0x000028F8
		public virtual LdapAttributeSet getSubset(string subtype)
		{
			LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
			foreach (object obj in this)
			{
				LdapAttribute ldapAttribute = (LdapAttribute)obj;
				if (ldapAttribute.hasSubtype(subtype))
				{
					ldapAttributeSet.Add(ldapAttribute.Clone());
				}
			}
			return ldapAttributeSet;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000473F File Offset: 0x0000293F
		public override IEnumerator GetEnumerator()
		{
			return this.map.Values.GetEnumerator();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004751 File Offset: 0x00002951
		public override bool IsEmpty()
		{
			return this.map.Count == 0;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004764 File Offset: 0x00002964
		public override bool Contains(object attr)
		{
			LdapAttribute ldapAttribute = (LdapAttribute)attr;
			return this.map.ContainsKey(ldapAttribute.Name.ToUpper());
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004790 File Offset: 0x00002990
		public override bool Add(object attr)
		{
			LdapAttribute ldapAttribute = (LdapAttribute)attr;
			string text = ldapAttribute.Name.ToUpper();
			if (this.map.ContainsKey(text))
			{
				return false;
			}
			SupportClass.PutElement(this.map, text, ldapAttribute);
			return true;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000047D0 File Offset: 0x000029D0
		public override bool Remove(object object_Renamed)
		{
			string text;
			if (object_Renamed is string)
			{
				text = (string)object_Renamed;
			}
			else
			{
				text = ((LdapAttribute)object_Renamed).Name;
			}
			return text != null && SupportClass.HashtableRemove(this.map, text.ToUpper()) != null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004813 File Offset: 0x00002A13
		public override void Clear()
		{
			this.map.Clear();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004820 File Offset: 0x00002A20
		public override bool AddAll(ICollection c)
		{
			bool flag = false;
			IEnumerator enumerator = c.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (this.Add(enumerator.Current))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004854 File Offset: 0x00002A54
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("LdapAttributeSet: ");
			IEnumerator enumerator = this.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				if (!flag)
				{
					stringBuilder.Append(" ");
				}
				flag = false;
				LdapAttribute ldapAttribute = (LdapAttribute)enumerator.Current;
				stringBuilder.Append(ldapAttribute.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400006D RID: 109
		private Hashtable map;
	}
}
