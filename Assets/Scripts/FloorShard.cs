using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class FloorShard : MonoBehaviour {

    public SpriteShapeController spriteShapeController;
    private List<AngleRange> list;
    private Spline sp;
    // Use this for initialization
    private void Awake()
    {
        /*
        list = spriteShapeController.spriteShape.angleRanges;

        sp = spriteShapeController.spline;

        sp.InsertPointAt(2, new Vector3(-4f, -1f, 0f));
        sp.SetTangentMode(2, ShapeTangentMode.Continuous);
        sp.SetLeftTangent(2, new Vector3(-1.5f, -1.5f, 0));


        //sp.SetRightTangent(2, new Vector3(1, 0, 0));




        foreach (AngleRange item in list)
        {

        }
     
    */
    }
    void Start () {


        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
