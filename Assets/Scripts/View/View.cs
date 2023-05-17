using App.Base;
using UnityEngine;
using UnityEngine.UI;

namespace App.Repository
{
    public class View : SingletonMono<View> 
    {
        [Header("UI")]
        public GameObject UI_MainMenu;
        public GameObject UI_GroupOrder;

        [Header("Button")]
        public Button Btn_AutoOrder;
        public Button Btn_Commit;
        public Button Btn_Back;
        public Button Btn_HandOrder;

        [Header("InputField")]
        public InputField Input_Id_Begin;
        public InputField Input_Id_End;
        public InputField Input_Id_Order;

        [Header("DropDown")]
        public Dropdown Dropdown_Group;
        public Dropdown Dropdown_RoleType;


        [Header("Text")]
        public Text[] Txt_AllId;
        public Text Txt_AGroup_Parent;
        public Text Txt_AGroup_Leader;
        public Text Txt_AGroup_Boy;
        public Text Txt_AGroup_Girl;
        public Text Txt_AGroup_LT;
        public Text Txt_AGroup_HM;
        public Text Txt_AGroup_PT;
        [Space]
        public Text Txt_BGroup_Parent;
        public Text Txt_BGroup_Leader;
        public Text Txt_BGroup_Boy;
        public Text Txt_BGroup_Girl;
        public Text Txt_BGroup_LT;
        public Text Txt_BGroup_HM;
        public Text Txt_BGroup_PT;
        [Space]
        public Text Txt_CGroup_Parent;
        public Text Txt_CGroup_Leader;
        public Text Txt_CGroup_Boy;
        public Text Txt_CGroup_Girl;
        public Text Txt_CGroup_LT;
        public Text Txt_CGroup_HM;
        public Text Txt_CGroup_PT;
        [Space]
        public Text Txt_DGroup_Parent;
        public Text Txt_DGroup_Leader;
        public Text Txt_DGroup_Boy;
        public Text Txt_DGroup_Girl;
        public Text Txt_DGroup_LT;
        public Text Txt_DGroup_HM;
        public Text Txt_DGroup_PT;
        [Space]
        public Text Txt_EGroup_Parent;
        public Text Txt_EGroup_Leader;
        public Text Txt_EGroup_Boy;
        public Text Txt_EGroup_Girl;
        public Text Txt_EGroup_LT;
        public Text Txt_EGroup_HM;
        public Text Txt_EGroup_PT;
        [Space]
        public Text Txt_FGroup_Parent;
        public Text Txt_FGroup_Leader;
        public Text Txt_FGroup_Boy;
        public Text Txt_FGroup_Girl;
        public Text Txt_FGroup_LT;
        public Text Txt_FGroup_HM;
        public Text Txt_FGroup_PT;

        public Button Btn_Over;

        public GameObject UI_CreateExcel;
        public Button Btn_PrintExcel;
        public Button Btn_Excel;
        public Button Btn_ExcelTotal;//15days
        public Button Btn_BackGroupOrder;
    }
}

