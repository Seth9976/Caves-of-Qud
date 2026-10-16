using System;
using System.Text;

namespace XRL.World.Conversations
{
	// Token: 0x02000BD1 RID: 3025
	[ConversationEvent(Base = true)]
	public abstract class ITemplateTextEvent : ConversationEvent
	{
		// Token: 0x06007988 RID: 31112 RVA: 0x002DCA4D File Offset: 0x002DAC4D
		public ITemplateTextEvent(int ID)
			: base(ID)
		{
		}

		// Token: 0x06007989 RID: 31113 RVA: 0x002DCA56 File Offset: 0x002DAC56
		public override void Reset()
		{
			base.Reset();
			this.Text = null;
			this.Subject = null;
			this.Object = null;
			this.ExplicitSubject = null;
			this.ExplicitSubjectPlural = false;
			this.ExplicitObject = null;
			this.ExplicitObjectPlural = false;
		}

		// Token: 0x04003790 RID: 14224
		[ConversationEvent.ParameterAttribute(Required = true)]
		public StringBuilder Text;

		// Token: 0x04003791 RID: 14225
		[ConversationEvent.ParameterAttribute(Reference = true)]
		public GameObject Subject;

		// Token: 0x04003792 RID: 14226
		[ConversationEvent.ParameterAttribute(Reference = true)]
		public GameObject Object;

		// Token: 0x04003793 RID: 14227
		[ConversationEvent.ParameterAttribute(Output = true)]
		public string ExplicitSubject;

		// Token: 0x04003794 RID: 14228
		[ConversationEvent.ParameterAttribute(Output = true)]
		public bool ExplicitSubjectPlural;

		// Token: 0x04003795 RID: 14229
		[ConversationEvent.ParameterAttribute(Output = true)]
		public string ExplicitObject;

		// Token: 0x04003796 RID: 14230
		[ConversationEvent.ParameterAttribute(Output = true)]
		public bool ExplicitObjectPlural;
	}
}
