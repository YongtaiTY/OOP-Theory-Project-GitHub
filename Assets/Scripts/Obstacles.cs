using UnityEngine;


public abstract class Obstacles : MonoBehaviour 
{
    public abstract float YPosition {get;} 
    public virtual float ZMinPosition => Ground.Instance.zMin; //563.5 
    public virtual float ZMaxPosition => Ground.Instance.zMax; //576.5 

   

}
