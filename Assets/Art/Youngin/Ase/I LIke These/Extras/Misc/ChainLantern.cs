using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChainLantern : MonoBehaviour
{
    [SerializeField] private int ChainLength;
    [SerializeField] private int PivotDistance;

    [SerializeField] private GameObject Anchor;
    [SerializeField] private GameObject Caboose;

    private Stack<HingeJoint2D> joints;



    private int previousChainLength;

    private void Awake()
    {
        previousChainLength = ChainLength;
    }

    private void Update()
    {
        if (previousChainLength == ChainLength)
            return;

        if (previousChainLength > ChainLength)
        {
            
        }
    }
}
