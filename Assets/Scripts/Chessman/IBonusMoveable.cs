using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBonusMoveable
{
    bool IsOnStartingPosition();
    bool UsedBonusMove();
    Tile ReturnBonusTile();
}
