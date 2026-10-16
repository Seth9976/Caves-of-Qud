using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000EA1 RID: 3745
	[Serializable]
	public class DroidScramblerWeakness : IPart
	{
		// Token: 0x06009679 RID: 38521 RVA: 0x00368606 File Offset: 0x00366806
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterDieEvent.ID || ID == PooledEvent<BeforeSetFeelingEvent>.ID;
		}

		// Token: 0x0600967A RID: 38522 RVA: 0x00368624 File Offset: 0x00366824
		public override bool HandleEvent(AfterDieEvent E)
		{
			if (this.IsScrambler(E.Killer))
			{
				this.ParentObject.SetIntProperty("NoXP", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600967B RID: 38523 RVA: 0x0036864E File Offset: 0x0036684E
		public override bool HandleEvent(GetFeelingEvent E)
		{
			if (E.Feeling < 0)
			{
				DroidScrambler.CheckScramblingFactions(The.Game.TimeTicks);
				if (this.IsScrambler(E.Target))
				{
					E.Feeling = 0;
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600967C RID: 38524 RVA: 0x00368688 File Offset: 0x00366888
		public override bool HandleEvent(BeforeSetFeelingEvent E)
		{
			if (E.Feeling < 0 && this.IsScrambler(E.Target))
			{
				if (!E.Target.IsPlayer() && E.Target.Target == this.ParentObject)
				{
					E.Target.StopFighting(false);
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600967D RID: 38525 RVA: 0x003686E4 File Offset: 0x003668E4
		public bool IsScrambler(GameObject Object)
		{
			string text;
			if (Object == null)
			{
				text = null;
			}
			else
			{
				Zone currentZone = Object.CurrentZone;
				text = ((currentZone != null) ? currentZone.ZoneID : null);
			}
			string text2 = text;
			List<string> list;
			if (text2 != null && DroidScrambler.Scrambled.TryGetValue(text2, out list) && list.Contains(DroidScrambler.GetScrambledFaction(Object)))
			{
				return true;
			}
			GameObject parentObject = this.ParentObject;
			string text3;
			if (parentObject == null)
			{
				text3 = null;
			}
			else
			{
				Zone currentZone2 = parentObject.CurrentZone;
				text3 = ((currentZone2 != null) ? currentZone2.ZoneID : null);
			}
			string text4 = text3;
			return text4 != null && text2 != text4 && DroidScrambler.Scrambled.TryGetValue(text4, out list) && list.Contains(DroidScrambler.GetScrambledFaction(Object));
		}

		// Token: 0x0600967E RID: 38526 RVA: 0x00368772 File Offset: 0x00366972
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600967F RID: 38527 RVA: 0x00368775 File Offset: 0x00366975
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AIBeginKill");
			Registrar.Register(PooledEvent<GetFeelingEvent>.ID, 0, false);
			base.Register(Object, Registrar);
		}

		// Token: 0x06009680 RID: 38528 RVA: 0x00368798 File Offset: 0x00366998
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AIBeginKill")
			{
				GameObject target = this.ParentObject.Target;
				if (target != null)
				{
					DroidScrambler.CheckScramblingFactions(The.Game.TimeTicks);
					if (this.IsScrambler(target))
					{
						this.ParentObject.StopFighting(false);
						return false;
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
