using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RoundManagerTest
{
    GameObject gameManager;
    GameObject player;

    [SetUp]
    public void SetUp() {
        gameManager = Resources.Load<GameObject>("GameManager");
        player = Resources.Load<GameObject>("PlayerPrefab");
    }

    [Test]
    public void RoundStatus_is_changed_when_round_starts() {
        var roundManager = gameManager.GetComponent<RoundManager>();
        var status = roundManager.roundStarted;

        roundManager.StartRound();

        status = roundManager.roundStarted;
        Assert.IsTrue(status);
    }

    [Test]
    public void Enemies_are_spawned_when_round_starts() {
        var roundManager = gameManager.GetComponent<RoundManager>();

        roundManager.StartRound();

        var enemiesSpawned = roundManager.enemiesAlive;
        Assert.AreNotEqual(enemiesSpawned, 0);
    }
}
