using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x0200115E RID: 4446
	[Serializable]
	public class ModQuantumReverb : IModification
	{
		// Token: 0x0600B0C4 RID: 45252 RVA: 0x0040FF3A File Offset: 0x0040E13A
		public ModQuantumReverb()
		{
		}

		// Token: 0x0600B0C5 RID: 45253 RVA: 0x0040FF42 File Offset: 0x0040E142
		public ModQuantumReverb(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B0C6 RID: 45254 RVA: 0x0040FF4B File Offset: 0x0040E14B
		public override void Configure()
		{
			base.IsTechScannable = true;
			this.WorksOnSelf = true;
			this.NameForStatus = "QuantumReverb";
		}

		// Token: 0x0600B0C7 RID: 45255 RVA: 0x0040FF68 File Offset: 0x0040E168
		public static GameObject CreateHologramOf(GameObject Object)
		{
			GameObject gameObject = GameObject.Create("Hologram Distraction");
			gameObject.Render.Tile = Object.Render.Tile;
			gameObject.Render.RenderString = Object.Render.RenderString;
			gameObject.Render.DisplayName = "hologram of " + Object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false);
			gameObject.GetPart<Description>().Short = "Light stammers in parallax to form the image of an object. " + Object.GetPart<Description>().Short;
			gameObject.RemovePart<Distraction>();
			return gameObject;
		}

		// Token: 0x0600B0C8 RID: 45256 RVA: 0x0041000C File Offset: 0x0040E20C
		public void PlaceHologram(GameObject Owner, Cell Target)
		{
			GameObject gameObject = ModQuantumReverb.CreateHologramOf(Owner);
			gameObject.RequirePart<Inventory>(false);
			Body body = gameObject.RequirePart<Body>(false);
			body.built = false;
			body._Body = Owner.Body._Body.DeepCopy(gameObject, body, null, null, false);
			body.built = true;
			body.CategorizeAll(22);
			body.UpdateBodyParts(0);
			GameObject gameObject2 = this.ParentObject.DeepCopy(false, false, null);
			BodyPart firstPart = body.GetFirstPart("Missile Weapon");
			gameObject2.Physics._Equipped = gameObject;
			firstPart.DoEquip(gameObject2, false, false);
			AIShootRound aishootRound = gameObject.RequirePart<AIShootRound>(false);
			aishootRound.SetTarget(Target);
			aishootRound.Cooldown = 1;
			Temporary.AddHierarchically(gameObject, 4, null, null, false);
			Owner.CurrentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			gameObject.Physics.DidX("appear", null, null, null, null, null, null, false, true, null, null, false, false, false, false, false, null);
		}

		// Token: 0x0600B0C9 RID: 45257 RVA: 0x004100F1 File Offset: 0x0040E2F1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B0CA RID: 45258 RVA: 0x0041010F File Offset: 0x0040E30F
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddWithClause("{{quantumreverb|quantum reverb}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0CB RID: 45259 RVA: 0x00410138 File Offset: 0x0040E338
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModQuantumReverb.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B0CC RID: 45260 RVA: 0x00410158 File Offset: 0x0040E358
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B0CD RID: 45261 RVA: 0x0041015B File Offset: 0x0040E35B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandFireMissile");
		}

		// Token: 0x0600B0CE RID: 45262 RVA: 0x00410168 File Offset: 0x0040E368
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandFireMissile")
			{
				GameObject gameObject = E.GetGameObjectParameter("Actor") ?? E.GetGameObjectParameter("Owner");
				Cell cell = E.GetParameter("TargetCell") as Cell;
				if (this.ParentObject.IsTemporary || !GameObject.Validate(gameObject) || gameObject.IsTemporary || cell == null)
				{
					return true;
				}
				this.PlaceHologram(gameObject, cell);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B0CF RID: 45263 RVA: 0x004101E4 File Offset: 0x0040E3E4
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexity(3, 3, null);
		}

		// Token: 0x0600B0D0 RID: 45264 RVA: 0x004101EF File Offset: 0x0040E3EF
		public static string GetDescription(int Tier)
		{
			return "Quantum reverb: When fired, this weapon creates a hologram of its wielder who continues to fire along the same path.";
		}
	}
}
