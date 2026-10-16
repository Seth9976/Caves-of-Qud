using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200004D RID: 77
	public class RespControlVector : ArrayList
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000E5A7 File Offset: 0x0000C7A7
		public RespControlVector(int cap, int incr)
			: base(cap)
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		public void registerResponseControl(string oid, Type controlClass)
		{
			lock (this)
			{
				this.Add(new RespControlVector.RegisteredControl(this, oid, controlClass));
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		public Type findResponseControl(string searchOID)
		{
			Type type;
			lock (this)
			{
				for (int i = 0; i < this.Count; i++)
				{
					RespControlVector.RegisteredControl registeredControl;
					if ((registeredControl = (RespControlVector.RegisteredControl)this[i]) == null)
					{
						throw new FieldAccessException();
					}
					if (registeredControl.myOID.CompareTo(searchOID) == 0)
					{
						return registeredControl.myClass;
					}
				}
				type = null;
			}
			return type;
		}

		// Token: 0x020000F4 RID: 244
		private class RegisteredControl
		{
			// Token: 0x06000626 RID: 1574 RVA: 0x00019354 File Offset: 0x00017554
			private void InitBlock(RespControlVector enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001935D File Offset: 0x0001755D
			public RespControlVector Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x06000628 RID: 1576 RVA: 0x00019365 File Offset: 0x00017565
			public RegisteredControl(RespControlVector enclosingInstance, string oid, Type controlClass)
			{
				this.InitBlock(enclosingInstance);
				this.myOID = oid;
				this.myClass = controlClass;
			}

			// Token: 0x040004E8 RID: 1256
			private RespControlVector enclosingInstance;

			// Token: 0x040004E9 RID: 1257
			public string myOID;

			// Token: 0x040004EA RID: 1258
			public Type myClass;
		}
	}
}
