using UnityEngine;
using System.Collections;

public class CBD_Object : CustomBlockData
{

    [SerializeField] private ObjectEnum _objectType;

    public ObjectEnum ObjectType { get { return _objectType;  } }
}
