using System;
using UnityEngine;
using App.Controller;
using UnityEngine.UI;
using App.Generic;
using UnityEngine.EventSystems;

namespace App.Model
{
    public class TableInfo : MonoBehaviour
    {
        private Vector3 originPos;
        public RoleInfo tableInfo;
        public Text content;
        bool isDrag;
        Vector3 offset;

        public void Awake()
        {
            isDrag = false;
            content = GetComponent<Text>();
            originPos = this.transform.position;
        }

        public void OnEnable()
        {
            MainController.Instance.ShowEvent += UpdateContent;
        }

        public void OnDisable()
        {
            MainController.Instance.ShowEvent -= UpdateContent;
        }

        public void UpdateContent(TableGroup tableGroup, RoleType roleType)
        {
            if ((tableGroup == TableGroup.All && roleType == RoleType.All) ||
                (tableGroup == tableInfo.group && roleType == tableInfo.role) ||
                (tableGroup == TableGroup.All && roleType == tableInfo.role))
            {
                if (MainController.Instance.studentDict.ContainsKey(tableInfo))
                {
                    content.text = MainController.Instance.studentDict[tableInfo].id.ToString();
                }
                else
                {
                    content.text = "Пе";
                }
            }
        }



    }

}
