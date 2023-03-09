using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Transactions;

public class GameObject
{
    public Sprite sprite;
    public Transform transform;

    public GameObject(Sprite sprite, Transform transform)
    {
        this.sprite = sprite;
        this.transform = transform;
    }
    public bool OnCollide(Rectangle otherBounds)
    {
        return sprite.Bounds.Intersects(otherBounds);
    }
    public void TranslatePosition(Vector2 offset)
    {
        transform.Position = new Vector2(transform.Position.X + offset.X, transform.Position.Y + offset.Y);
    }
    public void Move(Vector2 offset)
    {
        transform.TranslatePosition(offset);
        sprite.UpdateBounds(transform);
    }
    public void Update(GameTime gameTime)
    {
        //transform.CheckBounds(sprite);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch);
    }
}
public struct Sprite
{
    public Texture2D SpriteSheet;
    public Rectangle Bounds;
    public float Scale;

    public Sprite(Texture2D texture, Rectangle bounds, float scale)
    {
        this.SpriteSheet = texture;
        this.Bounds = bounds;
        this.Scale = scale;
    }
    public void UpdateBounds(Transform transform)
    {
        Bounds = new Rectangle(transform.Position.ToPoint(), Bounds.Size);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(SpriteSheet, Bounds, Color.White);
    }
}
public struct Transform
{
    public Vector2 Position;
    public Vector2 Direction;
    public float Rotation;
    public Transform(Vector2 position, Vector2 direction, float rotation)
    {
        this.Position = position;
        this.Direction = direction;
        this.Rotation = rotation;
    }
    public void TranslatePosition(Vector2 offsetVector)
    {
        //Position = new Vector2(Position.X + offsetVector.X, Position.Y + offsetVector.Y);
        //quicker version 
        Position += offsetVector;
    }
}