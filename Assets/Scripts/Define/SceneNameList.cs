namespace Sailing
{

	/// <summary>
	/// @brief シーン名をEnum型で管理するクラス
	/// <summary>
	public enum SceneNameEnum
	{

		Title,
		MainMenu,
		InGame,
		UserRanking,
		Lobby,
		MatchingRoom,

	}

	/// <summary>
	/// @brief シーン名をstring型で管理するクラス
	/// <summary>
	public static class SceneNameString
	{

		public const string Title = "Title";
		public const string MainMenu = "MainMenu";
		public const string InGame = "InGame";
		public const string UserRanking = "UserRanking";
		public const string Lobby = "Lobby";
		public const string MatchingRoom = "MatchingRoom";

	}

}
