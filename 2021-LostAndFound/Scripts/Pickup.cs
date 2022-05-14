using Godot;
using JetBrains.Annotations;

namespace LostAndFound.Scripts
{
    public class Pickup : Area2D
    {
        [Signal]
        public delegate void PickedUp();

        public override void _Ready()
        {
            Connect("PickedUp", GetNode("../../HUD"), "AddPoint");
        }

        [UsedImplicitly]
        private void _on_Sock_body_entered(PhysicsBody2D body2D)
        {
            if (body2D.Name == "Player")
            {
                EmitSignal("PickedUp");
                QueueFree();
            }
        }
    }
}