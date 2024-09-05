using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBonusMoveable
{
    bool HasBonusMove();
    void UpdateSatusBonusMove();
    PositionOnGrid ReturnBonusMovePattern();
}
