using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000F2E RID: 3886
	[Serializable]
	public class CyberneticsHasRandomImplants : IPart
	{
		// Token: 0x06009AE2 RID: 39650 RVA: 0x003826C4 File Offset: 0x003808C4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AE3 RID: 39651 RVA: 0x003826C7 File Offset: 0x003808C7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == AfterObjectCreatedEvent.ID;
		}

		// Token: 0x06009AE4 RID: 39652 RVA: 0x003826E5 File Offset: 0x003808E5
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!string.IsNullOrEmpty(this.Adjective) && this.ParentObject.AnyInstalledCybernetics())
			{
				E.AddAdjective(this.Adjective, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AE5 RID: 39653 RVA: 0x00382718 File Offset: 0x00380918
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (this.ImplantChance.RollCached().in100())
			{
				int num = 0;
				int num2 = this.LicensesAtLeast.RollCached();
				int intProperty = this.ParentObject.GetIntProperty("CyberneticsLicenses", 0);
				int num3 = 0;
				if (intProperty < num2)
				{
					this.ParentObject.SetIntProperty("CyberneticsLicenses", num2, false);
				}
				else
				{
					num2 = intProperty;
				}
				Body body = this.ParentObject.Body;
				if (body != null)
				{
					IL_01E7:
					while (++num <= 30 && num3 < num2)
					{
						string blueprint = PopulationManager.RollOneFrom(this.ImplantTable, null, null).Blueprint;
						GameObjectBlueprint gameObjectBlueprint;
						if (blueprint == null)
						{
							MetricsManager.LogError("got null blueprint from " + this.ImplantTable);
						}
						else if (!GameObjectFactory.Factory.Blueprints.TryGetValue(blueprint, out gameObjectBlueprint))
						{
							MetricsManager.LogError("got invalid blueprint \"" + blueprint + "\" from " + this.ImplantTable);
						}
						else
						{
							string partParameter = gameObjectBlueprint.GetPartParameter<string>("CyberneticsBaseItem", "Slots", null);
							string[] array = ((partParameter != null) ? partParameter.Split(',', StringSplitOptions.None) : null);
							if (array.IsNullOrEmpty<string>())
							{
								MetricsManager.LogError("Weird blueprint in random cybernetics table: " + blueprint + " from table " + this.ImplantTable);
							}
							else
							{
								array.ShuffleInPlace<string>(null);
								foreach (string text in array)
								{
									List<BodyPart> part = body.GetPart(text);
									part.ShuffleInPlace<BodyPart>(null);
									foreach (BodyPart bodyPart in part)
									{
										if (bodyPart != null && bodyPart._Cybernetics == null)
										{
											GameObject gameObject = GameObject.Create(blueprint);
											CyberneticsBaseItem part2 = gameObject.GetPart<CyberneticsBaseItem>();
											if (part2 != null)
											{
												if (num2 - num3 >= part2.Cost)
												{
													num3 += part2.Cost;
													bodyPart.Implant(gameObject, false, false);
													goto IL_01E7;
												}
												gameObject.Obliterate(null, false, null);
												goto IL_01E7;
											}
											else
											{
												gameObject.Obliterate(null, false, null);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AE6 RID: 39654 RVA: 0x00382934 File Offset: 0x00380B34
		public override bool Render(RenderEvent E)
		{
			if (this.ChangeColor && this.ParentObject.AnyInstalledCybernetics())
			{
				if (E.ColorString == "&C")
				{
					E.ColorString = "&W";
				}
				else
				{
					E.ColorString = "&C";
				}
			}
			return base.Render(E);
		}

		// Token: 0x04004056 RID: 16470
		public string ImplantTable = "Implants_1and2Pointers";

		// Token: 0x04004057 RID: 16471
		public string ImplantChance = "100";

		// Token: 0x04004058 RID: 16472
		public string LicensesAtLeast = "1";

		// Token: 0x04004059 RID: 16473
		public string Adjective = "{{implanted|implanted}}";

		// Token: 0x0400405A RID: 16474
		public bool ChangeColor = true;
	}
}
