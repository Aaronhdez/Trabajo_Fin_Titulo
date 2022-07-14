using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] private List<GameObject> waypoints;
    public List<GameObject> Waypoints { get => waypoints; set => waypoints = value; }

}
