using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A9E RID: 2718
	public class HindrenClues : ZoneBuilderSandbox
	{
		// Token: 0x06006F0A RID: 28426 RVA: 0x00266B04 File Offset: 0x00264D04
		public bool BuildZone(Zone Z)
		{
			foreach (string text in KithAndKinGameState.Instance.clueItems)
			{
				ZoneBuilderSandbox.PlaceObject(GameObjectFactory.Factory.CreateObject(text), Z, null);
			}
			using (List<HindrenClueLook>.Enumerator enumerator2 = KithAndKinGameState.Instance.lookClues.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					HindrenClueLook lookClue = enumerator2.Current;
					Predicate<GameObject> predicate;
					Predicate<GameObject> <>9__0;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = (GameObject go) => go.Blueprint == lookClue.target);
					}
					foreach (GameObject gameObject in Z.GetObjects(predicate))
					{
						lookClue.apply(gameObject);
					}
				}
			}
			return true;
		}
	}
}
