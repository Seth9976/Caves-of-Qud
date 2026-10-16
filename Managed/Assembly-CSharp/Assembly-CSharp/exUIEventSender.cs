using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class exUIEventSender : MonoBehaviour
{
	// Token: 0x06000824 RID: 2084 RVA: 0x000345B4 File Offset: 0x000327B4
	private void Awake()
	{
		exUIControl component = base.GetComponent<exUIControl>();
		if (component != null)
		{
			Type type = component.GetType();
			using (List<exUIEventSender.Emitter>.Enumerator enumerator = this.emitterList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					exUIEventSender.Emitter emitter = enumerator.Current;
					EventInfo @event = type.GetEvent(emitter.eventName);
					if (@event != null)
					{
						using (List<exUIEventSender.SlotInfo>.Enumerator enumerator2 = emitter.slots.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								exUIEventSender.SlotInfo slotInfo = enumerator2.Current;
								bool flag = false;
								foreach (MonoBehaviour monoBehaviour in slotInfo.receiver.GetComponents<MonoBehaviour>())
								{
									MethodInfo method = monoBehaviour.GetType().GetMethod(slotInfo.method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(GameObject) }, null);
									if (method != null)
									{
										Delegate @delegate = Delegate.CreateDelegate(typeof(Action<GameObject>), monoBehaviour, method);
										@event.AddEventHandler(component, @delegate);
										flag = true;
									}
								}
								if (!flag)
								{
									Debug.LogWarning("Can not find method " + slotInfo.method + " in " + slotInfo.receiver.name);
								}
							}
							continue;
						}
					}
					Debug.LogWarning("Can not find event " + emitter.eventName + " in " + base.gameObject.name);
				}
				return;
			}
		}
		Debug.LogWarning("Can not find exUIControl in this GameObject");
	}

	// Token: 0x0400059B RID: 1435
	public List<exUIEventSender.Emitter> emitterList = new List<exUIEventSender.Emitter>();

	// Token: 0x020015D0 RID: 5584
	[Serializable]
	public class SlotInfo
	{
		// Token: 0x040057DA RID: 22490
		public GameObject receiver;

		// Token: 0x040057DB RID: 22491
		public string method = "";
	}

	// Token: 0x020015D1 RID: 5585
	[Serializable]
	public class Emitter
	{
		// Token: 0x040057DC RID: 22492
		public string eventName;

		// Token: 0x040057DD RID: 22493
		public List<exUIEventSender.SlotInfo> slots;
	}
}
