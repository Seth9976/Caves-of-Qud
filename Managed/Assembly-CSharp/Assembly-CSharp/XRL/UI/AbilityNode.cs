using System;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x020014C9 RID: 5321
	public class AbilityNode : ConsoleTreeNode<AbilityNode>
	{
		// Token: 0x0600D083 RID: 53379 RVA: 0x004D16E0 File Offset: 0x004CF8E0
		public AbilityNode(ActivatedAbilityEntry Ability = null, string Category = "", AbilityNode ParentNode = null)
			: base(Category, true, ParentNode)
		{
			this.Ability = Ability;
		}

		// Token: 0x04005236 RID: 21046
		public ActivatedAbilityEntry Ability;
	}
}
