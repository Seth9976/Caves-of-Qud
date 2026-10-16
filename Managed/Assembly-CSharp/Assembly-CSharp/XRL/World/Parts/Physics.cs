using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using Newtonsoft.Json;
using Occult.Engine.CodeGeneration;
using Unity.Profiling;
using XRL.Core;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02000DC7 RID: 3527
	[GeneratePoolingPartial(Capacity = 128)]
	[GenerateSerializationPartial(PreWrite = "ValidateGameObjects")]
	[Serializable]
	public class Physics : IPart, IContextRelationManager
	{
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06008E88 RID: 36488 RVA: 0x0033122D File Offset: 0x0032F42D
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return Physics.PhysicsPool;
			}
		}

		// Token: 0x06008E89 RID: 36489 RVA: 0x00331234 File Offset: 0x0032F434
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override void Reset()
		{
			base.Reset();
			this.Flags = 2;
			this.AmbientCache = int.MinValue;
			this.Category = "Unknown";
			this._Weight = 0;
			this._Temperature = 25;
			this.FlameTemperature = 350;
			this.VaporTemperature = 10000;
			this.FreezeTemperature = 0;
			this.BrittleTemperature = -100;
			this.BaseElectricalConductivity = 0;
			this.Owner = null;
			this.SpecificHeat = 1f;
			this._CurrentCell = null;
			this._InInventory = null;
			this._Equipped = null;
			this.LastDamagedBy = null;
			this.LastWeaponDamagedBy = null;
			this.LastProjectileDamagedBy = null;
			this.InflamedBy = null;
			this.LastDamagedByType = "";
			this.LastDeathReason = "";
			this.LastDeathCategory = "";
			this.LastThirdPersonDeathReason = "";
			this.ConfusedName = null;
			this.lastPushSegment = int.MinValue;
			this.lastPushCount = 0;
		}

		// Token: 0x06008E8A RID: 36490 RVA: 0x00331328 File Offset: 0x0032F528
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			this.ValidateGameObjects();
			Writer.WriteOptimized(this.Flags);
			Writer.WriteOptimized(this.Category);
			Writer.WriteOptimized(this._Weight);
			Writer.WriteOptimized(this._Temperature);
			Writer.WriteOptimized(this.FlameTemperature);
			Writer.WriteOptimized(this.VaporTemperature);
			Writer.WriteOptimized(this.FreezeTemperature);
			Writer.WriteOptimized(this.BrittleTemperature);
			Writer.WriteOptimized(this.BaseElectricalConductivity);
			Writer.WriteOptimized(this.Owner);
			Writer.Write(this.SpecificHeat);
			Writer.Write(this._CurrentCell);
			Writer.WriteGameObject(this._InInventory);
			Writer.WriteGameObject(this._Equipped);
			Writer.WriteGameObject(this.LastDamagedBy);
			Writer.WriteGameObject(this.LastWeaponDamagedBy);
			Writer.WriteGameObject(this.LastProjectileDamagedBy);
			Writer.WriteGameObject(this.InflamedBy);
			Writer.WriteOptimized(this.LastDamagedByType);
			Writer.WriteOptimized(this.LastDeathReason);
			Writer.WriteOptimized(this.LastDeathCategory);
			Writer.WriteOptimized(this.LastThirdPersonDeathReason);
		}

		// Token: 0x06008E8B RID: 36491 RVA: 0x00331444 File Offset: 0x0032F644
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Flags = Reader.ReadOptimizedInt32();
			this.Category = Reader.ReadOptimizedString();
			this._Weight = Reader.ReadOptimizedInt32();
			this._Temperature = Reader.ReadOptimizedInt32();
			this.FlameTemperature = Reader.ReadOptimizedInt32();
			this.VaporTemperature = Reader.ReadOptimizedInt32();
			this.FreezeTemperature = Reader.ReadOptimizedInt32();
			this.BrittleTemperature = Reader.ReadOptimizedInt32();
			this.BaseElectricalConductivity = Reader.ReadOptimizedInt32();
			this.Owner = Reader.ReadOptimizedString();
			this.SpecificHeat = Reader.ReadSingle();
			this._CurrentCell = Reader.ReadCell();
			this._InInventory = Reader.ReadGameObject(null);
			this._Equipped = Reader.ReadGameObject(null);
			this.LastDamagedBy = Reader.ReadGameObject(null);
			this.LastWeaponDamagedBy = Reader.ReadGameObject(null);
			this.LastProjectileDamagedBy = Reader.ReadGameObject(null);
			this.InflamedBy = Reader.ReadGameObject(null);
			this.LastDamagedByType = Reader.ReadOptimizedString();
			this.LastDeathReason = Reader.ReadOptimizedString();
			this.LastDeathCategory = Reader.ReadOptimizedString();
			this.LastThirdPersonDeathReason = Reader.ReadOptimizedString();
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06008E8C RID: 36492 RVA: 0x0033155F File Offset: 0x0032F75F
		// (set) Token: 0x06008E8D RID: 36493 RVA: 0x0033156C File Offset: 0x0032F76C
		[JsonIgnore]
		public bool Solid
		{
			get
			{
				return (this.Flags & 1) == 1;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 1) : (this.Flags & -2));
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06008E8E RID: 36494 RVA: 0x0033158A File Offset: 0x0032F78A
		// (set) Token: 0x06008E8F RID: 36495 RVA: 0x00331597 File Offset: 0x0032F797
		[JsonIgnore]
		public bool Takeable
		{
			get
			{
				return (this.Flags & 2) == 2;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 2) : (this.Flags & -3));
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06008E90 RID: 36496 RVA: 0x003315B5 File Offset: 0x0032F7B5
		// (set) Token: 0x06008E91 RID: 36497 RVA: 0x003315C2 File Offset: 0x0032F7C2
		[JsonIgnore]
		public bool IsReal
		{
			get
			{
				return (this.Flags & 4) == 4;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 4) : (this.Flags & -5));
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06008E92 RID: 36498 RVA: 0x003315E0 File Offset: 0x0032F7E0
		// (set) Token: 0x06008E93 RID: 36499 RVA: 0x003315ED File Offset: 0x0032F7ED
		[JsonIgnore]
		public bool UsesTwoSlots
		{
			get
			{
				return (this.Flags & 8) == 8;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 8) : (this.Flags & -9));
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06008E94 RID: 36500 RVA: 0x0033160B File Offset: 0x0032F80B
		// (set) Token: 0x06008E95 RID: 36501 RVA: 0x0033161A File Offset: 0x0032F81A
		[JsonIgnore]
		public bool LastDamageAccidental
		{
			get
			{
				return (this.Flags & 16) == 16;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 16) : (this.Flags & -17));
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06008E96 RID: 36502 RVA: 0x00331639 File Offset: 0x0032F839
		// (set) Token: 0x06008E97 RID: 36503 RVA: 0x0033164E File Offset: 0x0032F84E
		[JsonIgnore]
		public bool WasFreezing
		{
			get
			{
				return (this.Flags & 256) == 256;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 256) : (this.Flags & -257));
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06008E98 RID: 36504 RVA: 0x00331673 File Offset: 0x0032F873
		// (set) Token: 0x06008E99 RID: 36505 RVA: 0x00331682 File Offset: 0x0032F882
		[JsonIgnore]
		public bool WasFrozen
		{
			get
			{
				return (this.Flags & 32) == 32;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 32) : (this.Flags & -33));
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06008E9A RID: 36506 RVA: 0x003316A1 File Offset: 0x0032F8A1
		// (set) Token: 0x06008E9B RID: 36507 RVA: 0x003316B0 File Offset: 0x0032F8B0
		[JsonIgnore]
		public bool WasAflame
		{
			get
			{
				return (this.Flags & 64) == 64;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 64) : (this.Flags & -65));
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06008E9C RID: 36508 RVA: 0x003316CF File Offset: 0x0032F8CF
		// (set) Token: 0x06008E9D RID: 36509 RVA: 0x003316E4 File Offset: 0x0032F8E4
		[JsonIgnore]
		public bool Organic
		{
			get
			{
				return (this.Flags & 128) == 128;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 128) : (this.Flags & -129));
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06008E9E RID: 36510 RVA: 0x00331709 File Offset: 0x0032F909
		[JsonIgnore]
		public override int Priority
		{
			get
			{
				return 90000;
			}
		}

		// Token: 0x06008E9F RID: 36511 RVA: 0x00331710 File Offset: 0x0032F910
		public bool ShouldRestAtAmbient()
		{
			return (this.ParentObject.IsWall() || this.ParentObject.IsScenery) && !this.ParentObject.IsCombatObject();
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06008EA0 RID: 36512 RVA: 0x0033173C File Offset: 0x0032F93C
		[JsonIgnore]
		public int AmbientTemperature
		{
			get
			{
				if (this.AmbientCache != -2147483648)
				{
					return this.AmbientCache;
				}
				Cell cell = this.CurrentCell ?? this.ParentObject.GetCurrentCell();
				if (cell != null && cell.ParentZone != null)
				{
					if (this.ShouldRestAtAmbient())
					{
						this.AmbientCache = cell.ParentZone.BaseTemperature;
					}
					else if (cell.ParentZone.BaseTemperature > 25 && this.ParentObject.Stat("HeatResistance", 0) > 0)
					{
						this.AmbientCache = Math.Max(25, cell.ParentZone.BaseTemperature - 4 * this.ParentObject.Stat("HeatResistance", 0));
					}
					else if (cell.ParentZone.BaseTemperature < 25 && this.ParentObject.Stat("ColdResistance", 0) > 0)
					{
						this.AmbientCache = Math.Min(25, cell.ParentZone.BaseTemperature + this.ParentObject.Stat("ColdResistance", 0));
					}
					else
					{
						this.AmbientCache = cell.ParentZone.BaseTemperature;
					}
				}
				else
				{
					this.AmbientCache = 25;
				}
				return this.AmbientCache;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06008EA1 RID: 36513 RVA: 0x00331863 File Offset: 0x0032FA63
		// (set) Token: 0x06008EA2 RID: 36514 RVA: 0x0033186B File Offset: 0x0032FA6B
		[JsonIgnore]
		public int Temperature
		{
			get
			{
				return this._Temperature;
			}
			set
			{
				if (this.SpecificHeat == 0f && this.Temperature != 25)
				{
					return;
				}
				this._Temperature = Math.Clamp(value, -10000, 10000);
			}
		}

		// Token: 0x06008EA3 RID: 36515 RVA: 0x0033189B File Offset: 0x0032FA9B
		public override void Attach()
		{
			this.ParentObject.Physics = this;
		}

		// Token: 0x06008EA4 RID: 36516 RVA: 0x003318A9 File Offset: 0x0032FAA9
		public override void Remove()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Physics : null) == this)
			{
				this.ParentObject.Physics = null;
			}
		}

		// Token: 0x06008EA5 RID: 36517 RVA: 0x003318CC File Offset: 0x0032FACC
		public double GetIntrinsicWeightEach()
		{
			double num = (double)this._Weight + (double)this.ParentObject.GetBodyWeight();
			double num2 = num;
			if (this.ParentObject.WantEvent(GetIntrinsicWeightEvent.ID, MinEvent.CascadeLevel))
			{
				GetIntrinsicWeightEvent getIntrinsicWeightEvent = GetIntrinsicWeightEvent.FromPool(this.ParentObject, num, num2);
				this.ParentObject.HandleEvent(getIntrinsicWeightEvent);
				num2 = getIntrinsicWeightEvent.Weight;
			}
			if (this.ParentObject.WantEvent(AdjustWeightEvent.ID, MinEvent.CascadeLevel))
			{
				AdjustWeightEvent adjustWeightEvent = AdjustWeightEvent.FromPool(this.ParentObject, num, num2);
				this.ParentObject.HandleEvent(adjustWeightEvent);
				num2 = adjustWeightEvent.Weight;
			}
			return num2;
		}

		// Token: 0x06008EA6 RID: 36518 RVA: 0x00331964 File Offset: 0x0032FB64
		public double GetWeightEach()
		{
			double num = (double)this._Weight + (double)this.ParentObject.GetBodyWeight();
			double num2 = this.GetIntrinsicWeightEach();
			if (this.ParentObject.WantEvent(GetExtrinsicWeightEvent.ID, MinEvent.CascadeLevel))
			{
				GetExtrinsicWeightEvent getExtrinsicWeightEvent = GetExtrinsicWeightEvent.FromPool(this.ParentObject, num, num2);
				this.ParentObject.HandleEvent(getExtrinsicWeightEvent);
				num2 = getExtrinsicWeightEvent.Weight;
			}
			if (this.ParentObject.WantEvent(AdjustTotalWeightEvent.ID, MinEvent.CascadeLevel))
			{
				AdjustTotalWeightEvent adjustTotalWeightEvent = AdjustTotalWeightEvent.FromPool(this.ParentObject, num, num2);
				this.ParentObject.HandleEvent(adjustTotalWeightEvent);
				num2 = adjustTotalWeightEvent.Weight;
			}
			return num2;
		}

		// Token: 0x06008EA7 RID: 36519 RVA: 0x00331A00 File Offset: 0x0032FC00
		public double GetIntrinsicWeight()
		{
			return this.GetIntrinsicWeightEach() * (double)this.ParentObject.Count;
		}

		// Token: 0x06008EA8 RID: 36520 RVA: 0x00331A15 File Offset: 0x0032FC15
		public double GetWeight()
		{
			return this.GetWeightEach() * (double)this.ParentObject.Count;
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06008EAA RID: 36522 RVA: 0x00331A33 File Offset: 0x0032FC33
		// (set) Token: 0x06008EA9 RID: 36521 RVA: 0x00331A2A File Offset: 0x0032FC2A
		public int IntrinsicWeight
		{
			get
			{
				return (int)this.GetIntrinsicWeight();
			}
			set
			{
				this._Weight = value;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06008EAC RID: 36524 RVA: 0x00331A45 File Offset: 0x0032FC45
		// (set) Token: 0x06008EAB RID: 36523 RVA: 0x00331A3C File Offset: 0x0032FC3C
		public int Weight
		{
			get
			{
				return (int)this.GetWeight();
			}
			set
			{
				this._Weight = value;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06008EAD RID: 36525 RVA: 0x00331A4E File Offset: 0x0032FC4E
		public int IntrinsicWeightEach
		{
			get
			{
				return (int)this.GetIntrinsicWeightEach();
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06008EAE RID: 36526 RVA: 0x00331A57 File Offset: 0x0032FC57
		public int WeightEach
		{
			get
			{
				return (int)this.GetWeightEach();
			}
		}

		// Token: 0x06008EAF RID: 36527 RVA: 0x00331A60 File Offset: 0x0032FC60
		public int GetIntrinsicWeightTimes(double Factor)
		{
			return (int)(this.GetIntrinsicWeight() * Factor);
		}

		// Token: 0x06008EB0 RID: 36528 RVA: 0x00331A6B File Offset: 0x0032FC6B
		public int GetWeightTimes(double Factor)
		{
			return (int)(this.GetWeight() * Factor);
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06008EB1 RID: 36529 RVA: 0x00331A76 File Offset: 0x0032FC76
		public string VaporObject
		{
			get
			{
				return this.ParentObject.GetTag("VaporObject", "");
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06008EB3 RID: 36531 RVA: 0x00331B24 File Offset: 0x0032FD24
		// (set) Token: 0x06008EB2 RID: 36530 RVA: 0x00331A90 File Offset: 0x0032FC90
		[JsonIgnore]
		public Cell CurrentCell
		{
			get
			{
				return this._CurrentCell;
			}
			set
			{
				if (this._CurrentCell != null && this._CurrentCell != value && this._CurrentCell.Objects.Contains(this.ParentObject))
				{
					try
					{
						this._CurrentCell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					}
					catch (Exception ex)
					{
						MetricsManager.LogException("CurrentCell failsafe remove", ex, "game_exception");
					}
				}
				this._CurrentCell = value;
				if (value != null)
				{
					this.InInventory = null;
					this.Equipped = null;
				}
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06008EB4 RID: 36532 RVA: 0x00331B2C File Offset: 0x0032FD2C
		public Zone CurrentZone
		{
			get
			{
				Cell currentCell = this.CurrentCell;
				if (currentCell == null)
				{
					return null;
				}
				return currentCell.ParentZone;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06008EB6 RID: 36534 RVA: 0x00331BE0 File Offset: 0x0032FDE0
		// (set) Token: 0x06008EB5 RID: 36533 RVA: 0x00331B40 File Offset: 0x0032FD40
		public GameObject InInventory
		{
			get
			{
				return this._InInventory;
			}
			set
			{
				if (this._InInventory != null && this._InInventory != value)
				{
					try
					{
						Inventory inventory = this._InInventory.Inventory;
						if (inventory != null && inventory.Objects.Contains(this.ParentObject))
						{
							inventory.RemoveObject(this.ParentObject, null);
						}
					}
					catch (Exception ex)
					{
						MetricsManager.LogException("InInventory failsafe remove", ex, "game_exception");
					}
				}
				this._InInventory = value;
				if (value != null)
				{
					this.Equipped = null;
					this.CurrentCell = null;
					if (Sidebar.CurrentTarget == this.ParentObject)
					{
						Sidebar.CurrentTarget = null;
					}
				}
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06008EB8 RID: 36536 RVA: 0x00331C63 File Offset: 0x0032FE63
		// (set) Token: 0x06008EB7 RID: 36535 RVA: 0x00331BE8 File Offset: 0x0032FDE8
		public GameObject Equipped
		{
			get
			{
				return this._Equipped;
			}
			set
			{
				GameObject equipped = this._Equipped;
				if (equipped != null)
				{
					if (equipped == value)
					{
						MetricsManager.LogWarning(string.Format("Physics.Equipped assigned to identical object: {0}.\n{1}", value, new StackTrace()));
						return;
					}
					this._Equipped = null;
					BeforeUnequippedEvent.Send(this.ParentObject, equipped);
					UnequippedEvent.Send(this.ParentObject, equipped);
				}
				this._Equipped = value;
				if (value != null)
				{
					this.InInventory = null;
					this.CurrentCell = null;
					if (Sidebar.CurrentTarget == this.ParentObject)
					{
						Sidebar.CurrentTarget = null;
					}
				}
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06008EB9 RID: 36537 RVA: 0x00331C6B File Offset: 0x0032FE6B
		// (set) Token: 0x06008EBA RID: 36538 RVA: 0x00331C75 File Offset: 0x0032FE75
		[JsonIgnore]
		public int ElectricalConductivity
		{
			get
			{
				return this.GetElectricalConductivity(null, 0);
			}
			set
			{
				this.BaseElectricalConductivity = value;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06008EBB RID: 36539 RVA: 0x00331C7E File Offset: 0x0032FE7E
		// (set) Token: 0x06008EBC RID: 36540 RVA: 0x00331C88 File Offset: 0x0032FE88
		[JsonIgnore]
		public int Conductivity
		{
			get
			{
				return this.GetElectricalConductivity(null, 0);
			}
			set
			{
				this.BaseElectricalConductivity = value;
			}
		}

		// Token: 0x06008EBD RID: 36541 RVA: 0x00331C91 File Offset: 0x0032FE91
		public int GetElectricalConductivity(out GameObject ReductionObject, out string ReductionReason, GameObject Source = null, int Phase = 0)
		{
			return GetElectricalConductivityEvent.GetFor(this.ParentObject, out ReductionObject, out ReductionReason, int.MinValue, Source, Phase);
		}

		// Token: 0x06008EBE RID: 36542 RVA: 0x00331CA8 File Offset: 0x0032FEA8
		public int GetElectricalConductivity(GameObject Source = null, int Phase = 0)
		{
			GameObject gameObject;
			string text;
			return this.GetElectricalConductivity(out gameObject, out text, Source, Phase);
		}

		// Token: 0x06008EBF RID: 36543 RVA: 0x00331CC1 File Offset: 0x0032FEC1
		public bool IsFrozen()
		{
			return this.Temperature <= this.BrittleTemperature;
		}

		// Token: 0x06008EC0 RID: 36544 RVA: 0x00331CD4 File Offset: 0x0032FED4
		public bool IsFreezing()
		{
			return this.Temperature <= this.FreezeTemperature;
		}

		// Token: 0x06008EC1 RID: 36545 RVA: 0x00331CE7 File Offset: 0x0032FEE7
		public bool IsAflame()
		{
			return this.Temperature >= this.FlameTemperature;
		}

		// Token: 0x06008EC2 RID: 36546 RVA: 0x00331CFA File Offset: 0x0032FEFA
		public bool IsVaporizing()
		{
			return this.Temperature >= this.VaporTemperature;
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06008EC3 RID: 36547 RVA: 0x00331D0D File Offset: 0x0032FF0D
		// (set) Token: 0x06008EC4 RID: 36548 RVA: 0x00331D20 File Offset: 0x0032FF20
		public string UsesSlots
		{
			get
			{
				return this.ParentObject.GetTagOrStringProperty("UsesSlots", null);
			}
			set
			{
				this.ParentObject.SetStringProperty("UsesSlots", value, false);
			}
		}

		// Token: 0x06008EC5 RID: 36549 RVA: 0x00331D34 File Offset: 0x0032FF34
		public void ValidateGameObjects()
		{
			GameObject.Validate(ref this.LastDamagedBy);
			GameObject.Validate(ref this.LastWeaponDamagedBy);
			GameObject.Validate(ref this.LastProjectileDamagedBy);
			GameObject.Validate(ref this.InflamedBy);
		}

		// Token: 0x06008EC6 RID: 36550 RVA: 0x00331D66 File Offset: 0x0032FF66
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Physics physics = base.DeepCopy(Parent, MapInv) as Physics;
			physics._CurrentCell = null;
			physics._Equipped = null;
			physics._InInventory = null;
			return physics;
		}

		// Token: 0x06008EC7 RID: 36551 RVA: 0x00331D8C File Offset: 0x0032FF8C
		public override bool SameAs(IPart p)
		{
			Physics physics = p as Physics;
			return physics._Weight == this._Weight && physics.Solid == this.Solid && !(physics.Category != this.Category) && base.SameAs(p);
		}

		// Token: 0x06008EC8 RID: 36552 RVA: 0x00331DDC File Offset: 0x0032FFDC
		public static bool IsMoveable(GameObject obj, bool Involuntary = true)
		{
			return obj.IsReal && !obj.IsScenery && !obj.HasTag("ExcavatoryTerrainFeature") && (!Involuntary || obj.CanBeInvoluntarilyMoved());
		}

		// Token: 0x06008EC9 RID: 36553 RVA: 0x00331E10 File Offset: 0x00330010
		public bool Push(string Direction, int Force, int MaxDistance = 9999999, bool IgnoreGravity = false, bool Involuntary = true, GameObject Actor = null)
		{
			if ((long)this.lastPushSegment != The.Game.Segments)
			{
				this.lastPushCount = 0;
			}
			else
			{
				if (this.lastPushCount >= 2)
				{
					return true;
				}
				this.lastPushCount++;
			}
			if (MaxDistance < 0)
			{
				return false;
			}
			if (this.CurrentCell == null)
			{
				return false;
			}
			if (Involuntary)
			{
				if (!Physics.IsMoveable(this.ParentObject, true))
				{
					return false;
				}
				int kineticResistance = this.ParentObject.GetKineticResistance();
				if (kineticResistance > Force)
				{
					return false;
				}
				if (kineticResistance < 0)
				{
					return false;
				}
			}
			List<string> adjacentDirections = Directions.GetAdjacentDirections(Direction, 2);
			if (50.in100())
			{
				adjacentDirections.Reverse();
			}
			adjacentDirections.Remove(Direction);
			adjacentDirections.Insert(0, Direction);
			foreach (string text in adjacentDirections)
			{
				Cell localCellFromDirection = this.CurrentCell.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection == null)
				{
					return false;
				}
				if (localCellFromDirection.IsEmpty())
				{
					if (this.ParentObject.Move(Direction, true, false, IgnoreGravity, false, true, true, null, null, false, null, null, null, false, false, null, null, 0))
					{
						return true;
					}
				}
				else
				{
					List<GameObject> list = Event.NewGameObjectList();
					list.AddRange(localCellFromDirection.GetSolidObjects());
					foreach (GameObject gameObject in localCellFromDirection.GetObjectsWithPartReadonly("Combat"))
					{
						if (!list.Contains(gameObject))
						{
							list.Add(gameObject);
						}
					}
					if (list.Count > 0)
					{
						int num = (Force * 9 / 10 - 10) / list.Count;
						int i = 0;
						int count = list.Count;
						while (i < count)
						{
							list[i].Push(Direction, num, MaxDistance - 1, false, true);
							i++;
						}
					}
					if (this.ParentObject.Move(Direction, true, false, IgnoreGravity, false, true, true, null, null, false, null, null, null, false, false, null, null, 0))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06008ECA RID: 36554 RVA: 0x00332064 File Offset: 0x00330264
		public int Accelerate(int Force, string Direction = null, Cell Toward = null, Cell AwayFrom = null, string Type = null, GameObject Actor = null, bool Accidental = false, GameObject IntendedTarget = null, string BonusDamage = null, double DamageFactor = 1.0, bool SuspendFalling = true, bool OneShort = false, bool Repeat = false, bool BuiltOnly = true, bool MessageForInanimate = true, bool DelayForDisplay = true)
		{
			if (!Physics.IsMoveable(this.ParentObject, true))
			{
				return 0;
			}
			return this.AccelerateInternal(Force, Direction, Toward, AwayFrom, Type, Actor, Accidental, IntendedTarget, BonusDamage, DamageFactor, SuspendFalling, OneShort, Repeat, BuiltOnly, MessageForInanimate, DelayForDisplay, false);
		}

		// Token: 0x06008ECB RID: 36555 RVA: 0x003320A8 File Offset: 0x003302A8
		protected int AccelerateInternal(int Force, string Direction = null, Cell Toward = null, Cell AwayFrom = null, string Type = null, GameObject Actor = null, bool Accidental = false, GameObject IntendedTarget = null, string BonusDamage = null, double DamageFactor = 1.0, bool SuspendFalling = true, bool OneShort = false, bool Repeat = false, bool BuiltOnly = true, bool MessageForInanimate = true, bool DelayForDisplay = true, bool Subsequent = false)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 5;
			List<string> list = null;
			List<string> list2 = null;
			List<GameObject> list3 = null;
			if (num > 10000 || (num >= 100 && num > Force * 2))
			{
				MetricsManager.LogError("infinite loop on " + this.ParentObject.DebugName);
				return num;
			}
			bool flag = Subsequent && (MessageForInanimate || this.ParentObject.IsCreature);
			while (this.CurrentCell != null)
			{
				int num4 = this.ParentObject.GetKineticResistance();
				if (num4 > Force)
				{
					return num;
				}
				if (num4 < 0)
				{
					return num;
				}
				if (num4 == 0)
				{
					num4 = 1;
				}
				Force -= num4;
				int springiness = this.ParentObject.GetSpringiness();
				int num5 = num4 + springiness;
				string text = Direction;
				if (text.IsNullOrEmpty())
				{
					if (list != null && list.Count == 0 && list2 != null)
					{
						list = new List<string>(list2);
					}
					if (Toward != null && !Repeat && (this.CurrentCell == Toward || (OneShort && this.ParentObject.DistanceTo(Toward) <= 1)))
					{
						return num;
					}
					if (list != null && list.Count > 0)
					{
						text = list[0];
						list.RemoveAt(0);
					}
					else if (Toward != null)
					{
						list = null;
						if (this.CurrentCell.ParentZone == Toward.ParentZone)
						{
							List<Tuple<Cell, char>> lineTo = this.ParentObject.GetLineTo(Toward, true, false);
							if (lineTo != null && lineTo.Count > 1)
							{
								list = new List<string>(lineTo.Count - 1);
								int i = 0;
								int num6 = lineTo.Count - 1;
								while (i < num6)
								{
									string directionFromCell = lineTo[i].Item1.GetDirectionFromCell(lineTo[i + 1].Item1, false);
									if (directionFromCell.IsNullOrEmpty() || !(directionFromCell != ".") || !(directionFromCell != "?"))
									{
										list = null;
										break;
									}
									list.Add(directionFromCell);
									i++;
								}
								if (list.Count <= 0)
								{
									list = null;
								}
							}
						}
						if (list != null)
						{
							num3 = 5 - list.Count / 5;
							if (Repeat)
							{
								list2 = new List<string>(list);
							}
							text = list[0];
							if (AwayFrom == null)
							{
								list.RemoveAt(0);
							}
							else
							{
								list = null;
							}
						}
						else
						{
							text = this.CurrentCell.GetDirectionFromCell(Toward, false);
						}
						if (AwayFrom != null)
						{
							text = Directions.CombineDirections(text, Directions.GetOppositeDirection(this.CurrentCell.GetDirectionFromCell(AwayFrom, false)), (int)this.CurrentCell.RealDistanceTo(Toward, true), (int)this.CurrentCell.RealDistanceTo(AwayFrom, true));
						}
					}
					else if (AwayFrom != null)
					{
						if (AwayFrom == this.CurrentCell)
						{
							text = Directions.GetRandomDirection();
						}
						else
						{
							text = Directions.GetOppositeDirection(this.CurrentCell.GetDirectionFromCell(AwayFrom, false));
						}
					}
				}
				if (text.IsNullOrEmpty() || text == "." || text == "?")
				{
					return num;
				}
				Cell cellFromDirection = this.CurrentCell.GetCellFromDirection(text, BuiltOnly);
				if (cellFromDirection == null || cellFromDirection == this.CurrentCell)
				{
					return num;
				}
				if (cellFromDirection.IsEmpty())
				{
					if (!this.ParentObject.Move(text, true, false, SuspendFalling, true, true, true, null, null, false, null, Type, null, false, false, null, null, 0))
					{
						this.ParentObject.CheckStack();
						return num;
					}
					if (flag)
					{
						base.DidX("are", "knocked " + Directions.GetDirectionDescription(text), null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
						flag = false;
					}
					if (Type == "Telekinetic")
					{
						this.ParentObject.TelekinesisBlip();
					}
					if (Direction != null)
					{
						Direction = text;
					}
					num++;
					if (!Subsequent)
					{
						The.Core.RenderBase(false, false);
						if (num3 > 0 && DelayForDisplay)
						{
							Thread.Sleep(num3);
						}
					}
				}
				else
				{
					if (flag)
					{
						base.DidX("are", "knocked " + Directions.GetDirectionDescription(text), null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
						flag = false;
					}
					List<GameObject> list4 = Event.NewGameObjectList();
					int j = 0;
					int count = cellFromDirection.Objects.Count;
					while (j < count)
					{
						GameObject gameObject = cellFromDirection.Objects[j];
						if (this.CollidesWith(gameObject, ref num2))
						{
							list4.Add(gameObject);
						}
						j++;
					}
					if (list4.Count > 0)
					{
						Direction = Directions.GetOppositeDirection(text);
						int num7 = num5;
						foreach (GameObject gameObject2 in list4)
						{
							num7 += gameObject2.GetKineticAbsorption();
						}
						int num8 = Force;
						Force = num8 * num5 / num7;
						if (list3 == null)
						{
							list3 = Event.NewGameObjectList();
						}
						string text2 = (this.ParentObject.IsPlayer() ? "you" : this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
						foreach (GameObject gameObject3 in list4)
						{
							int kineticResistance = gameObject3.GetKineticResistance();
							int springiness2 = gameObject3.GetSpringiness();
							if (MessageForInanimate || this.ParentObject.IsCreature || gameObject3.IsCreature)
							{
								base.DidXToY("collide", "with", gameObject3, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							}
							if (!list3.Contains(gameObject3))
							{
								list3.Add(gameObject3);
								int num9 = (int)((double)(num8 * (num4 + kineticResistance - springiness - springiness2)) * DamageFactor / (double)num7) / 20;
								int num10;
								int num11;
								this.CalculateIncrementalDamageRange(num9, out num10, out num11);
								bool flag2 = gameObject3.ConsiderSolid() || gameObject3.IsCombat;
								int num12;
								if (flag2)
								{
									num12 = 1;
								}
								else
								{
									int num13;
									int num14;
									this.GetCollidedWithPenetration(gameObject3, Force, out num13, out num14);
									num12 = Stat.RollDamagePenetrations(Stats.GetCombatAV(this.ParentObject), num13, num14);
								}
								int num15 = 0;
								for (int k = 0; k < num12; k++)
								{
									num15 += Stat.Random(num10, num11);
									if (!BonusDamage.IsNullOrEmpty())
									{
										num15 += BonusDamage.RollCached();
									}
								}
								bool flag3 = this.Solid || this.ParentObject.IsCombat;
								int num16;
								if (flag3)
								{
									num16 = 1;
								}
								else
								{
									int num17;
									int num18;
									this.GetCollidedWithPenetration(this.ParentObject, Force, out num17, out num18);
									num16 = Stat.RollDamagePenetrations(Stats.GetCombatAV(gameObject3), num17, num18);
								}
								int num19 = 0;
								for (int l = 0; l < num16; l++)
								{
									num19 += Stat.Random(num10, num11);
									if (!BonusDamage.IsNullOrEmpty())
									{
										num19 += BonusDamage.RollCached();
									}
								}
								if (num15 > 0)
								{
									string text3 = "from colliding with " + (gameObject3.IsPlayer() ? "you" : gameObject3.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false)) + ".";
									if (!flag2)
									{
										string resultColor = Stat.GetResultColor(num12);
										text3 = ("(x" + num12.ToString() + ")").Color(resultColor) + " " + text3;
									}
									GameObject parentObject = this.ParentObject;
									int num20 = num15;
									bool flag4 = Accidental && this.ParentObject != IntendedTarget;
									GameObject gameObject4 = gameObject3;
									parentObject.TakeDamage(num20, text3, "Crushing Collision", null, null, null, Actor, gameObject4, null, null, flag4, false, false, false, false, MessageForInanimate, false, false, false, 5, null);
								}
								if (num19 > 0)
								{
									string text4 = string.Concat(new string[] { "from ", text2, " colliding with ", gameObject3.them, "." });
									if (!flag3)
									{
										string resultColor2 = Stat.GetResultColor(num12);
										text4 = ("(x" + num16.ToString() + ")").Color(resultColor2) + " " + text4;
									}
									GameObject gameObject5 = gameObject3;
									int num21 = num19;
									bool flag4 = Accidental && this.ParentObject != IntendedTarget;
									GameObject parentObject2 = this.ParentObject;
									gameObject5.TakeDamage(num21, text4, "Crushing Collision", null, null, null, Actor, parentObject2, null, null, flag4, false, false, false, false, MessageForInanimate, false, false, false, 5, null);
								}
							}
							if (GameObject.Validate(gameObject3) && gameObject3.GetMatterPhase() <= 1)
							{
								gameObject3.Physics.AccelerateInternal(num8 * kineticResistance / num7, text, null, null, Type, Actor, true, null, BonusDamage, DamageFactor, SuspendFalling, false, false, BuiltOnly, MessageForInanimate, DelayForDisplay, true);
							}
							if (!GameObject.Validate(this.ParentObject))
							{
								return num;
							}
						}
					}
					if (!this.ParentObject.Move(text, true, false, SuspendFalling, true, true, true, null, null, false, null, Type, null, false, false, null, null, 0))
					{
						if (SuspendFalling)
						{
							this.ParentObject.Gravitate();
						}
						this.ParentObject.CheckStack();
						return num;
					}
					if (Type == "Telekinetic")
					{
						this.ParentObject.TelekinesisBlip();
					}
					if (Direction != null)
					{
						Direction = text;
					}
					num++;
					if (!Subsequent)
					{
						The.Core.RenderBase(false, false);
						if (num3 > 0 && DelayForDisplay)
						{
							Thread.Sleep(num3);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06008ECC RID: 36556 RVA: 0x003329F4 File Offset: 0x00330BF4
		private void CalculateIncrementalDamageRange(int Increments, out int Low, out int High)
		{
			Low = 0;
			High = 0;
			if (Increments > 0)
			{
				Low = 1;
				High = 2;
				for (int i = 1; i < Increments; i++)
				{
					if (i % 3 == 0)
					{
						Low++;
					}
					else
					{
						High += 2;
					}
				}
			}
		}

		// Token: 0x06008ECD RID: 36557 RVA: 0x00332A30 File Offset: 0x00330C30
		public static void LegacyApplyExplosion(Cell C, List<Cell> UsedCells, List<GameObject> Hit, int Force, bool Local = true, bool Show = true, GameObject Owner = null, string BonusDamage = null, int Phase = 1, float DamageModifier = 1f)
		{
			if (C == null)
			{
				return;
			}
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			if (Show)
			{
				TextConsole.LoadScrapBuffers();
				The.Core.RenderMapToBuffer(scrapBuffer);
			}
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			CleanQueue<int> cleanQueue2 = new CleanQueue<int>();
			CleanQueue<string> cleanQueue3 = new CleanQueue<string>();
			cleanQueue.Enqueue(C);
			cleanQueue2.Enqueue(Force);
			cleanQueue3.Enqueue(".");
			UsedCells.Add(C);
			while (cleanQueue.Count > 0)
			{
				Event.PinCurrentPool();
				Cell cell = cleanQueue.Dequeue();
				int num = cleanQueue2.Dequeue();
				string text = cleanQueue3.Dequeue();
				for (int i = 0; i < UsedCells.Count; i++)
				{
					Cell cell2 = UsedCells[i];
					if (cell2 == null)
					{
						return;
					}
					if (cell2.ParentZone == The.ZoneManager.ActiveZone)
					{
						scrapBuffer.Goto(cell2.X, cell2.Y);
						scrapBuffer.Write("&" + Phase.getRandomExplosionColor(Phase).ToString() + "*", true, false, false, null, -1);
					}
				}
				if (Show && C.ParentZone != null && C.ParentZone.IsActive())
				{
					textConsole.DrawBuffer(scrapBuffer, null, false);
					if (Force < 100000)
					{
						Thread.Sleep(5);
					}
				}
				List<Cell> list;
				if (Local)
				{
					list = cell.GetLocalAdjacentCells(1, false);
				}
				else
				{
					list = cell.GetAdjacentCells(true);
				}
				for (int j = 0; j < UsedCells.Count; j++)
				{
					Cell cell3 = UsedCells[j];
					if (list.CleanContains(cell3))
					{
						list.Remove(cell3);
					}
				}
				int num2 = 0;
				Damage damage = null;
				Event @event = null;
				foreach (GameObject gameObject in cell.GetObjectsWithPartReadonly("Physics"))
				{
					if (!Hit.Contains(gameObject))
					{
						Hit.Add(gameObject);
						if (gameObject.PhaseMatches(Phase))
						{
							num2 += gameObject.GetKineticResistance();
							if (damage == null || !BonusDamage.IsNullOrEmpty())
							{
								damage = new Damage((int)(DamageModifier * (float)num / 250f));
								if (!BonusDamage.IsNullOrEmpty())
								{
									damage.Amount += BonusDamage.RollCached();
								}
								damage.AddAttribute("Explosion");
								if (cell != C)
								{
									damage.AddAttribute("Accidental");
								}
							}
							if (@event == null || !BonusDamage.IsNullOrEmpty())
							{
								@event = Event.New("TakeDamage", 0, 0, 0);
								@event.SetParameter("Damage", damage);
								@event.SetParameter("Owner", Owner);
								@event.SetParameter("Attacker", Owner);
								@event.SetParameter("Message", "from %t explosion!");
							}
							gameObject.FireEvent(@event);
						}
					}
				}
				Random random = new Random();
				for (int k = 0; k < list.Count; k++)
				{
					int num3 = random.Next(0, list.Count);
					Cell cell4 = list[k];
					list[k] = list[num3];
					list[num3] = cell4;
				}
				Damage damage2 = null;
				Event event2 = null;
				for (;;)
				{
					IL_0303:
					for (int l = 0; l < list.Count; l++)
					{
						Cell cell5 = list[l];
						if (!Local || (cell5.X != 0 && cell5.X != 79 && cell5.Y != 0 && cell5.Y != 24))
						{
							foreach (GameObject gameObject2 in cell5.GetObjectsWithPartReadonly("Physics"))
							{
								if (!Hit.Contains(gameObject2))
								{
									Hit.Add(gameObject2);
									if (gameObject2.PhaseMatches(Phase))
									{
										if (damage2 == null || !BonusDamage.IsNullOrEmpty())
										{
											damage2 = new Damage(num / 250);
											if (!BonusDamage.IsNullOrEmpty())
											{
												damage2.Amount += BonusDamage.RollCached();
											}
											damage2.AddAttribute("Explosion");
											damage2.AddAttribute("Accidental");
										}
										if (event2 == null || !BonusDamage.IsNullOrEmpty())
										{
											event2 = Event.New("TakeDamage", 0, 0, 0);
											event2.SetParameter("Damage", damage2);
											event2.SetParameter("Owner", Owner);
											event2.SetParameter("Attacker", Owner);
											event2.SetParameter("Message", "from %t explosion!");
										}
										gameObject2.FireEvent(event2);
									}
								}
								if (gameObject2.PhaseMatches(Phase))
								{
									int kineticResistance = gameObject2.GetKineticResistance();
									if (kineticResistance > num)
									{
										list.Remove(cell5);
										goto IL_0303;
									}
									if (kineticResistance > 0)
									{
										gameObject2.Move((text == ".") ? Directions.GetRandomDirection() : text, true, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
									}
								}
							}
							if (cell5.IsSolid())
							{
								list.Remove(cell5);
								goto IL_0303;
							}
						}
					}
					break;
				}
				if (list.Count > 0)
				{
					int num4 = (num - num2) / list.Count;
					if (num4 > 100)
					{
						foreach (Cell cell6 in list)
						{
							if (cell6 != null && !UsedCells.Contains(cell6))
							{
								UsedCells.Add(cell6);
								cleanQueue.Enqueue(cell6);
								cleanQueue2.Enqueue(num4);
								cleanQueue3.Enqueue(cell.GetDirectionFromCell(cell6, false));
							}
						}
					}
				}
				Event.ResetToPin();
			}
		}

		// Token: 0x06008ECE RID: 36558 RVA: 0x00333020 File Offset: 0x00331220
		private void GetCollidingPenetration(GameObject obj, int Force, out int Bonus, out int MaxBonus)
		{
			Bonus = 0;
			MaxBonus = 0;
			ThrownWeapon part = obj.GetPart<ThrownWeapon>();
			MeleeWeapon part2 = obj.GetPart<MeleeWeapon>();
			if (part != null)
			{
				Bonus = part.Penetration;
				MaxBonus = Bonus * 2;
			}
			else if (part2 != null)
			{
				Bonus = (4 + part2.PenBonus) / 2;
				MaxBonus = Bonus + part2.MaxStrengthBonus;
			}
			else
			{
				Bonus = 2;
				MaxBonus = 4;
			}
			Bonus += Stat.GetScoreModifier(Force / 15);
			MaxBonus += obj.Weight / 50;
			if (Bonus > MaxBonus)
			{
				Bonus = MaxBonus;
			}
			if (Bonus < 0)
			{
				Bonus = 0;
			}
		}

		// Token: 0x06008ECF RID: 36559 RVA: 0x003330AC File Offset: 0x003312AC
		private void GetCollidedWithPenetration(GameObject obj, int Force, out int Bonus, out int MaxBonus)
		{
			Bonus = 0;
			MaxBonus = 0;
			MeleeWeapon part = obj.GetPart<MeleeWeapon>();
			if (part != null)
			{
				Bonus = (4 + part.PenBonus) / 2;
				MaxBonus = Bonus + part.MaxStrengthBonus;
			}
			else
			{
				Bonus = 2;
				MaxBonus = 4;
			}
			Bonus += Stat.GetScoreModifier(Force / 15);
			MaxBonus += obj.Weight / 50;
			if (Bonus > MaxBonus)
			{
				Bonus = MaxBonus;
			}
			if (Bonus < 0)
			{
				Bonus = 0;
			}
		}

		// Token: 0x06008ED0 RID: 36560 RVA: 0x00333120 File Offset: 0x00331320
		private bool CollidesWith(GameObject obj, ref int myMatterPhase)
		{
			if (!this.IsReal)
			{
				return false;
			}
			if (obj == this.ParentObject)
			{
				return false;
			}
			if (!GameObject.Validate(ref obj) || !GameObject.Validate(this.ParentObject))
			{
				return false;
			}
			if (obj.IsScenery)
			{
				return false;
			}
			if (obj.IsInGraveyard() || this.ParentObject.IsInGraveyard())
			{
				return false;
			}
			if (obj.GetMatterPhase() >= 3)
			{
				return false;
			}
			if (myMatterPhase == 0)
			{
				myMatterPhase = this.ParentObject.GetMatterPhase();
			}
			if (myMatterPhase >= 3)
			{
				return false;
			}
			if (obj.HasTag("ExcavatoryTerrainFeature"))
			{
				return false;
			}
			bool flag = false;
			if (obj.ConsiderSolidFor(this.ParentObject, false) || this.ParentObject.ConsiderSolidFor(obj, false))
			{
				flag = true;
			}
			else if (obj.IsCombat)
			{
				if (this.ParentObject.IsCombat)
				{
					flag = true;
				}
				else if (Stat.Random(1, 20) > Stats.GetCombatDV(obj))
				{
					flag = true;
				}
			}
			else if (obj.IsReal && Math.Min(20 + this.ParentObject.Weight + obj.Weight, 80).in100() && Stat.Random(1, 20) > Stats.GetCombatDV(obj))
			{
				flag = true;
			}
			return flag && obj.PhaseMatches(this.ParentObject) && (obj.FlightMatches(this.ParentObject) || 50.in100());
		}

		// Token: 0x06008ED1 RID: 36561 RVA: 0x00333268 File Offset: 0x00331468
		public bool CollidesWith(GameObject obj)
		{
			int num = 0;
			return this.CollidesWith(obj, ref num);
		}

		// Token: 0x06008ED2 RID: 36562 RVA: 0x00333280 File Offset: 0x00331480
		public static void ApplyExplosion(Cell C, int Force, List<Cell> UsedCells = null, List<GameObject> Hit = null, bool Local = true, bool Show = true, GameObject Owner = null, string BonusDamage = null, int Phase = 1, bool Neutron = false, bool Indirect = false, float DamageModifier = 1f, GameObject WhatExploded = null)
		{
			TextConsole textConsole = Look._TextConsole;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			if (Show && Options.UseParticleVFX)
			{
				TextConsole.LoadScrapBuffers();
				The.Core.RenderMapToBuffer(scrapBuffer);
			}
			if (UsedCells == null)
			{
				UsedCells = Event.NewCellList();
			}
			if (Hit == null)
			{
				Hit = Event.NewGameObjectList();
			}
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			CleanQueue<int> cleanQueue2 = new CleanQueue<int>();
			CleanQueue<string> cleanQueue3 = new CleanQueue<string>();
			cleanQueue.Enqueue(C);
			cleanQueue2.Enqueue(Force);
			cleanQueue3.Enqueue(".");
			UsedCells.Add(C);
			if (Options.UseParticleVFX && C != null && C.IsVisible() && !Neutron)
			{
				CombatJuice.playPrefabAnimation(C.Location, "Impacts/ImpactVFXExplosion", null, null, null, true);
			}
			if (Options.UseParticleVFX && C != null && C.IsVisible() && Neutron)
			{
				CombatJuice.playPrefabAnimation(C.Location, "Impacts/ImpactVFXNeutronImpact", null, null, null, true);
			}
			int num = 20 - Force / 1000;
			while (cleanQueue.Count > 0)
			{
				Cell cell = cleanQueue.Dequeue();
				if (cell != null)
				{
					if (Options.UseParticleVFX && cell.IsVisible() && !Neutron)
					{
						CombatJuice.playPrefabAnimation(cell.Location, "Impacts/ImpactVFXExplosion", null, null, null, true);
					}
					if (Options.UseParticleVFX && cell.IsVisible() && Neutron)
					{
						CombatJuice.playPrefabAnimation(cell.Location, "Impacts/ImpactVFXNeutronImpact", null, null, null, true);
					}
					cell.WakeCreaturesInArea();
					int num2 = cleanQueue2.Dequeue();
					string text = cleanQueue3.Dequeue();
					List<Cell> list;
					List<GameObject> list2;
					if (cell.OnWorldMap())
					{
						list = new List<Cell>(0);
						list2 = Event.NewGameObjectList();
						if (cell.Objects.Contains(The.Player))
						{
							list2.Add(The.Player);
						}
					}
					else
					{
						if (Local)
						{
							list = cell.GetLocalAdjacentCells(1, false);
						}
						else
						{
							list = cell.GetAdjacentCells(true);
						}
						for (int i = 0; i < UsedCells.Count; i++)
						{
							list.Remove(UsedCells[i]);
						}
						list2 = cell.GetObjectsWithPartReadonly("Physics");
					}
					int num3 = 0;
					foreach (GameObject gameObject in list2)
					{
						if (!Hit.Contains(gameObject))
						{
							Hit.Add(gameObject);
							int num4 = Physics.ExplosionDamage(gameObject, Owner, C, cell, num2, Phase, BonusDamage, DamageModifier, Neutron, Indirect, WhatExploded, true);
							num3 += num4;
						}
					}
					Random random = new Random();
					for (int j = 0; j < list.Count; j++)
					{
						int num5 = random.Next(0, list.Count);
						Cell cell2 = list[j];
						list[j] = list[num5];
						list[num5] = cell2;
					}
					for (;;)
					{
						IL_0298:
						for (int k = UsedCells.Count - 1; k >= 0; k--)
						{
							Cell cell3 = UsedCells[k];
							if (cell3 == null)
							{
								return;
							}
							if (cell3.ParentZone.IsActive() && !Options.UseParticleVFX)
							{
								scrapBuffer.WriteAt(cell3, "&" + Phase.getRandomExplosionColor(Phase).ToString() + "*", true);
							}
						}
						for (int l = list.Count - 1; l >= 0; l--)
						{
							Cell cell4 = list[l];
							if (cell4 == null)
							{
								return;
							}
							if (cell4.ParentZone.IsActive() && !Options.UseParticleVFX)
							{
								scrapBuffer.WriteAt(cell4, "&" + Phase.getRandomExplosionColor(Phase).ToString() + "*", true);
							}
						}
						if (Show && C.ParentZone != null && C.ParentZone.IsActive() && !Options.UseParticleVFX)
						{
							textConsole.DrawBuffer(scrapBuffer, null, false);
							if (num > 0)
							{
								Thread.Sleep(num);
							}
						}
						for (int m = 0; m < list.Count; m++)
						{
							Cell cell5 = list[m];
							if (!Local || (cell5.X != 0 && cell5.X != 79 && cell5.Y != 0 && cell5.Y != 24))
							{
								foreach (GameObject gameObject2 in cell5.GetObjectsWithPartReadonly("Physics"))
								{
									if (!Hit.Contains(gameObject2))
									{
										Hit.Add(gameObject2);
										int num6 = Physics.ExplosionDamage(gameObject2, Owner, C, cell, num2, Phase, BonusDamage, DamageModifier, Neutron, Indirect, WhatExploded, true);
										if (num6 > 0)
										{
											num3 += num6;
											if (num6 > num2)
											{
												list.Remove(cell5);
												goto IL_0298;
											}
											if (Physics.IsMoveable(gameObject2, true))
											{
												gameObject2.Move((text == ".") ? Directions.GetRandomDirection() : text, true, false, false, false, true, true, null, null, false, null, "Explosion", null, false, false, null, null, 0);
											}
										}
									}
								}
								if (cell5.IsSolid())
								{
									list.Remove(cell5);
									goto IL_0298;
								}
							}
						}
						goto Block_42;
					}
					return;
					Block_42:
					if (list.Count <= 0)
					{
						continue;
					}
					int num7 = (num2 - num3) / list.Count;
					if (num7 > 100)
					{
						foreach (Cell cell6 in list)
						{
							if (cell6 != null && !UsedCells.Contains(cell6))
							{
								UsedCells.Add(cell6);
								cleanQueue.Enqueue(cell6);
								cleanQueue2.Enqueue(num7);
								cleanQueue3.Enqueue(cell.GetDirectionFromCell(cell6, false));
							}
						}
						continue;
					}
					continue;
				}
			}
		}

		// Token: 0x06008ED3 RID: 36563 RVA: 0x00333834 File Offset: 0x00331A34
		private static int ExplosionDamage(GameObject GO, GameObject Owner, Cell C, Cell CurrentC, int CurrentForce, int Phase, string BonusDamage, float DamageModifier, bool Neutron, bool Indirect, GameObject WhatExploded, bool TrackResistance = true)
		{
			int num = 0;
			if (GO.PhaseMatches(Phase))
			{
				num = GO.GetKineticResistance();
				int num2 = (int)(DamageModifier * (float)CurrentForce / 250f);
				bool flag = CurrentC != C;
				if (!BonusDamage.IsNullOrEmpty())
				{
					num2 += BonusDamage.RollCached();
				}
				string text;
				string text2;
				string text3;
				if (Neutron)
				{
					text = "from being crushed under the weight of a thousand suns.";
					text2 = "You were crushed under the weight of a thousand suns.";
					text3 = GO.It + GO.GetVerb("were", true, false, false, true, false) + " @@crushed under the weight of a thousand suns.";
				}
				else
				{
					text = "from %t explosion!";
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
					stringBuilder.Append("You");
					stringBuilder2.Append(GO.It);
					if (WhatExploded == GO)
					{
						stringBuilder.Append(" exploded");
						stringBuilder2.Append(" @@exploded");
					}
					else
					{
						stringBuilder.Append(" died in ");
						stringBuilder2.Append(" @@died in ");
						if (WhatExploded != null)
						{
							stringBuilder.Append("the explosion of ").Append(WhatExploded.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
							stringBuilder2.Append("the explosion of ").Append(WhatExploded.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
						}
						else
						{
							stringBuilder.Append("an explosion");
							stringBuilder2.Append("an explosion");
						}
					}
					if (Owner != null && Owner != WhatExploded)
					{
						if (Owner == GO)
						{
							if (WhatExploded != null)
							{
								stringBuilder.Append(", which you caused");
								stringBuilder2.Append(", which ").Append(GO.it).Append(" caused");
							}
							else
							{
								stringBuilder.Append(" you caused");
								stringBuilder2.Append(' ').Append(GO.it).Append(" caused");
							}
						}
						else if (GO.IsPlayer() && Owner.GetStringProperty("PlayerCopy", null) == "true" && Owner.HasStringProperty("PlayerCopyDescription"))
						{
							stringBuilder.Append(" caused by ").Append(Owner.GetStringProperty("PlayerCopyDescription", null));
							stringBuilder2.Append(" caused by ").Append(Owner.GetStringProperty("PlayerCopyDescription", null));
						}
						else
						{
							stringBuilder.Append(" caused by ").Append(Owner.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
							stringBuilder2.Append(" caused by ").Append(Owner.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
						}
					}
					stringBuilder.Append('.');
					stringBuilder2.Append('.');
					text2 = stringBuilder.ToString();
					text3 = stringBuilder2.ToString();
				}
				int num3 = num2;
				string text4 = (Neutron ? "Neutron Explosion" : "Explosion");
				bool flag2 = flag;
				GO.TakeDamage(num3, text, text4, text2, text3, Owner, null, WhatExploded, null, null, flag2, false, Indirect, false, false, false, false, false, false, Phase, null);
			}
			return num;
		}

		// Token: 0x06008ED4 RID: 36564 RVA: 0x00333B7C File Offset: 0x00331D7C
		public int ApplyDischarge(Cell C, Cell TargetCell, int Voltage, int Damage = 0, string DamageRange = null, DieRoll DamageRoll = null, GameObject Target = null, List<Cell> UsedCells = null, GameObject Owner = null, GameObject Source = null, GameObject DescribeAsFrom = null, GameObject Skip = null, List<GameObject> SkipList = null, bool? SourceVisible = null, string SourceDesc = null, string SourceDirectionTowardTarget = null, int Phase = 0, bool Accidental = false, bool Environmental = false, GameObject Alternate = null, GameObject AlternateAvoidedBecauseObject = null, string AlternateAvoidedBecauseReason = null, bool UsePopups = false)
		{
			int num = 0;
			return this.ApplyDischarge(C, TargetCell, Voltage, ref num, Damage, DamageRange, DamageRoll, Target, UsedCells, Owner, Source, DescribeAsFrom, Skip, SkipList, SourceVisible, SourceDesc, SourceDirectionTowardTarget, Phase, Accidental, Environmental, Alternate, AlternateAvoidedBecauseObject, AlternateAvoidedBecauseReason, 0, UsePopups);
		}

		// Token: 0x06008ED5 RID: 36565 RVA: 0x00333BC0 File Offset: 0x00331DC0
		public int ApplyDischarge(Cell C, Cell TargetCell, int Voltage, ref int Shown, int Damage = 0, string DamageRange = null, DieRoll DamageRoll = null, GameObject Target = null, List<Cell> UsedCells = null, GameObject Owner = null, GameObject Source = null, GameObject DescribeAsFrom = null, GameObject Skip = null, List<GameObject> SkipList = null, bool? SourceVisible = null, string SourceDesc = null, string SourceDirectionTowardTarget = null, int Phase = 0, bool Accidental = false, bool Environmental = false, GameObject Alternate = null, GameObject AlternateAvoidedBecauseObject = null, string AlternateAvoidedBecauseReason = null, int Depth = 0, bool UsePopups = false)
		{
			if (C == null)
			{
				return Shown;
			}
			if (C.ParentZone == null)
			{
				return Shown;
			}
			if (TargetCell == null)
			{
				TargetCell = ((Target != null) ? Target.CurrentCell : null);
			}
			if (TargetCell == null)
			{
				return Shown;
			}
			if (TargetCell.ParentZone == null)
			{
				return Shown;
			}
			if (Damage == 0)
			{
				if (DamageRoll == null && !DamageRange.IsNullOrEmpty())
				{
					DamageRoll = DamageRange.GetCachedDieRoll();
				}
				if (DamageRoll != null)
				{
					Damage = DamageRoll.Resolve();
					if (Depth > 0)
					{
						Damage = Damage * 4 * Depth / (5 * Depth);
					}
				}
			}
			if (Damage <= 0)
			{
				return Shown;
			}
			if (Phase == 0 && Owner != null)
			{
				Phase = Owner.GetPhase();
			}
			if (UsedCells == null)
			{
				UsedCells = Event.NewCellList();
			}
			if (Options.DrawArcs)
			{
				TargetCell.ParticleBlip("&WX", 300, 0L, false);
			}
			List<Point> list;
			int num;
			if (C.IsSameOrAdjacent(TargetCell, true))
			{
				list = null;
				num = 0;
			}
			else
			{
				list = Zone.Line(C.X, C.Y, TargetCell.X, TargetCell.Y, false);
				num = 1;
			}
			int num2 = num;
			while ((list == null) ? (num2 == num) : (num2 < list.Count))
			{
				Cell cell = ((list == null) ? TargetCell : TargetCell.ParentZone.GetCell(list[num2]));
				if (cell.IsVisible())
				{
					cell.ParticleBlip("&" + Phase.getRandomElectricArcColor(Phase).ToString() + ((char)Stat.RandomCosmetic(191, 198)).ToString(), 30, 0L, false);
					cell.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_electric_conductiveJump", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (Target == null && (C.X != cell.X || C.Y != cell.Y || cell == TargetCell))
				{
					GameObject gameObject = ((Owner != null && Depth == 0) ? cell.GetCombatTarget(Owner, false, false, false, Phase, null, null, null, Skip, SkipList, true, false, null) : null);
					if (gameObject != null && gameObject.GetElectricalConductivity(Source, Phase) > 0)
					{
						Target = gameObject;
					}
					if (Target == null)
					{
						int num3 = 0;
						Physics.DischargeObjects.Clear();
						foreach (GameObject gameObject2 in cell.GetObjectsWithPartReadonly("Combat"))
						{
							if (Skip != gameObject2 && (SkipList == null || !SkipList.Contains(gameObject2)))
							{
								int electricalConductivity = gameObject2.GetElectricalConductivity(Source, Phase);
								if (electricalConductivity > num3)
								{
									Physics.DischargeObjects.Clear();
									Physics.DischargeObjects.Add(gameObject2);
									num3 = electricalConductivity;
								}
								else if (electricalConductivity == num3)
								{
									Physics.DischargeObjects.Add(gameObject2);
								}
							}
						}
						if (Physics.DischargeObjects.Count == 0)
						{
							foreach (GameObject gameObject3 in cell.GetObjectsWithPartReadonly("Physics"))
							{
								if (gameObject3.IsReal && gameObject3.IsScenery && Skip != gameObject3 && (SkipList == null || !SkipList.Contains(gameObject3)))
								{
									int electricalConductivity2 = gameObject3.GetElectricalConductivity(Source, Phase);
									if (electricalConductivity2 > num3)
									{
										Physics.DischargeObjects.Clear();
										Physics.DischargeObjects.Add(gameObject3);
										num3 = electricalConductivity2;
									}
									else if (electricalConductivity2 == num3)
									{
										Physics.DischargeObjects.Add(gameObject3);
									}
								}
							}
						}
						Target = Physics.DischargeObjects.GetRandomElement<GameObject>(null);
						Physics.DischargeObjects.Clear();
					}
				}
				bool flag = false;
				if (Target != null)
				{
					flag = Target.PhaseMatches(Phase);
					if (!UsedCells.Contains(C))
					{
						UsedCells.Add(C);
					}
					if (!UsedCells.Contains(cell))
					{
						UsedCells.Add(cell);
					}
					string text = ((Shown == 0) ? "An" : "The");
					string text2 = null;
					bool flag2 = false;
					if (GameObject.Validate(Source))
					{
						if (Shown == 0 && IComponent<GameObject>.Visible(Source) && IComponent<GameObject>.Visible(Target))
						{
							text2 = string.Concat(new string[]
							{
								text,
								" {{electrical|electrical arc}} leaps from ",
								Source.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" toward ",
								Target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								Target.IsPlayer() ? "" : (" " + Source.DescribeDirectionToward(Target, false, false))
							});
							Shown++;
							flag2 = true;
						}
						else if (IComponent<GameObject>.Visible(Target))
						{
							text2 = text + " {{electrical|electrical arc}} leaps toward " + Target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + (Target.IsPlayer() ? "" : (" " + Source.DescribeDirectionToward(Target, false, false)));
							Shown++;
						}
						else if (IComponent<GameObject>.Visible(Source))
						{
							text2 = string.Concat(new string[]
							{
								text,
								" {{electrical|electrical arc}} leaps from ",
								Source.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" toward something ",
								Source.DescribeDirectionToward(Target, false, false)
							});
							Shown++;
							flag2 = true;
						}
					}
					else if (!SourceDesc.IsNullOrEmpty() && !SourceDirectionTowardTarget.IsNullOrEmpty())
					{
						if (Shown == 0)
						{
							bool? flag3 = SourceVisible;
							bool flag4 = true;
							if (((flag3.GetValueOrDefault() == flag4) & (flag3 != null)) && IComponent<GameObject>.Visible(Target))
							{
								text2 = string.Concat(new string[]
								{
									text,
									" {{electrical|electrical arc}} leaps from ",
									SourceDesc,
									" toward ",
									Target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
									" ",
									SourceDirectionTowardTarget
								});
								Shown++;
								goto IL_06FC;
							}
						}
						if (IComponent<GameObject>.Visible(Target))
						{
							text2 = string.Concat(new string[]
							{
								text,
								" {{electrical|electrical arc}} leaps toward ",
								Target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" ",
								SourceDirectionTowardTarget
							});
							Shown++;
						}
						else
						{
							bool? flag3 = SourceVisible;
							bool flag4 = true;
							if ((flag3.GetValueOrDefault() == flag4) & (flag3 != null))
							{
								text2 = string.Concat(new string[] { text, " {{electrical|electrical arc}} leaps from ", SourceDesc, " toward something ", SourceDirectionTowardTarget });
								Shown++;
							}
						}
					}
					else if (IComponent<GameObject>.Visible(Target))
					{
						text2 = text + " {{electrical|electrical arc}} leaps toward " + Target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
						Shown++;
					}
					IL_06FC:
					if (!text2.IsNullOrEmpty())
					{
						if (GameObject.Validate(Alternate) && IComponent<GameObject>.Visible(Alternate))
						{
							text2 = text2 + ", avoiding " + Alternate.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
							if (flag2 && GameObject.Validate(Source))
							{
								text2 = text2 + " " + Source.DescribeRelativeDirectionToward(Alternate, false);
							}
							if (GameObject.Validate(AlternateAvoidedBecauseObject))
							{
								if (AlternateAvoidedBecauseObject.GetObjectContext() == Alternate)
								{
									text2 = text2 + " because of " + Alternate.its_(AlternateAvoidedBecauseObject, false);
								}
								else
								{
									text2 = text2 + " because of " + AlternateAvoidedBecauseObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false);
								}
							}
							else if (!AlternateAvoidedBecauseReason.IsNullOrEmpty())
							{
								text2 = text2 + " because of " + AlternateAvoidedBecauseReason;
							}
						}
						if (!flag && IComponent<GameObject>.Visible(Target))
						{
							text2 = text2 + ", but passes through " + Target.them;
						}
						text2 += "!";
						if (UsePopups)
						{
							Popup.Show(text2, null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(text2, null, true);
						}
					}
					bool flag5 = false;
					int num4 = 0;
					GameObject gameObject4 = null;
					bool flag6 = false;
					string text3 = null;
					string text4 = null;
					GameObject gameObject5 = null;
					int num5 = 0;
					GameObject gameObject6 = null;
					string text5 = null;
					if (Voltage > 1)
					{
						if (DamageRoll == null && DamageRange.IsNullOrEmpty())
						{
							num4 = Damage * 4 / 5;
							flag5 = num4 > 0;
						}
						else
						{
							num4 = 0;
							flag5 = true;
						}
						if (flag5)
						{
							GameObject gameObject7 = null;
							GameObject gameObject8 = null;
							string text6 = null;
							int num6 = 0;
							List<Cell> adjacentCells = cell.GetAdjacentCells(true);
							foreach (Cell cell2 in UsedCells)
							{
								adjacentCells.Remove(cell2);
							}
							if (adjacentCells.Count > 0)
							{
								Physics.DischargeObjects.Clear();
								foreach (Cell cell3 in adjacentCells)
								{
									foreach (GameObject gameObject9 in cell3.GetObjectsWithPartReadonly("Physics"))
									{
										GameObject gameObject10;
										string text7;
										int electricalConductivity3 = gameObject9.GetElectricalConductivity(out gameObject10, out text7, Source, Phase);
										if (electricalConductivity3 > num6)
										{
											if (gameObject7 != null && (gameObject5 == null || num6 > num5) && (gameObject8 != null || !text6.IsNullOrEmpty()))
											{
												gameObject5 = gameObject7;
												num5 = num6;
												gameObject6 = gameObject8;
												text5 = text6;
											}
											gameObject7 = gameObject9;
											num6 = electricalConductivity3;
											gameObject8 = gameObject10;
											text6 = text7;
											Physics.DischargeObjects.Clear();
											Physics.DischargeObjects.Add(gameObject9);
										}
										else if (electricalConductivity3 == num6)
										{
											if (gameObject9.IsCombatObject() && (gameObject7 == null || !gameObject7.IsCombatObject()) && (gameObject10 != null || !text7.IsNullOrEmpty()))
											{
												gameObject7 = gameObject9;
												num6 = electricalConductivity3;
												gameObject8 = gameObject10;
												text6 = text7;
											}
											Physics.DischargeObjects.Add(gameObject9);
										}
										else if ((gameObject5 == null || electricalConductivity3 > num5) && (gameObject10 != null || !text7.IsNullOrEmpty()))
										{
											gameObject5 = gameObject9;
											num5 = electricalConductivity3;
											gameObject6 = gameObject10;
											text5 = text7;
										}
									}
								}
								gameObject4 = Physics.DischargeObjects.GetRandomElement<GameObject>(null);
								Physics.DischargeObjects.Clear();
								if (gameObject4 != null)
								{
									flag6 = IComponent<GameObject>.Visible(Target);
									text3 = Target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
									text4 = Target.DescribeDirectionToward(gameObject4, false, false);
								}
							}
						}
					}
					if (flag)
					{
						GameObject gameObject11 = Target;
						int num7 = Damage;
						string text8 = "from %t electrical discharge!";
						string text9 = "Electric Shock";
						string text10 = null;
						string text11 = null;
						int num8 = Phase;
						if (gameObject11.TakeDamage(num7, text8, text9, text10, text11, Owner, Owner ?? Source, Source, null, DescribeAsFrom, Accidental, Environmental, false, false, false, false, false, false, UsePopups, num8, null))
						{
							for (int i = 0; i < 3; i++)
							{
								Target.ParticleText("&" + Phase.getRandomElectricArcColor(Phase).ToString() + "ú", false);
								Target.PlayWorldSound("Sounds/Enhancements/sfx_enhancement_electric_conductiveJump", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							}
							if (IComponent<GameObject>.Visible(Target))
							{
								Shown++;
							}
						}
					}
					if (!flag5 || gameObject4 == null)
					{
						break;
					}
					Cell cell4 = ((gameObject4 != null) ? gameObject4.CurrentCell : null);
					if (cell4 != null)
					{
						Cell cell5 = TargetCell;
						GameObject gameObject12 = gameObject4;
						Cell cell6 = cell4;
						int num9 = Voltage - 1;
						int num10 = num4;
						DieRoll dieRoll = DamageRoll;
						GameObject gameObject13 = gameObject12;
						List<Cell> list2 = UsedCells;
						GameObject gameObject14 = Target;
						GameObject gameObject15 = null;
						List<GameObject> list3 = null;
						bool? flag7 = new bool?(flag6);
						string text12 = text3;
						string text13 = text4;
						int num11 = Phase;
						bool flag8 = true;
						int num8 = Depth + 1;
						this.ApplyDischarge(cell5, cell6, num9, ref Shown, num10, DamageRange, dieRoll, gameObject13, list2, Owner, gameObject14, DescribeAsFrom, gameObject15, list3, flag7, text12, text13, num11, flag8, Environmental, gameObject5, gameObject6, text5, num8, UsePopups);
						break;
					}
					break;
				}
				else
				{
					num2++;
				}
			}
			return Shown;
		}

		// Token: 0x06008ED6 RID: 36566 RVA: 0x00334824 File Offset: 0x00332A24
		public override bool RenderTile(ConsoleChar E)
		{
			return true;
		}

		// Token: 0x06008ED7 RID: 36567 RVA: 0x00334827 File Offset: 0x00332A27
		private void DoSearching(Cell C, ref Event eSearched)
		{
			if (C.HasObjectWithRegisteredEvent("Searched"))
			{
				if (eSearched == null)
				{
					eSearched = Event.New("Searched", "Searcher", this.ParentObject);
				}
				C.FireEvent(eSearched);
			}
		}

		// Token: 0x06008ED8 RID: 36568 RVA: 0x0033485C File Offset: 0x00332A5C
		public void Search()
		{
			Event @event = null;
			this.DoSearching(this.CurrentCell, ref @event);
			List<Cell> localAdjacentCells = this.CurrentCell.GetLocalAdjacentCells();
			int i = 0;
			int count = localAdjacentCells.Count;
			while (i < count)
			{
				this.DoSearching(localAdjacentCells[i], ref @event);
				i++;
			}
		}

		// Token: 0x06008ED9 RID: 36569 RVA: 0x003348A8 File Offset: 0x00332AA8
		public bool EnterCell(Cell C)
		{
			if (this._CurrentCell == C)
			{
				return false;
			}
			if (!this.ParentObject.IsValid())
			{
				MetricsManager.LogError("Attempting to add invalid object '" + this.ParentObject.DebugName + "' to cell.");
				return false;
			}
			if (this.ParentObject.IsInGraveyard())
			{
				MetricsManager.LogError("Attempting to add graveyard object '" + this.ParentObject.DebugName + "' to cell.");
				return false;
			}
			this.CurrentCell = C;
			this.InInventory = null;
			this.Equipped = null;
			EnvironmentalUpdateEvent.Send(this.ParentObject);
			if (base.IsPlayer())
			{
				this.Search();
				if (!IComponent<GameObject>.TerseMessages)
				{
					Physics.PassingBy.Clear();
					int i = 0;
					int count = this.CurrentCell.Objects.Count;
					while (i < count)
					{
						GameObject gameObject = this.CurrentCell.Objects[i];
						if (gameObject != this.ParentObject && !this.SkipInPassingBy(gameObject, this.CurrentCell))
						{
							if (gameObject.HasProperty("SeenMessage"))
							{
								string stringProperty = gameObject.GetStringProperty("SeenMessage", null);
								if (!stringProperty.IsNullOrEmpty())
								{
									Physics.PassingBy.Add(stringProperty);
								}
							}
							else
							{
								Physics.PassingBy.Add(gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
							}
						}
						i++;
					}
					if (Physics.PassingBy.Count > 0)
					{
						IComponent<GameObject>.AddPlayerMessage("You pass by " + Grammar.MakeAndList(Physics.PassingBy, true) + ".", null, true);
					}
				}
			}
			return true;
		}

		// Token: 0x06008EDA RID: 36570 RVA: 0x00334A3C File Offset: 0x00332C3C
		private bool SkipInPassingBy(GameObject obj, Cell C)
		{
			return obj.Render == null || !obj.Render.Visible || obj.HasTagOrProperty("NoPassByMessage") || (obj.IsWadingDepthLiquid() && !C.HasBridge() && obj.PhaseAndFlightMatches(this.ParentObject));
		}

		// Token: 0x06008EDB RID: 36571 RVA: 0x00334A94 File Offset: 0x00332C94
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == PooledEvent<CanBeTradedEvent>.ID || ID == DroppedEvent.ID || ID == EquippedEvent.ID || ID == PooledEvent<EnvironmentalUpdateEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == PooledEvent<GetContextEvent>.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetInventoryCategoryEvent>.ID || ID == GetNavigationWeightEvent.ID || (ID == PooledEvent<GetSlotsRequiredEvent>.ID && this.UsesTwoSlots) || ID == PooledEvent<HasFlammableEquipmentEvent>.ID || ID == PooledEvent<HasFlammableEquipmentOrInventoryEvent>.ID || ID == InventoryActionEvent.ID || ID == LeftCellEvent.ID || ID == ObjectEnteredCellEvent.ID || (ID == ObjectEnteringCellEvent.ID && this.Solid) || (ID == PooledEvent<OkayToDamageEvent>.ID && !this.Owner.IsNullOrEmpty()) || ID == QueryEquippableListEvent.ID || ID == RemoveFromContextEvent.ID || ID == PooledEvent<ReplaceInContextEvent>.ID || ID == PooledEvent<StatChangeEvent>.ID || ID == SuspendingEvent.ID || ID == TakenEvent.ID || ID == TryRemoveFromContextEvent.ID || ID == UnequippedEvent.ID || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x06008EDC RID: 36572 RVA: 0x00334BCA File Offset: 0x00332DCA
		public override bool HandleEvent(OkayToDamageEvent E)
		{
			return this.Owner.IsNullOrEmpty() && base.HandleEvent(E);
		}

		// Token: 0x06008EDD RID: 36573 RVA: 0x00334BE2 File Offset: 0x00332DE2
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.LastDamagedBy = null;
			this.LastWeaponDamagedBy = null;
			this.LastProjectileDamagedBy = null;
			this.InflamedBy = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008EDE RID: 36574 RVA: 0x00334C07 File Offset: 0x00332E07
		public override bool HandleEvent(CanBeTradedEvent E)
		{
			return !this.IsAflame() && base.HandleEvent(E);
		}

		// Token: 0x06008EDF RID: 36575 RVA: 0x00334C1A File Offset: 0x00332E1A
		public override bool HandleEvent(EnvironmentalUpdateEvent E)
		{
			this.AmbientCache = int.MinValue;
			return base.HandleEvent(E);
		}

		// Token: 0x06008EE0 RID: 36576 RVA: 0x00334C2E File Offset: 0x00332E2E
		public override bool HandleEvent(HasFlammableEquipmentEvent E)
		{
			return (E.Object == this.ParentObject || this.FlameTemperature > E.Temperature) && base.HandleEvent(E);
		}

		// Token: 0x06008EE1 RID: 36577 RVA: 0x00334C55 File Offset: 0x00332E55
		public override bool HandleEvent(HasFlammableEquipmentOrInventoryEvent E)
		{
			return (E.Object == this.ParentObject || this.FlameTemperature > E.Temperature) && base.HandleEvent(E);
		}

		// Token: 0x06008EE2 RID: 36578 RVA: 0x00334C7C File Offset: 0x00332E7C
		public override bool HandleEvent(GetContextEvent E)
		{
			if (this._CurrentCell != null)
			{
				E.CellContext = this._CurrentCell;
				E.Relation = 1;
				E.RelationManager = this;
				return false;
			}
			if (GameObject.Validate(ref this._InInventory))
			{
				E.ObjectContext = this._InInventory;
				E.Relation = 2;
				E.RelationManager = this;
				return false;
			}
			if (GameObject.Validate(ref this._Equipped))
			{
				E.ObjectContext = this._Equipped;
				E.BodyPartContext = this._Equipped.FindEquippedObject(this.ParentObject);
				E.Relation = 3;
				E.RelationManager = this;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EE3 RID: 36579 RVA: 0x00334D20 File Offset: 0x00332F20
		public override bool HandleEvent(ReplaceInContextEvent E)
		{
			if (GameObject.Validate(ref this._InInventory))
			{
				try
				{
					Inventory inventory = this._InInventory.Inventory;
					if (inventory != null)
					{
						inventory.RemoveObject(this.ParentObject, null);
						inventory.ParentObject.ReceiveObject(E.Replacement, false, null);
					}
				}
				catch
				{
				}
			}
			if (GameObject.Validate(ref this._Equipped))
			{
				try
				{
					this.ParentObject.SplitFromStack();
					GameObject equipped = this._Equipped;
					BodyPart bodyPart = equipped.FindEquippedObject(this.ParentObject);
					if (bodyPart != null)
					{
						bodyPart.ForceUnequip(true, false, false);
						equipped.FireEvent(Event.New("CommandEquipObject", "Object", E.Replacement, "BodyPart", bodyPart));
					}
				}
				catch
				{
				}
			}
			if (this._CurrentCell != null)
			{
				try
				{
					Cell currentCell = this._CurrentCell;
					this._CurrentCell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					currentCell.AddObject(E.Replacement, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				catch
				{
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EE4 RID: 36580 RVA: 0x00334E50 File Offset: 0x00333050
		public override bool HandleEvent(RemoveFromContextEvent E)
		{
			if (GameObject.Validate(ref this._InInventory))
			{
				try
				{
					Inventory inventory = this._InInventory.Inventory;
					if (inventory != null)
					{
						inventory.RemoveObject(this.ParentObject, E);
					}
					this._InInventory = null;
				}
				catch
				{
				}
			}
			if (GameObject.Validate(ref this._Equipped))
			{
				try
				{
					BodyPart bodyPart = this._Equipped.FindEquippedObject(this.ParentObject);
					if (bodyPart != null)
					{
						bodyPart.ForceUnequip(true, false, true);
						if (bodyPart.Equipped == this.ParentObject)
						{
							bodyPart.Unequip();
						}
					}
					this.Equipped = null;
				}
				catch
				{
					this._Equipped = null;
				}
			}
			if (this._CurrentCell != null)
			{
				try
				{
					this._CurrentCell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, E);
					this._CurrentCell = null;
				}
				catch
				{
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EE5 RID: 36581 RVA: 0x00334F48 File Offset: 0x00333148
		public override bool HandleEvent(TryRemoveFromContextEvent E)
		{
			if (GameObject.Validate(ref this._InInventory))
			{
				Inventory inventory = this._InInventory.Inventory;
				if (inventory == null || !inventory.FireEvent(Event.New("CommandRemoveObject", "Object", this.ParentObject).SetSilent(true), E))
				{
					return false;
				}
				if (this._InInventory != null)
				{
					return false;
				}
			}
			if (GameObject.Validate(ref this._Equipped))
			{
				BodyPart bodyPart = this._Equipped.FindEquippedObject(this.ParentObject);
				if (bodyPart != null)
				{
					bodyPart.TryUnequip(true, false, false, true);
				}
				if (this._Equipped != null)
				{
					return false;
				}
			}
			if (this._CurrentCell != null)
			{
				if (!this._CurrentCell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, E))
				{
					return false;
				}
				if (this._CurrentCell != null)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EE6 RID: 36582 RVA: 0x00335014 File Offset: 0x00333214
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			bool flag = true;
			if (this.Solid && !E.IgnoresWalls && !E.WallWalker && (E.PhaseMatches(this.ParentObject) || (E.Autoexploring && Phase.IsTemporarilyPhasedOut(E.Actor))) && (!E.Flying || !this.ParentObject.HasTag("Flyover")) && !this.ParentObject.IsCombat)
			{
				flag = false;
				if (E.Smart)
				{
					Forcefield forcefield;
					if (this.ParentObject.TryGetPart<Forcefield>(out forcefield))
					{
						E.Uncacheable = true;
						if (forcefield.CanPass(E.Actor))
						{
							flag = true;
						}
					}
					Door door;
					if (!flag && this.ParentObject.TryGetPart<Door>(out door))
					{
						E.Uncacheable = true;
						if (door.CanPathThrough(E.Actor))
						{
							flag = true;
						}
					}
				}
				if (!flag && E.Burrower && this.ParentObject.IsDiggable())
				{
					bool flag2;
					if (OkayToDamageEvent.Check(this.ParentObject, E.Actor, out flag2))
					{
						flag = true;
						int num = 4 + this.ParentObject.Stat("AV", 0);
						num = GenericDeepRatingEvent.GetFor(this.ParentObject, "WallDigNavigationWeight", E.Actor, null, 0, num);
						E.MinWeight(num);
						E.MinSocialWeight(100);
					}
					if (flag2)
					{
						E.Uncacheable = true;
					}
				}
			}
			if (!flag)
			{
				E.MinWeight(100);
				return false;
			}
			if (this.Temperature > 200)
			{
				E.Uncacheable = true;
				int num2 = this.Temperature / 20;
				if (E.Smart && E.Actor != null)
				{
					int num3 = E.Actor.Stat("HeatResistance", 0);
					if (num3 != 0)
					{
						num2 = num2 * (100 - num3) / 100;
					}
				}
				E.MinWeight(num2, 98);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EE7 RID: 36583 RVA: 0x003351DE File Offset: 0x003333DE
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.CurrentCell = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008EE8 RID: 36584 RVA: 0x003351EE File Offset: 0x003333EE
		public override bool HandleEvent(GetSlotsRequiredEvent E)
		{
			if (E.Object == this.ParentObject && this.UsesTwoSlots)
			{
				E.Increases++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EE9 RID: 36585 RVA: 0x0033521C File Offset: 0x0033341C
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E != null)
			{
				GameObject @object = E.Object;
				bool? flag = ((@object != null) ? new bool?(@object.IsPlayer()) : null);
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					E.Object.PlayCombatSoundTag("StepSound", null, null, 0.25f);
				}
			}
			return true;
		}

		// Token: 0x06008EEA RID: 36586 RVA: 0x0033527C File Offset: 0x0033347C
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			if (this.Solid && !E.Object.HasTagOrProperty("IgnoresWalls"))
			{
				Brain brain = E.Object.Brain;
				if ((brain == null || !brain.WallWalker || E.Object.IsFlying) && (!E.Object.IsPlayer() || !The.Core.IDKFA) && (!this.ParentObject.HasPropertyOrTag("Flyover") || !E.Object.IsFlying) && E.Object.PhaseMatches(this.ParentObject) && (E.Ignore == null || E.Ignore != this.ParentObject))
				{
					Physics.eBeforePhysicsRejectObjectEntringCell.SetParameter("Object", E.Object);
					if (this.ParentObject.FireEvent(Physics.eBeforePhysicsRejectObjectEntringCell) && this.Solid && E.Object.FireEvent(Event.New("ObjectEnteringCellBlockedBySolid", "Object", this.ParentObject)) && this.Solid)
					{
						if (E.Object.IsPlayer() && !this.ParentObject.HasTagOrProperty("NoBlockMessage"))
						{
							if (E.Forced)
							{
								string text = "OUCH! You collide with " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".";
								if (The.Game.Player.Messages.LastLine != text)
								{
									IComponent<GameObject>.AddPlayerMessage(text, null, true);
								}
							}
							else if (E.Object.OnWorldMap())
							{
								if (this.ParentObject.HasTag("OverlandBlockMessage"))
								{
									IComponent<GameObject>.AddPlayerMessage(this.ParentObject.GetTag("OverlandBlockMessage", null), null, true);
								}
								else
								{
									IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " too difficult to traverse via the world map. You'll have to find your way on the surface.", null, true);
								}
							}
							else
							{
								IComponent<GameObject>.AddPlayerMessage("The way is blocked by " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", null, true);
							}
						}
						E.Blocking = this.ParentObject;
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EEB RID: 36587 RVA: 0x0033550C File Offset: 0x0033370C
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType == "Thrown Weapon" && E.Item == this.ParentObject && !E.RequireDesirable && !E.List.Contains(E.Item))
			{
				if (!E.RequirePossible)
				{
					E.List.Add(E.Item);
				}
				else if (E.Actor.IsGiganticCreature)
				{
					if (E.Item.IsGiganticEquipment || E.Item.HasPropertyOrTag("GiganticEquippable") || E.Item.IsNatural())
					{
						E.List.Add(E.Item);
					}
				}
				else if (!E.Item.IsGiganticEquipment || E.Item.IsNatural())
				{
					E.List.Add(E.Item);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EEC RID: 36588 RVA: 0x003355FC File Offset: 0x003337FC
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (!this.ParentObject.IsCombat)
			{
				if (this.Equipped != null)
				{
					if (!this.ParentObject.IsNatural() && !base.HasPropertyOrTag("NoRemoveOptionInInventory"))
					{
						E.AddAction("Remove", "remove", "Unequip", null, 'r', true, 10, 0, false, false, false, false, true, null, false);
					}
				}
				else if (this.Takeable)
				{
					bool flag = true;
					if (this.InInventory == null || !this.InInventory.IsPlayer())
					{
						if (this.ParentObject.IsTakeable())
						{
							E.AddAction("Get", "get", "CommandTakeObject", null, 'g', true, 30, 0, false, false, false, false, false, null, false);
						}
						else
						{
							flag = false;
						}
					}
					else if (!this.ParentObject.HasTagOrProperty("CannotDrop") && !The.Player.OnWorldMap())
					{
						E.AddAction("Drop", "drop", "CommandDropObject", null, 'd', true, 0, 0, false, false, false, false, true, null, false);
						if (Options.DropAll)
						{
							E.AddAction("DropAll", "drop all", "CommandDropAllObject", null, 'D', true, 0, 0, false, false, false, false, true, null, false);
						}
					}
					if (flag && !this.ParentObject.HasTagOrProperty("CannotEquip"))
					{
						E.AddAction("AutoEquip", "equip (auto)", "CommandAutoEquipObject", null, 'e', true, GetAutoEquipPriorityEvent.GetFor(this.ParentObject, 10), 0, false, false, false, false, false, null, false);
						E.AddAction("DoEquip", "equip (manual)", "CommandEquipObject", null, 'E', true, 0, 0, false, false, false, false, false, null, false);
					}
				}
			}
			if (this.IsAflame())
			{
				E.AddAction("Firefight", "fight fire", "FightFire", null, 'f', false, 40, 0, false, false, false, false, true, null, false);
			}
			if (this.CurrentCell != null && this.IsReal && this.ParentObject.HasStat("Hitpoints"))
			{
				E.AddAction("Attack", "attack", Physics.ATTACK_COMMAND, null, 'k', false, -10, 0, false, false, false, false, true, null, false);
			}
			GameObject equipped = this.Equipped;
			if (equipped == null || !equipped.IsPlayer())
			{
				GameObject inInventory = this.InInventory;
				if (inInventory == null || !inInventory.IsPlayer())
				{
					goto IL_02F9;
				}
			}
			if (!this.ParentObject.IsNatural() && !The.Player.IsConfused)
			{
				if (ItemModding.CanMod(this.ParentObject, The.Player))
				{
					E.AddAction("Mod", "mod with tinkering", "Mod", "tinkering", 't', false, 0, 0, false, false, false, false, true, null, true);
				}
				E.AddAction("Add Notes", "add notes", "AddNotes", null, 'n', false, 0, 0, false, false, false, false, true, null, false);
				if (this.ParentObject.IsImportant())
				{
					E.AddAction("Mark Unimportant", "mark unimportant", "MarkUnimportant", null, 'i', false, -1, 0, false, false, false, false, true, null, false);
				}
				else
				{
					E.AddAction("Mark Important", "mark important", "MarkImportant", null, 'i', false, -1, 0, false, false, false, false, true, null, false);
				}
			}
			IL_02F9:
			if (this.ParentObject.HasPart<Notes>() && !The.Player.IsConfused)
			{
				E.AddAction("Remove Notes", "remove notes", "RemoveNotes", null, 'n', false, 0, 0, false, true, false, false, true, null, false);
			}
			if (CheckAnythingToCleanEvent.Check(this.ParentObject, null) && CheckAnythingToCleanWithEvent.Check(The.Player, this.ParentObject))
			{
				E.AddAction("Clean", "clean", "CleanItem", null, 'a', false, -1, 10, false, false, false, false, true, null, false);
			}
			if (Options.DebugInternals)
			{
				E.AddAction("Show Internals", "show internals", "ShowInternals", null, 'W', false, -1, 0, false, true, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EED RID: 36589 RVA: 0x003359B4 File Offset: 0x00333BB4
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Unequip")
			{
				Body body = E.Actor.Body;
				BodyPart bodyPart = ((body != null) ? body.FindEquippedItem(E.Item) : null);
				if (bodyPart != null)
				{
					E.Actor.FireEvent(Event.New("CommandUnequipObject", "BodyPart", bodyPart));
				}
			}
			else if (E.Command == "CleanItem")
			{
				if (!E.Actor.CanMoveExtremities(null, true, false, true))
				{
					return false;
				}
				string cleaningLiquidGeneralization = LiquidVolume.GetCleaningLiquidGeneralization();
				List<GameObject> @for = GetCleaningItemsEvent.GetFor(E.Actor, this.ParentObject);
				if (@for != null && @for.Count > 0)
				{
					IList<GameObject> list = @for;
					string text = null;
					PickItem.PickItemDialogStyle pickItemDialogStyle = PickItem.PickItemDialogStyle.SelectItemDialog;
					string text2 = "[ {{W|Choose where to use a dram of " + cleaningLiquidGeneralization + " from}} ]";
					GameObject gameObject = PickItem.ShowPicker(list, text, pickItemDialogStyle, E.Actor, null, null, text2, false, null, true, true, false);
					if (gameObject != null)
					{
						if (!gameObject.Owner.IsNullOrEmpty() && Popup.ShowYesNoCancel(gameObject.Does("are", 2147483647, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " not owned by you. Are you sure you want to pour from " + gameObject.them + "?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
						{
							return false;
						}
						List<GameObject> list2;
						List<string> list3;
						if (CleanItemsEvent.PerformFor(E.Actor, this.ParentObject, gameObject, out list2, out list3) && list2 != null && list2.Count > 0)
						{
							E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_liquid_clean", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							LiquidVolume.CleaningMessage(E.Actor, list2, list3, gameObject, null, true);
							E.Actor.UseEnergy(1000, "Cleaning", null, null, false);
							E.RequestInterfaceExit();
							if (!gameObject.Owner.IsNullOrEmpty())
							{
								Physics physics = gameObject.Physics;
								if (physics != null)
								{
									physics.BroadcastForHelp(E.Actor);
								}
							}
						}
					}
				}
				else
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"You don't have any ",
						cleaningLiquidGeneralization,
						" to clean ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
						" with."
					}), true, true, true);
				}
			}
			else if (E.Command == "FightFire")
			{
				if (Firefighting.AttemptFirefighting(E.Actor, E.Item, 1000, false, true))
				{
					E.RequestInterfaceExit();
				}
			}
			else if (E.Command == Physics.ATTACK_COMMAND)
			{
				bool flag = true;
				if (E.Actor.IsPlayer() && !this.ParentObject.IsHostileTowards(E.Actor) && this.ParentObject != E.Actor.Target && Popup.ShowYesNo("Do you really want to attack " + ((this.ParentObject == E.Actor) ? E.Actor.itself : this.ParentObject.t(2147483647, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false)) + "?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
				{
					flag = false;
				}
				if (flag)
				{
					Combat.AttackObject(E.Actor, this.ParentObject, null, 0, 0, 0, false);
					E.RequestInterfaceExit();
				}
			}
			else if (E.Command == "Mod")
			{
				if (Options.ModernUI)
				{
					return false;
				}
				ScreenBuffer screenBuffer = ScreenBuffer.create(80, 25);
				screenBuffer.Copy(TextConsole.CurrentBuffer);
				new TinkeringScreen().Show(The.Player, E.Item, E);
				Popup._TextConsole.DrawBuffer(screenBuffer, null, false);
			}
			else if (E.Command == "MarkImportant")
			{
				E.Item.SetImportant(true, true, true);
			}
			else if (E.Command == "MarkUnimportant")
			{
				E.Item.SetImportant(false, true, true);
			}
			else if (E.Command == "AddNotes")
			{
				string text3 = Popup.AskString("Enter notes for " + E.Item.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + ".", "", "Sounds/UI/ui_notification", null, null, 30, 0, false, true, null);
				if (!text3.IsNullOrEmpty())
				{
					Notes notes = E.Item.RequirePart<Notes>(false);
					if (notes.Text.IsNullOrEmpty())
					{
						notes.Text = text3;
					}
					else
					{
						Notes notes2 = notes;
						notes2.Text = notes2.Text + "\n" + text3;
					}
					Popup.Show("Notes added.", null, "Sounds/UI/ui_notification", true, true, true, false, null);
				}
			}
			else if (E.Command == "RemoveNotes")
			{
				Notes part = E.Item.GetPart<Notes>();
				if (part != null)
				{
					this.ParentObject.RemovePart(part);
					Popup.Show("Notes removed.", null, "Sounds/UI/ui_notification", true, true, true, false, null);
				}
				else
				{
					Popup.ShowFail("No notes found.", true, true, true);
				}
			}
			else if (E.Command == "ShowInternals")
			{
				Popup.Show(GetDebugInternalsEvent.GetFor(this.ParentObject), null, "Sounds/UI/ui_notification", true, true, true, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EEE RID: 36590 RVA: 0x00335F42 File Offset: 0x00334142
		public override bool HandleEvent(EquippedEvent E)
		{
			this.Equipped = E.Actor;
			return base.HandleEvent(E);
		}

		// Token: 0x06008EEF RID: 36591 RVA: 0x00335F57 File Offset: 0x00334157
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (this._Equipped == E.Actor)
			{
				this._Equipped = null;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EF0 RID: 36592 RVA: 0x00335F75 File Offset: 0x00334175
		public override bool HandleEvent(TakenEvent E)
		{
			if (E.Actor != null && E.Actor.IsPlayer())
			{
				this.InInventory = E.Actor;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EF1 RID: 36593 RVA: 0x00335F9F File Offset: 0x0033419F
		public override bool HandleEvent(DroppedEvent E)
		{
			this.InInventory = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008EF2 RID: 36594 RVA: 0x00335FB0 File Offset: 0x003341B0
		private bool ShouldUpdateTemperature()
		{
			if (this.ParentObject == null)
			{
				return false;
			}
			if (this.ParentObject.IsPlayer())
			{
				return true;
			}
			if (this.CurrentCell != null && this.CurrentCell.ParentZone != null && this.CurrentCell.ParentZone.IsActive())
			{
				return true;
			}
			if (this.Equipped != null && this.Equipped.IsPlayerControlled())
			{
				return true;
			}
			if (this.InInventory != null && this.InInventory.IsPlayerControlled())
			{
				return true;
			}
			if (this.CurrentCell == null && this.Equipped == null && this.InInventory == null)
			{
				GameObject objectContext = this.ParentObject.GetObjectContext();
				if (objectContext != null)
				{
					if (objectContext.IsPlayer())
					{
						return true;
					}
					GameObject equipped = objectContext.Equipped;
					if (equipped != null && equipped.IsPlayer())
					{
						return true;
					}
					GameObject inInventory = objectContext.InInventory;
					if (inInventory != null && inInventory.IsPlayer())
					{
						return true;
					}
					if ((equipped != null || inInventory != null) && (this.IsAflame() || this.IsFrozen()))
					{
						return true;
					}
				}
			}
			else if (this.IsAflame() || this.IsFrozen())
			{
				return true;
			}
			return false;
		}

		// Token: 0x06008EF3 RID: 36595 RVA: 0x003360B4 File Offset: 0x003342B4
		private void UpdateTemperature()
		{
			using (Physics.s_PhysicsUpdateTemperature.Auto())
			{
				if (this.IsReal && this.Temperature != this.AmbientTemperature && !this.ParentObject.IsScenery)
				{
					if (this.ParentObject.HasTag("Nullphase") || this.ParentObject.GetIntProperty("Nullphase", 0) > 0)
					{
						this.Temperature = 25;
						return;
					}
					int intProperty = this.ParentObject.GetIntProperty("ThermalInsulation", 5);
					if (this.Temperature < this.AmbientTemperature - intProperty || this.Temperature > this.AmbientTemperature + intProperty)
					{
						int num = this.Temperature - this.AmbientTemperature;
						if (this.Temperature < this.AmbientTemperature)
						{
							num += intProperty;
							int num2 = Math.Max(5, (int)((double)(this.AmbientTemperature - this.Temperature) * 0.02));
							if (CanTemperatureReturnToAmbientEvent.Check(this.ParentObject, num2))
							{
								if (this.Temperature >= 25)
								{
									num2 = num2 * (100 - this.ParentObject.Stat("HeatResistance", 0)) / 100;
								}
								if (num2 > 0)
								{
									this.Temperature += num2;
								}
							}
						}
						else if (this.Temperature > this.AmbientTemperature)
						{
							num -= intProperty;
							int num3 = Math.Max(5, (int)((double)(this.Temperature - this.AmbientTemperature) * 0.02));
							if (CanTemperatureReturnToAmbientEvent.Check(this.ParentObject, num3))
							{
								if (this.Temperature <= 25 && !this.ShouldRestAtAmbient())
								{
									num3 = num3 * (100 - this.ParentObject.Stat("ColdResistance", 0)) / 100;
								}
								if (num3 > 0)
								{
									this.Temperature -= num3;
								}
							}
						}
						if (this.CurrentCell != null && !this.CurrentCell.OnWorldMap() && this.Temperature > this.AmbientTemperature)
						{
							int num4 = this.AmbientTemperature + num;
							int phase = this.ParentObject.GetPhase();
							foreach (Cell cell in this.CurrentCell.GetLocalAdjacentCells())
							{
								cell.TemperatureChange(num4, this.InflamedBy, true, false, false, false, phase);
							}
						}
					}
				}
				this.CheckThresholds(null);
			}
		}

		// Token: 0x06008EF4 RID: 36596 RVA: 0x0033634C File Offset: 0x0033454C
		public override void TurnTick(long TimeTick, int Amount)
		{
			GameObject.Validate(ref this.LastDamagedBy);
			GameObject.Validate(ref this.LastWeaponDamagedBy);
			GameObject.Validate(ref this.LastProjectileDamagedBy);
			GameObject.Validate(ref this.InflamedBy);
			if (this.ShouldUpdateTemperature())
			{
				this.UpdateTemperature();
			}
		}

		// Token: 0x06008EF5 RID: 36597 RVA: 0x0033638C File Offset: 0x0033458C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.NoConfusion && The.Player != null && The.Player.IsConfused)
			{
				bool flag = The.Player.GetFuriousConfusion() > 0;
				if (this.ConfusedName == null)
				{
					this.ConfusedName = NameMaker.MakeName(null, null, null, null, null, null, null, null, null, null, null, flag ? "FuriousConfusion" : "Confusion", null, false, false, null, null);
				}
				E.ReplaceEntirety(this.ConfusedName);
				if (flag)
				{
					E.AddColor("R", 0);
				}
				return false;
			}
			if (!E.Reference)
			{
				if (this.IsFrozen())
				{
					E.AddAdjective("{{freezing|frozen}}", -40);
				}
				if (this.IsAflame())
				{
					if (this.ParentObject.HasEffect<CoatedInPlasma>())
					{
						E.AddAdjective("{{auroral|auroral}}", -40);
					}
					else
					{
						E.AddAdjective("{{fiery|flaming}}", -40);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EF6 RID: 36598 RVA: 0x0033647C File Offset: 0x0033467C
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			if (this.ParentObject != null)
			{
				E.AddEntry("GameObject", "Flags", this.ParentObject.GetFlagsDebugString());
				E.AddEntry("GameObject", "Blueprint", this.ParentObject.Blueprint);
				E.AddEntry("GameObject", "DisplayNameStripped", this.ParentObject.DisplayNameStripped);
				E.AddEntry("GameObject", "ShortDisplayNameStripped", this.ParentObject.ShortDisplayNameStripped);
				E.AddEntry("GameObject", "IsEMPSensitive", this.ParentObject.IsEMPSensitive());
				E.AddEntry("GameObject", "IsEquippedProperly", this.ParentObject.IsEquippedProperly(null));
				E.AddEntry("GameObject", "KineticAbsorption", this.ParentObject.GetKineticAbsorption());
				E.AddEntry("GameObject", "KineticResistance", this.ParentObject.GetKineticResistance());
				E.AddEntry("GameObject", "MaximumLiquidExposure", this.ParentObject.GetMaximumLiquidExposure());
				E.AddEntry("GameObject", "PathAsBurrower", this.ParentObject.PathAsBurrower);
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				if (this.ParentObject.Property != null && this.ParentObject.Property.Count > 0)
				{
					stringBuilder.Clear();
					List<string> list = new List<string>(this.ParentObject.Property.Keys);
					list.Sort();
					foreach (string text in list)
					{
						stringBuilder.Append(text).Append(": ").Append(this.ParentObject.Property[text] ?? "NULL")
							.Append('\n');
					}
					E.AddEntry("GameObject", "Property", stringBuilder.ToString());
				}
				if (this.ParentObject.IntProperty != null && this.ParentObject.IntProperty.Count > 0)
				{
					stringBuilder.Clear();
					List<string> list2 = new List<string>(this.ParentObject.IntProperty.Keys);
					list2.Sort();
					foreach (string text2 in list2)
					{
						stringBuilder.Append(text2).Append(": ").Append(this.ParentObject.IntProperty[text2])
							.Append('\n');
					}
					E.AddEntry("GameObject", "IntProperty", stringBuilder.ToString());
				}
				if (this.ParentObject.Statistics != null && this.ParentObject.Statistics.Count > 0)
				{
					stringBuilder.Clear();
					List<string> list3 = new List<string>(this.ParentObject.Statistics.Keys);
					list3.Sort();
					foreach (string text3 in list3)
					{
						stringBuilder.Append(text3).Append(": ").Append(this.ParentObject.Stat(text3, 0).ToString() + " / " + this.ParentObject.BaseStat(text3, 0).ToString())
							.Append('\n');
					}
					E.AddEntry("GameObject", "Statistics", stringBuilder.ToString());
				}
				if (!this.ParentObject._Effects.IsNullOrEmpty<Effect>())
				{
					stringBuilder.Clear();
					foreach (Effect effect in this.ParentObject._Effects)
					{
						stringBuilder.Append(effect.GetType().GetName(false)).Append(": ").Append((effect.Duration == 9999) ? "Indefinite" : effect.Duration.ToString())
							.Append('\n');
					}
					E.AddEntry("GameObject", "Effects", stringBuilder.ToString());
				}
				E.AddEntry("GameObject", "Species", this.ParentObject.GetSpecies());
				E.AddEntry("Scanning", "ScanType", Scanning.GetScanTypeFor(this.ParentObject).ToString());
				if (this.CurrentCell != null && this.CurrentCell.ParentZone != null)
				{
					E.AddEntry("Cell", "LightLevel", this.CurrentCell.GetLight().ToString());
					E.AddEntry("Cell", "NavigationWeight", this.CurrentCell.GetNavigationWeightFor(The.Player, false, false, false, false, false));
					E.AddEntry("Cell", "AutoexploreNavigationWeight", this.CurrentCell.GetNavigationWeightFor(The.Player, true, false, false, false, false));
				}
			}
			E.AddEntry(this, "Temperature", this.Temperature);
			E.AddEntry(this, "FlameTemperature", this.FlameTemperature);
			E.AddEntry(this, "VaporTemperature", this.VaporTemperature);
			E.AddEntry(this, "VaporObject", this.VaporObject);
			E.AddEntry(this, "FreezeTemperature", this.FreezeTemperature);
			E.AddEntry(this, "BaseElectricalConductivity", this.BaseElectricalConductivity);
			E.AddEntry(this, "ElectricalConductivity", this.ElectricalConductivity);
			E.AddEntry(this, "Flags", this.Flags);
			E.AddEntry(this, "Solid", this.Solid);
			E.AddEntry(this, "Takeable", this.Takeable);
			E.AddEntry(this, "IsReal", this.IsReal);
			E.AddEntry(this, "LastDamageAccidental", this.LastDamageAccidental);
			E.AddEntry(this, "WasFrozen", this.WasFrozen);
			E.AddEntry(this, "Organic", this.Organic);
			E.AddEntry(this, "UsesTwoSlots", this.UsesTwoSlots);
			E.AddEntry(this, "UsesSlots", this.UsesSlots);
			if (this.CurrentCell != null)
			{
				E.AddEntry(this, "Position", this.CurrentCell.X.ToString() + ", " + this.CurrentCell.Y.ToString());
			}
			if (!this.Owner.IsNullOrEmpty())
			{
				E.AddEntry(this, "Owner", this.Owner);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EF7 RID: 36599 RVA: 0x00336B3C File Offset: 0x00334D3C
		public override bool HandleEvent(GetInventoryCategoryEvent E)
		{
			if (The.Player != null && The.Player.IsConfused && !E.AsIfKnown)
			{
				E.Category = "???";
				return false;
			}
			if (E.Category.IsNullOrEmpty())
			{
				E.Category = this.Category;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EF8 RID: 36600 RVA: 0x00336B94 File Offset: 0x00334D94
		public override bool HandleEvent(StatChangeEvent E)
		{
			if (E.Name == "Hitpoints")
			{
				if (E.NewValue < E.OldValue && E.NewValue - E.OldValue != E.NewBaseValue - E.OldBaseValue)
				{
					bool flag = this.ParentObject.IsPlayer();
					if (flag)
					{
						int hpwarningThreshold = Globals.HPWarningThreshold;
						if (hpwarningThreshold > E.NewValue * 100 / E.NewBaseValue && hpwarningThreshold <= E.OldValue * 100 / E.OldBaseValue)
						{
							The.Core.HPWarning = true;
						}
					}
					if (AutoAct.IsInterruptable())
					{
						if (flag)
						{
							AutoAct.Interrupt(null, null, null, false);
						}
						else if (this.ParentObject.IsPlayerLedAndPerceptible() && !this.ParentObject.IsTrifling)
						{
							AutoAct.Interrupt(string.Concat(new string[]
							{
								"you ",
								this.ParentObject.GetPerceptionVerb(),
								" ",
								this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, true, null, false, true, false, null, true, null, false),
								" being injured",
								this.ParentObject.IsVisible() ? "" : (" " + The.Player.DescribeDirectionToward(this.ParentObject, false, false))
							}), null, this.ParentObject, true);
						}
					}
				}
				if (!this.ParentObject.WillCheckHP(null))
				{
					this.CheckHP(new int?(E.NewValue), new int?(E.OldValue), new int?(E.Stat.BaseValue), false);
				}
			}
			else if (E.Name == "HeatResistance" || E.Name == "ColdResistance")
			{
				this.AmbientCache = int.MinValue;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EF9 RID: 36601 RVA: 0x00336D81 File Offset: 0x00334F81
		public override bool HandleEvent(SuspendingEvent E)
		{
			this.LastDamagedBy = null;
			this.LastWeaponDamagedBy = null;
			this.LastProjectileDamagedBy = null;
			this.InflamedBy = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008EFA RID: 36602 RVA: 0x00336DA8 File Offset: 0x00334FA8
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.AmbientCache = int.MinValue;
			if (this.IsReal && this.CurrentCell != null && this.CurrentCell.ParentZone != null)
			{
				int i = 0;
				int num = this.CurrentCell.Objects.Count;
				while (i < num)
				{
					GameObject gameObject = this.CurrentCell.Objects[i];
					if (gameObject != this.ParentObject && this.ParentObject.ConsiderUnableToOccupySameCell(gameObject))
					{
						if (this.ParentObject.IsWall() && gameObject.IsWall() && !this.ParentObject.IsCombat && !gameObject.IsCombat)
						{
							if (this.ParentObject.GetTier() > gameObject.GetTier())
							{
								gameObject.Obliterate(null, false, null);
								break;
							}
							this.ParentObject.Obliterate(null, false, null);
							break;
						}
						else if (!CheckSpawnMergeEvent.Check(this.ParentObject, gameObject))
						{
							if (!GameObject.Validate(this.ParentObject) || this.ParentObject.IsNowhere())
							{
								break;
							}
							i = -1;
							num = this.CurrentCell.Objects.Count;
						}
						else
						{
							using (IEnumerator<Cell> enumerator = this.CurrentCell.YieldAdjacentCells(9, true, true).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Cell cell = enumerator.Current;
									if (cell.IsPassable(this.ParentObject, true) && !cell.HasSpawnBlocker())
									{
										this.ParentObject.SystemMoveTo(cell, null, true, true, false, false);
										break;
									}
								}
								break;
							}
						}
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008EFB RID: 36603 RVA: 0x00336F5C File Offset: 0x0033515C
		public bool ProcessTakeDamage(Event E)
		{
			Physics.<>c__DisplayClass182_0 CS$<>8__locals1;
			CS$<>8__locals1.E = E;
			CS$<>8__locals1.<>4__this = this;
			if (this.ParentObject == null)
			{
				return false;
			}
			if (this.ParentObject.IsPlayer() && The.Core.IDKFA)
			{
				return false;
			}
			bool flag = CS$<>8__locals1.E.HasFlag("Indirect");
			Damage damage = CS$<>8__locals1.E.GetParameter("Damage") as Damage;
			GameObject gameObject;
			if ((gameObject = CS$<>8__locals1.E.GetGameObjectParameter("Source")) == null)
			{
				gameObject = CS$<>8__locals1.E.GetGameObjectParameter("Attacker") ?? CS$<>8__locals1.E.GetGameObjectParameter("Owner");
			}
			GameObject gameObject2 = gameObject;
			GameObject gameObject3 = CS$<>8__locals1.E.GetGameObjectParameter("Owner") ?? CS$<>8__locals1.E.GetGameObjectParameter("Attacker");
			GameObject gameObject4;
			if ((gameObject4 = CS$<>8__locals1.E.GetGameObjectParameter("DescribeAsFrom")) == null)
			{
				gameObject4 = (flag ? gameObject2 : null) ?? gameObject3;
			}
			GameObject gameObject5 = gameObject4;
			GameObject gameObjectParameter = CS$<>8__locals1.E.GetGameObjectParameter("Weapon");
			GameObject gameObjectParameter2 = CS$<>8__locals1.E.GetGameObjectParameter("Projectile");
			if (CS$<>8__locals1.E.HasParameter("Phase"))
			{
				if (!this.ParentObject.PhaseMatches(CS$<>8__locals1.E.GetIntParameter("Phase", 0)))
				{
					return false;
				}
			}
			else if (gameObject2 != null && !this.ParentObject.PhaseMatches(gameObject2))
			{
				return false;
			}
			this.LastWeaponDamagedBy = gameObjectParameter;
			this.LastProjectileDamagedBy = gameObjectParameter2;
			this.LastDamagedBy = gameObject3;
			if (GameObject.Validate(ref this.LastDamagedBy))
			{
				this.LastDamageAccidental = damage.HasAttribute("Accidental") && !this.LastDamagedBy.IsHostileTowards(this.ParentObject);
				if (this.LastDamagedBy.IsPlayer() && this.LastDamagedBy != this.ParentObject)
				{
					if (!flag && !CS$<>8__locals1.E.HasFlag("NoSetTarget") && Sidebar.CurrentTarget == null && this.ParentObject.IsCombat && IComponent<GameObject>.Visible(this.ParentObject))
					{
						Sidebar.CurrentTarget = this.ParentObject;
					}
					if (The.Core.IDKFA)
					{
						damage.Amount = 999;
					}
				}
			}
			else
			{
				this.LastDamageAccidental = false;
			}
			if (!damage.HasAttribute("IgnoreResist"))
			{
				int num = this.ParentObject.Stat("AcidResistance", 0);
				if (num != 0 && damage.Amount > 0 && damage.IsAcidDamage())
				{
					if (num > 0)
					{
						damage.Amount = (int)((float)damage.Amount * ((float)(100 - num) / 100f));
						if (num < 100 && damage.Amount < 1)
						{
							damage.Amount = 1;
						}
					}
					else if (num < 0)
					{
						damage.Amount += (int)((float)damage.Amount * ((float)num / -100f));
					}
				}
				int num2 = this.ParentObject.Stat("HeatResistance", 0);
				if (num2 != 0 && damage.Amount > 0 && damage.IsHeatDamage())
				{
					if (num2 > 0)
					{
						damage.Amount = (int)((float)damage.Amount * ((float)(100 - num2) / 100f));
						if (num2 < 100 && damage.Amount < 1)
						{
							damage.Amount = 1;
						}
					}
					else if (num2 < 0)
					{
						damage.Amount += (int)((float)damage.Amount * ((float)num2 / -100f));
					}
				}
				int num3 = this.ParentObject.Stat("ColdResistance", 0);
				if (num3 != 0 && damage.Amount > 0 && damage.IsColdDamage())
				{
					if (num3 > 0)
					{
						damage.Amount = (int)((float)damage.Amount * ((float)(100 - num3) / 100f));
						if (num3 < 100 && damage.Amount < 1)
						{
							damage.Amount = 1;
						}
					}
					else if (num3 < 0)
					{
						damage.Amount += (int)((float)damage.Amount * ((float)num3 / -100f));
					}
				}
				int num4 = this.ParentObject.Stat("ElectricResistance", 0);
				if (num4 != 0 && damage.Amount > 0 && damage.IsElectricDamage())
				{
					if (num4 > 0)
					{
						damage.Amount = (int)((float)damage.Amount * ((float)(100 - num4) / 100f));
						if (num4 < 100 && damage.Amount < 1)
						{
							damage.Amount = 1;
						}
					}
					else if (num4 < 0)
					{
						damage.Amount += (int)((float)damage.Amount * ((float)num4 / -100f));
					}
				}
			}
			string text = CS$<>8__locals1.E.GetStringParameter("Message", "");
			if (!BeforeApplyDamageEvent.Check(damage, this.ParentObject, gameObject3, gameObject2, gameObjectParameter, gameObjectParameter2, flag, CS$<>8__locals1.E))
			{
				return false;
			}
			if (!AttackerDealingDamageEvent.Check(damage, this.ParentObject, gameObject3, gameObject2, gameObjectParameter, gameObjectParameter2, flag, CS$<>8__locals1.E))
			{
				return false;
			}
			if (!LateBeforeApplyDamageEvent.Check(damage, this.ParentObject, gameObject3, gameObject2, gameObjectParameter, gameObjectParameter2, flag, CS$<>8__locals1.E))
			{
				return false;
			}
			string text2 = "killed";
			string text3 = "by";
			string text4 = null;
			bool flag2 = false;
			bool flag3 = this.LastDamageAccidental;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			if (damage.Amount > 0)
			{
				if (damage.IsHeatDamage())
				{
					if (damage.HasAttribute("NoBurn"))
					{
						this.LastDamagedByType = "Heat";
						text2 = "cooked";
					}
					else
					{
						this.LastDamagedByType = "Fire";
						text2 = "immolated";
					}
				}
				else if (damage.HasAttribute("Vaporized"))
				{
					this.LastDamagedByType = "Vaporized";
					text2 = "vaporized";
					flag3 = false;
				}
				else if (damage.IsColdDamage())
				{
					this.LastDamagedByType = "Cold";
					text2 = "frozen to death";
				}
				else if (damage.IsElectricDamage())
				{
					this.LastDamagedByType = "Electric";
					text2 = "electrocuted";
				}
				else if (damage.IsAcidDamage())
				{
					this.LastDamagedByType = "Acid";
					text2 = "dissolved";
				}
				else if (damage.IsDisintegrationDamage())
				{
					this.LastDamagedByType = "Disintegration";
					text2 = "disintegrated";
				}
				else if (damage.HasAttribute("Plasma"))
				{
					this.LastDamagedByType = "Plasma";
					text2 = "plasma-burned to death";
				}
				else if (damage.HasAttribute("Laser"))
				{
					this.LastDamagedByType = "Light";
					text2 = "lased to death";
				}
				else if (damage.HasAttribute("Light"))
				{
					this.LastDamagedByType = "Light";
					text2 = "illuminated to death";
				}
				else if (damage.HasAttribute("Poison"))
				{
					this.LastDamagedByType = "Poison";
					text2 = "died of poison";
					flag2 = true;
					text3 = "from";
				}
				else if (damage.HasAttribute("Bleeding"))
				{
					this.LastDamagedByType = "Bleeding";
					text2 = "bled to death";
					flag2 = true;
					text3 = "because of";
				}
				else if (damage.HasAttribute("Asphyxiation"))
				{
					this.LastDamagedByType = "Asphyxiation";
					text2 = "died of asphyxiation";
					flag2 = true;
					text3 = "from";
				}
				else if (damage.HasAttribute("Metabolic"))
				{
					this.LastDamagedByType = "Metabolic";
					text4 = "metabolism";
					text2 = "failed";
					text3 = "from";
				}
				else if (damage.HasAttribute("Drain"))
				{
					this.LastDamagedByType = "Drain";
					text4 = "vital essence was";
					text2 = "drained to extinction";
					text3 = "by";
				}
				else if (damage.HasAttribute("Psionic"))
				{
					this.LastDamagedByType = "Psionic";
					text2 = "psychically extinguished";
				}
				else if (damage.HasAttribute("Mental"))
				{
					this.LastDamagedByType = "Mental";
					text2 = "mentally obliterated";
				}
				else if (damage.HasAttribute("Thorns"))
				{
					this.LastDamagedByType = "Thorns";
					text2 = "pricked to death";
				}
				else if (damage.HasAttribute("Collision"))
				{
					this.LastDamagedByType = "Collision";
					text3 = "by colliding with";
				}
				else if (damage.HasAttribute("Bite"))
				{
					this.LastDamagedByType = "Bite";
					text2 = "bitten to death";
				}
				else
				{
					this.LastDamagedByType = ((damage.Attributes.Count > 0) ? damage.Attributes[0] : "Physical");
					flag6 = true;
				}
				if (damage.HasAttribute("Illusion"))
				{
					flag4 = true;
				}
				if (damage.HasAttribute("reflected"))
				{
					flag5 = true;
				}
				if (damage.HasAttribute("Neutron") && this.ParentObject.IsPlayer())
				{
					Achievement.CRUSHED_UNDER_SUNS.Unlock();
				}
			}
			this.LastDeathCategory = text2;
			if (CS$<>8__locals1.E.HasParameter("DeathReason"))
			{
				this.LastDeathReason = CS$<>8__locals1.E.GetStringParameter("DeathReason", null);
				if (CS$<>8__locals1.E.HasParameter("ThirdPersonDeathReason"))
				{
					this.LastThirdPersonDeathReason = CS$<>8__locals1.E.GetStringParameter("ThirdPersonDeathReason", null);
				}
				else
				{
					this.LastThirdPersonDeathReason = GameText.RoughConvertSecondPersonToThirdPerson(this.LastDeathReason, this.ParentObject);
				}
			}
			else
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
				if (flag4)
				{
					stringBuilder.Append("You convinced yourself ").Append((text4 != null) ? "your" : "you").Append(' ');
					stringBuilder2.Append(this.ParentObject.It).Append(" @@convinced ").Append(this.ParentObject.itself)
						.Append(' ')
						.Append((text4 != null) ? this.ParentObject.its : this.ParentObject.it)
						.Append(' ');
				}
				else
				{
					stringBuilder.Append((text4 != null) ? "Your " : "You ");
					stringBuilder2.Append((text4 != null) ? this.ParentObject.Its : this.ParentObject.It).Append(' ');
				}
				if (text4 != null)
				{
					stringBuilder.Append(text4).Append(' ');
					stringBuilder2.Append(text4).Append(' ');
				}
				else if (!flag2)
				{
					stringBuilder.Append("were ");
					stringBuilder2.Append(this.ParentObject.GetVerb("were", false, true, false, true, false)).Append(' ');
				}
				if (flag3 && text4 == null)
				{
					stringBuilder.Append("accidentally ");
					stringBuilder2.Append("accidentally ");
				}
				stringBuilder.Append(text2);
				if (!flag4)
				{
					stringBuilder2.Append("@@");
				}
				stringBuilder2.Append(text2);
				if (flag5)
				{
					stringBuilder.Append(" via reflected damage");
					stringBuilder2.Append(" via reflected damage");
				}
				if (gameObject3 != null && gameObject3 != this.ParentObject)
				{
					if (!text3.IsNullOrEmpty())
					{
						stringBuilder.Append(' ').Append(text3);
						stringBuilder2.Append(' ').Append(text3);
					}
					stringBuilder.Append(' ');
					stringBuilder2.Append(' ');
					if (this.ParentObject.IsPlayer() && gameObject3.GetStringProperty("PlayerCopy", null) == "true" && gameObject3.HasStringProperty("PlayerCopyDescription"))
					{
						stringBuilder.Append(gameObject3.GetStringProperty("PlayerCopyDescription", null));
						stringBuilder2.Append(gameObject3.GetStringProperty("PlayerCopyDescription", null));
					}
					else
					{
						string text5 = gameObject3.an(int.MaxValue, null, null, false, false, false, false, false, false, false, false, false, true, false, null, false, null, false);
						stringBuilder.Append(text5);
						stringBuilder2.Append(text5);
					}
				}
				if (flag6)
				{
					GameObject gameObject6;
					if ((gameObject6 = gameObjectParameter2) == null)
					{
						gameObject6 = gameObjectParameter ?? gameObject2;
					}
					GameObject gameObject7 = gameObject6;
					if (gameObject7 != null)
					{
						stringBuilder2.Append("## with ").Append(gameObject7.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false)).Append("##");
					}
				}
				stringBuilder.Append('.');
				stringBuilder2.Append('.');
				this.LastDeathReason = stringBuilder.ToString();
				this.LastThirdPersonDeathReason = stringBuilder2.ToString();
			}
			if (base.IsPlayer())
			{
				The.Game.DeathReason = this.LastDeathReason;
				The.Game.DeathCategory = this.LastDeathCategory;
			}
			Statistic stat = this.ParentObject.GetStat("Hitpoints");
			if (stat == null)
			{
				return false;
			}
			bool flag7 = false;
			if (damage.Amount > 0)
			{
				int count = this.ParentObject.Count;
				this.ParentObject.SplitFromStack();
				if (count != this.ParentObject.Count)
				{
					flag7 = true;
				}
			}
			if (!text.IsNullOrEmpty() && (damage.Amount > 0 || !CS$<>8__locals1.E.HasFlag("SilentIfNoDamage")))
			{
				bool flag8 = false;
				Physics.<>c__DisplayClass182_1 CS$<>8__locals2;
				CS$<>8__locals2._visible = null;
				if (this.ParentObject.IsPlayer())
				{
					flag8 = true;
				}
				else if (gameObject3 != null && gameObject3.IsPlayer() && (this.<ProcessTakeDamage>g__visible|182_0(ref CS$<>8__locals1, ref CS$<>8__locals2) ?? gameObject3.isAdjacentTo(this.ParentObject)))
				{
					flag8 = true;
				}
				else if (this.ParentObject.IsCombatObject() || CS$<>8__locals1.E.HasFlag("ShowForInanimate"))
				{
					GameObject gameObjectParameter3 = CS$<>8__locals1.E.GetGameObjectParameter("Perspective");
					if (gameObjectParameter3 != null && IComponent<GameObject>.Visible(gameObjectParameter3))
					{
						flag8 = true;
					}
					else if ((gameObject3 == null || CS$<>8__locals1.E.HasFlag("ShowUninvolved")) && this.<ProcessTakeDamage>g__visible|182_0(ref CS$<>8__locals1, ref CS$<>8__locals2).GetValueOrDefault())
					{
						flag8 = true;
					}
				}
				string stringParameter = CS$<>8__locals1.E.GetStringParameter("ShowDamageType", "damage");
				if (flag8)
				{
					text = text.Replace("%d", damage.Amount.ToString());
					text = text.Replace("%e", stringParameter);
					if (gameObject5 != null)
					{
						if (gameObject5.IsPlayer())
						{
							text = text.Replace("%o", "your");
							text = text.Replace("%O", "you");
							text = text.Replace("%S", " you started");
							text = text.Replace("%t", "your");
							text = text.Replace("%T", "you");
						}
						else if (gameObject5 == this.ParentObject)
						{
							if (text.Contains("%o"))
							{
								text = text.Replace("%o", gameObject5.its);
							}
							if (text.Contains("%O"))
							{
								text = text.Replace("%O", gameObject5.itself);
							}
							if (text.Contains("%S"))
							{
								text = text.Replace("%S", " started by " + gameObject5.itself);
							}
							if (text.Contains("%t"))
							{
								text = text.Replace("%t", gameObject5.its);
							}
							if (text.Contains("%T"))
							{
								text = text.Replace("%T", gameObject5.It);
							}
						}
						else
						{
							if (text.Contains("%o"))
							{
								text = text.Replace("%o", Grammar.MakePossessive(gameObject5.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true)));
							}
							if (text.Contains("%O"))
							{
								text = text.Replace("%O", gameObject5.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, new bool?(true), true, null, false));
							}
							if (text.Contains("%S"))
							{
								text = text.Replace("%S", " started by " + gameObject5.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, new bool?(true), true, null, false));
							}
							if (text.Contains("%t"))
							{
								text = text.Replace("%t", Grammar.MakePossessive(gameObject5.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, new bool?(true), true, null, false)));
							}
							if (text.Contains("%T"))
							{
								text = text.Replace("%T", gameObject5.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, new bool?(true), true, null, false));
							}
						}
					}
					else
					{
						text = text.Replace("%o", "the");
						text = text.Replace("%O ", "");
						text = text.Replace("%S", "");
						text = text.Replace("%t", "the");
						text = text.Replace("%T ", "");
					}
					if (CS$<>8__locals1.E.HasFlag("NoDamageMessage"))
					{
						base.EmitMessage(text, ' ', false, CS$<>8__locals1.E.HasFlag("UsePopups"), true, null, null);
					}
					else if (this.ParentObject.IsPlayer())
					{
						StringBuilder stringBuilder3 = Event.NewStringBuilder(null);
						stringBuilder3.Append("{{r|You take ");
						if (damage.Amount > 0)
						{
							stringBuilder3.Append(damage.Amount).Append(' ').Append(stringParameter);
						}
						else
						{
							stringBuilder3.Append("no damage");
						}
						stringBuilder3.Append(' ').Append(text).Append("}}");
						base.EmitMessage(stringBuilder3.ToString(), ' ', false, CS$<>8__locals1.E.HasFlag("UsePopups"), true, null, null);
					}
					else if (this.ParentObject.IsCombat || CS$<>8__locals1.E.HasFlag("ShowForInanimate"))
					{
						StringBuilder stringBuilder4 = Event.NewStringBuilder(null);
						stringBuilder4.Append(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)).Append(' ').Append(this.ParentObject.GetVerb("take", false, false, false, true, false))
							.Append(' ');
						if (damage.Amount > 0)
						{
							stringBuilder4.Append(damage.Amount).Append(' ').Append(stringParameter);
						}
						else
						{
							stringBuilder4.Append("no damage");
						}
						stringBuilder4.Append(' ').Append(text);
						base.EmitMessage(stringBuilder4.ToString(), ' ', false, CS$<>8__locals1.E.HasFlag("UsePopups"), true, null, null);
					}
				}
			}
			AttackerDealtDamageEvent.Send(damage, this.ParentObject, gameObject3, gameObject2, gameObjectParameter, gameObjectParameter2, flag, CS$<>8__locals1.E);
			if (damage.Amount > 0)
			{
				if (this.ParentObject.IsPlayer())
				{
					if (AutoAct.IsInterruptable())
					{
						AutoAct.Interrupt(null, null, null, false);
					}
				}
				else
				{
					if (!this.Owner.IsNullOrEmpty())
					{
						this.CheckBroadcastForHelp(gameObject3, this.LastDamageAccidental);
					}
					if (AutoAct.IsInterruptable() && this.ParentObject.IsPlayerLedAndPerceptible() && !this.ParentObject.IsTrifling)
					{
						AutoAct.Interrupt(string.Concat(new string[]
						{
							"you ",
							this.ParentObject.GetPerceptionVerb(),
							" ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, true, null, false, true, false, null, true, null, false),
							" being injured",
							this.ParentObject.IsVisible() ? "" : (" " + The.Player.DescribeDirectionToward(this.ParentObject, false, false))
						}), null, this.ParentObject, true);
					}
					this.ParentObject.PlayCombatSoundTag("TakeDamageSound", null, null, 0.5f);
				}
				if (this.ParentObject.IsFrozen())
				{
					base.PlayWorldSound("Sounds/Damage/sfx_damage_ice", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				if (base.juiceEnabled && GameObject.Validate(this.ParentObject) && (this.ParentObject.IsPlayer() || (gameObject3 != null && gameObject3.IsPlayer() && gameObject3.isAdjacentTo(this.ParentObject)) || base.Visible()))
				{
					float num5 = 1f;
					if (stat.BaseValue > 0)
					{
						float num6 = (float)damage.Amount / (float)stat.BaseValue;
						if ((double)num6 >= 0.5)
						{
							num5 = 1.8f;
						}
						else if ((double)num6 >= 0.4)
						{
							num5 = 1.6f;
						}
						else if ((double)num6 >= 0.3)
						{
							num5 = 1.4f;
						}
						else if ((double)num6 >= 0.2)
						{
							num5 = 1.2f;
						}
						else if ((double)num6 >= 0.1)
						{
							num5 = 1.1f;
						}
					}
					CombatJuice.floatingText(this.ParentObject, "-" + damage.Amount.ToString(), The.Color.Red, 1.5f, 24f, num5, false);
				}
				BeforeTookDamageEvent.Send(damage, this.ParentObject, gameObject3, gameObject2, gameObjectParameter, gameObjectParameter2, flag, CS$<>8__locals1.E);
				if (damage.Amount != 0)
				{
					stat.Penalty += damage.Amount;
				}
				if (GameObject.Validate(this.ParentObject))
				{
					TookDamageEvent.Send(damage, this.ParentObject, gameObject3, gameObject2, gameObjectParameter, gameObjectParameter2, flag, CS$<>8__locals1.E);
					if (GameObject.Validate(this.ParentObject) && damage.HasAttribute("Environmental"))
					{
						TookEnvironmentalDamageEvent.Send(damage, this.ParentObject, gameObject3, gameObject2, flag, CS$<>8__locals1.E);
					}
				}
			}
			if (flag7)
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.CheckStack();
				}
			}
			return true;
		}

		// Token: 0x06008EFC RID: 36604 RVA: 0x003384E8 File Offset: 0x003366E8
		public bool ProcessTargetedMove(Cell TargetCell, string Type, string PreEvent, string PostEvent, int? EnergyCost = null, bool Forced = false, bool System = false, bool IgnoreCombat = false, bool IgnoreGravity = false, bool NoStack = false, bool UsePopups = false, string LeaveVerb = null, string ArriveVerb = null, GameObject Ignore = null)
		{
			Cell cell = this.CurrentCell;
			if (cell == TargetCell)
			{
				return true;
			}
			if (!this.ParentObject.IsPlayer() || TutorialManager.BeforePlayerEnterCell(TargetCell))
			{
				if (TargetCell != null && TargetCell.HasObjectWithTagOrProperty("NoTeleport"))
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.Show(TargetCell.GetFirstObjectWithPropertyOrTag("NoTeleport").GetPropertyOrTag("NoTeleport", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				else
				{
					Zone zone = ((cell != null) ? cell.ParentZone : null);
					int num = EnergyCost ?? 1000;
					bool flag;
					if ((this.ParentObject.FireEvent(Event.New(PreEvent, "Cell", TargetCell, Type, 1)) || System) && this.ParentObject.ProcessBeginMove(out flag, TargetCell, Forced, System, IgnoreGravity, NoStack, null, Type, null, null, null, Ignore, null))
					{
						if (this.ParentObject.IsCombat && !IgnoreCombat)
						{
							foreach (GameObject gameObject in TargetCell.GetObjectsWithPartReadonly("Combat"))
							{
								if (!gameObject.IsHostileTowards(this.ParentObject))
								{
									if (this.ParentObject.IsPlayer() && Popup.ShowYesNo("Do you really want to attack " + gameObject.DisplayName + "?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
									{
										Combat.AttackCell(this.ParentObject, TargetCell, null, 0, 0, 0, false);
									}
									goto IL_03AD;
								}
								if (this.ParentObject.IsPlayer())
								{
									if (The.Core.PlayerWalking == "")
									{
										Combat.AttackCell(this.ParentObject, TargetCell, null, 0, 0, 0, false);
										goto IL_03AD;
									}
									The.Core.RenderBase(true, false);
								}
							}
						}
						cell = this.CurrentCell;
						Zone zone2 = ((cell != null) ? cell.ParentZone : null);
						GameObject gameObject2 = null;
						if (this.ParentObject.ProcessObjectLeavingCell(cell, ref gameObject2, Forced, System, IgnoreGravity, NoStack, null, Type, null, null, null, Ignore, null) && this.ParentObject.ProcessEnteringCell(TargetCell, ref gameObject2, Forced, System, IgnoreGravity, NoStack, null, Type, null, null, null, Ignore, null) && this.ParentObject.ProcessObjectEnteringCell(TargetCell, ref gameObject2, Forced, System, IgnoreGravity, NoStack, null, Type, null, null, null, Ignore, null) && (zone2 == TargetCell.ParentZone || this.ParentObject.ProcessEnteringZone(cell, TargetCell, ref gameObject2, Forced, System, IgnoreGravity, NoStack, null, Type, null, null, null, Ignore, null)) && this.ParentObject.ProcessLeaveCell(cell, ref gameObject2, Forced, System, IgnoreGravity, NoStack, null, Type, null, null, null, Ignore, null))
						{
							if (!this.ParentObject.IsPlayer() && !LeaveVerb.IsNullOrEmpty())
							{
								base.DidX(LeaveVerb, null, null, null, null, null, null, false, false, null, null, true, false, false, false, UsePopups, null);
							}
							if (cell != null)
							{
								cell.RemoveObject(this.ParentObject, Forced, System, IgnoreGravity, false, NoStack, true, true, null, Type, null, null, null, Ignore, null);
							}
							TargetCell.AddObject(this.ParentObject, Forced, System, IgnoreGravity, NoStack, false, true, true, null, Type, null, null, null, Ignore, null, null);
							if (!this.ParentObject.IsPlayer() && !ArriveVerb.IsNullOrEmpty())
							{
								base.DidX(ArriveVerb, null, null, null, null, null, null, false, true, null, null, false, true, false, false, UsePopups, null);
							}
							this.ParentObject.UseEnergy(num, "Movement", null, null, false);
							if (base.IsPlayer() && zone2 != TargetCell.ParentZone)
							{
								The.ZoneManager.SetActiveZone(TargetCell.ParentZone);
								The.ZoneManager.ProcessGoToPartyLeader();
							}
							this.ParentObject.FireEvent(Event.New(PostEvent, "FromCell", cell, Type, 1));
							return true;
						}
					}
				}
			}
			IL_03AD:
			AfterMoveFailedEvent.Send(this.ParentObject, cell, TargetCell, Forced, System, IgnoreGravity, NoStack, null, Type, null, null, null, Ignore);
			return false;
		}

		// Token: 0x06008EFD RID: 36605 RVA: 0x003388D0 File Offset: 0x00336AD0
		public void TeardownForDestroy(bool MoveToGraveyard = true, bool Silent = false)
		{
			Zone zone = this.CurrentZone;
			GameObject gameObject = this.Equipped;
			if (gameObject != null)
			{
				if (zone == null)
				{
					Cell cell = gameObject.FastGetCurrentCell();
					zone = ((cell != null) ? cell.ParentZone : null);
				}
				Body body = gameObject.Body;
				if (body != null)
				{
					int num = 0;
					BodyPart bodyPart;
					while ((bodyPart = body.FindDefaultOrEquippedItem(this.ParentObject)) != null)
					{
						if (++num >= 100)
						{
							MetricsManager.LogError("infinite looping trying to unequip " + this.ParentObject.DebugName);
							break;
						}
						if (bodyPart.Equipped == this.ParentObject)
						{
							Event @event = Event.New("CommandForceUnequipObject", 0, 0, 0);
							@event.SetParameter("BodyPart", bodyPart);
							@event.SetFlag("NoTake", true);
							@event.SetSilent(true);
							gameObject.FireEvent(@event);
						}
						if (bodyPart.DefaultBehavior == this.ParentObject)
						{
							bodyPart.DefaultBehavior = null;
						}
					}
				}
			}
			gameObject = this.InInventory;
			if (gameObject != null)
			{
				if (zone == null)
				{
					Cell cell2 = gameObject.FastGetCurrentCell();
					zone = ((cell2 != null) ? cell2.ParentZone : null);
				}
				Inventory inventory = gameObject.Inventory;
				if (inventory != null)
				{
					inventory.RemoveObject(this.ParentObject, null);
				}
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell != null)
			{
				if (currentCell.ParentZone != null && this.ParentObject.WantEvent(PooledEvent<CheckExistenceSupportEvent>.ID, CheckExistenceSupportEvent.CascadeLevel))
				{
					currentCell.ParentZone.WantSynchronizeExistence();
				}
				currentCell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			}
			ActionManager actionManager = The.ActionManager;
			if (actionManager != null)
			{
				actionManager.RemoveActiveObject(this.ParentObject);
			}
			if (MoveToGraveyard && !this.ParentObject.IsInGraveyard())
			{
				if (zone != null)
				{
					zone.Graveyard.Add(this.ParentObject);
					return;
				}
				Graveyard graveyard = The.Graveyard;
				if (graveyard == null)
				{
					return;
				}
				graveyard.Add(this.ParentObject);
			}
		}

		// Token: 0x06008EFE RID: 36606 RVA: 0x00338A94 File Offset: 0x00336C94
		public bool ProcessTemperatureChange(int Amount, GameObject Actor = null, bool Radiant = false, bool MinAmbient = false, bool MaxAmbient = false, bool IgnoreResistance = false, int Phase = 0, int? Min = null, int? Max = null)
		{
			if (!this.IsReal)
			{
				return false;
			}
			if (this.SpecificHeat == 0f)
			{
				return false;
			}
			if (Phase == 0)
			{
				if (Actor != null)
				{
					Phase = Actor.GetPhase();
				}
				if (Phase == 0)
				{
					Phase = 5;
				}
			}
			if (!this.ParentObject.PhaseMatches(Phase))
			{
				return false;
			}
			Amount = BeforeTemperatureChangeEvent.GetFor(this.ParentObject, Amount, Actor, Radiant, MinAmbient, MaxAmbient, IgnoreResistance, Phase, Min, Max);
			if (Amount == 0)
			{
				return false;
			}
			Amount = AttackerBeforeTemperatureChangeEvent.GetFor(this.ParentObject, Amount, Actor, Radiant, MinAmbient, MaxAmbient, IgnoreResistance, Phase, Min, Max);
			if (Amount == 0)
			{
				return false;
			}
			int temperature = this.Temperature;
			if (Radiant)
			{
				if (!IgnoreResistance)
				{
					if (Amount > 0)
					{
						int num = Math.Min(100, Math.Max(0, this.ParentObject.Stat("HeatResistance", 0)));
						if (num != 0)
						{
							Amount = (int)((float)Amount * ((float)(100 - num) / 100f));
						}
					}
					else if (Amount < 0)
					{
						int num2 = Math.Min(100, Math.Max(0, this.ParentObject.Stat("ColdResistance", 0)));
						if (num2 != 0)
						{
							Amount = (int)((float)Amount * ((float)(100 - num2) / 100f));
						}
					}
				}
				if (this.Temperature > Amount)
				{
					if (!this.IsAflame())
					{
						this.Temperature += (int)((float)(Amount - this.Temperature) * (0.035f / this.SpecificHeat));
					}
				}
				else
				{
					this.Temperature += (int)((float)(Amount - this.Temperature) * (0.035f / this.SpecificHeat));
				}
			}
			else
			{
				Amount = (int)((float)Amount / this.SpecificHeat);
				if (!IgnoreResistance)
				{
					Mutations mutations;
					if (this.ParentObject.TryGetPart<Mutations>(out mutations) && mutations.HasMutation("Fatty Hump"))
					{
						Amount /= 2;
					}
					if (Amount > 0 && this.Temperature + Amount > 50)
					{
						int num3 = this.ParentObject.Stat("HeatResistance", 0);
						if (num3 != 0)
						{
							Amount = (int)((float)Amount * ((float)(100 - num3) / 100f));
						}
					}
					else if (Amount < 0 && this.Temperature + Amount < 25)
					{
						int num4 = this.ParentObject.Stat("ColdResistance", 0);
						if (num4 != 0)
						{
							Amount = (int)((float)Amount * ((float)(100 - num4) / 100f));
						}
					}
				}
				this.Temperature += Amount;
			}
			if (MinAmbient && this.Temperature < this.AmbientTemperature)
			{
				this.Temperature = this.AmbientTemperature;
			}
			if (MaxAmbient && this.Temperature > this.AmbientTemperature)
			{
				this.Temperature = this.AmbientTemperature;
			}
			if (Min != null)
			{
				int temperature2 = this.Temperature;
				int? num5 = Min;
				if ((temperature2 < num5.GetValueOrDefault()) & (num5 != null))
				{
					this.Temperature = Min.Value;
				}
			}
			if (Max != null)
			{
				int temperature3 = this.Temperature;
				int? num5 = Max;
				if ((temperature3 > num5.GetValueOrDefault()) & (num5 != null))
				{
					this.Temperature = Max.Value;
				}
			}
			if (this.Temperature != temperature)
			{
				EnvironmentalUpdateEvent.Send(this.ParentObject);
			}
			this.CheckThresholds(Actor);
			return true;
		}

		// Token: 0x06008EFF RID: 36607 RVA: 0x00338D80 File Offset: 0x00336F80
		public void CheckThresholds(GameObject Actor = null)
		{
			if (this.IsVaporizing())
			{
				GameObject.Validate(ref this.InflamedBy);
				if (Actor == null)
				{
					Actor = this.InflamedBy;
				}
				if (VaporizedEvent.Check(this.ParentObject, Actor))
				{
					this.LastDamagedByType = "Vaporized";
					this.LastDamageAccidental = true;
					if (this.ParentObject.IsPlayer())
					{
						Achievement.VAPORIZED.Unlock();
					}
					this.ParentObject.Die(Actor, null, "You were vaporized.", this.ParentObject.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " @@vaporized.", true, null, null, false, false, null, null, null);
					if (this.CurrentCell != null && !this.VaporObject.IsNullOrEmpty())
					{
						GameObject gameObject = GameObject.Create(this.VaporObject, 0, 0, null, null, null, "Vaporization", null);
						Gas gas;
						if (Actor != null && gameObject.TryGetPart<Gas>(out gas))
						{
							gas.Creator = Actor;
						}
						this.CurrentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			else if (this.IsAflame())
			{
				if (!this.WasAflame)
				{
					this.WasAflame = true;
					this.InflamedBy = Actor;
					Burning.ApplyTo(this.ParentObject);
				}
			}
			else if (this.WasAflame)
			{
				this.WasAflame = false;
				this.ParentObject.RemoveEffect<Burning>();
			}
			if (this.IsFrozen())
			{
				if (!this.WasFrozen)
				{
					this.WasFrozen = true;
					this.InflamedBy = Actor;
					Frozen.ApplyTo(this.ParentObject);
				}
			}
			else if (this.WasFrozen)
			{
				this.WasFrozen = false;
				this.ParentObject.RemoveEffect<Frozen>();
			}
			if (this.IsFreezing())
			{
				if (!this.WasFreezing)
				{
					this.WasFreezing = true;
					this.InflamedBy = Actor;
					Freezing.ApplyTo(this.ParentObject);
					return;
				}
			}
			else if (this.WasFreezing)
			{
				this.WasFreezing = false;
				this.ParentObject.RemoveEffect<Freezing>();
			}
		}

		// Token: 0x06008F00 RID: 36608 RVA: 0x00338F78 File Offset: 0x00337178
		public bool CheckHP(int? CurrentHP = null, int? PreviousHP = null, int? MaxHP = null, bool Preregistered = false)
		{
			if (Preregistered)
			{
				this.ParentObject.WillCheckHP(new bool?(false));
			}
			if ((CurrentHP ?? this.ParentObject.hitpoints) <= 0)
			{
				bool flag = this.ParentObject.IsPlayer();
				if (GameObject.Validate(ref this.LastDamagedBy))
				{
					if (this.LastDeathReason.IsNullOrEmpty())
					{
						if (this.LastDamagedBy == this.ParentObject)
						{
							this.LastDeathReason = string.Concat(new string[]
							{
								"You ",
								this.LastDamageAccidental ? "accidentally " : "",
								"killed ",
								this.ParentObject.itself,
								"."
							});
						}
						else
						{
							this.LastDeathReason = string.Concat(new string[]
							{
								"You were ",
								this.LastDamageAccidental ? "accidentally " : "",
								"killed by ",
								this.LastDamagedBy.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
								"."
							});
						}
					}
					if (this.LastThirdPersonDeathReason.IsNullOrEmpty())
					{
						if (this.LastDamagedBy == this.ParentObject)
						{
							this.LastThirdPersonDeathReason = string.Concat(new string[]
							{
								this.ParentObject.It,
								" @@",
								this.LastDamageAccidental ? "accidentally " : "",
								"killed ",
								this.ParentObject.itself,
								"."
							});
						}
						else
						{
							this.LastThirdPersonDeathReason = string.Concat(new string[]
							{
								this.ParentObject.It,
								" were @@",
								this.LastDamageAccidental ? "accidentally " : "",
								"killed by ",
								this.LastDamagedBy.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
								"."
							});
						}
					}
					if (flag)
					{
						if (this.LastDamagedBy.HasProperty("EvilTwin") && this.LastDamagedBy.HasProperty("PlayerCopy"))
						{
							Achievement.KILLED_BY_TWIN.Unlock();
						}
						if (this.LastDamagedBy.Blueprint == "Chute Crab")
						{
							Achievement.KILLED_BY_CHUTE_CRAB.Unlock();
						}
						if (this.LastDamagedBy.IsPlayerLed())
						{
							Achievement.DIE_COMPANION.Unlock();
						}
					}
				}
				if (flag)
				{
					if (this.LastDeathReason != null && this.LastDeathReason.EndsWith("crags of spacetime."))
					{
						Achievement.DIE_NORMALITY.Unlock();
					}
					if (this.LastDamagedByType == "Dessicated")
					{
						Achievement.DIE_THIRST.Unlock();
					}
					if (The.Game.DeathReason.IsNullOrEmpty() && !this.LastDeathReason.IsNullOrEmpty())
					{
						The.Game.DeathReason = this.LastDeathReason;
					}
				}
				GameObject parentObject = this.ParentObject;
				GameObject lastDamagedBy = this.LastDamagedBy;
				string text = null;
				string lastDeathReason = this.LastDeathReason;
				string lastDeathCategory = this.LastDeathCategory;
				parentObject.Die(lastDamagedBy, text, lastDeathReason, this.LastThirdPersonDeathReason, this.LastDamageAccidental, this.LastWeaponDamagedBy, this.LastProjectileDamagedBy, false, false, null, null, lastDeathCategory);
				return true;
			}
			int? num2;
			if (PreviousHP != null)
			{
				int? num = CurrentHP;
				num2 = PreviousHP;
				if (!((num.GetValueOrDefault() < num2.GetValueOrDefault()) & ((num != null) & (num2 != null))))
				{
					return false;
				}
			}
			num2 = CurrentHP;
			int num3 = (MaxHP ?? this.ParentObject.baseHitpoints) / 4;
			if (((num2.GetValueOrDefault() <= num3) & (num2 != null)) && !this.ParentObject.IsCreature && this.ParentObject.HasTagOrProperty("Breakable"))
			{
				this.ParentObject.ForceApplyEffect(new Broken(true, false, false, false), null);
			}
			return false;
		}

		// Token: 0x06008F01 RID: 36609 RVA: 0x0033936C File Offset: 0x0033756C
		public bool RestoreContextRelation(GameObject Object, GameObject ObjectContext, Cell CellContext, BodyPart BodyPartContext, int Relation, bool Silent = true)
		{
			if (Relation == 3 && BodyPartContext != null)
			{
				if (BodyPartContext.Equipped == Object && Object.Equipped == ObjectContext)
				{
					return true;
				}
				if (BodyPartContext.Equip(Object, new int?(0), Silent, false, true, false))
				{
					return true;
				}
			}
			else if (Relation == 2 && ObjectContext != null)
			{
				if (Object.InInventory == ObjectContext)
				{
					return true;
				}
				Inventory inventory = ObjectContext.Inventory;
				if (((inventory != null) ? inventory.AddObject(Object, null, Silent, false, true, null) : null) == Object)
				{
					return true;
				}
			}
			else if (Relation == 1 && CellContext != null)
			{
				if (Object.CurrentCell == CellContext)
				{
					return true;
				}
				if (CellContext.AddObject(Object, false, true, false, false, false, true, true, null, "Context Relation Restore", null, null, null, null, null, null) == Object)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008F02 RID: 36610 RVA: 0x00339414 File Offset: 0x00337614
		private Cell GetBroadcastCell(GameObject Target)
		{
			if (!GameObject.Validate(ref Target))
			{
				return null;
			}
			if (this.Owner == null)
			{
				return null;
			}
			if (this.ParentObject.Brain != null)
			{
				return null;
			}
			if (Target.Owns(this.Owner) && !Target.IsPlayerControlled())
			{
				return null;
			}
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null)
			{
				return null;
			}
			return currentCell;
		}

		// Token: 0x06008F03 RID: 36611 RVA: 0x00339470 File Offset: 0x00337670
		public void CheckBroadcastForHelp(GameObject Target, bool Accidental = false)
		{
			if (this.GetBroadcastCell(Target) == null)
			{
				return;
			}
			double num = (Accidental ? 0.3 : 0.5);
			if (!this.ParentObject.isDamaged(num, false))
			{
				string text = "HelpBroadcastChecksFor" + Target.ID;
				int num2 = (Accidental ? 5 : 3);
				if (this.ParentObject.ModIntProperty(text, 1, false) < num2)
				{
					return;
				}
			}
			this.BroadcastForHelp(Target);
		}

		// Token: 0x06008F04 RID: 36612 RVA: 0x003394E0 File Offset: 0x003376E0
		public void BroadcastForHelp(GameObject Target)
		{
			if (this.GetBroadcastCell(Target) == null)
			{
				return;
			}
			AIHelpBroadcastEvent.Send(this.ParentObject, Target, null, null, 20, 1f, HelpCause.Trespass);
		}

		// Token: 0x06008F05 RID: 36613 RVA: 0x00339502 File Offset: 0x00337702
		public override void BasisError(GameObject Basis, SerializationReader Reader)
		{
			if (Basis.GetBlueprint(false) == null)
			{
				if (Basis.IsPlayer())
				{
					Basis.Blueprint = "Humanoid";
					return;
				}
				Reader.FlagObjectForRemoval(Basis);
			}
		}

		// Token: 0x06008F08 RID: 36616 RVA: 0x00339624 File Offset: 0x00337824
		[CompilerGenerated]
		private bool? <ProcessTakeDamage>g__visible|182_0(ref Physics.<>c__DisplayClass182_0 A_1, ref Physics.<>c__DisplayClass182_1 A_2)
		{
			if (A_2._visible == null)
			{
				A_2._visible = new bool?(A_1.E.HasFlag("IgnoreVisibility") || base.Visible());
			}
			return A_2._visible;
		}

		// Token: 0x04003B76 RID: 15222
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool PhysicsPool = new IPartPool(128);

		// Token: 0x04003B77 RID: 15223
		public static readonly string ATTACK_COMMAND = "Attack";

		// Token: 0x04003B78 RID: 15224
		public const int BASE_TEMPERATURE = 25;

		// Token: 0x04003B79 RID: 15225
		public const int PHYSICS_FLAG_SOLID = 1;

		// Token: 0x04003B7A RID: 15226
		public const int PHYSICS_FLAG_TAKEABLE = 2;

		// Token: 0x04003B7B RID: 15227
		public const int PHYSICS_FLAG_IS_REAL = 4;

		// Token: 0x04003B7C RID: 15228
		public const int PHYSICS_FLAG_USES_TWO_SLOTS = 8;

		// Token: 0x04003B7D RID: 15229
		public const int PHYSICS_FLAG_LAST_DAMAGE_ACCIDENTAL = 16;

		// Token: 0x04003B7E RID: 15230
		public const int PHYSICS_FLAG_WAS_FROZEN = 32;

		// Token: 0x04003B7F RID: 15231
		public const int PHYSICS_FLAG_WAS_AFLAME = 64;

		// Token: 0x04003B80 RID: 15232
		public const int PHYSICS_FLAG_ORGANIC = 128;

		// Token: 0x04003B81 RID: 15233
		public const int PHYSICS_FLAG_WAS_FREEZING = 256;

		// Token: 0x04003B82 RID: 15234
		public int Flags = 2;

		// Token: 0x04003B83 RID: 15235
		[NonSerialized]
		private int AmbientCache = int.MinValue;

		// Token: 0x04003B84 RID: 15236
		public string Category = "Unknown";

		// Token: 0x04003B85 RID: 15237
		public int _Weight;

		// Token: 0x04003B86 RID: 15238
		public int _Temperature = 25;

		// Token: 0x04003B87 RID: 15239
		public int FlameTemperature = 350;

		// Token: 0x04003B88 RID: 15240
		public int VaporTemperature = 10000;

		// Token: 0x04003B89 RID: 15241
		public int FreezeTemperature;

		// Token: 0x04003B8A RID: 15242
		public int BrittleTemperature = -100;

		// Token: 0x04003B8B RID: 15243
		public int BaseElectricalConductivity;

		// Token: 0x04003B8C RID: 15244
		public string Owner;

		// Token: 0x04003B8D RID: 15245
		public float SpecificHeat = 1f;

		// Token: 0x04003B8E RID: 15246
		public Cell _CurrentCell;

		// Token: 0x04003B8F RID: 15247
		public GameObject _InInventory;

		// Token: 0x04003B90 RID: 15248
		public GameObject _Equipped;

		// Token: 0x04003B91 RID: 15249
		public GameObject LastDamagedBy;

		// Token: 0x04003B92 RID: 15250
		public GameObject LastWeaponDamagedBy;

		// Token: 0x04003B93 RID: 15251
		public GameObject LastProjectileDamagedBy;

		// Token: 0x04003B94 RID: 15252
		public GameObject InflamedBy;

		// Token: 0x04003B95 RID: 15253
		public string LastDamagedByType = "";

		// Token: 0x04003B96 RID: 15254
		public string LastDeathReason = "";

		// Token: 0x04003B97 RID: 15255
		public string LastDeathCategory = "";

		// Token: 0x04003B98 RID: 15256
		public string LastThirdPersonDeathReason = "";

		// Token: 0x04003B99 RID: 15257
		[NonSerialized]
		public string ConfusedName;

		// Token: 0x04003B9A RID: 15258
		[NonSerialized]
		private int lastPushSegment = int.MinValue;

		// Token: 0x04003B9B RID: 15259
		[NonSerialized]
		private int lastPushCount;

		// Token: 0x04003B9C RID: 15260
		[NonSerialized]
		private static List<GameObject> DischargeObjects = new List<GameObject>();

		// Token: 0x04003B9D RID: 15261
		[NonSerialized]
		private static List<string> PassingBy = new List<string>();

		// Token: 0x04003B9E RID: 15262
		private static Event eBeforePhysicsRejectObjectEntringCell = new Event("BeforePhysicsRejectObjectEntringCell", "Object", null, "Actual", 1);

		// Token: 0x04003B9F RID: 15263
		private static readonly ProfilerMarker s_PhysicsUpdateTemperature = new ProfilerMarker("s_PhysicsUpdateTemperature");
	}
}
