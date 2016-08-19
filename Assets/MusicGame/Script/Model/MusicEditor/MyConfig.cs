using System;

public class MyConfig
{
	//MusicEditor用のコンフィグ
	public static class Editor
	{
		public static int NOTE_SPEED = 600;
		//MusicTimeの有効桁数。
		public static int EFFECTIVE_DIGIT = 2;
	}

	//MusicPlay用のコンフィグ
	public static class Play
	{
		public static int NOTE_SPEED = 400;
		public static int EFFECTIVE_DIGIT = 1;
	}
}