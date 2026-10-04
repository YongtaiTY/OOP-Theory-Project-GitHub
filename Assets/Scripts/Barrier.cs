using UnityEngine;

public class Barrier : Obstacles //INHERITANCE 
{
    public override float YPosition => 0.4183477f; //POLYMORPHISM
    public override float ZMinPosition => base.ZMinPosition + 0.5f; //563.5+0.5=564 //POLYMORPHISM
    public override float ZMaxPosition => base.ZMaxPosition - 0.5f; //576.5-0.5=576 //POLYMORPHISM
}
