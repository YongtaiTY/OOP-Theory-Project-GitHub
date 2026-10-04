using UnityEngine;

public class Fence : Obstacles //INHERITANCE 
{
    public override float YPosition => 1.315251f; 
    public override float ZMinPosition => base.ZMinPosition + 1.1f; //563.5+1.1=564.6 //POLYMORPHISM
    public override float ZMaxPosition => base.ZMaxPosition - 0.44f; //576.5-0.44=576.06 //POLYMORPHISM

    
}
