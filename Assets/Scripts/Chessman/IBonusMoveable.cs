using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBonusMoveable
{
    bool IsOnStartingPosition();
    Tile AddBonusTileToMove();
}
