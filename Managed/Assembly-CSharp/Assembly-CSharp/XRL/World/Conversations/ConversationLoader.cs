using System;
using System.Collections.Generic;
using System.Xml;
using XRL.UI;

namespace XRL.World.Conversations
{
	// Token: 0x02000BE6 RID: 3046
	[HasModSensitiveStaticCache]
	[Serializable]
	public class ConversationLoader
	{
		// Token: 0x06007A6A RID: 31338 RVA: 0x002DF904 File Offset: 0x002DDB04
		[PreGameCacheInit]
		public static void CheckInit()
		{
			if (Conversation._Blueprints == null)
			{
				Loading.LoadTask("Loading Conversations.xml", new Action(ConversationLoader.LoadConversations), true);
			}
		}

		// Token: 0x06007A6B RID: 31339 RVA: 0x002DF924 File Offset: 0x002DDB24
		private static void ReadConversation(string Path, BuildContext Context)
		{
			string text = null;
			using (XmlTextReader streamingAssetsXMLStream = DataManager.GetStreamingAssetsXMLStream(Path))
			{
				streamingAssetsXMLStream.WhitespaceHandling = WhitespaceHandling.None;
				while (streamingAssetsXMLStream.Read())
				{
					if (streamingAssetsXMLStream.Name == "conversations")
					{
						text = (Context.Namespace = streamingAssetsXMLStream.GetAttribute("Namespace"));
						if (MarkovCorpusGenerator.Generating && streamingAssetsXMLStream.GetAttribute("ExcludeFromCorpusGeneration").EqualsNoCase("true"))
						{
							break;
						}
					}
					else if (streamingAssetsXMLStream.Name == "conversation")
					{
						string text2 = streamingAssetsXMLStream.GetAttribute("Namespace");
						if (!text2.IsNullOrEmpty() && !text.IsNullOrEmpty())
						{
							text2 = text + "." + text2;
						}
						Context.Namespace = text2 ?? text;
						ConversationXMLBlueprint conversationXMLBlueprint = new ConversationXMLBlueprint
						{
							Inherits = "BaseConversation"
						};
						conversationXMLBlueprint.Read(streamingAssetsXMLStream, Context);
						ConversationXMLBlueprint conversationXMLBlueprint2;
						if (Conversation._Blueprints.TryGetValue(conversationXMLBlueprint.ID, out conversationXMLBlueprint2) && conversationXMLBlueprint.Load == 0)
						{
							conversationXMLBlueprint2.Merge(conversationXMLBlueprint);
						}
						else
						{
							Conversation._Blueprints[conversationXMLBlueprint.ID] = conversationXMLBlueprint;
						}
					}
				}
				streamingAssetsXMLStream.Close();
			}
		}

		// Token: 0x06007A6C RID: 31340 RVA: 0x002DFA68 File Offset: 0x002DDC68
		private static void LoadConversations()
		{
			Dictionary<string, ConversationXMLBlueprint> dictionary = (Conversation._Blueprints = new Dictionary<string, ConversationXMLBlueprint>());
			BuildContext buildContext = new BuildContext();
			foreach (DataFile dataFile in DataManager.GetXMLFilesWithRoot("Conversations"))
			{
				buildContext.File = dataFile;
				try
				{
					ConversationLoader.ReadConversation(dataFile, buildContext);
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(dataFile.Mod, ex);
				}
			}
			buildContext.Next.AddRange(dictionary.Values);
			int num = 20;
			while (num > 0 && buildContext.Next.Count > 0)
			{
				int count = buildContext.Next.Count;
				buildContext.Advance();
				foreach (ConversationXMLBlueprint conversationXMLBlueprint in buildContext.Current)
				{
					if (!conversationXMLBlueprint.Bake(buildContext))
					{
						buildContext.Next.Add(conversationXMLBlueprint);
					}
				}
				if (count == buildContext.Next.Count)
				{
					num--;
				}
			}
			foreach (string text in buildContext.Errors)
			{
				MetricsManager.LogError(text);
			}
			buildContext.Clear();
			foreach (ConversationXMLBlueprint conversationXMLBlueprint2 in dictionary.Values)
			{
				conversationXMLBlueprint2.DistributeChildren(buildContext);
			}
		}

		// Token: 0x040037D4 RID: 14292
		private Action<object> LogHandler = new Action<object>(MetricsManager.LogError);
	}
}
