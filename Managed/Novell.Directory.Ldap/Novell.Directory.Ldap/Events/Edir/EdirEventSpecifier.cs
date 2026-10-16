using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000B7 RID: 183
	public class EdirEventSpecifier
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000149EE File Offset: 0x00012BEE
		public EdirEventType EventType
		{
			get
			{
				return this.event_type;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000149F6 File Offset: 0x00012BF6
		public EdirEventResultType EventResultType
		{
			get
			{
				return this.event_result_type;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000149FE File Offset: 0x00012BFE
		public string EventFilter
		{
			get
			{
				return this.event_filter;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00014A06 File Offset: 0x00012C06
		public EdirEventSpecifier(EdirEventType eventType, EdirEventResultType eventResultType)
			: this(eventType, eventResultType, null)
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00014A11 File Offset: 0x00012C11
		public EdirEventSpecifier(EdirEventType eventType, EdirEventResultType eventResultType, string filter)
		{
			this.event_type = eventType;
			this.event_result_type = eventResultType;
			this.event_filter = filter;
		}

		// Token: 0x04000429 RID: 1065
		private EdirEventType event_type;

		// Token: 0x0400042A RID: 1066
		private EdirEventResultType event_result_type;

		// Token: 0x0400042B RID: 1067
		private string event_filter;
	}
}
