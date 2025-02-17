﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AsteroidSpawner : MonoBehaviourPun
{
    Boundary _boundary;
    PhotonView _view;

    public void StartSpawning()
    {
        _view = GetComponent<PhotonView>();
        if (!_view.IsMine)
            return;
        _boundary = FindObjectOfType<Boundary>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (!ServerNetwork.Instance.PlayerCanMove)
                yield return null;

            yield return new WaitForSeconds(Random.Range(3f, 5f));

            if (ServerNetwork.Instance.PlayerCanMove)
            {
                int side = Random.Range(0, 4);
                var asteroid = PhotonNetwork.Instantiate("Asteroid" + Random.Range(0, 3).ToString(), _boundary.GetRandomPositionOnBoundary(side), Quaternion.identity).GetComponent<AsteroidBehaviour>();
                if (asteroid)
                {
                    asteroid.SetVelocity(_boundary.GetDirectionBySide(side));
                }
            }
        }
    }
}
