﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpiderSpawner : MonoBehaviour
{
    SpiderSpawnerPoint[] _spawners;
    PhotonView _view;

    public void StartSpawning()
    {
        _view = GetComponent<PhotonView>();
        if (!_view.IsMine)
            return;
        _spawners = FindObjectsOfType<SpiderSpawnerPoint>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (!ServerNetwork.Instance.PlayerCanMove || _spawners == null || _spawners.Length <= 0)
                yield return null;

            var random = Random.Range(0, _spawners.Length);

            yield return new WaitForSeconds(Random.Range(13f, 15f));

            if (!ServerNetwork.Instance.PlayerCanMove)
                yield return null;

            while (Physics.OverlapSphere(_spawners[random].transform.position, 5f).Length > 0)
            {
                yield return null;
            }

            if (ServerNetwork.Instance.PlayerCanMove)
            {
                var spider = PhotonNetwork.Instantiate("Spider", _spawners[random].startWaypoint.transform.position, Quaternion.identity).GetComponent<SpiderBehaviour>();
                if (spider)
                {
                    spider.SetWaypoint(_spawners[random].startWaypoint);
                }
            }

        }
    }
}
