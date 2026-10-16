using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000F94 RID: 3988
	[Serializable]
	public class Forcefield : IPart
	{
		// Token: 0x06009E34 RID: 40500 RVA: 0x0039472C File Offset: 0x0039292C
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
			if (this.AllowPassage == null)
			{
				Writer.Write(0);
			}
			else
			{
				Writer.Write(1);
				Writer.WriteGameObjectList(this.AllowPassage);
			}
			Writer.Write(this.AllowFactions);
		}

		// Token: 0x06009E35 RID: 40501 RVA: 0x00394766 File Offset: 0x00392966
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			if (Reader.ReadInt32() == 1)
			{
				this.AllowPassage = new List<GameObject>(1);
				Reader.ReadGameObjectList(this.AllowPassage, null);
			}
			this.AllowFactions = Reader.ReadList<string>();
		}

		// Token: 0x06009E36 RID: 40502 RVA: 0x0039479E File Offset: 0x0039299E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforePhysicsRejectObjectEntringCell");
			Object.ModIntProperty("Electromagnetic", 1, false);
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E37 RID: 40503 RVA: 0x003947C4 File Offset: 0x003929C4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BlocksRadarEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<OkayToDamageEvent>.ID || ID == PooledEvent<RealityStabilizeEvent>.ID || ID == PooledEvent<ShouldAttackToReachTargetEvent>.ID || (ID == TookDamageEvent.ID && this.MovesWithOwner);
		}

		// Token: 0x06009E38 RID: 40504 RVA: 0x00394815 File Offset: 0x00392A15
		public override bool HandleEvent(ShouldAttackToReachTargetEvent E)
		{
			if (E.Object == this.ParentObject && E.Actor != this.Creator && !this.CanPass(E.Actor))
			{
				E.ShouldAttack = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E39 RID: 40505 RVA: 0x00394850 File Offset: 0x00392A50
		public override bool HandleEvent(OkayToDamageEvent E)
		{
			return (!this.MovesWithOwner || !GameObject.Validate(ref this.Creator) || this.ParentObject.DistanceTo(this.Creator) > 1 || !GameObject.Validate(E.Actor) || this.Creator.IsHostileTowards(E.Actor) || !this.Creator.FireEvent(Event.New("CanBeAngeredByPropertyCrime", "Attacker", E.Actor, "Object", this.ParentObject))) && base.HandleEvent(E);
		}

		// Token: 0x06009E3A RID: 40506 RVA: 0x003948DC File Offset: 0x00392ADC
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (this.MovesWithOwner && E.Object == this.ParentObject && E.Damage.Amount > 0 && GameObject.Validate(ref this.Creator) && this.ParentObject.DistanceTo(this.Creator) <= 1 && GameObject.Validate(E.Actor) && !this.Creator.IsHostileTowards(E.Actor) && this.Creator.FireEvent(Event.New("CanBeAngeredByPropertyCrime", "Attacker", E.Actor, "Object", this.ParentObject)))
			{
				this.Creator.AddOpinion<OpinionAttackProperty>(E.Actor, this.ParentObject, 1f);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E3B RID: 40507 RVA: 0x003949A7 File Offset: 0x00392BA7
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x06009E3C RID: 40508 RVA: 0x003949B4 File Offset: 0x00392BB4
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (E.Check(true))
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				Cell cell = ((currentCell != null) ? currentCell.GetRandomLocalAdjacentCell() : null);
				if (cell != null)
				{
					this.ParentObject.Discharge(cell, Stat.Random(1, 4), 0, "1d8", null, E.Effect.Owner, this.ParentObject, null, null, null, null, null, 0, false, false, false);
				}
				this.ParentObject.TileParticleBlip("items/sw_quills.bmp", "&B", "K", 10, false, false, false, 0L);
				base.DidX("collapse", "under the pressure of normality", null, null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
				this.ParentObject.Destroy(null, false, false, null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E3D RID: 40509 RVA: 0x00394A7B File Offset: 0x00392C7B
		public override bool HandleEvent(BlocksRadarEvent E)
		{
			return false;
		}

		// Token: 0x06009E3E RID: 40510 RVA: 0x00394A7E File Offset: 0x00392C7E
		public void AddAllowPassage(GameObject obj)
		{
			if (this.AllowPassage == null)
			{
				this.AllowPassage = new List<GameObject>();
			}
			if (!this.AllowPassage.Contains(obj))
			{
				this.AllowPassage.Add(obj);
			}
		}

		// Token: 0x06009E3F RID: 40511 RVA: 0x00394AAD File Offset: 0x00392CAD
		public void RemoveAllowPassage(GameObject obj)
		{
			if (this.AllowPassage != null)
			{
				this.AllowPassage.Remove(obj);
				if (this.AllowPassage.Count == 0)
				{
					this.AllowPassage = null;
				}
			}
		}

		// Token: 0x06009E40 RID: 40512 RVA: 0x00394AD8 File Offset: 0x00392CD8
		public void AddAllowPassage(string Faction)
		{
			if (this.AllowFactions == null)
			{
				this.AllowFactions = new List<string>(1);
			}
			this.AllowFactions.AddIfNot(Faction, new Func<string, bool>(this.AllowFactions.Contains));
		}

		// Token: 0x06009E41 RID: 40513 RVA: 0x00394B0C File Offset: 0x00392D0C
		public void RemoveAllowPassage(string Faction)
		{
			if (this.AllowFactions == null)
			{
				return;
			}
			this.AllowFactions.Remove(Faction);
			if (this.AllowFactions.Count == 0)
			{
				this.AllowFactions = null;
			}
		}

		// Token: 0x06009E42 RID: 40514 RVA: 0x00394B38 File Offset: 0x00392D38
		public bool CanPass(GameObject Object)
		{
			GameObject.Validate(ref this.Creator);
			if (Object == null)
			{
				return false;
			}
			if (Object.HasTagOrProperty("IgnoresForceWall"))
			{
				return true;
			}
			if (Object.HasTagOrProperty("ForcefieldNullifier"))
			{
				return true;
			}
			if (this.MovesWithOwner && Object == this.Creator)
			{
				return true;
			}
			if (this.Unwalkable)
			{
				return false;
			}
			if (this.AllowPassage != null && this.AllowPassage.Contains(Object))
			{
				return true;
			}
			if (this.AllowFactions != null && Object.Brain != null)
			{
				foreach (string text in this.AllowFactions)
				{
					if (text == "Player")
					{
						if (Object.IsPlayerControlled())
						{
							return true;
						}
					}
					else if (Object.GetAllegianceLevel(text) >= Brain.AllegianceLevel.Member)
					{
						return true;
					}
				}
			}
			return (!this.AllowPassageTag.IsNullOrEmpty() && Object.HasTagOrStringProperty(this.AllowPassageTag)) || (!this.RejectOwner && Object == this.Creator);
		}

		// Token: 0x06009E43 RID: 40515 RVA: 0x00394C54 File Offset: 0x00392E54
		public bool CanMissilePassFrom(GameObject Object, GameObject Projectile = null)
		{
			GameObject.Validate(ref this.Creator);
			if (Object == null)
			{
				return false;
			}
			if (Projectile != null)
			{
				if (Projectile.HasTagOrProperty("IgnoresForceWall"))
				{
					return true;
				}
				if (CanMissilePassForcefieldEvent.Check(Object, Projectile))
				{
					return true;
				}
			}
			if (this.MissileOpaque)
			{
				return false;
			}
			if (this.AllowPassage != null && this.AllowPassage.Contains(Object))
			{
				return true;
			}
			if (this.AllowFactions != null && Object.Brain != null)
			{
				foreach (string text in this.AllowFactions)
				{
					if (text == "Player")
					{
						if (Object.IsPlayerControlled())
						{
							return true;
						}
					}
					else if (Object.GetAllegianceLevel(text) >= Brain.AllegianceLevel.Member)
					{
						return true;
					}
				}
			}
			return (!this.AllowPassageTag.IsNullOrEmpty() && Object.HasTagOrStringProperty(this.AllowPassageTag)) || (!this.RejectOwner && Object == this.Creator);
		}

		// Token: 0x06009E44 RID: 40516 RVA: 0x00394D5C File Offset: 0x00392F5C
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "BeforePhysicsRejectObjectEntringCell") || !this.CanPass(E.GetGameObjectParameter("Object"))) && base.FireEvent(E);
		}

		// Token: 0x0400416C RID: 16748
		public bool RejectOwner = true;

		// Token: 0x0400416D RID: 16749
		public GameObject Creator;

		// Token: 0x0400416E RID: 16750
		[NonSerialized]
		public List<GameObject> AllowPassage;

		// Token: 0x0400416F RID: 16751
		[NonSerialized]
		public List<string> AllowFactions;

		// Token: 0x04004170 RID: 16752
		public string AllowPassageTag;

		// Token: 0x04004171 RID: 16753
		public bool Unwalkable;

		// Token: 0x04004172 RID: 16754
		public bool MissileOpaque;

		// Token: 0x04004173 RID: 16755
		public bool MovesWithOwner;
	}
}
