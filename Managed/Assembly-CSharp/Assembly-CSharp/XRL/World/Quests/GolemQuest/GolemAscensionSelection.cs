using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using XRL.Liquids;
using XRL.Wish;
using XRL.World.Parts;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B32 RID: 2866
	[HasWishCommand]
	[Serializable]
	public class GolemAscensionSelection : GolemMaterialSelection<string, string>
	{
		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06007396 RID: 29590 RVA: 0x002B9A0B File Offset: 0x002B7C0B
		// (set) Token: 0x06007397 RID: 29591 RVA: 0x002B9A13 File Offset: 0x002B7C13
		public override string Material
		{
			get
			{
				return this._Material;
			}
			set
			{
				this._Material = value;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06007398 RID: 29592 RVA: 0x002B9A1C File Offset: 0x002B7C1C
		public override string ID
		{
			get
			{
				return "Power";
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06007399 RID: 29593 RVA: 0x002B9A23 File Offset: 0x002B7C23
		public override string DisplayName
		{
			get
			{
				return "ascension power source";
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x0600739A RID: 29594 RVA: 0x002B9A2A File Offset: 0x002B7C2A
		public override char Key
		{
			get
			{
				return 'p';
			}
		}

		// Token: 0x0600739B RID: 29595 RVA: 0x002B9A30 File Offset: 0x002B7C30
		public override bool IsValid(string Material)
		{
			if (!base.IsValid(Material))
			{
				return false;
			}
			if (!(Material != "neutronflux"))
			{
				return base.GetValidHolders().Any<GameObject>((GameObject x) => x.GetFreeDrams("neutronflux", null, null, null, false) >= 3);
			}
			return true;
		}

		// Token: 0x0600739C RID: 29596 RVA: 0x002B9A84 File Offset: 0x002B7C84
		public override List<string> GetValidMaterials()
		{
			List<string> list = new List<string>();
			if (base.GetValidHolders().Any<GameObject>((GameObject x) => x.GetFreeDrams("neutronflux", null, null, null, false) >= 3))
			{
				list.Add("neutronflux");
			}
			if (The.Game.HasFinishedQuest("If, Then, Else"))
			{
				list.Add("chavvah");
			}
			return list;
		}

		// Token: 0x0600739D RID: 29597 RVA: 0x002B9AEC File Offset: 0x002B7CEC
		public override string GetNameFor(string Material)
		{
			if (Material == "neutronflux")
			{
				string text = "{0} {1} of {2}";
				object obj = 3;
				object obj2 = "drams";
				BaseLiquid liquid = LiquidVolume.GetLiquid(Material);
				return string.Format(text, obj, obj2, ((liquid != null) ? liquid.Name : null) ?? Material);
			}
			return "Chavvah";
		}

		// Token: 0x0600739E RID: 29598 RVA: 0x002B9B3D File Offset: 0x002B7D3D
		public override IRenderable GetIconFor(string Material)
		{
			if (Material == "neutronflux")
			{
				return base.System.Catalyst.GetIconFor(Material);
			}
			return new Renderable(GameObjectFactory.Factory.Blueprints["TerrainEynRoj"]);
		}

		// Token: 0x0600739F RID: 29599 RVA: 0x002B9B78 File Offset: 0x002B7D78
		public override void Apply(GameObject Object)
		{
			base.Apply(Object);
			if (this.Material == "neutronflux")
			{
				foreach (GameObject gameObject in base.GetValidHolders())
				{
					if (gameObject.GetFreeDrams("neutronflux", null, null, null, false) >= 3)
					{
						gameObject.UseDrams(3, this.Material, null, null, null, null, false);
						break;
					}
				}
			}
		}

		// Token: 0x060073A0 RID: 29600 RVA: 0x002B9C04 File Offset: 0x002B7E04
		[WishCommand("golemquest:power", null)]
		private static void Wish()
		{
			GolemAscensionSelection.ReceiveMaterials(The.Player);
		}

		// Token: 0x060073A1 RID: 29601 RVA: 0x002B9C10 File Offset: 0x002B7E10
		public static void ReceiveMaterials(GameObject Object)
		{
			Object.ReceiveObject(GameObjectFactory.Factory.CreateObject("Phial", delegate(GameObject x)
			{
				LiquidVolume liquidVolume = x.LiquidVolume;
				liquidVolume.InitialLiquid = "neutronflux-1000";
				liquidVolume.MaxVolume = 3;
				liquidVolume.StartVolume = 3.ToString();
			}), false, null);
		}

		// Token: 0x04003540 RID: 13632
		public const string BASE_ID = "Power";

		// Token: 0x04003541 RID: 13633
		public const string LIQUID = "neutronflux";

		// Token: 0x04003542 RID: 13634
		public const int DRAMS = 3;

		// Token: 0x04003543 RID: 13635
		public string _Material;
	}
}
