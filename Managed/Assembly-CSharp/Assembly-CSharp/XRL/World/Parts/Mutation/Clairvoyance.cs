using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E3 RID: 5091
	[Serializable]
	public class Clairvoyance : BaseMutation
	{
		// Token: 0x0600C4B8 RID: 50360 RVA: 0x004799B5 File Offset: 0x00477BB5
		public Clairvoyance()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C4B9 RID: 50361 RVA: 0x004799E8 File Offset: 0x00477BE8
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write(this.Cells.Count);
			foreach (VisCell visCell in this.Cells)
			{
				Writer.Write(visCell.C.ParentZone.ZoneID);
				Writer.Write(visCell.C.X);
				Writer.Write(visCell.C.Y);
				Writer.Write(visCell.Turns);
			}
			base.Write(Basis, Writer);
		}

		// Token: 0x0600C4BA RID: 50362 RVA: 0x00479A94 File Offset: 0x00477C94
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			int num = Reader.ReadInt32();
			this.Cells.Clear();
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadString();
				int num2 = Reader.ReadInt32();
				int num3 = Reader.ReadInt32();
				int num4 = Reader.ReadInt32();
				Cell cell = XRLCore.Core.Game.ZoneManager.GetZone(text).GetCell(num2, num3);
				this.Cells.Add(new VisCell(cell, num4));
			}
			base.Read(Basis, Reader);
		}

		// Token: 0x0600C4BB RID: 50363 RVA: 0x00479B18 File Offset: 0x00477D18
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C4BC RID: 50364 RVA: 0x00479B3E File Offset: 0x00477D3E
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("stars", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4BD RID: 50365 RVA: 0x00479B64 File Offset: 0x00477D64
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			foreach (VisCell visCell in this.Cells)
			{
				visCell.C.ParentZone.AddLight(visCell.C.X, visCell.C.Y, 0, LightLevel.Omniscient, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4BE RID: 50366 RVA: 0x00479BE4 File Offset: 0x00477DE4
		public override bool HandleEvent(EndTurnEvent E)
		{
			List<VisCell> list = null;
			foreach (VisCell visCell in this.Cells)
			{
				visCell.Turns--;
				if (visCell.Turns <= 0)
				{
					if (list == null)
					{
						list = new List<VisCell>();
					}
					list.Add(visCell);
				}
			}
			if (list != null)
			{
				foreach (VisCell visCell2 in list)
				{
					this.Cells.Remove(visCell2);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C4BF RID: 50367 RVA: 0x00479CA8 File Offset: 0x00477EA8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandClairvoyance");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C4C0 RID: 50368 RVA: 0x00479CC0 File Offset: 0x00477EC0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandClairvoyance")
			{
				if (this.RealityDistortionBased && !this.ParentObject.IsRealityDistortionUsable(null, null))
				{
					RealityStabilized.ShowGenericInterdictMessage(this.ParentObject, null);
					return true;
				}
				int num = 3 + base.Level;
				List<Cell> list;
				if (base.Level < 15)
				{
					list = base.PickCircle(num, 80, false, AllowVis.Any, "Clairvoyance");
					if (list == null)
					{
						return true;
					}
					if (list.Count == 0)
					{
						return true;
					}
					if (this.RealityDistortionBased)
					{
						Event @event = Event.New("InitiateRealityDistortionTransit", "Object", this.ParentObject, "Mutation", this, "Cell", list[0]);
						if (!this.ParentObject.FireEvent(@event, E) || !list[0].FireEvent(@event, E))
						{
							return true;
						}
					}
				}
				else
				{
					list = this.ParentObject.CurrentCell.ParentZone.GetCells();
					if (this.RealityDistortionBased && !this.ParentObject.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", this.ParentObject, "Mutation", this)))
					{
						return true;
					}
				}
				IComponent<GameObject>.PlayUISound(this.Sound, 1f, false, SoundRequest.SoundEffectType.None);
				if (this.ParentObject.IsPlayer())
				{
					base.DidX("peer", "into another space", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				}
				base.UseEnergy(1000, "Mental Mutation Clairvoyance");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 100, null, null, false);
				Predicate<Cell> predicate;
				if (this.RealityDistortionBased)
				{
					predicate = (Cell C) => C != null && !C.OnWorldMap() && IComponent<GameObject>.CheckRealityDistortionAccessibility(null, C, this.ParentObject, null, this, 0);
				}
				else
				{
					predicate = (Cell C) => C != null && !C.ParentZone.IsWorldMap();
				}
				foreach (Cell cell in list)
				{
					if (predicate(cell))
					{
						this.Cells.Add(new VisCell(cell, 19 + base.Level));
						cell.SetExplored();
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C4C1 RID: 50369 RVA: 0x00479EE4 File Offset: 0x004780E4
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			bool flag = stats.mode.Contains("ability");
			if (Level < 15)
			{
				stats.Set("VisionRadius", 3 + Level, !flag, 0);
			}
			else
			{
				stats.Set("VisionRadius", "whole map", !flag, 0);
			}
			stats.Set("VisionDuration", string.Format("{0}", Level + 19), !flag, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 100, 0);
		}

		// Token: 0x0600C4C2 RID: 50370 RVA: 0x00479F6A File Offset: 0x0047816A
		public override bool ChangeLevel(int NewLevel)
		{
			bool flag = base.ChangeLevel(NewLevel);
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return flag;
		}

		// Token: 0x0600C4C3 RID: 50371 RVA: 0x00479F90 File Offset: 0x00478190
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Clairvoyance", "CommandClairvoyance", "Mental Mutations", null, "+", null, false, false, false, false, this.RealityDistortionBased, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C4C4 RID: 50372 RVA: 0x00479FF9 File Offset: 0x004781F9
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004DDD RID: 19933
		public bool RealityDistortionBased = true;

		// Token: 0x04004DDE RID: 19934
		public string Sound = "Sounds/Abilities/sfx_ability_mutation_mental_generic_activate";

		// Token: 0x04004DDF RID: 19935
		[NonSerialized]
		public List<VisCell> Cells = new List<VisCell>(64);
	}
}
