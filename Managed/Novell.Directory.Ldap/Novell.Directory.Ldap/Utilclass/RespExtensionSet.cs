using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200004E RID: 78
	public class RespExtensionSet : SupportClass.AbstractSetSupport
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000E670 File Offset: 0x0000C870
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E67D File Offset: 0x0000C87D
		public RespExtensionSet()
		{
			this.map = new Hashtable();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000E690 File Offset: 0x0000C890
		public void registerResponseExtension(string oid, Type extClass)
		{
			lock (this)
			{
				if (!this.map.ContainsKey(oid))
				{
					this.map.Add(oid, extClass);
				}
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		public override IEnumerator GetEnumerator()
		{
			return this.map.Values.GetEnumerator();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		public Type findResponseExtension(string searchOID)
		{
			Type type;
			lock (this)
			{
				if (this.map.ContainsKey(searchOID))
				{
					type = (Type)this.map[searchOID];
				}
				else
				{
					type = null;
				}
			}
			return type;
		}

		// Token: 0x040001F7 RID: 503
		private Hashtable map;
	}
}
