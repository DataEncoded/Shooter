using System;
using System.Globalization;
using Godot;


public partial class EnemySpawner : Node

{
    public bool Spawn { get; set; }

    private Timer spawnTimer = new Timer();

    private PackedScene enemy = ResourceLoader.Load<PackedScene>("res://Scenes/EnemyA.tscn");

    public override void _Ready()
    {
        AddChild(spawnTimer);
        spawnTimer.OneShot = true;

    }

    public override void _Process(double delta)
    {
        if (spawnTimer.IsStopped())
        {
            spawnTimer.Start(3);

            // Spawn enemies
            // How many?
            int maxEnemies = (Score.Instance.score/10) + 1;

            long numEnemies = Math.Max(GD.Randi() % (maxEnemies + 1), 1);

            for (int i = 0; i < numEnemies; i++)
            {
                // Spawn enemy!
                Area2D enemyNode = enemy.Instantiate<Area2D>();

                long xSpawn = 0;
                long ySpawn = 0;

                uint leftRightUpDown = GD.Randi() % 4;
                switch (leftRightUpDown)
                {
                    case 0:
                        xSpawn = -36;
                        ySpawn = GD.Randi() % GetWindow().Size.Y;
                        break;
                    case 1:
                        xSpawn = GetWindow().Size.X + 36;
                        ySpawn = GD.Randi() % GetWindow().Size.Y;
                        break;
                    case 2:
                        ySpawn = -36;
                        xSpawn = GD.Randi() % GetWindow().Size.X;
                        break;
                    case 3:
                        ySpawn = GetWindow().Size.Y + 36;
                        xSpawn = GD.Randi() % GetWindow().Size.X;
                        break;
                }

                enemyNode.Position = new Vector2(xSpawn, ySpawn);
                
                GetWindow().AddChild(enemyNode);
            }
        }
    }

}