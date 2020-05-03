using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : MonoBehaviour
{
    public FlameJet flameJetPrefab;
    public Vector2 flameJetDirection;
    public float flameJetInterval = 2f;
    public float flameJetDuration = 1f;

    private FlameJet flameJetObject;
    private bool isFlameTrapEnabled;
    
    void Start()
    {
        // Setup flame jet object
        Quaternion rotation = Quaternion.FromToRotation(Vector2.right, flameJetDirection);
        Vector3 offset = flameJetDirection.normalized;
        flameJetObject = Instantiate(flameJetPrefab, transform.position + offset, rotation);

        isFlameTrapEnabled = true;
        StartCoroutine(FlameJetCycle());
    }

    private IEnumerator FlameJetCycle()
    {
        while (isFlameTrapEnabled)
        {
            flameJetObject.SetFlameJetActive(true);
            // Set flame jet animation active
            yield return new WaitForSeconds(flameJetDuration);
            
            flameJetObject.SetFlameJetActive(false);
            // Set flame jet animation inactive
            yield return new WaitForSeconds(flameJetInterval - flameJetDuration);
        }
    }
}
