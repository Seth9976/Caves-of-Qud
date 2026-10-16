using System;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Quests.GolemQuest;

namespace XRL.World.Units
{
	// Token: 0x020009A8 RID: 2472
	[Serializable]
	public class GameObjectBodyPartUnit : GameObjectUnit
	{
		// Token: 0x06006ABF RID: 27327 RVA: 0x002260D8 File Offset: 0x002242D8
		public override void Apply(GameObject Object)
		{
			Body body = Object.Body;
			BodyPart bodyPart = ((body != null) ? body.GetBody() : null);
			if (bodyPart == null)
			{
				return;
			}
			BodyPart bodyPart2;
			if (this.Type == "Random")
			{
				bodyPart2 = Object.RequirePart<Mutations>(false).AddChimericBodyPart(true, this.Manager, null);
			}
			else
			{
				BodyPart bodyPart3 = bodyPart;
				string type = this.Type;
				string manager = this.Manager;
				string insertAfter = this.InsertAfter;
				string orInsertBefore = this.OrInsertBefore;
				bodyPart2 = bodyPart3.AddPartAt(type, this.Laterality, null, null, null, null, manager, new int?((this.Category == -1) ? bodyPart.Category : this.Category), null, null, null, null, null, null, null, null, null, null, null, null, insertAfter, orInsertBefore, true);
			}
			if (this.Metachromed && bodyPart2 != null)
			{
				GolemQuestSystem golemQuestSystem = GolemQuestSystem.Get();
				string text;
				if (golemQuestSystem == null)
				{
					text = null;
				}
				else
				{
					GolemArmamentSelection armament = golemQuestSystem.Armament;
					text = ((armament != null) ? armament.GetFirstSkill() : null);
				}
				string text2 = text ?? GolemArmamentSelection.Skills.GetRandomElement<string>(null);
				if (!text2.IsNullOrEmpty())
				{
					bool flag = true;
					this.AddMetachrome(bodyPart2, text2, ref flag);
				}
			}
		}

		// Token: 0x06006AC0 RID: 27328 RVA: 0x00226240 File Offset: 0x00224440
		public void AddMetachrome(BodyPart Limb, string Skill, ref bool AddFinal)
		{
			GameObjectBlueprint gameObjectBlueprint;
			if (((AddFinal && Limb.Parts.IsNullOrEmpty<BodyPart>()) || !Limb.DefaultBehaviorBlueprint.IsNullOrEmpty()) && GameObjectMetachromeUnit.TryGetBlueprintFor(Limb.Type, Skill, out gameObjectBlueprint))
			{
				GameObjectMetachromeUnit.SetDefaultBehavior(Limb, gameObjectBlueprint);
				AddFinal = false;
			}
			foreach (BodyPart bodyPart in Limb.LoopSubparts())
			{
				this.AddMetachrome(bodyPart, Skill, ref AddFinal);
			}
		}

		// Token: 0x06006AC1 RID: 27329 RVA: 0x002262C8 File Offset: 0x002244C8
		public override void Remove(GameObject Object)
		{
			Body body = Object.Body;
			BodyPart bodyPart = ((body != null) ? body.GetBody() : null);
			if (bodyPart == null)
			{
				return;
			}
			if (this.Manager.IsNullOrEmpty())
			{
				BodyPart firstPart = bodyPart.GetFirstPart(this.Type, (this.Laterality == 0) ? 65535 : this.Laterality);
				bodyPart.RemovePart(firstPart, true);
				return;
			}
			Body body2 = Object.Body;
			if (body2 == null)
			{
				return;
			}
			body2.RemovePartsByManager(this.Manager, true);
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x0022633D File Offset: 0x0022453D
		public override void Reset()
		{
			base.Reset();
			this.Type = null;
			this.Manager = null;
			this.InsertAfter = null;
			this.OrInsertBefore = null;
			this.Category = -1;
			this.Laterality = 0;
			this.Metachromed = false;
		}

		// Token: 0x06006AC3 RID: 27331 RVA: 0x00226378 File Offset: 0x00224578
		public override string GetDescription(bool Inscription = false)
		{
			BodyPartType bodyPartType;
			if (Anatomies.BodyPartTypeTable.TryGetValue(this.Type, out bodyPartType))
			{
				return "Extra " + bodyPartType.Description + " slot";
			}
			return "Extra " + this.Type.ToLowerInvariant() + " slot";
		}

		// Token: 0x040030DD RID: 12509
		public string Type;

		// Token: 0x040030DE RID: 12510
		public string Manager;

		// Token: 0x040030DF RID: 12511
		public string InsertAfter;

		// Token: 0x040030E0 RID: 12512
		public string OrInsertBefore;

		// Token: 0x040030E1 RID: 12513
		public int Category = -1;

		// Token: 0x040030E2 RID: 12514
		public int Laterality;

		// Token: 0x040030E3 RID: 12515
		public bool Metachromed;
	}
}
