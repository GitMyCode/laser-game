using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    class StateMultiplayer : StateBase
    {
        StateSingle refToSingle; // behaviour of the game
        public override void Awake()
        {

            //Create room
            refToSingle = new StateSingle();
            refToSingle.Awake();
        }

        public override void Update()
        {
            refToSingle.Update();
        }


        public override void OnUIInteract(GameObject reference)
        {
            refToSingle.OnUIInteract(reference);

        }

    }
