using Godot;


public abstract partial class Projectile : Area2D
{
    [Export]
    public virtual float Speed {get; set;}

    static public Projectile Create(PackedScene providedProjectile, float initialSpeed)
    {
        Projectile myProjectile = providedProjectile.Instantiate<Projectile>();

        myProjectile.Speed = initialSpeed;
        
        return myProjectile;
    }
}