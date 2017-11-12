using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour {

    abstract public void launch();

    abstract public bool selectable();

    abstract public void highlight(bool h);
}
