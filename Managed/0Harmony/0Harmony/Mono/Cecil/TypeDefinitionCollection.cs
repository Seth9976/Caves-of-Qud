using System;
using System.Collections.Generic;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000272 RID: 626
	internal sealed class TypeDefinitionCollection : Collection<TypeDefinition>
	{
		// Token: 0x06000F5A RID: 3930 RVA: 0x0002F173 File Offset: 0x0002D373
		internal TypeDefinitionCollection(ModuleDefinition container)
		{
			this.container = container;
			this.name_cache = new Dictionary<Row<string, string>, TypeDefinition>(new RowEqualityComparer());
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002F192 File Offset: 0x0002D392
		internal TypeDefinitionCollection(ModuleDefinition container, int capacity)
			: base(capacity)
		{
			this.container = container;
			this.name_cache = new Dictionary<Row<string, string>, TypeDefinition>(capacity, new RowEqualityComparer());
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002F1B3 File Offset: 0x0002D3B3
		protected override void OnAdd(TypeDefinition item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002F1B3 File Offset: 0x0002D3B3
		protected override void OnSet(TypeDefinition item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002F1B3 File Offset: 0x0002D3B3
		protected override void OnInsert(TypeDefinition item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002F1BC File Offset: 0x0002D3BC
		protected override void OnRemove(TypeDefinition item, int index)
		{
			this.Detach(item);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002F1C8 File Offset: 0x0002D3C8
		protected override void OnClear()
		{
			foreach (TypeDefinition typeDefinition in this)
			{
				this.Detach(typeDefinition);
			}
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002F218 File Offset: 0x0002D418
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

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002F27B File Offset: 0x0002D47B
		private void Detach(TypeDefinition type)
		{
			type.module = null;
			type.scope = null;
			this.name_cache.Remove(new Row<string, string>(type.Namespace, type.Name));
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002F2A8 File Offset: 0x0002D4A8
		public TypeDefinition GetType(string fullname)
		{
			string text;
			string text2;
			TypeParser.SplitFullName(fullname, out text, out text2);
			return this.GetType(text, text2);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002F2C8 File Offset: 0x0002D4C8
		public TypeDefinition GetType(string @namespace, string name)
		{
			TypeDefinition typeDefinition;
			if (this.name_cache.TryGetValue(new Row<string, string>(@namespace, name), out typeDefinition))
			{
				return typeDefinition;
			}
			return null;
		}

		// Token: 0x0400054A RID: 1354
		private readonly ModuleDefinition container;

		// Token: 0x0400054B RID: 1355
		private readonly Dictionary<Row<string, string>, TypeDefinition> name_cache;
	}
}
