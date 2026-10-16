using System;
using System.Collections.Generic;

namespace UnityEngine.Timeline
{
	// Token: 0x02000052 RID: 82
	public interface IPropertyCollector
	{
		// Token: 0x060002F7 RID: 759
		void PushActiveGameObject(GameObject gameObject);

		// Token: 0x060002F8 RID: 760
		void PopActiveGameObject();

		// Token: 0x060002F9 RID: 761
		void AddFromClip(AnimationClip clip);

		// Token: 0x060002FA RID: 762
		void AddFromClips(IEnumerable<AnimationClip> clips);

		// Token: 0x060002FB RID: 763
		void AddFromName<T>(string name) where T : Component;

		// Token: 0x060002FC RID: 764
		void AddFromName(string name);

		// Token: 0x060002FD RID: 765
		void AddFromClip(GameObject obj, AnimationClip clip);

		// Token: 0x060002FE RID: 766
		void AddFromClips(GameObject obj, IEnumerable<AnimationClip> clips);

		// Token: 0x060002FF RID: 767
		void AddFromName<T>(GameObject obj, string name) where T : Component;

		// Token: 0x06000300 RID: 768
		void AddFromName(GameObject obj, string name);

		// Token: 0x06000301 RID: 769
		void AddFromName(Component component, string name);

		// Token: 0x06000302 RID: 770
		void AddFromComponent(GameObject obj, Component component);

		// Token: 0x06000303 RID: 771
		void AddObjectProperties(Object obj, AnimationClip clip);
	}
}
