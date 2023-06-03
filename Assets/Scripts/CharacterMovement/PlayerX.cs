using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerX : MonoBehaviour, ICharacterComponent
{
    [SerializeField]
    private CharacterModel _character = new CharacterModel();
    public CharacterModel character { get { return _character; } }
}
