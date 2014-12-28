using UnityEngine;
using System.Collections;

public class MyPlayer : GameBehaviours, IPlayer{

    public enum PlayerState
    {
        Idle = 0,
        Playing = 1,
        Dead = 2,
        Winner = 3
    }

    private PlayerState state;
    private int life;
    private int energy;
    private string name;

    public GameObject[] zones;
    public GameObject[] goals;
    public VectorGrid vectorGrid;

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



    public bool tryRemoveLife( int quantite)
    {
        if (Life - quantite > 0)
        {
            Life -= quantite;
            return true;
        }
        State = PlayerState.Dead;
        Life = 0;
        return false;
    }


    public bool tryAddEnergy( int quantite)
    {
        if (Energy < 5)
        {
            Energy += quantite;
            if (Energy > 5)
            {
                Energy = 5;
            }
            return true;
        }
        return false;
    }
    public bool tryRemoveEnergy( int quantite)
    {
        if (quantite >= 0 && Energy >= quantite)
        {
            Energy -= quantite;
            return true;
        }
        return false;
    }


    public virtual void Die()
    {

    }




    #region helper methods

    public void damageEffect(Vector3 point)
    {
        vectorGrid.AddGridForce(point, -3 * 0.1f, 4.0f, Color.blue, true);
    }

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

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

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
        return "";
        //return string.Format("life : {0},\n energy: {1} ", Life, Energy);
    }



    public PlayerState State
    {
        get
        {
            return state;
        }
        set
        {
            if (value != State)
            {
                if (value == MyPlayer.PlayerState.Dead)
                {
                    Die();
                }
                state = value;
            }
        }
    }

}
