using System.Collections;
using System.Collections.Generic;
using Managers.DataManager;
using UnityEngine;

public class DeleteUI : MonoBehaviour
{
    public void DeleteFile()
    {
        DataManager.Instance.DeleteData();
    }
}
