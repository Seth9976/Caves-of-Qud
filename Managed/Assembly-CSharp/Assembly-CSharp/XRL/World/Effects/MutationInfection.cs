using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000DAD RID: 3501
	[Serializable]
	public class MutationInfection : Effect
	{
		// Token: 0x06008BD7 RID: 35799 RVA: 0x0031A734 File Offset: 0x00318934
		public MutationInfection()
		{
			this.DisplayName = "&minhabited";
		}

		// Token: 0x06008BD8 RID: 35800 RVA: 0x0031A747 File Offset: 0x00318947
		public MutationInfection(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008BD9 RID: 35801 RVA: 0x0031A756 File Offset: 0x00318956
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008BDA RID: 35802 RVA: 0x0031A759 File Offset: 0x00318959
		public override int GetEffectType()
		{
			return 67108868;
		}

		// Token: 0x06008BDB RID: 35803 RVA: 0x0031A760 File Offset: 0x00318960
		public override bool Apply(GameObject Object)
		{
			return Object.FireEvent(Event.New("ApplyGirshInfection", "Duration", this.Duration));
		}

		// Token: 0x06008BDC RID: 35804 RVA: 0x0031A782 File Offset: 0x00318982
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008BDD RID: 35805 RVA: 0x0031A798 File Offset: 0x00318998
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				this.Duration--;
				if (this.Duration == 0)
				{
					List<BaseMutation> list = new List<BaseMutation>();
					List<BaseMutation> list2 = new List<BaseMutation>();
					GameObject @object = base.Object;
					Mutations part = @object.GetPart<Mutations>();
					foreach (BaseMutation baseMutation in part.MutationList)
					{
						list.Add(baseMutation);
					}
					int i = 0;
					int num = 0;
					IL_01CD:
					while (num < 1 && i < 50)
					{
						IL_008C:
						while (i <= 50)
						{
							i++;
							if (@object.IsEsper())
							{
								MutationsAPI.AddNewMentalMutation(list, 2, 999);
							}
							else if (@object.IsChimera())
							{
								MutationsAPI.AddNewPhysicalMutation(list, 2, 999);
							}
							else if (Stat.RandomLevelUpChoice(0, 1) == 0)
							{
								MutationsAPI.AddNewMentalMutation(list, 2, 999);
							}
							else
							{
								MutationsAPI.AddNewPhysicalMutation(list, 2, 999);
							}
							for (int j = 0; j < list.Count - 1; j++)
							{
								if (list[j].GetDisplayName(true) == list[list.Count - 1].GetDisplayName(true))
								{
									list.RemoveAt(list.Count - 1);
									goto IL_008C;
								}
								using (List<BaseMutation>.Enumerator enumerator = list2.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										if (enumerator.Current.GetDisplayName(true) == list[list.Count - 1].GetDisplayName(true))
										{
											list.RemoveAt(list.Count - 1);
											goto IL_008C;
										}
									}
								}
							}
							list2.Add(list[list.Count - 1]);
							list.RemoveAt(list.Count - 1);
							num++;
							goto IL_01CD;
						}
						break;
					}
					if (list2.Count > 0)
					{
						Popup.Show("You gain " + list2[0].GetDisplayName(true) + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						JournalAPI.AddAccomplishment("Your larva gestated and you gained the " + list2[0].GetDisplayName(true) + " mutation.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
						part.AddMutation(list2[0], 1, true);
					}
				}
			}
			return true;
		}
	}
}
