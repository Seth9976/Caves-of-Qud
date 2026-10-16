using System;
using System.Text;
using XRL.Language;
using XRL.Messages;
using XRL.UI;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F0 RID: 2544
	public static class Messaging
	{
		// Token: 0x06006C85 RID: 27781 RVA: 0x00233B7C File Offset: 0x00231D7C
		private static void HandleMessage(GameObject Source, string Msg, char Color = ' ', bool FromDialog = false, bool UsePopup = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			if (UsePopup || (FromDialog && Source != null && Source.IsPlayer()))
			{
				Popup.Show(Msg, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			if (ColorAsGoodFor != null || ColorAsBadFor != null)
			{
				MessageQueue.AddPlayerMessage(ColorCoding.ConsequentialColorize(Msg, ColorAsGoodFor, ColorAsBadFor, Color), null, true);
				return;
			}
			MessageQueue.AddPlayerMessage(Msg, Color, true);
		}

		// Token: 0x06006C86 RID: 27782 RVA: 0x00233BD0 File Offset: 0x00231DD0
		private static void HandleMessage(GameObject Source, StringBuilder Msg, char Color = ' ', bool FromDialog = false, bool UsePopup = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			Messaging.HandleMessage(Source, Msg.ToString(), Color, FromDialog, UsePopup, ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x00233BE6 File Offset: 0x00231DE6
		public static void EmitMessage(GameObject Source, string Msg, char Color = ' ', bool FromDialog = false, bool UsePopup = false, bool AlwaysVisible = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			if (Source != null && (AlwaysVisible || Source.IsPlayer() || Source.IsVisible()))
			{
				Messaging.HandleMessage(Source, Msg, Color, FromDialog, UsePopup, ColorAsGoodFor, ColorAsBadFor);
			}
		}

		// Token: 0x06006C88 RID: 27784 RVA: 0x00233C0E File Offset: 0x00231E0E
		public static void EmitMessage(GameObject Source, StringBuilder Msg, char Color = ' ', bool FromDialog = false, bool UsePopup = false, bool AlwaysVisible = false, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			Messaging.EmitMessage(Source, Msg.ToString(), Color, FromDialog, UsePopup, AlwaysVisible, ColorAsGoodFor, ColorAsBadFor);
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x00233C28 File Offset: 0x00231E28
		public static void XDidY(GameObject Actor, string Verb, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, GameObject SubjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			if (!UsePopup && FromDialog)
			{
				bool flag;
				if (SubjectPossessedBy != null)
				{
					flag = SubjectPossessedBy.IsPlayer();
				}
				else if (Actor == null)
				{
					flag = false;
				}
				else
				{
					GameObject holder = Actor.Holder;
					bool? flag2 = ((holder != null) ? new bool?(holder.IsPlayer()) : null);
					bool flag3 = true;
					flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
				}
				if (flag)
				{
					UsePopup = true;
				}
			}
			string text = Color ?? ColorCoding.ConsequentialColor(ColorAsGoodFor, ColorAsBadFor);
			if (SubjectOverride == null && Actor != null && Actor.IsPlayer())
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				if (!text.IsNullOrEmpty())
				{
					stringBuilder.Append("{{").Append(text).Append('|');
				}
				stringBuilder.Append("You ").Append(Verb);
				Messaging.AppendWithSpaceIfNeeded(stringBuilder, Extra);
				stringBuilder.Append(EndMark ?? ".");
				if (!text.IsNullOrEmpty())
				{
					stringBuilder.Append("}}");
				}
				Messaging.HandleMessage(Source ?? Actor ?? The.Player, stringBuilder, ' ', FromDialog, UsePopup, null, null);
				return;
			}
			if (!AlwaysVisible)
			{
				GameObject gameObject = UseVisibilityOf ?? Source ?? Actor;
				if (gameObject == null || !gameObject.IsVisible())
				{
					return;
				}
			}
			StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
			if (!text.IsNullOrEmpty())
			{
				stringBuilder2.Append("{{").Append(text).Append('|');
			}
			string text2 = null;
			if (DescribeSubjectDirection || DescribeSubjectDirectionLate)
			{
				text2 = The.Player.DescribeDirectionToward(SubjectPossessedBy ?? Source ?? Actor ?? The.Player, false, false);
			}
			string text3 = null;
			bool flag4 = IndefiniteSubject;
			if (SubjectPossessedBy != null && (text2.IsNullOrEmpty() || DescribeSubjectDirectionLate))
			{
				if (SubjectPossessedBy.IsPlayer())
				{
					text3 = "your";
					text2 = null;
				}
				else
				{
					int maxValue = int.MaxValue;
					string text4 = null;
					string text5 = null;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag3 = !UseFullNames;
					text3 = Grammar.MakePossessive(SubjectPossessedBy.One(maxValue, text4, text5, flag5, flag6, flag7, flag8, flag9, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false));
				}
				flag4 = false;
			}
			else
			{
				GameObject gameObject2 = ((Actor != null) ? Actor.Holder : null);
				if (gameObject2 != null)
				{
					if (gameObject2.IsPlayer())
					{
						text3 = "your";
						text2 = null;
					}
					else
					{
						GameObject gameObject3 = gameObject2;
						int maxValue2 = int.MaxValue;
						string text6 = null;
						string text7 = null;
						bool flag10 = false;
						bool flag11 = false;
						bool flag12 = false;
						bool flag13 = false;
						bool flag14 = false;
						bool flag3 = !UseFullNames;
						text3 = Grammar.MakePossessive(gameObject3.One(maxValue2, text6, text7, flag10, flag11, flag12, flag13, flag14, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false));
					}
					flag4 = false;
				}
			}
			if (SubjectOverride == null)
			{
				if (Actor != null)
				{
					StringBuilder stringBuilder3 = stringBuilder2;
					int maxValue3 = int.MaxValue;
					string text8 = null;
					string text9 = null;
					bool flag15 = false;
					bool flag16 = false;
					bool flag17 = false;
					bool flag18 = false;
					bool flag19 = false;
					bool flag3 = !UseFullNames;
					stringBuilder3.Append(Actor.One(maxValue3, text8, text9, flag15, flag16, flag17, flag18, flag19, !UseFullNames, flag3, false, flag4, text3, true, true, false, null, false, null, false));
				}
			}
			else if (stringBuilder2.Length > 0)
			{
				if (!text3.IsNullOrEmpty())
				{
					stringBuilder2.Append(text3).Append(' ');
				}
				stringBuilder2.Append(SubjectOverride);
			}
			else if (!text3.IsNullOrEmpty())
			{
				stringBuilder2.Append(text3.Capitalize()).Append(' ').Append(SubjectOverride);
			}
			else
			{
				stringBuilder2.Append(SubjectOverride.Capitalize());
			}
			if (!text2.IsNullOrEmpty() && !DescribeSubjectDirectionLate)
			{
				if (SubjectPossessedBy != null && (Actor == null || !Actor.HasProperName))
				{
					StringBuilder stringBuilder4 = stringBuilder2.Append(" of ");
					int maxValue4 = int.MaxValue;
					string text10 = null;
					string text11 = null;
					bool flag20 = false;
					bool flag21 = false;
					bool flag22 = false;
					bool flag23 = false;
					bool flag24 = false;
					bool flag3 = !UseFullNames;
					stringBuilder4.Append(SubjectPossessedBy.one(maxValue4, text10, text11, flag20, flag21, flag22, flag23, flag24, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false)).Append(' ').Append(text2);
				}
				else
				{
					stringBuilder2.Append(' ').Append(text2);
				}
			}
			if (Actor != null)
			{
				stringBuilder2.Append(Actor.GetVerb(Verb, true, false, false, true, false));
			}
			else
			{
				stringBuilder2.Append(' ').Append(Verb);
			}
			Messaging.AppendWithSpaceIfNeeded(stringBuilder2, Extra);
			if (!text2.IsNullOrEmpty() && DescribeSubjectDirectionLate)
			{
				stringBuilder2.Append(' ').Append(text2);
			}
			stringBuilder2.Append(EndMark ?? ".");
			if (!text.IsNullOrEmpty())
			{
				stringBuilder2.Append("}}");
			}
			Messaging.HandleMessage(Source ?? Actor ?? The.Player, stringBuilder2, ' ', FromDialog, UsePopup, null, null);
		}

		// Token: 0x06006C8A RID: 27786 RVA: 0x0023404C File Offset: 0x0023224C
		public static void XDidYToZ(GameObject Actor, string Verb, string Preposition = null, GameObject Object = null, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteObject = false, bool IndefiniteObjectForOthers = false, bool PossessiveObject = false, GameObject SubjectPossessedBy = null, GameObject ObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			if (!GameObject.Validate(ref Object))
			{
				return;
			}
			try
			{
				if (!UsePopup && FromDialog)
				{
					if (!Object.IsPlayer())
					{
						bool flag;
						if (SubjectPossessedBy != null)
						{
							flag = SubjectPossessedBy.IsPlayer();
						}
						else if (Actor == null)
						{
							flag = false;
						}
						else
						{
							GameObject holder = Actor.Holder;
							bool? flag2 = ((holder != null) ? new bool?(holder.IsPlayer()) : null);
							bool flag3 = true;
							flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
						}
						if (!flag)
						{
							bool flag4;
							if (ObjectPossessedBy != null)
							{
								flag4 = ObjectPossessedBy.IsPlayer();
							}
							else if (Object == null)
							{
								flag4 = false;
							}
							else
							{
								GameObject holder2 = Object.Holder;
								bool? flag2 = ((holder2 != null) ? new bool?(holder2.IsPlayer()) : null);
								bool flag3 = true;
								flag4 = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
							}
							if (!flag4)
							{
								goto IL_00B8;
							}
						}
					}
					UsePopup = true;
				}
				IL_00B8:
				if (SubjectOverride == null && Actor != null && Actor.IsPlayer())
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					string text = Color ?? ColorCoding.ConsequentialColor(ColorAsGoodFor, ColorAsBadFor);
					if (!text.IsNullOrEmpty())
					{
						stringBuilder.Append("{{").Append(text).Append('|');
					}
					stringBuilder.Append("You ").Append(Verb).Append(' ');
					if (!Preposition.IsNullOrEmpty())
					{
						stringBuilder.Append(Preposition).Append(' ');
					}
					if (Object.IsPlayer())
					{
						stringBuilder.Append(PossessiveObject ? Object.its : Object.itself);
					}
					else
					{
						string text2 = null;
						bool flag5 = IndefiniteObject;
						bool flag3;
						if (ObjectPossessedBy != null)
						{
							if (ObjectPossessedBy == Actor)
							{
								text2 = ObjectPossessedBy.its;
							}
							else
							{
								int maxValue = int.MaxValue;
								string text3 = null;
								string text4 = null;
								bool flag6 = false;
								bool flag7 = false;
								bool flag8 = false;
								bool flag9 = false;
								bool flag10 = false;
								flag3 = !UseFullNames;
								text2 = Grammar.MakePossessive(ObjectPossessedBy.One(maxValue, text3, text4, flag6, flag7, flag8, flag9, flag10, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false));
							}
							flag5 = false;
						}
						GameObject gameObject = Object;
						int maxValue2 = int.MaxValue;
						string text5 = null;
						string text6 = null;
						bool flag11 = false;
						bool flag12 = false;
						bool flag13 = false;
						bool flag14 = false;
						bool flag15 = false;
						flag3 = !UseFullNames;
						string text7 = gameObject.one(maxValue2, text5, text6, flag11, flag12, flag13, flag14, flag15, !UseFullNames, flag3, false, flag5, text2, true, true, false, null, false, null, false);
						if (PossessiveObject)
						{
							text7 = Grammar.MakePossessive(text7);
						}
						stringBuilder.Append(text7);
					}
					Messaging.AppendWithSpaceIfNeeded(stringBuilder, Extra);
					stringBuilder.Append(EndMark ?? ".");
					if (!text.IsNullOrEmpty())
					{
						stringBuilder.Append("}}");
					}
					Messaging.HandleMessage(Source ?? Actor ?? The.Player, stringBuilder, ' ', FromDialog, UsePopup, null, null);
				}
				else
				{
					if (!AlwaysVisible)
					{
						GameObject gameObject2 = UseVisibilityOf ?? Source ?? Actor;
						if (gameObject2 == null || !gameObject2.IsVisible())
						{
							goto IL_0680;
						}
					}
					StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
					string text8 = Color ?? ColorCoding.ConsequentialColor(ColorAsGoodFor, ColorAsBadFor);
					if (!text8.IsNullOrEmpty())
					{
						stringBuilder2.Append("{{").Append(text8).Append('|');
					}
					string text9 = null;
					if (DescribeSubjectDirection || DescribeSubjectDirectionLate)
					{
						text9 = The.Player.DescribeDirectionToward(SubjectPossessedBy ?? Source ?? Actor ?? The.Player, false, false);
					}
					string text10 = null;
					bool flag16 = IndefiniteSubject;
					if (SubjectPossessedBy != null && (text9.IsNullOrEmpty() || DescribeSubjectDirectionLate))
					{
						if (SubjectPossessedBy.IsPlayer())
						{
							text10 = "your";
							text9 = null;
						}
						else
						{
							int maxValue3 = int.MaxValue;
							string text11 = null;
							string text12 = null;
							bool flag17 = false;
							bool flag18 = false;
							bool flag19 = false;
							bool flag20 = false;
							bool flag21 = false;
							bool flag3 = !UseFullNames;
							text10 = Grammar.MakePossessive(SubjectPossessedBy.One(maxValue3, text11, text12, flag17, flag18, flag19, flag20, flag21, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false));
						}
					}
					else
					{
						GameObject gameObject3 = ((Actor != null) ? Actor.Holder : null);
						if (gameObject3 != null)
						{
							if (gameObject3.IsPlayer())
							{
								text10 = "your";
								text9 = null;
							}
							else
							{
								GameObject gameObject4 = gameObject3;
								int maxValue4 = int.MaxValue;
								string text13 = null;
								string text14 = null;
								bool flag22 = false;
								bool flag23 = false;
								bool flag24 = false;
								bool flag25 = false;
								bool flag26 = false;
								bool flag3 = !UseFullNames;
								text10 = Grammar.MakePossessive(gameObject4.One(maxValue4, text13, text14, flag22, flag23, flag24, flag25, flag26, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false));
							}
							flag16 = false;
						}
					}
					if (SubjectOverride == null)
					{
						if (Actor != null)
						{
							StringBuilder stringBuilder3 = stringBuilder2;
							int maxValue5 = int.MaxValue;
							string text15 = null;
							string text16 = null;
							bool flag27 = false;
							bool flag28 = false;
							bool flag29 = false;
							bool flag30 = false;
							bool flag31 = false;
							bool flag3 = !UseFullNames;
							stringBuilder3.Append(Actor.One(maxValue5, text15, text16, flag27, flag28, flag29, flag30, flag31, !UseFullNames, flag3, false, flag16, text10, true, true, false, null, false, null, false));
						}
					}
					else if (stringBuilder2.Length > 0)
					{
						if (!text10.IsNullOrEmpty())
						{
							stringBuilder2.Append(text10).Append(' ');
						}
						stringBuilder2.Append(SubjectOverride);
					}
					else if (!text10.IsNullOrEmpty())
					{
						stringBuilder2.Append(text10.Capitalize()).Append(' ').Append(SubjectOverride);
					}
					else
					{
						stringBuilder2.Append(SubjectOverride.Capitalize());
					}
					if (!text9.IsNullOrEmpty() && !DescribeSubjectDirectionLate)
					{
						if (SubjectPossessedBy != null && (Actor == null || !Actor.HasProperName))
						{
							StringBuilder stringBuilder4 = stringBuilder2.Append(" of ");
							int maxValue6 = int.MaxValue;
							string text17 = null;
							string text18 = null;
							bool flag32 = false;
							bool flag33 = false;
							bool flag34 = false;
							bool flag35 = false;
							bool flag36 = false;
							bool flag3 = !UseFullNames;
							stringBuilder4.Append(SubjectPossessedBy.one(maxValue6, text17, text18, flag32, flag33, flag34, flag35, flag36, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false)).Append(' ').Append(text9);
						}
						else
						{
							stringBuilder2.Append(' ').Append(text9);
						}
					}
					if (Actor != null)
					{
						stringBuilder2.Append(Actor.GetVerb(Verb, true, false, false, true, false));
					}
					else
					{
						stringBuilder2.Append(' ').Append(Verb);
					}
					Messaging.AppendWithSpaceIfNeeded(stringBuilder2, Preposition);
					stringBuilder2.Append(' ');
					if (Actor != null && Object == Actor && SubjectOverride == null)
					{
						stringBuilder2.Append(PossessiveObject ? Actor.its : Actor.itself);
					}
					else if (Object.IsPlayer())
					{
						stringBuilder2.Append(PossessiveObject ? "your" : "you");
					}
					else
					{
						string text19 = null;
						bool flag37 = IndefiniteObject || IndefiniteObjectForOthers;
						bool flag3;
						if (ObjectPossessedBy != null)
						{
							if (ObjectPossessedBy == Actor && SubjectOverride == null)
							{
								text19 = ObjectPossessedBy.its;
							}
							else
							{
								int maxValue7 = int.MaxValue;
								string text20 = null;
								string text21 = null;
								bool flag38 = false;
								bool flag39 = false;
								bool flag40 = false;
								bool flag41 = false;
								bool flag42 = false;
								flag3 = !UseFullNames;
								text19 = Grammar.MakePossessive(ObjectPossessedBy.One(maxValue7, text20, text21, flag38, flag39, flag40, flag41, flag42, !UseFullNames, flag3, false, IndefiniteObject, null, true, true, false, null, false, null, false));
							}
							flag37 = false;
						}
						GameObject gameObject5 = Object;
						int maxValue8 = int.MaxValue;
						string text22 = null;
						string text23 = null;
						bool flag43 = false;
						bool flag44 = false;
						bool flag45 = false;
						bool flag46 = false;
						bool flag47 = false;
						flag3 = !UseFullNames;
						string text24 = gameObject5.one(maxValue8, text22, text23, flag43, flag44, flag45, flag46, flag47, !UseFullNames, flag3, false, flag37, text19, true, true, false, null, false, null, false);
						if (PossessiveObject)
						{
							text24 = Grammar.MakePossessive(text24);
						}
						stringBuilder2.Append(text24);
					}
					Messaging.AppendWithSpaceIfNeeded(stringBuilder2, Extra);
					if (!text9.IsNullOrEmpty() && DescribeSubjectDirectionLate)
					{
						stringBuilder2.Append(' ').Append(text9);
					}
					stringBuilder2.Append(EndMark ?? ".");
					if (!text8.IsNullOrEmpty())
					{
						stringBuilder2.Append("}}");
					}
					Messaging.HandleMessage(Source ?? Actor ?? The.Player, stringBuilder2, ' ', FromDialog, UsePopup, null, null);
				}
				IL_0680:;
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("XDidYToZ", ex, "game_exception");
			}
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x0023470C File Offset: 0x0023290C
		public static void WDidXToYWithZ(GameObject Actor, string Verb, string DirectPreposition, GameObject DirectObject, string IndirectPreposition, GameObject IndirectObject, string Extra = null, string EndMark = null, string SubjectOverride = null, string Color = null, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, bool UseFullNames = false, bool IndefiniteSubject = false, bool IndefiniteDirectObject = false, bool IndefiniteIndirectObject = false, bool IndefiniteDirectObjectForOthers = false, bool IndefiniteIndirectObjectForOthers = false, bool PossessiveDirectObject = false, bool PossessiveIndirectObject = false, GameObject SubjectPossessedBy = null, GameObject DirectObjectPossessedBy = null, GameObject IndirectObjectPossessedBy = null, GameObject Source = null, bool DescribeSubjectDirection = false, bool DescribeSubjectDirectionLate = false, bool AlwaysVisible = false, bool FromDialog = false, bool UsePopup = false, GameObject UseVisibilityOf = null)
		{
			if (!GameObject.Validate(ref DirectObject) || !GameObject.Validate(ref IndirectObject))
			{
				return;
			}
			if (!UsePopup && FromDialog)
			{
				if (!DirectObject.IsPlayer() && !IndirectObject.IsPlayer())
				{
					bool flag;
					if (SubjectPossessedBy != null)
					{
						flag = SubjectPossessedBy.IsPlayer();
					}
					else if (Actor == null)
					{
						flag = false;
					}
					else
					{
						GameObject holder = Actor.Holder;
						bool? flag2 = ((holder != null) ? new bool?(holder.IsPlayer()) : null);
						bool flag3 = true;
						flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
					}
					if (!flag)
					{
						bool flag4;
						if (DirectObjectPossessedBy != null)
						{
							flag4 = DirectObjectPossessedBy.IsPlayer();
						}
						else if (DirectObject == null)
						{
							flag4 = false;
						}
						else
						{
							GameObject holder2 = DirectObject.Holder;
							bool? flag2 = ((holder2 != null) ? new bool?(holder2.IsPlayer()) : null);
							bool flag3 = true;
							flag4 = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
						}
						if (!flag4)
						{
							bool flag5;
							if (IndirectObjectPossessedBy != null)
							{
								flag5 = IndirectObjectPossessedBy.IsPlayer();
							}
							else if (IndirectObject == null)
							{
								flag5 = false;
							}
							else
							{
								GameObject holder3 = IndirectObject.Holder;
								bool? flag2 = ((holder3 != null) ? new bool?(holder3.IsPlayer()) : null);
								bool flag3 = true;
								flag5 = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
							}
							if (!flag5)
							{
								goto IL_011A;
							}
						}
					}
				}
				UsePopup = true;
			}
			IL_011A:
			if (SubjectOverride == null && Actor != null && Actor.IsPlayer())
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				string text = Color ?? ColorCoding.ConsequentialColor(ColorAsGoodFor, ColorAsBadFor);
				if (!text.IsNullOrEmpty())
				{
					stringBuilder.Append("{{").Append(text).Append('|');
				}
				stringBuilder.Append("You ").Append(Verb);
				Messaging.AppendWithSpaceIfNeeded(stringBuilder, DirectPreposition);
				stringBuilder.Append(' ');
				if (DirectObject.IsPlayer())
				{
					stringBuilder.Append(PossessiveDirectObject ? DirectObject.its : DirectObject.itself);
				}
				else
				{
					string text2 = null;
					bool flag6 = IndefiniteDirectObject;
					if (DirectObjectPossessedBy != null)
					{
						text2 = DirectObjectPossessedBy.its;
						flag6 = false;
					}
					GameObject gameObject = DirectObject;
					int maxValue = int.MaxValue;
					string text3 = null;
					string text4 = null;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag10 = false;
					bool flag11 = false;
					bool flag3 = !UseFullNames;
					string text5 = gameObject.one(maxValue, text3, text4, flag7, flag8, flag9, flag10, flag11, !UseFullNames, flag3, false, flag6, text2, true, true, false, null, false, null, false);
					if (PossessiveDirectObject)
					{
						text5 = Grammar.MakePossessive(text5);
					}
					stringBuilder.Append(text5);
				}
				Messaging.AppendWithSpaceIfNeeded(stringBuilder, IndirectPreposition);
				stringBuilder.Append(' ');
				if (IndirectObject == DirectObject)
				{
					stringBuilder.Append(PossessiveIndirectObject ? IndirectObject.itself : IndirectObject.them);
				}
				else if (Actor != null && IndirectObject == Actor && SubjectOverride == null)
				{
					stringBuilder.Append(PossessiveIndirectObject ? Actor.its : Actor.itself);
				}
				else if (IndirectObject.IsPlayer())
				{
					stringBuilder.Append(PossessiveIndirectObject ? "yours" : "you");
				}
				else
				{
					string text6 = null;
					if (IndirectObjectPossessedBy != null)
					{
						text6 = IndirectObjectPossessedBy.its;
					}
					GameObject gameObject2 = IndirectObject;
					int maxValue2 = int.MaxValue;
					string text7 = null;
					string text8 = null;
					bool flag12 = false;
					bool flag13 = false;
					bool flag14 = false;
					bool flag15 = false;
					bool flag16 = false;
					bool flag3 = !UseFullNames;
					string text9 = gameObject2.one(maxValue2, text7, text8, flag12, flag13, flag14, flag15, flag16, !UseFullNames, flag3, false, IndefiniteIndirectObject, text6, true, true, false, null, false, null, false);
					if (PossessiveIndirectObject)
					{
						text9 = Grammar.MakePossessive(text9);
					}
					stringBuilder.Append(text9);
				}
				Messaging.AppendWithSpaceIfNeeded(stringBuilder, Extra);
				stringBuilder.Append(EndMark ?? ".");
				if (!text.IsNullOrEmpty())
				{
					stringBuilder.Append("}}");
				}
				Messaging.HandleMessage(Source ?? Actor ?? The.Player, stringBuilder, ' ', FromDialog, UsePopup, null, null);
				return;
			}
			if (!AlwaysVisible)
			{
				GameObject gameObject3 = UseVisibilityOf ?? Source ?? Actor;
				if (gameObject3 == null || !gameObject3.IsVisible())
				{
					return;
				}
			}
			StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
			string text10 = Color ?? ColorCoding.ConsequentialColor(ColorAsGoodFor, ColorAsBadFor);
			if (!text10.IsNullOrEmpty())
			{
				stringBuilder2.Append("{{").Append(text10).Append('|');
			}
			string text11 = null;
			if (DescribeSubjectDirection || DescribeSubjectDirectionLate)
			{
				text11 = The.Player.DescribeDirectionToward(SubjectPossessedBy ?? Source ?? Actor ?? The.Player, false, false);
			}
			string text12 = null;
			bool flag17 = IndefiniteSubject;
			if (SubjectPossessedBy != null && (text11.IsNullOrEmpty() || DescribeSubjectDirectionLate))
			{
				if (SubjectPossessedBy.IsPlayer())
				{
					text12 = "your";
					text11 = null;
				}
				else
				{
					int maxValue3 = int.MaxValue;
					string text13 = null;
					string text14 = null;
					bool flag18 = false;
					bool flag19 = false;
					bool flag20 = false;
					bool flag21 = false;
					bool flag22 = false;
					bool flag3 = !UseFullNames;
					text12 = Grammar.MakePossessive(SubjectPossessedBy.One(maxValue3, text13, text14, flag18, flag19, flag20, flag21, flag22, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false));
				}
				flag17 = false;
			}
			else
			{
				GameObject gameObject4 = ((Actor != null) ? Actor.Holder : null);
				if (gameObject4 != null)
				{
					if (gameObject4.IsPlayer())
					{
						text12 = "your";
						text11 = null;
					}
					else
					{
						GameObject gameObject5 = gameObject4;
						int maxValue4 = int.MaxValue;
						string text15 = null;
						string text16 = null;
						bool flag23 = false;
						bool flag24 = false;
						bool flag25 = false;
						bool flag26 = false;
						bool flag27 = false;
						bool flag3 = !UseFullNames;
						text12 = Grammar.MakePossessive(gameObject5.One(maxValue4, text15, text16, flag23, flag24, flag25, flag26, flag27, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false));
					}
					flag17 = false;
				}
			}
			if (SubjectOverride == null)
			{
				if (Actor != null)
				{
					StringBuilder stringBuilder3 = stringBuilder2;
					int maxValue5 = int.MaxValue;
					string text17 = null;
					string text18 = null;
					bool flag28 = false;
					bool flag29 = false;
					bool flag30 = false;
					bool flag31 = false;
					bool flag32 = false;
					bool flag3 = !UseFullNames;
					stringBuilder3.Append(Actor.One(maxValue5, text17, text18, flag28, flag29, flag30, flag31, flag32, !UseFullNames, flag3, false, flag17, text12, true, true, false, null, false, null, false));
				}
			}
			else if (stringBuilder2.Length > 0)
			{
				if (!text12.IsNullOrEmpty())
				{
					stringBuilder2.Append(text12).Append(' ');
				}
				stringBuilder2.Append(SubjectOverride);
			}
			else if (!text12.IsNullOrEmpty())
			{
				stringBuilder2.Append(text12.Capitalize()).Append(' ').Append(SubjectOverride);
			}
			else
			{
				stringBuilder2.Append(SubjectOverride.Capitalize());
			}
			if (!text11.IsNullOrEmpty() && !DescribeSubjectDirectionLate)
			{
				if (SubjectPossessedBy != null && (Actor != null || !Actor.HasProperName))
				{
					StringBuilder stringBuilder4 = stringBuilder2.Append(" of ");
					int maxValue6 = int.MaxValue;
					string text19 = null;
					string text20 = null;
					bool flag33 = false;
					bool flag34 = false;
					bool flag35 = false;
					bool flag36 = false;
					bool flag37 = false;
					bool flag3 = !UseFullNames;
					stringBuilder4.Append(SubjectPossessedBy.one(maxValue6, text19, text20, flag33, flag34, flag35, flag36, flag37, !UseFullNames, flag3, false, IndefiniteSubject, null, true, true, false, null, false, null, false)).Append(' ').Append(text11);
				}
				else
				{
					stringBuilder2.Append(' ').Append(text11);
				}
			}
			if (Actor != null)
			{
				stringBuilder2.Append(Actor.GetVerb(Verb, true, false, false, true, false));
			}
			else
			{
				stringBuilder2.Append(' ').Append(Verb);
			}
			Messaging.AppendWithSpaceIfNeeded(stringBuilder2, DirectPreposition);
			stringBuilder2.Append(' ');
			if (Actor != null && DirectObject == Actor && SubjectOverride == null)
			{
				stringBuilder2.Append(PossessiveDirectObject ? Actor.its : Actor.itself);
			}
			else if (DirectObject.IsPlayer())
			{
				stringBuilder2.Append(PossessiveDirectObject ? "yours" : "you");
			}
			else
			{
				string text21 = null;
				bool flag38 = IndefiniteDirectObject || IndefiniteDirectObjectForOthers;
				if (DirectObjectPossessedBy != null && !DirectObject.HasProperName)
				{
					text21 = DirectObjectPossessedBy.its;
					flag38 = false;
				}
				GameObject gameObject6 = DirectObject;
				int maxValue7 = int.MaxValue;
				string text22 = null;
				string text23 = null;
				bool flag39 = false;
				bool flag40 = false;
				bool flag41 = false;
				bool flag42 = false;
				bool flag43 = false;
				bool flag3 = !UseFullNames;
				string text24 = gameObject6.one(maxValue7, text22, text23, flag39, flag40, flag41, flag42, flag43, !UseFullNames, flag3, false, flag38, text21, true, true, false, null, false, null, false);
				if (PossessiveDirectObject)
				{
					text24 = Grammar.MakePossessive(text24);
				}
				stringBuilder2.Append(text24);
			}
			Messaging.AppendWithSpaceIfNeeded(stringBuilder2, IndirectPreposition);
			stringBuilder2.Append(' ');
			if (IndirectObject == DirectObject)
			{
				stringBuilder2.Append(PossessiveIndirectObject ? IndirectObject.itself : IndirectObject.them);
			}
			else if (Actor != null && IndirectObject == Actor && SubjectOverride == null)
			{
				stringBuilder2.Append(PossessiveIndirectObject ? Actor.its : Actor.itself);
			}
			else if (IndirectObject.IsPlayer())
			{
				stringBuilder2.Append(PossessiveIndirectObject ? "yours" : "you");
			}
			else
			{
				string text25 = null;
				bool flag44 = IndefiniteIndirectObject || IndefiniteIndirectObjectForOthers;
				if (IndirectObjectPossessedBy != null)
				{
					text25 = IndirectObjectPossessedBy.its;
					flag44 = false;
				}
				GameObject gameObject7 = IndirectObject;
				int maxValue8 = int.MaxValue;
				string text26 = null;
				string text27 = null;
				bool flag45 = false;
				bool flag46 = false;
				bool flag47 = false;
				bool flag48 = false;
				bool flag49 = false;
				bool flag3 = !UseFullNames;
				string text28 = gameObject7.one(maxValue8, text26, text27, flag45, flag46, flag47, flag48, flag49, !UseFullNames, flag3, false, flag44, text25, true, true, false, null, false, null, false);
				if (PossessiveIndirectObject)
				{
					text28 = Grammar.MakePossessive(text28);
				}
				stringBuilder2.Append(text28);
			}
			Messaging.AppendWithSpaceIfNeeded(stringBuilder2, Extra);
			if (!text11.IsNullOrEmpty() && DescribeSubjectDirectionLate)
			{
				stringBuilder2.Append(' ').Append(text11);
			}
			stringBuilder2.Append(EndMark ?? ".");
			if (!text10.IsNullOrEmpty())
			{
				stringBuilder2.Append("}}");
			}
			Messaging.HandleMessage(Source ?? Actor ?? The.Player, stringBuilder2, ' ', FromDialog, UsePopup, null, null);
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x00234F38 File Offset: 0x00233138
		private static void AppendWithSpaceIfNeeded(StringBuilder SB, string Text)
		{
			if (!Text.IsNullOrEmpty())
			{
				if (!Text.StartsWith(",") && !Text.StartsWith(";") && !Text.StartsWith("."))
				{
					SB.Append(' ');
				}
				SB.Append(Text);
			}
		}
	}
}
