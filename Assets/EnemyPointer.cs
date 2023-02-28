using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Camera _camera;
    [SerializeField] Transform _pointerIconTranform;

    void Start(){
    }

    void Update()
    {    // {
    //     Vector3 fromPlayerToEnemy = transform.position - _playerTransform.position;
    //     Ray ray = new Ray(_playerTransform.position, fromPlayerToEnemy);
    //     Debug.DrawRay(_playerTransform.position, fromPlayerToEnemy);

    //     Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

    //     float minDistance = Mathf.Infinity;
        
    //     int planeIndex = 0;
    //     for(int i =0; i<4; i++){
    //         if(planes[i].Raycast(ray, out float distance)){
    //             if(distance < minDistance){
    //                 minDistance = distance;
    //                 planeIndex = i;
    //             }
    //         }
    //     }
    //     minDistance = Mathf.Clamp(minDistance, 0, fromPlayerToEnemy.magnitude);
    //     Vector3 worldPosition = ray.GetPoint(minDistance);
    //     _pointerIconTranform.position = worldPosition;
    //     _pointerIconTranform.rotation = GetIconRotation(planeIndex);

    }

    Quaternion GetIconRotation(int planeIndex){
        if(planeIndex == 0){
            return Quaternion.Euler(0f, 0f, 90f);
        }
        
        if(planeIndex == 1){
            return Quaternion.Euler(0f, 0f, -90f);
        }
        
        if(planeIndex == 2){
            return Quaternion.Euler(0f, 0f, 180f);
        }
        
        if(planeIndex == 3){
            return Quaternion.Euler(0f, 0f, 0f);
        }

        return Quaternion.identity;
    }
}
