using Godot;
using System;
public class Main : Node2D
{
    [Export]
    private float _gravitiy = 1;
    public Vector2 _start_position = new Vector2(82,0);
    public Vector2 _platform_up_left = new Vector2(300, 400);
    public override void _Ready()
    {
        var player = GetNode<KinematicBody2D>("Player");
        var ground = GetNode<StaticBody2D>("Ground");
        var platform = GetNode<StaticBody2D>("Platform");

        player.Position = _start_position;
        ground.Position = new Vector2(0, 400);
        platform.Position = _platform_up_left;
    }
}
