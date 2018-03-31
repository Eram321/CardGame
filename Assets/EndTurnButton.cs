using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core { 
    public class EndTurnButton : MonoBehaviour {

        public delegate void TurnEnd();
        public event TurnEnd OnTurnEnd;

        private bool active;
        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        void OnMouseDown()
        {
            if (!active) return;

            OnTurnEnd();
        }


    }
}
