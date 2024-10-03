using System.Threading.Tasks;
using Godot;
using Godot.Collections;

public partial class ResetManager: Node
{

    public override async void _Input(InputEvent userInput)
    {
        if (userInput.IsActionPressed("Reset"))
        {

            var t = GetTree().ReloadCurrentScene();

            while (GetTree().CurrentScene == null)
            {
                await Task.Delay(10);
            }

            reset();            
        }
    }

    private void reset()
    {
        Array<Node> enemies = GetTree().GetNodesInGroup("Enemy");

        foreach (Node enemy in enemies)
        {
            enemy.QueueFree();
        }

        Score.Instance.resetScore();

        EnemySpawner.Instance.restart();
    }
}