using Photon.Pun;
using Photon.Realtime;

namespace ONM.Mods
{
	internal class Guns
	{
		public static void Kick_Gun(Player target) //doesnt work btw
		{
			for (int i = 0; i < 25; i++)
			{
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
				PhotonNetwork.CloseConnection(target);
			}
		}
	}
}
