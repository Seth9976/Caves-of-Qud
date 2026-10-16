using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200014F RID: 335
	internal struct TypeTable
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x000573AC File Offset: 0x000555AC
		public IEnumerable<string> names
		{
			get
			{
				return this.table.Keys.Select<InternedString, string>((InternedString x) => x.ToString());
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x000573DD File Offset: 0x000555DD
		public IEnumerable<InternedString> internedNames
		{
			get
			{
				return this.table.Keys;
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000573EA File Offset: 0x000555EA
		public void Initialize(InputManager manager)
		{
			this.table = new Dictionary<InternedString, Type>();
			this.m_Manager = manager;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00057400 File Offset: 0x00055600
		public InternedString FindNameForType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			foreach (KeyValuePair<InternedString, Type> keyValuePair in this.table)
			{
				if (keyValuePair.Value == type)
				{
					return keyValuePair.Key;
				}
			}
			return default(InternedString);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00057484 File Offset: 0x00055684
		public void AddTypeRegistration(string name, Type type)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name cannot be null or empty", "name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			InternedString internedString = new InternedString(name);
			this.table[internedString] = type;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000574D2 File Offset: 0x000556D2
		public Type LookupTypeRegistration(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (this.table == null)
			{
				throw new InvalidOperationException("Input System not yet initialized");
			}
			return this.TryLookupTypeRegistration(new InternedString(name));
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00057500 File Offset: 0x00055700
		private Type TryLookupTypeRegistration(InternedString internedName)
		{
			Type type;
			if (!this.table.TryGetValue(internedName, out type) && this.m_Manager != null && this.m_Manager.RegisterCustomTypes())
			{
				this.table.TryGetValue(internedName, out type);
			}
			return type;
		}

		// Token: 0x0400074B RID: 1867
		public Dictionary<InternedString, Type> table;

		// Token: 0x0400074C RID: 1868
		private InputManager m_Manager;
	}
}
