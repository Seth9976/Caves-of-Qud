using System;
using XRL.Messages;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001422 RID: 5154
	[Serializable]
	public class QuantumFugue : TemporalFugue
	{
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x0600C8BE RID: 51390 RVA: 0x004924DA File Offset: 0x004906DA
		public override string CommandID
		{
			get
			{
				return "CommandQuantumFugue";
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x0600C8BF RID: 51391 RVA: 0x004924E1 File Offset: 0x004906E1
		public override string AbilityClass
		{
			get
			{
				return "Mutations";
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x0600C8C0 RID: 51392 RVA: 0x004924E8 File Offset: 0x004906E8
		public override bool IsRealityDistortionBased
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600C8C1 RID: 51393 RVA: 0x004924EB File Offset: 0x004906EB
		public override bool AffectedByWillpower
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600C8C2 RID: 51394 RVA: 0x004924EE File Offset: 0x004906EE
		public override int AIMaxDistance
		{
			get
			{
				return 9999999;
			}
		}

		// Token: 0x0600C8C4 RID: 51396 RVA: 0x00492505 File Offset: 0x00490705
		public override int GetCooldown(int Level)
		{
			return 100;
		}

		// Token: 0x0600C8C5 RID: 51397 RVA: 0x00492509 File Offset: 0x00490709
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDieEvent.ID;
		}

		// Token: 0x0600C8C6 RID: 51398 RVA: 0x0049251F File Offset: 0x0049071F
		public override IPart DeepCopy(GameObject Parent)
		{
			QuantumFugue quantumFugue = (QuantumFugue)base.DeepCopy(Parent);
			quantumFugue.EndTick = -1L;
			return quantumFugue;
		}

		// Token: 0x0600C8C7 RID: 51399 RVA: 0x00492538 File Offset: 0x00490738
		public override bool HandleEvent(BeforeDieEvent E)
		{
			if (this.EndTick >= 0L && !this.ParentObject.HasStringProperty("FugueCopy"))
			{
				GameObject bestCopyIn = this.GetBestCopyIn(this.ParentObject.CurrentZone, false);
				if (bestCopyIn != this.ParentObject)
				{
					this.Inhabit(bestCopyIn);
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8C8 RID: 51400 RVA: 0x00492590 File Offset: 0x00490790
		public override bool PerformTemporalFugue(IEvent TriggeringEvent = null)
		{
			if (this.EndTick >= 0L)
			{
				return false;
			}
			int temporalFugueDuration = TemporalFugue.GetTemporalFugueDuration(base.Level);
			GameObject parentObject = this.ParentObject;
			GameObject parentObject2 = this.ParentObject;
			GameObject gameObject = null;
			bool flag = false;
			int? num = new int?(temporalFugueDuration + 1);
			if (TemporalFugue.PerformTemporalFugue(parentObject, parentObject2, gameObject, this, TriggeringEvent, flag, this.IsRealityDistortionBased, num, null, 0, "Temporal Fugue", null, null, null, null))
			{
				this.EndTick = The.Game.TimeTicks + (long)temporalFugueDuration;
			}
			return false;
		}

		// Token: 0x0600C8C9 RID: 51401 RVA: 0x00492604 File Offset: 0x00490804
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600C8CA RID: 51402 RVA: 0x00492607 File Offset: 0x00490807
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.EndTick < 0L || TimeTick < this.EndTick)
			{
				return;
			}
			this.EndTick = -1L;
			this.Cohere(this.GetAnyBasisZone());
		}

		// Token: 0x0600C8CB RID: 51403 RVA: 0x00492634 File Offset: 0x00490834
		public void Inhabit(GameObject Copy)
		{
			try
			{
				MessageQueue.Suppress = true;
				this.ParentObject.SystemMoveTo(Copy.CurrentCell, null, true, true, false, false);
				this.ParentObject.hitpoints = Copy.hitpoints;
				this.ParentObject.RemoveEffectsOfPartialType(67108864, null, true);
				if (Copy.AnyEffects())
				{
					foreach (Effect effect in Copy.Effects)
					{
						if (effect.IsOfType(67108864) && !effect.IsOfType(128))
						{
							this.ParentObject.Effects.Add(effect.DeepCopy(this.ParentObject));
						}
					}
				}
				if (Sidebar.CurrentTarget == Copy)
				{
					Sidebar.CurrentTarget = this.ParentObject;
				}
				Copy.GetPart<Temporary>().Expire(true);
			}
			finally
			{
				MessageQueue.Suppress = false;
			}
		}

		// Token: 0x0600C8CC RID: 51404 RVA: 0x0049273C File Offset: 0x0049093C
		public GameObject GetBestCopyIn(Zone Zone, bool Expire = false)
		{
			string id = this.ParentObject.ID;
			GameObject gameObject = this.ParentObject;
			int num = this.ParentObject.hitpoints;
			foreach (GameObject gameObject2 in Zone.IterateObjects())
			{
				string stringProperty = gameObject2.GetStringProperty("FugueCopy", null);
				if (stringProperty != null && !(stringProperty != id))
				{
					int hitpoints = gameObject2.hitpoints;
					if (hitpoints <= num)
					{
						if (Expire)
						{
							gameObject2.GetPart<Temporary>().Expire(false);
						}
					}
					else
					{
						gameObject = gameObject2;
						num = hitpoints;
					}
				}
			}
			return gameObject;
		}

		// Token: 0x0600C8CD RID: 51405 RVA: 0x004927D0 File Offset: 0x004909D0
		public void Cohere(Zone Zone)
		{
			GameObject bestCopyIn = this.GetBestCopyIn(Zone, true);
			if (bestCopyIn != this.ParentObject)
			{
				this.Inhabit(bestCopyIn);
			}
			if (this.ParentObject.CurrentCell != null)
			{
				base.DidX("cohere", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
				this.ParentObject.FugueVFX();
			}
		}

		// Token: 0x04004E98 RID: 20120
		public long EndTick = -1L;
	}
}
