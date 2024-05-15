using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class ChainLantern : MonoBehaviour
{
    [SerializeField] private int ChainLength;
    [SerializeField] private Vector3 LinkDistance;
    [SerializeField] private GameObject Anchor;
    [SerializeField] private HingeJoint2D Caboose;
    [SerializeField] private GameObject Link;

    public List<HingeJoint2D> joints;

    private int previousChainLength;


    private void Update()
    {
        joints.Clear();

        var dumb = GetComponentsInChildren<HingeJoint2D>();
        foreach (var dummy in dumb)
        {
            joints.Add(dummy);
        }
        
        if (joints.Count == ChainLength)
            return;

        if (joints.Count < ChainLength)
        {
            Debug.Log("Adding Count: " + joints.Count + "    ChainLength: " + ChainLength);
            for (int i = joints.Count; i < ChainLength; i++)
            {
                AddLink(i);
            }
        }
        else
        {
            Debug.Log("Removing Count: " + joints.Count + "    ChainLength: " + ChainLength);
            for (int i = joints.Count -1; i >= ChainLength; i--)
            {
                RemoveLink(i);
            }
        }
    }

    private void AddLink(int index)
    {
        var newLink = Instantiate(Link, transform).GetComponent<HingeJoint2D>();
        joints.Add(newLink);

        if (index > 0)
        {
            var upperLink = joints[index - 1];
                    newLink.transform.localPosition = upperLink.transform.localPosition + LinkDistance;
            
                    newLink.connectedBody = upperLink.attachedRigidbody;
                    newLink.anchor = new Vector2(0, 0.2f);
                    newLink.connectedAnchor = new Vector2(0, -0.1f);
        }
        
        Caboose.connectedBody = newLink.attachedRigidbody;
        Caboose.transform.localPosition += LinkDistance;

    }

    private void RemoveLink(int index)
    {
        DestroyImmediate(joints[index].gameObject);
        joints.Remove(joints[index]);

        if (index - 1 < 0)
            return;
        Caboose.connectedBody = joints[index - 1].attachedRigidbody;
        Caboose.transform.localPosition -= LinkDistance;
    }
}
