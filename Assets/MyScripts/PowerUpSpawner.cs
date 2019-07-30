﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerUpSpawner : MonoBehaviour
{
    PowerUpSpawnPoints[] _spawners;

    public void StartSpawning()
    {
        _spawners = FindObjectsOfType<PowerUpSpawnPoints>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if(_spawners == null || _spawners.Length <= 0)
                yield return null;

            var random = Random.Range(0, _spawners.Length);

            while (Physics.OverlapSphere(_spawners[random].transform.position, 2f, Utility.LayerNumberToMask(11)).Length > 0)
            {
                yield return null;
            }

            if (!ServerNetwork.Instance.PlayerCanMove)
                yield return null;

            yield return new WaitForSeconds(Random.Range(10f, 20f));

            if (!ServerNetwork.Instance.PlayerCanMove)
                yield return null;

            PhotonNetwork.Instantiate("PowerUp", _spawners[random].transform.position, Quaternion.Euler(new Vector3 (-90,0,0)));
        }
    }
}