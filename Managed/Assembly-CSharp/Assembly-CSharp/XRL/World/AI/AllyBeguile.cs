using System;

namespace XRL.World.AI
{
	// Token: 0x02000B59 RID: 2905
	public class AllyBeguile : IAllyReasonSourced
	{
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06007501 RID: 29953 RVA: 0x002BF008 File Offset: 0x002BD208
		public override IAllyReason.ReplaceTarget Replace
		{
			get
			{
				return IAllyReason.ReplaceTarget.Type;
			}
		}

		// Token: 0x06007502 RID: 29954 RVA: 0x002BF00B File Offset: 0x002BD20B
		public override void Initialize(GameObject Actor, GameObject Source, AllegianceSet Set)
		{
			base.Initialize(Actor, Source, Set);
		}

		// Token: 0x06007503 RID: 29955 RVA: 0x002BF016 File Offset: 0x002BD216
		public override string GetText(GameObject Actor)
		{
			return "I fell in love with " + this.Name + ".";
		}
	}
}
