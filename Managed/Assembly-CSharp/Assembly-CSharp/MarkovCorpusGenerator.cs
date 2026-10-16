using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using HistoryKit;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using XRL;
using XRL.Core;
using XRL.Messages;
using XRL.Names;
using XRL.UI;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Skills;

// Token: 0x02000090 RID: 144
public class MarkovCorpusGenerator : MonoBehaviour
{
	// Token: 0x060004FF RID: 1279 RVA: 0x0002119A File Offset: 0x0001F39A
	private void Start()
	{
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0002119C File Offset: 0x0001F39C
	private IEnumerator Generate_()
	{
		yield return new WaitForEndOfFrame();
		this.downloadURLs.Clear();
		this.textFiles.Clear();
		Regex replacer = new Regex("=[^\\s]*?[=:]");
		Regex spice = new Regex("<[^\\s]*?>");
		Popup.Suppress = true;
		MessageQueue.Suppress = true;
		MinEvent.SuppressThreadWarning = true;
		MarkovCorpusGenerator.Generating = true;
		if (!string.IsNullOrEmpty(this.rootObject.transform.Find("Include/URLList").GetComponent<InputField>().text))
		{
			foreach (string text in this.rootObject.transform.Find("Include/URLList").GetComponent<InputField>().text.Split('\n', StringSplitOptions.None))
			{
				Debug.Log("Fetching: " + text);
				this.downloadURLs.Add(text);
			}
		}
		if (!string.IsNullOrEmpty(this.rootObject.transform.Find("Include/TextList").GetComponent<InputField>().text))
		{
			foreach (string text2 in this.rootObject.transform.Find("Include/TextList").GetComponent<InputField>().text.Split('\n', StringSplitOptions.None))
			{
				Debug.Log("File: " + text2);
				this.textFiles.Add(text2.Replace("\r", ""));
			}
		}
		StringBuilder CorpusBuilder = new StringBuilder();
		StringBuilder ObjectCorpusBuilder = new StringBuilder();
		HashSet<string> CorpusAdded = new HashSet<string>();
		if (this.GetCheck("URLToggle"))
		{
			int i;
			for (int x2 = 0; x2 < this.downloadURLs.Count; x2 = i + 1)
			{
				this.ProgressUpdate("Downloading: " + this.downloadURLs[x2]);
				yield return new WaitForEndOfFrame();
				using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(this.downloadURLs[x2]))
				{
					unityWebRequest.SendWebRequest();
					if (!unityWebRequest.isNetworkError && !unityWebRequest.isHttpError)
					{
						CorpusBuilder.Append(unityWebRequest.downloadHandler.text);
					}
					Debug.Log(unityWebRequest.downloadHandler.text);
				}
				CorpusBuilder.Append("\n");
				i = x2;
			}
		}
		this.ProgressUpdate("Hotloading game configuration...");
		yield return new WaitForEndOfFrame();
		XRLCore.Core.HotloadConfiguration(true);
		GameObjectFactory factory = GameObjectFactory.Factory;
		if (this.GetCheck("BooksToggle"))
		{
			foreach (string bookid in BookUI.BookCorpus.Keys)
			{
				this.ProgressUpdate("Processing book: " + bookid);
				yield return new WaitForEndOfFrame();
				CorpusBuilder.Append(ConsoleLib.Console.ColorUtility.StripFormatting(BookUI.BookCorpus[bookid]));
				CorpusBuilder.Append("\n");
				bookid = null;
			}
			Dictionary<string, string>.KeyCollection.Enumerator enumerator = default(Dictionary<string, string>.KeyCollection.Enumerator);
		}
		if (this.GetCheck("ConversationsToggle"))
		{
			using (Dictionary<string, ConversationXMLBlueprint>.Enumerator enumerator2 = Conversation.Blueprints.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					MarkovCorpusGenerator.<>c__DisplayClass5_0 CS$<>8__locals1 = new MarkovCorpusGenerator.<>c__DisplayClass5_0();
					CS$<>8__locals1.pair = enumerator2.Current;
					if (!CS$<>8__locals1.pair.Key.StartsWith("Base"))
					{
						this.ProgressUpdate("Processing conversation: " + CS$<>8__locals1.pair.Key);
						yield return new WaitForEndOfFrame();
						try
						{
							GameObjectBlueprint gameObjectBlueprint = factory.BlueprintList.Find((GameObjectBlueprint x) => !x.IsBaseBlueprint() && x.GetPartParameter<string>("ConversationScript", "ConversationID", null) == CS$<>8__locals1.pair.Key);
							ConversationUI.Speaker = ((gameObjectBlueprint != null) ? factory.CreateSampleObject(gameObjectBlueprint, null) : factory.CreateSampleObject("Arconaut", null));
							ConversationUI.Listener = The.Player;
							if (!ConversationUI.Speaker.HasProperName)
							{
								ConversationUI.Speaker.DisplayName = NameMaker.MakeName(ConversationUI.Speaker, null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
							}
							Conversation conversation = (ConversationUI.CurrentConversation = new Conversation());
							conversation.Load(CS$<>8__locals1.pair.Value);
							conversation.Awake();
							conversation.Enter();
							conversation.Entered();
							conversation.Prepare();
							foreach (IConversationElement conversationElement in conversation.Elements)
							{
								try
								{
									conversationElement.Awake();
									conversationElement.Enter();
									conversationElement.Entered();
									List<ConversationText> texts = conversationElement.Texts;
									string text3 = ((texts != null) ? texts.FirstOrDefault<ConversationText>().Text : null);
									if (!text3.IsNullOrEmpty() && !text3.Contains("MARKOV"))
									{
										if (!text3.Contains("GGRESULT"))
										{
											if (CorpusAdded.Add(text3))
											{
												conversationElement.Prepare();
												string text4 = Regex.Replace(conversationElement.Text, "\\{\\{emote\\|[^\\0]*?\\}\\}", "");
												text4 = ConsoleLib.Console.ColorUtility.StripFormatting(text4).Trim();
												if (replacer.IsMatch(text4))
												{
													MetricsManager.LogWarning(string.Concat(new string[] { "MarkovCorpusGenerator::ConversationElement::", conversation.ID, "::", conversationElement.ID, ": Parsed text has leftover variables, skipping." }));
												}
												else if (!string.IsNullOrWhiteSpace(text4))
												{
													CorpusBuilder.Append(text4).Append("\n\n");
													conversationElement.Leave();
													conversationElement.Left();
												}
											}
										}
									}
								}
								catch (Exception ex)
								{
									MetricsManager.LogException("MarkovCorpusGenerator::ConversationElement::" + conversation.ID + "::" + conversationElement.ID, ex, "game_exception");
								}
							}
							ConversationUI.Speaker.Pool();
							ConversationUI.Speaker = null;
							ConversationUI.Listener = null;
							ConversationUI.CurrentConversation = null;
						}
						catch (Exception ex2)
						{
							MetricsManager.LogException("MarkovCorpusGenerator::Conversation", ex2, "game_exception");
						}
						CS$<>8__locals1 = null;
					}
				}
			}
			Dictionary<string, ConversationXMLBlueprint>.Enumerator enumerator2 = default(Dictionary<string, ConversationXMLBlueprint>.Enumerator);
		}
		if (this.GetCheck("ManualToggle"))
		{
			int i;
			for (int x2 = 0; x2 < XRLCore.Manual.Page.Count; x2 = i + 1)
			{
				this.ProgressUpdate("Processing manual page: " + x2.ToString());
				yield return new WaitForEndOfFrame();
				CorpusBuilder.Append(ConsoleLib.Console.ColorUtility.StripFormatting(XRLCore.Manual.Page[x2].GetData(false)));
				i = x2;
			}
		}
		if (this.GetCheck("QuestsToggle"))
		{
			foreach (string bookid in QuestLoader.Loader.QuestsByID.Keys)
			{
				this.ProgressUpdate("Processing quest: " + bookid);
				yield return new WaitForEndOfFrame();
				foreach (string text5 in QuestLoader.Loader.QuestsByID[bookid].StepsByID.Keys)
				{
					CorpusBuilder.Append(ConsoleLib.Console.ColorUtility.StripFormatting(QuestLoader.Loader.QuestsByID[bookid].StepsByID[text5].Text));
					CorpusBuilder.Append("\n");
				}
				bookid = null;
			}
			Dictionary<string, Quest>.KeyCollection.Enumerator enumerator4 = default(Dictionary<string, Quest>.KeyCollection.Enumerator);
		}
		if (this.GetCheck("SkillsToggle"))
		{
			foreach (string bookid in SkillFactory.Factory.SkillList.Keys)
			{
				this.ProgressUpdate("Processing skill: " + bookid);
				yield return new WaitForEndOfFrame();
				CorpusBuilder.Append(ConsoleLib.Console.ColorUtility.StripFormatting(SkillFactory.Factory.SkillList[bookid].Description));
				CorpusBuilder.Append("\n");
				bookid = null;
			}
			Dictionary<string, SkillEntry>.KeyCollection.Enumerator enumerator6 = default(Dictionary<string, SkillEntry>.KeyCollection.Enumerator);
		}
		if (this.GetCheck("TextToggle"))
		{
			for (int j = 0; j < this.textFiles.Count; j++)
			{
				this.ProgressUpdate("Reading: " + this.textFiles[j]);
				CorpusBuilder.Append(Regex.Replace(File.ReadAllText(this.textFiles[j]), "[^\\u0000-\\u007F]", string.Empty));
			}
		}
		if (this.GetCheck("ObjectShortDescriptionsToggle"))
		{
			int i;
			for (int x2 = 0; x2 < factory.BlueprintList.Count; x2 = i + 1)
			{
				if (x2 % 100 == 0)
				{
					this.ProgressUpdate("Processing gameobject: " + x2.ToString());
					yield return new WaitForEndOfFrame();
				}
				GameObjectBlueprint gameObjectBlueprint2 = factory.BlueprintList[x2];
				if ((!gameObjectBlueprint2.Name.Contains("Figurine") || !gameObjectBlueprint2.DescendsFrom("Random Figurine")) && (!gameObjectBlueprint2.Name.Contains("Statue") || !gameObjectBlueprint2.DescendsFrom("Random Statue")))
				{
					string partParameter = gameObjectBlueprint2.GetPartParameter<string>("Description", "Short", null);
					if (!partParameter.IsNullOrEmpty() && CorpusAdded.Add(partParameter))
					{
						try
						{
							global::XRL.World.GameObject gameObject = factory.CreateSampleObject(gameObjectBlueprint2, null);
							string text6 = GameText.VariableReplace(partParameter, gameObject, null, false);
							if (replacer.IsMatch(text6))
							{
								MetricsManager.LogWarning("MarkovCorpusGenerator::ShortDescription::" + gameObjectBlueprint2.Name + ": Parsed text has leftover variables, skipping.");
							}
							else
							{
								ObjectCorpusBuilder.Append(ConsoleLib.Console.ColorUtility.StripFormatting(text6));
								ObjectCorpusBuilder.Append("\n");
								ObjectCorpusBuilder.Append("\n");
								gameObject.Pool();
							}
						}
						catch (Exception ex3)
						{
							MetricsManager.LogException("MarkovCorpusGenerator::ShortDescription::" + gameObjectBlueprint2.Name, ex3, "game_exception");
						}
					}
				}
				i = x2;
			}
		}
		if (this.GetCheck("SultanGospelsToggle"))
		{
			History sultanHistory = The.Game.sultanHistory;
			foreach (HistoricEntity historicEntity in sultanHistory.entities)
			{
				foreach (HistoricEvent story in historicEntity.events)
				{
					string text7;
					if (story.entityProperties != null && story.entityProperties.TryGetValue("type", out text7) && text7 == "sultan")
					{
						string text8;
						story.entityProperties.TryGetValue("name", out text8);
						this.ProgressUpdate("Processing sultan: " + text8);
						yield return new WaitForEndOfFrame();
					}
					try
					{
						if (story.eventProperties == null)
						{
							continue;
						}
						string text9;
						if (!story.eventProperties.TryGetValue("gospel", out text9))
						{
							continue;
						}
						if (spice.IsMatch(text9))
						{
							throw new FormatException("Parsed text has leftover variables.");
						}
						ObjectCorpusBuilder.Append(ConsoleLib.Console.ColorUtility.StripFormatting(text9));
						ObjectCorpusBuilder.Append("\n");
						ObjectCorpusBuilder.Append("\n");
					}
					catch (Exception ex4)
					{
						MetricsManager.LogException("MarkovCorpusGenerator::SultanGospel", ex4, "game_exception");
					}
					story = null;
				}
				List<HistoricEvent>.Enumerator enumerator8 = default(List<HistoricEvent>.Enumerator);
			}
			List<HistoricEntity>.Enumerator enumerator7 = default(List<HistoricEntity>.Enumerator);
		}
		MarkovCorpusGenerator.StripTextInBrackets(CorpusBuilder);
		MarkovCorpusGenerator.StripSentenceQuotes(CorpusBuilder);
		MarkovCorpusGenerator.StripTextInBrackets(ObjectCorpusBuilder);
		MarkovCorpusGenerator.StripSentenceQuotes(ObjectCorpusBuilder);
		this.ProgressUpdate("Generating corpus...");
		yield return new WaitForEndOfFrame();
		if (this.GetCheck("DebugText"))
		{
			Debug.LogWarning("Using debug text instead");
			CorpusBuilder.Length = 0;
			CorpusBuilder.Append("I come from the west. My parents sold me into slavery as a babe, though the practice had been formally abolished in Perth years earlier. As a boy I cut my teeth on the reefs of the Shore of Songs, and lulled the Pale Sea to sleep with many a hymn, that she might return our sailors from the Black Stair unscathed. How the melodies whisked inside my skull even then. I was given no name at birth, but the sailors called me Catu. At five and ten years I ran away. On Ettinspine I roosted with an eremite who ushered me into manhood. I learned to hunt, and laugh, and dream. In time I came to know of the Free City and the ingress afforded my kind. I shoved off from my stony nook, and trekked to their high gate, and settled there, though I preferred to sup with the anglers and other low folk. In a sun-warmed pool under the rampart the Free Seer first noticed me. The slant of his neck from where he watched on the parapet presaged my movements, I still recall. I stood from my bath and shamefully padded to my discarded vest, but stopped. I saw him from that distance, his eyes tucked deep into his shorn dome, his frock as inert as a statue's, and I felt no more shame. Under his tutelage I rose to prominence in his court. I stood by his side as he treated with the tuyuldars of Odrum, as he plotted against the southern freeholds. When a traitorous canter slipped poison into his cup, I smashed it from his hands and sundered the quisling's mind. That night, in the hour of the Beetle Moon, he introduced me to the mirth of an assembled mind. Together with the other canters we Joined, and he named me Mirthful, and raised me unto his honor guard. I would be dispossessed of it in under a year. But I must pause now. The Cant loudens, and the Elder nears. I vow to conclude my story if I still draw breath, and if I am free. It is dusk. I sit on the bank of the River Svy, but no fire warms my bones. The risk of drawing moon-doting beasts is too great, and any aggregation of minds is sure to attract the attention of the Elder. Instead, I write by a flame kindled on my thumb. I left off at being named Mirthful in the Hall of the Free Seer. No honor is higher for a canter of Oth. For months I trained with my masked compatriots beneath the Hall, where our clay cisterns stored the freshwater that would later haunt my dreams. In the spring a letter arrived from the Angler King. He sought a treaty with the Free Seer and invited us to a neutral council on the doorstep of Ibunudr, arbitrated by the tuyuldar of Doria. After much preparation we set out, rowing our sketties beyond the Bowl of Iris and to the frosty shores of that far tundra. Over the course of the journey the Free Seer's disposition soured. He withdrew from the counsel of the Mirthful, even my own, and spent the better part of the wintry trek alone in his tent. At first I attributed his mood to the fragility of our diplomatic high ground, but I soon learned the true tack. We arrived at the camp on Ibunudr on the summer solstice. The twin banners of Athenreach and Doria thrashed in the wind, and the hymns of the Eustace-Sutta carried. I remember smiling at the throaty crooning of their priests. As we studied the campground from a hillock, as the Free Seer ambled up to my side, I saw the jewel of the Hanging Hills, Nisramet, the Angler King's wife, she who cemented the peace between her father's fiefdoms and the Angler King's freehold. I saw her, and I immediately knew my own treachery. The Free Seer's gaze was acknowledgement enough. I've arrived at Kyakukya. Kind Nuntu shares his hut with me, offers me smoked mushroom, and asks nothing in return. I write these words at his desk, and though he lusts for knowledge himself, he leaves me be to chronicle my past. The campaign back toward Oth was grim. I awaited my fate at the Free Seer's hands, though I anticipated being given the dignity of first returning to the Hall and my home. I was not afforded so much. One dismal night, under the Beetle Moon, I was accosted in my tent by my masked brothers. To my bewilderment, the Stithening had come already. How had the Free Seer acquired the cherrydotters? Had he bargained for them at Ibunudr, or had he possessed them all along? To this day I do not know. I remember little of the Stithening outside the beating of my own heart high in my ears. I passed out, and when I awoke next morning, the cavalcade had vanished. Only a moment did my recognition last before the chanting in my head took hold. I had heard it before, of course. All my life, in fact. It was the chanting that I weaved my melodies around on the shores of Perth as a small boy. Now, though, it was as if a single, distant canter had summoned a thousand of his kin, and they each summoned a thousand of their own. I wept, then I screamed, then I tore the hair from my head. The chant animated my bones more surely than my volition ever had. East and south, it urged. East and south. East and south. Toward Qud. My recollection of the voyage to the Great Salt Desert is fractured and fitful. I grew sicker by the day, but the chant grew louder, and so quickened the force that compelled me. Surely I would die once I reached the moistureless plains of Moghra'yi, presumably as had my predecessors before me. Finally, I did reach them, where a salt-churned rivulet dried under a golden arch. The arch seems a thing born of my dementia but that I recall it so vividly, and that I know it's the least of the secrets kept by Moghra'yi. I edge closer to the grove of fungi and its preposterous promise. Are the whispers true? Could it be all so simple? I can feel the moistness of the Eater's flesh on my lips, though the memory is surely another's. If salvation awaits me, then I promise to take it, whether or not I deserve it. Tonight, though, the jungle shrouds me. I sit under a chrome belfry, a leech kneading at my skin. I feel no compulsion to bar its work. The companionship is welcome. Now to awaken the past once more. Moghra'yi. O, how the dromads know! How did I survive the Great Desert? I cannot say for sure. I owe something to the songs I sang at Perth, to the falconry I learned on Ettinspine, to the honing of my sinew under Oth. The fractured dream that was my voyage offers little elucidation. But I do remember chewing salt, and summoning gusts of wind to suck the moisture out of the dead air. Of all the riven memories that churn in my head, Moghra'yi only accounts for a coherent three. The first is a vision of ruin at the fabled Kubrisyeti, the seat of the Wrathchild empire. Stretched across the alabaster plain were the last of their wicked structures, toppled effigies of the manscorpions themselves. I reposed there, against my will and only out of exhaustion. Fixated on the night sky in fear and fatigue, I heard the echoes of a great saltback's steps. Even those gargantuans know to avoid the place. The second memory is the presence of the giants. Colossal skeletons in the forms of humanoids litter the desert. I had heard the accounts before, and indeed some of the specimens extend as far north as Odrum. Their history is wholly unknown to us or anyone else, but their bones provide some of the only shade on the salt pans, and so they grew to be welcome sights. So it was with horror that I discerned a truth, one night, while tucked into the rib cage of one of three cadavers in a small proximity. You see, all the carcasses shared a commonality: their orientation. East and south. East and south. The third memory is the rust. Rusted hulls and rusted spires. It took me weeks to realize what I beheld. For all my boyhood was rife with sketties and ships, but none were of this size. I envisioned what the past must have looked like, as little sense as it made to me. Moghra'yi as a vast sea, and monstrous vessels mounting its waves. Markings and etches on their hulls had no resonance for me then. Later, though, I recognized the figure of the Spindle, and I glimpsed the importance it held for these sea-faring Eaters. When I lurched to the ground and was righted by a brown-skinned man with a knife and glowing cat at his feet, I knew it was a dream. It must be a dream. I owe my life to =SEEKERENEMY=. In my haste to reach the fungal grove, I misstepped. My wandering and buoyant mind blinded itself to the presence of a chosen one. She neared me, set on enveloping me back into the fold, but she was intercepted by =SEEKERENEMY=. For what purpose did they intercede I do not know, but they have my thanks. So must they have the Elder's ire. I come now to Qud and the Seekers of the Sightless Way. The chanting compounded in my skull into a final ecstasy, wherein I joined an aggregate mind the likes of which I hadn't fathomed. I was primed for it, though. From my first joining on Ettinspine all those years ago, to the Rite of Mirth chaperoned by the Free Seer, I knew that sight guides us no more than salt does. There is ecstasy in the aggregate, indeed, and the Elder is as powerful a center as I've known. But a center is it still, and where there is a center, there is a hierarchy. Which piece of my past endowed me with the wisdom to see through the ruse? Was it the boyhood memory of crashing waves on the Shore of Songs, echoed later in my vision at Moghra'yi? Was it the jealousy in the eyes of the Free Seer at Ibunudr? Or was it a mere wrinkle in the mass mind, owing nothing to my own struggle? I cannot say, but while the other canters piped their songs to weaken the Elder's prison, unknowing thralls to a greater mind, I escaped. It must not be forgotten that energy is not a simple factor, but is always a product of two factors--a mass with a velocity, a mass with a temperature, a quantity of electricity into a pressure, and so on. One may sometimes meet the statement that matter and energy are the two realities; both are spoken of as entities. It is much more philosophical to speak of matter and motion, for in the absence of motion there is no energy, and the energy varies with the amount of motion; and furthermore, to understand any manifestation of energy one must inquire what kind of motion is involved. This we do when we speak of mechanical energy as the energy involved in a body having a translatory motion; also, when we speak of heat as a vibratory, and of light as a wave motion. To speak of energy without stating or implying these distinctions, is to speak loosely and to keep far within the bounds of actual knowledge. To speak thus of a body possessing energy, or expending energy, is to imply that the body possesses some kind of motion, and produces pressure upon another body because it has motion. Tait and others have pointed out the fact, that what is called potential energy must, in its nature, be kinetic. Tait says--Now it is impossible to conceive of a truly dormant form of energy, whose magnitude should depend, in any way, upon the unit of time; and we are forced to conclude that potential energy, like kinetic energy, depends(even if unexplained or unimagined) upon motion. All this means that it is now too late tostop with energy as a final factor in any phenomenon, that the _form of  motion_ which embodies the energy is the factor that determines _what_ happens, as distinguished from how _much_ happens. Here, then, are to be found the distinctions which have heretofore been called forces; here is embodied the proof that direct pressure of one body upon another is what causes the latter to move, and that the direction of movement depends on the point of application, with reference to the centre of mass.");
		}
		while (CorpusBuilder.Contains("\n\n\n"))
		{
			CorpusBuilder = CorpusBuilder.Replace("\n\n\n", "\n\n");
		}
		while (ObjectCorpusBuilder.Contains("\n\n\n"))
		{
			ObjectCorpusBuilder = ObjectCorpusBuilder.Replace("\n\n\n", "\n\n");
		}
		File.WriteAllText("Assets/StreamingAssets/Base/ " + this.GetSelectedFilename() + ".raw.txt", CorpusBuilder.ToString());
		File.AppendAllText("Assets/StreamingAssets/Base/ " + this.GetSelectedFilename() + ".raw.txt", ObjectCorpusBuilder.ToString());
		MarkovChainData Data = MarkovChain.BuildChain(CorpusBuilder.Replace("\n", " ").Replace("\r", "").Replace("  ", " ")
			.Replace("  ", " ")
			.Replace("  ", " ")
			.Replace("  ", " ")
			.Replace("~J211", "")
			.ToString(), 2);
		Data = MarkovChain.AppendCorpus(Data, ObjectCorpusBuilder.Replace("\n", " ").Replace("\r", "").Replace("  ", " ")
			.Replace("  ", " ")
			.Replace("  ", " ")
			.Replace("  ", " ")
			.Replace("~J211", "")
			.ToString(), false);
		this.ProgressUpdate("Saving corpus...");
		yield return new WaitForEndOfFrame();
		Data.SaveToFile("Assets/StreamingAssets/Base/" + this.GetSelectedFilename());
		Popup.Suppress = false;
		MessageQueue.Suppress = false;
		MinEvent.SuppressThreadWarning = false;
		MarkovCorpusGenerator.Generating = false;
		this.CloseProgress();
		yield break;
		yield break;
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000211AC File Offset: 0x0001F3AC
	public static void StripTextInBrackets(StringBuilder input)
	{
		Match match = Regex.Match(input.ToString(), "\\[\\[[^\\0]*?\\]\\]");
		while (match != null && !string.IsNullOrEmpty(match.Value))
		{
			input.Replace(match.Groups[0].Value, "");
			match = match.NextMatch();
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00021200 File Offset: 0x0001F400
	public static void StripSentenceQuotes(StringBuilder Input)
	{
		int i = 0;
		int num = -1;
		while (i < Input.Length)
		{
			char c = Input[i];
			if (c == '"')
			{
				if (num == -1)
				{
					num = i;
				}
				else
				{
					num = -1;
				}
			}
			else if (char.IsWhiteSpace(c) && num != -1)
			{
				Input.Remove(num, 1);
				num = -1;
			}
			i++;
		}
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00021250 File Offset: 0x0001F450
	public string GetSelectedFilename()
	{
		return this.rootObject.transform.Find("Include/OutputFilename").GetComponent<InputField>().text;
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00021271 File Offset: 0x0001F471
	public bool GetCheck(string id)
	{
		return this.rootObject.transform.Find("Include/" + id).gameObject.GetComponent<Toggle>().isOn;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x000212A0 File Offset: 0x0001F4A0
	public void ProgressUpdate(string s)
	{
		this.rootObject.transform.Find("ProgressPanel").gameObject.SetActive(true);
		this.rootObject.transform.Find("ProgressPanel/ProgressLabel").gameObject.GetComponent<global::UnityEngine.UI.Text>().text = s;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x000212F2 File Offset: 0x0001F4F2
	public void CloseProgress()
	{
		this.rootObject.transform.Find("ProgressPanel").gameObject.SetActive(false);
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00021314 File Offset: 0x0001F514
	public void Generate()
	{
		base.StartCoroutine("Generate_");
	}

	// Token: 0x040003E0 RID: 992
	public List<string> downloadURLs = new List<string>();

	// Token: 0x040003E1 RID: 993
	public List<string> textFiles = new List<string>();

	// Token: 0x040003E2 RID: 994
	public global::UnityEngine.GameObject rootObject;

	// Token: 0x040003E3 RID: 995
	public static bool Generating;
}
