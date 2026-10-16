using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Web
{
	// Token: 0x02000063 RID: 99
	internal abstract class BaseParamsCollection : WebROCollection
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0000755A File Offset: 0x0000575A
		public BaseParamsCollection(HttpRequest request)
		{
			this._request = request;
			base.IsReadOnly = true;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00007570 File Offset: 0x00005770
		private void LoadInfo()
		{
			if (this._loaded)
			{
				return;
			}
			base.IsReadOnly = false;
			this.InsertInfo();
			base.IsReadOnly = true;
			this._loaded = true;
		}

		// Token: 0x06000413 RID: 1043
		protected abstract void InsertInfo();

		// Token: 0x06000414 RID: 1044 RVA: 0x00007596 File Offset: 0x00005796
		public override string Get(int index)
		{
			this.LoadInfo();
			return base.Get(index);
		}

		// Token: 0x06000415 RID: 1045
		protected abstract string InternalGet(string name);

		// Token: 0x06000416 RID: 1046 RVA: 0x000075A8 File Offset: 0x000057A8
		public override string Get(string name)
		{
			if (!this._loaded)
			{
				string text = this.InternalGet(name);
				if (text != null && text.Length > 0)
				{
					return text;
				}
				this.LoadInfo();
			}
			return base.Get(name);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000075E0 File Offset: 0x000057E0
		public override string GetKey(int index)
		{
			this.LoadInfo();
			return base.GetKey(index);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000075F0 File Offset: 0x000057F0
		public override string[] GetValues(int index)
		{
			string text = this.Get(index);
			if (text == null)
			{
				return null;
			}
			return new string[] { text };
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00007614 File Offset: 0x00005814
		public override string[] GetValues(string name)
		{
			string text = this.Get(name);
			if (text == null)
			{
				return null;
			}
			return new string[] { text };
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00007638 File Offset: 0x00005838
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new SerializationException();
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000763F File Offset: 0x0000583F
		public override string[] AllKeys
		{
			get
			{
				this.LoadInfo();
				return base.AllKeys;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000764D File Offset: 0x0000584D
		public override int Count
		{
			get
			{
				this.LoadInfo();
				return base.Count;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000765B File Offset: 0x0000585B
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				this.LoadInfo();
				return base.Keys;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00007669 File Offset: 0x00005869
		public override IEnumerator GetEnumerator()
		{
			this.LoadInfo();
			return base.GetEnumerator();
		}

		// Token: 0x04000E4A RID: 3658
		protected HttpRequest _request;

		// Token: 0x04000E4B RID: 3659
		protected bool _loaded;
	}
}
