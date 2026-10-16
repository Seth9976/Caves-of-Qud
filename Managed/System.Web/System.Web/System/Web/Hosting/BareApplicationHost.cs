using System;
using System.Collections.Generic;

namespace System.Web.Hosting
{
	// Token: 0x02000547 RID: 1351
	internal sealed class BareApplicationHost : MarshalByRefObject
	{
		// Token: 0x06003A73 RID: 14963 RVA: 0x0009BA54 File Offset: 0x00099C54
		public BareApplicationHost()
		{
			this.Init();
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x0009BA64 File Offset: 0x00099C64
		private void Init()
		{
			this.hash = new Dictionary<Type, RegisteredItem>();
			HostingEnvironment.Host = this;
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.DomainUnload += this.OnDomainUnload;
			this.phys_path = (string)currentDomain.GetData(".appPath");
			this.vpath = (string)currentDomain.GetData(".appVPath");
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x0009BAC6 File Offset: 0x00099CC6
		public string VirtualPath
		{
			get
			{
				return this.vpath;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x0009BACE File Offset: 0x00099CCE
		public string PhysicalPath
		{
			get
			{
				return this.phys_path;
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06003A77 RID: 14967 RVA: 0x0009BAD6 File Offset: 0x00099CD6
		public AppDomain Domain
		{
			get
			{
				return AppDomain.CurrentDomain;
			}
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x0009BADD File Offset: 0x00099CDD
		public void Shutdown()
		{
			HostingEnvironment.InitiateShutdown();
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x0009BAE4 File Offset: 0x00099CE4
		public void StopObject(Type type)
		{
			if (!this.hash.ContainsKey(type))
			{
				return;
			}
			this.hash[type].Item.Stop(false);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x0009BB0C File Offset: 0x00099D0C
		public IRegisteredObject CreateInstance(Type type)
		{
			return (IRegisteredObject)Activator.CreateInstance(type, null);
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x0009BB1A File Offset: 0x00099D1A
		public void RegisterObject(IRegisteredObject obj, bool auto_clean)
		{
			this.hash[obj.GetType()] = new RegisteredItem(obj, auto_clean);
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x0009BB34 File Offset: 0x00099D34
		public bool UnregisterObject(IRegisteredObject obj)
		{
			return this.hash.Remove(obj.GetType());
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0009BB47 File Offset: 0x00099D47
		public IRegisteredObject GetObject(Type type)
		{
			if (this.hash.ContainsKey(type))
			{
				return this.hash[type].Item;
			}
			return null;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x0009BB6A File Offset: 0x00099D6A
		public string GetCodeGenDir()
		{
			return AppDomain.CurrentDomain.SetupInformation.DynamicBase;
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x0009BB7C File Offset: 0x00099D7C
		private void OnDomainUnload(object sender, EventArgs args)
		{
			this.Manager.RemoveHost(this.AppID);
			ICollection<RegisteredItem> values = this.hash.Values;
			RegisteredItem[] array = new RegisteredItem[this.hash.Count];
			values.CopyTo(array, 0);
			foreach (RegisteredItem registeredItem in array)
			{
				try
				{
					registeredItem.Item.Stop(true);
				}
				catch
				{
				}
			}
			this.hash.Clear();
		}

		// Token: 0x04001FD4 RID: 8148
		private string vpath;

		// Token: 0x04001FD5 RID: 8149
		private string phys_path;

		// Token: 0x04001FD6 RID: 8150
		private Dictionary<Type, RegisteredItem> hash;

		// Token: 0x04001FD7 RID: 8151
		internal ApplicationManager Manager;

		// Token: 0x04001FD8 RID: 8152
		internal string AppID;
	}
}
