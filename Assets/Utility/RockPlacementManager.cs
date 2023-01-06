using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPlacementManager : MonoBehaviour
{
    public List<Vector2> coordinates1 = new List<Vector2>();
    public List<Vector2> coordinates2 = new List<Vector2>();
    public GameObject rockShadow;
    public GameObject fallingRock;
    public GameObject rockCombo;

    private void Awake()
    {
        // coordinates1.Add(new Vector2(-29.5f, -11.5f));
        // //coordinates1.Add(new Vector2(-22.6f, 3f));
        // coordinates1.Add(new Vector2(-36.3f, 12.6f));
        // coordinates1.Add(new Vector2(-19f, 15.8f));
        coordinates1.Add(new Vector2(-14.9f, 5.6f)); // this
        //coordinates1.Add(new Vector2(-6.1f, 14.3f));
        coordinates1.Add(new Vector2(-6.4f, -15f));
        coordinates1.Add(new Vector2(8.9f, -15.2f));
        coordinates1.Add(new Vector2(13.3f, 12.2f));
        coordinates1.Add(new Vector2(28.7f, -7.2f));
        coordinates1.Add(new Vector2(3.7f, 6.4f));
        //coordinates1.Add(new Vector2(10f, -4.8f));
        coordinates1.Add(new Vector2(29.6f, 3f));
        coordinates1.Add(new Vector2(25.5f, 13.9f));
        coordinates1.Add(new Vector2(-3.4f, -1f));

        // foreach (var xy in coordinates1)
        // {
        //     coordinates2.Add(new Vector2(xy.x, xy.y - 15f));
        // }

    }
}

/* old
 *        coordinates1.Add(new Vector2(-29.5f, -26.5f));
        coordinates1.Add(new Vector2(-22.6f, -18f));
        coordinates1.Add(new Vector2(-36.3f, -3.6f));
        coordinates1.Add(new Vector2(-19f, 0.8f));
        coordinates1.Add(new Vector2(-14.9f, -10.6f));
        coordinates1.Add(new Vector2(-6.1f, -1.3f));
        coordinates1.Add(new Vector2(-6.4f, -30f));
        coordinates1.Add(new Vector2(8.9f, -30.2f));
        coordinates1.Add(new Vector2(13.3f, -3.2f));
        coordinates1.Add(new Vector2(28.7f, -22.2f));
        coordinates1.Add(new Vector2(3.7f, -9.4f));
        coordinates1.Add(new Vector2(10f, -19.8f));
        coordinates1.Add(new Vector2(29.6f, -12f));
        coordinates1.Add(new Vector2(25.5f, -2.9f));
        coordinates1.Add(new Vector2(-3.4f, -16f));
 *
 * 
 */
