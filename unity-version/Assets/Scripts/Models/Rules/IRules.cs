using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IRules
{


    bool hasRules(CollidableEvent e);
    void applyRules(CollidableEvent e);

}
