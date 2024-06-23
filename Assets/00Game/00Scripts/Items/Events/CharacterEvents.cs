using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents : MonoBehaviour
{
    
        //character dmg and dmg value
        public static UnityAction<GameObject, float> characterDamaged;

        //character healed and amount healed
        public static UnityAction<GameObject, float> characterHealed;
        public static UnityAction lost;
        public static UnityAction won;

}