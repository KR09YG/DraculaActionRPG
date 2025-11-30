using System.Collections;
using UnityEngine;

public interface IEnemyMove
{
    public abstract IEnumerator Move(float moveRange, float moveSpeed);
    
}
