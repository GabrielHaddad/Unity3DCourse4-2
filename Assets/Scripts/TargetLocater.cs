using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocater : MonoBehaviour
{
    [SerializeField] Transform towerWeapon;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;

    void Start()
    {

    }

    void Update()
    {
        FindClosestTarget();
        AimWeapom();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                maxDistance = targetDistance;
                closestTarget = enemy.transform;
            }
        }

        target = closestTarget;
    }

    void AimWeapom()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);

        Quaternion targetRotation = Quaternion.LookRotation(target.position - towerWeapon.position);
        towerWeapon.rotation = Quaternion.Slerp(towerWeapon.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //towerWeapon.LookAt(target);

        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
