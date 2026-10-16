using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.Events;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace XRL.CharacterBuilds.Qud.UI
{
	// Token: 0x020005B7 RID: 1463
	[UIView("CharacterCreation:CustomizeCharacter", false, false, false, null, null, false, 0, false, NavCategory = "Chargen", UICanvas = "Chargen/CustomizeCharacter", UICanvasHost = 1)]
	[HasModSensitiveStaticCache]
	public class QudCustomizeCharacterModuleWindow : EmbarkBuilderModuleWindowPrefabBase<QudCustomizeCharacterModule, FrameworkScroller>
	{
		// Token: 0x06003E56 RID: 15958 RVA: 0x00131D8C File Offset: 0x0012FF8C
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			if (base.module.data == null)
			{
				base.module.data = new QudCustomizeCharacterModuleData();
			}
			this.UpdateUI(descriptor);
			base.BeforeShow(descriptor);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00131DB9 File Offset: 0x0012FFB9
		public void UpdateUI(EmbarkBuilderModuleWindowDescriptor descriptor = null)
		{
			base.prefabComponent.BeforeShow(descriptor, this.GetSelections());
			base.prefabComponent.onSelected.RemoveAllListeners();
			base.prefabComponent.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.SelectMenuOption));
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x00131DF9 File Offset: 0x0012FFF9
		public IEnumerable<PrefixMenuOption> GetSelections()
		{
			PrefixMenuOption prefixMenuOption = new PrefixMenuOption();
			prefixMenuOption.Id = "Name";
			prefixMenuOption.Prefix = "Name: ";
			QudCustomizeCharacterModuleData data = base.module.data;
			prefixMenuOption.Description = ((data != null) ? data.name : null) ?? "<random>";
			yield return prefixMenuOption;
			if (Gender.EnableSelection)
			{
				yield return new PrefixMenuOption
				{
					Id = "Gender",
					Prefix = "Gender: ",
					Description = base.module.genderName
				};
			}
			if (PronounSet.EnableSelection)
			{
				yield return new PrefixMenuOption
				{
					Id = "PronounSet",
					Prefix = "Pronoun Set: ",
					Description = base.module.pronounSetName
				};
			}
			if (GameObjectFactory.Factory.GetBlueprintsWithTag("StartingPet", true).Count > 0)
			{
				PrefixMenuOption prefixMenuOption2 = new PrefixMenuOption();
				prefixMenuOption2.Id = "Pet";
				prefixMenuOption2.Prefix = "Pet: ";
				QudCustomizeCharacterModuleWindow.PetData petData = this.GetPets().Where<QudCustomizeCharacterModuleWindow.PetData>(delegate(QudCustomizeCharacterModuleWindow.PetData p)
				{
					string id = p.Id;
					QudCustomizeCharacterModuleData data2 = base.module.data;
					return id == ((data2 != null) ? data2.pet : null);
				}).FirstOrDefault<QudCustomizeCharacterModuleWindow.PetData>();
				prefixMenuOption2.Description = ((petData != null) ? petData.Description : null) ?? "<none>";
				yield return prefixMenuOption2;
			}
			if (Options.EnableSeed)
			{
				yield return new PrefixMenuOption
				{
					Id = "WorldSeed",
					Prefix = "World Seed: ",
					Description = base.module.builder.info.GameSeed
				};
			}
			yield break;
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00131E0C File Offset: 0x0013000C
		public override void RandomSelectionNoUI()
		{
			base.module.setName(base.module.builder.info.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_GENERATERANDOMPLAYERNAME, null, The.Core.GenerateRandomPlayerName(base.module.builder.GetModule<QudSubtypeModule>().data.Subtype)));
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00131E64 File Offset: 0x00130064
		public override void RandomSelection()
		{
			FrameworkDataElement dataAt = base.prefabComponent.scrollContext.GetDataAt(base.prefabComponent.scrollContext.selectedPosition);
			if (((dataAt != null) ? dataAt.Id : null) == "Name")
			{
				base.module.setName(base.module.builder.info.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_GENERATERANDOMPLAYERNAME, null, The.Core.GenerateRandomPlayerName(base.module.builder.GetModule<QudSubtypeModule>().data.Subtype)));
			}
			if (((dataAt != null) ? dataAt.Id : null) == "Pet")
			{
				base.module.setPet(this.GetPets().GetRandomElement<QudCustomizeCharacterModuleWindow.PetData>(null).Id);
			}
			if (((dataAt != null) ? dataAt.Id : null) == "WorldSeed")
			{
				base.module.builder.info.GameSeed = Guid.NewGuid().ToString();
			}
			this.UpdateUI(null);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00131F70 File Offset: 0x00130170
		public async void SelectMenuOption(FrameworkDataElement dataElement)
		{
			if (dataElement.Id == "Pet")
			{
				await this.OnChoosePet();
			}
			if (dataElement.Id == "Name")
			{
				string text = "Enter name:";
				QudCustomizeCharacterModuleData data = base.module.data;
				string text2 = await Popup.AskStringAsync(text, ((data != null) ? data.name : null) ?? "", 80, 0, null, false, true, null, false, null);
				base.module.setName(text2);
			}
			if (dataElement.Id == "WorldSeed")
			{
				string text3 = await Popup.AskStringAsync("Enter world seed:", base.module.builder.info.GameSeed, 80, 0, null, false, true, null, false, null);
				if (text3 != null)
				{
					base.module.builder.info.GameSeed = text3;
				}
			}
			if (dataElement.Id == "Gender")
			{
				Gender gender = await this.OnChooseGenderAsync();
				if (gender != null)
				{
					base.module.data.gender = gender;
				}
			}
			if (dataElement.Id == "PronounSet")
			{
				PronounSet pronounSet = await this.OnChoosePronounSetAsync();
				if (pronounSet == this.fromGenderPlaceholder)
				{
					base.module.data.pronounSet = null;
				}
				else if (pronounSet != null)
				{
					base.module.data.pronounSet = pronounSet;
				}
			}
			this.UpdateUI(null);
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x00131FB0 File Offset: 0x001301B0
		public async Task<Gender> OnChooseGenderAsync()
		{
			List<Gender> availableGenders = Gender.GetAllGenericPersonal();
			List<string> options = availableGenders.Select<Gender, string>((Gender gender) => gender.Name).ToList<string>();
			options.Add("<create new>");
			int num = await Popup.PickOptionAsync("Choose Gender", null, "", options.ToArray(), null, null, null, null, 0, 60, 0, -1, false, true, false, true);
			if (num > -1)
			{
				if (num != options.Count - 1)
				{
					return availableGenders[num];
				}
				int num2 = await Popup.PickOptionAsync("Select Base Gender", null, "", availableGenders.Select<Gender, string>((Gender PronounSet) => PronounSet.Name).ToArray<string>(), null, null, null, null, 0, 60, 0, -1, false, true, false, true);
				if (num2 > -1)
				{
					Gender newGender = new Gender(availableGenders[num2]);
					TaskAwaiter<bool> taskAwaiter = newGender.CustomizeAsync().GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						return newGender;
					}
					newGender = null;
				}
			}
			return null;
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x00131FEC File Offset: 0x001301EC
		public async Task<PronounSet> OnChoosePronounSetAsync()
		{
			List<PronounSet> availablePronounSets = PronounSet.GetAllGenericPersonal();
			List<string> options = new List<string>();
			options.Add("<from gender>");
			options.AddRange(availablePronounSets.Select<PronounSet, string>((PronounSet pronounSet) => pronounSet.Name));
			options.Add("<create new>");
			int num = await Popup.PickOptionAsync("Choose Pronoun Set", null, "", options.ToArray(), null, null, null, null, 0, 60, 0, -1, false, true, false, true);
			if (num > -1)
			{
				if (num == options.Count - 1)
				{
					int num2 = await Popup.PickOptionAsync("Select Base Set", null, "", availablePronounSets.Select<PronounSet, string>((PronounSet PronounSet) => PronounSet.Name).ToArray<string>(), null, null, null, null, 0, 60, 0, -1, false, true, false, true);
					if (num2 > -1)
					{
						PronounSet newPronounSet = new PronounSet(availablePronounSets[num2]);
						TaskAwaiter<bool> taskAwaiter = newPronounSet.CustomizeAsync().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<bool> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<bool>);
						}
						if (taskAwaiter.GetResult())
						{
							return newPronounSet;
						}
						newPronounSet = null;
					}
				}
				else
				{
					if (num == 0)
					{
						return this.fromGenderPlaceholder;
					}
					return availablePronounSets[num - 1];
				}
			}
			return null;
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x0013202F File Offset: 0x0013022F
		public IEnumerable<QudCustomizeCharacterModuleWindow.PetData> GetPets()
		{
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.GetBlueprintsWithTag("StartingPet", true))
			{
				yield return new QudCustomizeCharacterModuleWindow.PetData
				{
					Id = gameObjectBlueprint.Name,
					Icon = new Renderable(gameObjectBlueprint),
					Description = gameObjectBlueprint.GetTag("PetName", gameObjectBlueprint.DisplayName())
				};
			}
			List<GameObjectBlueprint>.Enumerator enumerator = default(List<GameObjectBlueprint>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x00132038 File Offset: 0x00130238
		public async Task OnChoosePet()
		{
			if (this.availablePets == null)
			{
				this.availablePets = this.GetPets().ToList<QudCustomizeCharacterModuleWindow.PetData>();
				if (this.availablePets.Count > 0)
				{
					this.availablePets.Insert(0, new QudCustomizeCharacterModuleWindow.PetData
					{
						Id = null,
						Description = "<none>",
						Icon = null
					});
				}
			}
			if (this.availablePets.Count > 0)
			{
				int num = await Popup.PickOptionAsync("Choose Pet", null, "", this.availablePets.Select<QudCustomizeCharacterModuleWindow.PetData, string>((QudCustomizeCharacterModuleWindow.PetData o) => o.Description).ToArray<string>(), null, this.availablePets.Select<QudCustomizeCharacterModuleWindow.PetData, Renderable>((QudCustomizeCharacterModuleWindow.PetData o) => o.Icon).ToArray<Renderable>(), null, null, 0, 60, 0, -1, false, true, false, true);
				if (base.module.data == null)
				{
					base.module.data = new QudCustomizeCharacterModuleData();
				}
				if (num <= 0)
				{
					base.module.data.pet = null;
				}
				else
				{
					base.module.data.pet = this.availablePets[num].Id;
				}
				base.module.setData(base.module.data);
			}
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x0013207C File Offset: 0x0013027C
		public override UIBreadcrumb GetBreadcrumb()
		{
			return new UIBreadcrumb
			{
				Id = base.GetType().FullName,
				Title = "Customize",
				IconPath = "Items/sw_book1.bmp",
				IconDetailColor = Color.clear,
				IconForegroundColor = The.Color.Gray
			};
		}

		// Token: 0x04001ECE RID: 7886
		private PronounSet fromGenderPlaceholder = new PronounSet();

		// Token: 0x04001ECF RID: 7887
		[ModSensitiveStaticCache(false)]
		private List<QudCustomizeCharacterModuleWindow.PetData> availablePets;

		// Token: 0x020019B5 RID: 6581
		public class PetData : FrameworkDataElement
		{
			// Token: 0x040066CF RID: 26319
			public Renderable Icon;
		}
	}
}
