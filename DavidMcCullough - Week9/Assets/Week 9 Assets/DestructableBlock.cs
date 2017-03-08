using UnityEngine;
using System.Collections;

public class DestructableBlock : MonoBehaviour
{
    public int blockHealth = 4;
    int _currentHealth;

    public void DamageBlock(int damageValue)
    {
        _currentHealth += damageValue;
        
        if(_currentHealth >= blockHealth)
        {
            // Block is destroyed, do cool stuff
            SendMessage("OnBlockDestroyed", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        else
        {
            SendMessage("OnBlockDamaged",SendMessageOptions.DontRequireReceiver);
        }
    }

    public void ResetBlock()
    {
        _currentHealth = 0;
        SendMessage("OnBlockDamaged", SendMessageOptions.DontRequireReceiver);
    }

    public int GetRemainingHealth()
    {
        return blockHealth - _currentHealth;
    }
}
