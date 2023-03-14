using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MosquitoAttack;
using System.Collections.Generic;

public class EnemyBullet : GameObject
{
    public float speed;
    public ProjectileState currentProjectileState = ProjectileState.NotFlying;
    public EnemyBullet(Sprite sprite, Transform transform) : base(sprite, transform)
    {
        this.sprite = sprite;
        this.transform = transform;
        speed = 1f;
    }
    public EnemyBullet(Sprite sprite, Transform transform, float speed) : base(sprite, transform)
    {
        this.sprite = sprite;
        this.transform = transform;
        this.speed = speed;
    }
    public void Update(GameTime gameTime)
    {
        Move(new(0, speed));
    }
    public void Deactivate()
    {
        if(currentProjectileState == ProjectileState.Flying && sprite.Bounds.Top > 600)
        {
            //DISABLE
            currentProjectileState = ProjectileState.NotFlying;
        }
    }
}