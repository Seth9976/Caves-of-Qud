using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001010 RID: 4112
	[Serializable]
	public class CyclopeanPrism : IPart
	{
		// Token: 0x0600A3C5 RID: 41925 RVA: 0x003BD386 File Offset: 0x003BB586
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A3C6 RID: 41926 RVA: 0x003BD38C File Offset: 0x003BB58C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<AfterGameLoadedEvent>.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GenericDeepNotifyEvent>.ID || ID == PooledEvent<GetPrecognitionRestoreGameStateEvent>.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == PooledEvent<RealityStabilizeEvent>.ID || ID == PooledEvent<ReplaceInContextEvent>.ID;
		}

		// Token: 0x0600A3C7 RID: 41927 RVA: 0x003BD3ED File Offset: 0x003BB5ED
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (this.ParentObject.Equipped != null && E.Check(false))
			{
				this.ParentObject.Equipped.ApplyEffect(new Dazed(1), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3C8 RID: 41928 RVA: 0x003BD424 File Offset: 0x003BB624
		public override bool HandleEvent(AfterGameLoadedEvent E)
		{
			string precognitionTransferKey = this.GetPrecognitionTransferKey();
			string text;
			if (The.Game.StringGameState.TryGetValue(precognitionTransferKey, out text))
			{
				The.Game.RemoveStringGameState(precognitionTransferKey);
				if (this.ParentObject.Equipped == null)
				{
					this.PtohAnnoyed(The.ZoneManager.FindObjectByID(text));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3C9 RID: 41929 RVA: 0x003BD47C File Offset: 0x003BB67C
		public override bool HandleEvent(EquippedEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				Achievement.WIELD_PRISM.Unlock();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3CA RID: 41930 RVA: 0x003BD49C File Offset: 0x003BB69C
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.ResetPrism();
			if (!E.Actor.IsDying)
			{
				this.PtohAnnoyed(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3CB RID: 41931 RVA: 0x003BD4C4 File Offset: 0x003BB6C4
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (E.Object == this.ParentObject.Equipped)
			{
				this.TurnCount++;
				if (E.Object == null || !E.Object.HasStat("Ego") || !E.Object.HasStat("Willpower"))
				{
					this.ResetPrism();
				}
				else
				{
					int num = E.Object.Stat("Ego", 0);
					if (num <= 15)
					{
						this.ParentObject.Render.DisplayName = "{{K|amaranthine}} prism";
						this.ParentObject.SetStringProperty("EquipmentFrameColors", "KKKK", false);
					}
					else if (num <= 19)
					{
						this.ParentObject.Render.DisplayName = "{{K|amara{{y|n}}thine}} prism";
						this.ParentObject.SetStringProperty("EquipmentFrameColors", "yKKy", false);
					}
					else if (num <= 23)
					{
						this.ParentObject.Render.DisplayName = "{{K|amar{{y|a{{Y|n}}t}}hine}} prism";
						this.ParentObject.SetStringProperty("EquipmentFrameColors", "yKyY", false);
					}
					else if (num <= 27)
					{
						this.ParentObject.Render.DisplayName = "{{K|am{{y|ar{{Y|a{{R|n}}t}}hi}}ne}} prism";
						this.ParentObject.SetStringProperty("EquipmentFrameColors", "ryYK", false);
					}
					else if (num <= 31)
					{
						this.ParentObject.Render.DisplayName = "{{y|am{{Y|a{{y|r{{r|a{{R|n}}t}}h}}i}}ne}} prism";
						this.ParentObject.SetStringProperty("EquipmentFrameColors", "yYrR", false);
					}
					else
					{
						this.ParentObject.Render.DisplayName = "{{r|a{{R|m{{Y|a{{y|r{{r|a{{R|n}}t}}h}}i}}n}}e}} prism";
						this.ParentObject.SetStringProperty("EquipmentFrameColors", "yRYr", false);
					}
					if (this.TurnCount >= 4800)
					{
						if (E.Object.Stat("Willpower", 0) <= 1)
						{
							if (E.Object.IsPlayer())
							{
								Achievement.WANDER_DARKLING.Unlock();
							}
							E.Object.Die(this.ParentObject, null, "You had a dream, which was not all a dream. The bright sun was extinguish'd, and the stars did wander darkling in the eternal space.", this.ParentObject.It + " had a dream, which was not all a dream. The bright sun was extinguish'd, and the stars did wander darkling in the eternal space.", false, null, null, false, false, null, null, null);
						}
						Armor part = this.ParentObject.GetPart<Armor>();
						part.Ego++;
						part.Willpower--;
						part.UpdateStatShifts(null);
						this.TurnCount = 0;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3CC RID: 41932 RVA: 0x003BD718 File Offset: 0x003BB918
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.ResetPrism();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3CD RID: 41933 RVA: 0x003BD727 File Offset: 0x003BB927
		public override bool HandleEvent(GetPrecognitionRestoreGameStateEvent E)
		{
			if (this.ParentObject.Equipped == E.Object)
			{
				E.Set(this.GetPrecognitionTransferKey(), E.Object.ID);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3CE RID: 41934 RVA: 0x003BD75A File Offset: 0x003BB95A
		public override bool HandleEvent(ReplaceInContextEvent E)
		{
			this.PtohAnnoyed(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3CF RID: 41935 RVA: 0x003BD76A File Offset: 0x003BB96A
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A3D0 RID: 41936 RVA: 0x003BD770 File Offset: 0x003BB970
		public void PtohAnnoyed(GameObject who = null)
		{
			if (who == null)
			{
				who = this.ParentObject.Holder ?? The.Player;
				if (who == null)
				{
					return;
				}
			}
			if (who.IsPlayer())
			{
				Popup.Show("From across the psychic sea, you feel the glare of unseen eyes. Someone is disappointed in you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				The.Game.PlayerReputation.Modify("Entropic", -100, "PtohDispleasure", null, null, false, false);
				Achievement.DISAPPOINT_HEB.Unlock();
			}
			Cell currentCell = who.CurrentCell;
			if (currentCell != null && currentCell.ParentZone != null)
			{
				for (int i = Stat.Random(2, 5); i >= 0; i--)
				{
					Cell cell = null;
					int num = 0;
					while (++num < 100)
					{
						cell = currentCell.ParentZone.GetRandomCell(0);
						int num2 = cell.PathDistanceTo(currentCell);
						if (num2 >= 4 && num2 <= 12 && !cell.HasObjectWithPart("Brain") && !cell.HasObjectWithPart("SpaceTimeVortex") && !cell.HasObjectWithPart("SpaceTimeRift") && IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cell, null, this.ParentObject, null, 0))
						{
							break;
						}
					}
					if (num < 100 && cell != null)
					{
						cell.AddObject("Space-Time Vortex", null, null, null, null);
					}
				}
			}
		}

		// Token: 0x0600A3D1 RID: 41937 RVA: 0x003BD891 File Offset: 0x003BBA91
		public void ResetPrism()
		{
			Armor part = this.ParentObject.GetPart<Armor>();
			part.Ego = 1;
			part.Willpower = -1;
			this.ParentObject.Render.DisplayName = "{{K|amaranthine}} prism";
		}

		// Token: 0x0600A3D2 RID: 41938 RVA: 0x003BD8C0 File Offset: 0x003BBAC0
		private string GetPrecognitionTransferKey()
		{
			return "AmaranthinePrism" + this.ParentObject.ID + "EquippedAtPrecognitionRestore";
		}

		// Token: 0x040043CD RID: 17357
		public int TurnCount;
	}
}
