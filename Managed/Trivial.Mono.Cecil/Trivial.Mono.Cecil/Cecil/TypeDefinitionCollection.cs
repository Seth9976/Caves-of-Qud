using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000BD RID: 189
	internal sealed class TypeDefinitionCollection : Collection<TypeDefinition>
	{
		// Token: 0x060007F2 RID: 2034 RVA: 0x000186CB File Offset: 0x000168CB
		internal TypeDefinitionCollection(ModuleDefinition container)
		{
			this.container = container;
			this.name_cache = new Dictionary<Row<string, string>, TypeDefinition>(new RowEqualityComparer());
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000186EA File Offset: 0x000168EA
		internal TypeDefinitionCollection(ModuleDefinition container, int capacity)
			: base(capacity)
		{
			this.container = container;
			this.name_cache = new Dictionary<Row<string, string>, TypeDefinition>(capacity, new RowEqualityComparer());
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001870B File Offset: 0x0001690B
		protected override void OnAdd(TypeDefinition item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001870B File Offset: 0x0001690B
		protected override void OnSet(TypeDefinition item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001870B File Offset: 0x0001690B
		protected override void OnInsert(TypeDefinition item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00018714 File Offset: 0x00016914
		protected override void OnRemove(TypeDefinition item, int index)
		{
			this.Detach(item);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00018720 File Offset: 0x00016920
		protected override void OnClear()
		{
			foreach (TypeDefinition typeDefinition in this)
			{
				this.Detach(typeDefinition);
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00018770 File Offset: 0x00016970
		private void Attach(TypeDefinition type)
		{
			if (type.Module != null && type.Module != this.container)
			{
				throw new ArgumentException("Type already attached");
			}
			type.module = this.container;
			type.scope = this.container;
			this.name_cache[new Row<string, string>(type.Namespace, type.Name)] = type;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000187D3 File Offset: 0x000169D3
		private void Detach(TypeDefinition type)
		{
			type.module = null;
			type.scope = null;
			this.name_cache.Remove(new Row<string, string>(type.Namespace, type.Name));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00018800 File Offset: 0x00016A00
		public TypeDefinition GetType(string fullname)
		{
			string text;
			string text2;
			TypeParser.SplitFullName(fullname, out text, out text2);
			return this.GetType(text, text2);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00018820 File Offset: 0x00016A20
		public TypeDefinition GetType(string @namespace, string name)
		{
			TypeDefinition typeDefinition;
			if (this.name_cache.TryGetValue(new Row<string, string>(@namespace, name), ref typeDefinition))
			{
				return typeDefinition;
			}
			return null;
		}

		// Token: 0x040002D2 RID: 722
		private readonly ModuleDefinition container;

		// Token: 0x040002D3 RID: 723
		private readonly Dictionary<Row<string, string>, TypeDefinition> name_cache;
	}
}
