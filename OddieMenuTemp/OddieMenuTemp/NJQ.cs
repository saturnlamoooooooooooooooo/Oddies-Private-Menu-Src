using UnityEngine;

namespace OddieMenuTemp
{
	internal class NJQ
	{
		public static void FixedUpdate()
		{
			Object.Destroy(GameObject.Find("jumpscare"));
			Object.Destroy(GameObject.Find("jumpscare spidr"));
			Object.Destroy(GameObject.Find("jumpscare yeti"));
			Object.Destroy(GameObject.Find("jumpscare lvel0"));
			Object.Destroy(GameObject.Find("jumpscare lvelFUN"));
			Object.Destroy(GameObject.Find("jumpscare lvel0"));
		}
	}
}
