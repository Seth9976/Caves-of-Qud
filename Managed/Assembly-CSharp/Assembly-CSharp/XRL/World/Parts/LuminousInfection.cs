using System;
using XRL.Rules;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02001034 RID: 4148
	[Serializable]
	public class LuminousInfection : IPart
	{
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600A537 RID: 42295 RVA: 0x003C684E File Offset: 0x003C4A4E
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::LuminousInfection";
			}
		}

		// Token: 0x0600A538 RID: 42296 RVA: 0x003C6865 File Offset: 0x003C4A65
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600A539 RID: 42297 RVA: 0x003C6868 File Offset: 0x003C4A68
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == OnDestroyObjectEvent.ID;
		}

		// Token: 0x0600A53A RID: 42298 RVA: 0x003C6896 File Offset: 0x003C4A96
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.ShroomCounter--;
			if (this.ShroomCounter < 0)
			{
				this.TryGrowMushroom();
				this.ShroomCounter = Stat.Random(300, 500);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A53B RID: 42299 RVA: 0x003C68D4 File Offset: 0x003C4AD4
		public override bool HandleEvent(EquippedEvent E)
		{
			GameObject parentObject = this.ParentObject;
			BodyPart bodyPart = ((parentObject != null) ? parentObject.EquippedOn() : null);
			if (bodyPart != null)
			{
				BodyPart bodyPart2 = bodyPart;
				string text = "Icy Outcrop";
				int num = 0;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				string text5 = null;
				string managerID = this.ManagerID;
				bool? flag = new bool?(true);
				string[] array = new string[] { "Feet", "Roots" };
				bodyPart2.AddPartAt(text, num, text2, text3, text4, text5, managerID, null, null, null, null, null, null, null, flag, null, null, null, null, null, "Icy Outcrop", array, true);
				this.TryGrowMushroom();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A53C RID: 42300 RVA: 0x003C69BA File Offset: 0x003C4BBA
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.RemoveBodyPartsByManager(this.ManagerID, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A53D RID: 42301 RVA: 0x003C69D6 File Offset: 0x003C4BD6
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				GameObject equipped = parentObject.Equipped;
				if (equipped != null)
				{
					equipped.RemoveBodyPartsByManager(this.ManagerID, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A53E RID: 42302 RVA: 0x003C6A04 File Offset: 0x003C4C04
		public void TryGrowMushroom()
		{
			GameObject parentObject = this.ParentObject;
			BodyPart bodyPart;
			if (parentObject == null)
			{
				bodyPart = null;
			}
			else
			{
				GameObject equipped = parentObject.Equipped;
				if (equipped == null)
				{
					bodyPart = null;
				}
				else
				{
					Body body = equipped.Body;
					bodyPart = ((body != null) ? body.FindEquippedItem(this.ParentObject) : null);
				}
			}
			BodyPart bodyPart2 = bodyPart;
			if (bodyPart2 == null)
			{
				return;
			}
			try
			{
				BodyPart bodyPart3 = bodyPart2.FindByManager(this.ManagerID);
				if (bodyPart3 != null && bodyPart3.Equipped == null)
				{
					GameObject gameObject = GameObject.CreateUnmodified("Hoarshroom");
					bodyPart3.Equip(gameObject, new int?(0), true, false, false, true);
					if (bodyPart3.Equipped == gameObject)
					{
						gameObject.RequirePart<Cursed>(false);
						gameObject.RequirePart<RemoveCursedOnUnequip>(false);
						if (this.ParentObject.Equipped.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You sprout a {{C|luminous hoarshroom}}.", null, true);
						}
					}
					else
					{
						gameObject.Obliterate(null, false, null);
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("TryGrowMushroom", ex, "game_exception");
			}
		}

		// Token: 0x04004452 RID: 17490
		public int ShroomCounter = Stat.Random(300, 500);
	}
}
