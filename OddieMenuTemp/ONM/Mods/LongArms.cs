using UnityEngine;

namespace ONM.Mods
{
	internal class LongArms
	{
		public static void Enable()
		{
			GorillaTagger.Instance.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
		}
		
		public static void Disable()
		{
			GorillaTagger.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}
}
