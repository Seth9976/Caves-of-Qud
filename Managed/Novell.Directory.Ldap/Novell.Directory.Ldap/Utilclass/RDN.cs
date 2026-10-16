using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200004A RID: 74
	public class RDN
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		protected internal virtual string RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
		public virtual string Type
		{
			get
			{
				return (string)this.types[0];
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public virtual string[] Types
		{
			get
			{
				string[] array = new string[this.types.Count];
				for (int i = 0; i < this.types.Count; i++)
				{
					array[i] = (string)this.types[i];
				}
				return array;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000E021 File Offset: 0x0000C221
		public virtual string Value
		{
			get
			{
				return (string)this.values[0];
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000E034 File Offset: 0x0000C234
		public virtual string[] Values
		{
			get
			{
				string[] array = new string[this.values.Count];
				for (int i = 0; i < this.values.Count; i++)
				{
					array[i] = (string)this.values[i];
				}
				return array;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000E07D File Offset: 0x0000C27D
		public virtual bool Multivalued
		{
			get
			{
				return this.values.Count > 1;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E090 File Offset: 0x0000C290
		public RDN(string rdn)
		{
			this.rawValue = rdn;
			ArrayList rdns = new DN(rdn).RDNs;
			if (rdns.Count != 1)
			{
				throw new ArgumentException("Invalid RDN: see API documentation");
			}
			RDN rdn2 = (RDN)rdns[0];
			this.types = rdn2.types;
			this.values = rdn2.values;
			this.rawValue = rdn2.rawValue;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E0F9 File Offset: 0x0000C2F9
		public RDN()
		{
			this.types = new ArrayList();
			this.values = new ArrayList();
			this.rawValue = "";
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E124 File Offset: 0x0000C324
		[CLSCompliant(false)]
		public virtual bool equals(RDN rdn)
		{
			if (this.values.Count != rdn.values.Count)
			{
				return false;
			}
			for (int i = 0; i < this.values.Count; i++)
			{
				int num = 0;
				while (num < this.values.Count && (!((string)this.values[i]).ToUpper().Equals(((string)rdn.values[num]).ToUpper()) || !this.equalAttrType((string)this.types[i], (string)rdn.types[num])))
				{
					num++;
				}
				if (num >= rdn.values.Count)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E1EB File Offset: 0x0000C3EB
		private bool equalAttrType(string attr1, string attr2)
		{
			if (char.IsDigit(attr1[0]) ^ char.IsDigit(attr2[0]))
			{
				throw new ArgumentException("OID numbers are not currently compared to attribute names");
			}
			return attr1.ToUpper().Equals(attr2.ToUpper());
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E224 File Offset: 0x0000C424
		public virtual void add(string attrType, string attrValue, string rawValue)
		{
			this.types.Add(attrType);
			this.values.Add(attrValue);
			this.rawValue += rawValue;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E252 File Offset: 0x0000C452
		public override string ToString()
		{
			return this.toString(false);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000E25C File Offset: 0x0000C45C
		[CLSCompliant(false)]
		public virtual string toString(bool noTypes)
		{
			int count = this.types.Count;
			string text = "";
			if (count < 1)
			{
				return null;
			}
			if (!noTypes)
			{
				object obj = this.types[0];
				text = ((obj != null) ? obj.ToString() : null) + "=";
			}
			string text2 = text;
			object obj2 = this.values[0];
			text = text2 + ((obj2 != null) ? obj2.ToString() : null);
			for (int i = 1; i < count; i++)
			{
				text += "+";
				if (!noTypes)
				{
					string text3 = text;
					object obj3 = this.types[i];
					text = text3 + ((obj3 != null) ? obj3.ToString() : null) + "=";
				}
				string text4 = text;
				object obj4 = this.values[i];
				text = text4 + ((obj4 != null) ? obj4.ToString() : null);
			}
			return text;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000E328 File Offset: 0x0000C528
		public virtual string[] explodeRDN(bool noTypes)
		{
			int count = this.types.Count;
			if (count < 1)
			{
				return null;
			}
			string[] array = new string[this.types.Count];
			if (!noTypes)
			{
				string[] array2 = array;
				int num = 0;
				object obj = this.types[0];
				array2[num] = ((obj != null) ? obj.ToString() : null) + "=";
			}
			string[] array3 = array;
			int num2 = 0;
			string text = array3[num2];
			object obj2 = this.values[0];
			array3[num2] = text + ((obj2 != null) ? obj2.ToString() : null);
			for (int i = 1; i < count; i++)
			{
				if (!noTypes)
				{
					string[] array4 = array;
					int num3 = i;
					string text2 = array4[num3];
					object obj3 = this.types[i];
					array4[num3] = text2 + ((obj3 != null) ? obj3.ToString() : null) + "=";
				}
				string[] array5 = array;
				int num4 = i;
				string text3 = array5[num4];
				object obj4 = this.values[i];
				array5[num4] = text3 + ((obj4 != null) ? obj4.ToString() : null);
			}
			return array;
		}

		// Token: 0x040001ED RID: 493
		private ArrayList types;

		// Token: 0x040001EE RID: 494
		private ArrayList values;

		// Token: 0x040001EF RID: 495
		private string rawValue;
	}
}
