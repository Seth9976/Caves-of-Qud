using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200002F RID: 47
	public abstract class EventReference : MemberReference
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00004F6B File Offset: 0x0000316B
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00004F73 File Offset: 0x00003173
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004F7C File Offset: 0x0000317C
		public override string FullName
		{
			get
			{
				return this.event_type.FullName + " " + base.MemberFullName();
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004F99 File Offset: 0x00003199
		protected EventReference(string name, TypeReference eventType)
			: base(name)
		{
			Mixin.CheckType(eventType, Mixin.Argument.eventType);
			this.event_type = eventType;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004FB1 File Offset: 0x000031B1
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x06000156 RID: 342
		public new abstract EventDefinition Resolve();

		// Token: 0x0400005A RID: 90
		private TypeReference event_type;
	}
}
