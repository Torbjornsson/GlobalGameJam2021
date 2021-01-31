using Godot;

public class HUD : Control
{
    private PackedScene packedScene;
    private int points;
    private Label label;

    public override void _Ready()
    {
        packedScene = (PackedScene) GD.Load("res://Scenes/Points.tscn");
        label = (Label)GetChild(0).GetChild(1);
    }

    private void AddPoint()
    {
        points++;
        var sock = (TextureRect) packedScene.Instance();
        GetChild(0).GetChild(0).AddChild(sock);
        if (points >= 3)
        {
            Wins();
            GD.Print("Win");
        }
    }

    private void Wins()
    {
        label.Text = "Congratulations you have found all the socks";
        
    }
}