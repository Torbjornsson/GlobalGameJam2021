using Godot;
using JetBrains.Annotations;

namespace LostAndFound.Scripts
{
	public class Hud : Control
	{
		private PackedScene _packedScene;
		private int _points;
		private Label _label;

		public override void _Ready()
		{
			_packedScene = (PackedScene) GD.Load("res://Scenes/Points.tscn");
			_label = (Label)GetChild(0).GetChild(1);
		}

		[UsedImplicitly]
		private void AddPoint()
		{
			_points++;
			var sock = (TextureRect) _packedScene.Instance();
			GetChild(0).GetChild(0).AddChild(sock);
			if (_points >= 3)
			{
				Wins();
				GD.Print("Win");
			}
		}

		private void Wins()
		{
			_label.Text = "Congratulations you have found all the socks";
		
		}
	}
}
