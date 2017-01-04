using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionData : MonoBehaviour
{
    public int ed_NumberOfExpeditions;
    int[] ed_CollectedTreasure = new int[16];//temporary variables, should be found in a .c file labelled "CONSTANTS"
    int[] ed_CollectedArtefacts = new int[16];
    int[] ed_MostUsedParty = new int[16];
}
