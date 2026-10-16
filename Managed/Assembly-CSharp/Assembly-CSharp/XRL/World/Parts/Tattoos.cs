using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000E11 RID: 3601
	[Serializable]
	public class Tattoos : IPart
	{
		// Token: 0x060092A8 RID: 37544 RVA: 0x0034D3E5 File Offset: 0x0034B5E5
		public static bool IsSuccess(Tattoos.ApplyResult Result)
		{
			return Result == Tattoos.ApplyResult.Tattooable || Result == Tattoos.ApplyResult.Engravable;
		}

		// Token: 0x17000A82 RID: 2690
		// (set) Token: 0x060092A9 RID: 37545 RVA: 0x0034D3F0 File Offset: 0x0034B5F0
		public string InitialTattoos
		{
			set
			{
				this.ParentObject.SetStringProperty("InitialTattoos", value, false);
			}
		}

		// Token: 0x060092AA RID: 37546 RVA: 0x0034D404 File Offset: 0x0034B604
		public static Tattoos.ApplyResult GetBodyPartGeneralTattooability(BodyPart part)
		{
			if (part.Abstract)
			{
				return Tattoos.ApplyResult.AbstractBodyPart;
			}
			if (!part.Contact)
			{
				return Tattoos.ApplyResult.NonContactBodyPart;
			}
			switch (part.Category)
			{
			case 1:
			case 3:
			case 4:
			case 5:
			case 12:
				return Tattoos.ApplyResult.Tattooable;
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 13:
			case 14:
			case 15:
			case 21:
				return Tattoos.ApplyResult.Engravable;
			}
			return Tattoos.ApplyResult.InappropriateBodyPart;
		}

		// Token: 0x060092AB RID: 37547 RVA: 0x0034D490 File Offset: 0x0034B690
		public static Tattoos.ApplyResult CanApplyTattoo(GameObject Subject, BodyPart Part, bool CanTattoo, bool CanEngrave)
		{
			Tattoos.ApplyResult bodyPartGeneralTattooability = Tattoos.GetBodyPartGeneralTattooability(Part);
			if (bodyPartGeneralTattooability == Tattoos.ApplyResult.Tattooable)
			{
				if (!CanTattoo)
				{
					return Tattoos.ApplyResult.InappropriateBodyPart;
				}
			}
			else if (bodyPartGeneralTattooability == Tattoos.ApplyResult.Engravable && !CanEngrave)
			{
				return Tattoos.ApplyResult.InappropriateBodyPart;
			}
			Tattoos part = Subject.GetPart<Tattoos>();
			if (part != null && !part.CanAddTattoo(Part))
			{
				return Tattoos.ApplyResult.TooManyTattoos;
			}
			return bodyPartGeneralTattooability;
		}

		// Token: 0x060092AC RID: 37548 RVA: 0x0034D4CC File Offset: 0x0034B6CC
		public static Tattoos.ApplyResult ApplyTattoo(GameObject Subject, BodyPart Part, bool CanTattoo, bool CanEngrave, string Desc, string Color = null, string Detail = null)
		{
			Tattoos.ApplyResult bodyPartGeneralTattooability = Tattoos.GetBodyPartGeneralTattooability(Part);
			if (!Tattoos.IsSuccess(bodyPartGeneralTattooability))
			{
				return bodyPartGeneralTattooability;
			}
			if (bodyPartGeneralTattooability == Tattoos.ApplyResult.Tattooable)
			{
				if (!CanTattoo)
				{
					return Tattoos.ApplyResult.InappropriateBodyPart;
				}
			}
			else if (bodyPartGeneralTattooability == Tattoos.ApplyResult.Engravable && !CanEngrave)
			{
				return Tattoos.ApplyResult.InappropriateBodyPart;
			}
			Tattoos tattoos = Subject.RequirePart<Tattoos>(false);
			if (!tattoos.AddTattoo(Part, Desc))
			{
				return Tattoos.ApplyResult.TooManyTattoos;
			}
			if (!Color.IsNullOrEmpty())
			{
				tattoos.ColorString = Color;
				if (!Detail.IsNullOrEmpty())
				{
					tattoos.DetailColor = Detail;
				}
			}
			Subject.CheckMarkOfDeath(null);
			return bodyPartGeneralTattooability;
		}

		// Token: 0x060092AD RID: 37549 RVA: 0x0034D53C File Offset: 0x0034B73C
		public static Tattoos.ApplyResult ApplyTattoo(GameObject Subject, bool CanTattoo, bool CanEngrave, string Desc, string Color = null, string Detail = null)
		{
			Body body = Subject.Body;
			if (body == null)
			{
				return Tattoos.ApplyResult.NoUsableBodyParts;
			}
			List<BodyPart> parts = body.GetParts();
			List<BodyPart> list = new List<BodyPart>(parts.Count);
			foreach (BodyPart bodyPart in parts)
			{
				if (Tattoos.IsSuccess(Tattoos.CanApplyTattoo(Subject, bodyPart, CanTattoo, CanEngrave)))
				{
					list.Add(bodyPart);
				}
			}
			if (list.Count == 0)
			{
				return Tattoos.ApplyResult.NoUsableBodyParts;
			}
			return Tattoos.ApplyTattoo(Subject, list.GetRandomElement<BodyPart>(null), CanTattoo, CanEngrave, Desc, Color, Detail);
		}

		// Token: 0x060092AE RID: 37550 RVA: 0x0034D5D8 File Offset: 0x0034B7D8
		public override IPart DeepCopy(GameObject Parent)
		{
			Tattoos tattoos = base.DeepCopy(Parent) as Tattoos;
			tattoos.Descriptions = new Dictionary<int, List<string>>(this.Descriptions.Count);
			foreach (KeyValuePair<int, List<string>> keyValuePair in this.Descriptions)
			{
				tattoos.Descriptions[keyValuePair.Key] = new List<string>(keyValuePair.Value);
			}
			return tattoos;
		}

		// Token: 0x060092AF RID: 37551 RVA: 0x0034D668 File Offset: 0x0034B868
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write(this.Descriptions.Count);
			foreach (int num in this.Descriptions.Keys)
			{
				Writer.Write(num);
				List<string> list = this.Descriptions[num];
				Writer.Write(list.Count);
				foreach (string text in list)
				{
					Writer.Write(text);
				}
			}
			base.Write(Basis, Writer);
		}

		// Token: 0x060092B0 RID: 37552 RVA: 0x0034D734 File Offset: 0x0034B934
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			int num = Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int num2 = Reader.ReadInt32();
				int num3 = Reader.ReadInt32();
				List<string> list = new List<string>(num3);
				for (int j = 0; j < num3; j++)
				{
					list.Add(Reader.ReadString());
				}
				this.Descriptions.Add(num2, list);
			}
			base.Read(Basis, Reader);
		}

		// Token: 0x060092B1 RID: 37553 RVA: 0x0034D7A0 File Offset: 0x0034B9A0
		public void ValidateTattoos()
		{
			if (this.Descriptions.Count == 0)
			{
				return;
			}
			Body body = this.ParentObject.Body;
			if (body == null)
			{
				this.RemoveTattoos();
				return;
			}
			List<int> list = null;
			foreach (int num in this.Descriptions.Keys)
			{
				if (body.GetPartByID(num, false) == null)
				{
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(num);
				}
			}
			if (list != null)
			{
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					this.RemoveTattoo(list[i]);
					i++;
				}
			}
		}

		// Token: 0x060092B2 RID: 37554 RVA: 0x0034D860 File Offset: 0x0034BA60
		public bool AnyTattoosOnTattooablePart()
		{
			if (this.Descriptions.Count == 0)
			{
				return false;
			}
			Body body = this.ParentObject.Body;
			if (body == null)
			{
				return false;
			}
			foreach (int num in this.Descriptions.Keys)
			{
				BodyPart partByID = body.GetPartByID(num, false);
				if (partByID != null && Tattoos.GetBodyPartGeneralTattooability(partByID) == Tattoos.ApplyResult.Tattooable)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060092B3 RID: 37555 RVA: 0x0034D8F0 File Offset: 0x0034BAF0
		public bool AnyTattoosOnEngravablePart()
		{
			if (this.Descriptions.Count == 0)
			{
				return false;
			}
			Body body = this.ParentObject.Body;
			if (body == null)
			{
				return false;
			}
			foreach (int num in this.Descriptions.Keys)
			{
				BodyPart partByID = body.GetPartByID(num, false);
				if (partByID != null && Tattoos.GetBodyPartGeneralTattooability(partByID) == Tattoos.ApplyResult.Engravable)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060092B4 RID: 37556 RVA: 0x0034D980 File Offset: 0x0034BB80
		public bool CanAddTattoo(BodyPart part)
		{
			return part._ID == 0 || !this.Descriptions.ContainsKey(part._ID) || this.Descriptions[part._ID].Count < RuleSettings.MAXIMUM_TATTOOS_PER_BODY_PART;
		}

		// Token: 0x060092B5 RID: 37557 RVA: 0x0034D9BD File Offset: 0x0034BBBD
		public bool CanAddTattoo(int BodyPartID)
		{
			return !this.Descriptions.ContainsKey(BodyPartID) || this.Descriptions[BodyPartID].Count < RuleSettings.MAXIMUM_TATTOOS_PER_BODY_PART;
		}

		// Token: 0x060092B6 RID: 37558 RVA: 0x0034D9E8 File Offset: 0x0034BBE8
		public bool AddTattoo(BodyPart part, string desc)
		{
			if (part._ID != 0 && this.Descriptions.ContainsKey(part._ID))
			{
				List<string> list = this.Descriptions[part.ID];
				if (list.Count >= RuleSettings.MAXIMUM_TATTOOS_PER_BODY_PART)
				{
					return false;
				}
				list.Add(desc);
			}
			else
			{
				this.Descriptions.Add(part.ID, new List<string> { desc });
			}
			return true;
		}

		// Token: 0x060092B7 RID: 37559 RVA: 0x0034DA58 File Offset: 0x0034BC58
		public bool AddTattoo(int BodyPartID, string desc)
		{
			if (this.Descriptions.ContainsKey(BodyPartID))
			{
				List<string> list = this.Descriptions[BodyPartID];
				if (list.Count >= RuleSettings.MAXIMUM_TATTOOS_PER_BODY_PART)
				{
					return false;
				}
				list.Add(desc);
			}
			else
			{
				this.Descriptions.Add(BodyPartID, new List<string> { desc });
			}
			return true;
		}

		// Token: 0x060092B8 RID: 37560 RVA: 0x0034DAB1 File Offset: 0x0034BCB1
		public override void Remove()
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				parentObject.CheckMarkOfDeath(this);
			}
			base.Remove();
		}

		// Token: 0x060092B9 RID: 37561 RVA: 0x0034DACB File Offset: 0x0034BCCB
		public bool RemoveTattoos()
		{
			bool flag = this.Descriptions.Count > 0;
			this.Descriptions.Clear();
			this.ColorString = null;
			this.DetailColor = null;
			GameObject parentObject = this.ParentObject;
			if (parentObject == null)
			{
				return flag;
			}
			parentObject.CheckMarkOfDeath(null);
			return flag;
		}

		// Token: 0x060092BA RID: 37562 RVA: 0x0034DB08 File Offset: 0x0034BD08
		public bool RemoveTattoo(int BodyPartID)
		{
			bool flag = this.Descriptions.ContainsKey(BodyPartID);
			this.Descriptions.Remove(BodyPartID);
			if (this.Descriptions.Count == 0)
			{
				this.ColorString = null;
				this.DetailColor = null;
			}
			GameObject parentObject = this.ParentObject;
			if (parentObject == null)
			{
				return flag;
			}
			parentObject.CheckMarkOfDeath(null);
			return flag;
		}

		// Token: 0x060092BB RID: 37563 RVA: 0x0034DB5C File Offset: 0x0034BD5C
		public List<int> GetBodyPartIDsSortedByPosition()
		{
			List<int> list = new List<int>(this.Descriptions.Keys);
			if (list.Count < 2)
			{
				return list;
			}
			Body body = this.ParentObject.Body;
			if (body == null)
			{
				return list;
			}
			List<BodyPart> parts = body.GetParts();
			List<int> PartIDs = new List<int>(parts.Count);
			foreach (BodyPart bodyPart in parts)
			{
				if (bodyPart._ID != 0)
				{
					PartIDs.Add(bodyPart._ID);
				}
			}
			list.Sort((int a, int b) => PartIDs.IndexOf(a).CompareTo(PartIDs.IndexOf(b)));
			return list;
		}

		// Token: 0x060092BC RID: 37564 RVA: 0x0034DC20 File Offset: 0x0034BE20
		public string GetTattoosDescription()
		{
			this.ValidateTattoos();
			if (this.Descriptions.Count == 0)
			{
				return null;
			}
			Body body = this.ParentObject.Body;
			if (body == null)
			{
				return null;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			foreach (int num in this.GetBodyPartIDsSortedByPosition())
			{
				BodyPart partByID = body.GetPartByID(num, false);
				List<string> list = this.Descriptions[num];
				stringBuilder.Compound(this.ParentObject.Its).Append(' ').Append(partByID.Name)
					.Append(' ')
					.Append(partByID.Plural ? "bear" : "bears")
					.Append(' ');
				bool flag = Tattoos.GetBodyPartGeneralTattooability(partByID) == Tattoos.ApplyResult.Engravable;
				string text;
				if (list.Count == 1)
				{
					stringBuilder.Append(flag ? "an engraving of " : "a tattoo of ").Append(list[0]);
					text = list[0];
				}
				else
				{
					stringBuilder.Append(flag ? "engravings of " : "tattoos of ").Append(Grammar.MakeAndList(list, true));
					text = list[list.Count - 1];
				}
				if (text.IndexOf('.') != -1 || text.IndexOf('!') != -1 || text.IndexOf('?') != -1)
				{
					char c = ColorUtility.LastCharacterExceptFormatting(text);
					if (c != '.' && c != '!' && c != '?')
					{
						stringBuilder.Append('.');
					}
				}
				else
				{
					stringBuilder.Append('.');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060092BD RID: 37565 RVA: 0x0034DDE8 File Offset: 0x0034BFE8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060092BE RID: 37566 RVA: 0x0034DDEB File Offset: 0x0034BFEB
		public override bool Render(RenderEvent E)
		{
			if (!this.ColorString.IsNullOrEmpty() || !this.DetailColor.IsNullOrEmpty())
			{
				E.ApplyColors(this.ColorString, this.DetailColor, Tattoos.ICON_COLOR_PRIORITY, Tattoos.ICON_COLOR_PRIORITY);
			}
			return true;
		}

		// Token: 0x060092BF RID: 37567 RVA: 0x0034DE24 File Offset: 0x0034C024
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x060092C0 RID: 37568 RVA: 0x0034DE54 File Offset: 0x0034C054
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "ColorString", this.ColorString);
			E.AddEntry(this, "DetailColor", this.DetailColor);
			if (this.Descriptions != null && this.Descriptions.Count > 0)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				foreach (int num in this.GetBodyPartIDsSortedByPosition())
				{
					BodyPart bodyPartByID = this.ParentObject.GetBodyPartByID(num, false);
					foreach (string text in this.Descriptions[num])
					{
						stringBuilder.Append(bodyPartByID.GetOrdinalName()).Append(": ").Append(text)
							.Append('\n');
					}
				}
				E.AddEntry(this, "Descriptions", stringBuilder.ToString());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060092C1 RID: 37569 RVA: 0x0034DF78 File Offset: 0x0034C178
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			string tattoosDescription = this.GetTattoosDescription();
			if (!tattoosDescription.IsNullOrEmpty())
			{
				if (E.Postfix.Length > 0 && E.Postfix[E.Postfix.Length - 1] != '\n')
				{
					E.Postfix.Append('\n');
				}
				E.Postfix.Append('\n').Append(tattoosDescription);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060092C2 RID: 37570 RVA: 0x0034DFE7 File Offset: 0x0034C1E7
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject && Cloning.IsCloning(E.Context))
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060092C3 RID: 37571 RVA: 0x0034E014 File Offset: 0x0034C214
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			string stringProperty = this.ParentObject.GetStringProperty("InitialTattoos", null);
			if (stringProperty != null)
			{
				this.ParentObject.RemoveStringProperty("InitialTattoos");
				Body body = this.ParentObject.Body;
				if (body != null)
				{
					foreach (string text in stringProperty.Split('|', StringSplitOptions.None))
					{
						string[] array2 = text.Split(Tattoos.splitterColon, 2);
						if (array2.Length == 2)
						{
							string text2 = array2[0];
							string text3 = array2[1];
							if (text2 == "*")
							{
								Tattoos.ApplyTattoo(this.ParentObject, true, true, text3, null, null);
							}
							else
							{
								BodyPart bodyPart = body.GetPartByName(text2) ?? body.GetFirstPart(text2);
								if (bodyPart != null)
								{
									Tattoos.ApplyTattoo(this.ParentObject, bodyPart, true, true, text3, null, null);
								}
								else
								{
									MetricsManager.LogError(string.Concat(new string[]
									{
										"could not find body part ",
										text2,
										" on ",
										this.ParentObject.Blueprint,
										" for InitialTattoos spec ",
										text
									}));
								}
							}
						}
						else
						{
							MetricsManager.LogError("bad InitialTattoos spec: " + text);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060092C4 RID: 37572 RVA: 0x0034E152 File Offset: 0x0034C352
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060092C5 RID: 37573 RVA: 0x0034E155 File Offset: 0x0034C355
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("HasTattoo");
			base.Register(Object, Registrar);
		}

		// Token: 0x060092C6 RID: 37574 RVA: 0x0034E16C File Offset: 0x0034C36C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "HasTattoo")
			{
				string stringParameter = E.GetStringParameter("MatchText", null);
				if (!stringParameter.IsNullOrEmpty())
				{
					CompareOptions compareOptions = (E.HasFlag("CaseSensitive") ? CompareOptions.None : CompareOptions.IgnoreCase);
					foreach (List<string> list in this.Descriptions.Values)
					{
						using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (enumerator2.Current.Contains(stringParameter, compareOptions))
								{
									return false;
								}
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003CC0 RID: 15552
		public static readonly int ICON_COLOR_PRIORITY = 150;

		// Token: 0x04003CC1 RID: 15553
		[NonSerialized]
		public Dictionary<int, List<string>> Descriptions = new Dictionary<int, List<string>>();

		// Token: 0x04003CC2 RID: 15554
		public string ColorString;

		// Token: 0x04003CC3 RID: 15555
		public string DetailColor;

		// Token: 0x04003CC4 RID: 15556
		[NonSerialized]
		public static char[] splitterColon = new char[] { ':' };

		// Token: 0x02001C5F RID: 7263
		public enum ApplyResult
		{
			// Token: 0x04006FF4 RID: 28660
			Tattooable,
			// Token: 0x04006FF5 RID: 28661
			Engravable,
			// Token: 0x04006FF6 RID: 28662
			TooManyTattoos,
			// Token: 0x04006FF7 RID: 28663
			AbstractBodyPart,
			// Token: 0x04006FF8 RID: 28664
			NonContactBodyPart,
			// Token: 0x04006FF9 RID: 28665
			InappropriateBodyPart,
			// Token: 0x04006FFA RID: 28666
			NoUsableBodyParts
		}
	}
}
