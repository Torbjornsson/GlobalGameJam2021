using Godot;

public class Player : KinematicBody2D
{
    public enum State
    {
        Idle,
        Walking,
        Running,
        Jumping,
        Climbing
    }

    public enum Facing
    {
        Left,
        Right,
    }

    [Export] public int gravity = 2000;
    [Export] public int jumpSpeed = -800;
    [Export] public int speed = 200;
    public State state = State.Jumping;
    private Vector2 velocity = Vector2.Zero;
    private Sprite sprite;
    private Facing facing = Facing.Right;

    public override void _Ready()
    {
        sprite = GetNode<Sprite>("./Sprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        HandleInput();
        Gravity(delta);
        velocity = MoveAndSlide(velocity, Vector2.Up);
        SetState();
    }

    private void HandleInput()
    {
        switch (state)
        {
            case State.Walking:
                Jumping();
                Walking();
                Running();
                break;
            case State.Climbing:
                Jumping();
                break;
            case State.Idle:
                Jumping();
                Walking();
                break;
            case State.Jumping:
                Walking();
                Running();
                break;
        }
    }

    private void Walking()
    {
        velocity.x = 0;

        if (Input.IsActionPressed("left"))
        {
            velocity.x -= speed;
            FlipDirection(Facing.Left);
        }

        if (Input.IsActionPressed("right"))
        {
            velocity.x += speed;
            FlipDirection(Facing.Right);
        }
    }

    private void FlipDirection(Facing newDirection)
    {
        if (newDirection != facing)
        {
            facing = newDirection;
            sprite.FlipH = facing == Facing.Left;
        }
    }

    private void Running()
    {
        if (Input.IsActionPressed("sprint")) velocity.x *= 2;
    }

    private void Jumping()
    {
        if (Input.IsActionPressed("jump"))
        {
            var scale = state == State.Climbing ? 0.75f : 1;
            velocity.y = jumpSpeed * scale;
            state = State.Jumping;
        }
    }

    private void Gravity(float delta)
    {
        if (state == State.Climbing) return;
        velocity.y += gravity * delta;
    }

    private void SetState()
    {
        if (IsOnWall())
        {
            state = State.Climbing;
            velocity.y = 0;
            return;
        }

        if (IsOnFloor() && velocity.x != 0)
        {
            state = State.Walking;
            return;
        }

        if (velocity == Vector2.Zero && state != State.Climbing && IsOnFloor()) state = State.Idle;
    }
}