using System;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Skill;

namespace XRL.CharacterCreation
{
	// Token: 0x02000577 RID: 1399
	public class CustomChargenSandbox
	{
		// Token: 0x06003C68 RID: 15464 RVA: 0x001275B8 File Offset: 0x001257B8
		public void AddSkill(GameObject body, string Class)
		{
			BaseSkill baseSkill = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Parts.Skill." + Class, false, false, true)) as BaseSkill;
			body.GetPart<Skills>().AddSkill(baseSkill, null, null);
		}
	}
}
