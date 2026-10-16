using System;

namespace XRL.World.Conversations
{
	/// <summary>
	/// Indicates that a method is a conversation delegate.
	/// Depending on the return type it is either registered as a Predicate (bool), Action (void) or Generator (IConversationPart).
	/// </summary>
	// Token: 0x02000BDA RID: 3034
	[AttributeUsage(AttributeTargets.Method)]
	public class ConversationDelegate : Attribute
	{
		/// <summary>
		/// The key used to invoke this delegate, defaults to method name.
		/// </summary>
		// Token: 0x040037BC RID: 14268
		public string Key;

		/// <summary>
		/// If this delegate is a conversation predicate, create another delegate returning the negated result of the first.
		/// </summary>
		// Token: 0x040037BD RID: 14269
		public bool Inverse = true;

		/// <summary>
		/// The key used to call the inverse delegate, defaults to IfNotXYZ should the original key follow the IfXYZ PascalCase pattern.
		/// E.g. IfHaveQuest -&gt; IfNotHaveQuest.
		/// </summary>
		// Token: 0x040037BE RID: 14270
		public string InverseKey;

		/// <summary>
		/// If this delegate is a conversation predicate or action, create another delegate with the speaker as the target instead of the player.
		/// </summary>
		// Token: 0x040037BF RID: 14271
		public bool Speaker;

		/// <summary>
		/// The key used to call the speaker delegate, defaults to IfSpeakerXYZ should the original key follow the IfXYZ PascalCase pattern.
		/// E.g. IfHavePart -&gt; IfSpeakerHavePart, SetIntProperty -&gt; SetSpeakerIntProperty.
		/// </summary>
		// Token: 0x040037C0 RID: 14272
		public string SpeakerKey;

		/// <summary>
		/// The key used to call the inverse speaker delegate, defaults to IfSpeakerNotXYZ should the original key follow the IfXYZ PascalCase pattern.
		/// E.g. IfHavePart -&gt; IfSpeakerNotHavePart.
		/// </summary>
		// Token: 0x040037C1 RID: 14273
		public string SpeakerInverseKey;

		/// <todo>
		/// If this delegate is a conversation predicate, create another delegate which greys out and prevents navigation instead of visibility.
		/// </todo>
		// Token: 0x040037C2 RID: 14274
		public bool Require = true;

		/// <todo>
		/// The key used to call the requirement delegate, defaults to RequireXYZ should the original key follow the IfXYZ PascalCase pattern.
		/// E.g. IfGenotype -&gt; RequireGenotype, RequireNotGenotype, RequireSpeakerNotGenotype.
		/// </todo>
		// Token: 0x040037C3 RID: 14275
		public string RequireKey;
	}
}
