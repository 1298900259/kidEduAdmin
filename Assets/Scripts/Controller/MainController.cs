using App.Base;
using App.Generic;
using App.Model;
using App.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace App.Controller
{


    public class MainController : SingletonMono<MainController>
    {
        public View vw;

        int start;
        int end;

        StudentAdmin admin;

        List<int> intArray;
        List<int> orderedArray;

        public Dictionary<RoleInfo, StudentInfo> studentDict;
        public event Action<TableGroup, RoleType> ShowEvent;


        public override void Awake()
        {
            base.Awake();
            intArray = new List<int>();
            orderedArray = new List<int>();
            studentDict = new Dictionary<RoleInfo, StudentInfo>(42);
        }

        void Start()
        {
            admin = StudentAdmin.Instance;
            vw = View.Instance;

            vw.Btn_AutoOrder.onClick.AddListener(AutoOrder);
            vw.Btn_AutoOrder.onClick.AddListener(HandOrder);
            vw.Btn_Back.onClick.AddListener(() => { vw.UI_MainMenu.SetActive(true); vw.UI_GroupOrder.SetActive(false); });
            vw.Btn_Commit.onClick.AddListener(CommitId);
            vw.Btn_Over.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            });
            StudentAdmin.Instance.Ctor();
            vw.Btn_Excel.onClick.AddListener(() => {
                CreateExcel.Instance.SaveExcel(StudentAdmin.Instance.GetDB());
            });
            vw.Btn_ExcelTotal.onClick.AddListener(() => {
                CreateExcel.Instance.SaveExcelTotal(StudentAdmin.Instance.GetDB());
            });
            vw.Btn_PrintExcel.onClick.AddListener(() =>
            {
                vw.UI_CreateExcel.SetActive(true);
            });
            vw.Btn_BackGroupOrder.onClick.AddListener(() =>
            {
                vw.UI_GroupOrder.SetActive(true);
                vw.UI_CreateExcel.SetActive(false);
            });
        }

        void HandOrder()
        {

        }

        void AutoOrder()
        {
            if (!Check()) return;
            GetIdArray();
            OrderByRandom();
            EnterGroupOrder();
            ShowAll();
        }

        void EnterGroupOrder()
        {
            vw.UI_MainMenu.SetActive(false);
            vw.UI_GroupOrder.SetActive(true);
        }

        bool Check()
        {
            start = 0;
            end = 0;

            if (!int.TryParse(vw.Input_Id_Begin.text, out start)) return false;
            if (!int.TryParse(vw.Input_Id_End.text, out end)) return false;
            if (end < start) return false;
            if (start < 0 || end < 0) return false;
            return true;
        }

        void GetIdArray()
        {
            intArray.Clear();
            intArray = new List<int>(end - start >= 41 ? 42 : end - start+1);
            for (int i = 0; i < intArray.Capacity; i++)
            {
                intArray.Add(i+start);
                Debug.Log(i+start);
            }

            foreach (var item in intArray)
            {
                Debug.Log(item);
            }
        }

        void OrderByRandom()
        {
            orderedArray.Clear();
            studentDict.Clear();
            orderedArray = new List<int>(intArray.Count);
            for (int i = intArray.Count; 0 < i; i--)
            {
                int random = UnityEngine.Random.Range(0, intArray.Count);
                orderedArray.Add(intArray[random]);
                intArray.Remove(intArray[random]);
            }

            foreach (int member in orderedArray)
            {
                StudentInfo temp;
                int numPos = orderedArray.IndexOf(member);
                int groupPos = numPos / 7;
                temp.id = member;
                temp.group_pos = numPos % 7;
                temp.group = groupPos switch
                {
                    0 => "A组",
                    1 => "B组",
                    2 => "C组",
                    3 => "D组",
                    4 => "E组",
                    5 => "F组",
                    _ => "null"
                };

                RoleInfo roleInfo;
                roleInfo.group = temp.group switch
                {
                    "A组" => TableGroup.A组,
                    "B组" => TableGroup.B组,
                    "C组" => TableGroup.C组,
                    "D组" => TableGroup.D组,
                    "E组" => TableGroup.E组,
                    "F组" => TableGroup.F组,
                };
                roleInfo.role = temp.group_pos switch
                {
                    0 => RoleType.Parents,
                    1 => RoleType.SchoolLeader,
                    2 => RoleType.KidA,
                    3 => RoleType.KidB,
                    4 => RoleType.LifeTeacher,
                    5 => RoleType.HeadTeacher,
                    6 => RoleType.ProbationTeacher
                };

                studentDict.Add(roleInfo, temp);
            }

        }

        void ShowAll()
        {
            ShowEvent?.Invoke(TableGroup.All, RoleType.All);
        }

        void CommitId()
        {
            ClearId();
            Task.Run(() =>
            {
                foreach (int member in orderedArray)
                {
                    StudentInfo temp;
                    int numPos = orderedArray.IndexOf(member);
                    int groupPos = numPos / 7;
                    temp.id = member;
                    temp.group_pos = numPos % 7;
                    temp.group = groupPos switch
                    {
                        0 => "A组",
                        1 => "B组",
                        2 => "C组",
                        3 => "D组",
                        4 => "E组",
                        5 => "F组",
                        _ => "null"
                    };

                    admin.Commit(temp);
                }
                Debug.Log("Commit Success!");
            });
#if UNITY_EDITOR
            Debug.Log("Commiting!");
#endif
        }

        void ClearId()
        {
            admin.Clear();
        }

        public StudentInfo GetRoleInfo(TableGroup tableGroup, RoleType roleType)
        {
            RoleInfo temp;
            temp.group = tableGroup;
            temp.role = roleType;
            if (studentDict.ContainsKey(temp))
            {
                return studentDict[temp];
            }
            return new StudentInfo();
        }

        public void ExchangeStudentInfo(RoleInfo a, RoleInfo b)
        {
            if (studentDict.ContainsKey(a) && studentDict.ContainsKey(b))
            {
                StudentInfo c = studentDict[a];
                studentDict[a] = studentDict[b];
                studentDict[b] = c;
                ShowEvent(a.group, a.role);
                ShowEvent(b.group, b.role);
            }
        }


    }


}
