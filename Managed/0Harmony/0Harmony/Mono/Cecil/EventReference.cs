using System;

namespace Mono.Cecil
{
	// Token: 0x02000201 RID: 513
	public abstract class EventReference : MemberReference
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00026CAD File Offset: 0x00024EAD
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00026CB5 File Offset: 0x00024EB5
		public TypeReference EventType
		{
			get
			{
				return this.event_type;
			}
			set
			{
				this.event_type = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00026CBE File Offset: 0x00024EBE
		public override string FullName
		{
			get
			{
				return this.event_type.FullName + " " + base.MemberFullName();
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00026CDB File Offset: 0x00024EDB
		protected EventReference(string name, TypeReference eventType)
			: base(name)
		{
			Mixin.CheckType(eventType, Mixin.Argument.eventType);
			this.event_type = eventType;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00026CF3 File Offset: 0x00024EF3
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x06000A81 RID: 2689
		public new abstract EventDefinition Resolve();

		// Token: 0x04000302 RID: 770
		private TypeReference event_type;
	}
}
