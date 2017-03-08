using UnityEngine;
using System.Collections;

public class BlockDigger : MonoBehaviour
{
    public Transform playerCamera;

    public LayerMask digMask;
    public float digRate = 0.5f;
    public float digRange = 2f;
    public int digDamage = 1;

    float _lastDigTime;

    RaycastHit _lastHit;

    public DestructableBlock _lastBlockHit;

	void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            TryDig();
        }
        else 
        {
            ResetLastHitBlock();
        }
	}

    void TryDig()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo, digRange, digMask))
        {
            DestructableBlock db = hitInfo.transform.GetComponent<DestructableBlock>();
            if (db != null)
            {
                _lastHit = hitInfo;
                // Same block
                if (db == _lastBlockHit)
                {
                    if(_lastDigTime + digRate < Time.time)
                    {
                        _lastDigTime = Time.time;
                        _lastBlockHit.DamageBlock(digDamage);
                        SendMessage("OnDigHit", hitInfo, SendMessageOptions.DontRequireReceiver);
                    }
                }
                // Different block
                else
                {
                    ResetLastHitBlock();

                    _lastDigTime = Time.time;
                    _lastBlockHit = db;
                    _lastBlockHit.DamageBlock(digDamage);
                    SendMessage("OnDigHit", hitInfo, SendMessageOptions.DontRequireReceiver);
                }
            }
            // No block
            else
            {
                ResetLastHitBlock();
                SendMessage("OnDigMiss", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void ResetLastHitBlock()
    {
        if (_lastBlockHit != null)
        {
            _lastBlockHit.ResetBlock();
            _lastBlockHit = null;
        }

    }

    public RaycastHit GetLastHit()
    {
        return _lastHit;
    }
}
