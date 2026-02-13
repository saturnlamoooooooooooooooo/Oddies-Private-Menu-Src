using Photon.Pun;
using Photon.Realtime;

namespace ONM.XR
{
	internal class PhotonXR
	{
		public static VRRig FindVRRigForPlayer(Player player)
		{
			foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
			{
				if (!vrrig.isOfflineVRRig && vrrig.GetComponent<PhotonView>().Owner == player)
				{
					return vrrig;
				}
			}
			return null;
		}
	}
}
