using UnityEngine;
using System.Collections;

public class Player : GameBehaviours, IPlayer{


    private int life;
    private int energy;
    private EPlayer playerEnum;

    public GameObject[] zones;
    public GameObject[] goals;

    protected GUIText textOutput;

    public Typee choose;
    public IPlayer playerImpl;
    public enum Typee{AI, HUMAN} 
    // peut etre ajouter une prefab pour un style de ligne et cercle



    protected void Awake()
    {
        
       
    }

    protected override void Start()
    {
        textOutput = GetComponent<GUIText>();
        life = 5;
        energy = 5;
        base.Start();

        float scalex = (float)(Screen.width) / 320.0f; //your scale x
        float scaley = (float)(Screen.height) / 480.0f; //your scale y
        Vector2 pixOff = textOutput.pixelOffset; //your pixel offset on screen
        int origSizeText = textOutput.fontSize;
        textOutput.pixelOffset = new Vector2(pixOff.x * scalex, pixOff.y * scaley);
        textOutput.fontSize = (int)(origSizeText * scalex);
    }
        

    protected override void GameUpdate()
    {
        base.GameUpdate();


    }

    protected override void GameFixedUpdate()
    {
        textOutput.text = ToString();
    }


    #region helper methods
    public Vector3 pointToWorlCam(Vector3 p)
    {
        Vector3 convertedPoint = Camera.main.ScreenToWorldPoint(p);
        convertedPoint.z = 0.0f;
        return convertedPoint;
    }

    public bool isInZone(Vector3 point)
    {
        foreach (GameObject zone in zones)
        {
            if (zone.transform.collider2D.bounds.Contains(point))
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Interface implement

    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
        }
    }

    public int Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
        }
    }

    public EPlayer PlayerEnumType
    {
        get
        {
            return playerEnum;
        }
        set
        {
            playerEnum = value;
        }
    }


    public void playerTurn()
    {
        throw new System.NotImplementedException();
    }

    public GameObject[] Goals
    {
        get
        {
            return goals;
        }
        set
        {
            goals = value;
        }
    }
    public GameObject[] Zones
    {
        get
        {
            return zones;
        }
        set
        {
            zones = value;
        }
    }


    #endregion

    public override string ToString()
    {
        return string.Format("life : 0,\n energy: 1 ", Life, Energy);
    }




}
