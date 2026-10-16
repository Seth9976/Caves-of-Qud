using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000CE0 RID: 3296
	[Serializable]
	public class ProceduralCookingEffect : Effect
	{
		// Token: 0x060082D0 RID: 33488 RVA: 0x002F87E2 File Offset: 0x002F69E2
		public ProceduralCookingEffect()
		{
			this.DisplayName = "{{W|metabolizing}}";
			this.Duration = 1;
		}

		// Token: 0x060082D1 RID: 33489 RVA: 0x002F8807 File Offset: 0x002F6A07
		public override int GetEffectType()
		{
			return 67108868;
		}

		// Token: 0x060082D2 RID: 33490 RVA: 0x002F880E File Offset: 0x002F6A0E
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060082D3 RID: 33491 RVA: 0x002F8811 File Offset: 0x002F6A11
		public override string GetDescription()
		{
			return "{{W|metabolizing}}";
		}

		// Token: 0x060082D4 RID: 33492 RVA: 0x002F8818 File Offset: 0x002F6A18
		public override string GetDetails()
		{
			return this.GetProceduralEffectDescription();
		}

		// Token: 0x060082D5 RID: 33493 RVA: 0x002F8820 File Offset: 0x002F6A20
		public virtual string GetTemplatedProceduralEffectDescription()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.units.Count > 0)
			{
				for (int i = 0; i < this.units.Count; i++)
				{
					stringBuilder.Append(this.units[i].GetTemplatedDescription()).Append("\n");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060082D6 RID: 33494 RVA: 0x002F8880 File Offset: 0x002F6A80
		public virtual string GetProceduralEffectDescription()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.units.Count > 0)
			{
				for (int i = 0; i < this.units.Count; i++)
				{
					stringBuilder.Append(this.units[i].GetDescription());
					stringBuilder.Append("\n");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060082D7 RID: 33495 RVA: 0x002F88E1 File Offset: 0x002F6AE1
		public void AddUnit(ProceduralCookingEffectUnit newUnit)
		{
			newUnit.parent = this;
			this.units.Add(newUnit);
		}

		// Token: 0x060082D8 RID: 33496 RVA: 0x002F88F8 File Offset: 0x002F6AF8
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			foreach (ProceduralCookingEffectUnit proceduralCookingEffectUnit in this.units)
			{
				proceduralCookingEffectUnit.parent = this;
			}
		}

		// Token: 0x060082D9 RID: 33497 RVA: 0x002F8954 File Offset: 0x002F6B54
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
		}

		// Token: 0x060082DA RID: 33498 RVA: 0x002F8960 File Offset: 0x002F6B60
		public virtual bool SameAs(ProceduralCookingEffect effect)
		{
			if (this.units.Count != effect.units.Count)
			{
				return false;
			}
			using (List<ProceduralCookingEffectUnit>.Enumerator enumerator = this.units.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ProceduralCookingEffectUnit unit = enumerator.Current;
					if (!effect.units.Any<ProceduralCookingEffectUnit>((ProceduralCookingEffectUnit e) => e.GetType() == unit.GetType()))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060082DB RID: 33499 RVA: 0x002F89F4 File Offset: 0x002F6BF4
		public override bool Apply(GameObject Object)
		{
			if (!this.init)
			{
				throw new Exception("You must call Init() before applying this to an object");
			}
			if (!this.bApplied)
			{
				this.StartTick = Calendar.TotalTimeTicks;
				this.bApplied = true;
				foreach (ProceduralCookingEffectUnit proceduralCookingEffectUnit in this.units)
				{
					proceduralCookingEffectUnit.Apply(Object, this);
				}
			}
			return true;
		}

		// Token: 0x060082DC RID: 33500 RVA: 0x002F8A74 File Offset: 0x002F6C74
		public override void Remove(GameObject Object)
		{
			if (this.bApplied)
			{
				this.bApplied = false;
				foreach (ProceduralCookingEffectUnit proceduralCookingEffectUnit in this.units)
				{
					proceduralCookingEffectUnit.Remove(Object, this);
				}
			}
		}

		// Token: 0x060082DD RID: 33501 RVA: 0x002F8AD8 File Offset: 0x002F6CD8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == SingletonEvent<AfterGameLoadedEvent>.ID || ID == ZoneThawedEvent.ID;
		}

		// Token: 0x060082DE RID: 33502 RVA: 0x002F8B00 File Offset: 0x002F6D00
		public override Effect DeepCopy(GameObject Parent)
		{
			ProceduralCookingEffect proceduralCookingEffect = base.DeepCopy(Parent) as ProceduralCookingEffect;
			proceduralCookingEffect.bApplied = false;
			if (this.units != null)
			{
				proceduralCookingEffect.units = new List<ProceduralCookingEffectUnit>(this.units.Count);
				foreach (ProceduralCookingEffectUnit proceduralCookingEffectUnit in this.units)
				{
					ProceduralCookingEffectUnit proceduralCookingEffectUnit2 = proceduralCookingEffectUnit.DeepCopy(proceduralCookingEffect);
					proceduralCookingEffect.units.Add(proceduralCookingEffectUnit2);
				}
			}
			return proceduralCookingEffect;
		}

		// Token: 0x060082DF RID: 33503 RVA: 0x002F8B94 File Offset: 0x002F6D94
		public override bool HandleEvent(AIBoredEvent E)
		{
			this.CheckNonPlayerExpiry();
			return base.HandleEvent(E);
		}

		// Token: 0x060082E0 RID: 33504 RVA: 0x002F8BA3 File Offset: 0x002F6DA3
		public override bool HandleEvent(AfterGameLoadedEvent E)
		{
			this.CheckNonPlayerExpiry();
			return base.HandleEvent(E);
		}

		// Token: 0x060082E1 RID: 33505 RVA: 0x002F8BB2 File Offset: 0x002F6DB2
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			this.CheckNonPlayerExpiry();
			return base.HandleEvent(E);
		}

		// Token: 0x060082E2 RID: 33506 RVA: 0x002F8BC4 File Offset: 0x002F6DC4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyWellFed");
			Registrar.Register("BecameFamished");
			Registrar.Register("BecameHungry");
			Registrar.Register("ClearFoodEffects");
			Registrar.Register("JoinedPartyLeader");
			Registrar.Register("RemoveProceduralCookingEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x060082E3 RID: 33507 RVA: 0x002F8C1C File Offset: 0x002F6E1C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BecameHungry" || E.ID == "BecameFamished" || E.ID == "ApplyWellFed" || E.ID == "RemoveProceduralCookingEffects" || E.ID == "ClearFoodEffects")
			{
				this.Duration = 0;
			}
			else if (E.ID == "JoinedPartyLeader")
			{
				this.CheckNonPlayerExpiry();
			}
			foreach (ProceduralCookingEffectUnit proceduralCookingEffectUnit in this.units)
			{
				proceduralCookingEffectUnit.FireEvent(E);
			}
			return base.FireEvent(E);
		}

		// Token: 0x060082E4 RID: 33508 RVA: 0x002F8CF0 File Offset: 0x002F6EF0
		public void CheckNonPlayerExpiry()
		{
			if (this.Duration <= 0)
			{
				return;
			}
			if (base.Object == null)
			{
				return;
			}
			if (base.Object.IsPlayer())
			{
				return;
			}
			if (base.Object.WasPlayer())
			{
				return;
			}
			if (Calendar.TotalTimeTicks >= this.StartTick + (long)this.GetNonPlayerDuration())
			{
				base.Object.RemoveEffect(this, true);
			}
		}

		// Token: 0x060082E5 RID: 33509 RVA: 0x002F8D4F File Offset: 0x002F6F4F
		public int GetNonPlayerDuration()
		{
			GameObject @object = base.Object;
			Stomach stomach = ((@object != null) ? @object.GetPart<Stomach>() : null);
			if (stomach == null)
			{
				return 1200;
			}
			return stomach.CalculateCookingIncrement();
		}

		// Token: 0x060082E6 RID: 33510 RVA: 0x002F8D74 File Offset: 0x002F6F74
		public virtual void Init(GameObject target)
		{
			this.bApplied = false;
			this.init = true;
			foreach (ProceduralCookingEffectUnit proceduralCookingEffectUnit in this.units)
			{
				proceduralCookingEffectUnit.Init(target);
			}
		}

		// Token: 0x060082E7 RID: 33511 RVA: 0x002F8DD4 File Offset: 0x002F6FD4
		private static void ApplyUnitFromTypeToEffect(ProceduralCookingEffect effect, string type)
		{
			GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints["ProceduralCookingIngredient_" + type];
			if (string.IsNullOrEmpty(gameObjectBlueprint.GetTag("Units", "")))
			{
				Debug.LogError("Tried to get units from a type without units: ProceduralCookingIngredient_" + type);
				return;
			}
			string text = gameObjectBlueprint.GetTag("Units", "").Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
			try
			{
				if (text.StartsWith("@"))
				{
					text = PopulationManager.RollOneFrom(text.Substring(1), null, null).Blueprint;
				}
				effect.AddUnit(Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + text, false, false, true)) as ProceduralCookingEffectUnit);
			}
			catch (Exception ex)
			{
				Debug.LogError("bad action: " + text + " ex:" + ex.ToString());
			}
		}

		// Token: 0x060082E8 RID: 33512 RVA: 0x002F8EB8 File Offset: 0x002F70B8
		private static void ApplyActionsFromTypeToEffect(ProceduralCookingEffectWithTrigger effect, string type)
		{
			GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints["ProceduralCookingIngredient_" + type];
			if (string.IsNullOrEmpty(gameObjectBlueprint.GetTag("Actions", "")))
			{
				Debug.LogError("Tried to get actions from a type without actions: ProceduralCookingIngredient_" + type);
				return;
			}
			string text = gameObjectBlueprint.GetTag("Actions", "").Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
			try
			{
				if (text.StartsWith("@"))
				{
					text = PopulationManager.RollOneFrom(text.Substring(1), null, null).Blueprint;
				}
				effect.triggeredActions.Add(Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + text, false, false, true)) as ProceduralCookingTriggeredAction);
			}
			catch (Exception ex)
			{
				Debug.LogError("bad triggered action: " + text + " ex:" + ex.ToString());
			}
		}

		// Token: 0x060082E9 RID: 33513 RVA: 0x002F8FA0 File Offset: 0x002F71A0
		private static ProceduralCookingEffectWithTrigger GetTriggeredActionEffectFromType(string type)
		{
			GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints["ProceduralCookingIngredient_" + type];
			if (string.IsNullOrEmpty(gameObjectBlueprint.GetTag("Triggers", "")))
			{
				Debug.LogError("Tried to get triggers from a type without triggers: ProceduralCookingIngredient_" + type);
				return null;
			}
			string text = gameObjectBlueprint.GetTag("Triggers", "").Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
			ProceduralCookingEffectWithTrigger proceduralCookingEffectWithTrigger;
			try
			{
				if (text.StartsWith("@"))
				{
					text = PopulationManager.RollOneFrom(text.Substring(1), null, null).Blueprint;
				}
				proceduralCookingEffectWithTrigger = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + text, false, false, true)) as ProceduralCookingEffectWithTrigger;
			}
			catch (Exception ex)
			{
				Debug.LogError("bad action with trigger: " + text + " ex:" + ex.ToString());
				proceduralCookingEffectWithTrigger = null;
			}
			return proceduralCookingEffectWithTrigger;
		}

		/// <summary>
		/// Creates a procedural effect from specific units, triggers and actions.
		/// </summary>
		/// <param name="units">All these units are applied.</param>
		/// <param name="triggers">One trigger is selected at random, if supplied and actions are supplied.</param>
		/// <param name="actions">One action is selected at random, if supplied and triggers are supplied.</param>
		/// <returns />
		// Token: 0x060082EA RID: 33514 RVA: 0x002F9084 File Offset: 0x002F7284
		public static ProceduralCookingEffect CreateSpecific(List<string> units = null, List<string> triggers = null, List<string> actions = null)
		{
			if (triggers != null && actions == null)
			{
				throw new ArgumentException("If either triggers or actions are supplied, both need to be supplied.");
			}
			if (triggers == null && actions != null)
			{
				throw new ArgumentException("If either triggers or actions are supplied, both need to be supplied.");
			}
			ProceduralCookingEffect proceduralCookingEffect = null;
			if (triggers == null)
			{
				proceduralCookingEffect = new ProceduralCookingEffect();
			}
			else
			{
				string text = triggers.GetRandomElement<string>(null);
				if (text.StartsWith("@"))
				{
					text = PopulationManager.RollOneFrom(text.Substring(1), null, null).Blueprint;
				}
				proceduralCookingEffect = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + text, false, false, true)) as ProceduralCookingEffectWithTrigger;
			}
			if (units != null)
			{
				foreach (string text2 in units)
				{
					try
					{
						string text3 = text2;
						if (text3.StartsWith("@"))
						{
							text3 = PopulationManager.RollOneFrom(text3.Substring(1), null, null).Blueprint;
						}
						proceduralCookingEffect.AddUnit(Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + text3, false, false, true)) as ProceduralCookingEffectUnit);
					}
					catch (Exception ex)
					{
						Debug.LogError("bad action: " + text2 + " ex:" + ex.ToString());
					}
				}
			}
			if (actions != null && triggers != null)
			{
				string text4 = actions.GetRandomElement<string>(null);
				if (text4.StartsWith("@"))
				{
					text4 = PopulationManager.RollOneFrom(text4.Substring(1), null, null).Blueprint;
				}
				(proceduralCookingEffect as ProceduralCookingEffectWithTrigger).triggeredActions.Add(Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + text4, false, false, true)) as ProceduralCookingTriggeredAction);
			}
			return proceduralCookingEffect;
		}

		// Token: 0x060082EB RID: 33515 RVA: 0x002F9224 File Offset: 0x002F7424
		public static ProceduralCookingEffect CreateJustUnits(List<string> types)
		{
			ProceduralCookingEffect proceduralCookingEffect = new ProceduralCookingEffect();
			foreach (string text in types)
			{
				ProceduralCookingEffect.ApplyUnitFromTypeToEffect(proceduralCookingEffect, text);
			}
			return proceduralCookingEffect;
		}

		// Token: 0x060082EC RID: 33516 RVA: 0x002F927C File Offset: 0x002F747C
		public static ProceduralCookingEffectWithTrigger CreateTriggeredAction(string triggerType, string actionType)
		{
			ProceduralCookingEffectWithTrigger triggeredActionEffectFromType = ProceduralCookingEffect.GetTriggeredActionEffectFromType(triggerType);
			ProceduralCookingEffect.ApplyActionsFromTypeToEffect(triggeredActionEffectFromType, actionType);
			return triggeredActionEffectFromType;
		}

		// Token: 0x060082ED RID: 33517 RVA: 0x002F928B File Offset: 0x002F748B
		public static ProceduralCookingEffectWithTrigger CreateBaseAndTriggeredAction(string baseType, string triggerType, string actionType)
		{
			ProceduralCookingEffectWithTrigger triggeredActionEffectFromType = ProceduralCookingEffect.GetTriggeredActionEffectFromType(triggerType);
			ProceduralCookingEffect.ApplyActionsFromTypeToEffect(triggeredActionEffectFromType, actionType);
			ProceduralCookingEffect.ApplyUnitFromTypeToEffect(triggeredActionEffectFromType, baseType);
			return triggeredActionEffectFromType;
		}

		// Token: 0x04003945 RID: 14661
		public long StartTick;

		// Token: 0x04003946 RID: 14662
		public List<ProceduralCookingEffectUnit> units = new List<ProceduralCookingEffectUnit>();

		// Token: 0x04003947 RID: 14663
		public bool bApplied;

		// Token: 0x04003948 RID: 14664
		public bool init;
	}
}
